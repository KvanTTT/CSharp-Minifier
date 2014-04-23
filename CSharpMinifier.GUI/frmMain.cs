using CSharpMinifier.GUI.Properties;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
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

			if (!Settings.Default.WindowLocation.IsEmpty)
				Location = Settings.Default.WindowLocation;
			if (!Settings.Default.WindowSize.IsEmpty)
				Size = Settings.Default.WindowSize;
			WindowState = (FormWindowState)Enum.Parse(typeof(FormWindowState), Settings.Default.WindowState);
			if (Settings.Default.InputPanelHeight != 0)
				splitContainer.SplitterDistance = Settings.Default.InputPanelHeight;
			cbRemoveComments.Checked = Settings.Default.RemoveComments;
			cbRemoveRegions.Checked = Settings.Default.RemoveRegions;
			cbCompressIdentifiers.Checked = Settings.Default.CompressIdentifiers;
			cbRemoveSpaces.Checked = Settings.Default.RemoveSpaces;
			cbCompressMisc.Checked = Settings.Default.MiscCompressing;
			cbRemoveNamespaces.Checked = Settings.Default.RemoveNamespaces;
			cbConsoleApp.Checked = Settings.Default.ConsoleApp;
			tbLineLength.Text = Settings.Default.LineLength.ToString();
			cbMinifyFiles.Checked = Settings.Default.MinifyFiles;
			if (Settings.Default.FileList != null)
				FillList(Settings.Default.FileList.Cast<string>().ToArray());
			cbCompressPublic.Checked = Settings.Default.CompressPublic;
			cbRemoveToStringMethods.Checked = Settings.Default.RemoveToStringMethods;
		}

		private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
		{
			Settings.Default.WindowLocation = Location;
			Settings.Default.WindowSize = Size;
			Settings.Default.WindowState = WindowState.ToString();
			Settings.Default.InputPanelHeight = splitContainer.Panel1.Height;
			Settings.Default.RemoveComments = cbRemoveComments.Checked;
			Settings.Default.RemoveRegions = cbRemoveRegions.Checked;
			Settings.Default.CompressIdentifiers = cbCompressIdentifiers.Checked;
			Settings.Default.RemoveSpaces = cbRemoveSpaces.Checked;
			Settings.Default.MiscCompressing = cbCompressMisc.Checked;
			Settings.Default.ConsoleApp = cbConsoleApp.Checked;
			Settings.Default.RemoveNamespaces = cbRemoveNamespaces.Checked;
			Settings.Default.LineLength = int.Parse(tbLineLength.Text);
			Settings.Default.MinifyFiles = cbMinifyFiles.Checked;
			var stringCollection = new StringCollection();
			foreach (var fileName in Sources)
				stringCollection.Add(fileName.Key);
			Settings.Default.FileList = stringCollection;
			Settings.Default.CompressPublic = cbCompressPublic.Checked;
			Settings.Default.RemoveToStringMethods = cbRemoveToStringMethods.Checked;
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
				ConsoleApp = cbConsoleApp.Checked,
				RemoveNamespaces = cbRemoveNamespaces.Checked,
				LineLength = int.Parse(tbLineLength.Text),
				RemoveToStringMethods = cbRemoveToStringMethods.Checked,
				CompressPublic = cbCompressPublic.Checked
			};
			Minifier minifier = new Minifier(minifierOptions);
			tbOutput.Text = !cbMinifyFiles.Checked ? minifier.MinifyFromString(tbInput.Text) : minifier.MinifyFiles(Sources.Select(source => source.Value).ToArray());

			tbInputLength.Text = tbInput.Text.Length.ToString();
			tbOutputLength.Text = tbOutput.Text.Length.ToString();
			tbOutputInputRatio.Text = ((double)tbOutput.Text.Length / tbInput.Text.Length).ToString("0.000000");
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

		private void tbInput_Load(object sender, EventArgs e)
		{

		}

		private void checkBox2_CheckedChanged(object sender, EventArgs e)
		{

		}
	}
}