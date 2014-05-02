namespace TestCrypt.Forms
{
    partial class FormSearch
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSearch));
            this.btnAccept = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblStartSector = new System.Windows.Forms.Label();
            this.txtStartSector = new System.Windows.Forms.TextBox();
            this.txtEndSector = new System.Windows.Forms.TextBox();
            this.lblEndSector = new System.Windows.Forms.Label();
            this.grpHeuristic = new System.Windows.Forms.GroupBox();
            this.SuspendLayout();
            // 
            // btnAccept
            // 
            resources.ApplyResources(this.btnAccept, "btnAccept");
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // lblStartSector
            // 
            resources.ApplyResources(this.lblStartSector, "lblStartSector");
            this.lblStartSector.Name = "lblStartSector";
            // 
            // txtStartSector
            // 
            resources.ApplyResources(this.txtStartSector, "txtStartSector");
            this.txtStartSector.Name = "txtStartSector";
            // 
            // txtEndSector
            // 
            resources.ApplyResources(this.txtEndSector, "txtEndSector");
            this.txtEndSector.Name = "txtEndSector";
            // 
            // lblEndSector
            // 
            resources.ApplyResources(this.lblEndSector, "lblEndSector");
            this.lblEndSector.Name = "lblEndSector";
            // 
            // grpHeuristic
            // 
            resources.ApplyResources(this.grpHeuristic, "grpHeuristic");
            this.grpHeuristic.Name = "grpHeuristic";
            this.grpHeuristic.TabStop = false;
            // 
            // FormSearch
            // 
            this.AcceptButton = this.btnAccept;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.Controls.Add(this.grpHeuristic);
            this.Controls.Add(this.txtEndSector);
            this.Controls.Add(this.lblEndSector);
            this.Controls.Add(this.txtStartSector);
            this.Controls.Add(this.lblStartSector);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnAccept);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormSearch";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAccept;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblStartSector;
        private System.Windows.Forms.TextBox txtStartSector;
        private System.Windows.Forms.TextBox txtEndSector;
        private System.Windows.Forms.Label lblEndSector;
        private System.Windows.Forms.GroupBox grpHeuristic;
    }
}