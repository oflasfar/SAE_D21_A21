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

namespace FormCreationMission
{
    public partial class gestionEngins: UserControl
    {
        private DataSet ds;
        private BindingSource bsEngins;
        public gestionEngins()
        {
            InitializeComponent();
            this.bsEngins = new BindingSource();
            ChargerTout();
        }

        public gestionEngins(DataSet dsp,Form4 f)
        {
            InitializeComponent();
            this.bsEngins = new BindingSource();
            this.ds = dsp;
        }



        private void gestionEngins_Load(object sender, EventArgs e)
        {
            if (ds != null && ds.Tables.Contains("Caserne"))
            {
                pbmax.Image = Properties.Resources.final_page;
                pbavant.Image = Properties.Resources.previous_page;
                pbapres.Image = Properties.Resources.next_page;
                pbmin.Image = Properties.Resources.first_page;

                cboChoix.DataSource = ds.Tables["Caserne"];
                cboChoix.DisplayMember = "nom";

                if (ds.Tables["Caserne"].Columns.Contains("id"))
                {
                    cboChoix.ValueMember = "id";

                    if (cboChoix.Items.Count > 0)
                    {
                        cboChoix.SelectedIndex = 0; // Ca déclenchera cboChoix_SelectedIndexChanged
                    }
                }
            }
            else
            {
                MessageBox.Show("Table non trouvée", "Erreur de Données", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Méthode qui établit la première connexion et récupère toutes les données que l'on viendra rechercher après (3 tables importantes) dans le DataSet
        // Voir si il faut adapter avec le fichier MesDatas
        private void ChargerTout()
        {
            string chaine = "Data Source=SDIS67.db;Version=3;";
            ds = new DataSet();

            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(chaine))
                {
                    connection.Open();

                    // Charger la table Caserne
                    using (SQLiteDataAdapter daCaserne = new SQLiteDataAdapter("SELECT id, nom FROM Caserne", connection))
                    {
                        daCaserne.Fill(ds, "Caserne");
                    }

                    // 2. Charger la table Engin 
                    using (SQLiteDataAdapter daEngin = new SQLiteDataAdapter("SELECT idCaserne, codeTypeEngin, numero, dateReception, enMission, enPanne FROM Engin", connection))
                    {
                        daEngin.Fill(ds, "Engin");
                    }

                    // 3. Charger la table TypeEngin 
                    using (SQLiteDataAdapter daTypeEngin = new SQLiteDataAdapter("SELECT code, nom FROM TypeEngin", connection))
                    {
                        daTypeEngin.Fill(ds, "TypeEngin");
                    }
                }
            }
            catch (SQLiteException err)
            {
                MessageBox.Show($"Erreur chargement de toutes les données: {err.Message}", "Erreur de Base de Données", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void cboChoix_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboChoix.SelectedItem == null)
            {
                return;
            }

            int idCaserneSelectionne;
            try
            {
                DataRowView selectedRow = cboChoix.SelectedItem as DataRowView;
                if (selectedRow != null && selectedRow.Row.Table.Columns.Contains("id"))
                {
                    idCaserneSelectionne = Convert.ToInt32(selectedRow["id"]);
                }
                else
                {
                    MessageBox.Show("Impossible de récupérer l'ID de la caserne sélectionnée. La colonne 'id' est introuvable ou l'élément sélectionné n'est pas une ligne de données valide.", "Erreur de Données", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la récupération de l'ID de la caserne: {ex.Message}", "Erreur de Conversion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DataTable dtEngins = new DataTable(); // DataTable pour les engins filtrés


            if (ds.Tables.Contains("Engin"))
            {
                // Fait une datatable seulement avec les engins de la caserne choisi
                DataRow[] filtre = ds.Tables["Engin"].Select($"idCaserne = {idCaserneSelectionne}");

                // Clone les colonnes de la table Engin pour le nouveau DataTable
                dtEngins = ds.Tables["Engin"].Clone();

                foreach (DataRow row in filtre)
                {
                    dtEngins.ImportRow(row);
                }


                if (ds.Tables.Contains("TypeEngin") && ds.Tables["TypeEngin"].Columns.Contains("nom"))
                {
                    if (!dtEngins.Columns.Contains("NomTypeEngin"))
                    {
                        dtEngins.Columns.Add("NomTypeEngin", typeof(string));
                    }

                    foreach (DataRow rowEngin in dtEngins.Rows)
                    {
                        string codeTypeEngin = rowEngin["codeTypeEngin"].ToString();
                        DataRow[] typeEnginRows = ds.Tables["TypeEngin"].Select($"code = '{codeTypeEngin}'");

                        if (typeEnginRows.Length > 0)
                        {
                            rowEngin["NomTypeEngin"] = typeEnginRows[0]["nom"];
                        }
                        else
                        {
                            rowEngin["NomTypeEngin"] = $"Type Inconnu ({codeTypeEngin})";
                        }
                    }
                }


                //creation du matricule
                if (dtEngins.Columns.Contains("idCaserne") && dtEngins.Columns.Contains("numero") && dtEngins.Columns.Contains("codeTypeEngin"))
                {

                    dtEngins.Columns.Add("matricule", typeof(string));

                    foreach (DataRow row in dtEngins.Rows)
                    {
                        string idCaserne = row.IsNull("idCaserne") ? string.Empty : row["idCaserne"].ToString();
                        string codeTypeEngin = row.IsNull("codeTypeEngin") ? string.Empty : row["codeTypeEngin"].ToString();
                        string numeroEngin = row.IsNull("numero") ? string.Empty : row["numero"].ToString();

                        row["matricule"] = $"{idCaserne}-{codeTypeEngin}-{numeroEngin}";
                    }
                }
                else
                {
                    MessageBox.Show("Les colonnes 'idCaserne', 'numero' ou 'codeTypeEngin' sont manquantes dans la table Engin du DataSet en mémoire. Impossible de construire le matricule.", "Erreur de Colonne", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                // Création d'une colonne Image avec toutes les images correspondant à la colonne code car nomImage= code + ".png" et ajouter dans la bonne ligne du DatagridView
                if (dtEngins.Columns.Contains("codeTypeEngin"))
                {
                    if (!dtEngins.Columns.Contains("ImageEngin"))
                    {
                        dtEngins.Columns.Add("ImageEngin", typeof(Image));
                    }

                    foreach (DataRow row in dtEngins.Rows)
                    {
                        string codeTypeEngin = row["codeTypeEngin"].ToString();

                        Image enginImage = (Image)Properties.Resources.ResourceManager.GetObject(codeTypeEngin);


                        // cas où l'image n'est pas trouvée pour ce codeTypeEngin
                        if (enginImage != null)
                        {
                            row["ImageEngin"] = enginImage;
                        }
                        else
                        {
                            Console.WriteLine($"Avertissement: Image non trouvée pour le code d'engin '{codeTypeEngin}'.");
                        }
                    }
                }

                // Affichage des labels pour la disponibilité du véhicule
                if (!dtEngins.Columns.Contains("Dispo"))
                {
                    dtEngins.Columns.Add("Dispo", typeof(string)); // Ajout de la colonne Dispo dans le DataTable
                }
                if (!dtEngins.Columns.Contains("MotifIndispo"))
                {
                    dtEngins.Columns.Add("MotifIndispo", typeof(string)); // Idem Ajout MotifIndispo
                }

                foreach (DataRow row in dtEngins.Rows)
                {
                    bool enMission = row.Field<long>("enMission") == 1; // si enMission vaut 1 alors true
                    bool enPanne = row.Field<long>("enPanne") == 1;

                    if (!enMission && !enPanne) // tout 2 false alors Engin disponible
                    {
                        row["Dispo"] = "Oui";
                        row["MotifIndispo"] = ""; // Pas de motif d'indisponibilité
                    }
                    else // dans le cas ou aumoins l'un des 2 est true
                    {
                        row["Dispo"] = "Non";
                        List<string> motifs = new List<string>();
                        if (enMission)
                        {
                            motifs.Add("Motif : En mission");
                        }
                        if (enPanne)
                        {
                            motifs.Add("Motif : En panne");
                        }
                        row["MotifIndispo"] = string.Join(" et ", motifs);
                    }
                }

                // Commpte le nombre d'Engins et écrit dans un label en bas de la page 1/n à chaque changement
                if (!dtEngins.Columns.Contains("Index"))
                {
                    dtEngins.Columns.Add("Index", typeof(string));
                }

                int total = dtEngins.Rows.Count;
                int i = 0;
                foreach (DataRow row in dtEngins.Rows)
                {
                    i++;
                    row["Index"] = i.ToString() + " / " + total.ToString();
                }
            }
            else
            {
                MessageBox.Show("table Engin pas chargée dans le DataSet", "Erreur Interne", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            bsEngins.DataSource = dtEngins; // Lie la DataTable filtré au BindingSource

            // Liaison des contrôles individuels au BindingSource
            txtNumero.DataBindings.Clear();
            txtReception.DataBindings.Clear();
            pbEnginImage.DataBindings.Clear(); // synchro picturebox avec bindingsource
            lblOuiNon.DataBindings.Clear(); // label Etat disponibilié
            lblMotif.DataBindings.Clear(); // label Motif Non disponibilité
            lblIndex.DataBindings.Clear(); // label index page Engin dans la table
            lblNomEngin.DataBindings.Clear(); // lbl qui affiche le nom complet de l'engin


            txtNumero.DataBindings.Add("Text", bsEngins, "matricule");
            txtReception.DataBindings.Add("Text", bsEngins, "dateReception");
            pbEnginImage.DataBindings.Add("Image", bsEngins, "ImageEngin", true, DataSourceUpdateMode.OnPropertyChanged);
            lblOuiNon.DataBindings.Add("Text", bsEngins, "Dispo");
            lblMotif.DataBindings.Add("Text", bsEngins, "MotifIndispo");
            lblIndex.DataBindings.Add("Text", bsEngins, "Index");
            lblNomEngin.DataBindings.Add("Text", bsEngins, "NomTypeEngin");

            if (dgvCaserne != null)
            {
                dgvCaserne.DataSource = bsEngins;
                dgvCaserne.Visible = false;
            }

            if (dtEngins.Rows.Count == 0)
            {
                MessageBox.Show("Aucun engin trouvé pour cette caserne.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                bsEngins.MoveFirst(); // se replace toujours au debut quand on change de "table" et donc de Caserne
            }

            // Chercher dans la table TypeEngin les noms complets des Engins pour les afficher dans le label correspondant
            if (ds.Tables.Contains("TypeEngin"))
            {

            }
        }

        private void pbmin_Click(object sender, EventArgs e)
        {
            if (bsEngins.Count > 0)
            {
                bsEngins.MoveFirst();
            }
        }

        private void pbavant_Click(object sender, EventArgs e)
        {
            if (bsEngins.Count > 0)
            {
                bsEngins.MovePrevious();
            }
        }

        private void pbapres_Click(object sender, EventArgs e)
        {
            if (bsEngins.Count > 0)
            {
                bsEngins.MoveNext();
            }
        }

        private void pbmax_Click(object sender, EventArgs e)
        {
            if (bsEngins.Count > 0)
            {
                bsEngins.MoveLast();
            }
        }

        private void cboChoix_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true; // Empêche la saisie de texte dans le ComboBox
        }

        private void txtNumero_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true; // Empêche la saisie de texte dans le TextBox
        }

        private void txtReception_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true; // Empêche la saisie de texte dans le TextBox
        }
    }
}
