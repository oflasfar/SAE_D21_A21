namespace FormCreationMission
{
    partial class Form3
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
            this.lblMatricule = new System.Windows.Forms.Label();
            this.lblNom = new System.Windows.Forms.Label();
            this.lblPrenom = new System.Windows.Forms.Label();
            this.lblSexe = new System.Windows.Forms.Label();
            this.lblType = new System.Windows.Forms.Label();
            this.lblDateNaissance = new System.Windows.Forms.Label();
            this.lblPortable = new System.Windows.Forms.Label();
            this.lblBip = new System.Windows.Forms.Label();
            this.lblGrade = new System.Windows.Forms.Label();
            this.lblDateEmbauche = new System.Windows.Forms.Label();
            this.txtMatricule = new System.Windows.Forms.TextBox();
            this.txtPrenom = new System.Windows.Forms.TextBox();
            this.txtNom = new System.Windows.Forms.TextBox();
            this.txtBip = new System.Windows.Forms.TextBox();
            this.txtPortable = new System.Windows.Forms.TextBox();
            this.rbM = new System.Windows.Forms.CheckBox();
            this.rbF = new System.Windows.Forms.CheckBox();
            this.cbGrade = new System.Windows.Forms.ComboBox();
            this.rbVo = new System.Windows.Forms.CheckBox();
            this.rbPro = new System.Windows.Forms.CheckBox();
            this.dtpEmbauche = new System.Windows.Forms.DateTimePicker();
            this.dtpNaissance = new System.Windows.Forms.DateTimePicker();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cbCaserne = new System.Windows.Forms.ComboBox();
            this.btnQuitter = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblMatricule
            // 
            this.lblMatricule.AutoSize = true;
            this.lblMatricule.Location = new System.Drawing.Point(111, 39);
            this.lblMatricule.Name = "lblMatricule";
            this.lblMatricule.Size = new System.Drawing.Size(67, 16);
            this.lblMatricule.TabIndex = 0;
            this.lblMatricule.Text = "Matricule :";
            // 
            // lblNom
            // 
            this.lblNom.AutoSize = true;
            this.lblNom.Location = new System.Drawing.Point(111, 78);
            this.lblNom.Name = "lblNom";
            this.lblNom.Size = new System.Drawing.Size(42, 16);
            this.lblNom.TabIndex = 1;
            this.lblNom.Text = "Nom :";
            // 
            // lblPrenom
            // 
            this.lblPrenom.AutoSize = true;
            this.lblPrenom.Location = new System.Drawing.Point(111, 114);
            this.lblPrenom.Name = "lblPrenom";
            this.lblPrenom.Size = new System.Drawing.Size(60, 16);
            this.lblPrenom.TabIndex = 2;
            this.lblPrenom.Text = "Prenom :";
            // 
            // lblSexe
            // 
            this.lblSexe.AutoSize = true;
            this.lblSexe.Location = new System.Drawing.Point(111, 149);
            this.lblSexe.Name = "lblSexe";
            this.lblSexe.Size = new System.Drawing.Size(44, 16);
            this.lblSexe.TabIndex = 3;
            this.lblSexe.Text = "Sexe :";
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Location = new System.Drawing.Point(111, 186);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(45, 16);
            this.lblType.TabIndex = 4;
            this.lblType.Text = "Type :";
            // 
            // lblDateNaissance
            // 
            this.lblDateNaissance.AutoSize = true;
            this.lblDateNaissance.Location = new System.Drawing.Point(111, 225);
            this.lblDateNaissance.Name = "lblDateNaissance";
            this.lblDateNaissance.Size = new System.Drawing.Size(126, 16);
            this.lblDateNaissance.TabIndex = 5;
            this.lblDateNaissance.Text = "Date de naissance :";
            // 
            // lblPortable
            // 
            this.lblPortable.AutoSize = true;
            this.lblPortable.Location = new System.Drawing.Point(111, 258);
            this.lblPortable.Name = "lblPortable";
            this.lblPortable.Size = new System.Drawing.Size(64, 16);
            this.lblPortable.TabIndex = 6;
            this.lblPortable.Text = "Portable :";
            // 
            // lblBip
            // 
            this.lblBip.AutoSize = true;
            this.lblBip.Location = new System.Drawing.Point(111, 289);
            this.lblBip.Name = "lblBip";
            this.lblBip.Size = new System.Drawing.Size(33, 16);
            this.lblBip.TabIndex = 7;
            this.lblBip.Text = "Bip :";
            // 
            // lblGrade
            // 
            this.lblGrade.AutoSize = true;
            this.lblGrade.Location = new System.Drawing.Point(111, 323);
            this.lblGrade.Name = "lblGrade";
            this.lblGrade.Size = new System.Drawing.Size(87, 16);
            this.lblGrade.TabIndex = 8;
            this.lblGrade.Text = "Code Grade :";
            // 
            // lblDateEmbauche
            // 
            this.lblDateEmbauche.AutoSize = true;
            this.lblDateEmbauche.Location = new System.Drawing.Point(111, 358);
            this.lblDateEmbauche.Name = "lblDateEmbauche";
            this.lblDateEmbauche.Size = new System.Drawing.Size(109, 16);
            this.lblDateEmbauche.TabIndex = 9;
            this.lblDateEmbauche.Text = "Date embauche :";
            // 
            // txtMatricule
            // 
            this.txtMatricule.Location = new System.Drawing.Point(233, 40);
            this.txtMatricule.Name = "txtMatricule";
            this.txtMatricule.Size = new System.Drawing.Size(243, 22);
            this.txtMatricule.TabIndex = 1;
            this.txtMatricule.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMatricule_KeyPress);
            // 
            // txtPrenom
            // 
            this.txtPrenom.Location = new System.Drawing.Point(233, 114);
            this.txtPrenom.Name = "txtPrenom";
            this.txtPrenom.Size = new System.Drawing.Size(243, 22);
            this.txtPrenom.TabIndex = 3;
            // 
            // txtNom
            // 
            this.txtNom.Location = new System.Drawing.Point(233, 78);
            this.txtNom.Name = "txtNom";
            this.txtNom.Size = new System.Drawing.Size(243, 22);
            this.txtNom.TabIndex = 2;
            // 
            // txtBip
            // 
            this.txtBip.Location = new System.Drawing.Point(233, 286);
            this.txtBip.Name = "txtBip";
            this.txtBip.Size = new System.Drawing.Size(243, 22);
            this.txtBip.TabIndex = 14;
            this.txtBip.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBip_KeyPress);
            // 
            // txtPortable
            // 
            this.txtPortable.Location = new System.Drawing.Point(233, 252);
            this.txtPortable.Name = "txtPortable";
            this.txtPortable.Size = new System.Drawing.Size(243, 22);
            this.txtPortable.TabIndex = 4;
            // 
            // rbM
            // 
            this.rbM.AutoSize = true;
            this.rbM.Location = new System.Drawing.Point(213, 157);
            this.rbM.Name = "rbM";
            this.rbM.Size = new System.Drawing.Size(40, 20);
            this.rbM.TabIndex = 16;
            this.rbM.Text = "M";
            this.rbM.UseVisualStyleBackColor = true;
            this.rbM.CheckedChanged += new System.EventHandler(this.rbM_CheckedChanged);
            // 
            // rbF
            // 
            this.rbF.AutoSize = true;
            this.rbF.Location = new System.Drawing.Point(291, 157);
            this.rbF.Name = "rbF";
            this.rbF.Size = new System.Drawing.Size(37, 20);
            this.rbF.TabIndex = 17;
            this.rbF.Text = "F";
            this.rbF.UseVisualStyleBackColor = true;
            this.rbF.CheckedChanged += new System.EventHandler(this.rbF_CheckedChanged);
            // 
            // cbGrade
            // 
            this.cbGrade.FormattingEnabled = true;
            this.cbGrade.Location = new System.Drawing.Point(224, 320);
            this.cbGrade.Name = "cbGrade";
            this.cbGrade.Size = new System.Drawing.Size(252, 24);
            this.cbGrade.TabIndex = 5;
            // 
            // rbVo
            // 
            this.rbVo.AutoSize = true;
            this.rbVo.Location = new System.Drawing.Point(353, 186);
            this.rbVo.Name = "rbVo";
            this.rbVo.Size = new System.Drawing.Size(90, 20);
            this.rbVo.TabIndex = 19;
            this.rbVo.Text = "Volontaire";
            this.rbVo.UseVisualStyleBackColor = true;
            this.rbVo.CheckedChanged += new System.EventHandler(this.rbVo_CheckedChanged);
            // 
            // rbPro
            // 
            this.rbPro.AutoSize = true;
            this.rbPro.Location = new System.Drawing.Point(213, 186);
            this.rbPro.Name = "rbPro";
            this.rbPro.Size = new System.Drawing.Size(115, 20);
            this.rbPro.TabIndex = 20;
            this.rbPro.Text = "Professionelle";
            this.rbPro.UseVisualStyleBackColor = true;
            this.rbPro.CheckedChanged += new System.EventHandler(this.rbPro_CheckedChanged);
            // 
            // dtpEmbauche
            // 
            this.dtpEmbauche.Location = new System.Drawing.Point(224, 358);
            this.dtpEmbauche.Name = "dtpEmbauche";
            this.dtpEmbauche.Size = new System.Drawing.Size(252, 22);
            this.dtpEmbauche.TabIndex = 21;
            this.dtpEmbauche.Value = new System.DateTime(2025, 5, 19, 0, 0, 0, 0);
            // 
            // dtpNaissance
            // 
            this.dtpNaissance.Location = new System.Drawing.Point(243, 224);
            this.dtpNaissance.Name = "dtpNaissance";
            this.dtpNaissance.Size = new System.Drawing.Size(233, 22);
            this.dtpNaissance.TabIndex = 22;
            this.dtpNaissance.Value = new System.DateTime(2025, 5, 19, 0, 0, 0, 0);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(647, 335);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(129, 39);
            this.button1.TabIndex = 23;
            this.button1.Text = "Ajouter";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(111, 395);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 16);
            this.label1.TabIndex = 24;
            this.label1.Text = "Date embauche :";
            // 
            // cbCaserne
            // 
            this.cbCaserne.FormattingEnabled = true;
            this.cbCaserne.Location = new System.Drawing.Point(224, 392);
            this.cbCaserne.Name = "cbCaserne";
            this.cbCaserne.Size = new System.Drawing.Size(252, 24);
            this.cbCaserne.TabIndex = 6;
            this.cbCaserne.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // btnQuitter
            // 
            this.btnQuitter.Location = new System.Drawing.Point(647, 383);
            this.btnQuitter.Name = "btnQuitter";
            this.btnQuitter.Size = new System.Drawing.Size(129, 28);
            this.btnQuitter.TabIndex = 26;
            this.btnQuitter.Text = "Quitter";
            this.btnQuitter.UseVisualStyleBackColor = true;
            this.btnQuitter.Click += new System.EventHandler(this.btnQuitter_Click);
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnQuitter);
            this.Controls.Add(this.cbCaserne);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dtpNaissance);
            this.Controls.Add(this.dtpEmbauche);
            this.Controls.Add(this.rbPro);
            this.Controls.Add(this.rbVo);
            this.Controls.Add(this.cbGrade);
            this.Controls.Add(this.rbF);
            this.Controls.Add(this.rbM);
            this.Controls.Add(this.txtPortable);
            this.Controls.Add(this.txtBip);
            this.Controls.Add(this.txtNom);
            this.Controls.Add(this.txtPrenom);
            this.Controls.Add(this.txtMatricule);
            this.Controls.Add(this.lblDateEmbauche);
            this.Controls.Add(this.lblGrade);
            this.Controls.Add(this.lblBip);
            this.Controls.Add(this.lblPortable);
            this.Controls.Add(this.lblDateNaissance);
            this.Controls.Add(this.lblType);
            this.Controls.Add(this.lblSexe);
            this.Controls.Add(this.lblPrenom);
            this.Controls.Add(this.lblNom);
            this.Controls.Add(this.lblMatricule);
            this.Name = "Form3";
            this.Text = "Form3";
            this.Load += new System.EventHandler(this.Form3_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblMatricule;
        private System.Windows.Forms.Label lblNom;
        private System.Windows.Forms.Label lblPrenom;
        private System.Windows.Forms.Label lblSexe;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.Label lblDateNaissance;
        private System.Windows.Forms.Label lblPortable;
        private System.Windows.Forms.Label lblBip;
        private System.Windows.Forms.Label lblGrade;
        private System.Windows.Forms.Label lblDateEmbauche;
        private System.Windows.Forms.TextBox txtMatricule;
        private System.Windows.Forms.TextBox txtPrenom;
        private System.Windows.Forms.TextBox txtNom;
        private System.Windows.Forms.TextBox txtBip;
        private System.Windows.Forms.TextBox txtPortable;
        private System.Windows.Forms.CheckBox rbM;
        private System.Windows.Forms.CheckBox rbF;
        private System.Windows.Forms.ComboBox cbGrade;
        private System.Windows.Forms.CheckBox rbVo;
        private System.Windows.Forms.CheckBox rbPro;
        private System.Windows.Forms.DateTimePicker dtpEmbauche;
        private System.Windows.Forms.DateTimePicker dtpNaissance;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbCaserne;
        private System.Windows.Forms.Button btnQuitter;
    }
}