using CSharpMinifier.GUI.Properties;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSharpMinifier.GUI
{
    public partial class frmMain : Form
    {
		class ListBoxItem
		{
			public string Key { get; set; }
			public string Value { get; set; }

			public ListBoxItem(string key, string value)
			{
				Key = key;
				Value = value;
			}

			public override string ToString()
			{
				return Value;
			}
		}

		void FillList(string[] fileNames)
		{
			Sources = new Dictionary<string, string>();
			foreach (var fileName in fileNames)
			{
				var lbItem = new ListBoxItem(fileName, Path.GetFileName(fileName));
				Sources[lbItem.Key] = File.ReadAllText(lbItem.Key);
				lbInputFiles.Items.Add(lbItem);
			}
		}

		Dictionary<string, string> Sources = new Dictionary<string,string>();

        public frmMain()
        {
            InitializeComponent();

			cbRemoveComments.Checked = Settings.Default.RemoveComments;
			cbCompressIdentifiers.Checked = Settings.Default.CompressIdentifiers;
			cbRemoveSpaces.Checked = Settings.Default.RemoveSpaces;
			tbLineLength.Text = Settings.Default.LineLength.ToString();
			cbMinifyFiles.Checked = Settings.Default.MinifyFiles;
			if (Settings.Default.FileList != null)
				FillList(Settings.Default.FileList.Cast<string>().ToArray());
        }

		private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
		{
			Settings.Default.RemoveComments = cbRemoveComments.Checked;
			Settings.Default.CompressIdentifiers = cbCompressIdentifiers.Checked;
			Settings.Default.RemoveSpaces = cbRemoveSpaces.Checked;
			Settings.Default.LineLength = int.Parse(tbLineLength.Text);
			Settings.Default.MinifyFiles = cbMinifyFiles.Checked;
			var stringCollection = new StringCollection();
			foreach (var fileName in Sources)
				stringCollection.Add(fileName.Key);
			Settings.Default.FileList = stringCollection;
			Settings.Default.Save();
		}

        private void btnMinify_Click(object sender, EventArgs e)
        {
            Minifier minifier = new Minifier(cbCompressIdentifiers.Checked, cbRemoveSpaces.Checked, cbRemoveSpaces.Checked, int.Parse(tbLineLength.Text));
			tbOutput.Text = !cbMinifyFiles.Checked ? minifier.MinifyFromString(tbInput.Text) : minifier.MinifyFiles(Sources.Select(source => source.Value).ToArray());
        }

        private void btnCopyToClipboard_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tbOutput.Text))
                Clipboard.SetText(tbOutput.Text);
        }

		private void btnOpenFiles_Click(object sender, EventArgs e)
		{
			if (ofdInputCodeFiles.ShowDialog() == DialogResult.OK)
			{
				FillList(ofdInputCodeFiles.FileNames);
			}
		}

		private void lbInputFiles_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				if (lbInputFiles.SelectedIndex != -1)
				{
					tbInput.Text = Sources[((ListBoxItem)lbInputFiles.SelectedItem).Key];
				}
			}
			catch
			{
			}
		}

		private void tbInput_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Control && e.KeyCode == Keys.A)
			{
				((TextBox)sender).SelectAll();
				e.SuppressKeyPress = true;
			}
		}
    }
}
