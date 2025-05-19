using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace FormCreationMission
{
    public partial class Form2: Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        private bool chargementTermine = false;


        private void Form2_Load(object sender, EventArgs e)
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
            MessageBox.Show(cbGradeNouveau.Name);


            MessageBox.Show("ID caserne sélectionnée = " + cbCaserne.SelectedValue);


            chargementTermine = true; // ✅ Autorise l'événement maintenant
            cbCaserne_SelectedIndexChanged(null, null); // ⚡ Forcer un premier chargement des pompiers

            
        }
        private void cbCaserne_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }


        private void pictureBox1_Click(object sender, EventArgs e)
        {

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
            this.Close();

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
       P.portable, P.bip, P.dateEmbauche, P.codeGrade,
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
                    // ✅ Déjà visible → on copie le texte + image du grade
                    if (cbGradeNouveau.SelectedItem is DataRowView selectedRow)
                    {
                        string libelleGrade = selectedRow["libelle"].ToString();
                        string codeGrade = selectedRow["code"].ToString();

                        txtGrade.Text = libelleGrade;

                        // ✅ Affichage de l’image correspondant au grade
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

                    cbGradeNouveau.Visible = false; // cacher après sélection
                }
                else
                {
                    // 👇 Première fois : on affiche et on remplit la ComboBox
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

                        // 🔹 1. Caserne actuelle
                        string sqlCaserne = @"SELECT C.nom FROM Caserne C JOIN Affectation A ON C.id = A.idCaserne WHERE A.matriculePompier = @mat AND A.dateFin IS NULL";

                        using (var cmd1 = new SQLiteCommand(sqlCaserne, Connexion.Connec))
                        {
                            cmd1.Parameters.AddWithValue("@mat", matricule);
                            var caserne = cmd1.ExecuteScalar();
                            if (caserne != null)
                                txtCaserneRattachement.Text = caserne.ToString();
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
    }
}
