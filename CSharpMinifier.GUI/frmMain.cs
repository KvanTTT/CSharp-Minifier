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

		Dictionary<string, string> Sources = new Dictionary<string, string>();

		public frmMain()
		{
			InitializeComponent();

			cbRemoveComments.Checked = Settings.Default.RemoveComments;
			cbRemoveRegions.Checked = Settings.Default.RemoveRegions;
			cbCompressIdentifiers.Checked = Settings.Default.CompressIdentifiers;
			cbRemoveSpaces.Checked = Settings.Default.RemoveSpaces;
			cbCompressMisc.Checked = Settings.Default.MiscCompressing;
			tbLineLength.Text = Settings.Default.LineLength.ToString();
			cbMinifyFiles.Checked = Settings.Default.MinifyFiles;
			if (Settings.Default.FileList != null)
				FillList(Settings.Default.FileList.Cast<string>().ToArray());
		}

		private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
		{
			Settings.Default.RemoveComments = cbRemoveComments.Checked;
			Settings.Default.RemoveRegions = cbRemoveRegions.Checked;
			Settings.Default.CompressIdentifiers = cbCompressIdentifiers.Checked;
			Settings.Default.RemoveSpaces = cbRemoveSpaces.Checked;
			Settings.Default.MiscCompressing = cbCompressMisc.Checked;
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
			var minifierOptions = new MinifierOptions
			{
				IdentifiersCompressing = cbCompressIdentifiers.Checked,
				SpacesRemoving = cbRemoveSpaces.Checked,
				RegionsRemoving = cbRemoveRegions.Checked,
				CommentsRemoving = cbRemoveComments.Checked,
				MiscCompressing = cbCompressMisc.Checked,
				LineLength = int.Parse(tbLineLength.Text),
			};
			Minifier minifier = new Minifier(minifierOptions);
			tbOutput.Text = !cbMinifyFiles.Checked ? minifier.MinifyFromString(tbInput.Text) : minifier.MinifyFiles(Sources.Select(source => source.Value).ToArray());

			if (CompileUtils.CanCompile(tbOutput.Text))
			{
				pbOutputCompilied.Image = Resources.Ok;
				lblOutputCompilied.Text = "Compilied";
			}
			else
			{
				pbOutputCompilied.Image = Resources.Error;
				lblOutputCompilied.Text = "Not compilied";
			}
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
	}
}