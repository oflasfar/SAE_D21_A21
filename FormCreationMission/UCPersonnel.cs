using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using System.IO;

namespace FormCreationMission
{
    public partial class UCPersonnel: UserControl
    {
        Form4 f;
        private bool chargementTermine = false;
        private bool estAdmin;
        public UCPersonnel(Form4 x)
        {
            InitializeComponent();
            f = x;
        }

        private void UCPersonnel_Load(object sender, EventArgs e)
        {
            estAdmin = false; // Indique si l'utilisateur est admin
            gbInformationCarriere.Visible = false; // Masquer le groupe d'informations de carrière au départ
            cbGradeNouveau.Visible = false;
            if (Connexion.Connec.State != ConnectionState.Open)
                Connexion.Connec.Open();

            string sqlCaserne = "SELECT id, nom FROM Caserne";
            using (SQLiteCommand cmd = new SQLiteCommand(sqlCaserne, Connexion.Connec))
            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                DataTable dtCaserne = new DataTable();
                dtCaserne.Load(reader);
                cbCaserne.DataSource = dtCaserne;
                cbCaserne.DisplayMember = "nom";
                cbCaserne.ValueMember = "id";
            }

            chargementTermine = true; //Autorise l'événement maintenant
            cbCaserne_SelectedIndexChanged(null, null); //Forcer un premier chargement des pompiers
        }

        private void cbCaserne_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            if (!chargementTermine || cbCaserne.SelectedValue == null)
                return;

