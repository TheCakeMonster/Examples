namespace CleanDotNetSolution
{
	partial class frmMain
	{
		/// <summary>
		///  Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		///  Clean up any resources being used.
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
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.btnSelect = new System.Windows.Forms.Button();
			this.btnExit = new System.Windows.Forms.Button();
			this.lblMainHeading = new System.Windows.Forms.Label();
			this.dlgSelectFolder = new System.Windows.Forms.FolderBrowserDialog();
			this.lblSubHeading = new System.Windows.Forms.Label();
			this.lblPath = new System.Windows.Forms.Label();
			this.txtPath = new System.Windows.Forms.TextBox();
			this.btnClean = new System.Windows.Forms.Button();
			this.lblFeedback = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// btnSelect
			// 
			this.btnSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSelect.Location = new System.Drawing.Point(793, 222);
			this.btnSelect.Name = "btnSelect";
			this.btnSelect.Size = new System.Drawing.Size(34, 29);
			this.btnSelect.TabIndex = 0;
			this.btnSelect.Text = "...";
			this.btnSelect.UseVisualStyleBackColor = true;
			this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
			// 
			// btnExit
			// 
			this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnExit.Location = new System.Drawing.Point(902, 412);
			this.btnExit.Name = "btnExit";
			this.btnExit.Size = new System.Drawing.Size(94, 29);
			this.btnExit.TabIndex = 1;
			this.btnExit.Text = "E&xit";
			this.btnExit.UseVisualStyleBackColor = true;
			this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
			// 
			// lblMainHeading
			// 
			this.lblMainHeading.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lblMainHeading.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
			this.lblMainHeading.Location = new System.Drawing.Point(12, 9);
			this.lblMainHeading.Name = "lblMainHeading";
			this.lblMainHeading.Size = new System.Drawing.Size(984, 55);
			this.lblMainHeading.TabIndex = 2;
			this.lblMainHeading.Text = ".NET Solution Cleaner";
			this.lblMainHeading.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// dlgSelectFolder
			// 
			this.dlgSelectFolder.Description = "Select Start Folder";
			this.dlgSelectFolder.InitialDirectory = "C:\\Users\\Andrew\\source\\repos\\MarimerLLC\\csla\\Source";
			// 
			// lblSubHeading
			// 
			this.lblSubHeading.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lblSubHeading.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.lblSubHeading.Location = new System.Drawing.Point(12, 64);
			this.lblSubHeading.Name = "lblSubHeading";
			this.lblSubHeading.Size = new System.Drawing.Size(984, 40);
			this.lblSubHeading.TabIndex = 3;
			this.lblSubHeading.Text = "Use this solution to recursively delete obj and bin folders";
			this.lblSubHeading.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// lblPath
			// 
			this.lblPath.Location = new System.Drawing.Point(98, 227);
			this.lblPath.Name = "lblPath";
			this.lblPath.Size = new System.Drawing.Size(62, 25);
			this.lblPath.TabIndex = 4;
			this.lblPath.Text = "Path:";
			// 
			// txtPath
			// 
			this.txtPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtPath.Location = new System.Drawing.Point(166, 224);
			this.txtPath.Name = "txtPath";
			this.txtPath.Size = new System.Drawing.Size(621, 27);
			this.txtPath.TabIndex = 5;
			this.txtPath.Text = "C:\\Users\\Andrew\\source\\repos\\MarimerLLC\\csla\\Source";
			// 
			// btnClean
			// 
			this.btnClean.Location = new System.Drawing.Point(166, 257);
			this.btnClean.Name = "btnClean";
			this.btnClean.Size = new System.Drawing.Size(94, 29);
			this.btnClean.TabIndex = 6;
			this.btnClean.Text = "&Clean";
			this.btnClean.UseVisualStyleBackColor = true;
			this.btnClean.Click += new System.EventHandler(this.btnClean_Click);
			// 
			// lblFeedback
			// 
			this.lblFeedback.AutoSize = true;
			this.lblFeedback.Location = new System.Drawing.Point(166, 300);
			this.lblFeedback.Name = "lblFeedback";
			this.lblFeedback.Size = new System.Drawing.Size(138, 20);
			this.lblFeedback.TabIndex = 7;
			this.lblFeedback.Text = "Click \'Clean\' to start";
			// 
			// frmMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1008, 453);
			this.Controls.Add(this.lblFeedback);
			this.Controls.Add(this.btnClean);
			this.Controls.Add(this.txtPath);
			this.Controls.Add(this.lblPath);
			this.Controls.Add(this.lblSubHeading);
			this.Controls.Add(this.lblMainHeading);
			this.Controls.Add(this.btnExit);
			this.Controls.Add(this.btnSelect);
			this.Name = "frmMain";
			this.Text = "Form1";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Button btnSelect;
		private Button btnExit;
		private Label lblMainHeading;
		private FolderBrowserDialog dlgSelectFolder;
		private Label lblSubHeading;
		private Label lblPath;
		private TextBox txtPath;
		private Button btnClean;
		private Label lblFeedback;
	}
}