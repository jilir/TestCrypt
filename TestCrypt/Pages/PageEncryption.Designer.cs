namespace TestCrypt.Pages
{
    partial class PageEncryption
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PageEncryption));
            this.grpEncryptionAlgorithm = new System.Windows.Forms.GroupBox();
            this.chkBlowfishAes = new System.Windows.Forms.CheckBox();
            this.chkTripleDes = new System.Windows.Forms.CheckBox();
            this.chkCast = new System.Windows.Forms.CheckBox();
            this.chkBlowfish = new System.Windows.Forms.CheckBox();
            this.chkTwofishAes = new System.Windows.Forms.CheckBox();
            this.chkTwofish = new System.Windows.Forms.CheckBox();
            this.chkSerpentBlowfishAes = new System.Windows.Forms.CheckBox();
            this.chkSerpentTwofishAes = new System.Windows.Forms.CheckBox();
            this.chkSerpentTwofish = new System.Windows.Forms.CheckBox();
            this.chkSerpent = new System.Windows.Forms.CheckBox();
            this.chkAesTwofishSerpent = new System.Windows.Forms.CheckBox();
            this.chkAesSerpent = new System.Windows.Forms.CheckBox();
            this.chkAes = new System.Windows.Forms.CheckBox();
            this.grpMode = new System.Windows.Forms.GroupBox();
            this.chkOuterCbc = new System.Windows.Forms.CheckBox();
            this.chkInnerCbc = new System.Windows.Forms.CheckBox();
            this.chkCbc = new System.Windows.Forms.CheckBox();
            this.chkLrw = new System.Windows.Forms.CheckBox();
            this.chkXts = new System.Windows.Forms.CheckBox();
            this.grpPkcs5Prf = new System.Windows.Forms.GroupBox();
            this.chkSha1 = new System.Windows.Forms.CheckBox();
            this.chkWhirlpool = new System.Windows.Forms.CheckBox();
            this.chkSha512 = new System.Windows.Forms.CheckBox();
            this.chkRipemd160 = new System.Windows.Forms.CheckBox();
            this.grpEncryptionAlgorithm.SuspendLayout();
            this.grpMode.SuspendLayout();
            this.grpPkcs5Prf.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpEncryptionAlgorithm
            // 
            resources.ApplyResources(this.grpEncryptionAlgorithm, "grpEncryptionAlgorithm");
            this.grpEncryptionAlgorithm.Controls.Add(this.chkBlowfishAes);
            this.grpEncryptionAlgorithm.Controls.Add(this.chkTripleDes);
            this.grpEncryptionAlgorithm.Controls.Add(this.chkCast);
            this.grpEncryptionAlgorithm.Controls.Add(this.chkBlowfish);
            this.grpEncryptionAlgorithm.Controls.Add(this.chkTwofishAes);
            this.grpEncryptionAlgorithm.Controls.Add(this.chkTwofish);
            this.grpEncryptionAlgorithm.Controls.Add(this.chkSerpentBlowfishAes);
            this.grpEncryptionAlgorithm.Controls.Add(this.chkSerpentTwofishAes);
            this.grpEncryptionAlgorithm.Controls.Add(this.chkSerpentTwofish);
            this.grpEncryptionAlgorithm.Controls.Add(this.chkSerpent);
            this.grpEncryptionAlgorithm.Controls.Add(this.chkAesTwofishSerpent);
            this.grpEncryptionAlgorithm.Controls.Add(this.chkAesSerpent);
            this.grpEncryptionAlgorithm.Controls.Add(this.chkAes);
            this.grpEncryptionAlgorithm.Name = "grpEncryptionAlgorithm";
            this.grpEncryptionAlgorithm.TabStop = false;
            // 
            // chkBlowfishAes
            // 
            resources.ApplyResources(this.chkBlowfishAes, "chkBlowfishAes");
            this.chkBlowfishAes.Name = "chkBlowfishAes";
            this.chkBlowfishAes.UseVisualStyleBackColor = true;
            this.chkBlowfishAes.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            // 
            // chkTripleDes
            // 
            resources.ApplyResources(this.chkTripleDes, "chkTripleDes");
            this.chkTripleDes.Name = "chkTripleDes";
            this.chkTripleDes.UseVisualStyleBackColor = true;
            this.chkTripleDes.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            // 
            // chkCast
            // 
            resources.ApplyResources(this.chkCast, "chkCast");
            this.chkCast.Name = "chkCast";
            this.chkCast.UseVisualStyleBackColor = true;
            this.chkCast.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            // 
            // chkBlowfish
            // 
            resources.ApplyResources(this.chkBlowfish, "chkBlowfish");
            this.chkBlowfish.Name = "chkBlowfish";
            this.chkBlowfish.UseVisualStyleBackColor = true;
            this.chkBlowfish.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            // 
            // chkTwofishAes
            // 
            resources.ApplyResources(this.chkTwofishAes, "chkTwofishAes");
            this.chkTwofishAes.Name = "chkTwofishAes";
            this.chkTwofishAes.UseVisualStyleBackColor = true;
            this.chkTwofishAes.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            // 
            // chkTwofish
            // 
            resources.ApplyResources(this.chkTwofish, "chkTwofish");
            this.chkTwofish.Name = "chkTwofish";
            this.chkTwofish.UseVisualStyleBackColor = true;
            this.chkTwofish.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            // 
            // chkSerpentBlowfishAes
            // 
            resources.ApplyResources(this.chkSerpentBlowfishAes, "chkSerpentBlowfishAes");
            this.chkSerpentBlowfishAes.Name = "chkSerpentBlowfishAes";
            this.chkSerpentBlowfishAes.UseVisualStyleBackColor = true;
            this.chkSerpentBlowfishAes.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            // 
            // chkSerpentTwofishAes
            // 
            resources.ApplyResources(this.chkSerpentTwofishAes, "chkSerpentTwofishAes");
            this.chkSerpentTwofishAes.Name = "chkSerpentTwofishAes";
            this.chkSerpentTwofishAes.UseVisualStyleBackColor = true;
            this.chkSerpentTwofishAes.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            // 
            // chkSerpentTwofish
            // 
            resources.ApplyResources(this.chkSerpentTwofish, "chkSerpentTwofish");
            this.chkSerpentTwofish.Name = "chkSerpentTwofish";
            this.chkSerpentTwofish.UseVisualStyleBackColor = true;
            this.chkSerpentTwofish.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            // 
            // chkSerpent
            // 
            resources.ApplyResources(this.chkSerpent, "chkSerpent");
            this.chkSerpent.Name = "chkSerpent";
            this.chkSerpent.UseVisualStyleBackColor = true;
            this.chkSerpent.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            // 
            // chkAesTwofishSerpent
            // 
            resources.ApplyResources(this.chkAesTwofishSerpent, "chkAesTwofishSerpent");
            this.chkAesTwofishSerpent.Name = "chkAesTwofishSerpent";
            this.chkAesTwofishSerpent.UseVisualStyleBackColor = true;
            this.chkAesTwofishSerpent.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            // 
            // chkAesSerpent
            // 
            resources.ApplyResources(this.chkAesSerpent, "chkAesSerpent");
            this.chkAesSerpent.Name = "chkAesSerpent";
            this.chkAesSerpent.UseVisualStyleBackColor = true;
            this.chkAesSerpent.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            // 
            // chkAes
            // 
            resources.ApplyResources(this.chkAes, "chkAes");
            this.chkAes.Checked = true;
            this.chkAes.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAes.Name = "chkAes";
            this.chkAes.UseVisualStyleBackColor = true;
            this.chkAes.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            // 
            // grpMode
            // 
            resources.ApplyResources(this.grpMode, "grpMode");
            this.grpMode.Controls.Add(this.chkOuterCbc);
            this.grpMode.Controls.Add(this.chkInnerCbc);
            this.grpMode.Controls.Add(this.chkCbc);
            this.grpMode.Controls.Add(this.chkLrw);
            this.grpMode.Controls.Add(this.chkXts);
            this.grpMode.Name = "grpMode";
            this.grpMode.TabStop = false;
            // 
            // chkOuterCbc
            // 
            resources.ApplyResources(this.chkOuterCbc, "chkOuterCbc");
            this.chkOuterCbc.Name = "chkOuterCbc";
            this.chkOuterCbc.UseVisualStyleBackColor = true;
            this.chkOuterCbc.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            // 
            // chkInnerCbc
            // 
            resources.ApplyResources(this.chkInnerCbc, "chkInnerCbc");
            this.chkInnerCbc.Name = "chkInnerCbc";
            this.chkInnerCbc.UseVisualStyleBackColor = true;
            this.chkInnerCbc.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            // 
            // chkCbc
            // 
            resources.ApplyResources(this.chkCbc, "chkCbc");
            this.chkCbc.Name = "chkCbc";
            this.chkCbc.UseVisualStyleBackColor = true;
            this.chkCbc.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            // 
            // chkLrw
            // 
            resources.ApplyResources(this.chkLrw, "chkLrw");
            this.chkLrw.Name = "chkLrw";
            this.chkLrw.UseVisualStyleBackColor = true;
            this.chkLrw.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            // 
            // chkXts
            // 
            resources.ApplyResources(this.chkXts, "chkXts");
            this.chkXts.Checked = true;
            this.chkXts.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkXts.Name = "chkXts";
            this.chkXts.UseVisualStyleBackColor = true;
            this.chkXts.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            // 
            // grpPkcs5Prf
            // 
            resources.ApplyResources(this.grpPkcs5Prf, "grpPkcs5Prf");
            this.grpPkcs5Prf.Controls.Add(this.chkSha1);
            this.grpPkcs5Prf.Controls.Add(this.chkWhirlpool);
            this.grpPkcs5Prf.Controls.Add(this.chkSha512);
            this.grpPkcs5Prf.Controls.Add(this.chkRipemd160);
            this.grpPkcs5Prf.Name = "grpPkcs5Prf";
            this.grpPkcs5Prf.TabStop = false;
            // 
            // chkSha1
            // 
            resources.ApplyResources(this.chkSha1, "chkSha1");
            this.chkSha1.Name = "chkSha1";
            this.chkSha1.UseVisualStyleBackColor = true;
            this.chkSha1.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            // 
            // chkWhirlpool
            // 
            resources.ApplyResources(this.chkWhirlpool, "chkWhirlpool");
            this.chkWhirlpool.Name = "chkWhirlpool";
            this.chkWhirlpool.UseVisualStyleBackColor = true;
            this.chkWhirlpool.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            // 
            // chkSha512
            // 
            resources.ApplyResources(this.chkSha512, "chkSha512");
            this.chkSha512.Name = "chkSha512";
            this.chkSha512.UseVisualStyleBackColor = true;
            this.chkSha512.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            // 
            // chkRipemd160
            // 
            resources.ApplyResources(this.chkRipemd160, "chkRipemd160");
            this.chkRipemd160.Checked = true;
            this.chkRipemd160.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkRipemd160.Name = "chkRipemd160";
            this.chkRipemd160.UseVisualStyleBackColor = true;
            this.chkRipemd160.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            // 
            // PageEncryption
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpEncryptionAlgorithm);
            this.Controls.Add(this.grpMode);
            this.Controls.Add(this.grpPkcs5Prf);
            this.Name = "PageEncryption";
            this.PageNext += new System.EventHandler<TestCrypt.WizardPage.PageTransitionEventArgs>(this.PageEncryption_PageNext);
            this.PageBack += new System.EventHandler<TestCrypt.WizardPage.PageTransitionEventArgs>(this.PageEncryption_PageBack);
            this.grpEncryptionAlgorithm.ResumeLayout(false);
            this.grpEncryptionAlgorithm.PerformLayout();
            this.grpMode.ResumeLayout(false);
            this.grpMode.PerformLayout();
            this.grpPkcs5Prf.ResumeLayout(false);
            this.grpPkcs5Prf.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpPkcs5Prf;
        private System.Windows.Forms.CheckBox chkSha1;
        private System.Windows.Forms.CheckBox chkWhirlpool;
        private System.Windows.Forms.CheckBox chkSha512;
        private System.Windows.Forms.CheckBox chkRipemd160;
        private System.Windows.Forms.GroupBox grpMode;
        private System.Windows.Forms.GroupBox grpEncryptionAlgorithm;
        private System.Windows.Forms.CheckBox chkOuterCbc;
        private System.Windows.Forms.CheckBox chkInnerCbc;
        private System.Windows.Forms.CheckBox chkCbc;
        private System.Windows.Forms.CheckBox chkLrw;
        private System.Windows.Forms.CheckBox chkXts;
        private System.Windows.Forms.CheckBox chkBlowfishAes;
        private System.Windows.Forms.CheckBox chkTripleDes;
        private System.Windows.Forms.CheckBox chkCast;
        private System.Windows.Forms.CheckBox chkBlowfish;
        private System.Windows.Forms.CheckBox chkTwofishAes;
        private System.Windows.Forms.CheckBox chkTwofish;
        private System.Windows.Forms.CheckBox chkSerpentBlowfishAes;
        private System.Windows.Forms.CheckBox chkSerpentTwofishAes;
        private System.Windows.Forms.CheckBox chkSerpentTwofish;
        private System.Windows.Forms.CheckBox chkSerpent;
        private System.Windows.Forms.CheckBox chkAesTwofishSerpent;
        private System.Windows.Forms.CheckBox chkAesSerpent;
        private System.Windows.Forms.CheckBox chkAes;

    }
}
