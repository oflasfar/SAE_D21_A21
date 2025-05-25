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

namespace UCRecapitulMission
{

    public partial class UCAffichageMission : UserControl
    {
        private SQLiteConnection cx;
        private DataSet ds;
        private String dateRetour;
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
                    lblDateDebut.Text = "Debut le : " + row["dateHeureDepart"].ToString();
                    lblTypeMission.Text  = row["motifAppel"].ToString();

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


        private void btnCloturer_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(this.dateRetour))
            {
                MessageBox.Show("🚫 La mission a déjà une date de retour.");
                return;
            }

            DialogResult result = MessageBox.Show(
                "Clôturer la mission avec l'heure actuelle ?",
                "Confirmation", MessageBoxButtons.YesNo);

            if (result != DialogResult.Yes)
                return;

            try
            {
                // 🔹 Récupérer l’ID directement depuis lblID2
                if (!int.TryParse(lblID2.Text.Trim(), out int id))
                {
                    MessageBox.Show("❌ L'ID de mission est invalide.");
                    return;
                }

                MessageBox.Show("ID de mission : " + id);

                string dateHeureActuelle = DateTime.Now.ToString("yyyy-MM-dd HH:mm");

                // 🔹 Mettre à jour dans le DataSet
                DataTable dt = this.ds.Tables["Mission"];
                DataRow[] rows = dt.Select($"id = {id}");
                if (rows.Length == 0)
                {
                    MessageBox.Show("❌ Mission non trouvée dans le DataSet.");
                    return;
                }

                DataRow mission = rows[0];
                mission["dateHeureRetour"] = dateHeureActuelle;
                mission["terminee"] = 1;
                this.dateRetour = dateHeureActuelle;

                // 🔹 Vérifier si la mission est dans la base
                string sqlCheck = "SELECT COUNT(*) FROM Mission";
                using (var checkCmd = new SQLiteCommand(sqlCheck, Connexion.Connec))
                {
                    if (Connexion.Connec.State != ConnectionState.Open)
                        Connexion.Connec.Open();

                    
                    int count = Convert.ToInt32(checkCmd.ExecuteScalar());
                    
                    if (count > id)
                    {
                        MessageBox.Show("count : "+count+"//id : "+id);
                        MessageBox.Show("✅ update");
                        // 🔁 UPDATE
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

                        using (var cmd = new SQLiteCommand(sqlUpdate, Connexion.Connec))
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
                        MessageBox.Show("✅ insert");
                        // 🆕 INSERT
                        string sqlInsert = @"INSERT INTO Mission 
                    (id, motifAppel, adresse, cp, ville, dateHeureDepart, dateHeureRetour, idCaserne, idNatureSinistre, terminee) 
                    VALUES (@id, @motif, @adresse, @cp, @ville, @depart, @retour, @caserne, @nature, @terminee)";

                        using (var cmd = new SQLiteCommand(sqlInsert, Connexion.Connec))
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

                MessageBox.Show("✅ Mission clôturée et bien enregistrée dans la base !");
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Erreur lors de la clôture ou l'ajout :\n" + ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }





        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnRapport_Click(object sender, EventArgs e)
        {
            if (this.dateRetour != null && this.dateRetour != "")
            {
                // Récupérer le chemin du dossier Téléchargements
                string downloadsPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                "Downloads",
                "Rapport de mission"
                );
                string id = lblId.Text.Split(':')[1].Trim();
                string nomfichier = "mission" + id + ".pdf";

                // Créer le dossier s’il n’existe pas
                if (!Directory.Exists(downloadsPath))
                {
                    Directory.CreateDirectory(downloadsPath);
                    MessageBox.Show("dossier creer");
                }

                // Chemin complet du fichier PDF à créer
                string cheminPDF = Path.Combine(downloadsPath, nomfichier);
                if (File.Exists(cheminPDF))
                {
                    MessageBox.Show("le pdf existe deja");
                }
                else
                {
                    Document doc = new Document();
                    try
                    {
                        PdfWriter.GetInstance(doc, new FileStream(cheminPDF, FileMode.Create));
                        doc.Open();

                        // Ajout de contenu au PDF



                        iTextSharp.text.Font titreFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 17);

                        // Ajouter un paragraphe avec cette police
                        doc.Add(new Paragraph("Rapport de mission N° " + id, titreFont));
                        doc.Add(new Paragraph("\n\n"));

                        string debut = lblDateDebut.Text.Split(':')[1].Trim();
                        doc.Add(new Paragraph("Debut de mission: " + debut));


                        
                        doc.Add(new Paragraph("Fin de mission: " + this.dateRetour));

                        doc.Add(new Paragraph(lblId.Text));

                        string type = lblDescription.Text;
                        doc.Add(new Paragraph("Type: " + type));

                        string descrip = lblTypeMission.Text;
                        doc.Add(new Paragraph("apeler pour: " + descrip));

                        string paragraphe = remplirInfoPDF(id, lblCaserne.Text.Split(':')[1].Trim());
                        doc.Add(new Paragraph(paragraphe));





                        MessageBox.Show("PDF enregistré dans :\n" + cheminPDF, "Succès");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erreur : " + ex.Message, "Erreur");
                    }
                    finally
                    {
                        doc.Close();
                    }
                }
            }
            else
            {
                MessageBox.Show("La mission doit d'abbord être terminé");
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
    }
}
