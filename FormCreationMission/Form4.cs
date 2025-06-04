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
                //C est une liste pour savoir les tables de la base de données si ils sont tous chargées ou pas
                //xx += ntable + "\n";
                string sql = "SELECT * FROM " + ntable;
                SQLiteDataAdapter da = new SQLiteDataAdapter(sql, Connexion.Connec);
                da.Fill(MesDatas.DsGlobal, ntable);
            }

            pnlAffichage.Controls.Clear();

            // placement du menu
            Form2 personnel = new Form2();
            UCMenueLateral menu = new UCMenueLateral(this, personnel);
            menu.Location = new System.Drawing.Point(12, 1);
            this.Controls.Add(menu);

            //Nettoyage du panel d'affichage
            pnlAffichage.Controls.Clear();
            int y = 10; // Position de départ verticale
            foreach (DataRow row in MesDatas.DsGlobal.Tables["Mission"].Rows)
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

        //Fonction pour charger le dataSet
        private void ChargerDataSet()
        {

            DataTable dt = new DataTable();
            dt = Connexion.Connec.GetSchema("Tables");
            string xx = "Liste :\n";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string ntable = dt.Rows[i]["TABLE_NAME"].ToString();
                xx += ntable + "\n";
                string sql = "SELECT * FROM " + ntable;
                SQLiteDataAdapter da = new SQLiteDataAdapter(sql, Connexion.Connec);
                da.Fill(MesDatas.DsGlobal, ntable);
            }
            //MessageBox.Show(ds.Tables.Count.ToString() + " table(s) chargées !");
            //dtgvEX.DataSource = ds.Tables["Mission"];
        }

        //Fonction pour remplir les informations du PDF
        private string remplirInfoPDF(string idMission, string idCaserne)
        {
            String descriptionDetaille = recapTableMission(idMission);
            descriptionDetaille = recapTableMobiliser(idMission);
            return descriptionDetaille;
        }

        //Pour avoir le récapitulatif de la mission (l'adresse)
        private string recapTableMission(string idMission)
        {
            string retour = "";
            foreach (DataRow row in MesDatas.DsGlobal.Tables["Mission"].Rows)
            {
                if (row["id"].ToString() == idMission)
                {
                    retour += "Adresse de la mission : " + row["adresse"].ToString() + " " + row["cp"].ToString() + " " + row["ville"].ToString() + "\n";
                    retour += "Compte rendue: " + row["compteRendu"].ToString() + "\n\n";
                }
            }
            return retour;
        }

        private string recapTableMobiliser(string idMission)
        {
            string retour = "Les pompier mobilisée sont:\n";
            foreach (DataRow row in MesDatas.DsGlobal.Tables["Mobiliser"].Rows)
            {
                if (row["idMission"].ToString() == idMission)
                {
                    retour += recapTablePompier(row["matriculePompier"].ToString()) + recapTableHabilitation(row["idHabilitation"].ToString()) + "\n";
                }
            }
            return retour;
        }
        private string recapTablePompier(string matricule)
        {
            string retour = "";
            foreach (DataRow row in MesDatas.DsGlobal.Tables["Pompier"].Rows)
            {
                if (row["matricule"].ToString() == matricule)
                {
                    if (row["sexe"].ToString() == "m")
                    {
                        retour += "Le ";
                    }
                    else
                    {
                        retour += "La ";
                    }
                    retour += recapTableGrade(row["codeGrade"].ToString()) + row["prenom"].ToString() + " " + row["nom"].ToString() + "(" + matricule + ")" + "\n";
                }
            }
            return retour;
        }
        private string recapTableGrade(string code)
        {
            string retour = "";
            foreach (DataRow row in MesDatas.DsGlobal.Tables["Grade"].Rows)
            {
                if (row["code"].ToString() == code)
                {
                    retour += row["libelle"].ToString() + " ";
                }
            }
            return retour;
        }
        private string recapTableHabilitation(string idHabilitation)
        {
            string retour = "";
            foreach (DataRow row in MesDatas.DsGlobal.Tables["Habilitation"].Rows)
            {
                if (row["id"].ToString() == idHabilitation)
                {
                    retour = "mobiliser en tant que: " + row["libelle"].ToString() + "\n";
                }
            }
            return retour;
        }



        private void chkbxEnCours_CheckedChanged(object sender, EventArgs e)
        {
            /*
            foreach (Control ctrl in pnlAffichage.Controls.OfType<Control>().ToList())
            {
                if (ctrl != lblTableauBord && ctrl != chkbxEnCours)
                {
                    pnlAffichage.Controls.Remove(ctrl);
                    ctrl.Dispose(); // facultatif mais propre
                }
            }
            int y = 160;
            int x = 185;
            foreach (DataRow row in ds.Tables["Mission"].Rows)
            {
                if (row["terminee"].ToString() == "0")
                {
                    // Exemple d'accès aux colonnes par nom
                    int id = Convert.ToInt32(row["id"]); // ou le vrai nom de colonne de ta table
                    string idcaserne = row["idCaserne"].ToString();
                    string debut = row["dateHeureDepart"].ToString();
                    string type = row["motifAppel"].ToString();

                    string nature = row["idNatureSinistre"].ToString();

                    string fin = row["dateHeureRetour"].ToString();

                    string description = remplirInfoPDF(id.ToString(), idcaserne);
                    foreach (DataRow naturerow in ds.Tables["NatureSinistre"].Rows)
                    {
                        if (naturerow["id"].ToString() == nature)
                        {
                            string libelleNature = naturerow["libelle"].ToString();
                            UCAffichageMission uCRecapitulMission = new UCAffichageMission(id, idcaserne, type, libelleNature, debut, description, fin);
                            

                            uCRecapitulMission.Location = new Point(x, y); // Position sur la Forme
                            pnlAffichage.Controls.Add(uCRecapitulMission);
                            y = y + 127;
                        }
                    }
                }
               
            }*/
        }

        private void btnpdf_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void grpbxMission_Enter(object sender, EventArgs e)
        {

        }

        private void ucMenueLateral1_Load(object sender, EventArgs e)
        {

        }

        //Boutton actualiser
        public void actualiser()
        {
            //Nettoyage du panel d'affichage
            pnlAffichage.Controls.Clear();
            int y = 10; // Position de départ verticale
            foreach (DataRow row in MesDatas.DsGlobal.Tables["Mission"].Rows)
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
                int y = 10; // Position de départ verticale
                foreach (DataRow row in MesDatas.DsGlobal.Tables["Mission"].Rows)
                {
                    if (row["terminee"].ToString() == "0")
                    {
                        int id = Convert.ToInt32(row["id"]);
                        string nature = row["idNatureSinistre"].ToString();
                        string libelleNature = "";
                        DataRow[] foundNature = MesDatas.DsGlobal.Tables["NatureSinistre"].Select("id = '" + nature + "'");
                        if (foundNature.Length > 0)
                        {
                            libelleNature = foundNature[0]["libelle"].ToString();
                        }
                        UCAffichageMission uCRecapitulMission = new UCAffichageMission(id, Connexion.Connec, MesDatas.DsGlobal);
                        uCRecapitulMission.Width = 600;
                        //Creation de chaque panel pour chaque mission
                        Panel wrapper = new Panel();
                        wrapper.IsAccessible = true;
                        wrapper.Width = pnlAffichage.ClientSize.Width;
                        wrapper.Height = uCRecapitulMission.Height + 10;
                        wrapper.Padding = new Padding(0);
                        wrapper.Margin = new Padding(0);
                        wrapper.BackColor = Color.Transparent;
                        //Ajout de l'UC dans le wrapper
                        wrapper.Controls.Add(uCRecapitulMission);
                        uCRecapitulMission.Left = (wrapper.Width - uCRecapitulMission.Width) / 2;
                        uCRecapitulMission.Top = 0;
                        //Important : positionne verticalement le wrapper
                        wrapper.Top = y;
                        wrapper.Left = 0;
                        //Ajout du wrapper dans le panel principale
                        pnlAffichage.Controls.Add(wrapper);
                        //Incrémentation pour la prochaine mission
                        y += wrapper.Height + 10;
                    }
                }
            }
        }
    }
}