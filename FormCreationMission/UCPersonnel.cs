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
        public UCPersonnel(Form4 x)
        {
            InitializeComponent();
            f = x;
        }

        private void UCPersonnel_Load(object sender, EventArgs e)
        {
            gbInformationCarriere.Visible = false; // Masquer le groupe d'informations de carrière au départ
            cbGradeNouveau.Visible = false;
            if (Connexion.Connec.State != ConnectionState.Open)
                Connexion.Connec.Open();

            string sqlCaserne = "SELECT id, nom FROM Caserne";
            using (var cmd = new SQLiteCommand(sqlCaserne, Connexion.Connec))
            using (var reader = cmd.ExecuteReader())
            {
                DataTable dtCaserne = new DataTable();
                dtCaserne.Load(reader);
                cbCaserne.DataSource = dtCaserne;
                cbCaserne.DisplayMember = "nom";
                cbCaserne.ValueMember = "id";
            }
            //MessageBox.Show(cbGradeNouveau.Name);
            //MessageBox.Show("ID caserne sélectionnée = " + cbCaserne.SelectedValue);


            chargementTermine = true; // ✅ Autorise l'événement maintenant
            cbCaserne_SelectedIndexChanged(null, null); // ⚡ Forcer un premier chargement des pompiers
        }

        private void cbCaserne_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Évite d’exécuter l’événement pendant le chargement du formulaire
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
                string sqlPompier = @"SELECT P.matricule, P.nom || ' ' || P.prenom AS nomComplet FROM Pompier P JOIN Affectation A ON P.matricule = A.matriculePompier WHERE A.idCaserne = @id";

                using (var cmd = new SQLiteCommand(sqlPompier, Connexion.Connec))
                {
                    cmd.Parameters.AddWithValue("@id", idCaserne);

                    using (var reader = cmd.ExecuteReader())
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
        private void cbCaserne_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true; // Ignore key press events
        }

        private void cbPompiers_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true; // Ignore key press events
        }

        private void btnQuitter_Click(object sender, EventArgs e)
        {
            //this.Close();

        }

        private void cbCaserne_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            // Évite d’exécuter l’événement pendant le chargement du formulaire
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
                string sqlPompier = @"SELECT P.matricule, P.nom || ' ' || P.prenom AS nomComplet FROM Pompier P JOIN Affectation A ON P.matricule = A.matriculePompier WHERE A.idCaserne = @id";

                using (var cmd = new SQLiteCommand(sqlPompier, Connexion.Connec))
                {
                    cmd.Parameters.AddWithValue("@id", idCaserne);

                    using (var reader = cmd.ExecuteReader())
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

        private void cbPompiers_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbPompiers.SelectedItem is DataRowView row)
                {
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

                    using (var cmd = new SQLiteCommand(sql, Connexion.Connec))
                    {
                        cmd.Parameters.AddWithValue("@mat", matricule);

                        using (var reader = cmd.ExecuteReader())
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

                                // ✅ Chargement du champ enConge
                                chbConge.Checked = false;
                                if (reader["enConge"] != DBNull.Value)
                                {
                                    chbConge.Checked = Convert.ToBoolean(reader["enConge"]);
                                }

                                // ✅ Cocher le type
                                string type = reader["type"].ToString().Trim().ToLower();
                                if (type == "p")
                                    rdbProfessionnel.Checked = true;
                                else if (type == "v")
                                    rdbVolontaire.Checked = true;

                                // ✅ Affichage de l’image du grade
                                string codeGrade = reader["codeGrade"].ToString();
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
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors du chargement du pompier : " + ex.Message);
            }

        }

        private void btnChanger_Click(object sender, EventArgs e)
        {

            try
            {
                if (cbGradeNouveau.Visible)
                {
                    // Déjà visible → on copie le texte + image du grade
                    if (cbGradeNouveau.SelectedItem is DataRowView selectedRow)
                    {
                        string libelleGrade = selectedRow["libelle"].ToString();
                        string codeGrade = selectedRow["code"].ToString();
                        int matricule = Convert.ToInt32(lblMatricule.Text); // récupère le matricule

                        // 🔁 Mise à jour du grade dans la base avec transaction
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
                    //Première fois : on affiche et on remplit la ComboBox
                    if (Connexion.Connec.State != ConnectionState.Open)
                        Connexion.Connec.Open();

                    string sqlGrade = "SELECT code, libelle FROM Grade";
                    using (var cmdGrade = new SQLiteCommand(sqlGrade, Connexion.Connec))
                    using (var reader = cmdGrade.ExecuteReader())
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

        private void txtGrade_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true; // Ignore key press events
        }
        

        private void BtnPlusInformation_Click(object sender, EventArgs e)
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

                        // 🔹 1. Charger toutes les casernes dans la ComboBox (en mode connecté)
                        string sqlToutesCaserne = "SELECT id, nom FROM Caserne";
                        SQLiteDataAdapter da = new SQLiteDataAdapter(sqlToutesCaserne, Connexion.Connec);
                        DataTable dtCaserne = new DataTable();
                        da.Fill(dtCaserne);

                        cbCaserneRattachement.DataSource = dtCaserne;
                        cbCaserneRattachement.DisplayMember = "nom";
                        cbCaserneRattachement.ValueMember = "id";

                        // 🔹 2. Récupérer l'ID de la caserne actuelle du pompier
                        string sqlCaserneActuelle = @"
    SELECT idCaserne 
    FROM Affectation 
    WHERE matriculePompier = @mat AND dateFin IS NULL";

                        using (var cmd = new SQLiteCommand(sqlCaserneActuelle, Connexion.Connec))
                        {
                            cmd.Parameters.AddWithValue("@mat", matricule);
                            object idCaserne = cmd.ExecuteScalar();

                            if (idCaserne != null)
                            {
                                cbCaserneRattachement.SelectedValue = Convert.ToInt32(idCaserne);
                            }
                        }

                        // 🔹 2. Habilitations du pompier
                        string sqlHabilitations = @"SELECT H.libelle FROM Habilitation H JOIN Passer P ON H.id = P.idHabilitation WHERE P.matriculePompier = @mat";

                        using (var cmd2 = new SQLiteCommand(sqlHabilitations, Connexion.Connec))
                        {
                            cmd2.Parameters.AddWithValue("@mat", matricule);
                            using (var reader = cmd2.ExecuteReader())
                            {
                                lstHabilitations.Items.Clear();
                                while (reader.Read())
                                {
                                    lstHabilitations.Items.Add(reader["libelle"].ToString());
                                }
                            }
                        }
                        ////////////////////////////////
                        ///

                        lstAffectationsPassees.Items.Clear();

                        // 🔴 En-tête pour les affectations en cours
                        lstAffectationsPassees.Items.Add("🟢 Affectations en cours :");

                        string sqlEnCours = @"SELECT A.dateA, C.nom FROM Affectation A JOIN Caserne C ON A.idCaserne = C.id WHERE A.matriculePompier = @mat AND A.dateFin IS NULL ORDER BY A.dateA DESC";

                        using (var cmd1 = new SQLiteCommand(sqlEnCours, Connexion.Connec))
                        {
                            cmd1.Parameters.AddWithValue("@mat", matricule);

                            using (var reader = cmd1.ExecuteReader())
                            {
                                bool aDesAffectations = false;

                                while (reader.Read())
                                {
                                    aDesAffectations = true;
                                    string dateDebut = reader["dateA"].ToString();
                                    string nomCaserne = reader["nom"].ToString();
                                    lstAffectationsPassees.Items.Add($"→ {dateDebut} à aujourd’hui : {nomCaserne}");
                                }

                                if (!aDesAffectations)
                                    lstAffectationsPassees.Items.Add("Aucune affectation en cours.");
                            }
                        }

                        // 🔵 Séparateur
                        lstAffectationsPassees.Items.Add("");
                        lstAffectationsPassees.Items.Add("📘 Affectations passées :");

                        string sqlPassees = @"SELECT A.dateA, A.dateFin, C.nom FROM Affectation A JOIN Caserne C ON A.idCaserne = C.id WHERE A.matriculePompier = @mat AND A.dateFin IS NOT NULL ORDER BY A.dateA DESC";

                        using (var cmd2 = new SQLiteCommand(sqlPassees, Connexion.Connec))
                        {
                            cmd2.Parameters.AddWithValue("@mat", matricule);

                            using (var reader = cmd2.ExecuteReader())
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
                    // 🔽 Si déjà visible, on la masque
                    gbInformationCarriere.Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Erreur lors du chargement des informations supplémentaires : " + ex.Message);
            }
        }

        private void txtCaserneRattachement_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true; // Ignore key press events
        }

        private void txtAffectationsPassees_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true; // Ignore key press events
        }

        private void btnAjoutPompiers_Click(object sender, EventArgs e)
        {
            Form3 formAjout = new Form3();
            formAjout.ShowDialog();
        }

        private void txtCaserneRattachement_TextChanged(object sender, EventArgs e)
        {

        }

        private void cbCaserneRattachement_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnMettreaJour_Click(object sender, EventArgs e)
        {
            SQLiteTransaction transaction = null;

            try
            {
                int matricule = Convert.ToInt32(lblMatricule.Text);
                int nouvelleCaserne = Convert.ToInt32(cbCaserneRattachement.SelectedValue);

                // ✅ Étape 1 : Récupérer l'ID de la caserne actuelle (celle avec dateFin NULL)
                int idCaserneActuelle = -1;

                string sqlSelect = @"SELECT idCaserne FROM Affectation WHERE matriculePompier = @mat AND dateFin IS NULL";
                using (var cmd = new SQLiteCommand(sqlSelect, Connexion.Connec))
                {
                    cmd.Parameters.AddWithValue("@mat", matricule);
                    object result = cmd.ExecuteScalar();
                    if (result != DBNull.Value)
                        idCaserneActuelle = Convert.ToInt32(result);
                }

                // ✅ Étape 2 : Si la caserne est inchangée, ne pas faire d'update/insert
                bool caserneChange = idCaserneActuelle != nouvelleCaserne;

                transaction = Connexion.Connec.BeginTransaction();

                if (caserneChange)
                {
                    // 🔁 Fermer l’ancienne affectation
                    string sqlUpdate = @"
            UPDATE Affectation
            SET dateFin = @dateFin
            WHERE matriculePompier = @mat AND dateFin IS NULL";

                    using (var cmdUpdate = new SQLiteCommand(sqlUpdate, Connexion.Connec, transaction))
                    {
                        cmdUpdate.Parameters.AddWithValue("@dateFin", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        cmdUpdate.Parameters.AddWithValue("@mat", matricule);
                        cmdUpdate.ExecuteNonQuery();
                    }

                    // 🔁 Nouvelle affectation
                    string nouvelleDate = DateTime.Now.AddSeconds(1).ToString("yyyy-MM-dd HH:mm:ss");

                    string sqlInsert = @"
            INSERT INTO Affectation (matriculePompier, dateA, idCaserne)
            VALUES (@matricule, @dateA, @idCaserne)";

                    using (var cmdInsert = new SQLiteCommand(sqlInsert, Connexion.Connec, transaction))
                    {
                        cmdInsert.Parameters.AddWithValue("@matricule", matricule);
                        cmdInsert.Parameters.AddWithValue("@dateA", nouvelleDate);
                        cmdInsert.Parameters.AddWithValue("@idCaserne", nouvelleCaserne);
                        cmdInsert.ExecuteNonQuery();
                    }
                }

                // 🔁 Mettre à jour enConge même si caserne ne change pas
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

            // 🔁 Mettre à jour l’affichage des affectations (même code que toi)
            try
            {
                // ✅ Actualiser l’affectation en cours
                string sqlEnCours = @"
        SELECT A.dateA, C.nom 
        FROM Affectation A 
        JOIN Caserne C ON A.idCaserne = C.id
        WHERE A.matriculePompier = @mat AND A.dateFin IS NULL";

                using (var cmd = new SQLiteCommand(sqlEnCours, Connexion.Connec))
                {
                    cmd.Parameters.AddWithValue("@mat", Convert.ToInt32(lblMatricule.Text));
                    using (var reader = cmd.ExecuteReader())
                    {
                        int indexCours = -1;
                        for (int i = 0; i < lstAffectationsPassees.Items.Count; i++)
                        {
                            if (lstAffectationsPassees.Items[i].ToString().StartsWith("→") &&
                                lstAffectationsPassees.Items[i].ToString().Contains("aujourd’hui"))
                            {
                                indexCours = i;
                                break;
                            }
                        }

                        if (indexCours != -1)
                            lstAffectationsPassees.Items.RemoveAt(indexCours);

                        if (reader.Read())
                        {
                            DateTime dateDebut = Convert.ToDateTime(reader["dateA"]);
                            string nomCaserne = reader["nom"].ToString();
                            lstAffectationsPassees.Items.Insert(1, $"→ {dateDebut:yyyy-MM-dd HH:mm:ss} à aujourd’hui : {nomCaserne}");
                        }
                    }
                }

                // ✅ Ajouter la dernière affectation passée (si caserne a changé)
                if (transaction != null)  // ça veut dire qu'on a fait une insertion
                {
                    string sqlLast = @"
            SELECT A.dateA, A.dateFin, C.nom 
            FROM Affectation A 
            JOIN Caserne C ON A.idCaserne = C.id
            WHERE A.matriculePompier = @mat 
              AND A.dateFin IS NOT NULL
            ORDER BY A.dateFin DESC
            LIMIT 1";

                    using (var cmd = new SQLiteCommand(sqlLast, Connexion.Connec))
                    {
                        cmd.Parameters.AddWithValue("@mat", Convert.ToInt32(lblMatricule.Text));

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                DateTime dateDebut = Convert.ToDateTime(reader["dateA"]);
                                DateTime dateFin = Convert.ToDateTime(reader["dateFin"]);
                                string nomCaserne = reader["nom"].ToString();

                                string ligne = $"→ {dateDebut:yyyy-MM-dd HH:mm:ss} à {dateFin:yyyy-MM-dd HH:mm:ss} : {nomCaserne}";
                                lstAffectationsPassees.Items.Add(ligne);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Erreur d’affichage : " + ex.Message);
            }
        }

        private void cbPompiers_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            try
            {
                if (cbPompiers.SelectedItem is DataRowView row)
                {
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

                    using (var cmd = new SQLiteCommand(sql, Connexion.Connec))
                    {
                        cmd.Parameters.AddWithValue("@mat", matricule);

                        using (var reader = cmd.ExecuteReader())
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

                                // ✅ Chargement du champ enConge
                                chbConge.Checked = false;
                                if (reader["enConge"] != DBNull.Value)
                                {
                                    chbConge.Checked = Convert.ToBoolean(reader["enConge"]);
                                }

                                // ✅ Cocher le type
                                string type = reader["type"].ToString().Trim().ToLower();
                                if (type == "p")
                                    rdbProfessionnel.Checked = true;
                                else if (type == "v")
                                    rdbVolontaire.Checked = true;

                                // ✅ Affichage de l’image du grade
                                string codeGrade = reader["codeGrade"].ToString();
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
            FormConn formConnexion = new FormConn();
            formConnexion.ShowDialog();

            if (formConnexion.EstConnecte)
            {
                // Connexion réussie → ouvre le formulaire de création du pompier
                Form3 formCreer = new Form3();
                formCreer.ShowDialog();
            }
        }

        private void cbGradeNouveau_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnChanger_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (cbGradeNouveau.Visible)
                {
                    // Déjà visible → on copie le texte + image du grade
                    if (cbGradeNouveau.SelectedItem is DataRowView selectedRow)
                    {
                        string libelleGrade = selectedRow["libelle"].ToString();
                        string codeGrade = selectedRow["code"].ToString();
                        int matricule = Convert.ToInt32(lblMatricule.Text); // récupère le matricule

                        // 🔁 Mise à jour du grade dans la base avec transaction
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
                    //Première fois : on affiche et on remplit la ComboBox
                    if (Connexion.Connec.State != ConnectionState.Open)
                        Connexion.Connec.Open();

                    string sqlGrade = "SELECT code, libelle FROM Grade";
                    using (var cmdGrade = new SQLiteCommand(sqlGrade, Connexion.Connec))
                    using (var reader = cmdGrade.ExecuteReader())
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

        private void BtnPlusInformation_Click_1(object sender, EventArgs e)
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

                        // 🔹 1. Charger toutes les casernes dans la ComboBox (en mode connecté)
                        string sqlToutesCaserne = "SELECT id, nom FROM Caserne";
                        SQLiteDataAdapter da = new SQLiteDataAdapter(sqlToutesCaserne, Connexion.Connec);
                        DataTable dtCaserne = new DataTable();
                        da.Fill(dtCaserne);

                        cbCaserneRattachement.DataSource = dtCaserne;
                        cbCaserneRattachement.DisplayMember = "nom";
                        cbCaserneRattachement.ValueMember = "id";

                        // 🔹 2. Récupérer l'ID de la caserne actuelle du pompier
                        string sqlCaserneActuelle = @"
    SELECT idCaserne 
    FROM Affectation 
    WHERE matriculePompier = @mat AND dateFin IS NULL";

                        using (var cmd = new SQLiteCommand(sqlCaserneActuelle, Connexion.Connec))
                        {
                            cmd.Parameters.AddWithValue("@mat", matricule);
                            object idCaserne = cmd.ExecuteScalar();

                            if (idCaserne != null)
                            {
                                cbCaserneRattachement.SelectedValue = Convert.ToInt32(idCaserne);
                            }
                        }

                        // 🔹 2. Habilitations du pompier
                        string sqlHabilitations = @"SELECT H.libelle FROM Habilitation H JOIN Passer P ON H.id = P.idHabilitation WHERE P.matriculePompier = @mat";

                        using (var cmd2 = new SQLiteCommand(sqlHabilitations, Connexion.Connec))
                        {
                            cmd2.Parameters.AddWithValue("@mat", matricule);
                            using (var reader = cmd2.ExecuteReader())
                            {
                                lstHabilitations.Items.Clear();
                                while (reader.Read())
                                {
                                    lstHabilitations.Items.Add(reader["libelle"].ToString());
                                }
                            }
                        }
                        ////////////////////////////////
                        ///

                        lstAffectationsPassees.Items.Clear();

                        // 🔴 En-tête pour les affectations en cours
                        lstAffectationsPassees.Items.Add("🟢 Affectations en cours :");

                        string sqlEnCours = @"SELECT A.dateA, C.nom FROM Affectation A JOIN Caserne C ON A.idCaserne = C.id WHERE A.matriculePompier = @mat AND A.dateFin IS NULL ORDER BY A.dateA DESC";

                        using (var cmd1 = new SQLiteCommand(sqlEnCours, Connexion.Connec))
                        {
                            cmd1.Parameters.AddWithValue("@mat", matricule);

                            using (var reader = cmd1.ExecuteReader())
                            {
                                bool aDesAffectations = false;

                                while (reader.Read())
                                {
                                    aDesAffectations = true;
                                    string dateDebut = reader["dateA"].ToString();
                                    string nomCaserne = reader["nom"].ToString();
                                    lstAffectationsPassees.Items.Add($"→ {dateDebut} à aujourd’hui : {nomCaserne}");
                                }

                                if (!aDesAffectations)
                                    lstAffectationsPassees.Items.Add("Aucune affectation en cours.");
                            }
                        }

                        // 🔵 Séparateur
                        lstAffectationsPassees.Items.Add("");
                        lstAffectationsPassees.Items.Add("📘 Affectations passées :");

                        string sqlPassees = @"SELECT A.dateA, A.dateFin, C.nom FROM Affectation A JOIN Caserne C ON A.idCaserne = C.id WHERE A.matriculePompier = @mat AND A.dateFin IS NOT NULL ORDER BY A.dateA DESC";

                        using (var cmd2 = new SQLiteCommand(sqlPassees, Connexion.Connec))
                        {
                            cmd2.Parameters.AddWithValue("@mat", matricule);

                            using (var reader = cmd2.ExecuteReader())
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
                    // 🔽 Si déjà visible, on la masque
                    gbInformationCarriere.Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Erreur lors du chargement des informations supplémentaires : " + ex.Message);
            }
        }

        private void btnMettreaJour_Click_1(object sender, EventArgs e)
        {
            SQLiteTransaction transaction = null;

            try
            {
                int matricule = Convert.ToInt32(lblMatricule.Text);
                int nouvelleCaserne = Convert.ToInt32(cbCaserneRattachement.SelectedValue);

                // ✅ Étape 1 : Récupérer l'ID de la caserne actuelle (celle avec dateFin NULL)
                int idCaserneActuelle = -1;

                string sqlSelect = @"SELECT idCaserne FROM Affectation WHERE matriculePompier = @mat AND dateFin IS NULL";
                using (var cmd = new SQLiteCommand(sqlSelect, Connexion.Connec))
                {
                    cmd.Parameters.AddWithValue("@mat", matricule);
                    object result = cmd.ExecuteScalar();
                    if (result != DBNull.Value)
                        idCaserneActuelle = Convert.ToInt32(result);
                }

                // ✅ Étape 2 : Si la caserne est inchangée, ne pas faire d'update/insert
                bool caserneChange = idCaserneActuelle != nouvelleCaserne;

                transaction = Connexion.Connec.BeginTransaction();

                if (caserneChange)
                {
                    // 🔁 Fermer l’ancienne affectation
                    string sqlUpdate = @"
            UPDATE Affectation
            SET dateFin = @dateFin
            WHERE matriculePompier = @mat AND dateFin IS NULL";

                    using (var cmdUpdate = new SQLiteCommand(sqlUpdate, Connexion.Connec, transaction))
                    {
                        cmdUpdate.Parameters.AddWithValue("@dateFin", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        cmdUpdate.Parameters.AddWithValue("@mat", matricule);
                        cmdUpdate.ExecuteNonQuery();
                    }

                    // 🔁 Nouvelle affectation
                    string nouvelleDate = DateTime.Now.AddSeconds(1).ToString("yyyy-MM-dd HH:mm:ss");

                    string sqlInsert = @"
            INSERT INTO Affectation (matriculePompier, dateA, idCaserne)
            VALUES (@matricule, @dateA, @idCaserne)";

                    using (var cmdInsert = new SQLiteCommand(sqlInsert, Connexion.Connec, transaction))
                    {
                        cmdInsert.Parameters.AddWithValue("@matricule", matricule);
                        cmdInsert.Parameters.AddWithValue("@dateA", nouvelleDate);
                        cmdInsert.Parameters.AddWithValue("@idCaserne", nouvelleCaserne);
                        cmdInsert.ExecuteNonQuery();
                    }
                }

                // 🔁 Mettre à jour enConge même si caserne ne change pas
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

            // 🔁 Mettre à jour l’affichage des affectations (même code que toi)
            try
            {
                // ✅ Actualiser l’affectation en cours
                string sqlEnCours = @"
        SELECT A.dateA, C.nom 
        FROM Affectation A 
        JOIN Caserne C ON A.idCaserne = C.id
        WHERE A.matriculePompier = @mat AND A.dateFin IS NULL";

                using (var cmd = new SQLiteCommand(sqlEnCours, Connexion.Connec))
                {
                    cmd.Parameters.AddWithValue("@mat", Convert.ToInt32(lblMatricule.Text));
                    using (var reader = cmd.ExecuteReader())
                    {
                        int indexCours = -1;
                        for (int i = 0; i < lstAffectationsPassees.Items.Count; i++)
                        {
                            if (lstAffectationsPassees.Items[i].ToString().StartsWith("→") &&
                                lstAffectationsPassees.Items[i].ToString().Contains("aujourd’hui"))
                            {
                                indexCours = i;
                                break;
                            }
                        }

                        if (indexCours != -1)
                            lstAffectationsPassees.Items.RemoveAt(indexCours);

                        if (reader.Read())
                        {
                            DateTime dateDebut = Convert.ToDateTime(reader["dateA"]);
                            string nomCaserne = reader["nom"].ToString();
                            lstAffectationsPassees.Items.Insert(1, $"→ {dateDebut:yyyy-MM-dd HH:mm:ss} à aujourd’hui : {nomCaserne}");
                        }
                    }
                }

                // ✅ Ajouter la dernière affectation passée (si caserne a changé)
                if (transaction != null)  // ça veut dire qu'on a fait une insertion
                {
                    string sqlLast = @"
            SELECT A.dateA, A.dateFin, C.nom 
            FROM Affectation A 
            JOIN Caserne C ON A.idCaserne = C.id
            WHERE A.matriculePompier = @mat 
              AND A.dateFin IS NOT NULL
            ORDER BY A.dateFin DESC
            LIMIT 1";

                    using (var cmd = new SQLiteCommand(sqlLast, Connexion.Connec))
                    {
                        cmd.Parameters.AddWithValue("@mat", Convert.ToInt32(lblMatricule.Text));

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                DateTime dateDebut = Convert.ToDateTime(reader["dateA"]);
                                DateTime dateFin = Convert.ToDateTime(reader["dateFin"]);
                                string nomCaserne = reader["nom"].ToString();

                                string ligne = $"→ {dateDebut:yyyy-MM-dd HH:mm:ss} à {dateFin:yyyy-MM-dd HH:mm:ss} : {nomCaserne}";
                                lstAffectationsPassees.Items.Add(ligne);
                            }
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
    }
}
