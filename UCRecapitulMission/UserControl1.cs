using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml.Linq;
using iTextSharp.text;
using iTextSharp.text.pdf;
using static System.Net.Mime.MediaTypeNames;
using System.Data.SQLite;
using SAEPageMission;
using System.Reflection.Emit;
using iTextFont = iTextSharp.text.Font;



namespace UCRecapitulMission
{

    public partial class UCAffichageMission : UserControl
    {
        
        private SQLiteConnection cx;
        private DataSet ds;
        private String dateRetour;
        string type;
        public UCAffichageMission()
        {
            InitializeComponent();
        }
        public UCAffichageMission(int id, SQLiteConnection cxi, DataSet dsi)
        {
            
            InitializeComponent();
            lblID2.Text =id.ToString();
            foreach (DataRow row in dsi.Tables["Mission"].Rows)
            {
                if (id == Convert.ToInt32(row["id"]))
                {
                    //MessageBox.Show(row["idCaserne"].ToString());
                    lblCaserne.Text = "Caserne : "+ row["idCaserne"].ToString();
                    lblDateDebut.Text = "Debut le : " + Convert.ToDateTime(row["dateHeureDepart"]).ToString("dd/MM/yyyy");
                    type  = row["motifAppel"].ToString();
                    string nature = row["idNatureSinistre"].ToString();
                    dateRetour = row["dateHeureRetour"].ToString();
                    foreach (DataRow naturerow in dsi.Tables["NatureSinistre"].Rows)
                    {
                        if (naturerow["id"].ToString() == nature)
                        {
                            lblDescription.Text = naturerow["libelle"].ToString();
                        }
                    }
                }
            }
            this.cx = cxi;
            this.ds = dsi;
        }

        private void UserControl1_Load(object sender, EventArgs e)
        {

        }

        //Bouton pour cloturer la mission
        private void btnCloturer_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(this.dateRetour))
            {
                MessageBox.Show("🚫 La mission a déjà une date de retour.");
                return;
            }
            //Pour demander la confirmation de la clôture de la mission
            DialogResult result = MessageBox.Show(
                "Clôturer la mission avec l'heure actuelle ?",
                "Confirmation", MessageBoxButtons.YesNo);
            //Verification de la reponse de l'utilisateur
            if (result != DialogResult.Yes)
                return;

