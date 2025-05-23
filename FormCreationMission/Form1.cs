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
using System.Globalization;
using iTextSharp.text;
using iTextSharp.text.pdf;





namespace FormCreationMission
{
    public partial class Form1 : Form
    {
        Form4 tableauDeBord;
        public Form1(Form4 tableauBord)
        {
            InitializeComponent();
            this.tableauDeBord = tableauBord;
            
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            MessageBox.Show("Form1 chargé");
            gbMobilisation.Visible = false;
            /*
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
            */

            //MessageBox.Show(xx);


            lblId.Text = (MesDatas.DsGlobal.Tables["Mission"].Rows.Count + 1).ToString();
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

        public void AfficherDans(Panel panel)
        {
            this.TopLevel = false;
            this.FormBorderStyle = FormBorderStyle.None;
            this.Dock = DockStyle.Fill;
            panel.Controls.Clear();
            panel.Controls.Add(this);
            this.Show();
        }

        

        private void btnQuitter_Click(object sender, EventArgs e)
        {
            //Application.Exit();
            this.Close();
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
            DataRow[] mobilisations = MesDatas.DsGlobal.Tables["Mobiliser"].Select("matriculePompier = " + matricule);

            foreach (DataRow mobilisation in mobilisations)
            {
                int idMission = Convert.ToInt32(mobilisation["idMission"]);

                DataRow[] mission = MesDatas.DsGlobal.Tables["Mission"]
                    .Select("id = " + idMission + " AND terminee = 0");

                if (mission.Length > 0)
                {
                    return true;
                }
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
            // Liste finale des engins nécessaires
            List<(string codeTypeEngin, int nombre)> enginsNecessaires = new List<(string, int)>();

            // 1. Récupération des valeurs depuis les ComboBox
            int idNatureSinistre = Convert.ToInt32(cbNatureSinistre.SelectedValue);
            int idCaserne = Convert.ToInt32(cbCaserneImmobiliser.SelectedValue);


            // 2. Recherche des engins nécessaires pour ce type de sinistre
            foreach (DataRow row in MesDatas.DsGlobal.Tables["Necessiter"].Select("idNatureSinistre = " + idNatureSinistre))
            {
                string type = row["codeTypeEngin"].ToString();
                int nb = Convert.ToInt32(row["nombre"]);

                // On prend les engins qui sont dispo dans la caserne
                DataRow[] enginsDispoDansCaserne = MesDatas.DsGlobal.Tables["Engin"].Select("codeTypeEngin = '" + type + "' AND idCaserne = " + idCaserne + " AND enMission = 0 AND enPanne = 0");
                if (enginsDispoDansCaserne.Length >= nb)
                {
                    enginsNecessaires.Add((type, nb));
                }
            }

            // 3. Affichage dans le DataGridView des engins
            dgvEngins.Rows.Clear();
            if (dgvEngins.Columns.Count == 0)
            {
                dgvEngins.Columns.Add("typeEngin", "Type d'engin");
                dgvEngins.Columns.Add("nombre", "Quantité requise");
                dgvEngins.Columns.Add("equipage", "Équipage requis");
            }

            foreach (var (type, nb) in enginsNecessaires)
            {
                // Récupérer l’équipage depuis la table TypeEngin
                int equipage = 0;
                DataRow[] typeEnginRow = MesDatas.DsGlobal.Tables["TypeEngin"]
                    .Select($"code = '{type}'");
                if (typeEnginRow.Length > 0)
                {
                    equipage = Convert.ToInt32(typeEnginRow[0]["equipage"]);
                }

                dgvEngins.Rows.Add(type, nb, equipage);
            }

            // 4. Affichage des pompiers
            dgvPompiers.Rows.Clear();
            if (dgvPompiers.Columns.Count == 0)
            {
                dgvPompiers.Columns.Add("matricule", "Matricule");
                dgvPompiers.Columns.Add("nom", "Nom");
                dgvPompiers.Columns.Add("prenom", "Prénom");
                dgvPompiers.Columns.Add("pourEngin", "Type Engin");
            }

            foreach (var (typeEngin, nombre) in enginsNecessaires)
            {
                List<int> habilitations = new List<int>();
                DataRow[] rowsEmbarquer = MesDatas.DsGlobal.Tables["Embarquer"].Select(
                    "codeTypeEngin = '" + typeEngin + "'"
                );

                foreach (DataRow row in rowsEmbarquer)
                {
                    int idHab = Convert.ToInt32(row["idHabilitation"]);
                    if (!habilitations.Contains(idHab))
                    {
                        habilitations.Add(idHab);
                    }
                }


                List<DataRow> pompiersEligibles = new List<DataRow>();

                foreach (int idHab in habilitations)
                {
                    DataRow[] rowsPasser = MesDatas.DsGlobal.Tables["Passer"]
                        .Select("idHabilitation = " + idHab);

                    foreach (DataRow passerRow in rowsPasser)
                    {
                        int matricule = Convert.ToInt32(passerRow["matriculePompier"]);
                        DataRow[] result = MesDatas.DsGlobal.Tables["Pompier"].Select("matricule = " + matricule);
                        DataRow pompier = null;

                        if (result.Length > 0)
                        {
                            pompier = result[0];
                        }

                        if (pompier != null && !estEnMission(matricule) && !estEnConge(pompier))
                        {
                            if (!pompiersEligibles.Contains(pompier))
                            {
                                pompiersEligibles.Add(pompier);
                            }
                        }
                    }
                }


                // Récupérer nombre d’équipiers requis pour cet engin
                int equipage = 0;
                DataRow[] rowType = MesDatas.DsGlobal.Tables["TypeEngin"].Select($"code = '{typeEngin}'");
                if (rowType.Length > 0)
                {
                    equipage = Convert.ToInt32(rowType[0]["equipage"]);
                }

                // nombre total de pompiers à prendre = nombre d'engins * équipage
                int totalPompiers = equipage * nombre;

                var selection = pompiersEligibles.Take(totalPompiers).ToList();

                foreach (var p in selection)
                {
                    dgvPompiers.Rows.Add(p["matricule"], p["nom"], p["prenom"], typeEngin);
                }
            }



        }

        private string Nettoyer(string input)
        {
            if (string.IsNullOrEmpty(input)) return "";

            string normalise = input.Normalize(System.Text.NormalizationForm.FormD);
            var sb = new StringBuilder();

            foreach (char c in normalise)
            {
                var uc = System.Globalization.CharUnicodeInfo.GetUnicodeCategory(c);
                if (uc != System.Globalization.UnicodeCategory.NonSpacingMark && c <= 127)
                {
                    sb.Append(c);
                }
            }

            return sb.ToString();
        }
        private void dgvEngins_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void dgvPompiers_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }


        private void btnRapport_Click(object sender, EventArgs e)
        {
            try
            {
                string nomFichier = "Rapport_Mission.pdf";
                string chemin = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), nomFichier);

                if (File.Exists(chemin))
                {
                    File.Delete(chemin);
                }

                // Création du document PDF avec iTextSharp
                Document document = new Document(PageSize.A4, 50, 50, 25, 25);
                PdfWriter.GetInstance(document, new FileStream(chemin, FileMode.Create));
                document.Open();

                // Titre
                var titreFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 18);
                var titre = new Paragraph("RAPPORT DE MISSION", titreFont);
                titre.Alignment = Element.ALIGN_CENTER;
                document.Add(titre);
                document.Add(new Paragraph("\n"));

