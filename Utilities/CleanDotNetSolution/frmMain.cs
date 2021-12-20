namespace CleanDotNetSolution
{
	public partial class frmMain : Form
	{
		public frmMain()
		{
			InitializeComponent();
		}

		private void btnSelect_Click(object sender, EventArgs e)
		{
			dlgSelectFolder.InitialDirectory = txtPath.Text;
			dlgSelectFolder.ShowDialog();
			txtPath.Text = dlgSelectFolder.SelectedPath;
		}

		private void btnClean_Click(object sender, EventArgs e)
		{
			string? startDirectory;

			try
			{
				// Calculate start point
				startDirectory = Path.GetDirectoryName(txtPath.Text);
				if (startDirectory is null || !Directory.Exists(startDirectory)) return;

				// Show that we are working
				this.Cursor = Cursors.WaitCursor;
				lblFeedback.Text = $"Cleaning {startDirectory} ...";
				this.Refresh();

				// Perform the clean operation
				CleanDirectory(startDirectory);
			}
			catch (Exception)
			{
				throw;
			}
			finally
			{
				// Update the user to let them know we have finished
				lblFeedback.Text = "Cleaning completed.";
				this.Cursor = Cursors.Default;
			}
		}

		private void btnExit_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		#region Private Helper Methods

		private void CleanDirectory(string directoryName)
		{
			string[] subdirectoryNames;

			subdirectoryNames = Directory.GetDirectories(directoryName);
			foreach (string subdirectoryName in subdirectoryNames)
			{
				// Delete obj and bin directories
				if (subdirectoryName.EndsWith(@"\obj", StringComparison.InvariantCultureIgnoreCase))
				{
					Directory.Delete(subdirectoryName, true);
					continue;
				}
				if (subdirectoryName.EndsWith(@"\bin", StringComparison.InvariantCultureIgnoreCase))
				{
					Directory.Delete(subdirectoryName, true);
					continue;
				}
				// Recurse any other directory
				CleanDirectory(subdirectoryName);
			}
		}

		#endregion

	}
}