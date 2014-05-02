namespace TestCrypt.Pages
{
    partial class PageSearch
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PageSearch));
            this.mnuSearch = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuRemove = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuRemoveAll = new System.Windows.Forms.ToolStripMenuItem();
            this.lsvSearch = new System.Windows.Forms.ListView();
            this.mnuSearch.SuspendLayout();
            this.SuspendLayout();
            // 
            // mnuSearch
            // 
            this.mnuSearch.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuAdd,
            this.toolStripMenuItem1,
            this.mnuRemove,
            this.mnuRemoveAll});
            this.mnuSearch.Name = "mnuSearch";
            resources.ApplyResources(this.mnuSearch, "mnuSearch");
            // 
            // mnuAdd
            // 
            this.mnuAdd.Name = "mnuAdd";
            resources.ApplyResources(this.mnuAdd, "mnuAdd");
            this.mnuAdd.Click += new System.EventHandler(this.mnuAdd_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            resources.ApplyResources(this.toolStripMenuItem1, "toolStripMenuItem1");
            // 
            // mnuRemove
            // 
            resources.ApplyResources(this.mnuRemove, "mnuRemove");
            this.mnuRemove.Name = "mnuRemove";
            // 
            // mnuRemoveAll
            // 
            resources.ApplyResources(this.mnuRemoveAll, "mnuRemoveAll");
            this.mnuRemoveAll.Name = "mnuRemoveAll";
            // 
            // lsvSearch
            // 
            this.lsvSearch.ContextMenuStrip = this.mnuSearch;
            resources.ApplyResources(this.lsvSearch, "lsvSearch");
            this.lsvSearch.Name = "lsvSearch";
            this.lsvSearch.UseCompatibleStateImageBehavior = false;
            this.lsvSearch.SelectedIndexChanged += new System.EventHandler(this.lsvSearch_SelectedIndexChanged);
            // 
            // PageSearch
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lsvSearch);
            this.Name = "PageSearch";
            this.PageBack += new System.EventHandler<TestCrypt.WizardPage.PageTransitionEventArgs>(this.PageSearch_PageBack);
            this.mnuSearch.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip mnuSearch;
        private System.Windows.Forms.ToolStripMenuItem mnuAdd;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem mnuRemove;
        private System.Windows.Forms.ToolStripMenuItem mnuRemoveAll;
        private System.Windows.Forms.ListView lsvSearch;
    }
}
