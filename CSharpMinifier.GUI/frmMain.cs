using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSharpMinifier.GUI
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void btnMinify_Click(object sender, EventArgs e)
        {
            Minifier minifier = new Minifier();
            tbOutput.Text = minifier.MinifyFromString(tbInput.Text,
                cbCompressIdentifiers.Checked, cbRemoveSpaces.Checked, cbRemoveComments.Checked);
        }

        private void btnCopyToClipboard_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tbOutput.Text))
                Clipboard.SetText(tbOutput.Text);
        }
    }
}
