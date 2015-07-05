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
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.tbInput = new System.Windows.Forms.TextBox();
            this.btnOpenFiles = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.lbInputFiles = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbOutput = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbRemoveSpaces = new System.Windows.Forms.CheckBox();
            this.cbCompressLocalVars = new System.Windows.Forms.CheckBox();
            this.cbRemoveComments = new System.Windows.Forms.CheckBox();
            this.btnCopyToClipboard = new System.Windows.Forms.Button();
            this.tbLineLength = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.ofdInputCodeFiles = new System.Windows.Forms.OpenFileDialog();
            this.cbMinifyFiles = new System.Windows.Forms.CheckBox();
            this.lblOutputCompilied = new System.Windows.Forms.Label();
            this.lblInputCompilied = new System.Windows.Forms.Label();
            this.cbRemoveRegions = new System.Windows.Forms.CheckBox();
            this.cbCompressMisc = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tbInputLength = new System.Windows.Forms.TextBox();
            this.tbOutputLength = new System.Windows.Forms.TextBox();
            this.tbOutputInputRatio = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.pbOutputCompilied = new System.Windows.Forms.PictureBox();
            this.cbRemoveNamespaces = new System.Windows.Forms.CheckBox();
            this.cbConsoleApp = new System.Windows.Forms.CheckBox();
            this.cbRemoveToStringMethods = new System.Windows.Forms.CheckBox();
            this.cbCompressPublic = new System.Windows.Forms.CheckBox();
            this.cbCompressMemebers = new System.Windows.Forms.CheckBox();
            this.cbCompressTypes = new System.Windows.Forms.CheckBox();
            this.dgvErrors = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label8 = new System.Windows.Forms.Label();
            this.cbUselessMembersCompressing = new System.Windows.Forms.CheckBox();
            this.cbEnumToIntConversion = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbOutputCompilied)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvErrors)).BeginInit();
            this.SuspendLayout();
            // 
            // btnMinify
            // 
            this.btnMinify.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMinify.Location = new System.Drawing.Point(665, 386);
            this.btnMinify.Name = "btnMinify";
            this.btnMinify.Size = new System.Drawing.Size(191, 26);
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
            this.splitContainer.Size = new System.Drawing.Size(645, 747);
            this.splitContainer.SplitterDistance = 364;
            this.splitContainer.TabIndex = 2;
            // 
            // tbInput
            // 
            this.tbInput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbInput.Location = new System.Drawing.Point(8, 33);
            this.tbInput.Multiline = true;
            this.tbInput.Name = "tbInput";
            this.tbInput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbInput.Size = new System.Drawing.Size(506, 325);
            this.tbInput.TabIndex = 11;
            this.tbInput.Text = resources.GetString("tbInput.Text");
            this.tbInput.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbInput_KeyDown);
            // 
            // btnOpenFiles
            // 
            this.btnOpenFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpenFiles.Location = new System.Drawing.Point(551, 5);
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
            this.label4.Location = new System.Drawing.Point(517, 10);
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
            this.lbInputFiles.Location = new System.Drawing.Point(520, 33);
            this.lbInputFiles.Name = "lbInputFiles";
            this.lbInputFiles.Size = new System.Drawing.Size(120, 316);
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
            // tbOutput
            // 
            this.tbOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbOutput.Location = new System.Drawing.Point(8, 25);
            this.tbOutput.Multiline = true;
            this.tbOutput.Name = "tbOutput";
            this.tbOutput.ReadOnly = true;
            this.tbOutput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbOutput.Size = new System.Drawing.Size(632, 351);
            this.tbOutput.TabIndex = 12;
            this.tbOutput.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbInput_KeyDown);
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
            this.cbRemoveSpaces.Location = new System.Drawing.Point(663, 86);
            this.cbRemoveSpaces.Name = "cbRemoveSpaces";
            this.cbRemoveSpaces.Size = new System.Drawing.Size(166, 17);
            this.cbRemoveSpaces.TabIndex = 3;
            this.cbRemoveSpaces.Text = "Remove spaces && line breaks";
            this.cbRemoveSpaces.UseVisualStyleBackColor = true;
            // 
            // cbCompressLocalVars
            // 
            this.cbCompressLocalVars.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbCompressLocalVars.AutoSize = true;
            this.cbCompressLocalVars.Checked = true;
            this.cbCompressLocalVars.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbCompressLocalVars.Location = new System.Drawing.Point(663, 17);
            this.cbCompressLocalVars.Name = "cbCompressLocalVars";
            this.cbCompressLocalVars.Size = new System.Drawing.Size(120, 17);
            this.cbCompressLocalVars.TabIndex = 4;
            this.cbCompressLocalVars.Text = "Compress local vars";
            this.cbCompressLocalVars.UseVisualStyleBackColor = true;
            // 
            // cbRemoveComments
            // 
            this.cbRemoveComments.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbRemoveComments.AutoSize = true;
            this.cbRemoveComments.Checked = true;
            this.cbRemoveComments.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbRemoveComments.Location = new System.Drawing.Point(663, 109);
            this.cbRemoveComments.Name = "cbRemoveComments";
            this.cbRemoveComments.Size = new System.Drawing.Size(117, 17);
            this.cbRemoveComments.TabIndex = 5;
            this.cbRemoveComments.Text = "Remove comments";
            this.cbRemoveComments.UseVisualStyleBackColor = true;
            // 
            // btnCopyToClipboard
            // 
            this.btnCopyToClipboard.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCopyToClipboard.Location = new System.Drawing.Point(665, 732);
            this.btnCopyToClipboard.Name = "btnCopyToClipboard";
            this.btnCopyToClipboard.Size = new System.Drawing.Size(189, 26);
            this.btnCopyToClipboard.TabIndex = 6;
            this.btnCopyToClipboard.Text = "Copy to clipboard";
            this.btnCopyToClipboard.UseVisualStyleBackColor = true;
            this.btnCopyToClipboard.Click += new System.EventHandler(this.btnCopyToClipboard_Click);
            // 
            // tbLineLength
            // 
            this.tbLineLength.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tbLineLength.Location = new System.Drawing.Point(745, 335);
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
            this.label3.Location = new System.Drawing.Point(665, 338);
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
            this.cbMinifyFiles.Location = new System.Drawing.Point(668, 363);
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
            this.lblOutputCompilied.Location = new System.Drawing.Point(706, 703);
            this.lblOutputCompilied.Name = "lblOutputCompilied";
            this.lblOutputCompilied.Size = new System.Drawing.Size(0, 13);
            this.lblOutputCompilied.TabIndex = 11;
            // 
            // lblInputCompilied
            // 
            this.lblInputCompilied.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblInputCompilied.AutoSize = true;
            this.lblInputCompilied.Location = new System.Drawing.Point(706, 703);
            this.lblInputCompilied.Name = "lblInputCompilied";
            this.lblInputCompilied.Size = new System.Drawing.Size(0, 13);
            this.lblInputCompilied.TabIndex = 13;
            this.lblInputCompilied.Visible = false;
            // 
            // cbRemoveRegions
            // 
            this.cbRemoveRegions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbRemoveRegions.AutoSize = true;
            this.cbRemoveRegions.Checked = true;
            this.cbRemoveRegions.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbRemoveRegions.Location = new System.Drawing.Point(663, 132);
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
            this.cbCompressMisc.Location = new System.Drawing.Point(663, 155);
            this.cbCompressMisc.Name = "cbCompressMisc";
            this.cbCompressMisc.Size = new System.Drawing.Size(97, 17);
            this.cbCompressMisc.TabIndex = 15;
            this.cbCompressMisc.Text = "Compress Misc";
            this.cbCompressMisc.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(664, 614);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 13);
            this.label5.TabIndex = 16;
            this.label5.Text = "Input Length";
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(664, 641);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(75, 13);
            this.label6.TabIndex = 17;
            this.label6.Text = "Output Length";
            // 
            // tbInputLength
            // 
            this.tbInputLength.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.tbInputLength.Location = new System.Drawing.Point(756, 611);
            this.tbInputLength.Name = "tbInputLength";
            this.tbInputLength.ReadOnly = true;
            this.tbInputLength.Size = new System.Drawing.Size(72, 20);
            this.tbInputLength.TabIndex = 18;
            // 
            // tbOutputLength
            // 
            this.tbOutputLength.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.tbOutputLength.Location = new System.Drawing.Point(756, 640);
            this.tbOutputLength.Name = "tbOutputLength";
            this.tbOutputLength.ReadOnly = true;
            this.tbOutputLength.Size = new System.Drawing.Size(72, 20);
            this.tbOutputLength.TabIndex = 19;
            // 
            // tbOutputInputRatio
            // 
            this.tbOutputInputRatio.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.tbOutputInputRatio.Location = new System.Drawing.Point(756, 666);
            this.tbOutputInputRatio.Name = "tbOutputInputRatio";
            this.tbOutputInputRatio.ReadOnly = true;
            this.tbOutputInputRatio.Size = new System.Drawing.Size(72, 20);
            this.tbOutputInputRatio.TabIndex = 21;
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(664, 669);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(32, 13);
            this.label7.TabIndex = 20;
            this.label7.Text = "Ratio";
            // 
            // pbOutputCompilied
            // 
            this.pbOutputCompilied.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pbOutputCompilied.Location = new System.Drawing.Point(668, 693);
            this.pbOutputCompilied.Name = "pbOutputCompilied";
            this.pbOutputCompilied.Size = new System.Drawing.Size(35, 33);
            this.pbOutputCompilied.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbOutputCompilied.TabIndex = 10;
            this.pbOutputCompilied.TabStop = false;
            // 
            // cbRemoveNamespaces
            // 
            this.cbRemoveNamespaces.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbRemoveNamespaces.AutoSize = true;
            this.cbRemoveNamespaces.Checked = true;
            this.cbRemoveNamespaces.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbRemoveNamespaces.Location = new System.Drawing.Point(663, 201);
            this.cbRemoveNamespaces.Name = "cbRemoveNamespaces";
            this.cbRemoveNamespaces.Size = new System.Drawing.Size(131, 17);
            this.cbRemoveNamespaces.TabIndex = 22;
            this.cbRemoveNamespaces.Text = "Remove Namespaces";
            this.cbRemoveNamespaces.UseVisualStyleBackColor = true;
            // 
            // cbConsoleApp
            // 
            this.cbConsoleApp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbConsoleApp.AutoSize = true;
            this.cbConsoleApp.Checked = true;
            this.cbConsoleApp.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbConsoleApp.Location = new System.Drawing.Point(663, 178);
            this.cbConsoleApp.Name = "cbConsoleApp";
            this.cbConsoleApp.Size = new System.Drawing.Size(86, 17);
            this.cbConsoleApp.TabIndex = 23;
            this.cbConsoleApp.Text = "Console App";
            this.cbConsoleApp.UseVisualStyleBackColor = true;
            // 
            // cbRemoveToStringMethods
            // 
            this.cbRemoveToStringMethods.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbRemoveToStringMethods.AutoSize = true;
            this.cbRemoveToStringMethods.Checked = true;
            this.cbRemoveToStringMethods.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbRemoveToStringMethods.Location = new System.Drawing.Point(663, 247);
            this.cbRemoveToStringMethods.Name = "cbRemoveToStringMethods";
            this.cbRemoveToStringMethods.Size = new System.Drawing.Size(115, 17);
            this.cbRemoveToStringMethods.TabIndex = 24;
            this.cbRemoveToStringMethods.Text = "Remove ToString()";
            this.cbRemoveToStringMethods.UseVisualStyleBackColor = true;
            // 
            // cbCompressPublic
            // 
            this.cbCompressPublic.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbCompressPublic.AutoSize = true;
            this.cbCompressPublic.Checked = true;
            this.cbCompressPublic.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbCompressPublic.Location = new System.Drawing.Point(663, 224);
            this.cbCompressPublic.Name = "cbCompressPublic";
            this.cbCompressPublic.Size = new System.Drawing.Size(104, 17);
            this.cbCompressPublic.TabIndex = 25;
            this.cbCompressPublic.Text = "Compress Public";
            this.cbCompressPublic.UseVisualStyleBackColor = true;
            // 
            // cbCompressMemebers
            // 
            this.cbCompressMemebers.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbCompressMemebers.AutoSize = true;
            this.cbCompressMemebers.Checked = true;
            this.cbCompressMemebers.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbCompressMemebers.Location = new System.Drawing.Point(663, 40);
            this.cbCompressMemebers.Name = "cbCompressMemebers";
            this.cbCompressMemebers.Size = new System.Drawing.Size(123, 17);
            this.cbCompressMemebers.TabIndex = 26;
            this.cbCompressMemebers.Text = "Compress memebers";
            this.cbCompressMemebers.UseVisualStyleBackColor = true;
            // 
            // cbCompressTypes
            // 
            this.cbCompressTypes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbCompressTypes.AutoSize = true;
            this.cbCompressTypes.Checked = true;
            this.cbCompressTypes.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbCompressTypes.Location = new System.Drawing.Point(663, 63);
            this.cbCompressTypes.Name = "cbCompressTypes";
            this.cbCompressTypes.Size = new System.Drawing.Size(100, 17);
            this.cbCompressTypes.TabIndex = 27;
            this.cbCompressTypes.Text = "Compress types";
            this.cbCompressTypes.UseVisualStyleBackColor = true;
            // 
            // dgvErrors
            // 
            this.dgvErrors.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvErrors.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvErrors.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4});
            this.dgvErrors.Location = new System.Drawing.Point(665, 480);
            this.dgvErrors.Name = "dgvErrors";
            this.dgvErrors.ReadOnly = true;
            this.dgvErrors.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvErrors.Size = new System.Drawing.Size(189, 111);
            this.dgvErrors.TabIndex = 28;
            this.dgvErrors.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvErrors_CellMouseDoubleClick);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Line";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 52;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Row";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 54;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Description";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 85;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Field";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Width = 54;
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(664, 459);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(34, 13);
            this.label8.TabIndex = 29;
            this.label8.Text = "Errors";
            // 
            // cbUselessMembersCompressing
            // 
            this.cbUselessMembersCompressing.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbUselessMembersCompressing.AutoSize = true;
            this.cbUselessMembersCompressing.Checked = true;
            this.cbUselessMembersCompressing.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbUselessMembersCompressing.Location = new System.Drawing.Point(663, 270);
            this.cbUselessMembersCompressing.Name = "cbUselessMembersCompressing";
            this.cbUselessMembersCompressing.Size = new System.Drawing.Size(172, 17);
            this.cbUselessMembersCompressing.TabIndex = 30;
            this.cbUselessMembersCompressing.Text = "Useless Members Compressing";
            this.cbUselessMembersCompressing.UseVisualStyleBackColor = true;
            // 
            // cbEnumToIntConversion
            // 
            this.cbEnumToIntConversion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbEnumToIntConversion.AutoSize = true;
            this.cbEnumToIntConversion.Checked = true;
            this.cbEnumToIntConversion.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbEnumToIntConversion.Location = new System.Drawing.Point(663, 293);
            this.cbEnumToIntConversion.Name = "cbEnumToIntConversion";
            this.cbEnumToIntConversion.Size = new System.Drawing.Size(140, 17);
            this.cbEnumToIntConversion.TabIndex = 31;
            this.cbEnumToIntConversion.Text = "Enum To Int Conversion";
            this.cbEnumToIntConversion.UseVisualStyleBackColor = true;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(866, 771);
            this.Controls.Add(this.cbEnumToIntConversion);
            this.Controls.Add(this.cbUselessMembersCompressing);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.dgvErrors);
            this.Controls.Add(this.cbCompressTypes);
            this.Controls.Add(this.cbCompressMemebers);
            this.Controls.Add(this.cbCompressPublic);
            this.Controls.Add(this.cbRemoveToStringMethods);
            this.Controls.Add(this.cbConsoleApp);
            this.Controls.Add(this.cbRemoveNamespaces);
            this.Controls.Add(this.tbOutputInputRatio);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.tbOutputLength);
            this.Controls.Add(this.tbInputLength);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cbCompressMisc);
            this.Controls.Add(this.cbRemoveRegions);
            this.Controls.Add(this.lblInputCompilied);
            this.Controls.Add(this.lblOutputCompilied);
            this.Controls.Add(this.pbOutputCompilied);
            this.Controls.Add(this.cbMinifyFiles);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbLineLength);
            this.Controls.Add(this.btnCopyToClipboard);
            this.Controls.Add(this.cbRemoveComments);
            this.Controls.Add(this.cbCompressLocalVars);
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
            ((System.ComponentModel.ISupportInitialize)(this.dgvErrors)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnMinify;
        private System.Windows.Forms.SplitContainer splitContainer;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox cbRemoveSpaces;
        private System.Windows.Forms.CheckBox cbCompressLocalVars;
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
		private System.Windows.Forms.CheckBox cbRemoveRegions;
        private System.Windows.Forms.CheckBox cbCompressMisc;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox tbInputLength;
		private System.Windows.Forms.TextBox tbOutputLength;
		private System.Windows.Forms.TextBox tbOutputInputRatio;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.CheckBox cbRemoveNamespaces;
		private System.Windows.Forms.CheckBox cbConsoleApp;
		private System.Windows.Forms.CheckBox cbRemoveToStringMethods;
		private System.Windows.Forms.CheckBox cbCompressPublic;
		private System.Windows.Forms.CheckBox cbCompressMemebers;
		private System.Windows.Forms.CheckBox cbCompressTypes;
		private System.Windows.Forms.DataGridView dgvErrors;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
		private System.Windows.Forms.CheckBox cbUselessMembersCompressing;
        private System.Windows.Forms.TextBox tbInput;
        private System.Windows.Forms.TextBox tbOutput;
        private System.Windows.Forms.CheckBox cbEnumToIntConversion;
    }
}

