using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UC_Page_d_accueil
{
    public partial class UserControl1: UserControl
    {
        public UserControl1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        public void setMissionData(int id, string caserne, string typeMission, string description,string date,DataSet ds)
        {
            lblCaserne.Text = caserne;
            lblTypeMission.Text = typeMission;
            lblDescription.Text = description;
            lblDate.Text = date;
            lblId.Text = id.ToString();

            
        }

        private void btnRapport_Click(object sender, EventArgs e)
        {

        }
    }
}
