namespace UCRecapitulMission
{
    partial class UCAffichageMission
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCAffichageMission));
            this.btnRapport = new System.Windows.Forms.Button();
            this.btnCloturer = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblDescription = new System.Windows.Forms.Label();
            this.lblTypeMission = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblCaserne = new System.Windows.Forms.Label();
            this.lblDate = new System.Windows.Forms.Label();
            this.lblDateDebut = new System.Windows.Forms.Label();
            this.lblIDMission = new System.Windows.Forms.Label();
            this.lblId = new System.Windows.Forms.Label();
            this.PBalarm = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PBalarm)).BeginInit();
            this.SuspendLayout();
            // 
            // btnRapport
            // 
            this.btnRapport.Image = ((System.Drawing.Image)(resources.GetObject("btnRapport.Image")));
            this.btnRapport.Location = new System.Drawing.Point(697, 74);
            this.btnRapport.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnRapport.Name = "btnRapport";
            this.btnRapport.Size = new System.Drawing.Size(53, 71);
            this.btnRapport.TabIndex = 8;
            this.btnRapport.UseVisualStyleBackColor = true;
            this.btnRapport.Click += new System.EventHandler(this.btnRapport_Click);
            // 
            // btnCloturer
            // 
            this.btnCloturer.Image = ((System.Drawing.Image)(resources.GetObject("btnCloturer.Image")));
            this.btnCloturer.Location = new System.Drawing.Point(697, 2);
            this.btnCloturer.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCloturer.Name = "btnCloturer";
            this.btnCloturer.Size = new System.Drawing.Size(53, 66);
            this.btnCloturer.TabIndex = 7;
            this.btnCloturer.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnCloturer.UseVisualStyleBackColor = false;
            this.btnCloturer.Click += new System.EventHandler(this.btnCloturer_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.lblDescription);
            this.panel1.Controls.Add(this.lblTypeMission);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.lblCaserne);
            this.panel1.Controls.Add(this.lblDate);
            this.panel1.Controls.Add(this.lblDateDebut);
            this.panel1.Controls.Add(this.lblIDMission);
            this.panel1.Controls.Add(this.lblId);
            this.panel1.Controls.Add(this.PBalarm);
            this.panel1.Location = new System.Drawing.Point(3, 2);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(687, 144);
            this.panel1.TabIndex = 6;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(139, 86);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(39, 16);
            this.lblDescription.TabIndex = 8;
            this.lblDescription.Text = "Type";
            // 
            // lblTypeMission
            // 
            this.lblTypeMission.AutoSize = true;
            this.lblTypeMission.Location = new System.Drawing.Point(429, 86);
            this.lblTypeMission.Name = "lblTypeMission";
            this.lblTypeMission.Size = new System.Drawing.Size(53, 16);
            this.lblTypeMission.TabIndex = 7;
            this.lblTypeMission.Text = "Mission";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(583, 26);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(0, 16);
            this.label6.TabIndex = 6;
            // 
            // lblCaserne
            // 
            this.lblCaserne.AutoSize = true;
            this.lblCaserne.Location = new System.Drawing.Point(497, 26);
            this.lblCaserne.Name = "lblCaserne";
            this.lblCaserne.Size = new System.Drawing.Size(67, 16);
            this.lblCaserne.TabIndex = 5;
            this.lblCaserne.Text = "Caserne : ";
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Location = new System.Drawing.Point(412, 26);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(0, 16);
            this.lblDate.TabIndex = 4;
            // 
            // lblDateDebut
            // 
            this.lblDateDebut.AutoSize = true;
            this.lblDateDebut.Location = new System.Drawing.Point(278, 26);
            this.lblDateDebut.Name = "lblDateDebut";
            this.lblDateDebut.Size = new System.Drawing.Size(63, 16);
            this.lblDateDebut.TabIndex = 3;
            this.lblDateDebut.Text = "Debut le :";
            // 
            // lblIDMission
            // 
            this.lblIDMission.AutoSize = true;
            this.lblIDMission.Location = new System.Drawing.Point(244, 26);
            this.lblIDMission.Name = "lblIDMission";
            this.lblIDMission.Size = new System.Drawing.Size(0, 16);
            this.lblIDMission.TabIndex = 2;
            // 
            // lblId
            // 
            this.lblId.AutoSize = true;
            this.lblId.Location = new System.Drawing.Point(139, 26);
            this.lblId.Name = "lblId";
            this.lblId.Size = new System.Drawing.Size(78, 16);
            this.lblId.TabIndex = 1;
            this.lblId.Text = "ID Mission : ";
            // 
            // PBalarm
            // 
            this.PBalarm.Image = ((System.Drawing.Image)(resources.GetObject("PBalarm.Image")));
            this.PBalarm.Location = new System.Drawing.Point(21, 22);
            this.PBalarm.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.PBalarm.Name = "PBalarm";
            this.PBalarm.Size = new System.Drawing.Size(91, 96);
            this.PBalarm.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.PBalarm.TabIndex = 0;
            this.PBalarm.TabStop = false;
            // 
            // UCAffichageMission
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnRapport);
            this.Controls.Add(this.btnCloturer);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "UCAffichageMission";
            this.Size = new System.Drawing.Size(753, 151);
            this.Load += new System.EventHandler(this.UserControl1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PBalarm)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnRapport;
        private System.Windows.Forms.Button btnCloturer;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label lblTypeMission;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblCaserne;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Label lblDateDebut;
        private System.Windows.Forms.Label lblIDMission;
        private System.Windows.Forms.Label lblId;
        private System.Windows.Forms.PictureBox PBalarm;
    }
}
