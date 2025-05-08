namespace FormCreationMission
{
    partial class Form1
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblIdMission = new System.Windows.Forms.Label();
            this.lblId = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblDateDeclanchee = new System.Windows.Forms.Label();
            this.gbInformationsUsager = new System.Windows.Forms.GroupBox();
            this.txtMotif = new System.Windows.Forms.TextBox();
            this.txtVille = new System.Windows.Forms.TextBox();
            this.txtCodePostale = new System.Windows.Forms.TextBox();
            this.txtRue = new System.Windows.Forms.TextBox();
            this.lblVille = new System.Windows.Forms.Label();
            this.lblRue = new System.Windows.Forms.Label();
            this.lblCodePostale = new System.Windows.Forms.Label();
            this.lblAdresseSinistre = new System.Windows.Forms.Label();
            this.lblMotif = new System.Windows.Forms.Label();
            this.gbDecisionRegulateur = new System.Windows.Forms.GroupBox();
            this.btnConstituerEquipe = new System.Windows.Forms.Button();
            this.cbCaserneImmobiliser = new System.Windows.Forms.ComboBox();
            this.btnQuitter = new System.Windows.Forms.Button();
            this.cbNatureSinistre = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.gbMobilisation = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dgvPompiers = new System.Windows.Forms.DataGridView();
            this.dgvEngins = new System.Windows.Forms.DataGridView();
            this.btnRapport = new System.Windows.Forms.Button();
            this.gbInformationsUsager.SuspendLayout();
            this.gbDecisionRegulateur.SuspendLayout();
            this.gbMobilisation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPompiers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEngins)).BeginInit();
            this.SuspendLayout();
            // 
            // lblIdMission
            // 
            this.lblIdMission.AutoSize = true;
            this.lblIdMission.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblIdMission.Location = new System.Drawing.Point(111, 27);
            this.lblIdMission.Name = "lblIdMission";
            this.lblIdMission.Size = new System.Drawing.Size(72, 16);
            this.lblIdMission.TabIndex = 0;
            this.lblIdMission.Text = "Mission n : ";
            // 
            // lblId
            // 
            this.lblId.AutoSize = true;
            this.lblId.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblId.Location = new System.Drawing.Point(193, 27);
            this.lblId.Name = "lblId";
            this.lblId.Size = new System.Drawing.Size(44, 16);
            this.lblId.TabIndex = 1;
            this.lblId.Text = "label2";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label3.Location = new System.Drawing.Point(562, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(103, 16);
            this.label3.TabIndex = 2;
            this.label3.Text = "Déclenchée le : ";
            // 
            // lblDateDeclanchee
            // 
            this.lblDateDeclanchee.AutoSize = true;
            this.lblDateDeclanchee.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblDateDeclanchee.Location = new System.Drawing.Point(671, 27);
            this.lblDateDeclanchee.Name = "lblDateDeclanchee";
            this.lblDateDeclanchee.Size = new System.Drawing.Size(44, 16);
            this.lblDateDeclanchee.TabIndex = 3;
            this.lblDateDeclanchee.Text = "label4";
            // 
            // gbInformationsUsager
            // 
            this.gbInformationsUsager.Controls.Add(this.txtMotif);
            this.gbInformationsUsager.Controls.Add(this.txtVille);
            this.gbInformationsUsager.Controls.Add(this.txtCodePostale);
            this.gbInformationsUsager.Controls.Add(this.txtRue);
            this.gbInformationsUsager.Controls.Add(this.lblVille);
            this.gbInformationsUsager.Controls.Add(this.lblRue);
            this.gbInformationsUsager.Controls.Add(this.lblCodePostale);
            this.gbInformationsUsager.Controls.Add(this.lblAdresseSinistre);
            this.gbInformationsUsager.Controls.Add(this.lblMotif);
            this.gbInformationsUsager.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.gbInformationsUsager.Location = new System.Drawing.Point(114, 57);
            this.gbInformationsUsager.Name = "gbInformationsUsager";
            this.gbInformationsUsager.Size = new System.Drawing.Size(943, 161);
            this.gbInformationsUsager.TabIndex = 4;
            this.gbInformationsUsager.TabStop = false;
            this.gbInformationsUsager.Text = "Informations Usager";
            // 
            // txtMotif
            // 
            this.txtMotif.Location = new System.Drawing.Point(65, 38);
            this.txtMotif.Multiline = true;
            this.txtMotif.Name = "txtMotif";
            this.txtMotif.Size = new System.Drawing.Size(219, 101);
            this.txtMotif.TabIndex = 14;
            // 
            // txtVille
            // 
            this.txtVille.Location = new System.Drawing.Point(451, 117);
            this.txtVille.Name = "txtVille";
            this.txtVille.Size = new System.Drawing.Size(182, 22);
            this.txtVille.TabIndex = 13;
            // 
            // txtCodePostale
            // 
            this.txtCodePostale.Location = new System.Drawing.Point(451, 77);
            this.txtCodePostale.Name = "txtCodePostale";
            this.txtCodePostale.Size = new System.Drawing.Size(182, 22);
            this.txtCodePostale.TabIndex = 12;
            // 
            // txtRue
            // 
            this.txtRue.Location = new System.Drawing.Point(451, 40);
            this.txtRue.Name = "txtRue";
            this.txtRue.Size = new System.Drawing.Size(182, 22);
            this.txtRue.TabIndex = 10;
            // 
            // lblVille
            // 
            this.lblVille.AutoSize = true;
            this.lblVille.Location = new System.Drawing.Point(350, 123);
            this.lblVille.Name = "lblVille";
            this.lblVille.Size = new System.Drawing.Size(39, 16);
            this.lblVille.TabIndex = 9;
            this.lblVille.Text = "Ville :";
            // 
            // lblRue
            // 
            this.lblRue.AutoSize = true;
            this.lblRue.Location = new System.Drawing.Point(350, 46);
            this.lblRue.Name = "lblRue";
            this.lblRue.Size = new System.Drawing.Size(38, 16);
            this.lblRue.TabIndex = 8;
            this.lblRue.Text = "Rue :";
            // 
            // lblCodePostale
            // 
            this.lblCodePostale.AutoSize = true;
            this.lblCodePostale.Location = new System.Drawing.Point(350, 83);
            this.lblCodePostale.Name = "lblCodePostale";
            this.lblCodePostale.Size = new System.Drawing.Size(95, 16);
            this.lblCodePostale.TabIndex = 7;
            this.lblCodePostale.Text = "Code Postale :";
            // 
            // lblAdresseSinistre
            // 
            this.lblAdresseSinistre.AutoSize = true;
            this.lblAdresseSinistre.Location = new System.Drawing.Point(484, 18);
            this.lblAdresseSinistre.Name = "lblAdresseSinistre";
            this.lblAdresseSinistre.Size = new System.Drawing.Size(105, 16);
            this.lblAdresseSinistre.TabIndex = 6;
            this.lblAdresseSinistre.Text = "Adresse Sinistre";
            // 
            // lblMotif
            // 
            this.lblMotif.AutoSize = true;
            this.lblMotif.Location = new System.Drawing.Point(18, 38);
            this.lblMotif.Name = "lblMotif";
            this.lblMotif.Size = new System.Drawing.Size(41, 16);
            this.lblMotif.TabIndex = 5;
            this.lblMotif.Text = "Motif :";
            // 
            // gbDecisionRegulateur
            // 
            this.gbDecisionRegulateur.Controls.Add(this.btnConstituerEquipe);
            this.gbDecisionRegulateur.Controls.Add(this.cbCaserneImmobiliser);
            this.gbDecisionRegulateur.Controls.Add(this.btnQuitter);
            this.gbDecisionRegulateur.Controls.Add(this.cbNatureSinistre);
            this.gbDecisionRegulateur.Controls.Add(this.label1);
            this.gbDecisionRegulateur.Controls.Add(this.label2);
            this.gbDecisionRegulateur.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.gbDecisionRegulateur.Location = new System.Drawing.Point(114, 235);
            this.gbDecisionRegulateur.Name = "gbDecisionRegulateur";
            this.gbDecisionRegulateur.Size = new System.Drawing.Size(943, 96);
            this.gbDecisionRegulateur.TabIndex = 15;
            this.gbDecisionRegulateur.TabStop = false;
            this.gbDecisionRegulateur.Text = "Décision du regulateur";
            // 
            // btnConstituerEquipe
            // 
            this.btnConstituerEquipe.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnConstituerEquipe.Location = new System.Drawing.Point(557, 67);
            this.btnConstituerEquipe.Name = "btnConstituerEquipe";
            this.btnConstituerEquipe.Size = new System.Drawing.Size(131, 23);
            this.btnConstituerEquipe.TabIndex = 17;
            this.btnConstituerEquipe.Text = "Constituer Equipe";
            this.btnConstituerEquipe.UseVisualStyleBackColor = true;
            this.btnConstituerEquipe.Click += new System.EventHandler(this.btnConstituerEquipe_Click);
            // 
            // cbCaserneImmobiliser
            // 
            this.cbCaserneImmobiliser.FormattingEnabled = true;
            this.cbCaserneImmobiliser.Location = new System.Drawing.Point(520, 24);
            this.cbCaserneImmobiliser.Name = "cbCaserneImmobiliser";
            this.cbCaserneImmobiliser.Size = new System.Drawing.Size(168, 24);
            this.cbCaserneImmobiliser.TabIndex = 19;
            this.cbCaserneImmobiliser.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cbCaserneImmobiliser_KeyPress);
            // 
            // btnQuitter
            // 
            this.btnQuitter.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnQuitter.Location = new System.Drawing.Point(431, 67);
            this.btnQuitter.Name = "btnQuitter";
            this.btnQuitter.Size = new System.Drawing.Size(120, 23);
            this.btnQuitter.TabIndex = 16;
            this.btnQuitter.Text = "Quitter";
            this.btnQuitter.UseVisualStyleBackColor = true;
            this.btnQuitter.Click += new System.EventHandler(this.btnQuitter_Click);
            // 
            // cbNatureSinistre
            // 
            this.cbNatureSinistre.FormattingEnabled = true;
            this.cbNatureSinistre.Location = new System.Drawing.Point(133, 27);
            this.cbNatureSinistre.Name = "cbNatureSinistre";
            this.cbNatureSinistre.Size = new System.Drawing.Size(215, 24);
            this.cbNatureSinistre.TabIndex = 18;
            this.cbNatureSinistre.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cbNatureSinistre_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(121, 16);
            this.label1.TabIndex = 16;
            this.label1.Text = "Nature du Sinistre : ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(375, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(139, 16);
            this.label2.TabIndex = 17;
            this.label2.Text = "Caserne Immobiliser : ";
            // 
            // gbMobilisation
            // 
            this.gbMobilisation.Controls.Add(this.label4);
            this.gbMobilisation.Controls.Add(this.label5);
            this.gbMobilisation.Controls.Add(this.dgvPompiers);
            this.gbMobilisation.Controls.Add(this.dgvEngins);
            this.gbMobilisation.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.gbMobilisation.Location = new System.Drawing.Point(114, 348);
            this.gbMobilisation.Name = "gbMobilisation";
            this.gbMobilisation.Size = new System.Drawing.Size(943, 321);
            this.gbMobilisation.TabIndex = 16;
            this.gbMobilisation.TabStop = false;
            this.gbMobilisation.Text = "Mobilisation des engins et des pompiers";
            this.gbMobilisation.Enter += new System.EventHandler(this.gbMobilisation_Enter);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label4.Location = new System.Drawing.Point(161, 33);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(109, 16);
            this.label4.TabIndex = 17;
            this.label4.Text = "Engins Mobilisés";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label5.Location = new System.Drawing.Point(627, 33);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(126, 16);
            this.label5.TabIndex = 18;
            this.label5.Text = "Pompiers Mobilisés";
            // 
            // dgvPompiers
            // 
            this.dgvPompiers.BackgroundColor = System.Drawing.SystemColors.ActiveBorder;
            this.dgvPompiers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPompiers.GridColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.dgvPompiers.Location = new System.Drawing.Point(431, 52);
            this.dgvPompiers.Name = "dgvPompiers";
            this.dgvPompiers.RowHeadersWidth = 51;
            this.dgvPompiers.RowTemplate.Height = 24;
            this.dgvPompiers.Size = new System.Drawing.Size(490, 185);
            this.dgvPompiers.TabIndex = 1;
            this.dgvPompiers.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dgvPompiers_KeyPress);
            // 
            // dgvEngins
            // 
            this.dgvEngins.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEngins.Location = new System.Drawing.Point(41, 52);
            this.dgvEngins.Name = "dgvEngins";
            this.dgvEngins.RowHeadersWidth = 51;
            this.dgvEngins.RowTemplate.Height = 24;
            this.dgvEngins.Size = new System.Drawing.Size(347, 185);
            this.dgvEngins.TabIndex = 0;
            this.dgvEngins.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dgvEngins_KeyPress);
            // 
            // btnRapport
            // 
            this.btnRapport.Location = new System.Drawing.Point(944, 675);
            this.btnRapport.Name = "btnRapport";
            this.btnRapport.Size = new System.Drawing.Size(113, 26);
            this.btnRapport.TabIndex = 20;
            this.btnRapport.Text = "Rapport";
            this.btnRapport.UseVisualStyleBackColor = true;
            this.btnRapport.Click += new System.EventHandler(this.btnRapport_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkRed;
            this.ClientSize = new System.Drawing.Size(1174, 738);
            this.Controls.Add(this.btnRapport);
            this.Controls.Add(this.gbMobilisation);
            this.Controls.Add(this.gbDecisionRegulateur);
            this.Controls.Add(this.gbInformationsUsager);
            this.Controls.Add(this.lblDateDeclanchee);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblId);
            this.Controls.Add(this.lblIdMission);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.gbInformationsUsager.ResumeLayout(false);
            this.gbInformationsUsager.PerformLayout();
            this.gbDecisionRegulateur.ResumeLayout(false);
            this.gbDecisionRegulateur.PerformLayout();
            this.gbMobilisation.ResumeLayout(false);
            this.gbMobilisation.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPompiers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEngins)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblIdMission;
        private System.Windows.Forms.Label lblId;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblDateDeclanchee;
        private System.Windows.Forms.GroupBox gbInformationsUsager;
        private System.Windows.Forms.TextBox txtVille;
        private System.Windows.Forms.TextBox txtCodePostale;
        private System.Windows.Forms.TextBox txtRue;
        private System.Windows.Forms.Label lblVille;
        private System.Windows.Forms.Label lblRue;
        private System.Windows.Forms.Label lblCodePostale;
        private System.Windows.Forms.Label lblAdresseSinistre;
        private System.Windows.Forms.Label lblMotif;
        private System.Windows.Forms.TextBox txtMotif;
        private System.Windows.Forms.GroupBox gbDecisionRegulateur;
        private System.Windows.Forms.Button btnConstituerEquipe;
        private System.Windows.Forms.ComboBox cbCaserneImmobiliser;
        private System.Windows.Forms.Button btnQuitter;
        private System.Windows.Forms.ComboBox cbNatureSinistre;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox gbMobilisation;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView dgvPompiers;
        private System.Windows.Forms.DataGridView dgvEngins;
        private System.Windows.Forms.Button btnRapport;
    }
}

