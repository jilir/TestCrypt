namespace TestCrypt.Pages
{
    partial class PartitionAnalyzerPage
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
            this.lblAnalyzerParameters = new System.Windows.Forms.GroupBox();
            this.grpEnd = new System.Windows.Forms.GroupBox();
            this.btnEndSet = new System.Windows.Forms.Button();
            this.txtEndSectorsAfter = new System.Windows.Forms.NumericUpDown();
            this.lblEndSectorsAfter = new System.Windows.Forms.Label();
            this.txtEndSectorsBefore = new System.Windows.Forms.NumericUpDown();
            this.lblEndSectorsBefore = new System.Windows.Forms.Label();
            this.optEndManual = new System.Windows.Forms.RadioButton();
            this.optEndAutomatic = new System.Windows.Forms.RadioButton();
            this.optEndNoAnalyze = new System.Windows.Forms.RadioButton();
            this.grpBegin = new System.Windows.Forms.GroupBox();
            this.btnBeginSet = new System.Windows.Forms.Button();
            this.txtBeginSectorsAfter = new System.Windows.Forms.NumericUpDown();
            this.lblBeginSectorsAfter = new System.Windows.Forms.Label();
            this.txtBeginSectorsBefore = new System.Windows.Forms.NumericUpDown();
            this.lblBeginSectorsBefore = new System.Windows.Forms.Label();
            this.optBeginManual = new System.Windows.Forms.RadioButton();
            this.optBeginAutomatic = new System.Windows.Forms.RadioButton();
            this.optBeginNoAnalyze = new System.Windows.Forms.RadioButton();
            this.lsvPartitions = new System.Windows.Forms.ListView();
            this.lsvPartitionsColPartition = new System.Windows.Forms.ColumnHeader();
            this.lsvPartitionsColStartOffset = new System.Windows.Forms.ColumnHeader();
            this.lsvPartitionsColEndOffset = new System.Windows.Forms.ColumnHeader();
            this.lsvPartitionsColSizeInBytes = new System.Windows.Forms.ColumnHeader();
            this.lsvPartitionsColSize = new System.Windows.Forms.ColumnHeader();
            this.lsvPartitionsColType = new System.Windows.Forms.ColumnHeader();
            this.lblAnalyzerParameters.SuspendLayout();
            this.grpEnd.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtEndSectorsAfter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEndSectorsBefore)).BeginInit();
            this.grpBegin.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtBeginSectorsAfter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBeginSectorsBefore)).BeginInit();
            this.SuspendLayout();
            // 
            // lblAnalyzerParameters
            // 
            this.lblAnalyzerParameters.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAnalyzerParameters.Controls.Add(this.grpEnd);
            this.lblAnalyzerParameters.Controls.Add(this.grpBegin);
            this.lblAnalyzerParameters.Controls.Add(this.lsvPartitions);
            this.lblAnalyzerParameters.Location = new System.Drawing.Point(3, 3);
            this.lblAnalyzerParameters.Name = "lblAnalyzerParameters";
            this.lblAnalyzerParameters.Size = new System.Drawing.Size(675, 333);
            this.lblAnalyzerParameters.TabIndex = 0;
            this.lblAnalyzerParameters.TabStop = false;
            this.lblAnalyzerParameters.Text = "Analyzer Parameters for each Partition";
            // 
            // grpEnd
            // 
            this.grpEnd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.grpEnd.Controls.Add(this.btnEndSet);
            this.grpEnd.Controls.Add(this.txtEndSectorsAfter);
            this.grpEnd.Controls.Add(this.lblEndSectorsAfter);
            this.grpEnd.Controls.Add(this.txtEndSectorsBefore);
            this.grpEnd.Controls.Add(this.lblEndSectorsBefore);
            this.grpEnd.Controls.Add(this.optEndManual);
            this.grpEnd.Controls.Add(this.optEndAutomatic);
            this.grpEnd.Controls.Add(this.optEndNoAnalyze);
            this.grpEnd.Location = new System.Drawing.Point(346, 171);
            this.grpEnd.Name = "grpEnd";
            this.grpEnd.Size = new System.Drawing.Size(323, 148);
            this.grpEnd.TabIndex = 0;
            this.grpEnd.TabStop = false;
            this.grpEnd.Text = "End of Partition";
            // 
            // btnEndSet
            // 
            this.btnEndSet.Location = new System.Drawing.Point(242, 119);
            this.btnEndSet.Name = "btnEndSet";
            this.btnEndSet.Size = new System.Drawing.Size(75, 23);
            this.btnEndSet.TabIndex = 7;
            this.btnEndSet.Text = "Set";
            this.btnEndSet.UseVisualStyleBackColor = true;
            this.btnEndSet.Click += new System.EventHandler(this.btnEndSet_Click);
            // 
            // txtEndSectorsAfter
            // 
            this.txtEndSectorsAfter.Location = new System.Drawing.Point(92, 68);
            this.txtEndSectorsAfter.Name = "txtEndSectorsAfter";
            this.txtEndSectorsAfter.Size = new System.Drawing.Size(92, 20);
            this.txtEndSectorsAfter.TabIndex = 6;
            // 
            // lblEndSectorsAfter
            // 
            this.lblEndSectorsAfter.AutoSize = true;
            this.lblEndSectorsAfter.Location = new System.Drawing.Point(6, 70);
            this.lblEndSectorsAfter.Name = "lblEndSectorsAfter";
            this.lblEndSectorsAfter.Size = new System.Drawing.Size(71, 13);
            this.lblEndSectorsAfter.TabIndex = 2;
            this.lblEndSectorsAfter.Text = "Sectors After:";
            // 
            // txtEndSectorsBefore
            // 
            this.txtEndSectorsBefore.Location = new System.Drawing.Point(92, 42);
            this.txtEndSectorsBefore.Name = "txtEndSectorsBefore";
            this.txtEndSectorsBefore.Size = new System.Drawing.Size(92, 20);
            this.txtEndSectorsBefore.TabIndex = 5;
            // 
            // lblEndSectorsBefore
            // 
            this.lblEndSectorsBefore.AutoSize = true;
            this.lblEndSectorsBefore.Location = new System.Drawing.Point(6, 44);
            this.lblEndSectorsBefore.Name = "lblEndSectorsBefore";
            this.lblEndSectorsBefore.Size = new System.Drawing.Size(80, 13);
            this.lblEndSectorsBefore.TabIndex = 1;
            this.lblEndSectorsBefore.Text = "Sectors Before:";
            // 
            // optEndManual
            // 
            this.optEndManual.AutoSize = true;
            this.optEndManual.Location = new System.Drawing.Point(6, 19);
            this.optEndManual.Name = "optEndManual";
            this.optEndManual.Size = new System.Drawing.Size(60, 17);
            this.optEndManual.TabIndex = 0;
            this.optEndManual.TabStop = true;
            this.optEndManual.Text = "Manual";
            this.optEndManual.UseVisualStyleBackColor = true;
            this.optEndManual.CheckedChanged += new System.EventHandler(this.grpEnd_CheckedChanged);
            // 
            // optEndAutomatic
            // 
            this.optEndAutomatic.AutoSize = true;
            this.optEndAutomatic.Location = new System.Drawing.Point(6, 95);
            this.optEndAutomatic.Name = "optEndAutomatic";
            this.optEndAutomatic.Size = new System.Drawing.Size(72, 17);
            this.optEndAutomatic.TabIndex = 3;
            this.optEndAutomatic.TabStop = true;
            this.optEndAutomatic.Text = "Automatic";
            this.optEndAutomatic.UseVisualStyleBackColor = true;
            this.optEndAutomatic.CheckedChanged += new System.EventHandler(this.grpEnd_CheckedChanged);
            // 
            // optEndNoAnalyze
            // 
            this.optEndNoAnalyze.AutoSize = true;
            this.optEndNoAnalyze.Location = new System.Drawing.Point(6, 118);
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
            this.grpBegin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.grpBegin.Controls.Add(this.btnBeginSet);
            this.grpBegin.Controls.Add(this.txtBeginSectorsAfter);
            this.grpBegin.Controls.Add(this.lblBeginSectorsAfter);
            this.grpBegin.Controls.Add(this.txtBeginSectorsBefore);
            this.grpBegin.Controls.Add(this.lblBeginSectorsBefore);
            this.grpBegin.Controls.Add(this.optBeginManual);
            this.grpBegin.Controls.Add(this.optBeginAutomatic);
            this.grpBegin.Controls.Add(this.optBeginNoAnalyze);
            this.grpBegin.Location = new System.Drawing.Point(6, 171);
            this.grpBegin.Name = "grpBegin";
            this.grpBegin.Size = new System.Drawing.Size(323, 148);
            this.grpBegin.TabIndex = 2;
            this.grpBegin.TabStop = false;
            this.grpBegin.Text = "Begin of Partition";
            // 
            // btnBeginSet
            // 
            this.btnBeginSet.Location = new System.Drawing.Point(242, 119);
            this.btnBeginSet.Name = "btnBeginSet";
            this.btnBeginSet.Size = new System.Drawing.Size(75, 23);
            this.btnBeginSet.TabIndex = 7;
            this.btnBeginSet.Text = "Set";
            this.btnBeginSet.UseVisualStyleBackColor = true;
            this.btnBeginSet.Click += new System.EventHandler(this.btnBeginSet_Click);
            // 
            // txtBeginSectorsAfter
            // 
            this.txtBeginSectorsAfter.Location = new System.Drawing.Point(92, 68);
            this.txtBeginSectorsAfter.Name = "txtBeginSectorsAfter";
            this.txtBeginSectorsAfter.Size = new System.Drawing.Size(92, 20);
            this.txtBeginSectorsAfter.TabIndex = 6;
            // 
            // lblBeginSectorsAfter
            // 
            this.lblBeginSectorsAfter.AutoSize = true;
            this.lblBeginSectorsAfter.Location = new System.Drawing.Point(6, 70);
            this.lblBeginSectorsAfter.Name = "lblBeginSectorsAfter";
            this.lblBeginSectorsAfter.Size = new System.Drawing.Size(71, 13);
            this.lblBeginSectorsAfter.TabIndex = 2;
            this.lblBeginSectorsAfter.Text = "Sectors After:";
            // 
            // txtBeginSectorsBefore
            // 
            this.txtBeginSectorsBefore.Location = new System.Drawing.Point(92, 42);
            this.txtBeginSectorsBefore.Name = "txtBeginSectorsBefore";
            this.txtBeginSectorsBefore.Size = new System.Drawing.Size(92, 20);
            this.txtBeginSectorsBefore.TabIndex = 5;
            // 
            // lblBeginSectorsBefore
            // 
            this.lblBeginSectorsBefore.AutoSize = true;
            this.lblBeginSectorsBefore.Location = new System.Drawing.Point(6, 44);
            this.lblBeginSectorsBefore.Name = "lblBeginSectorsBefore";
            this.lblBeginSectorsBefore.Size = new System.Drawing.Size(80, 13);
            this.lblBeginSectorsBefore.TabIndex = 1;
            this.lblBeginSectorsBefore.Text = "Sectors Before:";
            // 
            // optBeginManual
            // 
            this.optBeginManual.AutoSize = true;
            this.optBeginManual.Location = new System.Drawing.Point(6, 19);
            this.optBeginManual.Name = "optBeginManual";
            this.optBeginManual.Size = new System.Drawing.Size(60, 17);
            this.optBeginManual.TabIndex = 0;
            this.optBeginManual.TabStop = true;
            this.optBeginManual.Text = "Manual";
            this.optBeginManual.UseVisualStyleBackColor = true;
            this.optBeginManual.CheckedChanged += new System.EventHandler(this.grpBegin_CheckedChanged);
            // 
            // optBeginAutomatic
            // 
            this.optBeginAutomatic.AutoSize = true;
            this.optBeginAutomatic.Location = new System.Drawing.Point(6, 95);
            this.optBeginAutomatic.Name = "optBeginAutomatic";
            this.optBeginAutomatic.Size = new System.Drawing.Size(72, 17);
            this.optBeginAutomatic.TabIndex = 3;
            this.optBeginAutomatic.TabStop = true;
            this.optBeginAutomatic.Text = "Automatic";
            this.optBeginAutomatic.UseVisualStyleBackColor = true;
            this.optBeginAutomatic.CheckedChanged += new System.EventHandler(this.grpBegin_CheckedChanged);
            // 
            // optBeginNoAnalyze
            // 
            this.optBeginNoAnalyze.AutoSize = true;
            this.optBeginNoAnalyze.Location = new System.Drawing.Point(6, 118);
            this.optBeginNoAnalyze.Name = "optBeginNoAnalyze";
            this.optBeginNoAnalyze.Size = new System.Drawing.Size(96, 17);
            this.optBeginNoAnalyze.TabIndex = 4;
            this.optBeginNoAnalyze.TabStop = true;
            this.optBeginNoAnalyze.Text = "Do not analyze";
            this.optBeginNoAnalyze.UseVisualStyleBackColor = true;
            this.optBeginNoAnalyze.CheckedChanged += new System.EventHandler(this.grpBegin_CheckedChanged);
            // 
            // lsvPartitions
            // 
            this.lsvPartitions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lsvPartitions.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.lsvPartitionsColPartition,
            this.lsvPartitionsColStartOffset,
            this.lsvPartitionsColEndOffset,
            this.lsvPartitionsColSizeInBytes,
            this.lsvPartitionsColSize,
            this.lsvPartitionsColType});
            this.lsvPartitions.FullRowSelect = true;
            this.lsvPartitions.HideSelection = false;
            this.lsvPartitions.Location = new System.Drawing.Point(6, 19);
            this.lsvPartitions.MultiSelect = false;
            this.lsvPartitions.Name = "lsvPartitions";
            this.lsvPartitions.Size = new System.Drawing.Size(663, 146);
            this.lsvPartitions.TabIndex = 0;
            this.lsvPartitions.UseCompatibleStateImageBehavior = false;
            this.lsvPartitions.View = System.Windows.Forms.View.Details;
            this.lsvPartitions.SelectedIndexChanged += new System.EventHandler(this.lsvPartitions_SelectedIndexChanged);
            this.lsvPartitions.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lsvPartitions_MouseUp);
            this.lsvPartitions.KeyUp += new System.Windows.Forms.KeyEventHandler(this.lsvPartitions_KeyUp);
            // 
            // lsvPartitionsColPartition
            // 
            this.lsvPartitionsColPartition.Text = "Partition";
            // 
            // lsvPartitionsColStartOffset
            // 
            this.lsvPartitionsColStartOffset.Text = "Start Offset (C/H/S)";
            // 
            // lsvPartitionsColEndOffset
            // 
            this.lsvPartitionsColEndOffset.Text = "End Offset (C/H/S)";
            // 
            // lsvPartitionsColSizeInBytes
            // 
            this.lsvPartitionsColSizeInBytes.Text = "Size in Bytes";
            // 
            // lsvPartitionsColSize
            // 
            this.lsvPartitionsColSize.Text = "Size";
            // 
            // lsvPartitionsColType
            // 
            this.lsvPartitionsColType.Text = "Type";
            // 
            // PartitionAnalyzerPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.lblAnalyzerParameters);
            this.Name = "PartitionAnalyzerPage";
            this.Size = new System.Drawing.Size(681, 342);
            this.Subtitle = "Configure analyzer parameters to scan the beginning or end of partitions";
            this.Title = "Partition Analyzer";
            this.PageActivated += new System.EventHandler<System.EventArgs>(this.PartitionAnalyzerPage_PageActivated);
            this.lblAnalyzerParameters.ResumeLayout(false);
            this.grpEnd.ResumeLayout(false);
            this.grpEnd.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtEndSectorsAfter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEndSectorsBefore)).EndInit();
            this.grpBegin.ResumeLayout(false);
            this.grpBegin.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtBeginSectorsAfter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBeginSectorsBefore)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox lblAnalyzerParameters;
        private System.Windows.Forms.ListView lsvPartitions;
        private System.Windows.Forms.GroupBox grpBegin;
        private System.Windows.Forms.RadioButton optBeginManual;
        private System.Windows.Forms.RadioButton optBeginAutomatic;
        private System.Windows.Forms.RadioButton optBeginNoAnalyze;
        private System.Windows.Forms.NumericUpDown txtBeginSectorsAfter;
        private System.Windows.Forms.Label lblBeginSectorsAfter;
        private System.Windows.Forms.NumericUpDown txtBeginSectorsBefore;
        private System.Windows.Forms.Label lblBeginSectorsBefore;
        private System.Windows.Forms.GroupBox grpEnd;
        private System.Windows.Forms.NumericUpDown txtEndSectorsAfter;
        private System.Windows.Forms.Label lblEndSectorsAfter;
        private System.Windows.Forms.NumericUpDown txtEndSectorsBefore;
        private System.Windows.Forms.Label lblEndSectorsBefore;
        private System.Windows.Forms.RadioButton optEndManual;
        private System.Windows.Forms.RadioButton optEndAutomatic;
        private System.Windows.Forms.RadioButton optEndNoAnalyze;
        private System.Windows.Forms.Button btnBeginSet;
        private System.Windows.Forms.Button btnEndSet;
        private System.Windows.Forms.ColumnHeader lsvPartitionsColPartition;
        private System.Windows.Forms.ColumnHeader lsvPartitionsColStartOffset;
        private System.Windows.Forms.ColumnHeader lsvPartitionsColSizeInBytes;
        private System.Windows.Forms.ColumnHeader lsvPartitionsColSize;
        private System.Windows.Forms.ColumnHeader lsvPartitionsColEndOffset;
        private System.Windows.Forms.ColumnHeader lsvPartitionsColType;

    }
}
