using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SpawnEditor2
{
	public class EntryEdit : Form
	{
		public TextBox textEntryEdit;

		private Button btnCancel;

		private Button btnOk;

		private Panel panel1;

		private FontDialog fontDialog1;

		private System.Windows.Forms.ContextMenu contextMenu1;

		private MenuItem menuItem1;

		private MenuItem menuItem2;

		private MenuItem menuItem3;

		private MenuItem menuItem4;

		private MenuItem menuItem5;

		private System.ComponentModel.Container components = null;

		private SpawnEditor _Editor;

		public EntryEdit(SpawnEditor editor)
		{
			this._Editor = editor;
			this.InitializeComponent();
		}

		private void btnOk_Click(object sender, EventArgs e)
		{
			base.DialogResult = System.Windows.Forms.DialogResult.OK;
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

		private void EntryEdit_Load(object sender, EventArgs e)
		{
			if (this._Editor.TopMost)
			{
				base.TopMost = true;
			}
		}

		private void fontDialog1_Apply(object sender, EventArgs e)
		{
		}

		private void InitializeComponent()
		{
			this.textEntryEdit = new TextBox();
			this.contextMenu1 = new System.Windows.Forms.ContextMenu();
			this.menuItem1 = new MenuItem();
			this.btnCancel = new Button();
			this.btnOk = new Button();
			this.panel1 = new Panel();
			this.fontDialog1 = new FontDialog();
			this.menuItem2 = new MenuItem();
			this.menuItem3 = new MenuItem();
			this.menuItem4 = new MenuItem();
			this.menuItem5 = new MenuItem();
			this.panel1.SuspendLayout();
			base.SuspendLayout();
			this.textEntryEdit.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.textEntryEdit.ContextMenu = this.contextMenu1;
			this.textEntryEdit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.textEntryEdit.Location = new Point(8, 8);
			this.textEntryEdit.Multiline = true;
			this.textEntryEdit.Name = "textEntryEdit";
			this.textEntryEdit.ScrollBars = ScrollBars.Vertical;
			this.textEntryEdit.Size = new System.Drawing.Size(320, 224);
			this.textEntryEdit.TabIndex = 0;
			this.textEntryEdit.Text = "";
			System.Windows.Forms.Menu.MenuItemCollection menuItems = this.contextMenu1.MenuItems;
			MenuItem[] menuItemArray = new MenuItem[] { this.menuItem1, this.menuItem2, this.menuItem3, this.menuItem4, this.menuItem5 };
			menuItems.AddRange(menuItemArray);
			this.menuItem1.Index = 0;
			this.menuItem1.Text = "Font 8pt";
			this.menuItem1.Click += new EventHandler(this.menuItem1_Click);
			this.btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new Point(88, 248);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.TabIndex = 5;
			this.btnCancel.Text = "&Cancel";
			this.btnOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			this.btnOk.Location = new Point(8, 248);
			this.btnOk.Name = "btnOk";
			this.btnOk.TabIndex = 4;
			this.btnOk.Text = "&Ok";
			this.btnOk.Click += new EventHandler(this.btnOk_Click);
			this.panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.panel1.Controls.Add(this.textEntryEdit);
			this.panel1.Location = new Point(8, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(336, 240);
			this.panel1.TabIndex = 6;
			this.fontDialog1.Apply += new EventHandler(this.fontDialog1_Apply);
			this.menuItem2.Index = 1;
			this.menuItem2.Text = "Font 10pt";
			this.menuItem2.Click += new EventHandler(this.menuItem2_Click);
			this.menuItem3.Index = 2;
			this.menuItem3.Text = "Font 12pt";
			this.menuItem3.Click += new EventHandler(this.menuItem3_Click);
			this.menuItem4.Index = 3;
			this.menuItem4.Text = "Font 14pt";
			this.menuItem4.Click += new EventHandler(this.menuItem4_Click);
			this.menuItem5.Index = 4;
			this.menuItem5.Text = "Font 18pt";
			this.menuItem5.Click += new EventHandler(this.menuItem5_Click);
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			base.ClientSize = new System.Drawing.Size(344, 273);
			base.Controls.Add(this.panel1);
			base.Controls.Add(this.btnCancel);
			base.Controls.Add(this.btnOk);
			base.Name = "EntryEdit";
			base.Load += new EventHandler(this.EntryEdit_Load);
			this.panel1.ResumeLayout(false);
			base.ResumeLayout(false);
		}

		private void menuItem1_Click(object sender, EventArgs e)
		{
			this.textEntryEdit.Font = new System.Drawing.Font(this.textEntryEdit.Font.Name, 8f);
		}

		private void menuItem2_Click(object sender, EventArgs e)
		{
			this.textEntryEdit.Font = new System.Drawing.Font(this.textEntryEdit.Font.Name, 10f);
		}

		private void menuItem3_Click(object sender, EventArgs e)
		{
			this.textEntryEdit.Font = new System.Drawing.Font(this.textEntryEdit.Font.Name, 12f);
		}

		private void menuItem4_Click(object sender, EventArgs e)
		{
			this.textEntryEdit.Font = new System.Drawing.Font(this.textEntryEdit.Font.Name, 14f);
		}

		private void menuItem5_Click(object sender, EventArgs e)
		{
			this.textEntryEdit.Font = new System.Drawing.Font(this.textEntryEdit.Font.Name, 18f);
		}
	}
}