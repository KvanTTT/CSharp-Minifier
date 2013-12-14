namespace CSharpMinifier.GUI
{
    partial class frmMain
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
			this.btnMinify = new System.Windows.Forms.Button();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.btnOpenFiles = new System.Windows.Forms.Button();
			this.label4 = new System.Windows.Forms.Label();
			this.lbInputFiles = new System.Windows.Forms.ListBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.cbRemoveSpaces = new System.Windows.Forms.CheckBox();
			this.cbCompressIdentifiers = new System.Windows.Forms.CheckBox();
			this.cbRemoveComments = new System.Windows.Forms.CheckBox();
			this.btnCopyToClipboard = new System.Windows.Forms.Button();
			this.tbLineLength = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.ofdInputCodeFiles = new System.Windows.Forms.OpenFileDialog();
			this.cbMinifyFiles = new System.Windows.Forms.CheckBox();
			this.lblOutputCompilied = new System.Windows.Forms.Label();
			this.pbOutputCompilied = new System.Windows.Forms.PictureBox();
			this.lblInputCompilied = new System.Windows.Forms.Label();
			this.pbInputCompilied = new System.Windows.Forms.PictureBox();
			this.cbRemoveRegions = new System.Windows.Forms.CheckBox();
			this.tbInput = new FastColoredTextBoxNS.FastColoredTextBox();
			this.tbOutput = new FastColoredTextBoxNS.FastColoredTextBox();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pbOutputCompilied)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pbInputCompilied)).BeginInit();
			this.SuspendLayout();
			// 
			// btnMinify
			// 
			this.btnMinify.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnMinify.Location = new System.Drawing.Point(577, 157);
			this.btnMinify.Name = "btnMinify";
			this.btnMinify.Size = new System.Drawing.Size(164, 26);
			this.btnMinify.TabIndex = 1;
			this.btnMinify.Text = "Minify";
			this.btnMinify.UseVisualStyleBackColor = true;
			this.btnMinify.Click += new System.EventHandler(this.btnMinify_Click);
			// 
			// splitContainer1
			// 
			this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.splitContainer1.Location = new System.Drawing.Point(12, 12);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.tbInput);
			this.splitContainer1.Panel1.Controls.Add(this.btnOpenFiles);
			this.splitContainer1.Panel1.Controls.Add(this.label4);
			this.splitContainer1.Panel1.Controls.Add(this.lbInputFiles);
			this.splitContainer1.Panel1.Controls.Add(this.label1);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.tbOutput);
			this.splitContainer1.Panel2.Controls.Add(this.label2);
			this.splitContainer1.Size = new System.Drawing.Size(559, 697);
			this.splitContainer1.SplitterDistance = 341;
			this.splitContainer1.TabIndex = 2;
			// 
			// btnOpenFiles
			// 
			this.btnOpenFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOpenFiles.Location = new System.Drawing.Point(465, 5);
			this.btnOpenFiles.Name = "btnOpenFiles";
			this.btnOpenFiles.Size = new System.Drawing.Size(89, 23);
			this.btnOpenFiles.TabIndex = 9;
			this.btnOpenFiles.Text = "Open";
			this.btnOpenFiles.UseVisualStyleBackColor = true;
			this.btnOpenFiles.Click += new System.EventHandler(this.btnOpenFiles_Click);
			// 
			// label4
			// 
			this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label4.Location = new System.Drawing.Point(431, 10);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(37, 16);
			this.label4.TabIndex = 9;
			this.label4.Text = "Files";
			// 
			// lbInputFiles
			// 
			this.lbInputFiles.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lbInputFiles.FormattingEnabled = true;
			this.lbInputFiles.Location = new System.Drawing.Point(434, 33);
			this.lbInputFiles.Name = "lbInputFiles";
			this.lbInputFiles.Size = new System.Drawing.Size(120, 303);
			this.lbInputFiles.TabIndex = 9;
			this.lbInputFiles.SelectedIndexChanged += new System.EventHandler(this.lbInputFiles_SelectedIndexChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label1.Location = new System.Drawing.Point(5, 10);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(36, 16);
			this.label1.TabIndex = 2;
			this.label1.Text = "Input";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label2.Location = new System.Drawing.Point(5, 6);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(46, 16);
			this.label2.TabIndex = 3;
			this.label2.Text = "Output";
			// 
			// cbRemoveSpaces
			// 
			this.cbRemoveSpaces.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cbRemoveSpaces.AutoSize = true;
			this.cbRemoveSpaces.Checked = true;
			this.cbRemoveSpaces.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbRemoveSpaces.Location = new System.Drawing.Point(577, 81);
			this.cbRemoveSpaces.Name = "cbRemoveSpaces";
			this.cbRemoveSpaces.Size = new System.Drawing.Size(103, 17);
			this.cbRemoveSpaces.TabIndex = 3;
			this.cbRemoveSpaces.Text = "Remove spaces";
			this.cbRemoveSpaces.UseVisualStyleBackColor = true;
			// 
			// cbCompressIdentifiers
			// 
			this.cbCompressIdentifiers.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cbCompressIdentifiers.AutoSize = true;
			this.cbCompressIdentifiers.Checked = true;
			this.cbCompressIdentifiers.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbCompressIdentifiers.Location = new System.Drawing.Point(577, 58);
			this.cbCompressIdentifiers.Name = "cbCompressIdentifiers";
			this.cbCompressIdentifiers.Size = new System.Drawing.Size(119, 17);
			this.cbCompressIdentifiers.TabIndex = 4;
			this.cbCompressIdentifiers.Text = "Compress identifiers";
			this.cbCompressIdentifiers.UseVisualStyleBackColor = true;
			// 
			// cbRemoveComments
			// 
			this.cbRemoveComments.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cbRemoveComments.AutoSize = true;
			this.cbRemoveComments.Checked = true;
			this.cbRemoveComments.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbRemoveComments.Location = new System.Drawing.Point(577, 12);
			this.cbRemoveComments.Name = "cbRemoveComments";
			this.cbRemoveComments.Size = new System.Drawing.Size(117, 17);
			this.cbRemoveComments.TabIndex = 5;
			this.cbRemoveComments.Text = "Remove comments";
			this.cbRemoveComments.UseVisualStyleBackColor = true;
			// 
			// btnCopyToClipboard
			// 
			this.btnCopyToClipboard.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCopyToClipboard.Location = new System.Drawing.Point(577, 683);
			this.btnCopyToClipboard.Name = "btnCopyToClipboard";
			this.btnCopyToClipboard.Size = new System.Drawing.Size(164, 26);
			this.btnCopyToClipboard.TabIndex = 6;
			this.btnCopyToClipboard.Text = "Copy to clipboard";
			this.btnCopyToClipboard.UseVisualStyleBackColor = true;
			this.btnCopyToClipboard.Click += new System.EventHandler(this.btnCopyToClipboard_Click);
			// 
			// tbLineLength
			// 
			this.tbLineLength.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.tbLineLength.Location = new System.Drawing.Point(657, 104);
			this.tbLineLength.Name = "tbLineLength";
			this.tbLineLength.Size = new System.Drawing.Size(84, 20);
			this.tbLineLength.TabIndex = 7;
			this.tbLineLength.Text = "80";
			this.tbLineLength.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label3
			// 
			this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(577, 107);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(63, 13);
			this.label3.TabIndex = 8;
			this.label3.Text = "Line Length";
			// 
			// ofdInputCodeFiles
			// 
			this.ofdInputCodeFiles.Filter = "C# files|*.cs";
			this.ofdInputCodeFiles.Multiselect = true;
			// 
			// cbMinifyFiles
			// 
			this.cbMinifyFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cbMinifyFiles.AutoSize = true;
			this.cbMinifyFiles.Location = new System.Drawing.Point(580, 134);
			this.cbMinifyFiles.Name = "cbMinifyFiles";
			this.cbMinifyFiles.Size = new System.Drawing.Size(47, 17);
			this.cbMinifyFiles.TabIndex = 9;
			this.cbMinifyFiles.Text = "Files";
			this.cbMinifyFiles.UseVisualStyleBackColor = true;
			// 
			// lblOutputCompilied
			// 
			this.lblOutputCompilied.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.lblOutputCompilied.AutoSize = true;
			this.lblOutputCompilied.Location = new System.Drawing.Point(618, 654);
			this.lblOutputCompilied.Name = "lblOutputCompilied";
			this.lblOutputCompilied.Size = new System.Drawing.Size(0, 13);
			this.lblOutputCompilied.TabIndex = 11;
			// 
			// pbOutputCompilied
			// 
			this.pbOutputCompilied.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.pbOutputCompilied.Location = new System.Drawing.Point(577, 644);
			this.pbOutputCompilied.Name = "pbOutputCompilied";
			this.pbOutputCompilied.Size = new System.Drawing.Size(35, 33);
			this.pbOutputCompilied.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pbOutputCompilied.TabIndex = 10;
			this.pbOutputCompilied.TabStop = false;
			// 
			// lblInputCompilied
			// 
			this.lblInputCompilied.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.lblInputCompilied.AutoSize = true;
			this.lblInputCompilied.Location = new System.Drawing.Point(618, 615);
			this.lblInputCompilied.Name = "lblInputCompilied";
			this.lblInputCompilied.Size = new System.Drawing.Size(0, 13);
			this.lblInputCompilied.TabIndex = 13;
			this.lblInputCompilied.Visible = false;
			// 
			// pbInputCompilied
			// 
			this.pbInputCompilied.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.pbInputCompilied.Location = new System.Drawing.Point(577, 605);
			this.pbInputCompilied.Name = "pbInputCompilied";
			this.pbInputCompilied.Size = new System.Drawing.Size(35, 33);
			this.pbInputCompilied.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pbInputCompilied.TabIndex = 12;
			this.pbInputCompilied.TabStop = false;
			this.pbInputCompilied.Visible = false;
			// 
			// cbRemoveRegions
			// 
			this.cbRemoveRegions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cbRemoveRegions.AutoSize = true;
			this.cbRemoveRegions.Checked = true;
			this.cbRemoveRegions.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbRemoveRegions.Location = new System.Drawing.Point(577, 35);
			this.cbRemoveRegions.Name = "cbRemoveRegions";
			this.cbRemoveRegions.Size = new System.Drawing.Size(103, 17);
			this.cbRemoveRegions.TabIndex = 14;
			this.cbRemoveRegions.Text = "Remove regions";
			this.cbRemoveRegions.UseVisualStyleBackColor = true;
			// 
			// tbInput
			// 
			this.tbInput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tbInput.AutoScrollMinSize = new System.Drawing.Size(0, 1575);
			this.tbInput.BackBrush = null;
			this.tbInput.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.tbInput.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
			this.tbInput.Language = FastColoredTextBoxNS.Language.CSharp;
			this.tbInput.Location = new System.Drawing.Point(8, 33);
			this.tbInput.Name = "tbInput";
			this.tbInput.Paddings = new System.Windows.Forms.Padding(0);
			this.tbInput.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
			this.tbInput.Size = new System.Drawing.Size(420, 303);
			this.tbInput.TabIndex = 10;
			this.tbInput.Text = resources.GetString("tbInput.Text");
			this.tbInput.WordWrap = true;
			// 
			// tbOutput
			// 
			this.tbOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tbOutput.AutoScrollMinSize = new System.Drawing.Size(0, 15);
			this.tbOutput.BackBrush = null;
			this.tbOutput.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.tbOutput.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
			this.tbOutput.Language = FastColoredTextBoxNS.Language.CSharp;
			this.tbOutput.Location = new System.Drawing.Point(8, 25);
			this.tbOutput.Name = "tbOutput";
			this.tbOutput.Paddings = new System.Windows.Forms.Padding(0);
			this.tbOutput.ReadOnly = true;
			this.tbOutput.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
			this.tbOutput.Size = new System.Drawing.Size(546, 324);
			this.tbOutput.TabIndex = 15;
			this.tbOutput.WordWrap = true;
			// 
			// frmMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(753, 721);
			this.Controls.Add(this.cbRemoveRegions);
			this.Controls.Add(this.lblInputCompilied);
			this.Controls.Add(this.pbInputCompilied);
			this.Controls.Add(this.lblOutputCompilied);
			this.Controls.Add(this.pbOutputCompilied);
			this.Controls.Add(this.cbMinifyFiles);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.tbLineLength);
			this.Controls.Add(this.btnCopyToClipboard);
			this.Controls.Add(this.cbRemoveComments);
			this.Controls.Add(this.cbCompressIdentifiers);
			this.Controls.Add(this.cbRemoveSpaces);
			this.Controls.Add(this.splitContainer1);
			this.Controls.Add(this.btnMinify);
			this.Name = "frmMain";
			this.Text = "C# Minifier";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmMain_FormClosed);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel1.PerformLayout();
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.Panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pbOutputCompilied)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pbInputCompilied)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnMinify;
        private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox cbRemoveSpaces;
        private System.Windows.Forms.CheckBox cbCompressIdentifiers;
        private System.Windows.Forms.CheckBox cbRemoveComments;
        private System.Windows.Forms.Button btnCopyToClipboard;
		private System.Windows.Forms.TextBox tbLineLength;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button btnOpenFiles;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.ListBox lbInputFiles;
		private System.Windows.Forms.OpenFileDialog ofdInputCodeFiles;
		private System.Windows.Forms.CheckBox cbMinifyFiles;
		private System.Windows.Forms.PictureBox pbOutputCompilied;
		private System.Windows.Forms.Label lblOutputCompilied;
		private System.Windows.Forms.Label lblInputCompilied;
		private System.Windows.Forms.PictureBox pbInputCompilied;
		private System.Windows.Forms.CheckBox cbRemoveRegions;
		private FastColoredTextBoxNS.FastColoredTextBox tbInput;
		private FastColoredTextBoxNS.FastColoredTextBox tbOutput;
    }
}

