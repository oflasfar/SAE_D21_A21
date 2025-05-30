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


namespace UCRecapitulMission
{

    public partial class UCAffichageMission : UserControl
    {
        private List<object[]> pompiersDeLaMission = new List<object[]>();
        private List<object[]> enginsDeLaMission = new List<object[]>();
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
                if (!int.TryParse(lblID2.Text.Trim(), out int id))
                {
                    MessageBox.Show("❌ L'ID de mission est invalide.");
                    return;
                }

                string dateHeureActuelle = DateTime.Now.ToString("yyyy-MM-dd HH:mm");

                DataTable dtMission = this.ds.Tables["Mission"];
                DataRow[] rowsMission = dtMission.Select($"id = {id}");
                if (rowsMission.Length == 0)
                {
                    MessageBox.Show("❌ Mission non trouvée.");
                    return;
                }

                DataRow mission = rowsMission[0];
                mission["dateHeureRetour"] = dateHeureActuelle;
                mission["terminee"] = 1;
                this.dateRetour = dateHeureActuelle;

                // 🔸 SAUVEGARDE des données AVANT modification
                var dtMobiliser = this.ds.Tables["Mobiliser"];
                var lignesMobilises = dtMobiliser.Select($"idMission = {id}");
                this.pompiersDeLaMission = lignesMobilises
                    .Select(r => (object[])r.ItemArray.Clone())
                    .ToList();

                var dtPartirAvec = this.ds.Tables["PartirAvec"];
                var lignesEngins = dtPartirAvec.Select($"idMission = {id}");
                this.enginsDeLaMission = lignesEngins
                    .Select(r => (object[])r.ItemArray.Clone())
                    .ToList();

                // 🔁 Mise à jour dans la base de données
                string sqlCheck = "SELECT COUNT(*) FROM Mission";
                using (var checkCmd = new SQLiteCommand(sqlCheck, Connexion.Connec))
                {
                    if (Connexion.Connec.State != ConnectionState.Open)
                        Connexion.Connec.Open();

                    int count = Convert.ToInt32(checkCmd.ExecuteScalar());

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

                // ✅ Libération des pompiers
                var dtPompier = this.ds.Tables["Pompier"];
                foreach (var ligne in lignesMobilises)
                {
                    int matricule = Convert.ToInt32(ligne["matriculePompier"]);
                    DataRow[] rowsPompier = dtPompier.Select($"matricule = {matricule}");
                    if (rowsPompier.Length > 0)
                        rowsPompier[0]["enMission"] = 0;
                }

                // ✅ Libération des engins
                var dtEngins = this.ds.Tables["Engin"];
                foreach (var ligne in lignesEngins)
                {
                    if (ligne["numeroEngin"] != DBNull.Value)
                    {
                        int num = Convert.ToInt32(ligne["numeroEngin"]);
                        DataRow[] enginRows = dtEngins.Select($"numero = {num}");
                        foreach (var r in enginRows)
                            r["enMission"] = 0;
                    }
                }

                this.ds.AcceptChanges();
                MessageBox.Show("✅ Mission clôturée avec succès !");
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
                string nomFichier = $"Mission_{id}.pdf";

                if (!Directory.Exists(bureauPath))
                    Directory.CreateDirectory(bureauPath);

                string cheminPDF = Path.Combine(bureauPath, nomFichier);

                if (File.Exists(cheminPDF))
                {
                    MessageBox.Show("❗ Ce rapport existe déjà dans le Bureau.");
                    return;
                }

                Document doc = new Document();

                try
                {
                    PdfWriter.GetInstance(doc, new FileStream(cheminPDF, FileMode.Create));
                    doc.Open();

                    // 📝 TITRE
                    iTextSharp.text.Font titreFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 18);
                    doc.Add(new Paragraph($"RAPPORT DE MISSION N° {id}", titreFont));
                    doc.Add(new Paragraph("\n"));

                    // 📅 INFOS DE BASE
                    string debut = lblDateDebut.Text.Split(':')[1].Trim();
                    string type = lblTypeMission.Text;
                    string description = lblDescription.Text;
                    string caserne = lblCaserne.Text.Split(':')[1].Trim();

                    doc.Add(new Paragraph($"📅 Début de mission : {debut}"));
                    doc.Add(new Paragraph($"📅 Fin de mission   : {this.dateRetour}"));
                    doc.Add(new Paragraph($"🏢 Caserne          : {caserne}"));
                    doc.Add(new Paragraph($"📞 Motif d'appel    : {type}"));
                    doc.Add(new Paragraph($"🔥 Nature sinistre  : {description}"));
                    doc.Add(new Paragraph("\n-----------------------------\n"));

                    // 🚒 ENGINS MOBILISÉS
                    doc.Add(new Paragraph("🚒 Engins mobilisés :"));

                    var lignesEngins = ds.Tables["PartirAvec"].Select($"idMission = {id}");
                    var dtEngin = ds.Tables["Engin"];
                    var dtTypeEngin = ds.Tables["TypeEngin"];

                    if (lignesEngins.Length == 0)
                    {
                        doc.Add(new Paragraph("→ Aucun engin mobilisé."));
                    }
                    else
                    {
                        foreach (var ligne in lignesEngins)
                        {
                            int num = Convert.ToInt32(ligne["numeroEngin"]);
                            var engin = dtEngin.Select($"numero = {num}").FirstOrDefault();
                            if (engin != null)
                            {
                                string typeCode = engin["codeTypeEngin"].ToString();
                                // libelle = dtTypeEngin.Select($"code = '{typeCode}'").FirstOrDefault()?["libelle"]?.ToString() ?? typeCode;
                                string libelleType = dtTypeEngin.Select($"code = '{typeCode}'").FirstOrDefault()?["nom"].ToString() ?? typeCode;

                                doc.Add(new Paragraph($"→({libelleType})"));
                            }
                        }
                    }

                    // 👨‍🚒 POMPIERS MOBILISÉS
                    doc.Add(new Paragraph("\n👨‍🚒 Pompiers mobilisés :"));

                    var lignesPompiers = ds.Tables["Mobiliser"].Select($"idMission = {id}");
                    var dtPompier = ds.Tables["Pompier"];
                    var dtGrade = ds.Tables["Grade"];
                    var dtHabilitation = ds.Tables["Habilitation"];

                    if (lignesPompiers.Length == 0)
                    {
                        doc.Add(new Paragraph("→ Aucun pompier mobilisé."));
                    }
                    else
                    {
                        foreach (var ligne in lignesPompiers)
                        {
                            int matricule = Convert.ToInt32(ligne["matriculePompier"]);
                            int idHab = Convert.ToInt32(ligne["idHabilitation"]);

                            var pompier = dtPompier.Select($"matricule = {matricule}").FirstOrDefault();
                            if (pompier != null)
                            {
                                string nom = pompier["nom"].ToString();
                                string prenom = pompier["prenom"].ToString();
                                string grade = dtGrade.Select($"code = '{pompier["codeGrade"]}'").FirstOrDefault()?["libelle"].ToString() ?? "";
                                string hab = dtHabilitation.Select($"id = {idHab}").FirstOrDefault()?["libelle"].ToString() ?? "";

                                doc.Add(new Paragraph($"→ {grade} {prenom} {nom} (matricule : {matricule}) - {hab}"));
                            }
                        }
                    }

                    doc.Add(new Paragraph("\n📄 Rapport généré le : " + DateTime.Now.ToString("dd/MM/yyyy HH:mm")));
                    doc.Close();
                    MessageBox.Show("✅ Rapport PDF créé avec succès dans :\n" + cheminPDF, "Succès");
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
    }
}