            try
            {
                if (!int.TryParse(lblID2.Text.Trim(), out int id))
                {
                    MessageBox.Show("❌ L'ID de mission est invalide.");
                    return;
                }
                //On stocke la date actuelelle dans une variable
                string dateHeureActuelle = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
                //On cherche la mission dans la base de données la ligne
                DataTable dtMission = this.ds.Tables["Mission"];
                //rowsMission contient les lignes de la table Mission qui ont l'id correspondant
                DataRow[] rowsMission = dtMission.Select($"id = {id}");
                //Si la mission n'existe pas, on affiche un message d'erreur
                if (rowsMission.Length == 0)
                {
                    MessageBox.Show("❌ Mission non trouvée.");
                    return;
                }
                //On récupère la première ligne de la mission trouvée
                DataRow mission = rowsMission[0];
                mission["dateHeureRetour"] = dateHeureActuelle;
                mission["terminee"] = 1;
                this.dateRetour = dateHeureActuelle;

                // 🔸 SAUVEGARDE des données AVANT modification
                DataTable dtMobiliser = this.ds.Tables["Mobiliser"];
                DataRow[] lignesMobilises = dtMobiliser.Select($"idMission = {id}");

                DataTable dtPartirAvec = this.ds.Tables["PartirAvec"];
                DataRow[] lignesEngins = dtPartirAvec.Select($"idMission = {id}");



                // 🔁 Mise à jour dans la base de données
                string sqlCheck = "SELECT COUNT(*) FROM Mission";
                using (SQLiteCommand checkCmd = new SQLiteCommand(sqlCheck, Connexion.Connec))
                {
                    if (Connexion.Connec.State != ConnectionState.Open)
                        Connexion.Connec.Open();

                    int count = Convert.ToInt32(checkCmd.ExecuteScalar());
                    //On regarde si l'id de la mission existe déjà dans la base de données
                    if (count > id)
                    {
                        string sqlUpdate = @"UPDATE Mission SET 
                    motifAppel = @motif,
                    adresse = @adresse,
                    cp = @cp,
                    ville = @ville,
                    dateHeureDepart = @depart,
                    dateHeureRetour = @retour,
                    idCaserne = @caserne,
                    idNatureSinistre = @nature,
                    terminee = @terminee
                    WHERE id = @id";

                        using (SQLiteCommand cmd = new SQLiteCommand(sqlUpdate, Connexion.Connec))
                        {
                            cmd.Parameters.AddWithValue("@id", mission["id"]);
                            cmd.Parameters.AddWithValue("@motif", mission["motifAppel"]);
                            cmd.Parameters.AddWithValue("@adresse", mission["adresse"]);
                            cmd.Parameters.AddWithValue("@cp", mission["cp"]);
                            cmd.Parameters.AddWithValue("@ville", mission["ville"]);
                            cmd.Parameters.AddWithValue("@depart", mission["dateHeureDepart"]);
                            cmd.Parameters.AddWithValue("@retour", mission["dateHeureRetour"]);
                            cmd.Parameters.AddWithValue("@caserne", mission["idCaserne"]);
                            cmd.Parameters.AddWithValue("@nature", mission["idNatureSinistre"]);
                            cmd.Parameters.AddWithValue("@terminee", 1);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        //on fait une insertion dans la base de données
                        string sqlInsert = @"INSERT INTO Mission 
                    (id, motifAppel, adresse, cp, ville, dateHeureDepart, dateHeureRetour, idCaserne, idNatureSinistre, terminee) 
                    VALUES (@id, @motif, @adresse, @cp, @ville, @depart, @retour, @caserne, @nature, @terminee)";

                        using (SQLiteCommand cmd = new SQLiteCommand(sqlInsert, Connexion.Connec))
                        {
                            cmd.Parameters.AddWithValue("@id", id);
                            cmd.Parameters.AddWithValue("@motif", mission["motifAppel"]);
                            cmd.Parameters.AddWithValue("@adresse", mission["adresse"]);
                            cmd.Parameters.AddWithValue("@cp", mission["cp"]);
                            cmd.Parameters.AddWithValue("@ville", mission["ville"]);
                            cmd.Parameters.AddWithValue("@depart", mission["dateHeureDepart"]);
                            cmd.Parameters.AddWithValue("@retour", mission["dateHeureRetour"]);
                            cmd.Parameters.AddWithValue("@caserne", mission["idCaserne"]);
                            cmd.Parameters.AddWithValue("@nature", mission["idNatureSinistre"]);
                            cmd.Parameters.AddWithValue("@terminee", 1);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }

                //Libération des pompiers
                DataTable dtPompier = this.ds.Tables["Pompier"];
                foreach (DataRow ligne in lignesMobilises)
                {
                    int matricule = Convert.ToInt32(ligne["matriculePompier"]);
                    DataRow[] rowsPompier = dtPompier.Select($"matricule = {matricule}");
                    if (rowsPompier.Length > 0)
                        rowsPompier[0]["enMission"] = 0;
                }

                //Libération des engins
                DataTable dtEngins = this.ds.Tables["Engin"];
                foreach (DataRow ligne in lignesEngins)
                {
                    if (ligne["numeroEngin"] != DBNull.Value)
                    {
                        int num = Convert.ToInt32(ligne["numeroEngin"]);
                        DataRow[] enginRows = dtEngins.Select($"numero = {num}");
                        foreach (DataRow r in enginRows)
                            r["enMission"] = 0;
                    }
                }
                //Ajouter les changement dans la base de données locale
                this.ds.AcceptChanges();
                //MessageBox.Show("✅ Mission clôturée avec succès !");
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Erreur lors de la clôture de la mission : " + ex.Message);
            }
        }






        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnRapport_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(this.dateRetour))
            {
                string bureauPath = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                    "Rapports Missions"
                );

                string id = lblID2.Text.Trim();
                string nomFichier = $"Mission_{id}_Rapport.pdf";
                if (!Directory.Exists(bureauPath)) Directory.CreateDirectory(bureauPath);
                string cheminPDF = Path.Combine(bureauPath, nomFichier);

                if (File.Exists(cheminPDF))
                {
                    MessageBox.Show("❗ Ce rapport existe déjà dans le Bureau.");
                    return;
                }

                Document doc = new Document(PageSize.A4, 50, 50, 50, 50);
                try
                {
                    PdfWriter.GetInstance(doc, new FileStream(cheminPDF, FileMode.Create));
                    doc.Open();

                    // Fonts
                    iTextFont titreFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 18, BaseColor.BLACK);
                    iTextFont sectionFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14, BaseColor.DARK_GRAY);
                    iTextFont normalFont = FontFactory.GetFont(FontFactory.HELVETICA, 12, BaseColor.BLACK);
                    iTextFont smallFont = FontFactory.GetFont(FontFactory.HELVETICA, 10, BaseColor.GRAY);

