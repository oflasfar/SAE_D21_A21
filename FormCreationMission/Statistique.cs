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
using FormCreationMission;

namespace FormCreationMission
{
    public partial class Statistique : UserControl
    {
        private SQLiteConnection cx;
        private DataSet ds;
        private SQLiteDataAdapter da;
        private bool isLoading = true;

        public Statistique()
        {
            InitializeComponent();
        }
        public Statistique(Form4 f, SQLiteConnection cxi, DataSet dsi)
        {
            this.cx = cxi;
            this.ds = dsi;
            InitializeComponent();
        }

        private void Statistique_Load(object sender, EventArgs e)
        {
            cboCaserne.Items.Add("choisir une caserne");
            cboCaserne.SelectedIndex = 0;

            string sql = "SELECT nom FROM Caserne";
            try
            {
                SQLiteCommand com = new SQLiteCommand(sql, this.cx);
                SQLiteDataReader dr = com.ExecuteReader();

                while (dr.Read())
                {
                    cboCaserne.Items.Add(dr[0].ToString());

                }
            }
            catch (SQLiteException err)
            {
                MessageBox.Show(err.Message);
            }

            pnlCaserne.Visible = false;
            string[] requeteCaserne = { "Information sur ", "Engins les plus utilisés", "Cumul d’utilisation" };
            cboRequeteParCaserne.Items.AddRange(requeteCaserne);
            cboRequeteParCaserne.SelectedIndex = 0;


            string[] requeteGlobale = { "Stat global ", "Nombre d’interventions par type de sinistre", "Habilitations les plus sollicitées", "Liste des pompiers par habilitation" };

            cboStatGlobal.Items.AddRange(requeteGlobale);
            cboStatGlobal.SelectedIndex = 0;

            cboHabilitation.Items.Add("choisir habilitation");
            String sql2 = "SELECT libelle FROM Habilitation ORDER BY id";
            try
            {
                SQLiteCommand com = new SQLiteCommand(sql2, this.cx);
                SQLiteDataReader dr = com.ExecuteReader();

                while (dr.Read())
                {
                    cboHabilitation.Items.Add(dr[0].ToString());

                }
            }
            catch (SQLiteException err)
            {
                MessageBox.Show(err.Message);
            }
            cboHabilitation.SelectedIndex = 0;


            cboTypeSinistre.Items.Add("choisir type sinistre");
            String sql3 = "SELECT libelle FROM NatureSinistre ORDER BY id";
            try
            {
                SQLiteCommand com = new SQLiteCommand(sql3, this.cx);
                SQLiteDataReader dr = com.ExecuteReader();

                while (dr.Read())
                {
                    cboTypeSinistre.Items.Add(dr[0].ToString());

                }
            }
            catch (SQLiteException err)
            {
                MessageBox.Show(err.Message);
            }
            cboTypeSinistre.SelectedIndex = 0;
            pnlGlobal.AutoScroll = true;
            isLoading = false;
            gbDonnes.Controls.Add(lblStatCasern);
            gbDonnes.Controls.Add(cboCaserne);
            gbDonnes.Controls.Add(cboRequeteParCaserne);
            gbDonnes.Controls.Add(lblStatistiqueGlobal);
            gbDonnes.Controls.Add(cboStatGlobal);
            gbDonnes.Controls.Add(cboTypeSinistre);
            gbDonnes.Controls.Add(cboHabilitation);

        }

