namespace FormCreationMission
{
    partial class Form2
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnQuitter = new System.Windows.Forms.Button();
            this.cbPompiers = new System.Windows.Forms.ComboBox();
            this.cbCaserne = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblEmbauche = new System.Windows.Forms.Label();
            this.lblDateNaissance = new System.Windows.Forms.Label();
            this.rdbVolontaire = new System.Windows.Forms.RadioButton();
            this.rdbProfessionnel = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtGrade = new System.Windows.Forms.TextBox();
            this.btnChanger = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.lblPortable = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.lblBip = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblSexe = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblNom = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblPrenom = new System.Windows.Forms.Label();
            this.lblMatricule = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.pbGrade = new System.Windows.Forms.PictureBox();
            this.cbGradeNouveau = new System.Windows.Forms.ComboBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbGrade)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.DarkRed;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btnQuitter);
            this.panel1.Controls.Add(this.cbPompiers);
            this.panel1.Controls.Add(this.cbCaserne);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Location = new System.Drawing.Point(30, 26);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(343, 671);
            this.panel1.TabIndex = 0;
            // 
            // btnQuitter
            // 
            this.btnQuitter.Location = new System.Drawing.Point(57, 568);
            this.btnQuitter.Name = "btnQuitter";
            this.btnQuitter.Size = new System.Drawing.Size(75, 23);
            this.btnQuitter.TabIndex = 7;
            this.btnQuitter.Text = "Quitter";
            this.btnQuitter.UseVisualStyleBackColor = true;
            this.btnQuitter.Click += new System.EventHandler(this.btnQuitter_Click);
            // 
            // cbPompiers
            // 
            this.cbPompiers.FormattingEnabled = true;
            this.cbPompiers.Location = new System.Drawing.Point(57, 326);
            this.cbPompiers.Name = "cbPompiers";
            this.cbPompiers.Size = new System.Drawing.Size(203, 24);
            this.cbPompiers.TabIndex = 6;
            this.cbPompiers.SelectedIndexChanged += new System.EventHandler(this.cbPompiers_SelectedIndexChanged);
            this.cbPompiers.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cbPompiers_KeyPress);
            // 
            // cbCaserne
            // 
            this.cbCaserne.FormattingEnabled = true;
            this.cbCaserne.Location = new System.Drawing.Point(57, 216);
            this.cbCaserne.Name = "cbCaserne";
            this.cbCaserne.Size = new System.Drawing.Size(203, 24);
            this.cbCaserne.TabIndex = 5;
            this.cbCaserne.SelectedIndexChanged += new System.EventHandler(this.cbCaserne_SelectedIndexChanged_1);
            this.cbCaserne.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cbCaserne_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(54, 284);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(206, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "Veuiilez selectionner un pompier :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(54, 175);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(213, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "Veuillez selectionner une caserne :";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(120, 31);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(83, 79);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lblEmbauche);
            this.panel2.Controls.Add(this.lblDateNaissance);
            this.panel2.Controls.Add(this.rdbVolontaire);
            this.panel2.Controls.Add(this.rdbProfessionnel);
            this.panel2.Controls.Add(this.groupBox1);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.lblSexe);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.lblNom);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.lblPrenom);
            this.panel2.Controls.Add(this.lblMatricule);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.pbGrade);
            this.panel2.Location = new System.Drawing.Point(415, 26);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(747, 670);
            this.panel2.TabIndex = 1;
            // 
            // lblEmbauche
            // 
            this.lblEmbauche.AutoSize = true;
            this.lblEmbauche.Location = new System.Drawing.Point(207, 327);
            this.lblEmbauche.Name = "lblEmbauche";
            this.lblEmbauche.Size = new System.Drawing.Size(51, 16);
            this.lblEmbauche.TabIndex = 22;
            this.lblEmbauche.Text = "label10";
            // 
            // lblDateNaissance
            // 
            this.lblDateNaissance.AutoSize = true;
            this.lblDateNaissance.Location = new System.Drawing.Point(207, 272);
            this.lblDateNaissance.Name = "lblDateNaissance";
            this.lblDateNaissance.Size = new System.Drawing.Size(51, 16);
            this.lblDateNaissance.TabIndex = 21;
            this.lblDateNaissance.Text = "label10";
            // 
            // rdbVolontaire
            // 
            this.rdbVolontaire.AutoSize = true;
            this.rdbVolontaire.Location = new System.Drawing.Point(184, 217);
            this.rdbVolontaire.Name = "rdbVolontaire";
            this.rdbVolontaire.Size = new System.Drawing.Size(89, 20);
            this.rdbVolontaire.TabIndex = 20;
            this.rdbVolontaire.TabStop = true;
            this.rdbVolontaire.Text = "Volontaire";
            this.rdbVolontaire.UseVisualStyleBackColor = true;
            // 
            // rdbProfessionnel
            // 
            this.rdbProfessionnel.AutoSize = true;
            this.rdbProfessionnel.Location = new System.Drawing.Point(55, 217);
            this.rdbProfessionnel.Name = "rdbProfessionnel";
            this.rdbProfessionnel.Size = new System.Drawing.Size(110, 20);
            this.rdbProfessionnel.TabIndex = 19;
            this.rdbProfessionnel.TabStop = true;
            this.rdbProfessionnel.Text = "Professionnel";
            this.rdbProfessionnel.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbGradeNouveau);
            this.groupBox1.Controls.Add(this.txtGrade);
            this.groupBox1.Controls.Add(this.btnChanger);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.lblPortable);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.lblBip);
            this.groupBox1.Location = new System.Drawing.Point(60, 379);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(593, 98);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Carriére";
            // 
            // txtGrade
            // 
            this.txtGrade.Location = new System.Drawing.Point(76, 35);
            this.txtGrade.Name = "txtGrade";
            this.txtGrade.Size = new System.Drawing.Size(154, 22);
            this.txtGrade.TabIndex = 22;
            this.txtGrade.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtGrade_KeyPress);
            // 
            // btnChanger
            // 
            this.btnChanger.Location = new System.Drawing.Point(453, 32);
            this.btnChanger.Name = "btnChanger";
            this.btnChanger.Size = new System.Drawing.Size(75, 23);
            this.btnChanger.TabIndex = 21;
            this.btnChanger.Text = "Changer";
            this.btnChanger.UseVisualStyleBackColor = true;
            this.btnChanger.Click += new System.EventHandler(this.btnChanger_Click);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(19, 35);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(51, 16);
            this.label14.TabIndex = 20;
            this.label14.Text = "Grade :";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(19, 69);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(79, 16);
            this.label13.TabIndex = 19;
            this.label13.Text = "Téléphone :";
            // 
            // lblPortable
            // 
            this.lblPortable.AutoSize = true;
            this.lblPortable.Location = new System.Drawing.Point(111, 69);
            this.lblPortable.Name = "lblPortable";
            this.lblPortable.Size = new System.Drawing.Size(51, 16);
            this.lblPortable.TabIndex = 18;
            this.lblPortable.Text = "label10";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(405, 69);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(33, 16);
            this.label11.TabIndex = 17;
            this.label11.Text = "Bip :";
            // 
            // lblBip
            // 
            this.lblBip.AutoSize = true;
            this.lblBip.Location = new System.Drawing.Point(477, 69);
            this.lblBip.Name = "lblBip";
            this.lblBip.Size = new System.Drawing.Size(51, 16);
            this.lblBip.TabIndex = 16;
            this.lblBip.Text = "label10";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(57, 327);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(120, 16);
            this.label7.TabIndex = 17;
            this.label7.Text = "Date d\'embauche :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(55, 272);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(129, 16);
            this.label5.TabIndex = 16;
            this.label5.Text = "Date de Naissance :";
            // 
            // lblSexe
            // 
            this.lblSexe.AutoSize = true;
            this.lblSexe.Location = new System.Drawing.Point(142, 176);
            this.lblSexe.Name = "lblSexe";
            this.lblSexe.Size = new System.Drawing.Size(51, 16);
            this.lblSexe.TabIndex = 15;
            this.lblSexe.Text = "label10";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(55, 176);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(44, 16);
            this.label9.TabIndex = 14;
            this.label9.Text = "Sexe :";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(57, 95);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(42, 16);
            this.label8.TabIndex = 13;
            this.label8.Text = "Nom :";
            // 
            // lblNom
            // 
            this.lblNom.AutoSize = true;
            this.lblNom.Location = new System.Drawing.Point(142, 95);
            this.lblNom.Name = "lblNom";
            this.lblNom.Size = new System.Drawing.Size(44, 16);
            this.lblNom.TabIndex = 12;
            this.lblNom.Text = "label7";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(57, 135);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(60, 16);
            this.label6.TabIndex = 11;
            this.label6.Text = "Prénom :";
            // 
            // lblPrenom
            // 
            this.lblPrenom.AutoSize = true;
            this.lblPrenom.Location = new System.Drawing.Point(142, 135);
            this.lblPrenom.Name = "lblPrenom";
            this.lblPrenom.Size = new System.Drawing.Size(44, 16);
            this.lblPrenom.TabIndex = 10;
            this.lblPrenom.Text = "label5";
            // 
            // lblMatricule
            // 
            this.lblMatricule.AutoSize = true;
            this.lblMatricule.Location = new System.Drawing.Point(283, 32);
            this.lblMatricule.Name = "lblMatricule";
            this.lblMatricule.Size = new System.Drawing.Size(44, 16);
            this.lblMatricule.TabIndex = 9;
            this.lblMatricule.Text = "label4";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(207, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 16);
            this.label3.TabIndex = 8;
            this.label3.Text = "Matricule : ";
            // 
            // pbGrade
            // 
            this.pbGrade.Location = new System.Drawing.Point(557, 85);
            this.pbGrade.Name = "pbGrade";
            this.pbGrade.Size = new System.Drawing.Size(96, 95);
            this.pbGrade.TabIndex = 7;
            this.pbGrade.TabStop = false;
            // 
            // cbGradeNouveau
            // 
            this.cbGradeNouveau.FormattingEnabled = true;
            this.cbGradeNouveau.Location = new System.Drawing.Point(236, 35);
            this.cbGradeNouveau.Name = "cbGradeNouveau";
            this.cbGradeNouveau.Size = new System.Drawing.Size(150, 24);
            this.cbGradeNouveau.TabIndex = 23;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1237, 747);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "Form2";
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbGrade)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cbPompiers;
        private System.Windows.Forms.ComboBox cbCaserne;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblSexe;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblNom;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblPrenom;
        private System.Windows.Forms.Label lblMatricule;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pbGrade;
        private System.Windows.Forms.Label lblDateNaissance;
        private System.Windows.Forms.RadioButton rdbVolontaire;
        private System.Windows.Forms.RadioButton rdbProfessionnel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtGrade;
        private System.Windows.Forms.Button btnChanger;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label lblPortable;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lblBip;
        private System.Windows.Forms.Button btnQuitter;
        private System.Windows.Forms.Label lblEmbauche;
        private System.Windows.Forms.ComboBox cbGradeNouveau;
    }
}