namespace TestCrypt.Pages
{
    partial class AnalyzePage
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
            this.prgTask = new System.Windows.Forms.ProgressBar();
            this.prgTotal = new System.Windows.Forms.ProgressBar();
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.lblPercentageTask = new System.Windows.Forms.Label();
            this.lblPercentageTotal = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.lblTask = new System.Windows.Forms.Label();
            this.lblTotalEstimatedTimeRemaining = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // prgTask
            // 
            this.prgTask.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.prgTask.Location = new System.Drawing.Point(3, 66);
            this.prgTask.Name = "prgTask";
            this.prgTask.Size = new System.Drawing.Size(669, 23);
            this.prgTask.TabIndex = 0;
            // 
            // prgTotal
            // 
            this.prgTotal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.prgTotal.Location = new System.Drawing.Point(3, 244);
            this.prgTotal.Name = "prgTotal";
            this.prgTotal.Size = new System.Drawing.Size(669, 23);
            this.prgTotal.TabIndex = 1;
            // 
            // backgroundWorker
            // 
            this.backgroundWorker.WorkerReportsProgress = true;
            this.backgroundWorker.WorkerSupportsCancellation = true;
            this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_DoWork);
            this.backgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker_RunWorkerCompleted);
            this.backgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker_ProgressChanged);
            // 
            // lblPercentageTask
            // 
            this.lblPercentageTask.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPercentageTask.Location = new System.Drawing.Point(602, 92);
            this.lblPercentageTask.Name = "lblPercentageTask";
            this.lblPercentageTask.Size = new System.Drawing.Size(70, 19);
            this.lblPercentageTask.TabIndex = 2;
            this.lblPercentageTask.Text = "0.00%";
            this.lblPercentageTask.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblPercentageTotal
            // 
            this.lblPercentageTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPercentageTotal.Location = new System.Drawing.Point(602, 270);
            this.lblPercentageTotal.Name = "lblPercentageTotal";
            this.lblPercentageTotal.Size = new System.Drawing.Size(70, 19);
            this.lblPercentageTotal.TabIndex = 3;
            this.lblPercentageTotal.Text = "0.00%";
            this.lblPercentageTotal.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblTotal
            // 
            this.lblTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTotal.AutoSize = true;
            this.lblTotal.Location = new System.Drawing.Point(3, 228);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(75, 13);
            this.lblTotal.TabIndex = 4;
            this.lblTotal.Text = "Total Progress";
            // 
            // lblTask
            // 
            this.lblTask.AutoSize = true;
            this.lblTask.Location = new System.Drawing.Point(3, 50);
            this.lblTask.Name = "lblTask";
            this.lblTask.Size = new System.Drawing.Size(31, 13);
            this.lblTask.TabIndex = 5;
            this.lblTask.Text = "Task";
            // 
            // lblTotalEstimatedTimeRemaining
            // 
            this.lblTotalEstimatedTimeRemaining.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTotalEstimatedTimeRemaining.AutoSize = true;
            this.lblTotalEstimatedTimeRemaining.Location = new System.Drawing.Point(3, 270);
            this.lblTotalEstimatedTimeRemaining.Name = "lblTotalEstimatedTimeRemaining";
            this.lblTotalEstimatedTimeRemaining.Size = new System.Drawing.Size(35, 13);
            this.lblTotalEstimatedTimeRemaining.TabIndex = 6;
            this.lblTotalEstimatedTimeRemaining.Text = "label1";
            // 
            // AnalyzePage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblTotalEstimatedTimeRemaining);
            this.Controls.Add(this.lblTask);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.lblPercentageTotal);
            this.Controls.Add(this.lblPercentageTask);
            this.Controls.Add(this.prgTotal);
            this.Controls.Add(this.prgTask);
            this.Name = "AnalyzePage";
            this.Size = new System.Drawing.Size(675, 333);
            this.Subtitle = "The analyzer is scanning for TrueCrypt volumes";
            this.Title = "Analyze";
            this.PageActivated += new System.EventHandler<System.EventArgs>(this.AnalyzePage_PageActivated);
            this.PageBack += new System.EventHandler<TestCrypt.WizardPage.PageTransitionEventArgs>(this.AnalyzePage_PageBack);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar prgTask;
        private System.Windows.Forms.ProgressBar prgTotal;
        private System.ComponentModel.BackgroundWorker backgroundWorker;
        private System.Windows.Forms.Label lblPercentageTask;
        private System.Windows.Forms.Label lblPercentageTotal;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label lblTask;
        private System.Windows.Forms.Label lblTotalEstimatedTimeRemaining;
    }
}