        private void cboCaserne_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isLoading) return;
            pnlCaserne.Visible = true;
            int y = 25;
            pnlCaserne.Controls.Clear();
            if (cboRequeteParCaserne.SelectedIndex == 1)
            {
                string sql = "SELECT codeTypeEngin, numeroEngin, COUNT(*) FROM PartirAvec WHERE idCaserne = " + cboCaserne.SelectedIndex + " GROUP BY idCaserne, codeTypeEngin, numeroEngin;";

                try
                {
                    SQLiteCommand com = new SQLiteCommand(sql, this.cx);
                    SQLiteDataReader dr = com.ExecuteReader();


                    pnlCaserne.Controls.Clear();



                    while (dr.Read())
                    {
                        String engin = dr.GetString(0);
                        int num = dr.GetInt32(1);
                        String numero = num.ToString();
                        int compteur = dr.GetInt32(2);

                        Label lbl = new Label();
                        if (compteur > 1)
                        {
                            lbl.Text = "Engin " + engin + " (N°" + numero + ")" + " : " + compteur + " utilisations";
                        }
                        else
                        {
                            lbl.Text = "Engin " + engin + " (N°" + numero + ")" + " : " + compteur + " utilisation";
                        }

                        lbl.Location = new Point(10, y);
                        lbl.AutoSize = true;

                        pnlCaserne.Controls.Add(lbl);
                        y += 25;
                    }

                    dr.Close();
                }
                catch (SQLiteException err)
                {
                    MessageBox.Show("Erreur SQL : " + err.Message);
                }

            }
            else if (cboRequeteParCaserne.SelectedIndex == 2)
            {
                string sql = "SELECT p.codeTypeEngin,  p.numeroEngin,  SUM(CAST((julianday(IFNULL(m.dateHeureRetour, CURRENT_TIMESTAMP)) - julianday(m.dateHeureDepart)) * 24 * 60 AS INTEGER)) AS duree_minutes FROM PartirAvec p JOIN Mission m ON p.idMission = m.id WHERE p.idCaserne = " + cboCaserne.SelectedIndex + " GROUP BY p.codeTypeEngin, p.numeroEngin;";

                try
                {
                    SQLiteCommand com = new SQLiteCommand(sql, this.cx);
                    SQLiteDataReader dr = com.ExecuteReader();


                    pnlCaserne.Controls.Clear();



                    while (dr.Read())
                    {
                        String engin = dr.GetString(0);
                        int num = dr.GetInt32(1);
                        String numero = num.ToString();
                        int compteur = dr.GetInt32(2);

                        Label lbl = new Label();

                        lbl.Text = "Engin " + engin + " (N°" + numero + ")" + " : " + compteur + " minutes";


                        lbl.Location = new Point(10, y);
                        lbl.AutoSize = true;

                        pnlCaserne.Controls.Add(lbl);
                        y += 25;
                    }

                    dr.Close();
                }
                catch (SQLiteException err)
                {
                    MessageBox.Show("Erreur SQL : " + err.Message);
                }
            }
            if (cboCaserne.SelectedIndex == 0)
            {

                Label lbl = new Label();
                lbl.Text = "Veuillez choisir une caserne";


                lbl.Location = new Point(10, y);
                lbl.AutoSize = true;

                pnlCaserne.Controls.Add(lbl);
                y += 25;
            }
            if (cboRequeteParCaserne.SelectedIndex == 0)
            {
                Label lbl = new Label();
                lbl.Text = "Veuillez choisir un type de statistique";


                lbl.Location = new Point(10, y);
                lbl.AutoSize = true;

                pnlCaserne.Controls.Add(lbl);
            }
        }

        private void cboRequeteParCaserne_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isLoading) return;
            int y = 25;
            pnlCaserne.Visible = true;
            pnlCaserne.Controls.Clear();
            if (cboRequeteParCaserne.SelectedIndex == 1)
            {
                string sql = "SELECT codeTypeEngin, numeroEngin, COUNT(*) FROM PartirAvec WHERE idCaserne = " + cboCaserne.SelectedIndex + " GROUP BY idCaserne, codeTypeEngin, numeroEngin;";

                try
                {
                    SQLiteCommand com = new SQLiteCommand(sql, this.cx);
                    SQLiteDataReader dr = com.ExecuteReader();


                    pnlCaserne.Controls.Clear();



                    while (dr.Read())
                    {
                        String engin = dr.GetString(0);
                        int num = dr.GetInt32(1);
                        String numero = num.ToString();
                        int compteur = dr.GetInt32(2);

                        Label lbl = new Label();
                        if (compteur > 1)
                        {
                            lbl.Text = "Engin " + engin + " (N°" + numero + ")" + " : " + compteur + " utilisations";
                        }
                        else
                        {
                            lbl.Text = "Engin " + engin + " (N°" + numero + ")" + " : " + compteur + " utilisation";
                        }

                        lbl.Location = new Point(10, y);
                        lbl.AutoSize = true;

                        pnlCaserne.Controls.Add(lbl);
                        y += 25;
                    }

                    dr.Close();
                }
                catch (SQLiteException err)
                {
                    MessageBox.Show("Erreur SQL : " + err.Message);
                }

            }
            else if (cboRequeteParCaserne.SelectedIndex == 2)
            {
                string sql = "SELECT p.codeTypeEngin,  p.numeroEngin,  SUM(CAST((julianday(IFNULL(m.dateHeureRetour, CURRENT_TIMESTAMP)) - julianday(m.dateHeureDepart)) * 24 * 60 AS INTEGER)) AS duree_minutes FROM PartirAvec p JOIN Mission m ON p.idMission = m.id WHERE p.idCaserne = " + cboCaserne.SelectedIndex + " GROUP BY p.codeTypeEngin, p.numeroEngin;";

                try
                {
                    SQLiteCommand com = new SQLiteCommand(sql, this.cx);
                    SQLiteDataReader dr = com.ExecuteReader();


                    pnlCaserne.Controls.Clear();



                    while (dr.Read())
                    {
                        String engin = dr.GetString(0);
                        int num = dr.GetInt32(1);
                        String numero = num.ToString();
                        int compteur = dr.GetInt32(2);

                        Label lbl = new Label();

                        lbl.Text = "Engin " + engin + " (N°" + numero + ")" + " : " + compteur + " minutes";


                        lbl.Location = new Point(10, y);
                        lbl.AutoSize = true;

                        pnlCaserne.Controls.Add(lbl);
                        y += 25;
                    }

                    dr.Close();
                }
                catch (SQLiteException err)
                {
                    MessageBox.Show("Erreur SQL : " + err.Message);
                }

            }
            if (cboCaserne.SelectedIndex == 0)
            {

                Label lbl = new Label();
                lbl.Text = "Veuillez choisir une caserne";


                lbl.Location = new Point(10, y);
                lbl.AutoSize = true;

                pnlCaserne.Controls.Add(lbl);
                y += 25;
            }
            if (cboRequeteParCaserne.SelectedIndex == 0)
            {
                Label lbl = new Label();
                lbl.Text = "Veuillez choisir un type de statistique";


                lbl.Location = new Point(10, y);
                lbl.AutoSize = true;

                pnlCaserne.Controls.Add(lbl);
            }
        }

        private void cboStatGlobal_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isLoading) return;
            pnlGlobal.Controls.Clear();


            int y = 20;
            if (cboStatGlobal.SelectedIndex == 1)
            {
                cboTypeSinistre.SelectedIndex = 0;
                cboTypeSinistre.Visible = true;
                pnlGlobal.Visible = false;
            }
            else
            {
                cboTypeSinistre.Visible = false;
            }
            if (cboStatGlobal.SelectedIndex == 3)
            {
                cboHabilitation.SelectedIndex = 0;
                cboHabilitation.Visible = true;
                pnlGlobal.Visible = false;
            }
            else
            {
                cboHabilitation.Visible = false;
            }
            if (cboStatGlobal.SelectedIndex == 2)
            {
                pnlGlobal.Visible = true;
                String sql = "SELECT h.libelle, COUNT(*) FROM Mobiliser m JOIN Habilitation h ON m.idHabilitation=h.id GROUP BY m.idHabilitation ORDER BY  COUNT(*) DESC";

                try
                {
                    SQLiteCommand com = new SQLiteCommand(sql, this.cx);
                    SQLiteDataReader dr = com.ExecuteReader();
                    while (dr.Read())
                    {

                        String habilitation = dr.GetString(0);
                        int num = dr.GetInt32(1);
                        String nbinter = num.ToString();


                        Label lbl = new Label();

                        //lbl.Text = " L' Habilitation " + habilitation + " a été appelée ( " + nbinter + " ) fois";
                        if(nbinter == "1")
                        {
                            lbl.Text = "( " + nbinter + " )  appel pour l'habilitation " + habilitation;
                        }
                        else
                        {
                            int x = int.Parse(nbinter);
                            if (x > 9)
                            {
                                lbl.Text = "(" + nbinter + ") appels pour l'habilitation " + habilitation;
                            }
                            else
                            {
                                lbl.Text = "( " + nbinter + " ) appels pour l'habilitation " + habilitation;
                            }
                        }


                        lbl.Location = new Point(10, y);
                        lbl.AutoSize = true;

                        pnlGlobal.Controls.Add(lbl);
                        y += 25;
                    }

                    dr.Close();
                }
                catch (SQLiteException err)
                {
                    MessageBox.Show("Erreur SQL : " + err.Message);
                }
            }
        }

        private void cboHabilitation_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isLoading) return;
            int y = 20;
            pnlGlobal.Visible = true;
            pnlGlobal.Controls.Clear();
            if (cboHabilitation.SelectedIndex == 0)
            {
                Label lbl = new Label();
                lbl.Text = "Veulliez choisir une habilitation";
                lbl.Location = new Point(10, y);
                lbl.AutoSize = true;
                pnlGlobal.Controls.Add(lbl);
            }
            else
            {
                String sql = "SELECT g.abreviation,po.nom,po.prenom, p.matriculePompier FROM Passer p JOIN Pompier po ON po.matricule=p.matriculePompier JOIN Grade g ON po.codeGrade=g.code WHERE p.idHabilitation=" + cboHabilitation.SelectedIndex + "  GROUP BY p.matriculePompier ORDER BY po.matricule";
                try
                {
                    SQLiteCommand com = new SQLiteCommand(sql, this.cx);
                    SQLiteDataReader dr = com.ExecuteReader();

                    int compteur = 0;
                    while (dr.Read())
                    {
                        compteur++;
                    }
                    dr.Close(); // Toujours fermer le DataReader avant d'en rouvrir un

                    Label lbl = new Label();
                    lbl.Text = " Il y a " + compteur.ToString() + " Pompier(s) qui possède l'habilitation " + cboHabilitation.Text;
                    lbl.Location = new Point(10, y);
                    lbl.AutoSize = true;

                    pnlGlobal.Controls.Add(lbl);
                    y += 25;

                    // Nouvelle commande pour la 2e lecture
                    SQLiteCommand com2 = new SQLiteCommand(sql, this.cx);
                    SQLiteDataReader dr2 = com2.ExecuteReader(); // <- ici on utilise bien com2




                    while (dr2.Read())
                    {
                        String abrev = dr2.GetString(0);
                        String nom = dr2.GetString(1);
                        String prenom = dr2.GetString(2);
                        int matricule = dr2.GetInt32(3);
                        Label lbl2 = new Label();
                        lbl2.Text = abrev + " " + nom + " " + prenom + " Matricule N°" + matricule.ToString();
                        lbl2.Location = new Point(10, y);
                        lbl2.AutoSize = true;

                        pnlGlobal.Controls.Add(lbl2);
                        y += 25;
                    }

                    dr2.Close();
                }
                catch (SQLiteException err)
                {
                    MessageBox.Show("Erreur SQL : " + err.Message);
                }
            }
        }

        private void cboTypeSinistre_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isLoading) return;
            pnlGlobal.Visible = true;
            pnlGlobal.Controls.Clear();
            int y = 20;
            if (cboTypeSinistre.SelectedIndex == 0)
            {
                Label lbl = new Label();
                lbl.Text = "Veulliez choisir un type de sinistre";
                lbl.Location = new Point(10, y);
                lbl.AutoSize = true;
                pnlGlobal.Controls.Add(lbl);
            }
            else
            {
                String sql = "SELECT COUNT(*), s.libelle FROM Mission m JOIN NatureSinistre s ON s.id=m.idNatureSinistre WHERE s.id =" + cboTypeSinistre.SelectedIndex + " GROUP BY s.id ORDER BY count(*) DESC";
                try
                {
                    SQLiteCommand com = new SQLiteCommand(sql, this.cx);
                    SQLiteDataReader dr = com.ExecuteReader();
                    int compteur = 0;
                    while (dr.Read())
                    {
                        pnlGlobal.Visible = true;

                        int num = dr.GetInt32(0);
                        String nbinter = num.ToString();
                        String type = dr.GetString(1);
                        Label lbl = new Label();

                        lbl.Text = " Il y a eu " + nbinter + " intervention de type " + type;


                        lbl.Location = new Point(10, y);
                        lbl.AutoSize = true;

                        pnlGlobal.Controls.Add(lbl);
                        compteur++;
                        y += 25;
                    }
                    if (compteur == 0)
                    {
                        Label lbl = new Label();

                        lbl.Text = " Il n'y a eu aucune intervention de type " + cboTypeSinistre.Text;


                        lbl.Location = new Point(10, y);
                        lbl.AutoSize = true;

                        pnlGlobal.Controls.Add(lbl);
                    }

                    dr.Close();
                }
                catch (SQLiteException err)
                {
                    MessageBox.Show("Erreur SQL : " + err.Message);
                }
            }
        }

        private void lblStatCasern_Click(object sender, EventArgs e)
        {

        }
    }
}
