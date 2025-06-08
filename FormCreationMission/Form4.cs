using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Data.Sql;
using UCRecapitulMission;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.IO;
using Org.BouncyCastle.Asn1.Ocsp;
using Org.BouncyCastle.Asn1.IsisMtt.X509;
using UCMenue;
using FormCreationMission;

namespace FormCreationMission
{
    public partial class Form4 : Form
    {

        private SQLiteDataAdapter da;

        public Form4()
        {
            InitializeComponent();

            pnlAffichage.AutoScroll = true;
            pnlAffichage.Padding = new Padding(10);
            pnlAffichage.AutoScroll = true;
            this.WindowState = FormWindowState.Maximized;
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            MesDatas.DsGlobal.Clear();
            DataTable dt = new DataTable();
            dt = Connexion.Connec.GetSchema("Tables");
            string xx = "Liste :\n";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string ntable = dt.Rows[i]["TABLE_NAME"].ToString();
                string sql = "SELECT * FROM " + ntable;
                SQLiteDataAdapter da = new SQLiteDataAdapter(sql, Connexion.Connec);
                da.Fill(MesDatas.DsGlobal, ntable);
            }

            pnlAffichage.Controls.Clear();

            // placement du menu
            UCMenueLateral menu = new UCMenueLateral(this);
            menu.Location = new System.Drawing.Point(12, 1);
            this.Controls.Add(menu);

            //Nettoyage du panel d'affichage
            pnlAffichage.Controls.Clear();
            // Inverser les lignes de la table Mission sans changer la boucle
            DataRow[] missionsInversees = MesDatas.DsGlobal.Tables["Mission"].Select("", "id DESC");
            int y = 10; // Position de départ verticale
            //Affichage des mission dans le panel en mode inverse
            foreach (DataRow row in missionsInversees)
            {
                //on cherche l'Id
                int id = Convert.ToInt32(row["id"]);
                //on cherche la nature du sinistre
                string nature = row["idNatureSinistre"].ToString();
                string libelleNature = "";
                //on cherche le libellé de la nature du sinistre
                DataRow[] foundNature = MesDatas.DsGlobal.Tables["NatureSinistre"].Select("id = '" + nature + "'");
                if (foundNature.Length > 0)
                {
                    libelleNature = foundNature[0]["libelle"].ToString();
                }
                //Création de l'UC pour afficher la mission
                UCAffichageMission uCRecapitulMission = new UCAffichageMission(id, Connexion.Connec, MesDatas.DsGlobal);
                
                //On creer un panel wrapper pour center l'UC
                Panel wrapper = new Panel();
                wrapper.IsAccessible = true;
                wrapper.Width = pnlAffichage.ClientSize.Width;
                wrapper.Height = uCRecapitulMission.Height + 10;
                wrapper.Padding = new Padding(0);
                wrapper.Margin = new Padding(0);
                wrapper.BackColor = Color.Transparent;
                //Ajouter l'UC dans le wrapper
                wrapper.Controls.Add(uCRecapitulMission);
                //Centrer l'UC dans le wrapper
                uCRecapitulMission.Left = (wrapper.Width - uCRecapitulMission.Width) / 2;
                uCRecapitulMission.Top = 0;
                //Important : positionne verticalement le wrapper
                wrapper.Top = y;
                wrapper.Left = 0;
                //Ajouter le panel wrapper dans le panel d'affichage
                pnlAffichage.Controls.Add(wrapper);
                //Incrémentation pour la prochaine mission
                y += wrapper.Height + 10;
            }
        }

        //Methode pour afficher le User Control du Menu
        public void AfficherDansPanel()
        {
            try
            {
                pnlAffichage.Controls.Clear();
                UCCreerMission uc = new UCCreerMission(this);
                uc.Dock = DockStyle.Fill;
                pnlAffichage.Controls.Add(uc);
                uc.BringToFront();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur : " + ex.Message);
            }
        }

        public void AfficherEngins()
        {
            try
            {
                pnlAffichage.Controls.Clear();
                gestionEngins uc = new gestionEngins(MesDatas.DsGlobal,this);
                uc.Dock = DockStyle.Fill;
                pnlAffichage.Controls.Add(uc);
                uc.BringToFront();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur : " + ex.Message);
            }
        }

        public void AfficherStatistique()
        {
            try
            {
                pnlAffichage.Controls.Clear();
                Statistique uc = new Statistique(this,Connexion.Connec,MesDatas.DsGlobal);
                uc.Dock = DockStyle.Fill;
                pnlAffichage.Controls.Add(uc);
                uc.BringToFront();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur : " + ex.Message);
            }
        }

        //Methode pour afficher le User Control du Personnel
        public void AfficherPersonnel()
        {
            try
            {
                pnlAffichage.Controls.Clear();
                UCPersonnel userc = new UCPersonnel(this);
                userc.Dock = DockStyle.Fill;
                pnlAffichage.Controls.Add(userc);
                userc.BringToFront();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur : " + ex.Message);
            }
        }
        

