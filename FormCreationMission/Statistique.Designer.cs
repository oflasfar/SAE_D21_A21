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
            this.gbDonnes = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.gbDonnes.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCaserne
            // 
            this.pnlCaserne.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.pnlCaserne.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCaserne.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlCaserne.Location = new System.Drawing.Point(45, 284);
            this.pnlCaserne.Margin = new System.Windows.Forms.Padding(4);
            this.pnlCaserne.Name = "pnlCaserne";
            this.pnlCaserne.Size = new System.Drawing.Size(602, 395);
            this.pnlCaserne.TabIndex = 19;
            this.pnlCaserne.Visible = false;
            // 
            // pnlGlobal
            // 
            this.pnlGlobal.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.pnlGlobal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlGlobal.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlGlobal.Location = new System.Drawing.Point(707, 284);
            this.pnlGlobal.Margin = new System.Windows.Forms.Padding(4);
            this.pnlGlobal.Name = "pnlGlobal";
            this.pnlGlobal.Size = new System.Drawing.Size(692, 395);
            this.pnlGlobal.TabIndex = 18;
            this.pnlGlobal.Visible = false;
            // 
            // cboHabilitation
            // 
            this.cboHabilitation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboHabilitation.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboHabilitation.FormattingEnabled = true;
            this.cboHabilitation.Location = new System.Drawing.Point(888, 90);
            this.cboHabilitation.Margin = new System.Windows.Forms.Padding(4);
            this.cboHabilitation.Name = "cboHabilitation";
            this.cboHabilitation.Size = new System.Drawing.Size(431, 37);
            this.cboHabilitation.TabIndex = 17;
            this.cboHabilitation.Visible = false;
            this.cboHabilitation.SelectedIndexChanged += new System.EventHandler(this.cboHabilitation_SelectedIndexChanged);
            // 
            // cboTypeSinistre
            // 
            this.cboTypeSinistre.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTypeSinistre.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboTypeSinistre.FormattingEnabled = true;
            this.cboTypeSinistre.Location = new System.Drawing.Point(888, 89);
            this.cboTypeSinistre.Margin = new System.Windows.Forms.Padding(4);
            this.cboTypeSinistre.Name = "cboTypeSinistre";
            this.cboTypeSinistre.Size = new System.Drawing.Size(431, 37);
            this.cboTypeSinistre.TabIndex = 16;
            this.cboTypeSinistre.Visible = false;
            this.cboTypeSinistre.SelectedIndexChanged += new System.EventHandler(this.cboTypeSinistre_SelectedIndexChanged);
            // 
            // cboStatGlobal
            // 
            this.cboStatGlobal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboStatGlobal.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboStatGlobal.FormattingEnabled = true;
            this.cboStatGlobal.Location = new System.Drawing.Point(395, 88);
            this.cboStatGlobal.Margin = new System.Windows.Forms.Padding(4);
            this.cboStatGlobal.Name = "cboStatGlobal";
            this.cboStatGlobal.Size = new System.Drawing.Size(462, 37);
            this.cboStatGlobal.TabIndex = 15;
            this.cboStatGlobal.SelectedIndexChanged += new System.EventHandler(this.cboStatGlobal_SelectedIndexChanged);
            // 
            // cboRequeteParCaserne
            // 
            this.cboRequeteParCaserne.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboRequeteParCaserne.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboRequeteParCaserne.FormattingEnabled = true;
            this.cboRequeteParCaserne.Location = new System.Drawing.Point(888, 22);
            this.cboRequeteParCaserne.Margin = new System.Windows.Forms.Padding(4);
            this.cboRequeteParCaserne.Name = "cboRequeteParCaserne";
            this.cboRequeteParCaserne.Size = new System.Drawing.Size(431, 37);
            this.cboRequeteParCaserne.TabIndex = 14;
            this.cboRequeteParCaserne.SelectedIndexChanged += new System.EventHandler(this.cboRequeteParCaserne_SelectedIndexChanged);
            // 
            // cboCaserne
            // 
            this.cboCaserne.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCaserne.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboCaserne.FormattingEnabled = true;
            this.cboCaserne.Location = new System.Drawing.Point(395, 22);
            this.cboCaserne.Margin = new System.Windows.Forms.Padding(4);
            this.cboCaserne.Name = "cboCaserne";
            this.cboCaserne.Size = new System.Drawing.Size(462, 37);
            this.cboCaserne.TabIndex = 13;
            this.cboCaserne.SelectedIndexChanged += new System.EventHandler(this.cboCaserne_SelectedIndexChanged);
            // 
            // lblStatistiqueGlobal
            // 
            this.lblStatistiqueGlobal.AutoSize = true;
            this.lblStatistiqueGlobal.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblStatistiqueGlobal.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatistiqueGlobal.Location = new System.Drawing.Point(112, 90);
            this.lblStatistiqueGlobal.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStatistiqueGlobal.Name = "lblStatistiqueGlobal";
            this.lblStatistiqueGlobal.Size = new System.Drawing.Size(204, 29);
            this.lblStatistiqueGlobal.TabIndex = 12;
            this.lblStatistiqueGlobal.Text = "Statisque global : ";
            // 
            // lblStatCasern
            // 
            this.lblStatCasern.AutoSize = true;
            this.lblStatCasern.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblStatCasern.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatCasern.Location = new System.Drawing.Point(112, 22);
            this.lblStatCasern.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStatCasern.Name = "lblStatCasern";
            this.lblStatCasern.Size = new System.Drawing.Size(248, 29);
            this.lblStatCasern.TabIndex = 11;
            this.lblStatCasern.Text = "Statistique la caserne:";
            this.lblStatCasern.Click += new System.EventHandler(this.lblStatCasern_Click);
            // 
            // gbDonnes
            // 
            this.gbDonnes.BackColor = System.Drawing.Color.Navy;
            this.gbDonnes.Controls.Add(this.cboRequeteParCaserne);
            this.gbDonnes.Controls.Add(this.lblStatCasern);
            this.gbDonnes.Controls.Add(this.lblStatistiqueGlobal);
            this.gbDonnes.Controls.Add(this.cboHabilitation);
            this.gbDonnes.Controls.Add(this.cboCaserne);
            this.gbDonnes.Controls.Add(this.cboTypeSinistre);
            this.gbDonnes.Controls.Add(this.cboStatGlobal);
            this.gbDonnes.Location = new System.Drawing.Point(42, 19);
            this.gbDonnes.Name = "gbDonnes";
            this.gbDonnes.Size = new System.Drawing.Size(1357, 189);
            this.gbDonnes.TabIndex = 0;
            this.gbDonnes.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(667, 284);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(21, 32);
            this.label1.TabIndex = 0;
            this.label1.Text = "|";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(667, 638);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(21, 32);
            this.label2.TabIndex = 20;
            this.label2.Text = "|";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(667, 606);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(21, 32);
            this.label3.TabIndex = 21;
            this.label3.Text = "|";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(667, 574);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(21, 32);
            this.label4.TabIndex = 22;
            this.label4.Text = "|";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(667, 542);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(21, 32);
            this.label5.TabIndex = 23;
            this.label5.Text = "|";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(667, 510);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(21, 32);
            this.label6.TabIndex = 24;
            this.label6.Text = "|";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(667, 476);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(21, 32);
            this.label7.TabIndex = 25;
            this.label7.Text = "|";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(667, 444);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(21, 32);
            this.label8.TabIndex = 26;
            this.label8.Text = "|";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(667, 412);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(21, 32);
            this.label9.TabIndex = 27;
            this.label9.Text = "|";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(667, 380);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(21, 32);
            this.label10.TabIndex = 28;
            this.label10.Text = "|";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(667, 348);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(21, 32);
            this.label11.TabIndex = 29;
            this.label11.Text = "|";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(667, 316);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(21, 32);
            this.label12.TabIndex = 30;
            this.label12.Text = "|";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(205, 227);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(263, 32);
            this.label13.TabIndex = 31;
            this.label13.Text = "Statistique Caserne";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(903, 227);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(240, 32);
            this.label14.TabIndex = 32;
            this.label14.Text = "Statistique Global";
            // 
            // Statistique
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.gbDonnes);
            this.Controls.Add(this.pnlCaserne);
            this.Controls.Add(this.pnlGlobal);
            this.Name = "Statistique";
            this.Size = new System.Drawing.Size(1454, 891);
            this.Load += new System.EventHandler(this.Statistique_Load);
            this.gbDonnes.ResumeLayout(false);
            this.gbDonnes.PerformLayout();
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
        private System.Windows.Forms.GroupBox gbDonnes;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
    }
}
