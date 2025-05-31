namespace FormCreationMission
{
    partial class Statistique
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

        #region Code généré par le Concepteur de composants

        /// <summary> 
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlCaserne = new System.Windows.Forms.Panel();
            this.pnlGlobal = new System.Windows.Forms.Panel();
            this.cboHabilitation = new System.Windows.Forms.ComboBox();
            this.cboTypeSinistre = new System.Windows.Forms.ComboBox();
            this.cboStatGlobal = new System.Windows.Forms.ComboBox();
            this.cboRequeteParCaserne = new System.Windows.Forms.ComboBox();
            this.cboCaserne = new System.Windows.Forms.ComboBox();
            this.lblStatistiqueGlobal = new System.Windows.Forms.Label();
            this.lblStatCasern = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // pnlCaserne
            // 
            this.pnlCaserne.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.pnlCaserne.Location = new System.Drawing.Point(42, 227);
            this.pnlCaserne.Margin = new System.Windows.Forms.Padding(4);
            this.pnlCaserne.Name = "pnlCaserne";
            this.pnlCaserne.Size = new System.Drawing.Size(650, 395);
            this.pnlCaserne.TabIndex = 19;
            this.pnlCaserne.Visible = false;
            // 
            // pnlGlobal
            // 
            this.pnlGlobal.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.pnlGlobal.Location = new System.Drawing.Point(760, 227);
            this.pnlGlobal.Margin = new System.Windows.Forms.Padding(4);
            this.pnlGlobal.Name = "pnlGlobal";
            this.pnlGlobal.Size = new System.Drawing.Size(584, 395);
            this.pnlGlobal.TabIndex = 18;
            this.pnlGlobal.Visible = false;
            // 
            // cboHabilitation
            // 
            this.cboHabilitation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboHabilitation.FormattingEnabled = true;
            this.cboHabilitation.Location = new System.Drawing.Point(1149, 131);
            this.cboHabilitation.Margin = new System.Windows.Forms.Padding(4);
            this.cboHabilitation.Name = "cboHabilitation";
            this.cboHabilitation.Size = new System.Drawing.Size(195, 24);
            this.cboHabilitation.TabIndex = 17;
            this.cboHabilitation.Visible = false;
            this.cboHabilitation.SelectedIndexChanged += new System.EventHandler(this.cboHabilitation_SelectedIndexChanged);
            // 
            // cboTypeSinistre
            // 
            this.cboTypeSinistre.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTypeSinistre.FormattingEnabled = true;
            this.cboTypeSinistre.Location = new System.Drawing.Point(1149, 131);
            this.cboTypeSinistre.Margin = new System.Windows.Forms.Padding(4);
            this.cboTypeSinistre.Name = "cboTypeSinistre";
            this.cboTypeSinistre.Size = new System.Drawing.Size(195, 24);
            this.cboTypeSinistre.TabIndex = 16;
            this.cboTypeSinistre.Visible = false;
            this.cboTypeSinistre.SelectedIndexChanged += new System.EventHandler(this.cboTypeSinistre_SelectedIndexChanged);
            // 
            // cboStatGlobal
            // 
            this.cboStatGlobal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboStatGlobal.FormattingEnabled = true;
            this.cboStatGlobal.Location = new System.Drawing.Point(930, 131);
            this.cboStatGlobal.Margin = new System.Windows.Forms.Padding(4);
            this.cboStatGlobal.Name = "cboStatGlobal";
            this.cboStatGlobal.Size = new System.Drawing.Size(211, 24);
            this.cboStatGlobal.TabIndex = 15;
            this.cboStatGlobal.SelectedIndexChanged += new System.EventHandler(this.cboStatGlobal_SelectedIndexChanged);
            // 
            // cboRequeteParCaserne
            // 
            this.cboRequeteParCaserne.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboRequeteParCaserne.FormattingEnabled = true;
            this.cboRequeteParCaserne.Location = new System.Drawing.Point(482, 131);
            this.cboRequeteParCaserne.Margin = new System.Windows.Forms.Padding(4);
            this.cboRequeteParCaserne.Name = "cboRequeteParCaserne";
            this.cboRequeteParCaserne.Size = new System.Drawing.Size(210, 24);
            this.cboRequeteParCaserne.TabIndex = 14;
            this.cboRequeteParCaserne.SelectedIndexChanged += new System.EventHandler(this.cboRequeteParCaserne_SelectedIndexChanged);
            // 
            // cboCaserne
            // 
            this.cboCaserne.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCaserne.FormattingEnabled = true;
            this.cboCaserne.Location = new System.Drawing.Point(249, 131);
            this.cboCaserne.Margin = new System.Windows.Forms.Padding(4);
            this.cboCaserne.Name = "cboCaserne";
            this.cboCaserne.Size = new System.Drawing.Size(225, 24);
            this.cboCaserne.TabIndex = 13;
            this.cboCaserne.SelectedIndexChanged += new System.EventHandler(this.cboCaserne_SelectedIndexChanged);
            // 
            // lblStatistiqueGlobal
            // 
            this.lblStatistiqueGlobal.AutoSize = true;
            this.lblStatistiqueGlobal.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblStatistiqueGlobal.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatistiqueGlobal.Location = new System.Drawing.Point(755, 131);
            this.lblStatistiqueGlobal.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStatistiqueGlobal.Name = "lblStatistiqueGlobal";
            this.lblStatistiqueGlobal.Size = new System.Drawing.Size(167, 25);
            this.lblStatistiqueGlobal.TabIndex = 12;
            this.lblStatistiqueGlobal.Text = "Statisque global : ";
            // 
            // lblStatCasern
            // 
            this.lblStatCasern.AutoSize = true;
            this.lblStatCasern.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblStatCasern.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatCasern.Location = new System.Drawing.Point(37, 131);
            this.lblStatCasern.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStatCasern.Name = "lblStatCasern";
            this.lblStatCasern.Size = new System.Drawing.Size(204, 25);
            this.lblStatCasern.TabIndex = 11;
            this.lblStatCasern.Text = "Statistique la caserne:";
            // 
            // Statistique
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlCaserne);
            this.Controls.Add(this.pnlGlobal);
            this.Controls.Add(this.cboHabilitation);
            this.Controls.Add(this.cboTypeSinistre);
            this.Controls.Add(this.cboStatGlobal);
            this.Controls.Add(this.cboRequeteParCaserne);
            this.Controls.Add(this.cboCaserne);
            this.Controls.Add(this.lblStatistiqueGlobal);
            this.Controls.Add(this.lblStatCasern);
            this.Name = "Statistique";
            this.Size = new System.Drawing.Size(1454, 891);
            this.Load += new System.EventHandler(this.Statistique_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlCaserne;
        private System.Windows.Forms.Panel pnlGlobal;
        private System.Windows.Forms.ComboBox cboHabilitation;
        private System.Windows.Forms.ComboBox cboTypeSinistre;
        private System.Windows.Forms.ComboBox cboStatGlobal;
        private System.Windows.Forms.ComboBox cboRequeteParCaserne;
        private System.Windows.Forms.ComboBox cboCaserne;
        private System.Windows.Forms.Label lblStatistiqueGlobal;
        private System.Windows.Forms.Label lblStatCasern;
    }
}