                // Informations mission
                document.Add(new Paragraph("Numero de mission : " + Nettoyer(lblId.Text)));
                document.Add(new Paragraph("Date declenchement : " + Nettoyer(lblDateDeclanchee.Text)));
                document.Add(new Paragraph("Nature du sinistre : " + Nettoyer(cbNatureSinistre.Text)));
                document.Add(new Paragraph("Caserne : " + Nettoyer(cbCaserneImmobiliser.Text)));
                document.Add(new Paragraph("Adresse : " + Nettoyer(txtRue.Text + ", " + txtCodePostale.Text + " " + txtVille.Text)));
                document.Add(new Paragraph("Description : " + Nettoyer(txtMotif.Text)));

                document.Add(new Paragraph("\n-----------------------------\n"));

                // Engins mobilisés
                document.Add(new Paragraph("Engins mobilises :"));
                if (dgvEngins.Rows.Count == 0)
                {
                    document.Add(new Paragraph("- Aucun engin mobilise."));
                }
                else
                {
                    foreach (DataGridViewRow row in dgvEngins.Rows)
                    {
                        if (!row.IsNewRow && row.Cells[0].Value != null)
                        {
                            string type = Nettoyer(row.Cells[0].Value?.ToString() ?? "Inconnu");
                            string quantite = row.Cells.Count > 1 && row.Cells[1].Value != null ? Nettoyer(row.Cells[1].Value.ToString()) : "N/A";
                            string equipage = row.Cells.Count > 2 && row.Cells[2].Value != null ? Nettoyer(row.Cells[2].Value.ToString()) : "N/A";

                            document.Add(new Paragraph("- " + type + " : " + quantite + " engin(s), " + equipage + " pompier(s) par engin"));
                        }
                    }
                }

