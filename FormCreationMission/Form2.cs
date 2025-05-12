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
            Application.Exit();
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
                                else
                                    MessageBox.Show("⚠ Type inconnu : " + type);
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
                    // ✅ Déjà visible → on copie juste le texte sélectionné dans le TextBox
                    if (cbGradeNouveau.SelectedItem is DataRowView selectedRow)
                    {
                        string libelleGrade = selectedRow["libelle"].ToString();
                        txtGrade.Text = libelleGrade;
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
    }
}
