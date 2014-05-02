namespace TestCrypt.Pages
{
    partial class PageFragment
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PageFragment));
            this.dlgOpenFile = new System.Windows.Forms.OpenFileDialog();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.grpHeader = new System.Windows.Forms.GroupBox();
            this.btnSelect = new System.Windows.Forms.Button();
            this.txtHeader = new System.Windows.Forms.TextBox();
            this.lblInfo = new System.Windows.Forms.Label();
            this.lsvInfo = new System.Windows.Forms.ListView();
            this.lblHeader = new System.Windows.Forms.Label();
            this.grpMatch = new System.Windows.Forms.GroupBox();
            this.hexMatch = new Be.Windows.Forms.HexBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.grpHeader.SuspendLayout();
            this.grpMatch.SuspendLayout();
            this.SuspendLayout();
            // 
            // dlgOpenFile
            // 
            resources.ApplyResources(this.dlgOpenFile, "dlgOpenFile");
            // 
            // splitContainer
            // 
            resources.ApplyResources(this.splitContainer, "splitContainer");
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.grpHeader);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.grpMatch);
            // 
            // grpHeader
            // 
            resources.ApplyResources(this.grpHeader, "grpHeader");
            this.grpHeader.Controls.Add(this.btnSelect);
            this.grpHeader.Controls.Add(this.txtHeader);
            this.grpHeader.Controls.Add(this.lblInfo);
            this.grpHeader.Controls.Add(this.lsvInfo);
            this.grpHeader.Controls.Add(this.lblHeader);
            this.grpHeader.Name = "grpHeader";
            this.grpHeader.TabStop = false;
            // 
            // btnSelect
            // 
            resources.ApplyResources(this.btnSelect, "btnSelect");
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // txtHeader
            // 
            resources.ApplyResources(this.txtHeader, "txtHeader");
            this.txtHeader.Name = "txtHeader";
            this.txtHeader.ReadOnly = true;
            // 
            // lblInfo
            // 
            resources.ApplyResources(this.lblInfo, "lblInfo");
            this.lblInfo.Name = "lblInfo";
            // 
            // lsvInfo
            // 
            resources.ApplyResources(this.lsvInfo, "lsvInfo");
            this.lsvInfo.BackColor = System.Drawing.SystemColors.Control;
            this.lsvInfo.Name = "lsvInfo";
            this.lsvInfo.UseCompatibleStateImageBehavior = false;
            // 
            // lblHeader
            // 
            resources.ApplyResources(this.lblHeader, "lblHeader");
            this.lblHeader.Name = "lblHeader";
            // 
            // grpMatch
            // 
            resources.ApplyResources(this.grpMatch, "grpMatch");
            this.grpMatch.Controls.Add(this.hexMatch);
            this.grpMatch.Name = "grpMatch";
            this.grpMatch.TabStop = false;
            // 
            // hexMatch
            // 
            resources.ApplyResources(this.hexMatch, "hexMatch");
            this.hexMatch.LineInfoForeColor = System.Drawing.Color.Empty;
            this.hexMatch.Name = "hexMatch";
            this.hexMatch.ShadowSelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(60)))), ((int)(((byte)(188)))), ((int)(((byte)(255)))));
            // 
            // PageFragment
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer);
            this.Name = "PageFragment";
            this.PageActivated += new System.EventHandler<System.EventArgs>(this.TrueCryptPage_PageActivated);
            this.PageNext += new System.EventHandler<TestCrypt.WizardPage.PageTransitionEventArgs>(this.TrueCryptPage_PageNext);
            this.PageBack += new System.EventHandler<TestCrypt.WizardPage.PageTransitionEventArgs>(this.PageFragment_PageBack);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.grpHeader.ResumeLayout(false);
            this.grpHeader.PerformLayout();
            this.grpMatch.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog dlgOpenFile;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.GroupBox grpHeader;
        internal System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.TextBox txtHeader;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.ListView lsvInfo;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.GroupBox grpMatch;
        private Be.Windows.Forms.HexBox hexMatch;

    }
}
