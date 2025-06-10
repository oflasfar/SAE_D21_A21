namespace FormCreationMission
{
    partial class Form4
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form4));
            this.lblTableauBord = new System.Windows.Forms.Label();
            this.chkbxEnCours = new System.Windows.Forms.CheckBox();
            this.btnActualiser = new System.Windows.Forms.Button();
            this.pnlAffichage = new System.Windows.Forms.Panel();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // lblTableauBord
            // 
            this.lblTableauBord.AutoSize = true;
            this.lblTableauBord.BackColor = System.Drawing.Color.White;
            this.lblTableauBord.Font = new System.Drawing.Font("MS UI Gothic", 28.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTableauBord.Location = new System.Drawing.Point(1075, 15);
            this.lblTableauBord.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTableauBord.Name = "lblTableauBord";
            this.lblTableauBord.Size = new System.Drawing.Size(334, 47);
            this.lblTableauBord.TabIndex = 2;
            this.lblTableauBord.Text = "Tableau de bord";
            // 
            // chkbxEnCours
            // 
            this.chkbxEnCours.AutoSize = true;
            this.chkbxEnCours.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkbxEnCours.Location = new System.Drawing.Point(599, 20);
            this.chkbxEnCours.Margin = new System.Windows.Forms.Padding(4);
            this.chkbxEnCours.Name = "chkbxEnCours";
            this.chkbxEnCours.Size = new System.Drawing.Size(103, 26);
            this.chkbxEnCours.TabIndex = 3;
            this.chkbxEnCours.Text = "En cours";
            this.chkbxEnCours.UseVisualStyleBackColor = true;
            this.chkbxEnCours.CheckedChanged += new System.EventHandler(this.chkbxEnCours_CheckedChanged_1);
            // 
            // btnActualiser
            // 
            this.btnActualiser.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnActualiser.Location = new System.Drawing.Point(759, 15);
            this.btnActualiser.Name = "btnActualiser";
            this.btnActualiser.Size = new System.Drawing.Size(241, 28);
            this.btnActualiser.TabIndex = 0;
            this.btnActualiser.Text = "Actualiser";
            this.btnActualiser.UseVisualStyleBackColor = true;
            this.btnActualiser.Visible = false;
            this.btnActualiser.Click += new System.EventHandler(this.btnActualiser_Click);
            // 
            // pnlAffichage
            // 
            this.pnlAffichage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlAffichage.Location = new System.Drawing.Point(458, 100);
            this.pnlAffichage.Name = "pnlAffichage";
            this.pnlAffichage.Size = new System.Drawing.Size(1454, 852);
            this.pnlAffichage.TabIndex = 4;
            // 
            // Form4
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1924, 1055);
            this.Controls.Add(this.pnlAffichage);
            this.Controls.Add(this.btnActualiser);
            this.Controls.Add(this.chkbxEnCours);
            this.Controls.Add(this.lblTableauBord);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form4";
            this.Text = "CaserNet";
            this.Load += new System.EventHandler(this.Form4_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.Windows.Forms.Button btnActualiser;
        private System.Windows.Forms.Panel pnlAffichage;
        public System.Windows.Forms.Label lblTableauBord;
        public System.Windows.Forms.CheckBox chkbxEnCours;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}