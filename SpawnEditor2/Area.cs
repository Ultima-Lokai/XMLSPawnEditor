using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SpawnEditor2
{
	public class Area : Form
	{
		private SpawnEditor _Editor;

		private SpawnPoint _Spawn;

		private bool _IsConstructed;

		private NumericUpDown spnX;

		private NumericUpDown spnY;

		private NumericUpDown spnWidth;

		private NumericUpDown spnHeight;

		private Label lblY;

		private Label lblX;

		private Label lblWidth;

		private Label lblHeight;

		private Button btnCancel;

		private Label label1;

		private NumericUpDown spnCentreZ;

		private NumericUpDown spnCentreY;

		private NumericUpDown spnCentreX;

		private Label label2;

		private Label label3;

		private Label label4;

		private Label label5;

		private System.ComponentModel.Container components = null;

		public Area(SpawnPoint Spawn, SpawnEditor Editor)
		{
			this.InitializeComponent();
			this._Spawn = Spawn;
			this._Editor = Editor;
			int left = this._Editor.grpSpawnEdit.Left + this._Editor.grpSpawnEdit.Parent.Left + this._Editor.Left;
			int top = this._Editor.grpSpawnEdit.Top + this._Editor.grpSpawnEdit.Parent.Top + this._Editor.btnUpdateSpawn.Top + this._Editor.Top;
			base.Left = left;
			base.Top = top;
			NumericUpDown x = this.spnX;
			Rectangle bounds = this._Spawn.Bounds;
			x.Value = bounds.X;
			NumericUpDown y = this.spnY;
			bounds = this._Spawn.Bounds;
			y.Value = bounds.Y;
			NumericUpDown width = this.spnWidth;
			bounds = this._Spawn.Bounds;
			width.Value = bounds.Width;
			NumericUpDown height = this.spnHeight;
			bounds = this._Spawn.Bounds;
			height.Value = bounds.Height;
			this.spnCentreX.Value = this._Spawn.CentreX;
			this.spnCentreY.Value = this._Spawn.CentreY;
			this.spnCentreZ.Value = this._Spawn.CentreZ;
			this._IsConstructed = true;
		}

		private void Area_KeyDown(object sender, KeyEventArgs e)
		{
			int num = 1;
			if (e.Shift)
			{
				num = 5;
			}
			if (e.KeyCode == Keys.Down)
			{
				if (!e.Control)
				{
					NumericUpDown value = this.spnY;
					value.Value = value.Value + num;
				}
				else
				{
					NumericUpDown numericUpDown = this.spnHeight;
					numericUpDown.Value = numericUpDown.Value + num;
				}
				e.Handled = true;
				return;
			}
			if (e.KeyCode == Keys.Up)
			{
				if (!e.Control)
				{
					NumericUpDown value1 = this.spnY;
					value1.Value = value1.Value - num;
				}
				else
				{
					NumericUpDown numericUpDown1 = this.spnHeight;
					numericUpDown1.Value = numericUpDown1.Value - num;
				}
				e.Handled = true;
				return;
			}
			if (e.KeyCode == Keys.Left)
			{
				if (!e.Control)
				{
					NumericUpDown value2 = this.spnX;
					value2.Value = value2.Value - num;
				}
				else
				{
					NumericUpDown numericUpDown2 = this.spnWidth;
					numericUpDown2.Value = numericUpDown2.Value - num;
				}
				e.Handled = true;
				return;
			}
			if (e.KeyCode == Keys.Right)
			{
				if (!e.Control)
				{
					NumericUpDown value3 = this.spnX;
					value3.Value = value3.Value + num;
				}
				else
				{
					NumericUpDown numericUpDown3 = this.spnWidth;
					numericUpDown3.Value = numericUpDown3.Value + num;
				}
				e.Handled = true;
			}
		}

		private void Area_Load(object sender, EventArgs e)
		{
			if (this._Editor.TopMost)
			{
				base.TopMost = true;
			}
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
			this.spnX = new NumericUpDown();
			this.spnY = new NumericUpDown();
			this.spnWidth = new NumericUpDown();
			this.spnHeight = new NumericUpDown();
			this.lblY = new Label();
			this.lblX = new Label();
			this.lblWidth = new Label();
			this.lblHeight = new Label();
			this.btnCancel = new Button();
			this.spnCentreZ = new NumericUpDown();
			this.label1 = new Label();
			this.spnCentreY = new NumericUpDown();
			this.spnCentreX = new NumericUpDown();
			this.label2 = new Label();
			this.label3 = new Label();
			this.label4 = new Label();
			this.label5 = new Label();
			((ISupportInitialize)this.spnX).BeginInit();
			((ISupportInitialize)this.spnY).BeginInit();
			((ISupportInitialize)this.spnWidth).BeginInit();
			((ISupportInitialize)this.spnHeight).BeginInit();
			((ISupportInitialize)this.spnCentreZ).BeginInit();
			((ISupportInitialize)this.spnCentreY).BeginInit();
			((ISupportInitialize)this.spnCentreX).BeginInit();
			base.SuspendLayout();
			this.spnX.Location = new Point(8, 88);
			NumericUpDown num = this.spnX;
			int[] numArray = new int[] { 10000, 0, 0, 0 };
			num.Maximum = new decimal(numArray);
			this.spnX.Name = "spnX";
			this.spnX.Size = new System.Drawing.Size(64, 20);
			this.spnX.TabIndex = 1;
			this.spnX.Enter += new EventHandler(this.TextEntryControl_Enter);
			this.spnX.ValueChanged += new EventHandler(this.SpinBox_ValueChanged);
			this.spnY.Location = new Point(48, 48);
			NumericUpDown numericUpDown = this.spnY;
			numArray = new int[] { 10000, 0, 0, 0 };
			numericUpDown.Maximum = new decimal(numArray);
			this.spnY.Name = "spnY";
			this.spnY.Size = new System.Drawing.Size(64, 20);
			this.spnY.TabIndex = 3;
			this.spnY.Enter += new EventHandler(this.TextEntryControl_Enter);
			this.spnY.ValueChanged += new EventHandler(this.SpinBox_ValueChanged);
			this.spnWidth.Location = new Point(88, 88);
			NumericUpDown num1 = this.spnWidth;
			numArray = new int[] { 10000, 0, 0, 0 };
			num1.Maximum = new decimal(numArray);
			this.spnWidth.Name = "spnWidth";
			this.spnWidth.Size = new System.Drawing.Size(64, 20);
			this.spnWidth.TabIndex = 5;
			this.spnWidth.Enter += new EventHandler(this.TextEntryControl_Enter);
			this.spnWidth.ValueChanged += new EventHandler(this.SpinBox_ValueChanged);
			this.spnHeight.Location = new Point(48, 128);
			NumericUpDown numericUpDown1 = this.spnHeight;
			numArray = new int[] { 10000, 0, 0, 0 };
			numericUpDown1.Maximum = new decimal(numArray);
			this.spnHeight.Name = "spnHeight";
			this.spnHeight.Size = new System.Drawing.Size(64, 20);
			this.spnHeight.TabIndex = 7;
			this.spnHeight.Enter += new EventHandler(this.TextEntryControl_Enter);
			this.spnHeight.ValueChanged += new EventHandler(this.SpinBox_ValueChanged);
			this.lblY.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.lblY.Location = new Point(48, 32);
			this.lblY.Name = "lblY";
			this.lblY.Size = new System.Drawing.Size(64, 16);
			this.lblY.TabIndex = 2;
			this.lblY.Text = "Y";
			this.lblY.TextAlign = ContentAlignment.TopCenter;
			this.lblX.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.lblX.Location = new Point(8, 72);
			this.lblX.Name = "lblX";
			this.lblX.Size = new System.Drawing.Size(64, 16);
			this.lblX.TabIndex = 0;
			this.lblX.Text = "X";
			this.lblX.TextAlign = ContentAlignment.TopCenter;
			this.lblWidth.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.lblWidth.Location = new Point(88, 72);
			this.lblWidth.Name = "lblWidth";
			this.lblWidth.Size = new System.Drawing.Size(64, 16);
			this.lblWidth.TabIndex = 4;
			this.lblWidth.Text = "Width";
			this.lblWidth.TextAlign = ContentAlignment.TopCenter;
			this.lblHeight.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.lblHeight.Location = new Point(48, 112);
			this.lblHeight.Name = "lblHeight";
			this.lblHeight.Size = new System.Drawing.Size(64, 16);
			this.lblHeight.TabIndex = 6;
			this.lblHeight.Text = "Height";
			this.lblHeight.TextAlign = ContentAlignment.TopCenter;
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new Point(176, 128);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(8, 8);
			this.btnCancel.TabIndex = 8;
			this.btnCancel.TabStop = false;
			this.btnCancel.Text = "Cancel";
			this.spnCentreZ.Location = new Point(48, 248);
			NumericUpDown num2 = this.spnCentreZ;
			numArray = new int[] { 65000, 0, 0, 0 };
			num2.Maximum = new decimal(numArray);
			NumericUpDown numericUpDown2 = this.spnCentreZ;
			numArray = new int[] { 32768, 0, 0, -2147483648 };
			numericUpDown2.Minimum = new decimal(numArray);
			this.spnCentreZ.Name = "spnCentreZ";
			this.spnCentreZ.Size = new System.Drawing.Size(80, 20);
			this.spnCentreZ.TabIndex = 9;
			this.spnCentreZ.ValueChanged += new EventHandler(this.spnCentreZ_ValueChanged);
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label1.Location = new Point(8, 248);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(32, 16);
			this.label1.TabIndex = 10;
			this.label1.Text = "Z";
			this.label1.TextAlign = ContentAlignment.TopCenter;
			this.spnCentreY.Location = new Point(48, 224);
			NumericUpDown num3 = this.spnCentreY;
			numArray = new int[] { 10000, 0, 0, 0 };
			num3.Maximum = new decimal(numArray);
			this.spnCentreY.Name = "spnCentreY";
			this.spnCentreY.Size = new System.Drawing.Size(80, 20);
			this.spnCentreY.TabIndex = 11;
			this.spnCentreY.Enter += new EventHandler(this.TextEntryControl_Enter);
			this.spnCentreY.ValueChanged += new EventHandler(this.spnCentreY_ValueChanged);
			this.spnCentreX.Location = new Point(48, 200);
			NumericUpDown numericUpDown3 = this.spnCentreX;
			numArray = new int[] { 10000, 0, 0, 0 };
			numericUpDown3.Maximum = new decimal(numArray);
			this.spnCentreX.Name = "spnCentreX";
			this.spnCentreX.Size = new System.Drawing.Size(80, 20);
			this.spnCentreX.TabIndex = 12;
			this.spnCentreX.Enter += new EventHandler(this.TextEntryControl_Enter);
			this.spnCentreX.ValueChanged += new EventHandler(this.spnCentreX_ValueChanged);
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label2.Location = new Point(8, 224);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(32, 16);
			this.label2.TabIndex = 13;
			this.label2.Text = "Y";
			this.label2.TextAlign = ContentAlignment.TopCenter;
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label3.Location = new Point(8, 200);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(32, 16);
			this.label3.TabIndex = 14;
			this.label3.Text = "X";
			this.label3.TextAlign = ContentAlignment.TopCenter;
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label4.Location = new Point(24, 176);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(112, 16);
			this.label4.TabIndex = 15;
			this.label4.Text = "Spawner Location";
			this.label4.TextAlign = ContentAlignment.TopCenter;
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label5.Location = new Point(24, 8);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(112, 16);
			this.label5.TabIndex = 16;
			this.label5.Text = "Spawner Bounds";
			this.label5.TextAlign = ContentAlignment.TopCenter;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			base.CancelButton = this.btnCancel;
			base.ClientSize = new System.Drawing.Size(160, 280);
			base.Controls.Add(this.label5);
			base.Controls.Add(this.label4);
			base.Controls.Add(this.label3);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.spnCentreX);
			base.Controls.Add(this.spnCentreY);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.spnCentreZ);
			base.Controls.Add(this.lblHeight);
			base.Controls.Add(this.lblWidth);
			base.Controls.Add(this.lblX);
			base.Controls.Add(this.lblY);
			base.Controls.Add(this.spnHeight);
			base.Controls.Add(this.spnWidth);
			base.Controls.Add(this.spnY);
			base.Controls.Add(this.spnX);
			base.Controls.Add(this.btnCancel);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			base.KeyPreview = true;
			base.Name = "Area";
			base.StartPosition = FormStartPosition.Manual;
			this.Text = "Location and Bounds";
			base.TransparencyKey = Color.Red;
			base.KeyDown += new KeyEventHandler(this.Area_KeyDown);
			base.Load += new EventHandler(this.Area_Load);
			((ISupportInitialize)this.spnX).EndInit();
			((ISupportInitialize)this.spnY).EndInit();
			((ISupportInitialize)this.spnWidth).EndInit();
			((ISupportInitialize)this.spnHeight).EndInit();
			((ISupportInitialize)this.spnCentreZ).EndInit();
			((ISupportInitialize)this.spnCentreY).EndInit();
			((ISupportInitialize)this.spnCentreX).EndInit();
			base.ResumeLayout(false);
		}

		private void SpinBox_ValueChanged(object sender, EventArgs e)
		{
			Rectangle bounds;
			if (this._IsConstructed)
			{
				NumericUpDown numericUpDown = sender as NumericUpDown;
				if (numericUpDown != null)
				{
					if (numericUpDown == this.spnX)
					{
						SpawnPoint rectangle = this._Spawn;
						int value = (int)this.spnX.Value;
						int y = this._Spawn.Bounds.Y;
						int width = this._Spawn.Bounds.Width;
						bounds = this._Spawn.Bounds;
						rectangle.Bounds = new Rectangle(value, y, width, bounds.Height);
					}
					else if (numericUpDown == this.spnY)
					{
						SpawnPoint spawnPoint = this._Spawn;
						int x = this._Spawn.Bounds.X;
						int num = (int)this.spnY.Value;
						int width1 = this._Spawn.Bounds.Width;
						bounds = this._Spawn.Bounds;
						spawnPoint.Bounds = new Rectangle(x, num, width1, bounds.Height);
					}
					else if (numericUpDown == this.spnWidth)
					{
						SpawnPoint rectangle1 = this._Spawn;
						int x1 = this._Spawn.Bounds.X;
						int y1 = this._Spawn.Bounds.Y;
						int value1 = (int)this.spnWidth.Value;
						bounds = this._Spawn.Bounds;
						rectangle1.Bounds = new Rectangle(x1, y1, value1, bounds.Height);
					}
					else if (numericUpDown == this.spnHeight)
					{
						SpawnPoint spawnPoint1 = this._Spawn;
						int num1 = this._Spawn.Bounds.X;
						int y2 = this._Spawn.Bounds.Y;
						bounds = this._Spawn.Bounds;
						spawnPoint1.Bounds = new Rectangle(num1, y2, bounds.Width, (int)this.spnHeight.Value);
					}
					if (!this._Editor.SpawnLocationLocked)
					{
						SpawnPoint width2 = this._Spawn;
						int x2 = this._Spawn.Bounds.X;
						bounds = this._Spawn.Bounds;
						width2.CentreX = (short)(x2 + bounds.Width / 2);
						SpawnPoint height = this._Spawn;
						int num2 = this._Spawn.Bounds.Y;
						bounds = this._Spawn.Bounds;
						height.CentreY = (short)(num2 + bounds.Height / 2);
					}
					this._Editor.spnSpawnRange.Value = new decimal(-1);
					this._Editor.RefreshSpawnPoints();
				}
			}
		}

		private void spnCentreX_ValueChanged(object sender, EventArgs e)
		{
			if (this._IsConstructed)
			{
				this._Spawn.CentreX = (short)this.spnCentreX.Value;
				this._Editor.RefreshSpawnPoints();
			}
		}

		private void spnCentreY_ValueChanged(object sender, EventArgs e)
		{
			if (this._IsConstructed)
			{
				this._Spawn.CentreY = (short)this.spnCentreY.Value;
				this._Editor.RefreshSpawnPoints();
			}
		}

		private void spnCentreZ_ValueChanged(object sender, EventArgs e)
		{
			if (this._IsConstructed)
			{
				this._Spawn.CentreZ = (short)this.spnCentreZ.Value;
				this._Editor.RefreshSpawnPoints();
			}
		}

		private void TextEntryControl_Enter(object sender, EventArgs e)
		{
			if (sender is TextBox)
			{
				TextBox textBox = (TextBox)sender;
				textBox.Select(0, textBox.MaxLength);
				return;
			}
			if (sender is NumericUpDown)
			{
				((NumericUpDown)sender).Select(0, 2147483647);
			}
		}
	}
}