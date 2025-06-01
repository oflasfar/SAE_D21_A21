using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FormCreationMission;


namespace UCMenue
{
    public partial class UCMenueLateral: UserControl
    {
        Form4 f;
        Form2 Personnel;
        
        public UCMenueLateral()
        {
            InitializeComponent();
        }
        public UCMenueLateral(Form4 fp,Form2 Personnel)
        {
            InitializeComponent();
            this.f = fp;
            this.Personnel = Personnel;
        }

        private void UserControl1_Load(object sender, EventArgs e)
        {
            //
        }

        private void btnQuitter_Click(object sender, EventArgs e)
        {
            this.FindForm().Close();
        }

        private void btnTableuDeBord_Click(object sender, EventArgs e)
        {
            f.btnActualiser_Click(sender, e);
        }

        private void btnNouvelleMission_Click(object sender, EventArgs e)
        {
            if (f != null) // f est la Form4
            {
                f.AfficherDansPanel(); // méthode de Form4
            }
            else
            {
                MessageBox.Show("La fenêtre principale n’est pas prête.");
            }
        }


        private void btnGestionPersonnel_Click(object sender, EventArgs e)
        {
            if (Personnel != null)
            {
                f.AfficherPersonnel(); // méthode de Form4


            }
            else
            {
                MessageBox.Show("Le formulaire de gestion du personnel n’a pas été fourni.");
            }
        }

        private void btnStatistique_Click(object sender, EventArgs e)
        {
            f.AfficherStatistique(); // méthode de Form4
        }

        private void btnGestionEngin_Click(object sender, EventArgs e)
        {
            f.AfficherEngins(); // méthode de Form4
        }
    }
}
