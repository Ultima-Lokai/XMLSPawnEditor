using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SpawnEditor2
{
	public class Amount : Form
	{
		private Button btnOk;

		private Button btnCancel;

		private TextBox txtSpawnObject;

		private NumericUpDown spnSpawnAmount;

		private System.ComponentModel.Container components = null;

		public int SpawnAmount
		{
			get
			{
				return (int)this.spnSpawnAmount.Value;
			}
		}

		public string SpawnName
		{
			get
			{
				return this.txtSpawnObject.Text;
			}
		}

		public Amount(string Name, int Amount)
		{
			this.InitializeComponent();
			this.txtSpawnObject.Text = Name;
			this.spnSpawnAmount.Value = Amount;
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

		private void InitializeComponent()
		{
			this.txtSpawnObject = new TextBox();
			this.spnSpawnAmount = new NumericUpDown();
			this.btnOk = new Button();
			this.btnCancel = new Button();
			((ISupportInitialize)this.spnSpawnAmount).BeginInit();
			base.SuspendLayout();
			this.txtSpawnObject.Location = new Point(3, 8);
			this.txtSpawnObject.Name = "txtSpawnObject";
			this.txtSpawnObject.ReadOnly = true;
			this.txtSpawnObject.Size = new System.Drawing.Size(208, 20);
			this.txtSpawnObject.TabIndex = 0;
			this.txtSpawnObject.TabStop = false;
			this.txtSpawnObject.Text = "";
			this.spnSpawnAmount.Location = new Point(213, 8);
			this.spnSpawnAmount.Name = "spnSpawnAmount";
			this.spnSpawnAmount.Size = new System.Drawing.Size(75, 20);
			this.spnSpawnAmount.TabIndex = 1;
			this.spnSpawnAmount.Enter += new EventHandler(this.spnSpawnAmount_Enter);
			this.btnOk.Location = new Point(136, 32);
			this.btnOk.Name = "btnOk";
			this.btnOk.TabIndex = 2;
			this.btnOk.Text = "&Ok";
			this.btnOk.Click += new EventHandler(this.btnOk_Click);
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new Point(213, 32);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.TabIndex = 3;
			this.btnCancel.Text = "&Cancel";
			base.AcceptButton = this.btnOk;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			base.CancelButton = this.btnCancel;
			base.ClientSize = new System.Drawing.Size(292, 61);
			Control.ControlCollection controls = base.Controls;
			Control[] controlArray = new Control[] { this.btnCancel, this.btnOk, this.spnSpawnAmount, this.txtSpawnObject };
			controls.AddRange(controlArray);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			base.Name = "Amount";
			base.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Set Amount";
			((ISupportInitialize)this.spnSpawnAmount).EndInit();
			base.ResumeLayout(false);
		}

		private void spnSpawnAmount_Enter(object sender, EventArgs e)
		{
			this.spnSpawnAmount.Select(0, 2147483647);
		}
	}
}