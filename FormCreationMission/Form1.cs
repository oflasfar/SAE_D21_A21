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
using System.IO;
using System.Runtime.Remoting.Contexts;
using iText.Kernel.Pdf;
using iText.Layout.Element;
using iText.Layout;
using iText.Kernel.Font;
using iText.IO.Font.Constants;
using iText.Layout.Properties;



namespace FormCreationMission
{
    public partial class Form1: Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            gbMobilisation.Visible = false;

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
            MessageBox.Show(xx);
            lblId.Text = (MesDatas.DsGlobal.Tables.Count + 1).ToString();
            lblDateDeclanchee.Text = DateTime.Now.ToString();
            //Premiere comboBox : NatureSinistre
            cbNatureSinistre.DataSource = MesDatas.DsGlobal.Tables["NatureSinistre"];
            cbNatureSinistre.DisplayMember = "libelle";
            cbNatureSinistre.ValueMember = "id";
            //Deuxieme comboBox : TypeIntervention
            cbCaserneImmobiliser.DataSource = MesDatas.DsGlobal.Tables["Caserne"];
            cbCaserneImmobiliser.DisplayMember = "nom";
            cbCaserneImmobiliser.ValueMember = "id";
        }

        private void btnQuitter_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void cbNatureSinistre_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void cbCaserneImmobiliser_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void gbMobilisation_Enter(object sender, EventArgs e)
        {
            
        }

        private bool estEnMission(int matricule)
        {
            var mobilisations = MesDatas.DsGlobal.Tables["Mobiliser"]
                .Select($"matriculePompier = {matricule}");

            foreach (var mobilisation in mobilisations)
            {
                int idMission = Convert.ToInt32(mobilisation["idMission"]);

                DataRow[] mission = MesDatas.DsGlobal.Tables["Mission"]
                    .Select($"id = {idMission} AND terminee = 0");

                if (mission.Length > 0)
                    return true;
            }

            return false;
        }


        private bool estEnConge(DataRow pompier)
        {
            return Convert.ToBoolean(pompier["enConge"]);
        }



        private void btnConstituerEquipe_Click(object sender, EventArgs e)
        {
            gbMobilisation.Visible = true;

            List<(string codeTypeEngin, int nombre)> enginsNecessaires = new List<(string, int)>();

            //Recuperation de la valeur returner par la combobox NatureSinistre
            int idNatureSinistre = Convert.ToInt32(cbNatureSinistre.SelectedValue);
            //Recuperation de la valeur returner par la combobox Caserne
            int idCaserne = Convert.ToInt32(cbCaserneImmobiliser.SelectedValue);
            MessageBox.Show("ID caserne sélectionnée : " + idCaserne);



            // Récupération des engins nécessaires
            foreach (DataRow row in MesDatas.DsGlobal.Tables["Necessiter"].Select("idNatureSinistre = " + idNatureSinistre))
            {
                string type = row["codeTypeEngin"].ToString();
                int nb = Convert.ToInt32(row["nombre"]);
                // Vérifier si la caserne dispose de ce type d’engin
                var enginsDispoDansCaserne = MesDatas.DsGlobal.Tables["Engin"].Select(
                    $"codeTypeEngin = '{type}' AND idCaserne = '{idCaserne}' AND enMission = 0 AND enPanne = 0"
                );
                //MessageBox.Show($"[DEBUG] {enginsDispoDansCaserne.Length} engin(s) dispo de type {type} dans caserne {idCaserne}");


                if (enginsDispoDansCaserne.Length >= nb)
                {
                    enginsNecessaires.Add((type, nb));
                }
            }

            // Préparer le DataGridView
            dgvEngins.Rows.Clear();
            if (dgvEngins.Columns.Count == 0)
            {
                dgvEngins.Columns.Add("typeEngin", "Type d'engin");
                dgvEngins.Columns.Add("nombre", "Quantité requise");
            }

            // Remplir le DataGridView
            foreach (var (type, nb) in enginsNecessaires)
            {
                dgvEngins.Rows.Add(type, nb);
            }
            //////
            /////////
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            /////////
            ///

            dgvPompiers.Rows.Clear();
            foreach (var (typeEngin, nombre) in enginsNecessaires)
            {
                // Étape 1 : Récupérer les ID d’habilitations nécessaires pour ce type d’engin
                var habilitations = MesDatas.DsGlobal.Tables["Embarquer"]
                    .Select($"codeTypeEngin = '{typeEngin}'")
                    .Select(row => Convert.ToInt32(row["idHabilitation"]))
                    .Distinct();

                List<DataRow> pompiersEligibles = new List<DataRow>();

                foreach (int idHab in habilitations)
                {
                    var rowsPasser = MesDatas.DsGlobal.Tables["Passer"]
                        .Select($"idHabilitation = {idHab}");

                    foreach (var passerRow in rowsPasser)
                    {
                        int matricule = Convert.ToInt32(passerRow["matriculePompier"]);
                        DataRow pompier = MesDatas.DsGlobal.Tables["Pompier"]
                            .Select($"matricule = {matricule}")
                            .FirstOrDefault();

                        if (pompier != null && !estEnMission(matricule) && !estEnConge(pompier))
                        {
                            if (!pompiersEligibles.Contains(pompier))
                                pompiersEligibles.Add(pompier);
                        }
                    }
                }

                // Prendre autant de pompiers que nécessaire (ou moins si pas assez)
                var selection = pompiersEligibles.Take(nombre).ToList();
                if (dgvPompiers.Columns.Count == 0)
                {
                    dgvPompiers.Columns.Add("matricule", "Matricule");
                    dgvPompiers.Columns.Add("nom", "Nom");
                    dgvPompiers.Columns.Add("prenom", "Prénom");
                }
                // Affichage dans DataGridView
                foreach (var p in selection)
                {
                    dgvPompiers.Rows.Add(p["matricule"], p["nom"], p["prenom"]);
                }

                ///////////////////////////:
                ///
                
            }

        }

        private void dgvEngins_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled=true;
        }

        private void dgvPompiers_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void btnRapport_Click(object sender, EventArgs e)
        {
            try
            {
                string nFichier = "Rapport_Mission.pdf";
                string chemin = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), nFichier);

                using (PdfWriter writer = new PdfWriter(chemin))
                using (PdfDocument pdf = new PdfDocument(writer))
                using (Document doc = new Document(pdf))
                {
                    // Titre
                    doc.Add(new Paragraph("📋 RAPPORT DE MISSION")
                        .SetFontSize(16)
                        .SetTextAlignment(TextAlignment.CENTER));

                    doc.Add(new Paragraph("\n"));

                    // Infos principales
                    doc.Add(new Paragraph($"📌 Mission n° : {lblId.Text ?? "N/A"}"));
                    doc.Add(new Paragraph($"📅 Date de déclenchement : {lblDateDeclanchee.Text ?? "N/A"}"));
                    doc.Add(new Paragraph($"🔥 Nature du sinistre : {cbNatureSinistre.Text ?? "N/A"}"));
                    doc.Add(new Paragraph($"🏢 Caserne : {cbCaserneImmobiliser.Text ?? "N/A"}"));
                    doc.Add(new Paragraph($"📍 Adresse : {(txtRue?.Text ?? "")}, {(txtCodePostale?.Text ?? "")}, {(txtVille?.Text ?? "")}"));
                    doc.Add(new Paragraph($"📝 Description : {(txtMotif?.Text ?? "")}"));

                    doc.Add(new Paragraph("\n==================================================\n"));

                    // Engins
                    doc.Add(new Paragraph("🚒 Engins mobilisés :"));
                    foreach (DataGridViewRow row in dgvEngins.Rows)
                    {
                        if (!row.IsNewRow && row.Cells[0].Value != null)
                        {
                            string type = row.Cells[0].Value.ToString();
                            string numero = row.Cells.Count > 1 && row.Cells[1].Value != null ? row.Cells[1].Value.ToString() : "N/A";
                            doc.Add(new Paragraph($"- {type} (n° {numero})"));
                        }
                    }

                    // Pompiers
                    doc.Add(new Paragraph("\n👨‍🚒 Pompiers mobilisés :"));
                    foreach (DataGridViewRow row in dgvPompiers.Rows)
                    {
                        if (!row.IsNewRow && row.Cells[0].Value != null)
                        {
                            string nom = row.Cells.Count > 1 ? row.Cells[1].Value?.ToString() ?? "NOM" : "NOM";
                            string prenom = row.Cells.Count > 2 ? row.Cells[2].Value?.ToString() ?? "PRÉNOM" : "PRÉNOM";
                            doc.Add(new Paragraph($"- {prenom} {nom}"));
                        }
                    }

                    // Signature
                    doc.Add(new Paragraph($"\n📄 Rapport généré le {DateTime.Now:dd/MM/yyyy HH:mm}"));
                }

                MessageBox.Show("✅ Rapport PDF généré sur le bureau !");
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ ERREUR PDF : " + ex.Message);
            }

        }
    }
}
