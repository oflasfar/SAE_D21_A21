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
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SQLiteTransaction transaction = null;

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
                cmd.Parameters.AddWithValue("@bip", int.Parse(txtBip.Text));
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
    }
}
