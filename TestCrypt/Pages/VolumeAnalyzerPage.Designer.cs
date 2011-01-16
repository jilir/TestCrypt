namespace TestCrypt.Pages
{
    partial class VolumeAnalyzerPage
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
            this.grpEnd = new System.Windows.Forms.GroupBox();
            this.txtEndSectors = new System.Windows.Forms.NumericUpDown();
            this.lblEndSectors = new System.Windows.Forms.Label();
            this.optEndManual = new System.Windows.Forms.RadioButton();
            this.optEndAutomatic = new System.Windows.Forms.RadioButton();
            this.optEndNoAnalyze = new System.Windows.Forms.RadioButton();
            this.grpBegin = new System.Windows.Forms.GroupBox();
            this.txtBeginSectors = new System.Windows.Forms.NumericUpDown();
            this.lblBeginSectors = new System.Windows.Forms.Label();
            this.optBeginManual = new System.Windows.Forms.RadioButton();
            this.optBeginAutomatic = new System.Windows.Forms.RadioButton();
            this.optBeginNoAnalyze = new System.Windows.Forms.RadioButton();
            this.grpCustomAnalyzer = new System.Windows.Forms.GroupBox();
            this.dgvCustomAnalyzer = new System.Windows.Forms.DataGridView();
            this.dgvCustomAnalyzerColStart = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvCustomAnalyzerColEnd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grpEnd.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtEndSectors)).BeginInit();
            this.grpBegin.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtBeginSectors)).BeginInit();
            this.grpCustomAnalyzer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCustomAnalyzer)).BeginInit();
            this.SuspendLayout();
            // 
            // grpEnd
            // 
            this.grpEnd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.grpEnd.Controls.Add(this.txtEndSectors);
            this.grpEnd.Controls.Add(this.lblEndSectors);
            this.grpEnd.Controls.Add(this.optEndManual);
            this.grpEnd.Controls.Add(this.optEndAutomatic);
            this.grpEnd.Controls.Add(this.optEndNoAnalyze);
            this.grpEnd.Location = new System.Drawing.Point(499, 3);
            this.grpEnd.Name = "grpEnd";
            this.grpEnd.Size = new System.Drawing.Size(173, 122);
            this.grpEnd.TabIndex = 13;
            this.grpEnd.TabStop = false;
            this.grpEnd.Text = "End of Volume";
            // 
            // txtEndSectors
            // 
            this.txtEndSectors.Location = new System.Drawing.Point(58, 42);
            this.txtEndSectors.Name = "txtEndSectors";
            this.txtEndSectors.Size = new System.Drawing.Size(92, 20);
            this.txtEndSectors.TabIndex = 9;
            this.txtEndSectors.ValueChanged += new System.EventHandler(this.txtEndSectors_ValueChanged);
            // 
            // lblEndSectors
            // 
            this.lblEndSectors.AutoSize = true;
            this.lblEndSectors.Location = new System.Drawing.Point(6, 44);
            this.lblEndSectors.Name = "lblEndSectors";
            this.lblEndSectors.Size = new System.Drawing.Size(46, 13);
            this.lblEndSectors.TabIndex = 7;
            this.lblEndSectors.Text = "Sectors:";
            // 
            // optEndManual
            // 
            this.optEndManual.AutoSize = true;
            this.optEndManual.Location = new System.Drawing.Point(6, 19);
            this.optEndManual.Name = "optEndManual";
            this.optEndManual.Size = new System.Drawing.Size(60, 17);
            this.optEndManual.TabIndex = 6;
            this.optEndManual.TabStop = true;
            this.optEndManual.Text = "Manual";
            this.optEndManual.UseVisualStyleBackColor = true;
            this.optEndManual.CheckedChanged += new System.EventHandler(this.grpEnd_CheckedChanged);
            // 
            // optEndAutomatic
            // 
            this.optEndAutomatic.AutoSize = true;
            this.optEndAutomatic.Location = new System.Drawing.Point(6, 70);
            this.optEndAutomatic.Name = "optEndAutomatic";
            this.optEndAutomatic.Size = new System.Drawing.Size(72, 17);
            this.optEndAutomatic.TabIndex = 5;
            this.optEndAutomatic.TabStop = true;
            this.optEndAutomatic.Text = "Automatic";
            this.optEndAutomatic.UseVisualStyleBackColor = true;
            this.optEndAutomatic.CheckedChanged += new System.EventHandler(this.grpEnd_CheckedChanged);
            // 
            // optEndNoAnalyze
            // 
            this.optEndNoAnalyze.AutoSize = true;
            this.optEndNoAnalyze.Location = new System.Drawing.Point(6, 93);
            this.optEndNoAnalyze.Name = "optEndNoAnalyze";
            this.optEndNoAnalyze.Size = new System.Drawing.Size(96, 17);
            this.optEndNoAnalyze.TabIndex = 4;
            this.optEndNoAnalyze.TabStop = true;
            this.optEndNoAnalyze.Text = "Do not analyze";
            this.optEndNoAnalyze.UseVisualStyleBackColor = true;
            this.optEndNoAnalyze.CheckedChanged += new System.EventHandler(this.grpEnd_CheckedChanged);
            // 
            // grpBegin
            // 
            this.grpBegin.Controls.Add(this.txtBeginSectors);
            this.grpBegin.Controls.Add(this.lblBeginSectors);
            this.grpBegin.Controls.Add(this.optBeginManual);
            this.grpBegin.Controls.Add(this.optBeginAutomatic);
            this.grpBegin.Controls.Add(this.optBeginNoAnalyze);
            this.grpBegin.Location = new System.Drawing.Point(3, 3);
            this.grpBegin.Name = "grpBegin";
            this.grpBegin.Size = new System.Drawing.Size(173, 122);
            this.grpBegin.TabIndex = 12;
            this.grpBegin.TabStop = false;
            this.grpBegin.Text = "Begin of Volume";
            // 
            // txtBeginSectors
            // 
            this.txtBeginSectors.Location = new System.Drawing.Point(58, 42);
            this.txtBeginSectors.Name = "txtBeginSectors";
            this.txtBeginSectors.Size = new System.Drawing.Size(92, 20);
            this.txtBeginSectors.TabIndex = 9;
            this.txtBeginSectors.ValueChanged += new System.EventHandler(this.txtBeginSectors_ValueChanged);
            // 
            // lblBeginSectors
            // 
            this.lblBeginSectors.AutoSize = true;
            this.lblBeginSectors.Location = new System.Drawing.Point(6, 44);
            this.lblBeginSectors.Name = "lblBeginSectors";
            this.lblBeginSectors.Size = new System.Drawing.Size(46, 13);
            this.lblBeginSectors.TabIndex = 7;
            this.lblBeginSectors.Text = "Sectors:";
            // 
            // optBeginManual
            // 
            this.optBeginManual.AutoSize = true;
            this.optBeginManual.Location = new System.Drawing.Point(6, 19);
            this.optBeginManual.Name = "optBeginManual";
            this.optBeginManual.Size = new System.Drawing.Size(60, 17);
            this.optBeginManual.TabIndex = 6;
            this.optBeginManual.TabStop = true;
            this.optBeginManual.Text = "Manual";
            this.optBeginManual.UseVisualStyleBackColor = true;
            this.optBeginManual.CheckedChanged += new System.EventHandler(this.grpBegin_CheckedChanged);
            // 
            // optBeginAutomatic
            // 
            this.optBeginAutomatic.AutoSize = true;
            this.optBeginAutomatic.Location = new System.Drawing.Point(6, 70);
            this.optBeginAutomatic.Name = "optBeginAutomatic";
            this.optBeginAutomatic.Size = new System.Drawing.Size(72, 17);
            this.optBeginAutomatic.TabIndex = 5;
            this.optBeginAutomatic.TabStop = true;
            this.optBeginAutomatic.Text = "Automatic";
            this.optBeginAutomatic.UseVisualStyleBackColor = true;
            this.optBeginAutomatic.CheckedChanged += new System.EventHandler(this.grpBegin_CheckedChanged);
            // 
            // optBeginNoAnalyze
            // 
            this.optBeginNoAnalyze.AutoSize = true;
            this.optBeginNoAnalyze.Location = new System.Drawing.Point(6, 93);
            this.optBeginNoAnalyze.Name = "optBeginNoAnalyze";
            this.optBeginNoAnalyze.Size = new System.Drawing.Size(96, 17);
            this.optBeginNoAnalyze.TabIndex = 4;
            this.optBeginNoAnalyze.TabStop = true;
            this.optBeginNoAnalyze.Text = "Do not analyze";
            this.optBeginNoAnalyze.UseVisualStyleBackColor = true;
            this.optBeginNoAnalyze.CheckedChanged += new System.EventHandler(this.grpBegin_CheckedChanged);
            // 
            // grpCustomAnalyzer
            // 
            this.grpCustomAnalyzer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grpCustomAnalyzer.Controls.Add(this.dgvCustomAnalyzer);
            this.grpCustomAnalyzer.Location = new System.Drawing.Point(3, 131);
            this.grpCustomAnalyzer.Name = "grpCustomAnalyzer";
            this.grpCustomAnalyzer.Size = new System.Drawing.Size(669, 202);
            this.grpCustomAnalyzer.TabIndex = 15;
            this.grpCustomAnalyzer.TabStop = false;
            this.grpCustomAnalyzer.Text = "Custom Analyzer";
            // 
            // dgvCustomAnalyzer
            // 
            this.dgvCustomAnalyzer.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCustomAnalyzer.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvCustomAnalyzer.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCustomAnalyzer.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvCustomAnalyzerColStart,
            this.dgvCustomAnalyzerColEnd});
            this.dgvCustomAnalyzer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCustomAnalyzer.Location = new System.Drawing.Point(3, 16);
            this.dgvCustomAnalyzer.Name = "dgvCustomAnalyzer";
            this.dgvCustomAnalyzer.Size = new System.Drawing.Size(663, 183);
            this.dgvCustomAnalyzer.TabIndex = 0;
            this.dgvCustomAnalyzer.RowValidating += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dgvCustomAnalyzer_RowValidating);
            // 
            // dgvCustomAnalyzerColStart
            // 
            this.dgvCustomAnalyzerColStart.HeaderText = "Start Offset (C/H/S or LBA)";
            this.dgvCustomAnalyzerColStart.Name = "dgvCustomAnalyzerColStart";
            // 
            // dgvCustomAnalyzerColEnd
            // 
            this.dgvCustomAnalyzerColEnd.HeaderText = "End Offset (C/H/S or LBA)";
            this.dgvCustomAnalyzerColEnd.Name = "dgvCustomAnalyzerColEnd";
            // 
            // VolumeAnalyzerPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpCustomAnalyzer);
            this.Controls.Add(this.grpEnd);
            this.Controls.Add(this.grpBegin);
            this.Name = "VolumeAnalyzerPage";
            this.Size = new System.Drawing.Size(675, 333);
            this.Subtitle = "Configure analyzer parameters to scan for TrueCrypt volumes";
            this.Title = "Volume Analyzer";
            this.PageActivated += new System.EventHandler<System.EventArgs>(this.VolumeAnalyzerPage_PageActivated);
            this.PageNext += new System.EventHandler<TestCrypt.WizardPage.PageTransitionEventArgs>(this.VolumeAnalyzerPage_PageNext);
            this.grpEnd.ResumeLayout(false);
            this.grpEnd.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtEndSectors)).EndInit();
            this.grpBegin.ResumeLayout(false);
            this.grpBegin.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtBeginSectors)).EndInit();
            this.grpCustomAnalyzer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCustomAnalyzer)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpEnd;
        private System.Windows.Forms.NumericUpDown txtEndSectors;
        private System.Windows.Forms.Label lblEndSectors;
        private System.Windows.Forms.RadioButton optEndManual;
        private System.Windows.Forms.RadioButton optEndAutomatic;
        private System.Windows.Forms.RadioButton optEndNoAnalyze;
        private System.Windows.Forms.GroupBox grpBegin;
        private System.Windows.Forms.NumericUpDown txtBeginSectors;
        private System.Windows.Forms.Label lblBeginSectors;
        private System.Windows.Forms.RadioButton optBeginManual;
        private System.Windows.Forms.RadioButton optBeginAutomatic;
        private System.Windows.Forms.RadioButton optBeginNoAnalyze;
        private System.Windows.Forms.GroupBox grpCustomAnalyzer;
        private System.Windows.Forms.DataGridView dgvCustomAnalyzer;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvCustomAnalyzerColStart;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvCustomAnalyzerColEnd;
    }
}
