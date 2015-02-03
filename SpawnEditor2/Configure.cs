using Microsoft.Win32;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace SpawnEditor2
{
	public class Configure : Form
	{
		private readonly string UOTDRegistryKey = "Software\\Origin Worlds Online\\Ultima Online Third Dawn\\1.0";

		private readonly string T2ARegistryKey = "Software\\Origin Worlds Online\\Ultima Online\\1.0";

		private readonly string UOExePathValue = "ExePath";

		private readonly string AppRegistryKey = "Software\\Spawn Editor";

		private readonly string AppRunUoPathValue = "RunUO Exe Path";

		private readonly string AppUoClientPathValue = "Ultima Client Exe Path";

		private readonly string AppZoomLevelValue = "Zoom Level";

		private readonly string AppRunUoCmdPrefixValue = "RunUO Cmd Prefix";

		private readonly string AppUoClientWindowValue = "Ultima Client Window";

		private readonly string AppSpawnNameValue = "Spawn Name";

		private readonly string AppSpawnHomeRangeValue = "Spawn Home Range";

		private readonly string AppSpawnMaxCountValue = "Spawn Max Count";

		private readonly string AppSpawnMinDelayValue = "Spawn Min Delay";

		private readonly string AppSpawnMaxDelayValue = "Spawn Max Delay";

		private readonly string AppSpawnTeamValue = "Spawn Team";

		private readonly string AppSpawnGroupValue = "Spawn Group";

		private readonly string AppSpawnRunningValue = "Spawn Running";

		private readonly string AppSpawnRelativeHomeValue = "Spawn Relative Home";

		private readonly string AppStartingStaticsValue = "Starting Statics";

		private readonly string AppStartingDetailsValue = "Starting Details";

		private readonly string AppStartingMapValue = "Starting Map";

		private readonly string AppStartingOnTopValue = "Starting On Top";

		private readonly string AppStartingXValue = "Starting X";

		private readonly string AppStartingYValue = "Starting Y";

		private readonly string AppStartingWidthValue = "Starting Width";

		private readonly string AppStartingHeightValue = "Starting Height";

		private readonly string AppTransferServerAddressValue = "Transfer Server Address";

		private readonly string AppTransferServerPortValue = "Transfer Server Port";

		public string CfgRunUoPathValue;

		public string CfgUoClientPathValue;

		public string CfgUoClientWindowValue = "Ultima Online Third Dawn";

		public short CfgZoomLevelValue = -4;

		public string CfgRunUoCmdPrefix = "[";

		public string CfgSpawnNameValue = "Spawn";

		public int CfgSpawnHomeRangeValue = 10;

		public int CfgSpawnMaxCountValue = 1;

		public int CfgSpawnMinDelayValue = 5;

		public int CfgSpawnMaxDelayValue = 10;

		public int CfgSpawnTeamValue = 0;

		public bool CfgSpawnGroupValue = false;

		public bool CfgSpawnRunningValue = true;

		public bool CfgSpawnRelativeHomeValue = true;

		public bool CfgStartingStaticsValue = false;

		public bool CfgStartingDetailsValue = false;

		public WorldMap CfgStartingMapValue = WorldMap.Trammel;

		public bool CfgStartingOnTopValue = false;

		public int CfgStartingXValue = -1;

		public int CfgStartingYValue = -1;

		public int CfgStartingWidthValue = -1;

		public int CfgStartingHeightValue = -1;

		public string CfgTransferServerAddressValue = "127.0.0.1";

		public int CfgTransferServerPortValue = 8030;

		private bool _IsValidConfiguration = false;

		private RegistryKey _HKLMKey = null;

		private RegistryKey _HKCUKey = null;

		private OpenFileDialog ofdOpenFile;

		private TextBox txtRunUOExe;

		private Button btnRunUOExe;

		private Label lblRunUOExe;

		private TextBox txtUltimaClient;

		private Label lblUltimaClient;

		private Button btnUltimaClient;

		private Label label1;

		private TrackBar trkZoom;

		private GroupBox grpSpawnEdit;

		private Label lblMaxDelay;

		private Label lblHomeRange;

		private Label lblTeam;

		private Label lblMaxCount;

		private Label lblMinDelay;

		private CheckBox chkSpawnRunning;

		private NumericUpDown spnSpawnMaxCount;

		private TextBox txtSpawnName;

		private NumericUpDown spnSpawnRange;

		private NumericUpDown spnSpawnMinDelay;

		private NumericUpDown spnSpawnTeam;

		private CheckBox chkSpawnGroup;

		private NumericUpDown spnSpawnMaxDelay;

		private Button btnOk;

		private Button btnCancel;

		private CheckBox chkHomeRangeIsRelative;

		private TextBox txtCmdPrefix;

		private Label lblCmdPrefix;

		private Label lblClientWindow;

		private TextBox txtClientWindow;

		private CheckBox startingStatics;

		private CheckBox startingDetails;

		private ComboBox startingMap;

		private Label label2;

		private CheckBox startingOnTop;

		private IContainer components;

		private SpawnEditor _Editor;

		public bool IsValidConfiguration
		{
			get
			{
				return this._IsValidConfiguration;
			}
		}

		public Configure(SpawnEditor editor)
		{
			this._Editor = editor;
			this.InitializeComponent();
			this._HKCUKey = Registry.CurrentUser.OpenSubKey(this.AppRegistryKey, true);
			if (this._HKCUKey != null && this._HKCUKey.ValueCount >= 14)
			{
				this.CfgRunUoPathValue = (string)this._HKCUKey.GetValue(this.AppRunUoPathValue, string.Empty);
				this.CfgUoClientPathValue = (string)this._HKCUKey.GetValue(this.AppUoClientPathValue, string.Empty);
				this.CfgUoClientWindowValue = (string)this._HKCUKey.GetValue(this.AppUoClientWindowValue, "Ultima Online Third Dawn");
				this.CfgZoomLevelValue = short.Parse(this._HKCUKey.GetValue(this.AppZoomLevelValue, "-4") as string);
				this.CfgRunUoCmdPrefix = (string)this._HKCUKey.GetValue(this.AppRunUoCmdPrefixValue, "[");
				this.CfgSpawnNameValue = (string)this._HKCUKey.GetValue(this.AppSpawnNameValue, "Spawner");
				this.CfgSpawnHomeRangeValue = (int)this._HKCUKey.GetValue(this.AppSpawnHomeRangeValue, 5);
				this.CfgSpawnMaxCountValue = (int)this._HKCUKey.GetValue(this.AppSpawnMaxCountValue, 1);
				this.CfgSpawnMinDelayValue = (int)this._HKCUKey.GetValue(this.AppSpawnMinDelayValue, 5);
				this.CfgSpawnMaxDelayValue = (int)this._HKCUKey.GetValue(this.AppSpawnMaxDelayValue, 10);
				this.CfgSpawnTeamValue = (int)this._HKCUKey.GetValue(this.AppSpawnTeamValue, 0);
				this.CfgSpawnGroupValue = bool.Parse(this._HKCUKey.GetValue(this.AppSpawnGroupValue, bool.FalseString) as string);
				this.CfgSpawnRunningValue = bool.Parse(this._HKCUKey.GetValue(this.AppSpawnRunningValue, bool.TrueString) as string);
				this.CfgSpawnRelativeHomeValue = bool.Parse(this._HKCUKey.GetValue(this.AppSpawnRelativeHomeValue, bool.TrueString) as string);
				this.CfgStartingStaticsValue = bool.Parse(this._HKCUKey.GetValue(this.AppStartingStaticsValue, bool.FalseString) as string);
				this.CfgStartingDetailsValue = bool.Parse(this._HKCUKey.GetValue(this.AppStartingDetailsValue, bool.FalseString) as string);
				this.CfgStartingMapValue = (WorldMap)((int)((WorldMap)Enum.Parse(typeof(WorldMap), this._HKCUKey.GetValue(this.AppStartingMapValue, "Trammel") as string)));
				this.CfgStartingOnTopValue = bool.Parse(this._HKCUKey.GetValue(this.AppStartingOnTopValue, bool.FalseString) as string);
				this.CfgStartingXValue = (int)this._HKCUKey.GetValue(this.AppStartingXValue, -1);
				this.CfgStartingYValue = (int)this._HKCUKey.GetValue(this.AppStartingYValue, -1);
				this.CfgStartingWidthValue = (int)this._HKCUKey.GetValue(this.AppStartingWidthValue, -1);
				this.CfgStartingHeightValue = (int)this._HKCUKey.GetValue(this.AppStartingHeightValue, -1);
				this.CfgTransferServerAddressValue = (string)this._HKCUKey.GetValue(this.AppTransferServerAddressValue, "127.0.0.1");
				this.CfgTransferServerPortValue = (int)this._HKCUKey.GetValue(this.AppTransferServerPortValue, 8030);
				if (File.Exists(this.CfgRunUoPathValue) && this.CfgUoClientPathValue.Length > 0)
				{
					this._IsValidConfiguration = true;
				}
			}
		}

		private void btnOk_Click(object sender, EventArgs e)
		{
			this.CfgRunUoPathValue = this.txtRunUOExe.Text;
			this.CfgUoClientPathValue = this.txtUltimaClient.Text;
			this.CfgUoClientWindowValue = this.txtClientWindow.Text;
			this.CfgZoomLevelValue = (short)this.trkZoom.Value;
			this.CfgRunUoCmdPrefix = this.txtCmdPrefix.Text;
			this.CfgSpawnNameValue = this.txtSpawnName.Text;
			this.CfgSpawnHomeRangeValue = (int)this.spnSpawnRange.Value;
			this.CfgSpawnMaxCountValue = (int)this.spnSpawnMaxCount.Value;
			this.CfgSpawnMinDelayValue = (int)this.spnSpawnMinDelay.Value;
			this.CfgSpawnMaxDelayValue = (int)this.spnSpawnMaxDelay.Value;
			this.CfgSpawnTeamValue = (int)this.spnSpawnTeam.Value;
			this.CfgSpawnGroupValue = this.chkSpawnGroup.Checked;
			this.CfgSpawnRunningValue = this.chkSpawnRunning.Checked;
			this.CfgSpawnRelativeHomeValue = this.chkHomeRangeIsRelative.Checked;
			this.CfgStartingStaticsValue = this.startingStatics.Checked;
			this.CfgStartingDetailsValue = this.startingDetails.Checked;
			this.CfgStartingMapValue = (WorldMap)this.startingMap.SelectedIndex;
			this.CfgStartingOnTopValue = this.startingOnTop.Checked;
			if (this.CfgRunUoPathValue.Length == 0)
			{
				MessageBox.Show(this, "You must set the path to the RunUO EXE before proceeding!", "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return;
			}
			if (this.CfgUoClientPathValue.Length == 0)
			{
				MessageBox.Show(this, "You must set the path to the Ultima Online client EXE before proceeding!", "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return;
			}
			if (this._HKCUKey == null)
			{
				this._HKCUKey = Registry.CurrentUser.CreateSubKey(this.AppRegistryKey);
			}
			this._HKCUKey.SetValue(this.AppRunUoPathValue, this.CfgRunUoPathValue);
			this._HKCUKey.SetValue(this.AppUoClientPathValue, this.CfgUoClientPathValue);
			this._HKCUKey.SetValue(this.AppUoClientWindowValue, this.CfgUoClientWindowValue);
			this._HKCUKey.SetValue(this.AppZoomLevelValue, this.CfgZoomLevelValue.ToString());
			this._HKCUKey.SetValue(this.AppRunUoCmdPrefixValue, this.CfgRunUoCmdPrefix);
			this._HKCUKey.SetValue(this.AppSpawnNameValue, this.CfgSpawnNameValue);
			this._HKCUKey.SetValue(this.AppSpawnHomeRangeValue, this.CfgSpawnHomeRangeValue);
			this._HKCUKey.SetValue(this.AppSpawnMaxCountValue, this.CfgSpawnMaxCountValue);
			this._HKCUKey.SetValue(this.AppSpawnMinDelayValue, this.CfgSpawnMinDelayValue);
			this._HKCUKey.SetValue(this.AppSpawnMaxDelayValue, this.CfgSpawnMaxDelayValue);
			this._HKCUKey.SetValue(this.AppSpawnTeamValue, this.CfgSpawnTeamValue);
			this._HKCUKey.SetValue(this.AppSpawnGroupValue, this.CfgSpawnGroupValue);
			this._HKCUKey.SetValue(this.AppSpawnRunningValue, this.CfgSpawnRunningValue);
			this._HKCUKey.SetValue(this.AppSpawnRelativeHomeValue, this.CfgSpawnRelativeHomeValue);
			this._HKCUKey.SetValue(this.AppStartingStaticsValue, this.CfgStartingStaticsValue);
			this._HKCUKey.SetValue(this.AppStartingDetailsValue, this.CfgStartingDetailsValue);
			this._HKCUKey.SetValue(this.AppStartingMapValue, this.CfgStartingMapValue.ToString());
			this._HKCUKey.SetValue(this.AppStartingOnTopValue, this.CfgStartingOnTopValue);
			this._IsValidConfiguration = true;
			base.Close();
		}

		private void btnRunUOExe_Click(object sender, EventArgs e)
		{
			if (this.ofdOpenFile.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
			{
				this.txtRunUOExe.Text = this.ofdOpenFile.FileName;
			}
		}

		private void btnUltimaClient_Click(object sender, EventArgs e)
		{
			if (this.ofdOpenFile.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
			{
				this.txtUltimaClient.Text = this.ofdOpenFile.FileName;
				this.SetClientWindowName();
			}
		}

		private void Configure_Load(object sender, EventArgs e)
		{
			if (this._Editor.TopMost)
			{
				base.TopMost = true;
			}
			foreach (object value in Enum.GetValues(typeof(WorldMap)))
			{
				WorldMap worldMap = (WorldMap)((int)((WorldMap)value));
				if (worldMap == WorldMap.Internal)
				{
					continue;
				}
				this.startingMap.Items.Add(worldMap);
			}
			if (!this._IsValidConfiguration)
			{
				this._HKLMKey = Registry.LocalMachine.OpenSubKey(this.UOTDRegistryKey);
				this.CfgUoClientWindowValue = "Ultima Online Third Dawn";
				if (this._HKLMKey == null)
				{
					this._HKLMKey = Registry.LocalMachine.OpenSubKey(this.T2ARegistryKey);
				}
				if (this._HKLMKey != null)
				{
					this.CfgUoClientPathValue = (string)this._HKLMKey.GetValue(this.UOExePathValue);
					this.txtUltimaClient.Text = this.CfgUoClientPathValue;
					this.SetClientWindowName();
				}
			}
			this.txtRunUOExe.Text = this.CfgRunUoPathValue;
			this.txtUltimaClient.Text = this.CfgUoClientPathValue;
			this.txtClientWindow.Text = this.CfgUoClientWindowValue;
			this.trkZoom.Value = this.CfgZoomLevelValue;
			this.txtCmdPrefix.Text = this.CfgRunUoCmdPrefix;
			this.txtSpawnName.Text = this.CfgSpawnNameValue;
			this.spnSpawnRange.Value = this.CfgSpawnHomeRangeValue;
			this.spnSpawnMaxCount.Value = this.CfgSpawnMaxCountValue;
			this.spnSpawnMinDelay.Value = this.CfgSpawnMinDelayValue;
			this.spnSpawnMaxDelay.Value = this.CfgSpawnMaxDelayValue;
			this.spnSpawnTeam.Value = this.CfgSpawnTeamValue;
			this.chkSpawnGroup.Checked = this.CfgSpawnGroupValue;
			this.chkSpawnRunning.Checked = this.CfgSpawnRunningValue;
			this.chkHomeRangeIsRelative.Checked = this.CfgSpawnRelativeHomeValue;
			this.startingStatics.Checked = this.CfgStartingStaticsValue;
			this.startingDetails.Checked = this.CfgStartingDetailsValue;
			this.startingMap.SelectedIndex = (int)this.CfgStartingMapValue;
			this.startingOnTop.Checked = this.CfgStartingOnTopValue;
		}

		public void ConfigureTransferServer()
		{
			this._Editor._TransferDialog.txtTransferServerAddress.Text = this.CfgTransferServerAddressValue;
			this._Editor._TransferDialog.txtTransferServerPort.Text = this.CfgTransferServerPortValue.ToString();
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
			this.ofdOpenFile = new OpenFileDialog();
			this.txtRunUOExe = new TextBox();
			this.btnRunUOExe = new Button();
			this.lblRunUOExe = new Label();
			this.txtUltimaClient = new TextBox();
			this.lblUltimaClient = new Label();
			this.btnUltimaClient = new Button();
			this.label1 = new Label();
			this.trkZoom = new TrackBar();
			this.grpSpawnEdit = new GroupBox();
			this.chkHomeRangeIsRelative = new CheckBox();
			this.lblMaxDelay = new Label();
			this.chkSpawnRunning = new CheckBox();
			this.lblHomeRange = new Label();
			this.spnSpawnMaxCount = new NumericUpDown();
			this.txtSpawnName = new TextBox();
			this.spnSpawnRange = new NumericUpDown();
			this.lblTeam = new Label();
			this.lblMaxCount = new Label();
			this.spnSpawnMinDelay = new NumericUpDown();
			this.spnSpawnTeam = new NumericUpDown();
			this.chkSpawnGroup = new CheckBox();
			this.spnSpawnMaxDelay = new NumericUpDown();
			this.lblMinDelay = new Label();
			this.btnOk = new Button();
			this.btnCancel = new Button();
			this.txtCmdPrefix = new TextBox();
			this.lblCmdPrefix = new Label();
			this.lblClientWindow = new Label();
			this.txtClientWindow = new TextBox();
			this.startingStatics = new CheckBox();
			this.startingDetails = new CheckBox();
			this.startingMap = new ComboBox();
			this.label2 = new Label();
			this.startingOnTop = new CheckBox();
			((ISupportInitialize)this.trkZoom).BeginInit();
			this.grpSpawnEdit.SuspendLayout();
			((ISupportInitialize)this.spnSpawnMaxCount).BeginInit();
			((ISupportInitialize)this.spnSpawnRange).BeginInit();
			((ISupportInitialize)this.spnSpawnMinDelay).BeginInit();
			((ISupportInitialize)this.spnSpawnTeam).BeginInit();
			((ISupportInitialize)this.spnSpawnMaxDelay).BeginInit();
			base.SuspendLayout();
			this.ofdOpenFile.DefaultExt = "exe";
			this.ofdOpenFile.Filter = "Executable (*.exe)|*.exe|All Files (*.*)|*.*";
			this.ofdOpenFile.ReadOnlyChecked = true;
			this.txtRunUOExe.Location = new Point(80, 8);
			this.txtRunUOExe.Name = "txtRunUOExe";
			this.txtRunUOExe.ReadOnly = true;
			this.txtRunUOExe.Size = new System.Drawing.Size(208, 20);
			this.txtRunUOExe.TabIndex = 1;
			this.txtRunUOExe.Text = "";
			this.btnRunUOExe.Location = new Point(288, 8);
			this.btnRunUOExe.Name = "btnRunUOExe";
			this.btnRunUOExe.Size = new System.Drawing.Size(24, 20);
			this.btnRunUOExe.TabIndex = 2;
			this.btnRunUOExe.Text = "...";
			this.btnRunUOExe.Click += new EventHandler(this.btnRunUOExe_Click);
			this.lblRunUOExe.Location = new Point(8, 8);
			this.lblRunUOExe.Name = "lblRunUOExe";
			this.lblRunUOExe.Size = new System.Drawing.Size(80, 20);
			this.lblRunUOExe.TabIndex = 0;
			this.lblRunUOExe.Text = "RunUO.EXE:";
			this.lblRunUOExe.TextAlign = ContentAlignment.MiddleLeft;
			this.txtUltimaClient.Location = new Point(80, 32);
			this.txtUltimaClient.Name = "txtUltimaClient";
			this.txtUltimaClient.ReadOnly = true;
			this.txtUltimaClient.Size = new System.Drawing.Size(208, 20);
			this.txtUltimaClient.TabIndex = 4;
			this.txtUltimaClient.Text = "";
			this.lblUltimaClient.Location = new Point(8, 32);
			this.lblUltimaClient.Name = "lblUltimaClient";
			this.lblUltimaClient.Size = new System.Drawing.Size(80, 20);
			this.lblUltimaClient.TabIndex = 3;
			this.lblUltimaClient.Text = "Ultima Client:";
			this.lblUltimaClient.TextAlign = ContentAlignment.MiddleLeft;
			this.btnUltimaClient.Location = new Point(288, 32);
			this.btnUltimaClient.Name = "btnUltimaClient";
			this.btnUltimaClient.Size = new System.Drawing.Size(24, 20);
			this.btnUltimaClient.TabIndex = 5;
			this.btnUltimaClient.Text = "...";
			this.btnUltimaClient.Click += new EventHandler(this.btnUltimaClient_Click);
			this.label1.Location = new Point(8, 64);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(112, 20);
			this.label1.TabIndex = 6;
			this.label1.Text = "Default Zoom Level:";
			this.label1.TextAlign = ContentAlignment.MiddleLeft;
			this.trkZoom.LargeChange = 2;
			this.trkZoom.Location = new Point(112, 56);
			this.trkZoom.Maximum = 4;
			this.trkZoom.Minimum = -4;
			this.trkZoom.Name = "trkZoom";
			this.trkZoom.Size = new System.Drawing.Size(146, 45);
			this.trkZoom.TabIndex = 7;
			this.trkZoom.TickStyle = TickStyle.TopLeft;
			this.grpSpawnEdit.Controls.Add(this.chkHomeRangeIsRelative);
			this.grpSpawnEdit.Controls.Add(this.lblMaxDelay);
			this.grpSpawnEdit.Controls.Add(this.chkSpawnRunning);
			this.grpSpawnEdit.Controls.Add(this.lblHomeRange);
			this.grpSpawnEdit.Controls.Add(this.spnSpawnMaxCount);
			this.grpSpawnEdit.Controls.Add(this.txtSpawnName);
			this.grpSpawnEdit.Controls.Add(this.spnSpawnRange);
			this.grpSpawnEdit.Controls.Add(this.lblTeam);
			this.grpSpawnEdit.Controls.Add(this.lblMaxCount);
			this.grpSpawnEdit.Controls.Add(this.spnSpawnMinDelay);
			this.grpSpawnEdit.Controls.Add(this.spnSpawnTeam);
			this.grpSpawnEdit.Controls.Add(this.chkSpawnGroup);
			this.grpSpawnEdit.Controls.Add(this.spnSpawnMaxDelay);
			this.grpSpawnEdit.Controls.Add(this.lblMinDelay);
			this.grpSpawnEdit.Location = new Point(112, 104);
			this.grpSpawnEdit.Name = "grpSpawnEdit";
			this.grpSpawnEdit.Size = new System.Drawing.Size(152, 200);
			this.grpSpawnEdit.TabIndex = 10;
			this.grpSpawnEdit.TabStop = false;
			this.grpSpawnEdit.Text = "Default Spawn Details";
			this.chkHomeRangeIsRelative.CheckAlign = ContentAlignment.MiddleRight;
			this.chkHomeRangeIsRelative.Checked = true;
			this.chkHomeRangeIsRelative.CheckState = CheckState.Checked;
			this.chkHomeRangeIsRelative.Location = new Point(8, 176);
			this.chkHomeRangeIsRelative.Name = "chkHomeRangeIsRelative";
			this.chkHomeRangeIsRelative.Size = new System.Drawing.Size(102, 16);
			this.chkHomeRangeIsRelative.TabIndex = 13;
			this.chkHomeRangeIsRelative.Text = "Relative Home:";
			this.lblMaxDelay.Location = new Point(8, 104);
			this.lblMaxDelay.Name = "lblMaxDelay";
			this.lblMaxDelay.Size = new System.Drawing.Size(80, 16);
			this.lblMaxDelay.TabIndex = 7;
			this.lblMaxDelay.Text = "Max Delay (m)";
			this.chkSpawnRunning.CheckAlign = ContentAlignment.MiddleRight;
			this.chkSpawnRunning.Checked = true;
			this.chkSpawnRunning.CheckState = CheckState.Checked;
			this.chkSpawnRunning.Location = new Point(8, 160);
			this.chkSpawnRunning.Name = "chkSpawnRunning";
			this.chkSpawnRunning.Size = new System.Drawing.Size(102, 16);
			this.chkSpawnRunning.TabIndex = 12;
			this.chkSpawnRunning.Text = "Running:";
			this.lblHomeRange.Location = new Point(8, 44);
			this.lblHomeRange.Name = "lblHomeRange";
			this.lblHomeRange.Size = new System.Drawing.Size(80, 16);
			this.lblHomeRange.TabIndex = 1;
			this.lblHomeRange.Text = "Home Range:";
			this.spnSpawnMaxCount.Location = new Point(96, 60);
			NumericUpDown num = this.spnSpawnMaxCount;
			int[] numArray = new int[] { 10000, 0, 0, 0 };
			num.Maximum = new decimal(numArray);
			this.spnSpawnMaxCount.Name = "spnSpawnMaxCount";
			this.spnSpawnMaxCount.Size = new System.Drawing.Size(48, 20);
			this.spnSpawnMaxCount.TabIndex = 4;
			NumericUpDown numericUpDown = this.spnSpawnMaxCount;
			numArray = new int[] { 1, 0, 0, 0 };
			numericUpDown.Value = new decimal(numArray);
			this.txtSpawnName.Location = new Point(8, 16);
			this.txtSpawnName.Name = "txtSpawnName";
			this.txtSpawnName.Size = new System.Drawing.Size(136, 20);
			this.txtSpawnName.TabIndex = 0;
			this.txtSpawnName.Text = "Spawn";
			this.spnSpawnRange.Location = new Point(96, 40);
			NumericUpDown num1 = this.spnSpawnRange;
			numArray = new int[] { 10000, 0, 0, 0 };
			num1.Maximum = new decimal(numArray);
			NumericUpDown numericUpDown1 = this.spnSpawnRange;
			numArray = new int[] { 1, 0, 0, 0 };
			numericUpDown1.Minimum = new decimal(numArray);
			this.spnSpawnRange.Name = "spnSpawnRange";
			this.spnSpawnRange.Size = new System.Drawing.Size(48, 20);
			this.spnSpawnRange.TabIndex = 2;
			NumericUpDown num2 = this.spnSpawnRange;
			numArray = new int[] { 10, 0, 0, 0 };
			num2.Value = new decimal(numArray);
			this.lblTeam.Location = new Point(8, 124);
			this.lblTeam.Name = "lblTeam";
			this.lblTeam.Size = new System.Drawing.Size(80, 16);
			this.lblTeam.TabIndex = 9;
			this.lblTeam.Text = "Team:";
			this.lblMaxCount.Location = new Point(8, 64);
			this.lblMaxCount.Name = "lblMaxCount";
			this.lblMaxCount.Size = new System.Drawing.Size(80, 16);
			this.lblMaxCount.TabIndex = 3;
			this.lblMaxCount.Text = "Max Count:";
			this.spnSpawnMinDelay.Location = new Point(96, 80);
			NumericUpDown numericUpDown2 = this.spnSpawnMinDelay;
			numArray = new int[] { 65535, 0, 0, 0 };
			numericUpDown2.Maximum = new decimal(numArray);
			this.spnSpawnMinDelay.Name = "spnSpawnMinDelay";
			this.spnSpawnMinDelay.Size = new System.Drawing.Size(48, 20);
			this.spnSpawnMinDelay.TabIndex = 6;
			NumericUpDown num3 = this.spnSpawnMinDelay;
			numArray = new int[] { 5, 0, 0, 0 };
			num3.Value = new decimal(numArray);
			this.spnSpawnTeam.Location = new Point(96, 120);
			NumericUpDown numericUpDown3 = this.spnSpawnTeam;
			numArray = new int[] { 65535, 0, 0, 0 };
			numericUpDown3.Maximum = new decimal(numArray);
			this.spnSpawnTeam.Name = "spnSpawnTeam";
			this.spnSpawnTeam.Size = new System.Drawing.Size(48, 20);
			this.spnSpawnTeam.TabIndex = 10;
			this.chkSpawnGroup.CheckAlign = ContentAlignment.MiddleRight;
			this.chkSpawnGroup.Location = new Point(8, 144);
			this.chkSpawnGroup.Name = "chkSpawnGroup";
			this.chkSpawnGroup.Size = new System.Drawing.Size(102, 16);
			this.chkSpawnGroup.TabIndex = 11;
			this.chkSpawnGroup.Text = "Group:";
			this.spnSpawnMaxDelay.Location = new Point(96, 100);
			NumericUpDown num4 = this.spnSpawnMaxDelay;
			numArray = new int[] { 65535, 0, 0, 0 };
			num4.Maximum = new decimal(numArray);
			this.spnSpawnMaxDelay.Name = "spnSpawnMaxDelay";
			this.spnSpawnMaxDelay.Size = new System.Drawing.Size(48, 20);
			this.spnSpawnMaxDelay.TabIndex = 8;
			NumericUpDown numericUpDown4 = this.spnSpawnMaxDelay;
			numArray = new int[] { 10, 0, 0, 0 };
			numericUpDown4.Value = new decimal(numArray);
			this.lblMinDelay.Location = new Point(8, 84);
			this.lblMinDelay.Name = "lblMinDelay";
			this.lblMinDelay.Size = new System.Drawing.Size(80, 16);
			this.lblMinDelay.TabIndex = 5;
			this.lblMinDelay.Text = "Min Delay (m)";
			this.btnOk.Location = new Point(112, 368);
			this.btnOk.Name = "btnOk";
			this.btnOk.TabIndex = 11;
			this.btnOk.Text = "&Ok";
			this.btnOk.Click += new EventHandler(this.btnOk_Click);
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new Point(192, 368);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.TabIndex = 12;
			this.btnCancel.Text = "&Cancel";
			this.txtCmdPrefix.Location = new Point(8, 120);
			this.txtCmdPrefix.Name = "txtCmdPrefix";
			this.txtCmdPrefix.Size = new System.Drawing.Size(96, 20);
			this.txtCmdPrefix.TabIndex = 9;
			this.txtCmdPrefix.Text = "[";
			this.lblCmdPrefix.Location = new Point(8, 104);
			this.lblCmdPrefix.Name = "lblCmdPrefix";
			this.lblCmdPrefix.Size = new System.Drawing.Size(96, 16);
			this.lblCmdPrefix.TabIndex = 8;
			this.lblCmdPrefix.Text = "Command Prefix:";
			this.lblClientWindow.Location = new Point(8, 152);
			this.lblClientWindow.Name = "lblClientWindow";
			this.lblClientWindow.Size = new System.Drawing.Size(88, 16);
			this.lblClientWindow.TabIndex = 13;
			this.lblClientWindow.Text = "Client Window:";
			this.txtClientWindow.Location = new Point(8, 168);
			this.txtClientWindow.Name = "txtClientWindow";
			this.txtClientWindow.Size = new System.Drawing.Size(96, 20);
			this.txtClientWindow.TabIndex = 14;
			this.txtClientWindow.Text = "Ultima Online";
			this.startingStatics.Location = new Point(8, 216);
			this.startingStatics.Name = "startingStatics";
			this.startingStatics.Size = new System.Drawing.Size(96, 16);
			this.startingStatics.TabIndex = 15;
			this.startingStatics.Text = "Statics";
			this.startingDetails.Location = new Point(8, 232);
			this.startingDetails.Name = "startingDetails";
			this.startingDetails.Size = new System.Drawing.Size(96, 16);
			this.startingDetails.TabIndex = 16;
			this.startingDetails.Text = "Details";
			this.startingMap.DropDownStyle = ComboBoxStyle.DropDownList;
			this.startingMap.Location = new Point(8, 272);
			this.startingMap.Name = "startingMap";
			this.startingMap.Size = new System.Drawing.Size(77, 21);
			this.startingMap.TabIndex = 17;
			this.label2.Location = new Point(8, 200);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(88, 16);
			this.label2.TabIndex = 18;
			this.label2.Text = "On Startup:";
			this.startingOnTop.Location = new Point(8, 248);
			this.startingOnTop.Name = "startingOnTop";
			this.startingOnTop.Size = new System.Drawing.Size(96, 16);
			this.startingOnTop.TabIndex = 19;
			this.startingOnTop.Text = "On Top";
			base.AcceptButton = this.btnOk;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			base.CancelButton = this.btnCancel;
			base.ClientSize = new System.Drawing.Size(314, 392);
			base.Controls.Add(this.startingOnTop);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.startingMap);
			base.Controls.Add(this.startingDetails);
			base.Controls.Add(this.startingStatics);
			base.Controls.Add(this.txtClientWindow);
			base.Controls.Add(this.lblClientWindow);
			base.Controls.Add(this.lblCmdPrefix);
			base.Controls.Add(this.txtCmdPrefix);
			base.Controls.Add(this.btnCancel);
			base.Controls.Add(this.btnOk);
			base.Controls.Add(this.grpSpawnEdit);
			base.Controls.Add(this.trkZoom);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.txtUltimaClient);
			base.Controls.Add(this.lblUltimaClient);
			base.Controls.Add(this.btnUltimaClient);
			base.Controls.Add(this.txtRunUOExe);
			base.Controls.Add(this.lblRunUOExe);
			base.Controls.Add(this.btnRunUOExe);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			base.Name = "Configure";
			base.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Spawn Editor Configuration";
			base.Load += new EventHandler(this.Configure_Load);
			((ISupportInitialize)this.trkZoom).EndInit();
			this.grpSpawnEdit.ResumeLayout(false);
			((ISupportInitialize)this.spnSpawnMaxCount).EndInit();
			((ISupportInitialize)this.spnSpawnRange).EndInit();
			((ISupportInitialize)this.spnSpawnMinDelay).EndInit();
			((ISupportInitialize)this.spnSpawnTeam).EndInit();
			((ISupportInitialize)this.spnSpawnMaxDelay).EndInit();
			base.ResumeLayout(false);
		}

		public void SaveTransferServerConfiguration()
		{
			if (this._HKCUKey != null && this._Editor != null)
			{
				this._HKCUKey.SetValue(this.AppTransferServerAddressValue, this._Editor._TransferDialog.txtTransferServerAddress.Text);
				try
				{
					this._HKCUKey.SetValue(this.AppTransferServerPortValue, int.Parse(this._Editor._TransferDialog.txtTransferServerPort.Text));
				}
				catch
				{
				}
			}
		}

		public void SaveWindowConfiguration()
		{
			if (this._HKCUKey != null && this._Editor != null)
			{
				RegistryKey registryKey = this._HKCUKey;
				string appStartingXValue = this.AppStartingXValue;
				Point location = this._Editor.Location;
				registryKey.SetValue(appStartingXValue, location.X);
				RegistryKey registryKey1 = this._HKCUKey;
				string appStartingYValue = this.AppStartingYValue;
				location = this._Editor.Location;
				registryKey1.SetValue(appStartingYValue, location.Y);
				this._HKCUKey.SetValue(this.AppStartingWidthValue, this._Editor.Width);
				this._HKCUKey.SetValue(this.AppStartingHeightValue, this._Editor.Height);
			}
		}

		private void SetClientWindowName()
		{
			string lower = Path.GetFileName(this.txtUltimaClient.Text).ToLower();
			"client.exe";
			string str = lower;
			string str1 = str;
			if (str != null)
			{
				str1 = string.IsInterned(str1);
				if (str1 == "uotd.exe")
				{
					this.CfgUoClientWindowValue = "Ultima Online Third Dawn";
					this.txtClientWindow.Text = this.CfgUoClientWindowValue;
					return;
				}
				else
				{
					if (str1 != "client.exe")
					{
						this.CfgUoClientWindowValue = "???";
						this.txtClientWindow.Text = this.CfgUoClientWindowValue;
						return;
					}
					this.CfgUoClientWindowValue = "Ultima Online";
					this.txtClientWindow.Text = this.CfgUoClientWindowValue;
					return;
				}
			}
			this.CfgUoClientWindowValue = "???";
			this.txtClientWindow.Text = this.CfgUoClientWindowValue;
		}
	}
}