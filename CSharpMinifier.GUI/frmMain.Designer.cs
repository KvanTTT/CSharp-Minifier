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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
			this.btnMinify = new System.Windows.Forms.Button();
			this.splitContainer = new System.Windows.Forms.SplitContainer();
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
			this.cbCompressMisc = new System.Windows.Forms.CheckBox();
			this.tbInput = new FastColoredTextBoxNS.FastColoredTextBox();
			this.tbOutput = new FastColoredTextBoxNS.FastColoredTextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.tbInputLength = new System.Windows.Forms.TextBox();
			this.tbOutputLength = new System.Windows.Forms.TextBox();
			this.tbOutputInputRatio = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
			this.splitContainer.Panel1.SuspendLayout();
			this.splitContainer.Panel2.SuspendLayout();
			this.splitContainer.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pbOutputCompilied)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pbInputCompilied)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.tbInput)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.tbOutput)).BeginInit();
			this.SuspendLayout();
			// 
			// btnMinify
			// 
			this.btnMinify.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnMinify.Location = new System.Drawing.Point(577, 190);
			this.btnMinify.Name = "btnMinify";
			this.btnMinify.Size = new System.Drawing.Size(164, 26);
			this.btnMinify.TabIndex = 1;
			this.btnMinify.Text = "Minify";
			this.btnMinify.UseVisualStyleBackColor = true;
			this.btnMinify.Click += new System.EventHandler(this.btnMinify_Click);
			// 
			// splitContainer
			// 
			this.splitContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.splitContainer.Location = new System.Drawing.Point(12, 12);
			this.splitContainer.Name = "splitContainer";
			this.splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer.Panel1
			// 
			this.splitContainer.Panel1.Controls.Add(this.tbInput);
			this.splitContainer.Panel1.Controls.Add(this.btnOpenFiles);
			this.splitContainer.Panel1.Controls.Add(this.label4);
			this.splitContainer.Panel1.Controls.Add(this.lbInputFiles);
			this.splitContainer.Panel1.Controls.Add(this.label1);
			// 
			// splitContainer.Panel2
			// 
			this.splitContainer.Panel2.Controls.Add(this.tbOutput);
			this.splitContainer.Panel2.Controls.Add(this.label2);
			this.splitContainer.Size = new System.Drawing.Size(559, 697);
			this.splitContainer.SplitterDistance = 341;
			this.splitContainer.TabIndex = 2;
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
			this.tbLineLength.Location = new System.Drawing.Point(657, 137);
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
			this.label3.Location = new System.Drawing.Point(577, 140);
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
			this.cbMinifyFiles.Location = new System.Drawing.Point(580, 167);
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
			// cbCompressMisc
			// 
			this.cbCompressMisc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cbCompressMisc.AutoSize = true;
			this.cbCompressMisc.Checked = true;
			this.cbCompressMisc.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbCompressMisc.Location = new System.Drawing.Point(577, 104);
			this.cbCompressMisc.Name = "cbCompressMisc";
			this.cbCompressMisc.Size = new System.Drawing.Size(97, 17);
			this.cbCompressMisc.TabIndex = 15;
			this.cbCompressMisc.Text = "Compress Misc";
			this.cbCompressMisc.UseVisualStyleBackColor = true;
			// 
			// tbInput
			// 
			this.tbInput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tbInput.AutoScrollMinSize = new System.Drawing.Size(395, 994);
			this.tbInput.BackBrush = null;
			this.tbInput.CharHeight = 14;
			this.tbInput.CharWidth = 8;
			this.tbInput.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.tbInput.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
			this.tbInput.IsReplaceMode = false;
			this.tbInput.Language = FastColoredTextBoxNS.Language.CSharp;
			this.tbInput.LeftBracket = '(';
			this.tbInput.Location = new System.Drawing.Point(8, 33);
			this.tbInput.Name = "tbInput";
			this.tbInput.Paddings = new System.Windows.Forms.Padding(0);
			this.tbInput.RightBracket = ')';
			this.tbInput.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
			this.tbInput.Size = new System.Drawing.Size(420, 303);
			this.tbInput.TabIndex = 10;
			this.tbInput.Text = resources.GetString("tbInput.Text");
			this.tbInput.Zoom = 100;
			// 
			// tbOutput
			// 
			this.tbOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tbOutput.AutoScrollMinSize = new System.Drawing.Size(0, 14);
			this.tbOutput.BackBrush = null;
			this.tbOutput.CharHeight = 14;
			this.tbOutput.CharWidth = 8;
			this.tbOutput.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.tbOutput.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
			this.tbOutput.IsReplaceMode = false;
			this.tbOutput.Language = FastColoredTextBoxNS.Language.CSharp;
			this.tbOutput.LeftBracket = '(';
			this.tbOutput.Location = new System.Drawing.Point(8, 25);
			this.tbOutput.Name = "tbOutput";
			this.tbOutput.Paddings = new System.Windows.Forms.Padding(0);
			this.tbOutput.ReadOnly = true;
			this.tbOutput.RightBracket = ')';
			this.tbOutput.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
			this.tbOutput.Size = new System.Drawing.Size(546, 324);
			this.tbOutput.TabIndex = 11;
			this.tbOutput.WordWrap = true;
			this.tbOutput.Zoom = 100;
			// 
			// label5
			// 
			this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(577, 508);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(67, 13);
			this.label5.TabIndex = 16;
			this.label5.Text = "Input Length";
			// 
			// label6
			// 
			this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(577, 537);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(75, 13);
			this.label6.TabIndex = 17;
			this.label6.Text = "Output Length";
			// 
			// tbInputLength
			// 
			this.tbInputLength.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.tbInputLength.Location = new System.Drawing.Point(669, 505);
			this.tbInputLength.Name = "tbInputLength";
			this.tbInputLength.ReadOnly = true;
			this.tbInputLength.Size = new System.Drawing.Size(72, 20);
			this.tbInputLength.TabIndex = 18;
			// 
			// tbOutputLength
			// 
			this.tbOutputLength.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.tbOutputLength.Location = new System.Drawing.Point(669, 534);
			this.tbOutputLength.Name = "tbOutputLength";
			this.tbOutputLength.ReadOnly = true;
			this.tbOutputLength.Size = new System.Drawing.Size(72, 20);
			this.tbOutputLength.TabIndex = 19;
			// 
			// tbOutputInputRatio
			// 
			this.tbOutputInputRatio.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.tbOutputInputRatio.Location = new System.Drawing.Point(669, 560);
			this.tbOutputInputRatio.Name = "tbOutputInputRatio";
			this.tbOutputInputRatio.ReadOnly = true;
			this.tbOutputInputRatio.Size = new System.Drawing.Size(72, 20);
			this.tbOutputInputRatio.TabIndex = 21;
			// 
			// label7
			// 
			this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(577, 563);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(32, 13);
			this.label7.TabIndex = 20;
			this.label7.Text = "Ratio";
			// 
			// frmMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(753, 721);
			this.Controls.Add(this.tbOutputInputRatio);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.tbOutputLength);
			this.Controls.Add(this.tbInputLength);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.cbCompressMisc);
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
			this.Controls.Add(this.splitContainer);
			this.Controls.Add(this.btnMinify);
			this.Name = "frmMain";
			this.Text = "C# Minifier";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmMain_FormClosed);
			this.splitContainer.Panel1.ResumeLayout(false);
			this.splitContainer.Panel1.PerformLayout();
			this.splitContainer.Panel2.ResumeLayout(false);
			this.splitContainer.Panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
			this.splitContainer.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pbOutputCompilied)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pbInputCompilied)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.tbInput)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.tbOutput)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnMinify;
        private System.Windows.Forms.SplitContainer splitContainer;
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
		private System.Windows.Forms.CheckBox cbCompressMisc;
		private FastColoredTextBoxNS.FastColoredTextBox tbInput;
		private FastColoredTextBoxNS.FastColoredTextBox tbOutput;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox tbInputLength;
		private System.Windows.Forms.TextBox tbOutputLength;
		private System.Windows.Forms.TextBox tbOutputInputRatio;
		private System.Windows.Forms.Label label7;
    }
}

