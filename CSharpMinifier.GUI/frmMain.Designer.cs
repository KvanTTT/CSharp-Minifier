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
            this.label1 = new System.Windows.Forms.Label();
            this.tbInput = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbOutput = new System.Windows.Forms.TextBox();
            this.cbRemoveSpaces = new System.Windows.Forms.CheckBox();
            this.cbCompressIdentifiers = new System.Windows.Forms.CheckBox();
            this.cbRemoveComments = new System.Windows.Forms.CheckBox();
            this.btnCopyToClipboard = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnMinify
            // 
            this.btnMinify.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMinify.Location = new System.Drawing.Point(576, 81);
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
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.tbInput);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Panel2.Controls.Add(this.tbOutput);
            this.splitContainer1.Size = new System.Drawing.Size(558, 689);
            this.splitContainer1.SplitterDistance = 339;
            this.splitContainer1.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Input";
            // 
            // tbInput
            // 
            this.tbInput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbInput.Location = new System.Drawing.Point(3, 20);
            this.tbInput.Multiline = true;
            this.tbInput.Name = "tbInput";
            this.tbInput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbInput.Size = new System.Drawing.Size(552, 316);
            this.tbInput.TabIndex = 1;
            this.tbInput.Text = resources.GetString("tbInput.Text");
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Output";
            // 
            // tbOutput
            // 
            this.tbOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbOutput.Location = new System.Drawing.Point(3, 23);
            this.tbOutput.Multiline = true;
            this.tbOutput.Name = "tbOutput";
            this.tbOutput.ReadOnly = true;
            this.tbOutput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbOutput.Size = new System.Drawing.Size(552, 320);
            this.tbOutput.TabIndex = 2;
            // 
            // cbRemoveSpaces
            // 
            this.cbRemoveSpaces.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbRemoveSpaces.AutoSize = true;
            this.cbRemoveSpaces.Checked = true;
            this.cbRemoveSpaces.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbRemoveSpaces.Location = new System.Drawing.Point(576, 58);
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
            this.cbCompressIdentifiers.Location = new System.Drawing.Point(576, 35);
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
            this.cbRemoveComments.Location = new System.Drawing.Point(576, 12);
            this.cbRemoveComments.Name = "cbRemoveComments";
            this.cbRemoveComments.Size = new System.Drawing.Size(117, 17);
            this.cbRemoveComments.TabIndex = 5;
            this.cbRemoveComments.Text = "Remove comments";
            this.cbRemoveComments.UseVisualStyleBackColor = true;
            // 
            // btnCopyToClipboard
            // 
            this.btnCopyToClipboard.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCopyToClipboard.Location = new System.Drawing.Point(576, 675);
            this.btnCopyToClipboard.Name = "btnCopyToClipboard";
            this.btnCopyToClipboard.Size = new System.Drawing.Size(164, 26);
            this.btnCopyToClipboard.TabIndex = 6;
            this.btnCopyToClipboard.Text = "Copy to clipboard";
            this.btnCopyToClipboard.UseVisualStyleBackColor = true;
            this.btnCopyToClipboard.Click += new System.EventHandler(this.btnCopyToClipboard_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(752, 713);
            this.Controls.Add(this.btnCopyToClipboard);
            this.Controls.Add(this.cbRemoveComments);
            this.Controls.Add(this.cbCompressIdentifiers);
            this.Controls.Add(this.cbRemoveSpaces);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.btnMinify);
            this.Name = "Form1";
            this.Text = "C# Minifier";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnMinify;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbInput;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbOutput;
        private System.Windows.Forms.CheckBox cbRemoveSpaces;
        private System.Windows.Forms.CheckBox cbCompressIdentifiers;
        private System.Windows.Forms.CheckBox cbRemoveComments;
        private System.Windows.Forms.Button btnCopyToClipboard;
    }
}