        //Boutton actualiser
        public void actualiser()
        {
            //Nettoyage du panel d'affichage
            pnlAffichage.Controls.Clear();
            // Inverser les lignes de la table Mission sans changer la boucle
            DataRow[] missionsInversees = MesDatas.DsGlobal.Tables["Mission"].Select("", "id DESC");
            int y = 10; // Position de départ verticale
            foreach (DataRow row in missionsInversees)
            {
                //on cherche l'Id
                int id = Convert.ToInt32(row["id"]);
                //on cherche la nature du sinistre
                string nature = row["idNatureSinistre"].ToString();
                string libelleNature = "";
                //on cherche le libellé de la nature du sinistre
                DataRow[] foundNature = MesDatas.DsGlobal.Tables["NatureSinistre"].Select("id = '" + nature + "'");
                if (foundNature.Length > 0)
                {
                    libelleNature = foundNature[0]["libelle"].ToString();
                }
                //Création de l'UC pour afficher la mission
                UCAffichageMission uCRecapitulMission = new UCAffichageMission(id, Connexion.Connec, MesDatas.DsGlobal);

                //On creer un panel wrapper pour center l'UC
                Panel wrapper = new Panel();
                wrapper.IsAccessible = true;
                wrapper.Width = pnlAffichage.ClientSize.Width;
                wrapper.Height = uCRecapitulMission.Height + 10;
                wrapper.Padding = new Padding(0);
                wrapper.Margin = new Padding(0);
                wrapper.BackColor = Color.Transparent;
                //Ajouter l'UC dans le wrapper
                wrapper.Controls.Add(uCRecapitulMission);
                //Centrer l'UC dans le wrapper
                uCRecapitulMission.Left = (wrapper.Width - uCRecapitulMission.Width) / 2;
                uCRecapitulMission.Top = 0;
                //Important : positionne verticalement le wrapper
                wrapper.Top = y;
                wrapper.Left = 0;
                //Ajouter le panel wrapper dans le panel d'affichage
                pnlAffichage.Controls.Add(wrapper);
                //Incrémentation pour la prochaine mission
                y += wrapper.Height + 10;
            }
        }

        public void btnActualiser_Click(object sender, EventArgs e)
        {
            actualiser();
        }

        private void chkbxEnCours_CheckedChanged_1(object sender, EventArgs e)
        {
            //Verification si la checkbox elle est cocher ou pas
            if (!chkbxEnCours.Checked)
            {
                actualiser();
            }
            else
            {
                //On nettoie le panel principale
                pnlAffichage.Controls.Clear();
                DataRow[] missionsInversees = MesDatas.DsGlobal.Tables["Mission"].Select("", "id DESC");
                int y = 10; // Position de départ verticale
                foreach (DataRow row in missionsInversees)
                {
                    if (row["terminee"].ToString() == "0")
                    {
                        //on cherche l'Id
                        int id = Convert.ToInt32(row["id"]);
                        //on cherche la nature du sinistre
                        string nature = row["idNatureSinistre"].ToString();
                        string libelleNature = "";
                        //on cherche le libellé de la nature du sinistre
                        DataRow[] foundNature = MesDatas.DsGlobal.Tables["NatureSinistre"].Select("id = '" + nature + "'");
                        if (foundNature.Length > 0)
                        {
                            libelleNature = foundNature[0]["libelle"].ToString();
                        }
                        //Création de l'UC pour afficher la mission
                        UCAffichageMission uCRecapitulMission = new UCAffichageMission(id, Connexion.Connec, MesDatas.DsGlobal);

                        //On creer un panel wrapper pour center l'UC
                        Panel wrapper = new Panel();
                        wrapper.IsAccessible = true;
                        wrapper.Width = pnlAffichage.ClientSize.Width;
                        wrapper.Height = uCRecapitulMission.Height + 10;
                        wrapper.Padding = new Padding(0);
                        wrapper.Margin = new Padding(0);
                        wrapper.BackColor = Color.Transparent;
                        //Ajouter l'UC dans le wrapper
                        wrapper.Controls.Add(uCRecapitulMission);
                        //Centrer l'UC dans le wrapper
                        uCRecapitulMission.Left = (wrapper.Width - uCRecapitulMission.Width) / 2;
                        uCRecapitulMission.Top = 0;
                        //Important : positionne verticalement le wrapper
                        wrapper.Top = y;
                        wrapper.Left = 0;
                        //Ajouter le panel wrapper dans le panel d'affichage
                        pnlAffichage.Controls.Add(wrapper);
                        //Incrémentation pour la prochaine mission
                        y += wrapper.Height + 10;
                    }
                }
            }
        }
    }
}