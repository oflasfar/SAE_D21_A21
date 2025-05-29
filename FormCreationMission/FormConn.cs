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
    public partial class FormConn: Form
    {
        public bool EstConnecte { get; private set; } = false;
        public FormConn()
        {
            InitializeComponent();
        }

        private void FormConn_Load(object sender, EventArgs e)
        {
            string login = txtLogin.Text.Trim();
            string mdp = txtMDP.Text.Trim();
        }

        private void btnConnexion_Click(object sender, EventArgs e)
        {
            string login = txtLogin.Text.Trim();
            string mdp = txtMDP.Text.Trim();

            string sql = "SELECT COUNT(*) FROM Admin WHERE login = @login AND mdp = @mdp";
            using (SQLiteCommand cmd = new SQLiteCommand(sql, Connexion.Connec))
            {
                cmd.Parameters.AddWithValue("@login", login);
                cmd.Parameters.AddWithValue("@mdp", mdp); // tu peux aussi utiliser un hash ici

                if (Connexion.Connec.State != ConnectionState.Open)
                    Connexion.Connec.Open();

                int count = Convert.ToInt32(cmd.ExecuteScalar());
                if (count > 0)
                {
                    EstConnecte = true;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Identifiants incorrects", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
