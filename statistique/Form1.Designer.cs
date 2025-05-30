namespace SAEpageStatistique
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
            this.lblStatCasern = new System.Windows.Forms.Label();
            this.lblStatistiqueGlobal = new System.Windows.Forms.Label();
            this.cboCaserne = new System.Windows.Forms.ComboBox();
            this.cboRequeteParCaserne = new System.Windows.Forms.ComboBox();
            this.cboStatGlobal = new System.Windows.Forms.ComboBox();
            this.cboTypeSinistre = new System.Windows.Forms.ComboBox();
            this.cboHabilitation = new System.Windows.Forms.ComboBox();
            this.pnlGlobal = new System.Windows.Forms.Panel();
            this.pnlCaserne = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // lblStatCasern
            // 
            this.lblStatCasern.AutoSize = true;
            this.lblStatCasern.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblStatCasern.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatCasern.Location = new System.Drawing.Point(265, 55);
            this.lblStatCasern.Name = "lblStatCasern";
            this.lblStatCasern.Size = new System.Drawing.Size(166, 20);
            this.lblStatCasern.TabIndex = 0;
            this.lblStatCasern.Text = "Statistique la caserne:";
            // 
            // lblStatistiqueGlobal
            // 
            this.lblStatistiqueGlobal.AutoSize = true;
            this.lblStatistiqueGlobal.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblStatistiqueGlobal.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatistiqueGlobal.Location = new System.Drawing.Point(803, 55);
            this.lblStatistiqueGlobal.Name = "lblStatistiqueGlobal";
            this.lblStatistiqueGlobal.Size = new System.Drawing.Size(135, 20);
            this.lblStatistiqueGlobal.TabIndex = 1;
            this.lblStatistiqueGlobal.Text = "Statisque global : ";
            // 
            // cboCaserne
            // 
            this.cboCaserne.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCaserne.FormattingEnabled = true;
            this.cboCaserne.Location = new System.Drawing.Point(460, 55);
            this.cboCaserne.Name = "cboCaserne";
            this.cboCaserne.Size = new System.Drawing.Size(134, 21);
            this.cboCaserne.TabIndex = 3;
            this.cboCaserne.SelectedIndexChanged += new System.EventHandler(this.cboCaserne_SelectedIndexChanged);
            // 
            // cboRequeteParCaserne
            // 
            this.cboRequeteParCaserne.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboRequeteParCaserne.FormattingEnabled = true;
            this.cboRequeteParCaserne.Location = new System.Drawing.Point(628, 54);
            this.cboRequeteParCaserne.Name = "cboRequeteParCaserne";
            this.cboRequeteParCaserne.Size = new System.Drawing.Size(129, 21);
            this.cboRequeteParCaserne.TabIndex = 5;
            this.cboRequeteParCaserne.SelectedIndexChanged += new System.EventHandler(this.cboRequeteParCaserne_SelectedIndexChanged);
            // 
            // cboStatGlobal
            // 
            this.cboStatGlobal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboStatGlobal.FormattingEnabled = true;
            this.cboStatGlobal.Location = new System.Drawing.Point(973, 57);
            this.cboStatGlobal.Name = "cboStatGlobal";
            this.cboStatGlobal.Size = new System.Drawing.Size(121, 21);
            this.cboStatGlobal.TabIndex = 6;
            this.cboStatGlobal.SelectedIndexChanged += new System.EventHandler(this.cboStatGlobal_SelectedIndexChanged);
            // 
            // cboTypeSinistre
            // 
            this.cboTypeSinistre.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTypeSinistre.FormattingEnabled = true;
            this.cboTypeSinistre.Location = new System.Drawing.Point(1125, 57);
            this.cboTypeSinistre.Name = "cboTypeSinistre";
            this.cboTypeSinistre.Size = new System.Drawing.Size(121, 21);
            this.cboTypeSinistre.TabIndex = 7;
            this.cboTypeSinistre.Visible = false;
            this.cboTypeSinistre.SelectedIndexChanged += new System.EventHandler(this.cboTypeSinistre_SelectedIndexChanged);
            // 
            // cboHabilitation
            // 
            this.cboHabilitation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboHabilitation.FormattingEnabled = true;
            this.cboHabilitation.Location = new System.Drawing.Point(1125, 57);
            this.cboHabilitation.Name = "cboHabilitation";
            this.cboHabilitation.Size = new System.Drawing.Size(121, 21);
            this.cboHabilitation.TabIndex = 8;
            this.cboHabilitation.Visible = false;
            this.cboHabilitation.SelectedIndexChanged += new System.EventHandler(this.cboHabilitation_SelectedIndexChanged);
            // 
            // pnlGlobal
            // 
            this.pnlGlobal.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.pnlGlobal.Location = new System.Drawing.Point(807, 112);
            this.pnlGlobal.Name = "pnlGlobal";
            this.pnlGlobal.Size = new System.Drawing.Size(439, 267);
            this.pnlGlobal.TabIndex = 9;
            this.pnlGlobal.Visible = false;
            // 
            // pnlCaserne
            // 
            this.pnlCaserne.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.pnlCaserne.Location = new System.Drawing.Point(269, 112);
            this.pnlCaserne.Name = "pnlCaserne";
            this.pnlCaserne.Size = new System.Drawing.Size(439, 267);
            this.pnlCaserne.TabIndex = 10;
            this.pnlCaserne.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(1258, 450);
            this.Controls.Add(this.pnlCaserne);
            this.Controls.Add(this.pnlGlobal);
            this.Controls.Add(this.cboHabilitation);
            this.Controls.Add(this.cboTypeSinistre);
            this.Controls.Add(this.cboStatGlobal);
            this.Controls.Add(this.cboRequeteParCaserne);
            this.Controls.Add(this.cboCaserne);
            this.Controls.Add(this.lblStatistiqueGlobal);
            this.Controls.Add(this.lblStatCasern);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblStatCasern;
        private System.Windows.Forms.Label lblStatistiqueGlobal;
        private System.Windows.Forms.ComboBox cboCaserne;
        private System.Windows.Forms.ComboBox cboRequeteParCaserne;
        private System.Windows.Forms.ComboBox cboStatGlobal;
        private System.Windows.Forms.ComboBox cboTypeSinistre;
        private System.Windows.Forms.ComboBox cboHabilitation;
        private System.Windows.Forms.Panel pnlGlobal;
        private System.Windows.Forms.Panel pnlCaserne;
    }
}