                // Pompiers mobilisés
                document.Add(new Paragraph("\nPompiers mobilises :"));
                if (dgvPompiers.Rows.Count == 0)
                {
                    document.Add(new Paragraph("- Aucun pompier affecte."));
                }
                else
                {
                    foreach (DataGridViewRow row in dgvPompiers.Rows)
                    {
                        if (!row.IsNewRow && row.Cells[0].Value != null)
                        {
                            string nom = row.Cells.Count > 1 ? Nettoyer(row.Cells[1].Value?.ToString() ?? "Nom") : "Nom";
                            string prenom = row.Cells.Count > 2 ? Nettoyer(row.Cells[2].Value?.ToString() ?? "Prenom") : "Prenom";
                            string engin = row.Cells.Count > 3 ? Nettoyer(row.Cells[3].Value?.ToString() ?? "?") : "?";

                            document.Add(new Paragraph("- " + prenom + " " + nom + " (engin : " + engin + ")"));
                        }
                    }
                }

                document.Add(new Paragraph("\nRapport genere le : " + DateTime.Now.ToString("dd/MM/yyyy HH:mm")));
                document.Close();

                MessageBox.Show("PDF genere avec succes sur le bureau !");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur PDF : " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.ShowDialog();
        }

        private void dgvEngins_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnMAJ_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dtMission = MesDatas.DsGlobal.Tables["Mission"];
                DataTable dtEngin = MesDatas.DsGlobal.Tables["Engin"];
                DataTable dtPompier = MesDatas.DsGlobal.Tables["Pompier"];

                DateTime date = DateTime.Now;

                // --- Création de la nouvelle ligne en mémoire
                DataRow nouvelleMission = dtMission.NewRow();
                nouvelleMission["motifAppel"] = txtMotif.Text.Trim();
                nouvelleMission["adresse"] = txtRue.Text.Trim();
                nouvelleMission["cp"] = txtCodePostale.Text.Trim();
                nouvelleMission["ville"] = txtVille.Text.Trim();
                nouvelleMission["dateHeureDepart"] = date;

                // --- Vérification ID mission
                if (string.IsNullOrWhiteSpace(lblId.Text))
                {
                    MessageBox.Show("❌ L'ID de la mission est vide.");
                    return;
                }
                string id = lblId.Text.Trim();
                nouvelleMission["id"] = id;

                // --- Vérification comboBox
                if (cbCaserneImmobiliser.SelectedValue == null || cbNatureSinistre.SelectedValue == null)
                {
                    MessageBox.Show("❌ Veuillez sélectionner une caserne et une nature de sinistre.");
                    return;
                }