            try
            {
                cbPompiers.DataSource = null;
                // Vérifie que l’ID caserne est bien un entier
                if (!int.TryParse(cbCaserne.SelectedValue.ToString(), out int idCaserne))
                {
                    MessageBox.Show("Erreur : ID caserne invalide.");
                    return;
                }

                // Ouvre la connexion si nécessaire
                if (Connexion.Connec.State != ConnectionState.Open)
                    Connexion.Connec.Open();

                // Requête SQL pour récupérer les pompiers de la caserne
                string sqlPompier = @"SELECT P.matricule, P.nom || ' ' || P.prenom AS nomComplet FROM Pompier P JOIN Affectation A ON P.matricule = A.matriculePompier WHERE A.idCaserne = @id";//Le || ' ' || c est pour la concatenation

                using (SQLiteCommand cmd = new SQLiteCommand(sqlPompier, Connexion.Connec))
                {
                    cmd.Parameters.AddWithValue("@id", idCaserne);//On ajoute la valeur de @id dans la requtte

                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        DataTable dtPompiers = new DataTable();
                        dtPompiers.Load(reader);
                        // Remplir la ComboBox des pompiers
                        cbPompiers.DataSource = dtPompiers;
                        cbPompiers.DisplayMember = "nomComplet";
                        cbPompiers.ValueMember = "matricule";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Erreur lors du chargement des pompiers : " + ex.Message);
            }
        }       
        private void cbPompiers_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            try
            {
                if (cbPompiers.SelectedItem is DataRowView row)
                {
                    if(gbInformationCarriere.Visible)
                        gbInformationCarriere.Visible = false; // Masquer le groupe d'informations de carrière si visible
                    int matricule = Convert.ToInt32(row["matricule"]);

                    if (Connexion.Connec.State != ConnectionState.Open)
                        Connexion.Connec.Open();

                    string sql = @"
                    SELECT P.nom, P.prenom, P.sexe, P.dateNaissance, P.type, 
                           P.portable, P.bip, P.dateEmbauche, P.codeGrade, P.enConge,
                           G.libelle AS gradeLibelle
                    FROM Pompier P
                    LEFT JOIN Grade G ON P.codeGrade = G.code
                    WHERE P.matricule = @mat";

                    using (SQLiteCommand cmd = new SQLiteCommand(sql, Connexion.Connec))
                    {
                        cmd.Parameters.AddWithValue("@mat", matricule);

                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                lblMatricule.Text = matricule.ToString();
                                lblNom.Text = reader["nom"].ToString();
                                lblPrenom.Text = reader["prenom"].ToString();
                                lblSexe.Text = reader["sexe"].ToString();
                                lblDateNaissance.Text = reader["dateNaissance"].ToString();
                                lblPortable.Text = reader["portable"].ToString();
                                lblBip.Text = reader["bip"].ToString();
                                lblEmbauche.Text = reader["dateEmbauche"].ToString();
                                txtGrade.Text = reader["gradeLibelle"].ToString();

                                //Chargement du champ enConge
                                chbConge.Checked = false;
                                if (reader["enConge"] != DBNull.Value)
                                {
                                    chbConge.Checked = Convert.ToBoolean(reader["enConge"]);
                                }

                                //Cocher le type
                                string type = reader["type"].ToString().Trim().ToLower();
                                if (type == "p")
                                    rdbProfessionnel.Checked = true;
                                else if (type == "v")
                                    rdbVolontaire.Checked = true;

                                //Affichage de l’image du grade
                                string codeGrade = reader["codeGrade"].ToString();
                                string imagePath = Path.Combine(@"C:\OMAR\S2\SAE-D21\ImagesGrades", codeGrade + ".png");
                                //On verifie si l image existe
                                if (File.Exists(imagePath))
                                {
                                    pbGrade.Image = Image.FromFile(imagePath);
                                    pbGrade.SizeMode = PictureBoxSizeMode.StretchImage;
                                }
                                else
                                {
                                    pbGrade.Image = null;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors du chargement du pompier : " + ex.Message);
            }
        }

        private void btnAjoutPompiers_Click_1(object sender, EventArgs e)
        {
            //verification si il est admin ou pas
            if (estAdmin)
            {
                // Si l'utilisateur est admin, on ouvre directement le formulaire de création
                Form3 formCreer = new Form3();
                formCreer.ShowDialog();
            }
            else
            {
                //Demander si il est admin ou pas
                FormConn formConnexion = new FormConn();
                formConnexion.ShowDialog();

                if (formConnexion.EstConnecte)
                {
                    estAdmin = true; // L'utilisateur est maintenant admin
                    // Connexion réussie → ouvre le formulaire de création du pompier
                    Form3 formCreer = new Form3();
                    formCreer.ShowDialog();
                }
                
            }
        }



        private void btnChanger_Click_1(object sender, EventArgs e)
        {
            if(estAdmin)
            {
                try
                {
                    if (cbGradeNouveau.Visible)
                    {
                        //Déjà visible  on copie le texte + image du grade
                        if (cbGradeNouveau.SelectedItem is DataRowView selectedRow)
                        {
                            string libelleGrade = selectedRow["libelle"].ToString();
                            string codeGrade = selectedRow["code"].ToString();
                            int matricule = Convert.ToInt32(lblMatricule.Text); // récupère le matricule

                            //Mise à jour du grade dans la base avec transaction
                            using (SQLiteTransaction transaction = Connexion.Connec.BeginTransaction())
                            {
                                try
                                {
                                    using (SQLiteCommand cmd = Connexion.Connec.CreateCommand())
                                    {
                                        cmd.Transaction = transaction;
                                        cmd.CommandText = "UPDATE Pompier SET codeGrade = @codeGrade WHERE matricule = @matricule";
                                        cmd.Parameters.AddWithValue("@codeGrade", codeGrade);
                                        cmd.Parameters.AddWithValue("@matricule", matricule);
                                        cmd.ExecuteNonQuery();
                                    }

                                    transaction.Commit();

                                    //Mise à jour visuelle
                                    txtGrade.Text = libelleGrade;

                                    string imagePath = Path.Combine(@"C:\OMAR\S2\SAE-D21\ImagesGrades", codeGrade + ".png");
                                    if (File.Exists(imagePath))
                                    {
                                        pbGrade.Image = Image.FromFile(imagePath);
                                        pbGrade.SizeMode = PictureBoxSizeMode.StretchImage;
                                    }
                                    else
                                    {
                                        pbGrade.Image = null;
                                    }
                                }
                                catch (Exception ex)
                                {
                                    transaction.Rollback();
                                    MessageBox.Show("❌ Erreur pendant la mise à jour du grade : " + ex.Message);
                                }
                            }
                        }
                        cbGradeNouveau.Visible = false; // cacher après sélection
                    }
                    else
                    {
                        //Première fois on affiche et on remplit la comboboxx
                        if (Connexion.Connec.State != ConnectionState.Open)
                            Connexion.Connec.Open();

                        string sqlGrade = "SELECT code, libelle FROM Grade";
                        using (SQLiteCommand cmdGrade = new SQLiteCommand(sqlGrade, Connexion.Connec))
                        using (SQLiteDataReader reader = cmdGrade.ExecuteReader())
                        {
                            DataTable dtGrades = new DataTable();
                            dtGrades.Load(reader);
                            cbGradeNouveau.DataSource = dtGrades;
                            cbGradeNouveau.DisplayMember = "libelle";
                            cbGradeNouveau.ValueMember = "code";
                        }
                        cbGradeNouveau.Visible = true;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("❌ Erreur : " + ex.Message);
                }
            }
            //Si c'est pas un admin on verifie
            else
            {
                FormConn formConnexion = new FormConn();
                formConnexion.ShowDialog();
                if(formConnexion.EstConnecte)
                {
                    estAdmin = true;
                    // L'utilisateur est maintenant admin
                    try
                    {
                        if (cbGradeNouveau.Visible)
                        {
                            // Déjà visible = on copie le texte + image du grade
                            if (cbGradeNouveau.SelectedItem is DataRowView selectedRow)
                            {
                                string libelleGrade = selectedRow["libelle"].ToString();
                                string codeGrade = selectedRow["code"].ToString();
                                int matricule = Convert.ToInt32(lblMatricule.Text); // récupère le matricule

                                //Mise à jour du grade dans la base avec transaction
                                using (SQLiteTransaction transaction = Connexion.Connec.BeginTransaction())
                                {
                                    try
                                    {
                                        using (SQLiteCommand cmd = Connexion.Connec.CreateCommand())
                                        {
                                            cmd.Transaction = transaction;
                                            cmd.CommandText = "UPDATE Pompier SET codeGrade = @codeGrade WHERE matricule = @matricule";
                                            cmd.Parameters.AddWithValue("@codeGrade", codeGrade);
                                            cmd.Parameters.AddWithValue("@matricule", matricule);
                                            cmd.ExecuteNonQuery();
                                        }

                                        transaction.Commit();

                                        //Mise à jour visuelle (affichage)
                                        txtGrade.Text = libelleGrade;

                                        string imagePath = Path.Combine(@"C:\OMAR\S2\SAE-D21\ImagesGrades", codeGrade + ".png");
                                        if (File.Exists(imagePath))
                                        {
                                            pbGrade.Image = Image.FromFile(imagePath);
                                            pbGrade.SizeMode = PictureBoxSizeMode.StretchImage;
                                        }
                                        else
                                        {
                                            pbGrade.Image = null;
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        transaction.Rollback();
                                        MessageBox.Show("❌ Erreur pendant la mise à jour du grade : " + ex.Message);
                                    }
                                }
                            }

                            cbGradeNouveau.Visible = false; // cacher après sélection
                        }
                        else
                        {
                            //Première fois on affiche et on remplit la ComboBox
                            if (Connexion.Connec.State != ConnectionState.Open)
                                Connexion.Connec.Open();

                            string sqlGrade = "SELECT code, libelle FROM Grade";
                            using (SQLiteCommand cmdGrade = new SQLiteCommand(sqlGrade, Connexion.Connec))
                            using (SQLiteDataReader reader = cmdGrade.ExecuteReader())
                            {
                                DataTable dtGrades = new DataTable();
                                dtGrades.Load(reader);
                                cbGradeNouveau.DataSource = dtGrades;
                                cbGradeNouveau.DisplayMember = "libelle";
                                cbGradeNouveau.ValueMember = "code";
                            }

                            cbGradeNouveau.Visible = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("❌ Erreur : " + ex.Message);
                    }
                }
            }
            
        }

        private void BtnPlusInformation_Click_1(object sender, EventArgs e)
        {
            if (!estAdmin)
            {
                FormConn formConnexion = new FormConn();
                formConnexion.ShowDialog();

                if (formConnexion.EstConnecte)
                {
                    estAdmin = true;
                    try
                    {
                        if (!gbInformationCarriere.Visible)
                        {
                            gbInformationCarriere.Visible = true;

                            if (cbPompiers.SelectedItem is DataRowView row)
                            {
                                int matricule = Convert.ToInt32(row["matricule"]);

                                if (Connexion.Connec.State != ConnectionState.Open)
                                    Connexion.Connec.Open();

                                //Charger toutes les casernes
                                string sqlToutesCaserne = "SELECT id, nom FROM Caserne";
                                SQLiteDataAdapter da = new SQLiteDataAdapter(sqlToutesCaserne, Connexion.Connec);
                                DataTable dtCaserne = new DataTable();
                                da.Fill(dtCaserne);
                                cbCaserneRattachement.DataSource = dtCaserne;
                                cbCaserneRattachement.DisplayMember = "nom";
                                cbCaserneRattachement.ValueMember = "id";

                                //Caserne actuelle
                                lstCaserneActuelle.Items.Clear();
                                string sqlCaserneActuelle = @"
                                SELECT A.dateA, C.nom 
                                FROM Affectation A JOIN Caserne C ON A.idCaserne = C.id 
                                WHERE A.matriculePompier = @mat AND A.dateFin IS NULL";

                                using (SQLiteCommand cmd = new SQLiteCommand(sqlCaserneActuelle, Connexion.Connec))
                                {
                                    cmd.Parameters.AddWithValue("@mat", matricule);
                                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                                    {
                                        if (reader.Read())
                                        {
                                            string dateA = reader["dateA"].ToString();
                                            string nomCaserne = reader["nom"].ToString();
                                            lstCaserneActuelle.Items.Add($"→ Depuis le {dateA} : {nomCaserne}");
                                        }
                                        else
                                        {
                                            lstCaserneActuelle.Items.Add("Aucune affectation en cours.");
                                        }
                                    }
                                }

                                //Habilitations
                                lstHabilitations.Items.Clear();
                                string sqlHabilitations = @"SELECT H.libelle FROM Habilitation H JOIN Passer P ON H.id = P.idHabilitation WHERE P.matriculePompier = @mat";
                                using (SQLiteCommand cmd2 = new SQLiteCommand(sqlHabilitations, Connexion.Connec))
                                {
                                    cmd2.Parameters.AddWithValue("@mat", matricule);
                                    using (SQLiteDataReader reader = cmd2.ExecuteReader())
                                    {
                                        while (reader.Read())
                                        {
                                            lstHabilitations.Items.Add(reader["libelle"].ToString());
                                        }
                                    }
                                }

                                //anciennes affectations
                                lstAffectationsPassees.Items.Clear(); //on utilise une autre ListBox ici
                                string sqlPassees = @"
                                SELECT A.dateA, A.dateFin, C.nom 
                                FROM Affectation A 
                                JOIN Caserne C ON A.idCaserne = C.id 
                                WHERE A.matriculePompier = @mat AND A.dateFin IS NOT NULL 
                                ORDER BY A.dateA DESC";

                                using (SQLiteCommand cmd3 = new SQLiteCommand(sqlPassees, Connexion.Connec))
                                {
                                    cmd3.Parameters.AddWithValue("@mat", matricule);
                                    using (SQLiteDataReader reader = cmd3.ExecuteReader())
                                    {
                                        bool aDesAnciennes = false;
                                        while (reader.Read())
                                        {
                                            aDesAnciennes = true;
                                            string dateDebut = reader["dateA"].ToString();
                                            string dateFin = reader["dateFin"].ToString();
                                            string nomCaserne = reader["nom"].ToString();
                                            lstAffectationsPassees.Items.Add($"→ {dateDebut} à {dateFin} : {nomCaserne}");
                                        }

                                        if (!aDesAnciennes)
                                            lstAffectationsPassees.Items.Add("Aucune affectation passée.");
                                    }
                                }
                            }
                        }
                        else
                        {
                            gbInformationCarriere.Visible = false;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("❌ Erreur : " + ex.Message);
                    }
                }
            }
            else
            {
                try
                {
                    if (!gbInformationCarriere.Visible)
                    {
                        gbInformationCarriere.Visible = true;
                        if (cbPompiers.SelectedItem is DataRowView row)
                        {
                            int matricule = Convert.ToInt32(row["matricule"]);

                            if (Connexion.Connec.State != ConnectionState.Open)
                                Connexion.Connec.Open();

                            // Charger toutes les casernes
                            string sqlToutesCaserne = "SELECT id, nom FROM Caserne";
                            SQLiteDataAdapter da = new SQLiteDataAdapter(sqlToutesCaserne, Connexion.Connec);
                            DataTable dtCaserne = new DataTable();
                            da.Fill(dtCaserne);
                            cbCaserneRattachement.DataSource = dtCaserne;
                            cbCaserneRattachement.DisplayMember = "nom";
                            cbCaserneRattachement.ValueMember = "id";

                            //Caserne actuelle
                            lstCaserneActuelle.Items.Clear();
                            string sqlCaserneActuelle = @"
                            SELECT A.dateA, C.nom 
                            FROM Affectation A JOIN Caserne C ON A.idCaserne = C.id 
                            WHERE A.matriculePompier = @mat AND A.dateFin IS NULL";

                            using (SQLiteCommand cmd = new SQLiteCommand(sqlCaserneActuelle, Connexion.Connec))
                            {
                                cmd.Parameters.AddWithValue("@mat", matricule);
                                using (SQLiteDataReader reader = cmd.ExecuteReader())
                                {
                                    if (reader.Read())
                                    {
                                        string nomCaserne = reader["nom"].ToString();
                                        string dateA = Convert.ToDateTime(reader["dateA"]).ToString("yyyy-MM-dd");
                                        lstCaserneActuelle.Items.Add($"→ Depuis le {dateA} : {nomCaserne}");
                                    }
                                    else
                                    {
                                        lstCaserneActuelle.Items.Add("Aucune affectation en cours.");
                                    }
                                }
                            }

                            //Habilitations
                            lstHabilitations.Items.Clear();
                            string sqlHabilitations = @"SELECT H.libelle FROM Habilitation H JOIN Passer P ON H.id = P.idHabilitation WHERE P.matriculePompier = @mat";
                            using (SQLiteCommand cmd2 = new SQLiteCommand(sqlHabilitations, Connexion.Connec))
                            {
                                cmd2.Parameters.AddWithValue("@mat", matricule);
                                using (SQLiteDataReader reader = cmd2.ExecuteReader())
                                {
                                    while (reader.Read())
                                    {
                                        lstHabilitations.Items.Add(reader["libelle"].ToString());
                                    }
                                }
                            }

                            //Anciennes affectations
                            lstAffectationsPassees.Items.Clear(); //on utilise une autre ListBox ici
                            string sqlPassees = @"
                            SELECT A.dateA, A.dateFin, C.nom 
                            FROM Affectation A 
                            JOIN Caserne C ON A.idCaserne = C.id 
                            WHERE A.matriculePompier = @mat AND A.dateFin IS NOT NULL 
                            ORDER BY A.dateA DESC";

                            using (SQLiteCommand cmd3 = new SQLiteCommand(sqlPassees, Connexion.Connec))
                            {
                                cmd3.Parameters.AddWithValue("@mat", matricule);
                                using (SQLiteDataReader reader = cmd3.ExecuteReader())
                                {
                                    bool aDesAnciennes = false;
                                    while (reader.Read())
                                    {
                                        aDesAnciennes = true;
                                        string dateDebut = Convert.ToDateTime(reader["dateA"]).ToString("yyyy-MM-dd");
                                        string dateFin = Convert.ToDateTime(reader["dateFin"]).ToString("yyyy-MM-dd");
                                        string nomCaserne = reader["nom"].ToString();
                                        lstAffectationsPassees.Items.Add($"→ {dateDebut} à {dateFin} : {nomCaserne}");
                                    }
                                    if (!aDesAnciennes)
                                        lstAffectationsPassees.Items.Add("Aucune affectation passée.");
                                }
                            }
                        }
                    }
                    else
                    {
                        gbInformationCarriere.Visible = false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("❌ Erreur : " + ex.Message);
                }

            }
        }

        private void btnMettreaJour_Click_1(object sender, EventArgs e)
        {
            //
            SQLiteTransaction transaction = null;

            try
            {
                int matricule = Convert.ToInt32(lblMatricule.Text);
                int nouvelleCaserne = Convert.ToInt32(cbCaserneRattachement.SelectedValue);

                //Récupérer l'ID de la caserne actuelle
                int idCaserneActuelle = -1;
                string sqlSelect = "SELECT idCaserne FROM Affectation WHERE matriculePompier = @mat AND dateFin IS NULL";
                using (SQLiteCommand cmd = new SQLiteCommand(sqlSelect, Connexion.Connec))
                {
                    cmd.Parameters.AddWithValue("@mat", matricule);
                    object result = cmd.ExecuteScalar();
                    if (result != DBNull.Value)
                        idCaserneActuelle = Convert.ToInt32(result);
                }

                // Vérifier si la caserne a changé
                bool caserneChange = idCaserneActuelle != nouvelleCaserne;

                transaction = Connexion.Connec.BeginTransaction();

                if (caserneChange)
                {
                    //Fermer ancienne affectation
                    string sqlUpdate = @"
                    UPDATE Affectation
                    SET dateFin = @dateFin
                    WHERE matriculePompier = @mat AND dateFin IS NULL";

                    using (SQLiteCommand cmdUpdate = new SQLiteCommand(sqlUpdate, Connexion.Connec, transaction))
                    {
                        cmdUpdate.Parameters.AddWithValue("@dateFin", DateTime.Now.ToString("yyyy-MM-dd"));
                        cmdUpdate.Parameters.AddWithValue("@mat", matricule);
                        cmdUpdate.ExecuteNonQuery();
                    }

                    //Nouvelle affectation
                    string nouvelleDate = DateTime.Now.AddSeconds(1).ToString("yyyy-MM-dd HH:mm:ss");


                    string sqlInsert = @"
                    INSERT INTO Affectation (matriculePompier, dateA, idCaserne)
                    VALUES (@matricule, @dateA, @idCaserne)";

                    using (SQLiteCommand cmdInsert = new SQLiteCommand(sqlInsert, Connexion.Connec, transaction))
                    {
                        cmdInsert.Parameters.AddWithValue("@matricule", matricule);
                        cmdInsert.Parameters.AddWithValue("@dateA", nouvelleDate);
                        cmdInsert.Parameters.AddWithValue("@idCaserne", nouvelleCaserne);
                        cmdInsert.ExecuteNonQuery();
                    }
                }

                //Mise à jour du champ enConge
                string sqlConge = "UPDATE Pompier SET enConge = @etatConge WHERE matricule = @matricule";

                using (SQLiteCommand cmdConge = new SQLiteCommand(sqlConge, Connexion.Connec, transaction))
                {
                    cmdConge.Parameters.AddWithValue("@etatConge", chbConge.Checked ? 1 : 0);
                    cmdConge.Parameters.AddWithValue("@matricule", matricule);
                    cmdConge.ExecuteNonQuery();
                }

                transaction.Commit();
                MessageBox.Show("✅ Mise à jour effectuée avec succès !");
            }
            catch (Exception ex)
            {
                transaction?.Rollback();
                MessageBox.Show("❌ Erreur pendant la mise à jour : " + ex.Message);
                return;
            }

            //Mise à jour des deux listes d'affichage
            try
            {
                lstCaserneActuelle.Items.Clear();
                lstAffectationsPassees.Items.Clear();

                //Liste des affectations passées
                string sqlPassees = @"
                SELECT A.dateA, A.dateFin, C.nom 
                FROM Affectation A 
                JOIN Caserne C ON A.idCaserne = C.id
                WHERE A.matriculePompier = @mat AND A.dateFin IS NOT NULL
                ORDER BY A.dateFin DESC";

                using (SQLiteCommand cmdPassees = new SQLiteCommand(sqlPassees, Connexion.Connec))
                {
                    cmdPassees.Parameters.AddWithValue("@mat", Convert.ToInt32(lblMatricule.Text));
                    using (SQLiteDataReader reader = cmdPassees.ExecuteReader())
                    {
                        if (!reader.HasRows)
                        {
                            lstAffectationsPassees.Items.Add("Aucune affectation passée.");
                        }
                        else
                        {
                            while (reader.Read())
                            {
                                DateTime dateDebut = Convert.ToDateTime(reader["dateA"]);
                                DateTime dateFin = Convert.ToDateTime(reader["dateFin"]);
                                string nomCaserne = reader["nom"].ToString();
                                lstAffectationsPassees.Items.Add($"→ {dateDebut:yyyy-MM-dd} à {dateFin:yyyy-MM-dd} : {nomCaserne}");
                            }
                        }
                    }
                }

                //Affectation en cours
                string sqlEnCours = @"
                SELECT A.dateA, C.nom 
                FROM Affectation A 
                JOIN Caserne C ON A.idCaserne = C.id
                WHERE A.matriculePompier = @mat AND A.dateFin IS NULL";

                using (SQLiteCommand cmdCours = new SQLiteCommand(sqlEnCours, Connexion.Connec))
                {
                    cmdCours.Parameters.AddWithValue("@mat", Convert.ToInt32(lblMatricule.Text));
                    using (SQLiteDataReader reader = cmdCours.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            DateTime dateDebut = Convert.ToDateTime(reader["dateA"]);
                            string nomCaserne = reader["nom"].ToString();
                            lstCaserneActuelle.Items.Add($"→ Depuis le {dateDebut:yyyy-MM-dd HH:mm:ss} : {nomCaserne}");
                        }
                        else
                        {
                            lstCaserneActuelle.Items.Add("Aucune affectation en cours.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Erreur d’affichage : " + ex.Message);
            }

        }


        private void btnQuitter_Click_1(object sender, EventArgs e)
        {
            f.actualiser();
        }

        

        private void rdbProfessionnel_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true; // Ignore key press events
        }

        private void rdbVolontaire_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true; // Ignore key press events
        }
    }
}
