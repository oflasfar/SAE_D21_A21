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
using Org.BouncyCastle.Bcpg.Sig;

namespace FormCreationMission
{
    public partial class Form3: Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            cbGrade.DataSource = MesDatas.DsGlobal.Tables["Grade"];
            cbGrade.DisplayMember = "libelle";
            cbGrade.ValueMember = "code";
            // ⚠️ Remplir la liste des casernes
            cbCaserne.DataSource = MesDatas.DsGlobal.Tables["Caserne"];
            cbCaserne.DisplayMember = "nom";
            cbCaserne.ValueMember = "id";
            try
            {
                string sql = "SELECT MAX(matricule) FROM Pompier";
                SQLiteCommand cmd = new SQLiteCommand(sql, Connexion.Connec);
                object result = cmd.ExecuteScalar();

                int prochainMatricule = 1;

                if (result != DBNull.Value)
                {
                    prochainMatricule = Convert.ToInt32(result) + 1;
                }

                txtMatricule.Text = prochainMatricule.ToString();
                txtMatricule.ReadOnly = true; // pour éviter que l'utilisateur ne le modifie
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de la génération du matricule : " + ex.Message);
            }
            //Chercher les bips déjà utilisés
            string sqlBip = "SELECT bip FROM Pompier";
            SQLiteCommand cmdBip = new SQLiteCommand(sqlBip, Connexion.Connec);
            SQLiteDataReader reader = cmdBip.ExecuteReader();

            HashSet<int> bipsUtilises = new HashSet<int>();
            while (reader.Read())
            {
                if (!reader.IsDBNull(0))
                {
                    bipsUtilises.Add(reader.GetInt32(0));
                }
            }
            reader.Close();

            //Chercher le plus petit bip libre
            int bipAttribue = 1;
            while (bipsUtilises.Contains(bipAttribue))
            {
                bipAttribue++;
            }

            txtBip.Text = bipAttribue.ToString();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SQLiteTransaction transaction = null;
            if (string.IsNullOrWhiteSpace(txtNom.Text) ||
                string.IsNullOrWhiteSpace(txtPrenom.Text) ||
                string.IsNullOrWhiteSpace(txtPortable.Text) ||
                string.IsNullOrWhiteSpace(txtBip.Text) ||
                (!rbM.Checked && !rbF.Checked) ||
                (!rbPro.Checked && !rbVo.Checked) ||
                cbGrade.SelectedIndex == -1 ||
                cbCaserne.SelectedIndex == -1)
            {
                MessageBox.Show("❌ Veuillez remplir tous les champs obligatoires.", "Champs manquants", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // On bloque ici
            }

            DateTime naissance = dtpNaissance.Value;
            int age = DateTime.Now.Year - naissance.Year;
            if (naissance > DateTime.Now.AddYears(-age)) age--;

            if (age < 18)
            {
                MessageBox.Show("❌ Le pompier doit avoir au moins 18 ans.", "Âge insuffisant", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                transaction = Connexion.Connec.BeginTransaction();

                SQLiteCommand cmd = Connexion.Connec.CreateCommand();
                cmd.Transaction = transaction;

                // 🔁 Insertion dans la table Pompier
                cmd.CommandText = @"INSERT INTO Pompier 
    (matricule, nom, prenom, sexe, dateNaissance, type, portable, bip, enMission, enConge, codeGrade, dateEmbauche)
    VALUES (@matricule, @nom, @prenom, @sexe, @dateNaissance, @type, @portable, @bip, 0, 0, @codeGrade, @dateEmbauche)";

                cmd.Parameters.AddWithValue("@matricule", int.Parse(txtMatricule.Text));
                cmd.Parameters.AddWithValue("@nom", txtNom.Text);
                cmd.Parameters.AddWithValue("@prenom", txtPrenom.Text);
                cmd.Parameters.AddWithValue("@sexe", rbM.Checked ? "m" : "f");
                cmd.Parameters.AddWithValue("@dateNaissance", dtpNaissance.Value.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@type", rbPro.Checked ? "p" : "v");
                cmd.Parameters.AddWithValue("@portable", txtPortable.Text);
                cmd.Parameters.AddWithValue("@bip", txtBip.Text);
                cmd.Parameters.AddWithValue("@codeGrade", cbGrade.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@dateEmbauche", DateTime.Now.ToString("yyyy-MM-dd"));

                cmd.ExecuteNonQuery();

                // 🔁 Insertion dans la table Affectation
                SQLiteCommand cmdAffectation = Connexion.Connec.CreateCommand();
                cmdAffectation.Transaction = transaction;

                cmdAffectation.CommandText = @"INSERT INTO Affectation 
    (matriculePompier, dateA, dateFin, idCaserne)
    VALUES (@matricule, @dateA, NULL, @idCaserne)";

                cmdAffectation.Parameters.AddWithValue("@matricule", int.Parse(txtMatricule.Text));
                cmdAffectation.Parameters.AddWithValue("@dateA", DateTime.Now.ToString("yyyy-MM-dd"));
                cmdAffectation.Parameters.AddWithValue("@idCaserne", Convert.ToInt32(cbCaserne.SelectedValue));

                cmdAffectation.ExecuteNonQuery();

                transaction.Commit();
                MessageBox.Show("✅ Pompier ajouté avec affectation !");
                this.Close();
            }
            catch (Exception ex)
            {
                if (transaction != null)
                    transaction.Rollback();

                MessageBox.Show("❌ Erreur pendant l'ajout : " + ex.Message);
            }
        }

        private void txtMatricule_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled=true;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnQuitter_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtBip_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void rbPro_CheckedChanged(object sender, EventArgs e)
        {
            if(rbVo.Checked)
            {
                rbVo.Checked = false;
            }
        }

        private void rbVo_CheckedChanged(object sender, EventArgs e)
        {
            if (rbPro.Checked)
            {
                rbPro.Checked = false;
            }
        }

        private void rbM_CheckedChanged(object sender, EventArgs e)
        {
            if (rbF.Checked)
            {
                rbF.Checked = false;
            }
        }

        private void rbF_CheckedChanged(object sender, EventArgs e)
        {
            if (rbM.Checked)
            {
                rbM.Checked = false;
            }
        }

        private void txtPortable_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Autoriser uniquement les chiffres et les touches de contrôle (ex: retour arrière)
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Empêche la saisie
            }
        }

        private void txtPortable_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
