using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Resources;
using System.Windows.Forms;

namespace SpawnEditor2
{
	public class AboutForm : Form
	{
		private PictureBox pictureBox1;

		private Label label1;

		private Label label3;

		private Label label4;

		private Label label5;

		private Label label6;

		private Label label7;

		private Button button1;

		private Label label8;

		private Label labelVersion;

		private LinkLabel linkDonation;

		private System.ComponentModel.Container components = null;

		private SpawnEditor _Editor;

		public AboutForm(SpawnEditor editor)
		{
			this._Editor = editor;
			this.InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			ResourceManager resourceManager = new ResourceManager(typeof(AboutForm));
			this.pictureBox1 = new PictureBox();
			this.label1 = new Label();
			this.labelVersion = new Label();
			this.label3 = new Label();
			this.label4 = new Label();
			this.label5 = new Label();
			this.label6 = new Label();
			this.label7 = new Label();
			this.button1 = new Button();
			this.label8 = new Label();
			this.linkDonation = new LinkLabel();
			base.SuspendLayout();
			this.pictureBox1.BorderStyle = BorderStyle.Fixed3D;
			this.pictureBox1.Image = (Image)resourceManager.GetObject("pictureBox1.Image");
			this.pictureBox1.Location = new Point(24, 24);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(312, 176);
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			this.label1.Font = new System.Drawing.Font("Arial", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.label1.Location = new Point(70, 32);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(230, 16);
			this.label1.TabIndex = 1;
			this.label1.Text = "Spawn Editor 2";
			this.label1.TextAlign = ContentAlignment.TopCenter;
			this.labelVersion.Font = new System.Drawing.Font("Arial", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.labelVersion.Location = new Point(70, 48);
			this.labelVersion.Name = "labelVersion";
			this.labelVersion.Size = new System.Drawing.Size(230, 16);
			this.labelVersion.TabIndex = 2;
			this.labelVersion.Text = "Version ###";
			this.labelVersion.TextAlign = ContentAlignment.TopCenter;
			this.label3.Font = new System.Drawing.Font("Arial", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.label3.Location = new Point(72, 88);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(230, 24);
			this.label3.TabIndex = 3;
			this.label3.Text = "written by ArteGordon, 7/7/05";
			this.label3.TextAlign = ContentAlignment.TopCenter;
			this.label4.Font = new System.Drawing.Font("Arial", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.label4.Location = new Point(32, 112);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(296, 64);
			this.label4.TabIndex = 4;
			this.label4.Text = "If you find this, or any of the XmlSpawner2 programs useful, please consider making a donation to the American Epilepsy Foundation, or any other worthy charitable cause.";
			this.label4.TextAlign = ContentAlignment.TopCenter;
			this.label5.Font = new System.Drawing.Font("Arial", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.label5.Location = new Point(72, 208);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(248, 16);
			this.label5.TabIndex = 5;
			this.label5.Text = "modified from the original Spawn Editor";
			this.label5.TextAlign = ContentAlignment.TopCenter;
			this.label6.Font = new System.Drawing.Font("Arial", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.label6.Location = new Point(72, 224);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(200, 16);
			this.label6.TabIndex = 6;
			this.label6.Text = "written by BobSmart";
			this.label6.TextAlign = ContentAlignment.TopCenter;
			this.label7.Font = new System.Drawing.Font("Arial", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.label7.Location = new Point(72, 240);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(200, 24);
			this.label7.TabIndex = 7;
			this.label7.Text = "April 16, 2003";
			this.label7.TextAlign = ContentAlignment.TopCenter;
			this.button1.Location = new Point(144, 264);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(88, 24);
			this.button1.TabIndex = 8;
			this.button1.Text = "OK";
			this.button1.Click += new EventHandler(this.button1_Click);
			this.label8.Font = new System.Drawing.Font("Arial", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.label8.Location = new Point(70, 64);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(230, 16);
			this.label8.TabIndex = 9;
			this.label8.Text = "updated October 5, 2005";
			this.label8.TextAlign = ContentAlignment.TopCenter;
			this.linkDonation.Location = new Point(48, 176);
			this.linkDonation.Name = "linkDonation";
			this.linkDonation.Size = new System.Drawing.Size(264, 16);
			this.linkDonation.TabIndex = 10;
			this.linkDonation.TabStop = true;
			this.linkDonation.Text = "http://www.epilepsyfoundation.org/howtohelp/index.cfm";
			this.linkDonation.LinkClicked += new LinkLabelLinkClickedEventHandler(this.linkDonation_LinkClicked);
			base.AcceptButton = this.button1;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			base.ClientSize = new System.Drawing.Size(360, 297);
			base.ControlBox = false;
			base.Controls.Add(this.linkDonation);
			base.Controls.Add(this.label8);
			base.Controls.Add(this.button1);
			base.Controls.Add(this.label7);
			base.Controls.Add(this.label6);
			base.Controls.Add(this.label5);
			base.Controls.Add(this.label4);
			base.Controls.Add(this.label3);
			base.Controls.Add(this.labelVersion);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.pictureBox1);
			base.Name = "AboutForm";
			this.Text = "About Spawn Editor 2";
			base.Load += new EventHandler(this.TestForm_Load);
			base.ResumeLayout(false);
		}

		private void linkDonation_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			try
			{
				this.VisitLink();
			}
			catch
			{
				MessageBox.Show("Unable to open link.");
			}
		}

		private void TestForm_Load(object sender, EventArgs e)
		{
			if (this._Editor.TopMost)
			{
				base.TopMost = true;
			}
			this.labelVersion.Text = string.Format("Version {0}", Assembly.GetAssembly(typeof(SpawnEditor)).GetName().Version);
		}

		private void VisitLink()
		{
			this.linkDonation.LinkVisited = true;
			Process.Start(this.linkDonation.Text);
		}
	}
}