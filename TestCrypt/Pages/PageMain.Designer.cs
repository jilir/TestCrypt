namespace TestCrypt.Pages
{
    partial class PageMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PageMain));
            this.optRescueDisk = new System.Windows.Forms.RadioButton();
            this.optSearchDeep = new System.Windows.Forms.RadioButton();
            this.optSearchQuick = new System.Windows.Forms.RadioButton();
            this.optSearchAdvanced = new System.Windows.Forms.RadioButton();
            this.otpSearchFragment = new System.Windows.Forms.RadioButton();
            this.optMountInPlace = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // optRescueDisk
            // 
            resources.ApplyResources(this.optRescueDisk, "optRescueDisk");
            this.optRescueDisk.Name = "optRescueDisk";
            this.optRescueDisk.TabStop = true;
            this.optRescueDisk.Tag = "MountRescueDisk";
            this.optRescueDisk.UseVisualStyleBackColor = true;
            this.optRescueDisk.CheckedChanged += new System.EventHandler(this.PageMain_CheckedChanged);
            // 
            // optSearchDeep
            // 
            resources.ApplyResources(this.optSearchDeep, "optSearchDeep");
            this.optSearchDeep.Name = "optSearchDeep";
            this.optSearchDeep.TabStop = true;
            this.optSearchDeep.Tag = "SearchDeep";
            this.optSearchDeep.UseVisualStyleBackColor = true;
            this.optSearchDeep.CheckedChanged += new System.EventHandler(this.PageMain_CheckedChanged);
            // 
            // optSearchQuick
            // 
            resources.ApplyResources(this.optSearchQuick, "optSearchQuick");
            this.optSearchQuick.Name = "optSearchQuick";
            this.optSearchQuick.TabStop = true;
            this.optSearchQuick.Tag = "SearchQuick";
            this.optSearchQuick.UseVisualStyleBackColor = true;
            this.optSearchQuick.CheckedChanged += new System.EventHandler(this.PageMain_CheckedChanged);
            // 
            // optSearchAdvanced
            // 
            resources.ApplyResources(this.optSearchAdvanced, "optSearchAdvanced");
            this.optSearchAdvanced.Name = "optSearchAdvanced";
            this.optSearchAdvanced.TabStop = true;
            this.optSearchAdvanced.Tag = "SearchAdvanced";
            this.optSearchAdvanced.UseVisualStyleBackColor = true;
            this.optSearchAdvanced.CheckedChanged += new System.EventHandler(this.PageMain_CheckedChanged);
            // 
            // otpSearchFragment
            // 
            resources.ApplyResources(this.otpSearchFragment, "otpSearchFragment");
            this.otpSearchFragment.Name = "otpSearchFragment";
            this.otpSearchFragment.TabStop = true;
            this.otpSearchFragment.Tag = "SearchFragment";
            this.otpSearchFragment.UseVisualStyleBackColor = true;
            this.otpSearchFragment.CheckedChanged += new System.EventHandler(this.PageMain_CheckedChanged);
            // 
            // optMountInPlace
            // 
            resources.ApplyResources(this.optMountInPlace, "optMountInPlace");
            this.optMountInPlace.Name = "optMountInPlace";
            this.optMountInPlace.TabStop = true;
            this.optMountInPlace.Tag = "MountInPlace";
            this.optMountInPlace.UseVisualStyleBackColor = true;
            this.optMountInPlace.CheckedChanged += new System.EventHandler(this.PageMain_CheckedChanged);
            // 
            // PageMain
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.optRescueDisk);
            this.Controls.Add(this.optSearchDeep);
            this.Controls.Add(this.optSearchQuick);
            this.Controls.Add(this.optSearchAdvanced);
            this.Controls.Add(this.otpSearchFragment);
            this.Controls.Add(this.optMountInPlace);
            this.Name = "PageMain";
            this.PageNext += new System.EventHandler<TestCrypt.WizardPage.PageTransitionEventArgs>(this.PageMain_PageNext);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton optSearchQuick;
        private System.Windows.Forms.RadioButton optMountInPlace;
        private System.Windows.Forms.RadioButton optSearchAdvanced;
        private System.Windows.Forms.RadioButton otpSearchFragment;
        private System.Windows.Forms.RadioButton optSearchDeep;
        private System.Windows.Forms.RadioButton optRescueDisk;


    }
}