                nouvelleMission["idCaserne"] = Convert.ToInt32(cbCaserneImmobiliser.SelectedValue);
                nouvelleMission["idNatureSinistre"] = Convert.ToInt32(cbNatureSinistre.SelectedValue);

                // --- Ajout dans le DataSet
                dtMission.Rows.Add(nouvelleMission);

                // --- Ajout dans la base de données
                string sql = @"INSERT INTO Mission 
        (id, motifAppel, adresse, cp, ville, dateHeureDepart, idCaserne, idNatureSinistre) 
        VALUES (@id, @motif, @adresse, @cp, @ville, @depart, @caserne, @nature)";

                using (SQLiteCommand cmd = new SQLiteCommand(sql, Connexion.Connec))
                {
                    if (Connexion.Connec.State != ConnectionState.Open)
                        Connexion.Connec.Open();

                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@motif", nouvelleMission["motifAppel"]);
                    cmd.Parameters.AddWithValue("@adresse", nouvelleMission["adresse"]);
                    cmd.Parameters.AddWithValue("@cp", nouvelleMission["cp"]);
                    cmd.Parameters.AddWithValue("@ville", nouvelleMission["ville"]);
                    cmd.Parameters.AddWithValue("@depart", date.ToString("yyyy-MM-dd HH:mm"));
                    cmd.Parameters.AddWithValue("@caserne", nouvelleMission["idCaserne"]);
                    cmd.Parameters.AddWithValue("@nature", nouvelleMission["idNatureSinistre"]);

                    cmd.ExecuteNonQuery();
                }

                // --- Mise à jour enMission pour les pompiers
                foreach (DataGridViewRow row in dgvPompiers.Rows)
                {
                    if (row.Cells["Matricule"].Value != null)
                    {
                        int matricule = Convert.ToInt32(row.Cells["Matricule"].Value);
                        DataRow[] pompierRow = dtPompier.Select($"matricule = {matricule}");
                        if (pompierRow.Length > 0)
                        {
                            pompierRow[0]["enMission"] = 1;
                        }
                    }
                }

                // --- Mise à jour enMission pour les engins
                foreach (DataGridViewRow row in dgvEngins.Rows)
                {
                    if (row.Cells["typeEngin"].Value != null)
                    {
                        string codeEngin = row.Cells["typeEngin"].Value.ToString();
                        DataRow[] enginRow = dtEngin.Select($"codeTypeEngin = '{codeEngin}'");
                        if (enginRow.Length > 0)
                        {
                            enginRow[0]["enMission"] = 1;
                        }
                    }
                }

                // --- Nettoyage du formulaire
                txtMotif.Text = "";
                txtRue.Text = "";
                txtVille.Text = "";
                txtCodePostale.Text = "";

                cbNatureSinistre.SelectedIndex = -1;
                cbCaserneImmobiliser.SelectedIndex = -1;
                dgvEngins.Rows.Clear();
                dgvPompiers.Rows.Clear();

                // --- Prochain ID
                int prochainId = 1;
                if (dtMission.Rows.Count > 0)
                {
                    var lastRow = dtMission.Rows[dtMission.Rows.Count - 1];
                    int lastId = Convert.ToInt32(lastRow["id"]);
                    prochainId = lastId + 1;
                }

                lblId.Text = prochainId.ToString();

                MesDatas.DsGlobal.AcceptChanges();

                // --- Message final
                MessageBox.Show("✅ Mission enregistrée dans la base et le DataSet !");

                // --- Rafraîchir tableau de bord
                if (tableauDeBord != null)
                {
                    tableauDeBord.btnActualiser.PerformClick();
                }
                else
                {
                    MessageBox.Show("⚠️ Le tableau de bord est introuvable.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Erreur :\n" + ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void button2_Click(object sender, EventArgs e)
        {
            Form4 f4 = new Form4();
            f4.ShowDialog();
        }
    }

}