                    // Logo
                    string cheminLogo = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images", "logo.png");
                    if (File.Exists(cheminLogo))
                    {
                        iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(cheminLogo);
                        logo.ScaleToFit(100f, 100f);
                        logo.Alignment = Element.ALIGN_CENTER;
                        doc.Add(logo);
                    }

                    doc.Add(new Paragraph("\n"));
                    doc.Add(new Paragraph($"RAPPORT DE MISSION N° {id}", titreFont) { Alignment = Element.ALIGN_CENTER });
                    doc.Add(new Paragraph("\n"));

                    // Infos générales
                    string debut = lblDateDebut.Text.Split(':')[1].Trim();
                    string description = lblDescription.Text;
                    string caserne = lblCaserne.Text.Split(':')[1].Trim();

                    PdfPTable tableInfo = new PdfPTable(1);
                    tableInfo.WidthPercentage = 100;
                    tableInfo.SpacingAfter = 10f;

                    tableInfo.AddCell(new PdfPCell(new Phrase("Informations Générales", sectionFont)) { Colspan = 1 });
                    tableInfo.AddCell(new Phrase($"📅 Début : {debut}", normalFont));
                    tableInfo.AddCell(new Phrase($"📅 Fin   : {this.dateRetour}", normalFont));
                    tableInfo.AddCell(new Phrase($"🏢 Caserne : {caserne}", normalFont));
                    tableInfo.AddCell(new Phrase($"📞 Motif : {type}", normalFont));
                    tableInfo.AddCell(new Phrase($"🔥 Nature : {description}", normalFont));
                    doc.Add(tableInfo);

                    // Engins mobilisés
                    doc.Add(new Paragraph("🚒 Engins mobilisés :", sectionFont));
                    var lignesEngins = ds.Tables["PartirAvec"].Select($"idMission = {id}");
                    var dtEngin = ds.Tables["Engin"];
                    var dtTypeEngin = ds.Tables["TypeEngin"];

                    if (lignesEngins.Length == 0)
                    {
                        doc.Add(new Paragraph("→ Aucun engin mobilisé.", normalFont));
                    }
                    else
                    {
                        foreach (var ligne in lignesEngins)
                        {
                            int num = Convert.ToInt32(ligne["numeroEngin"]);
                            var engin = dtEngin.Select($"numero = {num}").FirstOrDefault();
                            if (engin != null)
                            {
                                string code = engin["codeTypeEngin"].ToString();
                                string libelle = dtTypeEngin.Select($"code = '{code}'").FirstOrDefault()?["nom"].ToString() ?? code;
                                doc.Add(new Paragraph($"→ {libelle}", normalFont));
                            }
                        }
                    }

                    doc.Add(new Paragraph("\n"));

                    // Pompiers mobilisés
                    doc.Add(new Paragraph("👨‍🚒 Pompiers mobilisés :", sectionFont));
                    string lignes = recapTableMobiliser(id);
                    doc.Add(new Paragraph(lignes, normalFont));

                    // Date de génération
                    doc.Add(new Paragraph($"\n📄 Rapport généré le : {DateTime.Now:dd/MM/yyyy HH:mm}", smallFont));

                    doc.Close();
                    MessageBox.Show("✅ Rapport généré avec succès !");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("❌ Erreur lors de la génération du rapport :\n" + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("❌ La mission doit d'abord être terminée pour générer le rapport.");
            }
        }




        private string remplirInfoPDF(string idMission, string idCaserne)
        {
            String descriptionDetaille = recapTableMission(idMission);
            descriptionDetaille = recapTableMobiliser(idMission);

            return descriptionDetaille;
        }
        private string recapTableMission(string idMission)
        {
            string retour = "";
            foreach (DataRow row in ds.Tables["Mission"].Rows)
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
            string retour = "\nLes pompier mobilisée sont:\n\n";
            foreach (DataRow row in ds.Tables["Mobiliser"].Rows)
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
            foreach (DataRow row in ds.Tables["Pompier"].Rows)
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
            foreach (DataRow row in ds.Tables["Grade"].Rows)
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
            foreach (DataRow row in ds.Tables["Habilitation"].Rows)
            {
                if (row["id"].ToString() == idHabilitation)
                {
                    retour = "mobiliser en tant que: " + row["libelle"].ToString() + "\n";

                }
            }
            return retour;

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblId_Click(object sender, EventArgs e)
        {

        }

        private void lblCaserne_Click(object sender, EventArgs e)
        {

        }
    }
}
