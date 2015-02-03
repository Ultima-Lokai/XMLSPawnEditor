using AxUOMAPLib;
using Server.Engines.XmlSpawner2;
using SpawnEditor2.Forms;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using Ultima;

namespace SpawnEditor2
{
	public class SpawnEditor : Form
	{
		private const string SpawnEditorTitle = "Spawn Editor 2";

		private const string SpawnDataSetName = "Spawns";

		private const string SpawnTablePointName = "Points";

		private const string SpawnTableObjectName = "Objects";

		private readonly string DefaultZoomLevelText = "Zoom Level:  ";

		private readonly string SpawnPackFile = "SpawnPacks.dat";

		private readonly string HelpFile = "ReadMe.htm";

		internal Configure _CfgDialog;

		internal TransferServerSettings _TransferDialog;

		internal SpawnerFilters _SpawnerFilters;

		private Type[] _RunUOScriptTypes;

		internal SpawnEditor.SelectionWindow _SelectionWindow = null;

		private bool GoToSelected = false;

		private bool RightMouseDown = false;

		private bool _ExtendedDiagnostics = false;

		private static bool _Debug;

		private static ArrayList AssemblyList;

		internal ObjectData[] MobLocArray;

		internal ObjectData[] PlayerLocArray;

		internal ObjectData[] ItemLocArray;

		internal MapLocation[] MapLoc = new MapLocation[5];

		public Guid SessionID = Guid.NewGuid();

		internal string StartingDirectory;

		private bool MouseResize = false;

		private bool Tracking = false;

		private SpawnPoint SelectedSpawn;

		private SpawnPointNode SelectedSpawnNode;

		private DateTime RightMouseDownStart;

		private MapLocation MyLocation = new MapLocation();

		internal AxUOMap axUOMap;

		private ToolTip ttpSpawnInfo;

		private Panel pnlControls;

		internal TrackBar trkZoom;

		private CheckBox chkDrawStatics;

		private GroupBox grpMapControl;

		private CheckedListBox clbRunUOTypes;

		private Label lblTotalTypesLoaded;

		private RadioButton radShowAll;

		private RadioButton radShowItemsOnly;

		private RadioButton radShowMobilesOnly;

		private Label lblTotalSpawn;

		private Button btnLoadSpawn;

		private Button btnSaveSpawn;

		internal TreeView tvwSpawnPoints;

		private Button btnResetTypes;

		private Button btnMergeSpawn;

		private OpenFileDialog ofdLoadFile;

		private SaveFileDialog sfdSaveFile;

		private System.Windows.Forms.ContextMenu mncSpawns;

		private GroupBox grpSpawnTypes;

		private GroupBox grpSpawnList;

		private StatusBar stbMain;

		private MenuItem menuItem3;

		private MenuItem mniDeleteAllSpawns;

		private MenuItem mniDeleteSpawn;

		private CheckBox chkShowMapTip;

		private CheckBox chkShowSpawns;

		internal ComboBox cbxMap;

		private CheckBox chkSyncUO;

		private System.Windows.Forms.ContextMenu mncLoad;

		private MenuItem mniForceLoad;

		private System.Windows.Forms.ContextMenu mncMerge;

		private MenuItem mniForceMerge;

		private GroupBox grpSpawnEntries;

		internal GroupBox grpSpawnEdit;

		private CheckBox chkHomeRangeIsRelative;

		private Button btnMove;

		private Button btnRestoreSpawnDefaults;

		private Button btnDeleteSpawn;

		internal Button btnUpdateSpawn;

		private Label lblMaxDelay;

		private CheckBox chkRunning;

		private Label lblHomeRange;

		private NumericUpDown spnMaxCount;

		private TextBox txtName;

		private NumericUpDown spnHomeRange;

		private Label lblTeam;

		private Label lblMaxCount;

		private NumericUpDown spnMinDelay;

		private NumericUpDown spnTeam;

		private CheckBox chkGroup;

		private NumericUpDown spnMaxDelay;

		private Label lblMinDelay;

		private NumericUpDown entryMax1;

		private Label label1;

		private Label label2;

		private Label label3;

		private Label label4;

		private VScrollBar vScrollBar1;

		private Label label5;

		private Label label6;

		private Label label7;

		private Label label8;

		private Label label9;

		private MainMenu mainMenu1;

		private MenuItem menuItem8;

		private MenuItem menuItem9;

		private Label label11;

		private Label label12;

		private Label label14;

		private Label label15;

		private Label label16;

		private Label label17;

		private Label label18;

		private Label label19;

		private Label label20;

		private Label label21;

		private Label label22;

		private Label label23;

		private Label label24;

		private Panel panel1;

		private Panel panel3;

		private System.Windows.Forms.ContextMenu editEntryMenu1;

		private Label label25;

		private Label label26;

		private Label label27;

		private Label label28;

		internal NumericUpDown spnSpawnRange;

		private NumericUpDown spnProximityRange;

		private CheckBox chkGameTOD;

		private CheckBox chkRealTOD;

		private CheckBox chkAllowGhost;

		private CheckBox chkSmartSpawning;

		private CheckBox chkSequentialSpawn;

		private CheckBox chkSpawnOnTrigger;

		private NumericUpDown spnDuration;

		private TextBox textSkillTrigger;

		private TextBox textSpeechTrigger;

		private TextBox textProximityMsg;

		private TextBox textPlayerTrigProp;

		private TextBox textNoTriggerOnCarried;

		private TextBox textTriggerOnCarried;

		private TextBox textTrigObjectProp;

		private NumericUpDown spnMinRefract;

		private NumericUpDown spnTODStart;

		private NumericUpDown spnMaxRefract;

		private NumericUpDown spnDespawn;

		private NumericUpDown spnTODEnd;

		private TextBox entryMaxD8;

		private TextBox entryMaxD7;

		private TextBox entryMaxD6;

		private TextBox entryMaxD5;

		private TextBox entryMaxD4;

		private TextBox entryMaxD3;

		private TextBox entryMaxD2;

		private TextBox entryMaxD1;

		private TextBox entryMinD8;

		private TextBox entryMinD7;

		private TextBox entryMinD6;

		private TextBox entryMinD5;

		private TextBox entryMinD4;

		private TextBox entryMinD3;

		private TextBox entryMinD2;

		private TextBox entryMinD1;

		private TextBox entryKills8;

		private TextBox entryKills7;

		private TextBox entryKills6;

		private TextBox entryKills5;

		private TextBox entryKills4;

		private TextBox entryKills3;

		private TextBox entryKills2;

		private TextBox entryKills1;

		private TextBox entryReset8;

		private TextBox entryReset7;

		private TextBox entryReset6;

		private TextBox entryReset5;

		private TextBox entryReset4;

		private TextBox entryReset3;

		private TextBox entryReset2;

		private TextBox entryReset1;

		private TextBox entryTo8;

		private TextBox entrySub8;

		private CheckBox chkRK8;

		private NumericUpDown entryMax8;

		private Button btnEntryEdit8;

		private TextBox entryText8;

		private CheckBox chkClr8;

		private TextBox entryTo7;

		private TextBox entrySub7;

		private CheckBox chkRK7;

		private NumericUpDown entryMax7;

		private Button btnEntryEdit7;

		private TextBox entryText7;

		private CheckBox chkClr7;

		private TextBox entryTo6;

		private TextBox entrySub6;

		private CheckBox chkRK6;

		private NumericUpDown entryMax6;

		private Button btnEntryEdit6;

		private TextBox entryText6;

		private CheckBox chkClr6;

		private TextBox entryTo5;

		private TextBox entrySub5;

		private CheckBox chkRK5;

		private NumericUpDown entryMax5;

		private Button btnEntryEdit5;

		private TextBox entryText5;

		private CheckBox chkClr5;

		private TextBox entryTo4;

		private TextBox entrySub4;

		private CheckBox chkRK4;

		private NumericUpDown entryMax4;

		private Button btnEntryEdit4;

		private TextBox entryText4;

		private CheckBox chkClr4;

		private TextBox entryTo3;

		private TextBox entrySub3;

		private CheckBox chkRK3;

		private NumericUpDown entryMax3;

		private Button btnEntryEdit3;

		private TextBox entryText3;

		private CheckBox chkClr3;

		private TextBox entryTo2;

		private TextBox entrySub2;

		private CheckBox chkRK2;

		private NumericUpDown entryMax2;

		private Button btnEntryEdit2;

		private TextBox entryText2;

		private CheckBox chkClr2;

		private TextBox entryTo1;

		private TextBox entrySub1;

		private CheckBox chkRK1;

		private Button btnEntryEdit1;

		private TextBox entryText1;

		private CheckBox chkClr1;

		private NumericUpDown spnProximitySnd;

		private NumericUpDown spnKillReset;

		private GroupBox groupTemplateList;

		private TreeView tvwTemplates;

		private Label label29;

		private Button btnLoadTemplate;

		private Button btnMergeTemplate;

		private Button btnSaveTemplate;

		private CheckBox chkDetails;

		private Button btnGo;

		private CheckBox chkInContainer;

		private CheckBox chkTracking;

		private TabControl tabControl1;

		private GroupBox groupBox1;

		private Label label13;

		private TextBox textMobTriggerName;

		private Label label10;

		private TextBox textMobTrigProp;

		private Label label31;

		private NumericUpDown spnTriggerProbability;

		private Label label32;

		private NumericUpDown spnStackAmount;

		private Label labelContainerZ;

		private Label labelContainerY;

		private CheckBox chkExternalTriggering;

		private Label label33;

		private Label label34;

		private Label label35;

		private Label label36;

		private Label label37;

		private TextBox textRegionName;

		private TextBox textWayPoint;

		private TextBox textConfigFile;

		private TextBox textTrigObjectName;

		private TextBox textSetObjectName;

		private Label labelContainerX;

		private NumericUpDown spnContainerX;

		private NumericUpDown spnContainerY;

		private NumericUpDown spnContainerZ;

		private System.Windows.Forms.ContextMenu deleteEntry;

		private MenuItem menuItem1;

		private MenuItem menuItem2;

		private MenuItem menuItem4;

		private CheckBox chkLockSpawn;

		private CheckBox chkSnapRegion;

		private TabControl tabControl2;

		private TabPage tabPage3;

		private TabPage tabPage4;

		private TabPage tabPage5;

		private TreeView treeRegionView;

		private TreeView treeGoView;

		private HelpProvider helpProvider1;

		internal CheckBox checkSpawnFilter;

		private TabPage tabBasic;

		private TabPage tabAdvanced;

		private TabPage tabSpawnTypes;

		private GroupBox groupBox2;

		private Button button1;

		private Label label39;

		private TextBox textSpawnPackName;

		private GroupBox groupBox3;

		private CheckedListBox clbSpawnPack;

		internal Button btnAddToSpawnPack;

		internal Button btnUpdateFromSpawnPack;

		internal Button btnUpdateSpawnPacks;

		private TreeView tvwSpawnPacks;

		private System.Windows.Forms.ContextMenu mcnSpawnPack;

		private MenuItem mniDeleteType;

		private MenuItem mniDeleteAllTypes;

		private System.Windows.Forms.ContextMenu mcnSpawnPacks;

		private MenuItem mniDeletePack;

		private MenuItem menuItem5;

		private MenuItem menuItem6;

		private MenuItem menuItem7;

		private OpenFileDialog openSpawnPacks;

		private SaveFileDialog saveSpawnPacks;

		private MenuItem menuItem10;

		private MenuItem menuItem11;

		private SaveFileDialog exportAllSpawnTypes;

		private OpenFileDialog importAllSpawnTypes;

		private ComboBox cbxShade;

		private CheckBox chkShade;

		private MenuItem menuItem12;

		private OpenFileDialog importMapFile;

		private MenuItem menuItem13;

		private OpenFileDialog importMSFFile;

		private MenuItem menuItem14;

		private MenuItem menuItem16;

		private MenuItem menuItem18;

		private MenuItem mniAlwaysOnTop;

		private MenuItem menuItem15;

		private MenuItem menuItem17;

		private TabControl tabControl3;

		private TabPage tabMapSettings;

		private TextBox txtNotes;

		private Label label44;

		internal ProgressBar progressBar1;

		internal Label lblTransferStatus;

		internal Label lblTrkMax;

		internal Label lblTrkMin;

		private Button btnSendSpawn;

		private MenuItem mniUnloadSpawners;

		private MenuItem menuItem19;

		private MenuItem menuItem20;

		private MenuItem menuItem21;

		private Button btnSendSingleSpawner;

		private System.Windows.Forms.ContextMenu unloadSpawners;

		private System.Windows.Forms.ContextMenu unloadSingleSpawner;

		private MenuItem menuItem23;

		private MenuItem mniUnloadSingleSpawner;

		private MenuItem menuItem22;

		internal MenuItem mniDeleteInSelectionWindow;

		internal MenuItem mniDeleteNotSelected;

		private System.Windows.Forms.ContextMenu highlightDetail;

		private MenuItem menuItem24;

		private MenuItem menuItem25;

		private MenuItem mniToolbarDeleteAllSpawns;

		private MenuItem mniDisplayFilterSettings;

		private Button btnFilterSettings;

		private MenuItem mniDeleteAllFiltered;

		private MenuItem mniDeleteAllUnfiltered;

		private MenuItem mniModifyInSelectionWindow;

		private Label label30;

		private NumericUpDown entryPer1;

		private NumericUpDown entryPer2;

		private NumericUpDown entryPer3;

		private NumericUpDown entryPer4;

		private NumericUpDown entryPer5;

		private NumericUpDown entryPer6;

		private NumericUpDown entryPer7;

		private NumericUpDown entryPer8;

		private MenuItem mniModifiedUnfiltered;

		private IContainer components;

		private static Hashtable typeHash;

		private System.Drawing.Size savewindowsize = System.Drawing.Size.Empty;

		private System.Drawing.Size savemapsize = System.Drawing.Size.Empty;

		private System.Drawing.Size savelistsize = System.Drawing.Size.Empty;

		private System.Drawing.Size savepanelsize = System.Drawing.Size.Empty;

		private int entrychanged = 0;

		private string changedentrystring = null;

		private bool namechanged = false;

		private string changednamestring = null;

		private bool maxvaluechanged = false;

		private int changedmaxvalue = 0;

		private Hashtable ControlModHash = new Hashtable();

		public bool SpawnLocationLocked
		{
			get
			{
				return this.chkLockSpawn.Checked;
			}
		}

		static SpawnEditor()
		{
			SpawnEditor._Debug = false;
			SpawnEditor.AssemblyList = new ArrayList();
			SpawnEditor.typeHash = new Hashtable();
		}

		public SpawnEditor()
		{
			SpawnEditor.Debug("");
			SpawnEditor.Debug("=======================================");
			SpawnEditor.Debug("Starting");
			this.InitializeMapCenters();
			this.InitializeComponent();
			SpawnEditor.Debug("Initialized");
			this.SmallWindow();
			SpawnEditor.Debug("WindowConfigured");
			this._CfgDialog = new Configure(this);
			SpawnEditor.Debug("ConfigurationDialog");
			this._TransferDialog = new TransferServerSettings(this);
			SpawnEditor.Debug("TransferDialog");
			this._SpawnerFilters = new SpawnerFilters(this);
			SpawnEditor.Debug("SpawnerFilters");
		}

		public void ActivateTracking()
		{
			Client.Calibrate();
			SpawnEditor.TrackerThread trackerThread = new SpawnEditor.TrackerThread(this);
			Thread thread = new Thread(new ThreadStart(trackerThread.TrackerThreadMain))
			{
				Name = "Tracker Thread"
			};
			thread.Start();
		}

		private void AddEntryOnChange()
		{
			if (this.entrychanged > 0)
			{
				if (this.HasEntry(this.SelectedSpawn, this.entrychanged))
				{
					this.UpdateSpawnEntries();
					this.UpdateSpawnNode();
				}
				else
				{
					this.UpdateSpawnEntries();
					this.UpdateSpawnNode();
					if (this.SelectedSpawn != null)
					{
						this.SelectedSpawn.SpawnObjects.Add(new SpawnObject(this.changedentrystring, 1));
					}
					this.UpdateSpawnerMaxCount();
					this.DisplaySpawnEntries();
					this.UpdateSpawnNode();
				}
				this.entrychanged = 0;
				this.changedentrystring = null;
			}
		}

		private void ApplyModifications(SpawnPoint spawn)
		{
			if (this.ControlHasBeenSelected(this.txtName.Name))
			{
				spawn.SpawnName = this.txtName.Text;
			}
			if (this.ControlHasBeenSelected(this.spnHomeRange.Name))
			{
				spawn.HomeRange = (int)this.spnHomeRange.Value;
			}
			if (this.ControlHasBeenSelected(this.spnMaxCount.Name))
			{
				spawn.MaxCount = (int)this.spnMaxCount.Value;
			}
			if (this.ControlHasBeenSelected(this.spnMinDelay.Name))
			{
				spawn.MinDelay = (double)((double)this.spnMinDelay.Value);
			}
			if (this.ControlHasBeenSelected(this.spnMaxDelay.Name))
			{
				spawn.MaxDelay = (double)((double)this.spnMaxDelay.Value);
			}
			if (this.ControlHasBeenSelected(this.spnTeam.Name))
			{
				spawn.Team = (int)this.spnTeam.Value;
			}
			if (this.ControlHasBeenSelected(this.spnSpawnRange.Name))
			{
				spawn.SpawnRange = (int)this.spnSpawnRange.Value;
			}
			if (this.ControlHasBeenSelected(this.spnProximityRange.Name))
			{
				spawn.ProximityRange = (int)this.spnProximityRange.Value;
			}
			if (this.ControlHasBeenSelected(this.spnDuration.Name))
			{
				spawn.Duration = (double)((double)this.spnDuration.Value);
			}
			if (this.ControlHasBeenSelected(this.spnDespawn.Name))
			{
				spawn.Despawn = (double)((double)this.spnDespawn.Value);
			}
			if (this.ControlHasBeenSelected(this.spnMinRefract.Name))
			{
				spawn.MinRefract = (double)((double)this.spnMinRefract.Value);
			}
			if (this.ControlHasBeenSelected(this.spnMaxRefract.Name))
			{
				spawn.MaxRefract = (double)((double)this.spnMaxRefract.Value);
			}
			if (this.ControlHasBeenSelected(this.spnTODStart.Name))
			{
				spawn.TODStart = (double)((double)this.spnTODStart.Value);
			}
			if (this.ControlHasBeenSelected(this.spnTODEnd.Name))
			{
				spawn.TODEnd = (double)((double)this.spnTODEnd.Value);
			}
			if (this.ControlHasBeenSelected(this.spnKillReset.Name))
			{
				spawn.KillReset = (int)this.spnKillReset.Value;
			}
			if (this.ControlHasBeenSelected(this.spnProximitySnd.Name))
			{
				spawn.ProximitySnd = (int)this.spnProximitySnd.Value;
			}
			if (this.ControlHasBeenSelected(this.chkGroup.Name))
			{
				spawn.Group = this.chkGroup.Checked;
			}
			if (this.ControlHasBeenSelected(this.chkRunning.Name))
			{
				spawn.Running = this.chkRunning.Checked;
			}
			if (this.ControlHasBeenSelected(this.chkHomeRangeIsRelative.Name))
			{
				spawn.RelativeHome = this.chkHomeRangeIsRelative.Checked;
			}
			if (this.ControlHasBeenSelected(this.chkInContainer.Name))
			{
				spawn.InContainer = this.chkInContainer.Checked;
			}
			if (this.ControlHasBeenSelected(this.chkRealTOD.Name))
			{
				spawn.RealTOD = this.chkRealTOD.Checked;
			}
			if (this.ControlHasBeenSelected(this.chkGameTOD.Name))
			{
				spawn.GameTOD = this.chkGameTOD.Checked;
			}
			if (this.ControlHasBeenSelected(this.chkSpawnOnTrigger.Name))
			{
				spawn.SpawnOnTrigger = this.chkSpawnOnTrigger.Checked;
			}
			if (this.ControlHasBeenSelected(this.chkSequentialSpawn.Name))
			{
				spawn.SequentialSpawn = this.chkSequentialSpawn.Checked;
			}
			if (this.ControlHasBeenSelected(this.chkSmartSpawning.Name))
			{
				spawn.SmartSpawning = this.chkSmartSpawning.Checked;
			}
		}

		internal void ApplySpawnFilter()
		{
			if (this.tvwSpawnPoints == null || this.tvwSpawnPoints.Nodes == null)
			{
				return;
			}
			foreach (SpawnPointNode node in this.tvwSpawnPoints.Nodes)
			{
				if (this._SpawnerFilters.HasMatch(node.Spawn))
				{
					node.Filtered = false;
				}
				else
				{
					node.Filtered = true;
				}
			}
		}

		internal void AssignCenter(short X, short Y, short facet)
		{
			this.MapLoc[facet].X = X;
			this.MapLoc[facet].Y = Y;
			this.axUOMap.SetCenter(X, Y);
		}

		private void axUOMap_MouseDownEvent(object sender, _DUOMapEvents_MouseDownEvent e)
		{
			short mapX = this.axUOMap.CtrlToMapX((short)e.x);
			short mapY = this.axUOMap.CtrlToMapY((short)e.y);
			short mapHeight = this.axUOMap.GetMapHeight(mapX, mapY);
			this.RightMouseDown = false;
			this.RightMouseDownStart = DateTime.MaxValue;
			if (e.button == 1)
			{
				if (this.GoToSelected)
				{
					this.SendGoCommand(mapX, mapY, mapHeight, (WorldMap)((int)((WorldMap)this.cbxMap.SelectedItem)));
					this.GoToSelected = false;
				}
				if (this._SelectionWindow != null && this._SelectionWindow.Index > -1)
				{
					this.axUOMap.RemoveDrawRectAt(this._SelectionWindow.Index);
					this.ClearSelectionWindow();
				}
				this._SelectionWindow = new SpawnEditor.SelectionWindow()
				{
					X = mapX,
					Y = mapY,
					SX = mapX,
					SY = mapY,
					Index = this.axUOMap.AddDrawRect(this._SelectionWindow.X, this._SelectionWindow.Y, 1, 1, 2, 16777215)
				};
				this.EnableSelectionWindowOption(true);
			}
			else if (e.button == 2)
			{
				this.RightMouseDown = true;
				this.RightMouseDownStart = DateTime.Now;
			}
			this.RefreshSpawnPoints();
		}

		private void axUOMap_MouseMoveEvent(object sender, _DUOMapEvents_MouseMoveEvent e)
		{
			Rectangle bounds;
			short mapX = this.axUOMap.CtrlToMapX((short)e.x);
			short mapY = this.axUOMap.CtrlToMapY((short)e.y);
			short mapHeight = this.axUOMap.GetMapHeight(mapX, mapY);
			WorldMap selectedItem = (WorldMap)((int)((WorldMap)this.cbxMap.SelectedItem));
			if (e.button == 0)
			{
				this.trkZoom.Focus();
				this.MouseResize = false;
				string empty = string.Empty;
				bool flag = false;
				short value = (short)(6 - (short)this.trkZoom.Value);
				if (this._TransferDialog.chkShowTips.Checked)
				{
					if (this._TransferDialog.chkShowCreatures.Checked && this.MobLocArray != null)
					{
						int num = 0;
						while (num < (int)this.MobLocArray.Length)
						{
							if (this.MobLocArray[num].Map != (int)selectedItem || mapX >= this.MobLocArray[num].X + value || mapX <= this.MobLocArray[num].X - value || mapY >= this.MobLocArray[num].Y + value || mapY <= this.MobLocArray[num].Y - value)
							{
								num++;
							}
							else
							{
								empty = this.MobLocArray[num].Name;
								flag = true;
								break;
							}
						}
					}
					if (this._TransferDialog.chkShowPlayers.Checked && this.PlayerLocArray != null && !flag)
					{
						int num1 = 0;
						while (num1 < (int)this.PlayerLocArray.Length)
						{
							if (this.PlayerLocArray[num1].Map != (int)selectedItem || mapX >= this.PlayerLocArray[num1].X + value || mapX <= this.PlayerLocArray[num1].X - value || mapY >= this.PlayerLocArray[num1].Y + value || mapY <= this.PlayerLocArray[num1].Y - value)
							{
								num1++;
							}
							else
							{
								empty = this.PlayerLocArray[num1].Name;
								flag = true;
								break;
							}
						}
					}
					if (this._TransferDialog.chkShowItems.Checked && this.ItemLocArray != null && !flag)
					{
						int num2 = 0;
						while (num2 < (int)this.ItemLocArray.Length)
						{
							if (this.ItemLocArray[num2].Map != (int)selectedItem || mapX >= this.ItemLocArray[num2].X + value || mapX <= this.ItemLocArray[num2].X - value || mapY >= this.ItemLocArray[num2].Y + value || mapY <= this.ItemLocArray[num2].Y - value)
							{
								num2++;
							}
							else
							{
								empty = this.ItemLocArray[num2].Name;
								flag = true;
								break;
							}
						}
					}
				}
				if (this.chkShowMapTip.Checked && this.chkShowSpawns.Checked && !flag)
				{
					ArrayList arrayLists = new ArrayList(this.tvwSpawnPoints.Nodes);
					arrayLists.Sort(new SpawnPointAreaComparer());
					foreach (SpawnPointNode arrayList in arrayLists)
					{
						if ((int)arrayList.Spawn.Map != (int)((WorldMap)this.cbxMap.SelectedItem) || arrayList.Filtered || !arrayList.Spawn.IsSameArea(mapX, mapY, 1))
						{
							continue;
						}
						AxUOMap axUOMap = this.axUOMap;
						bounds = arrayList.Spawn.Bounds;
						int ctrlX = axUOMap.MapToCtrlX((short)bounds.X);
						AxUOMap axUOMap1 = this.axUOMap;
						bounds = arrayList.Spawn.Bounds;
						int ctrlY = axUOMap1.MapToCtrlY((short)bounds.Y);
						AxUOMap axUOMap2 = this.axUOMap;
						bounds = arrayList.Spawn.Bounds;
						int x = bounds.X;
						bounds = arrayList.Spawn.Bounds;
						int ctrlX1 = axUOMap2.MapToCtrlX((short)(x + bounds.Width)) - ctrlX;
						AxUOMap axUOMap3 = this.axUOMap;
						bounds = arrayList.Spawn.Bounds;
						int y = bounds.Y;
						bounds = arrayList.Spawn.Bounds;
						int ctrlY1 = axUOMap3.MapToCtrlY((short)(y + bounds.Height)) - ctrlY;
						if (arrayList.Spawn != this.SelectedSpawn || (double)e.x <= (double)ctrlX + (double)ctrlX1 * 0.8 || e.x >= ctrlX + ctrlX1 || (double)e.y <= (double)ctrlY + (double)ctrlY1 * 0.8 || e.y >= ctrlY + ctrlY1)
						{
							empty = arrayList.Spawn.ToString();
							break;
						}
						else
						{
							empty = "Resize";
							this.MouseResize = true;
							break;
						}
					}
				}
				this.ttpSpawnInfo.SetToolTip(this.axUOMap, empty);
				if (this._SelectionWindow == null)
				{
					this.stbMain.Text = string.Format("[X={0} Y={1} H={2}]", mapX, mapY, mapHeight);
					return;
				}
			}
			else if (e.button == 1)
			{
				if (this._SelectionWindow != null)
				{
					if (this._SelectionWindow.Index > -1)
					{
						this.axUOMap.RemoveDrawRectAt(this._SelectionWindow.Index);
						this._SelectionWindow.Index = -1;
					}
					short sX = (short)(mapX - this._SelectionWindow.SX);
					short sY = (short)(mapY - this._SelectionWindow.SY);
					this._SelectionWindow.Width = Math.Abs(sX);
					this._SelectionWindow.Height = Math.Abs(sY);
					this._SelectionWindow.X = this._SelectionWindow.SX;
					this._SelectionWindow.Y = this._SelectionWindow.SY;
					if (sY < 0)
					{
						this._SelectionWindow.Y = (short)(this._SelectionWindow.SY + sY);
					}
					if (sX < 0)
					{
						this._SelectionWindow.X = (short)(this._SelectionWindow.SX + sX);
					}
					foreach (SpawnPointNode node in this.tvwSpawnPoints.Nodes)
					{
						node.Spawn.IsSelected = false;
					}
					this.txtName.Text = string.Concat(this._CfgDialog.CfgSpawnNameValue, this.tvwSpawnPoints.Nodes.Count);
					this.txtName.Refresh();
					this._SelectionWindow.Index = this.axUOMap.AddDrawRect(this._SelectionWindow.X, this._SelectionWindow.Y, this._SelectionWindow.Width, this._SelectionWindow.Height, 2, 16777215);
					StatusBar statusBar = this.stbMain;
					object[] objArray = new object[] { this._SelectionWindow.X, this._SelectionWindow.Y, (int)(this._SelectionWindow.X + this._SelectionWindow.Width), (int)(this._SelectionWindow.Y + this._SelectionWindow.Height), this._SelectionWindow.Width, this._SelectionWindow.Height };
					statusBar.Text = string.Format("[X1={0} Y1={1}] TO [X2={2} Y2={3}] (Width={4}, Height={5})", objArray);
					return;
				}
			}
			else if (e.button == 2)
			{
				SpawnPoint spawn = null;
				foreach (SpawnPointNode spawnPointNode in this.tvwSpawnPoints.Nodes)
				{
					if ((int)spawnPointNode.Spawn.Map != (int)((WorldMap)this.cbxMap.SelectedItem) || !spawnPointNode.Spawn.IsSelected)
					{
						continue;
					}
					spawn = spawnPointNode.Spawn;
					break;
				}
				if (spawn != null)
				{
					int width = spawn.Bounds.Width;
					int height = spawn.Bounds.Height;
					int x1 = spawn.Bounds.X;
					int y1 = spawn.Bounds.Y;
					if (this.MouseResize)
					{
						spawn.Bounds = new Rectangle(x1, y1, mapX - x1 + 1, mapY - y1 + 1);
						if (!this.SpawnLocationLocked)
						{
							int x2 = spawn.Bounds.X;
							bounds = spawn.Bounds;
							spawn.CentreX = (short)(x2 + bounds.Width / 2);
							int y2 = spawn.Bounds.Y;
							bounds = spawn.Bounds;
							spawn.CentreY = (short)(y2 + bounds.Height / 2);
						}
						this.spnSpawnRange.Value = new decimal(-1);
					}
					else if ((DateTime.Now - this.RightMouseDownStart) > TimeSpan.FromSeconds(0.4))
					{
						spawn.Bounds = new Rectangle(mapX - width / 2, mapY - height / 2, width, height);
						if (!this.SpawnLocationLocked)
						{
							int x3 = spawn.Bounds.X;
							bounds = spawn.Bounds;
							spawn.CentreX = (short)(x3 + bounds.Width / 2);
							int y3 = spawn.Bounds.Y;
							bounds = spawn.Bounds;
							spawn.CentreY = (short)(y3 + bounds.Height / 2);
						}
					}
					this.RefreshSpawnPoints();
				}
			}
		}

		private void axUOMap_MouseUpEvent(object sender, _DUOMapEvents_MouseUpEvent e)
		{
			short mapX = this.axUOMap.CtrlToMapX((short)e.x);
			short mapY = this.axUOMap.CtrlToMapY((short)e.y);
			this.axUOMap.GetMapHeight(mapX, mapY);
			if (this.RightMouseDown)
			{
				if (this._SelectionWindow != null && this._SelectionWindow.IsWithinWindow(mapX, mapY))
				{
					this.txtName.Text = this.txtName.Text.Trim();
					this.spnSpawnRange.Value = new decimal(-1);
					if (this.txtName.Text.Length == 0)
					{
						MessageBox.Show(this, "You must specify a name for the spawner!", "Spawn Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
						return;
					}
					foreach (SpawnPointNode node in this.tvwSpawnPoints.Nodes)
					{
						node.Spawn.IsSelected = false;
					}
					SpawnPointNode spawnPointNode = new SpawnPointNode(new SpawnPoint(Guid.NewGuid(), (WorldMap)((int)((WorldMap)this.cbxMap.SelectedItem)), this._SelectionWindow.Bounds));
					this.SetSpawn(spawnPointNode, false);
					spawnPointNode.Spawn.CentreZ = -32768;
					this.tvwSpawnPoints.Nodes.Add(spawnPointNode);
					this.lblTotalSpawn.Text = string.Concat("Total Spawns = ", this.tvwSpawnPoints.Nodes.Count);
					this.ClearSelectionWindow();
					this.SelectedSpawn = spawnPointNode.Spawn;
					this.DisplaySpawnDetails(this.SelectedSpawn);
				}
				foreach (SpawnPointNode node1 in this.tvwSpawnPoints.Nodes)
				{
					node1.Spawn.IsSelected = false;
				}
				ArrayList arrayLists = new ArrayList(this.tvwSpawnPoints.Nodes);
				arrayLists.Sort(new SpawnPointAreaComparer());
				foreach (SpawnPointNode arrayList in arrayLists)
				{
					if ((int)arrayList.Spawn.Map != (int)((WorldMap)this.cbxMap.SelectedItem) || arrayList.Filtered || !arrayList.Spawn.IsSameArea(mapX, mapY))
					{
						continue;
					}
					arrayList.Spawn.IsSelected = true;
					this.SelectedSpawn = arrayList.Spawn;
					this.SendGoCommand(arrayList.Spawn);
					this.DisplaySpawnDetails(this.SelectedSpawn);
					this.DisplaySpawnEntries();
					this.tvwSpawnPoints.SelectedNode = arrayList;
					this.tvwSpawnPoints.SelectedNode.EnsureVisible();
					this.SetSelectedSpawnTypes();
					break;
				}
				this.RefreshSpawnPoints();
			}
			else if (this._SelectionWindow != null && this._SelectionWindow.SX == mapX && this._SelectionWindow.SY == mapY)
			{
				if (this._SelectionWindow.Index > -1)
				{
					this.axUOMap.RemoveDrawRectAt(this._SelectionWindow.Index);
					this.ClearSelectionWindow();
				}
				this.AssignCenter(mapX, mapY, (short)this.cbxMap.SelectedIndex);
				this.RefreshSpawnPoints();
			}
			this.trkZoom.Focus();
		}

		private void btnAddToSpawnPack_Click(object sender, EventArgs e)
		{
			this.clbSpawnPack.Sorted = false;
			foreach (string checkedItem in this.clbRunUOTypes.CheckedItems)
			{
				bool flag = false;
				foreach (string item in this.clbSpawnPack.Items)
				{
					if (checkedItem.ToUpper() != item.ToUpper())
					{
						continue;
					}
					flag = true;
					break;
				}
				if (flag)
				{
					continue;
				}
				this.clbSpawnPack.Items.Add(checkedItem);
			}
			this.clbSpawnPack.Sorted = true;
		}

		private void btnConfigure_Click(object sender, EventArgs e)
		{
			this.UpdateMyLocation();
			this.RefreshSpawnPoints();
		}

		private void btnDeleteSpawn_Click(object sender, EventArgs e)
		{
			this.mniDeleteSpawn_Click(sender, e);
		}

		private void btnEntryEdit1_Click(object sender, EventArgs e)
		{
			EntryEdit entryEdit = new EntryEdit(this)
			{
				Text = base.Name
			};
			entryEdit.textEntryEdit.Text = this.entryText1.Text;
			if (entryEdit.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
			{
				this.entryText1.Text = entryEdit.textEntryEdit.Text;
			}
		}

		private void btnEntryEdit2_Click(object sender, EventArgs e)
		{
			EntryEdit entryEdit = new EntryEdit(this)
			{
				Text = base.Name
			};
			entryEdit.textEntryEdit.Text = this.entryText2.Text;
			if (entryEdit.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
			{
				this.entryText2.Text = entryEdit.textEntryEdit.Text;
			}
		}

		private void btnEntryEdit3_Click(object sender, EventArgs e)
		{
			EntryEdit entryEdit = new EntryEdit(this)
			{
				Text = base.Name
			};
			entryEdit.textEntryEdit.Text = this.entryText3.Text;
			if (entryEdit.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
			{
				this.entryText3.Text = entryEdit.textEntryEdit.Text;
			}
		}

		private void btnEntryEdit4_Click(object sender, EventArgs e)
		{
			EntryEdit entryEdit = new EntryEdit(this)
			{
				Text = base.Name
			};
			entryEdit.textEntryEdit.Text = this.entryText4.Text;
			if (entryEdit.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
			{
				this.entryText4.Text = entryEdit.textEntryEdit.Text;
			}
		}

		private void btnEntryEdit5_Click(object sender, EventArgs e)
		{
			EntryEdit entryEdit = new EntryEdit(this)
			{
				Text = base.Name
			};
			entryEdit.textEntryEdit.Text = this.entryText5.Text;
			if (entryEdit.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
			{
				this.entryText5.Text = entryEdit.textEntryEdit.Text;
			}
		}

		private void btnEntryEdit6_Click(object sender, EventArgs e)
		{
			EntryEdit entryEdit = new EntryEdit(this)
			{
				Text = base.Name
			};
			entryEdit.textEntryEdit.Text = this.entryText6.Text;
			if (entryEdit.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
			{
				this.entryText6.Text = entryEdit.textEntryEdit.Text;
			}
		}

		private void btnEntryEdit7_Click(object sender, EventArgs e)
		{
			EntryEdit entryEdit = new EntryEdit(this)
			{
				Text = base.Name
			};
			entryEdit.textEntryEdit.Text = this.entryText7.Text;
			if (entryEdit.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
			{
				this.entryText7.Text = entryEdit.textEntryEdit.Text;
			}
		}

		private void btnEntryEdit8_Click(object sender, EventArgs e)
		{
			EntryEdit entryEdit = new EntryEdit(this)
			{
				Text = base.Name
			};
			entryEdit.textEntryEdit.Text = this.entryText8.Text;
			if (entryEdit.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
			{
				this.entryText8.Text = entryEdit.textEntryEdit.Text;
			}
		}

		private void btnFilterSettings_Click(object sender, EventArgs e)
		{
			this._SpawnerFilters.Show();
		}

		private void btnGo_Click(object sender, EventArgs e)
		{
			this.GoToSelected = true;
		}

		private void btnLoadSpawn_Click(object sender, EventArgs e)
		{
			try
			{
				this.ofdLoadFile.Title = "Load Spawn File";
				if (this.ofdLoadFile.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
				{
					this.Text = string.Concat("Spawn Editor 2 - ", this.ofdLoadFile.FileName);
					this.stbMain.Text = string.Format("Loading {0}...", this.ofdLoadFile.FileName);
					this.tvwSpawnPoints.Nodes.Clear();
					this.Refresh();
					this.LoadSpawnFile(this.ofdLoadFile.FileName, WorldMap.Internal);
				}
			}
			finally
			{
				this.stbMain.Text = "Finished loading spawn file.";
			}
			this.checkSpawnFilter.Checked = false;
		}

		private void btnMergeSpawn_Click(object sender, EventArgs e)
		{
			try
			{
				this.ofdLoadFile.Title = "Merge Spawn File";
				if (this.ofdLoadFile.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
				{
					this.Text = string.Concat("Spawn Editor 2 - ", this.ofdLoadFile.FileName);
					this.stbMain.Text = string.Format("Merging {0}...", this.ofdLoadFile.FileName);
					this.Refresh();
					this.LoadSpawnFile(this.ofdLoadFile.FileName, WorldMap.Internal);
				}
			}
			finally
			{
				this.stbMain.Text = "Finished merging spawn file.";
			}
			this.checkSpawnFilter.Checked = false;
		}

		private void btnMove_Click(object sender, EventArgs e)
		{
			this.SelectedSpawnNode = this.tvwSpawnPoints.SelectedNode as SpawnPointNode;
			SpawnObjectNode selectedNode = this.tvwSpawnPoints.SelectedNode as SpawnObjectNode;
			if (selectedNode != null)
			{
				this.SelectedSpawnNode = (SpawnPointNode)selectedNode.Parent;
			}
			if (this.SelectedSpawnNode != null)
			{
				Area area = new Area(this.SelectedSpawnNode.Spawn, this);
				area.ShowDialog(this);
			}
		}

		private void btnResetTypes_Click(object sender, EventArgs e)
		{
			this.clbRunUOTypes.ClearSelected();
			for (int i = 0; i < this.clbRunUOTypes.Items.Count; i++)
			{
				this.clbRunUOTypes.SetItemChecked(i, false);
			}
		}

		private void btnRestoreSpawnDefaults_Click(object sender, EventArgs e)
		{
			this.LoadDefaultSpawnValues();
		}

		private void btnSaveSpawn_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.sfdSaveFile.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
				{
					this.Text = string.Concat("Spawn Editor 2 - ", this.sfdSaveFile.FileName);
					this.stbMain.Text = string.Format("Saving {0}...", this.ofdLoadFile.FileName);
					this.Refresh();
					this.SaveSpawnFile(this.sfdSaveFile.FileName);
				}
			}
			finally
			{
				this.stbMain.Text = "Finished saving spawn file.";
			}
		}

		private void btnSendSpawn_Click(object sender, EventArgs e)
		{
			if (this.tvwSpawnPoints.Nodes == null || this.tvwSpawnPoints.Nodes.Count <= 0)
			{
				return;
			}
			SpawnPoint selectedSpawn = null;
			int num = this.CountUnfilteredNodes();
			if (sender == this.btnSendSingleSpawner)
			{
				selectedSpawn = this.SelectedSpawn;
				num = 1;
			}
			this.UpdateSpawnDetails(this.SelectedSpawn);
			if (MessageBox.Show(this, string.Format("Send {0} spawners to Server {1}?", num, this._TransferDialog.txtTransferServerAddress.Text), "Send Spawners to Server", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != System.Windows.Forms.DialogResult.OK)
			{
				return;
			}
			SaveSpawnerData saveSpawnerDatum = new SaveSpawnerData();
			MemoryStream memoryStream = new MemoryStream();
			this.SaveSpawnFile(memoryStream, "Memory Stream", selectedSpawn);
			saveSpawnerDatum.Data = memoryStream.GetBuffer();
			if (saveSpawnerDatum.Data == null)
			{
				MessageBox.Show(this, "No Spawners found.", "Empty Send", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return;
			}
			saveSpawnerDatum.AuthenticationID = this.SessionID;
			saveSpawnerDatum.UseMainThread = true;
			string text = this._TransferDialog.txtTransferServerAddress.Text;
			int num1 = -1;
			try
			{
				num1 = int.Parse(this._TransferDialog.txtTransferServerPort.Text);
			}
			catch
			{
			}
			this._TransferDialog.DisplayStatusIndicator("Sending Spawners...");
			TransferMessage transferMessage = TransferConnection.ProcessMessage(text, num1, saveSpawnerDatum);
			if (transferMessage is ReturnSpawnerSaveStatus)
			{
				int processedSpawners = ((ReturnSpawnerSaveStatus)transferMessage).ProcessedSpawners;
				int processedMaps = ((ReturnSpawnerSaveStatus)transferMessage).ProcessedMaps;
				if (processedSpawners != 0)
				{
					MessageBox.Show(string.Format("Successfully sent {0} spawners", processedSpawners));
				}
				else
				{
					MessageBox.Show(this, "No Spawners sent.", "Empty Send", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
			}
			this._TransferDialog.HideStatusIndicator();
		}

		private void btnSpawnPackClear(object sender, EventArgs e)
		{
			this.clbSpawnPack.ClearSelected();
			for (int i = 0; i < this.clbSpawnPack.Items.Count; i++)
			{
				this.clbSpawnPack.SetItemChecked(i, false);
			}
		}

		private void btnUpdateFromSpawnPack_Click(object sender, EventArgs e)
		{
			TreeNode selectedNode = this.tvwSpawnPoints.SelectedNode;
			this.SelectedSpawnNode = selectedNode as SpawnPointNode;
			SpawnObjectNode spawnObjectNode = selectedNode as SpawnObjectNode;
			if (spawnObjectNode != null)
			{
				this.SelectedSpawnNode = spawnObjectNode.Parent as SpawnPointNode;
			}
			if (this.SelectedSpawnNode != null)
			{
				this.SetSpawnFromSpawnPack(this.SelectedSpawnNode, true);
			}
			this.RefreshSpawnPoints();
		}

		private void btnUpdateSpawn_Click(object sender, EventArgs e)
		{
			TreeNode selectedNode = this.tvwSpawnPoints.SelectedNode;
			this.SelectedSpawnNode = selectedNode as SpawnPointNode;
			SpawnObjectNode spawnObjectNode = selectedNode as SpawnObjectNode;
			if (spawnObjectNode != null)
			{
				this.SelectedSpawnNode = spawnObjectNode.Parent as SpawnPointNode;
			}
			if (this.SelectedSpawnNode != null)
			{
				this.SetSpawn(this.SelectedSpawnNode, true);
			}
			this.RefreshSpawnPoints();
		}

		private void btnUpdateSpawnPacks_Click(object sender, EventArgs e)
		{
			this.UpdateSpawnPacks(this.textSpawnPackName.Text, this.clbSpawnPack.Items);
		}

		private void cbxMap_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.ClearSelectionWindow();
			this.stbMain.Text = string.Concat(this.cbxMap.SelectedItem.ToString(), " Map Selected");
			this.stbMain.Refresh();
			int x = 0;
			int y = 0;
			switch ((int)((WorldMap)this.cbxMap.SelectedItem))
			{
				case 0:
				{
					this.axUOMap.set_MapFile(0);
					x = this.MapLoc[0].X;
					y = this.MapLoc[0].Y;
					break;
				}
				case 1:
				{
					this.axUOMap.set_MapFile(1);
					x = this.MapLoc[1].X;
					y = this.MapLoc[1].Y;
					break;
				}
				case 2:
				{
					this.axUOMap.set_MapFile(2);
					x = this.MapLoc[2].X;
					y = this.MapLoc[2].Y;
					break;
				}
				case 3:
				{
					this.axUOMap.set_MapFile(3);
					x = this.MapLoc[3].X;
					y = this.MapLoc[3].Y;
					break;
				}
				case 4:
				{
					this.axUOMap.set_MapFile(4);
					x = this.MapLoc[4].X;
					y = this.MapLoc[4].Y;
					break;
				}
			}
			this.axUOMap.SetCenter((short)x, (short)y);
			this.axUOMap.set_xCenter((short)x);
			this.axUOMap.set_yCenter((short)y);
			this.RefreshSpawnPoints();
		}

		private void cbxShade_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.chkShade.Checked)
			{
				this.RefreshSpawnPoints();
			}
		}

		private void checkBox1_CheckedChanged(object sender, EventArgs e)
		{
			if (this.chkTracking.Checked)
			{
				this.Tracking = true;
				this.ActivateTracking();
				return;
			}
			this.Tracking = false;
			this.axUOMap.RemoveDrawObjects();
			this.RefreshSpawnPoints();
		}

		private void checkBox20_CheckedChanged(object sender, EventArgs e)
		{
		}

		private void checkSpawnFilter_CheckedChanged(object sender, EventArgs e)
		{
			if (this.checkSpawnFilter.Checked)
			{
				this.ApplySpawnFilter();
			}
			else
			{
				this.ClearSpawnFilter();
			}
			this.RefreshSpawnPoints();
		}

		private void chkDetails_CheckedChanged(object sender, EventArgs e)
		{
			if (!this.chkDetails.Checked)
			{
				this.SmallWindow();
				return;
			}
			if (this.axUOMap != null && this.tabControl2 != null && this.panel1 != null)
			{
				this.savewindowsize = base.Size;
				this.savemapsize = this.axUOMap.Size;
				this.savelistsize = this.tabControl2.Size;
				this.savepanelsize = this.panel1.Size;
			}
			this.LargeWindow();
		}

		private void chkDrawStatics_CheckedChanged(object sender, EventArgs e)
		{
			this.axUOMap.set_DrawStatics(this.chkDrawStatics.Checked);
		}

		private void chkInContainer_CheckedChanged(object sender, EventArgs e)
		{
			if (this.chkInContainer.Checked)
			{
				this.spnContainerX.Enabled = true;
				this.spnContainerY.Enabled = true;
				this.spnContainerZ.Enabled = true;
				this.labelContainerX.Enabled = true;
				this.labelContainerY.Enabled = true;
				this.labelContainerZ.Enabled = true;
				return;
			}
			this.spnContainerX.Enabled = false;
			this.spnContainerY.Enabled = false;
			this.spnContainerZ.Enabled = false;
			this.labelContainerX.Enabled = false;
			this.labelContainerY.Enabled = false;
			this.labelContainerZ.Enabled = false;
		}

		private void chkShade_CheckedChanged(object sender, EventArgs e)
		{
			this.RefreshSpawnPoints();
		}

		private void chkShowCreatures_CheckedChanged(object sender, EventArgs e)
		{
			this.RefreshSpawnPoints();
		}

		private void chkShowItems_CheckedChanged(object sender, EventArgs e)
		{
			this.RefreshSpawnPoints();
		}

		private void chkShowPlayers_CheckedChanged(object sender, EventArgs e)
		{
			this.RefreshSpawnPoints();
		}

		private void chkShowSpawns_CheckedChanged(object sender, EventArgs e)
		{
			this.RefreshSpawnPoints();
		}

		private void clbSpawnPack_MouseUp(object sender, MouseEventArgs e)
		{
			int num = this.clbSpawnPack.IndexFromPoint(e.X, e.Y);
			if (num >= 0)
			{
				this.clbSpawnPack.SelectedItem = this.clbSpawnPack.Items[num];
			}
			if (this.clbSpawnPack.SelectedItem is string && e.Button == System.Windows.Forms.MouseButtons.Right)
			{
				this.mcnSpawnPack.Show(this.clbSpawnPack, new Point(e.X, e.Y));
			}
		}

		private void ClearEntries()
		{
			this.entryText1.Text = null;
			this.entryText2.Text = null;
			this.entryText3.Text = null;
			this.entryText4.Text = null;
			this.entryText5.Text = null;
			this.entryText6.Text = null;
			this.entryText7.Text = null;
			this.entryText8.Text = null;
			this.entryMax1.Value = new decimal(0);
			this.entryMax2.Value = new decimal(0);
			this.entryMax3.Value = new decimal(0);
			this.entryMax4.Value = new decimal(0);
			this.entryMax5.Value = new decimal(0);
			this.entryMax6.Value = new decimal(0);
			this.entryMax7.Value = new decimal(0);
			this.entryMax8.Value = new decimal(0);
			this.entryPer1.Value = new decimal(0);
			this.entryPer2.Value = new decimal(0);
			this.entryPer3.Value = new decimal(0);
			this.entryPer4.Value = new decimal(0);
			this.entryPer5.Value = new decimal(0);
			this.entryPer6.Value = new decimal(0);
			this.entryPer7.Value = new decimal(0);
			this.entryPer8.Value = new decimal(0);
			this.entrySub1.Text = null;
			this.entrySub2.Text = null;
			this.entrySub3.Text = null;
			this.entrySub4.Text = null;
			this.entrySub5.Text = null;
			this.entrySub6.Text = null;
			this.entrySub7.Text = null;
			this.entrySub8.Text = null;
			this.entryReset1.Text = null;
			this.entryReset2.Text = null;
			this.entryReset3.Text = null;
			this.entryReset4.Text = null;
			this.entryReset5.Text = null;
			this.entryReset6.Text = null;
			this.entryReset7.Text = null;
			this.entryReset8.Text = null;
			this.entryTo1.Text = null;
			this.entryTo2.Text = null;
			this.entryTo3.Text = null;
			this.entryTo4.Text = null;
			this.entryTo5.Text = null;
			this.entryTo6.Text = null;
			this.entryTo7.Text = null;
			this.entryTo8.Text = null;
			this.entryKills1.Text = null;
			this.entryKills2.Text = null;
			this.entryKills3.Text = null;
			this.entryKills4.Text = null;
			this.entryKills5.Text = null;
			this.entryKills6.Text = null;
			this.entryKills7.Text = null;
			this.entryKills8.Text = null;
			this.entryMinD1.Text = null;
			this.entryMinD2.Text = null;
			this.entryMinD3.Text = null;
			this.entryMinD4.Text = null;
			this.entryMinD5.Text = null;
			this.entryMinD6.Text = null;
			this.entryMinD7.Text = null;
			this.entryMinD8.Text = null;
			this.entryMaxD1.Text = null;
			this.entryMaxD2.Text = null;
			this.entryMaxD3.Text = null;
			this.entryMaxD4.Text = null;
			this.entryMaxD5.Text = null;
			this.entryMaxD6.Text = null;
			this.entryMaxD7.Text = null;
			this.entryMaxD8.Text = null;
			this.chkRK1.Checked = false;
			this.chkRK2.Checked = false;
			this.chkRK3.Checked = false;
			this.chkRK4.Checked = false;
			this.chkRK5.Checked = false;
			this.chkRK6.Checked = false;
			this.chkRK7.Checked = false;
			this.chkRK8.Checked = false;
			this.chkClr1.Checked = false;
			this.chkClr2.Checked = false;
			this.chkClr3.Checked = false;
			this.chkClr4.Checked = false;
			this.chkClr5.Checked = false;
			this.chkClr6.Checked = false;
			this.chkClr7.Checked = false;
			this.chkClr8.Checked = false;
		}

		private void ClearSelectionWindow()
		{
			this._SelectionWindow = null;
			this.EnableSelectionWindowOption(false);
		}

		internal void ClearSpawnFilter()
		{
			if (this.tvwSpawnPoints == null || this.tvwSpawnPoints.Nodes == null)
			{
				return;
			}
			foreach (SpawnPointNode node in this.tvwSpawnPoints.Nodes)
			{
				node.Filtered = false;
			}
		}

		private void ClearTreeColor(TreeNode t, Color c)
		{
			if (t != null)
			{
				t.BackColor = c;
				if (t.Nodes != null)
				{
					foreach (TreeNode node in t.Nodes)
					{
						this.ClearTreeColor(node, c);
					}
				}
			}
		}

		private void ClearTreeFacetSelection()
		{
			foreach (TreeNode node in this.treeRegionView.Nodes)
			{
				node.Checked = false;
			}
		}

		public static double ComputeDensity(SpawnPoint spawn)
		{
			int spawnHomeRange = 4 * spawn.SpawnHomeRange * spawn.SpawnHomeRange;
			if (spawn.SpawnHomeRangeIsRelative)
			{
				Rectangle bounds = spawn.Bounds;
				int height = bounds.Height + 2 * spawn.SpawnHomeRange;
				bounds = spawn.Bounds;
				spawnHomeRange = height * (bounds.Width + 2 * spawn.SpawnHomeRange);
			}
			int spawnMaxCount = spawn.SpawnMaxCount;
			double num = 0;
			if (spawnHomeRange > 0)
			{
				num = (double)spawnMaxCount / (double)spawnHomeRange;
			}
			return num;
		}

		private static int ComputeDensityColor(SpawnPoint spawn)
		{
			int num = (int)(SpawnEditor.ComputeDensity(spawn) * 100000 + 20);
			if (num > 255)
			{
				num = 255;
			}
			return num * 256 * 256 + num;
		}

		private static int ComputeSpeedColor(SpawnPoint spawn)
		{
			int spawnMinDelay = (int)(spawn.SpawnMinDelay + spawn.SpawnMaxDelay) / 2;
			if (spawnMinDelay <= 0)
			{
				spawnMinDelay = 1;
			}
			int num = 1000 / spawnMinDelay + 20;
			if (num > 255)
			{
				num = 255;
			}
			return num * 256 * 256 + num;
		}

		private bool ControlHasBeenSelected(object key)
		{
			if (!this.ControlModHash.Contains(key))
			{
				return false;
			}
			return (bool)this.ControlModHash[key];
		}

		private int CountUnfilteredNodes()
		{
			if (this.tvwSpawnPoints.Nodes == null || this.tvwSpawnPoints.Nodes.Count <= 0)
			{
				return 0;
			}
			int num = 0;
			for (int i = 0; i < this.tvwSpawnPoints.Nodes.Count; i++)
			{
				if (!((SpawnPointNode)this.tvwSpawnPoints.Nodes[i]).Filtered)
				{
					num++;
				}
			}
			return num;
		}

		public static void Debug(string msg)
		{
			if (SpawnEditor._Debug)
			{
				try
				{
					using (StreamWriter streamWriter = new StreamWriter("debug.log", true))
					{
						streamWriter.WriteLine("{0}: {1}", DateTime.Now, msg);
					}
				}
				catch
				{
				}
			}
		}

		private void DeleteAllSpawns()
		{
			if (this.tvwSpawnPoints.Nodes != null && MessageBox.Show(this, string.Format("Are you sure you want to delete ALL {0} spawns?", this.tvwSpawnPoints.Nodes.Count), "Delete All Spawns", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
			{
				this.tvwSpawnPoints.Nodes.Clear();
				this.SelectedSpawn = null;
				this.LoadDefaultSpawnValues();
				this.RefreshSpawnPoints();
			}
		}

		public void DisplayMyLocation()
		{
			if (this.cbxMap.SelectedIndex == this.MyLocation.Facet)
			{
				this.axUOMap.RemoveDrawObjects();
				this.axUOMap.AddDrawObject((short)this.MyLocation.X, (short)this.MyLocation.Y, 1, 12, 65280);
				this.axUOMap.AddDrawObject((short)this.MyLocation.X, (short)this.MyLocation.Y, 3, 2, 255);
			}
		}

		private void DisplaySpawnDetails(SpawnPoint Spawn)
		{
			if (Spawn == null)
			{
				return;
			}
			this.txtName.Text = Spawn.SpawnName;
			this.spnHomeRange.Value = Spawn.SpawnHomeRange;
			this.spnMaxCount.Value = Spawn.SpawnMaxCount;
			this.spnMinDelay.Value = (decimal)((double)Spawn.SpawnMinDelay);
			this.spnMaxDelay.Value = (decimal)((double)Spawn.SpawnMaxDelay);
			this.spnTeam.Value = Spawn.SpawnTeam;
			this.chkGroup.Checked = Spawn.SpawnIsGroup;
			this.chkRunning.Checked = Spawn.SpawnIsRunning;
			this.chkHomeRangeIsRelative.Checked = Spawn.SpawnHomeRangeIsRelative;
			this.spnSpawnRange.Value = Spawn.SpawnSpawnRange;
			this.spnProximityRange.Value = Spawn.SpawnProximityRange;
			this.spnDuration.Value = (decimal)((double)Spawn.SpawnDuration);
			this.spnDespawn.Value = (decimal)((double)Spawn.SpawnDespawn);
			this.spnMinRefract.Value = (decimal)((double)Spawn.SpawnMinRefract);
			this.spnMaxRefract.Value = (decimal)((double)Spawn.SpawnMaxRefract);
			this.spnTODStart.Value = (decimal)((double)Spawn.SpawnTODStart);
			this.spnTODEnd.Value = (decimal)((double)Spawn.SpawnTODEnd);
			this.spnKillReset.Value = Spawn.SpawnKillReset;
			this.spnProximitySnd.Value = Spawn.SpawnProximitySnd;
			this.chkAllowGhost.Checked = Spawn.SpawnAllowGhost;
			this.chkSpawnOnTrigger.Checked = Spawn.SpawnSpawnOnTrigger;
			this.chkSequentialSpawn.Checked = Spawn.SpawnSequentialSpawn > -1;
			this.chkSmartSpawning.Checked = Spawn.SpawnSmartSpawning;
			if (Spawn.SpawnTODMode != 0)
			{
				this.chkRealTOD.Checked = false;
				this.chkGameTOD.Checked = true;
			}
			else
			{
				this.chkRealTOD.Checked = true;
				this.chkGameTOD.Checked = false;
			}
			this.chkInContainer.Checked = Spawn.SpawnInContainer;
			this.textSkillTrigger.Text = Spawn.SpawnSkillTrigger;
			this.textSpeechTrigger.Text = Spawn.SpawnSpeechTrigger;
			this.textProximityMsg.Text = Spawn.SpawnProximityMsg;
			this.textMobTriggerName.Text = Spawn.SpawnMobTriggerName;
			this.textMobTrigProp.Text = Spawn.SpawnMobTrigProp;
			this.textPlayerTrigProp.Text = Spawn.SpawnPlayerTrigProp;
			this.textTrigObjectProp.Text = Spawn.SpawnTrigObjectProp;
			this.textTriggerOnCarried.Text = Spawn.SpawnTriggerOnCarried;
			this.textNoTriggerOnCarried.Text = Spawn.SpawnNoTriggerOnCarried;
			this.spnTriggerProbability.Value = (decimal)((double)Spawn.SpawnTriggerProbability);
			this.spnStackAmount.Value = Spawn.SpawnStackAmount;
			this.txtNotes.Text = Spawn.SpawnNotes;
			this.spnContainerX.Value = Spawn.SpawnContainerX;
			this.spnContainerY.Value = Spawn.SpawnContainerY;
			this.spnContainerZ.Value = Spawn.SpawnContainerZ;
			this.chkExternalTriggering.Checked = Spawn.SpawnExternalTriggering;
			this.textTrigObjectName.Text = Spawn.SpawnObjectPropertyItemName;
			this.textSetObjectName.Text = Spawn.SpawnSetPropertyItemName;
			this.textRegionName.Text = Spawn.SpawnRegionName;
			this.textConfigFile.Text = Spawn.SpawnConfigFile;
			this.textWayPoint.Text = Spawn.SpawnWaypoint;
		}

		private void DisplaySpawnEntries()
		{
			int num;
			this.ClearEntries();
			if (this.SelectedSpawn == null)
			{
				this.vScrollBar1.Maximum = 8;
				return;
			}
			if (this.SelectedSpawn.SpawnObjects == null)
			{
				this.vScrollBar1.Maximum = 8;
				return;
			}
			int value = this.vScrollBar1.Value;
			if (this.SelectedSpawn.SpawnObjects.Count > 7)
			{
				this.vScrollBar1.Maximum = this.SelectedSpawn.SpawnObjects.Count + 2;
			}
			int num1 = 0;
			int num2 = 0;
			IEnumerator enumerator = this.SelectedSpawn.SpawnObjects.GetEnumerator();
			try
			{
				do
				{
				Label0:
					if (!enumerator.MoveNext())
					{
						break;
					}
					SpawnObject current = (SpawnObject)enumerator.Current;
					int num3 = num1;
					num1 = num3 + 1;
					if (num3 >= value)
					{
						switch (num2)
						{
							case 0:
							{
								this.entryText1.Text = current.TypeName;
								this.entryMax1.Value = current.Count;
								this.entryPer1.Value = current.SpawnsPerTick;
								this.entrySub1.Text = current.SubGroup.ToString();
								this.entryReset1.Text = current.SequentialResetTime.ToString();
								this.entryTo1.Text = current.SequentialResetTo.ToString();
								this.entryKills1.Text = current.KillsNeeded.ToString();
								if (current.MinDelay < 0)
								{
									this.entryMinD1.Text = null;
								}
								else
								{
									this.entryMinD1.Text = current.MinDelay.ToString();
								}
								if (current.MaxDelay < 0)
								{
									this.entryMaxD1.Text = null;
								}
								else
								{
									this.entryMaxD1.Text = current.MaxDelay.ToString();
								}
								this.chkRK1.Checked = current.RestrictKillsToSubgroup;
								this.chkClr1.Checked = current.ClearOnAdvance;
								break;
							}
							case 1:
							{
								this.entryText2.Text = current.TypeName;
								this.entryMax2.Value = current.Count;
								this.entryPer2.Value = current.SpawnsPerTick;
								this.entrySub2.Text = current.SubGroup.ToString();
								this.entryReset2.Text = current.SequentialResetTime.ToString();
								this.entryTo2.Text = current.SequentialResetTo.ToString();
								this.entryKills2.Text = current.KillsNeeded.ToString();
								if (current.MinDelay < 0)
								{
									this.entryMinD2.Text = null;
								}
								else
								{
									this.entryMinD2.Text = current.MinDelay.ToString();
								}
								if (current.MaxDelay < 0)
								{
									this.entryMaxD2.Text = null;
								}
								else
								{
									this.entryMaxD2.Text = current.MaxDelay.ToString();
								}
								this.chkRK2.Checked = current.RestrictKillsToSubgroup;
								this.chkClr2.Checked = current.ClearOnAdvance;
								break;
							}
							case 2:
							{
								this.entryText3.Text = current.TypeName;
								this.entryMax3.Value = current.Count;
								this.entryPer3.Value = current.SpawnsPerTick;
								this.entrySub3.Text = current.SubGroup.ToString();
								this.entryReset3.Text = current.SequentialResetTime.ToString();
								this.entryTo3.Text = current.SequentialResetTo.ToString();
								this.entryKills3.Text = current.KillsNeeded.ToString();
								if (current.MinDelay < 0)
								{
									this.entryMinD3.Text = null;
								}
								else
								{
									this.entryMinD3.Text = current.MinDelay.ToString();
								}
								if (current.MaxDelay < 0)
								{
									this.entryMaxD3.Text = null;
								}
								else
								{
									this.entryMaxD3.Text = current.MaxDelay.ToString();
								}
								this.chkRK3.Checked = current.RestrictKillsToSubgroup;
								this.chkClr3.Checked = current.ClearOnAdvance;
								break;
							}
							case 3:
							{
								this.entryText4.Text = current.TypeName;
								this.entryMax4.Value = current.Count;
								this.entryPer4.Value = current.SpawnsPerTick;
								this.entrySub4.Text = current.SubGroup.ToString();
								this.entryReset4.Text = current.SequentialResetTime.ToString();
								this.entryTo4.Text = current.SequentialResetTo.ToString();
								this.entryKills4.Text = current.KillsNeeded.ToString();
								if (current.MinDelay < 0)
								{
									this.entryMinD4.Text = null;
								}
								else
								{
									this.entryMinD4.Text = current.MinDelay.ToString();
								}
								if (current.MaxDelay < 0)
								{
									this.entryMaxD4.Text = null;
								}
								else
								{
									this.entryMaxD4.Text = current.MaxDelay.ToString();
								}
								this.chkRK4.Checked = current.RestrictKillsToSubgroup;
								this.chkClr4.Checked = current.ClearOnAdvance;
								break;
							}
							case 4:
							{
								this.entryText5.Text = current.TypeName;
								this.entryMax5.Value = current.Count;
								this.entryPer5.Value = current.SpawnsPerTick;
								this.entrySub5.Text = current.SubGroup.ToString();
								this.entryReset5.Text = current.SequentialResetTime.ToString();
								this.entryTo5.Text = current.SequentialResetTo.ToString();
								this.entryKills5.Text = current.KillsNeeded.ToString();
								if (current.MinDelay < 0)
								{
									this.entryMinD5.Text = null;
								}
								else
								{
									this.entryMinD5.Text = current.MinDelay.ToString();
								}
								if (current.MaxDelay < 0)
								{
									this.entryMaxD5.Text = null;
								}
								else
								{
									this.entryMaxD5.Text = current.MaxDelay.ToString();
								}
								this.chkRK5.Checked = current.RestrictKillsToSubgroup;
								this.chkClr5.Checked = current.ClearOnAdvance;
								break;
							}
							case 5:
							{
								this.entryText6.Text = current.TypeName;
								this.entryMax6.Value = current.Count;
								this.entryPer6.Value = current.SpawnsPerTick;
								this.entrySub6.Text = current.SubGroup.ToString();
								this.entryReset6.Text = current.SequentialResetTime.ToString();
								this.entryTo6.Text = current.SequentialResetTo.ToString();
								this.entryKills6.Text = current.KillsNeeded.ToString();
								if (current.MinDelay < 0)
								{
									this.entryMinD6.Text = null;
								}
								else
								{
									this.entryMinD6.Text = current.MinDelay.ToString();
								}
								if (current.MaxDelay < 0)
								{
									this.entryMaxD6.Text = null;
								}
								else
								{
									this.entryMaxD6.Text = current.MaxDelay.ToString();
								}
								this.chkRK6.Checked = current.RestrictKillsToSubgroup;
								this.chkClr6.Checked = current.ClearOnAdvance;
								break;
							}
							case 6:
							{
								this.entryText7.Text = current.TypeName;
								this.entryMax7.Value = current.Count;
								this.entryPer7.Value = current.SpawnsPerTick;
								this.entrySub7.Text = current.SubGroup.ToString();
								this.entryReset7.Text = current.SequentialResetTime.ToString();
								this.entryTo7.Text = current.SequentialResetTo.ToString();
								this.entryKills7.Text = current.KillsNeeded.ToString();
								if (current.MinDelay < 0)
								{
									this.entryMinD7.Text = null;
								}
								else
								{
									this.entryMinD7.Text = current.MinDelay.ToString();
								}
								if (current.MaxDelay < 0)
								{
									this.entryMaxD7.Text = null;
								}
								else
								{
									this.entryMaxD7.Text = current.MaxDelay.ToString();
								}
								this.chkRK7.Checked = current.RestrictKillsToSubgroup;
								this.chkClr7.Checked = current.ClearOnAdvance;
								break;
							}
							case 7:
							{
								this.entryText8.Text = current.TypeName;
								this.entryMax8.Value = current.Count;
								this.entryPer8.Value = current.SpawnsPerTick;
								this.entrySub8.Text = current.SubGroup.ToString();
								this.entryReset8.Text = current.SequentialResetTime.ToString();
								this.entryTo8.Text = current.SequentialResetTo.ToString();
								this.entryKills8.Text = current.KillsNeeded.ToString();
								if (current.MinDelay < 0)
								{
									this.entryMinD8.Text = null;
								}
								else
								{
									this.entryMinD8.Text = current.MinDelay.ToString();
								}
								if (current.MaxDelay < 0)
								{
									this.entryMaxD8.Text = null;
								}
								else
								{
									this.entryMaxD8.Text = current.MaxDelay.ToString();
								}
								this.chkRK8.Checked = current.RestrictKillsToSubgroup;
								this.chkClr8.Checked = current.ClearOnAdvance;
								break;
							}
						}
						num = num2 + 1;
						num2 = num;
					}
					else
					{
						goto Label0;
					}
				}
				while (num <= 7);
			}
			finally
			{
				IDisposable disposable = enumerator as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
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

		private void DoUnloadSpawners(SpawnPoint selectedspawn)
		{
			int num = this.CountUnfilteredNodes();
			if (selectedspawn != null)
			{
				num = 1;
			}
			if (MessageBox.Show(this, string.Format("Unload {0} spawners from Server {1}?", num, this._TransferDialog.txtTransferServerAddress.Text), "Unload Spawners from Server", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != System.Windows.Forms.DialogResult.OK)
			{
				return;
			}
			UnloadSpawnerData unloadSpawnerDatum = new UnloadSpawnerData();
			MemoryStream memoryStream = new MemoryStream();
			this.SaveSpawnFile(memoryStream, "Memory Stream", selectedspawn);
			unloadSpawnerDatum.Data = memoryStream.GetBuffer();
			if (unloadSpawnerDatum.Data == null)
			{
				MessageBox.Show(this, "No Spawners found.", "Empty Unload", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return;
			}
			unloadSpawnerDatum.AuthenticationID = this.SessionID;
			unloadSpawnerDatum.UseMainThread = true;
			string text = this._TransferDialog.txtTransferServerAddress.Text;
			int num1 = -1;
			try
			{
				num1 = int.Parse(this._TransferDialog.txtTransferServerPort.Text);
			}
			catch
			{
			}
			this._TransferDialog.DisplayStatusIndicator("Unloading Spawners...");
			TransferMessage transferMessage = TransferConnection.ProcessMessage(text, num1, unloadSpawnerDatum);
			if (transferMessage is ReturnSpawnerUnloadStatus)
			{
				int processedSpawners = ((ReturnSpawnerUnloadStatus)transferMessage).ProcessedSpawners;
				int processedMaps = ((ReturnSpawnerUnloadStatus)transferMessage).ProcessedMaps;
				if (processedSpawners != 0)
				{
					MessageBox.Show(string.Format("Successfully unloaded {0} spawners", processedSpawners));
				}
				else
				{
					MessageBox.Show(this, "No Spawners unloaded.", "Empty Unload", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
			}
			this._TransferDialog.HideStatusIndicator();
		}

		internal void DSLoadSpawnFile(Stream stream, string FilePath, WorldMap ForceMap)
		{
			if (stream == null)
			{
				MessageBox.Show(this, "Unable to Load Spawns: Empty Stream.", "Read Failure", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return;
			}
			try
			{
				this.tvwSpawnPoints.Sorted = false;
				DataSet dataSet = new DataSet("Spawns");
				dataSet.ReadXml(stream);
				RectangleConverter rectangleConverter = new RectangleConverter();
				this.progressBar1.Maximum = dataSet.Tables["Points"].Rows.Count;
				int num = 0;
				foreach (DataRow row in dataSet.Tables["Points"].Rows)
				{
					num++;
					this.progressBar1.Value = num;
					if (ForceMap != WorldMap.Internal)
					{
						if (!dataSet.Tables["Points"].Columns.Contains("Map"))
						{
							dataSet.Tables["Points"].Columns.Add("Map");
						}
						if (!dataSet.Tables["Points"].Columns.Contains("UniqueId"))
						{
							dataSet.Tables["Points"].Columns.Add("UniqueId");
						}
						row["Map"] = ForceMap.ToString();
						row["UniqueId"] = Guid.NewGuid().ToString();
					}
					SpawnPointNode spawnPointNode = new SpawnPointNode(new SpawnPoint(row));
					this.tvwSpawnPoints.Nodes.Add(spawnPointNode);
				}
				this.lblTotalSpawn.Text = string.Concat("Total Spawns = ", this.tvwSpawnPoints.Nodes.Count);
				this.txtName.Text = string.Concat(this._CfgDialog.CfgSpawnNameValue, this.tvwSpawnPoints.Nodes.Count);
				this.tvwSpawnPoints.Sorted = true;
				this.RefreshSpawnPoints();
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				string[] filePath = new string[] { "Failed to load file [", FilePath, "] for the following reason:", Environment.NewLine, this.ExceptionMessage(exception) };
				MessageBox.Show(this, string.Concat(filePath), "Load Failure", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}

		internal void EnableSelectionWindowOption(bool enabled)
		{
			if (enabled)
			{
				this._TransferDialog.chkSpawnerWithinSelectionWindow.Checked = true;
				this._TransferDialog.chkSpawnerWithinSelectionWindow.Enabled = true;
				this.mniDeleteInSelectionWindow.Enabled = true;
				this.mniDeleteNotSelected.Enabled = true;
				this.mniModifyInSelectionWindow.Enabled = true;
				return;
			}
			this._TransferDialog.chkSpawnerWithinSelectionWindow.Checked = false;
			this._TransferDialog.chkSpawnerWithinSelectionWindow.Enabled = false;
			this.mniDeleteInSelectionWindow.Enabled = false;
			this.mniDeleteNotSelected.Enabled = false;
			this.mniModifyInSelectionWindow.Enabled = false;
		}

		private void entryMax1_Click(object sender, EventArgs e)
		{
			this.maxvaluechanged = true;
			this.changedmaxvalue = (int)this.entryMax2.Value;
		}

		private void entryMax1_KeyUp(object sender, KeyEventArgs e)
		{
			this.maxvaluechanged = true;
			this.changedmaxvalue = (int)this.entryMax1.Value;
		}

		private void entryMax1_Leave(object sender, EventArgs e)
		{
			if (this.maxvaluechanged)
			{
				this.UpdateSpawnEntries();
				this.UpdateSpawnerMaxCount();
				this.maxvaluechanged = false;
			}
		}

		private void entryMax1_ValueChanged(object sender, EventArgs e)
		{
		}

		private void entryMax2_Click(object sender, EventArgs e)
		{
			this.maxvaluechanged = true;
			this.changedmaxvalue = (int)this.entryMax2.Value;
		}

		private void entryMax2_KeyUp(object sender, KeyEventArgs e)
		{
			this.maxvaluechanged = true;
			this.changedmaxvalue = (int)this.entryMax1.Value;
		}

		private void entryMax2_Leave(object sender, EventArgs e)
		{
			if (this.maxvaluechanged)
			{
				this.UpdateSpawnEntries();
				this.UpdateSpawnerMaxCount();
				this.maxvaluechanged = false;
			}
		}

		private void entryMax2_ValueChanged(object sender, EventArgs e)
		{
		}

		private void entryMax2_ValueChanged_1(object sender, EventArgs e)
		{
		}

		private void entryMax3_Click(object sender, EventArgs e)
		{
			this.maxvaluechanged = true;
			this.changedmaxvalue = (int)this.entryMax3.Value;
		}

		private void entryMax3_KeyUp(object sender, KeyEventArgs e)
		{
			this.maxvaluechanged = true;
			this.changedmaxvalue = (int)this.entryMax3.Value;
		}

		private void entryMax3_Leave(object sender, EventArgs e)
		{
			if (this.maxvaluechanged)
			{
				this.UpdateSpawnEntries();
				this.UpdateSpawnerMaxCount();
				this.maxvaluechanged = false;
			}
		}

		private void entryMax4_Click(object sender, EventArgs e)
		{
			this.maxvaluechanged = true;
			this.changedmaxvalue = (int)this.entryMax4.Value;
		}

		private void entryMax4_KeyUp(object sender, KeyEventArgs e)
		{
			this.maxvaluechanged = true;
			this.changedmaxvalue = (int)this.entryMax4.Value;
		}

		private void entryMax4_Leave(object sender, EventArgs e)
		{
			if (this.maxvaluechanged)
			{
				this.UpdateSpawnEntries();
				this.UpdateSpawnerMaxCount();
				this.maxvaluechanged = false;
			}
		}

		private void entryMax5_Click(object sender, EventArgs e)
		{
			this.maxvaluechanged = true;
			this.changedmaxvalue = (int)this.entryMax5.Value;
		}

		private void entryMax5_KeyUp(object sender, KeyEventArgs e)
		{
			this.maxvaluechanged = true;
			this.changedmaxvalue = (int)this.entryMax5.Value;
		}

		private void entryMax5_Leave(object sender, EventArgs e)
		{
			if (this.maxvaluechanged)
			{
				this.UpdateSpawnEntries();
				this.UpdateSpawnerMaxCount();
				this.maxvaluechanged = false;
			}
		}

		private void entryMax6_Click(object sender, EventArgs e)
		{
			this.maxvaluechanged = true;
			this.changedmaxvalue = (int)this.entryMax6.Value;
		}

		private void entryMax6_KeyUp(object sender, KeyEventArgs e)
		{
			this.maxvaluechanged = true;
			this.changedmaxvalue = (int)this.entryMax6.Value;
		}

		private void entryMax6_Leave(object sender, EventArgs e)
		{
			if (this.maxvaluechanged)
			{
				this.UpdateSpawnEntries();
				this.UpdateSpawnerMaxCount();
				this.maxvaluechanged = false;
			}
		}

		private void entryMax7_Click(object sender, EventArgs e)
		{
			this.maxvaluechanged = true;
			this.changedmaxvalue = (int)this.entryMax7.Value;
		}

		private void entryMax7_KeyUp(object sender, KeyEventArgs e)
		{
			this.maxvaluechanged = true;
			this.changedmaxvalue = (int)this.entryMax7.Value;
		}

		private void entryMax7_Leave(object sender, EventArgs e)
		{
			if (this.maxvaluechanged)
			{
				this.UpdateSpawnEntries();
				this.UpdateSpawnerMaxCount();
				this.maxvaluechanged = false;
			}
		}

		private void entryMax8_Click(object sender, EventArgs e)
		{
			this.maxvaluechanged = true;
			this.changedmaxvalue = (int)this.entryMax8.Value;
		}

		private void entryMax8_KeyUp(object sender, KeyEventArgs e)
		{
			this.maxvaluechanged = true;
			this.changedmaxvalue = (int)this.entryMax8.Value;
		}

		private void entryMax8_Leave(object sender, EventArgs e)
		{
			if (this.maxvaluechanged)
			{
				this.UpdateSpawnEntries();
				this.UpdateSpawnerMaxCount();
				this.maxvaluechanged = false;
			}
		}

		private void entryMax8_ValueChanged(object sender, EventArgs e)
		{
			if (this.entrySub8.Text == null || this.entrySub8.Text.Length == 0)
			{
				return;
			}
			int num = 0;
			try
			{
				num = int.Parse(this.entrySub8.Text);
			}
			catch
			{
			}
			if (num <= 0)
			{
				this.entryText8.ForeColor = this.grpSpawnEntries.ForeColor;
				this.entrySub8.ForeColor = this.grpSpawnEntries.ForeColor;
				return;
			}
			this.entryText8.ForeColor = Color.FromArgb(this.RandomColor(num));
			this.entrySub8.ForeColor = this.entryText8.ForeColor;
		}

		private void entrySub1_TextChanged(object sender, EventArgs e)
		{
			if (this.entrySub1.Text == null || this.entrySub1.Text.Length == 0)
			{
				return;
			}
			int num = 0;
			try
			{
				num = int.Parse(this.entrySub1.Text);
			}
			catch
			{
			}
			if (num <= 0)
			{
				this.entryText1.ForeColor = this.grpSpawnEntries.ForeColor;
				this.entrySub1.ForeColor = this.grpSpawnEntries.ForeColor;
				return;
			}
			this.entryText1.ForeColor = Color.FromArgb(this.RandomColor(num));
			this.entrySub1.ForeColor = this.entryText1.ForeColor;
		}

		private void entrySub2_TextChanged(object sender, EventArgs e)
		{
			if (this.entrySub2.Text == null || this.entrySub2.Text.Length == 0)
			{
				return;
			}
			int num = 0;
			try
			{
				num = int.Parse(this.entrySub2.Text);
			}
			catch
			{
			}
			if (num <= 0)
			{
				this.entryText2.ForeColor = this.grpSpawnEntries.ForeColor;
				this.entrySub2.ForeColor = this.grpSpawnEntries.ForeColor;
				return;
			}
			this.entryText2.ForeColor = Color.FromArgb(this.RandomColor(num));
			this.entrySub2.ForeColor = this.entryText2.ForeColor;
		}

		private void entrySub3_TextChanged(object sender, EventArgs e)
		{
			if (this.entrySub3.Text == null || this.entrySub3.Text.Length == 0)
			{
				return;
			}
			int num = 0;
			try
			{
				num = int.Parse(this.entrySub3.Text);
			}
			catch
			{
			}
			if (num <= 0)
			{
				this.entryText3.ForeColor = this.grpSpawnEntries.ForeColor;
				this.entrySub3.ForeColor = this.grpSpawnEntries.ForeColor;
				return;
			}
			this.entryText3.ForeColor = Color.FromArgb(this.RandomColor(num));
			this.entrySub3.ForeColor = this.entryText3.ForeColor;
		}

		private void entrySub4_TextChanged(object sender, EventArgs e)
		{
			if (this.entrySub4.Text == null || this.entrySub4.Text.Length == 0)
			{
				return;
			}
			int num = 0;
			try
			{
				num = int.Parse(this.entrySub4.Text);
			}
			catch
			{
			}
			if (num <= 0)
			{
				this.entryText4.ForeColor = this.grpSpawnEntries.ForeColor;
				this.entrySub4.ForeColor = this.grpSpawnEntries.ForeColor;
				return;
			}
			this.entryText4.ForeColor = Color.FromArgb(this.RandomColor(num));
			this.entrySub4.ForeColor = this.entryText4.ForeColor;
		}

		private void entrySub5_TextChanged(object sender, EventArgs e)
		{
			if (this.entrySub5.Text == null || this.entrySub5.Text.Length == 0)
			{
				return;
			}
			int num = 0;
			try
			{
				num = int.Parse(this.entrySub5.Text);
			}
			catch
			{
			}
			if (num <= 0)
			{
				this.entryText5.ForeColor = this.grpSpawnEntries.ForeColor;
				this.entrySub5.ForeColor = this.grpSpawnEntries.ForeColor;
				return;
			}
			this.entryText5.ForeColor = Color.FromArgb(this.RandomColor(num));
			this.entrySub5.ForeColor = this.entryText5.ForeColor;
		}

		private void entrySub6_TextChanged(object sender, EventArgs e)
		{
			if (this.entrySub6.Text == null || this.entrySub6.Text.Length == 0)
			{
				return;
			}
			int num = 0;
			try
			{
				num = int.Parse(this.entrySub6.Text);
			}
			catch
			{
			}
			if (num <= 0)
			{
				this.entryText6.ForeColor = this.grpSpawnEntries.ForeColor;
				this.entrySub6.ForeColor = this.grpSpawnEntries.ForeColor;
				return;
			}
			this.entryText6.ForeColor = Color.FromArgb(this.RandomColor(num));
			this.entrySub6.ForeColor = this.entryText6.ForeColor;
		}

		private void entrySub7_TextChanged(object sender, EventArgs e)
		{
			if (this.entrySub7.Text == null || this.entrySub7.Text.Length == 0)
			{
				return;
			}
			int num = 0;
			try
			{
				num = int.Parse(this.entrySub7.Text);
			}
			catch
			{
			}
			if (num <= 0)
			{
				this.entryText7.ForeColor = this.grpSpawnEntries.ForeColor;
				this.entrySub7.ForeColor = this.grpSpawnEntries.ForeColor;
				return;
			}
			this.entryText7.ForeColor = Color.FromArgb(this.RandomColor(num));
			this.entrySub7.ForeColor = this.entryText7.ForeColor;
		}

		private void entryText1_KeyUp(object sender, KeyEventArgs e)
		{
			this.entrychanged = 1;
			this.changedentrystring = this.entryText1.Text;
		}

		private void entryText1_MouseLeave(object sender, EventArgs e)
		{
			this.AddEntryOnChange();
		}

		private void entryText1_TextChanged(object sender, EventArgs e)
		{
		}

		private void entryText2_KeyUp(object sender, KeyEventArgs e)
		{
			this.entrychanged = 2;
			this.changedentrystring = this.entryText2.Text;
		}

		private void entryText2_MouseLeave(object sender, EventArgs e)
		{
			this.AddEntryOnChange();
		}

		private void entryText2_TextChanged(object sender, EventArgs e)
		{
		}

		private void entryText3_KeyUp(object sender, KeyEventArgs e)
		{
			this.entrychanged = 3;
			this.changedentrystring = this.entryText3.Text;
		}

		private void entryText3_MouseLeave(object sender, EventArgs e)
		{
			this.AddEntryOnChange();
		}

		private void entryText3_TextChanged(object sender, EventArgs e)
		{
		}

		private void entryText4_KeyUp(object sender, KeyEventArgs e)
		{
			this.entrychanged = 4;
			this.changedentrystring = this.entryText4.Text;
		}

		private void entryText4_MouseLeave(object sender, EventArgs e)
		{
			this.AddEntryOnChange();
		}

		private void entryText4_TextChanged(object sender, EventArgs e)
		{
		}

		private void entryText5_KeyUp(object sender, KeyEventArgs e)
		{
			this.entrychanged = 5;
			this.changedentrystring = this.entryText5.Text;
		}

		private void entryText5_MouseLeave(object sender, EventArgs e)
		{
			this.AddEntryOnChange();
		}

		private void entryText5_TextChanged(object sender, EventArgs e)
		{
		}

		private void entryText6_KeyUp(object sender, KeyEventArgs e)
		{
			this.entrychanged = 6;
			this.changedentrystring = this.entryText6.Text;
		}

		private void entryText6_MouseLeave(object sender, EventArgs e)
		{
			this.AddEntryOnChange();
		}

		private void entryText6_TextChanged(object sender, EventArgs e)
		{
		}

		private void entryText7_KeyUp(object sender, KeyEventArgs e)
		{
			this.entrychanged = 7;
			this.changedentrystring = this.entryText7.Text;
		}

		private void entryText7_MouseLeave(object sender, EventArgs e)
		{
			this.AddEntryOnChange();
		}

		private void entryText7_TextChanged(object sender, EventArgs e)
		{
		}

		private void entryText8_KeyUp(object sender, KeyEventArgs e)
		{
			this.entrychanged = 8;
			this.changedentrystring = this.entryText8.Text;
		}

		private void entryText8_MouseLeave(object sender, EventArgs e)
		{
			this.AddEntryOnChange();
		}

		private void entryText8_TextChanged(object sender, EventArgs e)
		{
		}

		public string ExceptionMessage(Exception se)
		{
			if (se == null)
			{
				return null;
			}
			if (this._ExtendedDiagnostics)
			{
				return se.ToString();
			}
			return se.Message;
		}

		public void ExportSpawnTypes(string filename)
		{
			string str = filename;
			try
			{
				XmlTextWriter xmlTextWriter = new XmlTextWriter(new StreamWriter(str))
				{
					Formatting = Formatting.Indented
				};
				xmlTextWriter.WriteStartDocument(true);
				xmlTextWriter.WriteStartElement("SpawnTypes");
				for (int i = 0; i < this.clbRunUOTypes.Items.Count; i++)
				{
					xmlTextWriter.WriteStartElement("T");
					xmlTextWriter.WriteString(this.clbRunUOTypes.Items[i].ToString());
					xmlTextWriter.WriteEndElement();
				}
				xmlTextWriter.WriteEndElement();
				xmlTextWriter.Close();
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				string[] newLine = new string[] { "Failed to save Spawn Types file [", str, "] for the following reason:", Environment.NewLine, this.ExceptionMessage(exception) };
				MessageBox.Show(this, string.Concat(newLine), "Save Failure", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}

		private void FillGoTree(string dirpath)
		{
			LocationTree locationTree = new LocationTree(dirpath, "felucca.xml", WorldMap.Felucca);
			LocationTree locationTree1 = new LocationTree(dirpath, "trammel.xml", WorldMap.Trammel);
			LocationTree locationTree2 = new LocationTree(dirpath, "ilshenar.xml", WorldMap.Ilshenar);
			LocationTree locationTree3 = new LocationTree(dirpath, "malas.xml", WorldMap.Malas);
			LocationTree locationTree4 = new LocationTree(dirpath, "tokuno.xml", WorldMap.Tokuno);
			this.treeGoView.Nodes.Add(new LocationNode(locationTree1));
			this.treeGoView.Nodes.Add(new LocationNode(locationTree));
			this.treeGoView.Nodes.Add(new LocationNode(locationTree2));
			this.treeGoView.Nodes.Add(new LocationNode(locationTree3));
			this.treeGoView.Nodes.Add(new LocationNode(locationTree4));
		}

		private void FillRegionTree()
		{
			this.treeRegionView.Nodes.Clear();
			for (int i = 0; i < 5; i++)
			{
				this.treeRegionView.Nodes.Add(new RegionFacetNode((WorldMap)i));
			}
			foreach (SpawnEditor2.Region region in SpawnEditor2.Region.Regions)
			{
				foreach (RegionFacetNode node in this.treeRegionView.Nodes)
				{
					if (node.Facet != region.Map)
					{
						continue;
					}
					RegionNode regionNode = new RegionNode(region);
					node.Nodes.Add(regionNode);
					break;
				}
			}
		}

		public static Type FindRunUOType(string name)
		{
			if (name == null)
			{
				return null;
			}
			Type type = null;
			string lower = name.ToLower();
			if (SpawnEditor.typeHash.Contains(lower))
			{
				return (Type)SpawnEditor.typeHash[lower];
			}
			IEnumerator enumerator = SpawnEditor.AssemblyList.GetEnumerator();
			try
			{
				do
				{
					if (!enumerator.MoveNext())
					{
						break;
					}
					Type[] types = ((Assembly)enumerator.Current).GetTypes();
					if (types == null)
					{
						continue;
					}
					int num = 0;
					while (num < (int)types.Length)
					{
						if (!types[num].Name.ToLower().Equals(lower))
						{
							num++;
						}
						else
						{
							type = types[num];
							goto Label0;
						}
					}
				Label0:
				}
				while (type == null);
			}
			finally
			{
				IDisposable disposable = enumerator as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}
			SpawnEditor.typeHash.Add(lower, type);
			return type;
		}

		[DllImport("User32.dll", CharSet=CharSet.None, EntryPoint="FindWindowA", ExactSpelling=false)]
		public static extern int FindWindow(string _ClassName, string _WindowName);

		private int GetIndex(SpawnPoint Spawn, int entrynum)
		{
			if (Spawn == null || Spawn.SpawnObjects == null)
			{
				return -1;
			}
			return this.vScrollBar1.Value + entrynum - 1;
		}

		private short GetZoomAdjustedSize(short DefaultSize)
		{
			if (this.axUOMap.get_ZoomLevel() == 0)
			{
				return DefaultSize;
			}
			if (this.axUOMap.get_ZoomLevel() > 0)
			{
				short num = (short)(Math.Pow(2, (double)this.axUOMap.get_ZoomLevel()) * (double)DefaultSize);
				return num;
			}
			short num1 = (short)(Math.Pow(2, (double)this.axUOMap.get_ZoomLevel()) * (double)DefaultSize);
			if (num1 > 0)
			{
				return num1;
			}
			return 1;
		}

		private void groupBox1_Leave(object sender, EventArgs e)
		{
			this.UpdateSpawnDetails(this.SelectedSpawn);
		}

		private void grpSpawnEdit_Leave(object sender, EventArgs e)
		{
			this.UpdateSpawnDetails(this.SelectedSpawn);
		}

		private void grpSpawnEntries_Enter(object sender, EventArgs e)
		{
		}

		private void grpSpawnEntries_Leave(object sender, EventArgs e)
		{
			this.UpdateSpawnEntries();
			this.UpdateSpawnNode();
		}

		private bool HasEntry(SpawnPoint Spawn, int entrynum)
		{
			if (Spawn == null || Spawn.SpawnObjects == null)
			{
				return false;
			}
			return this.vScrollBar1.Value + entrynum - 1 < Spawn.SpawnObjects.Count;
		}

		private void highlightDetail_Popup(object sender, EventArgs e)
		{
			if (sender is System.Windows.Forms.ContextMenu)
			{
				Control sourceControl = ((System.Windows.Forms.ContextMenu)sender).SourceControl;
				bool item = false;
				string name = sourceControl.Name;
				if (name == null || name == string.Empty)
				{
					name = sourceControl.Parent.Name;
				}
				if (!this.ControlModHash.Contains(name))
				{
					item = false;
					this.ControlModHash.Add(name, item);
				}
				else
				{
					item = (bool)this.ControlModHash[name];
				}
				this.ControlModHash[name] = !item;
				Color window = SystemColors.Window;
				if (sourceControl is CheckBox)
				{
					window = this.tabControl1.BackColor;
				}
				if ((bool&)this.ControlModHash[name])
				{
					sourceControl.BackColor = Color.Yellow;
					return;
				}
				sourceControl.BackColor = window;
			}
		}

		public void ImportSpawnTypes(string filePath)
		{
			if (filePath == null || filePath.Length == 0)
			{
				return;
			}
			if (File.Exists(filePath))
			{
				this.clbRunUOTypes.BeginUpdate();
				this.clbRunUOTypes.Sorted = false;
				this.clbRunUOTypes.Items.Clear();
				try
				{
					XmlDocument xmlDocument = new XmlDocument();
					xmlDocument.Load(filePath);
					XmlElement item = xmlDocument["SpawnTypes"];
					if (item != null)
					{
						foreach (XmlElement elementsByTagName in item.GetElementsByTagName("T"))
						{
							this.clbRunUOTypes.Items.Add(elementsByTagName.InnerText);
						}
					}
				}
				catch (Exception exception1)
				{
					Exception exception = exception1;
					string[] strArrays = new string[] { "Failed to read Spawn Types file [", filePath, "] for the following reason:", Environment.NewLine, this.ExceptionMessage(exception) };
					MessageBox.Show(this, string.Concat(strArrays), "Read Failure", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
				this.clbRunUOTypes.Sorted = true;
				this.clbRunUOTypes.EndUpdate();
				this.clbRunUOTypes.Refresh();
				this.lblTotalTypesLoaded.Text = string.Concat("Types Loaded = ", this.clbRunUOTypes.Items.Count);
			}
		}

		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			ResourceManager resourceManager = new ResourceManager(typeof(SpawnEditor));
			this.axUOMap = new AxUOMap();
			this.ttpSpawnInfo = new ToolTip(this.components);
			this.btnSaveSpawn = new Button();
			this.btnLoadSpawn = new Button();
			this.mncLoad = new System.Windows.Forms.ContextMenu();
			this.mniForceLoad = new MenuItem();
			this.menuItem21 = new MenuItem();
			this.trkZoom = new TrackBar();
			this.chkDrawStatics = new CheckBox();
			this.radShowMobilesOnly = new RadioButton();
			this.radShowItemsOnly = new RadioButton();
			this.radShowAll = new RadioButton();
			this.clbRunUOTypes = new CheckedListBox();
			this.tvwSpawnPoints = new TreeView();
			this.btnResetTypes = new Button();
			this.btnMergeSpawn = new Button();
			this.mncMerge = new System.Windows.Forms.ContextMenu();
			this.mniForceMerge = new MenuItem();
			this.menuItem20 = new MenuItem();
			this.chkShowMapTip = new CheckBox();
			this.chkShowSpawns = new CheckBox();
			this.cbxMap = new ComboBox();
			this.chkSyncUO = new CheckBox();
			this.chkHomeRangeIsRelative = new CheckBox();
			this.highlightDetail = new System.Windows.Forms.ContextMenu();
			this.btnMove = new Button();
			this.btnRestoreSpawnDefaults = new Button();
			this.btnDeleteSpawn = new Button();
			this.btnUpdateSpawn = new Button();
			this.chkRunning = new CheckBox();
			this.spnMaxCount = new NumericUpDown();
			this.txtName = new TextBox();
			this.spnHomeRange = new NumericUpDown();
			this.spnMinDelay = new NumericUpDown();
			this.spnTeam = new NumericUpDown();
			this.chkGroup = new CheckBox();
			this.spnMaxDelay = new NumericUpDown();
			this.spnSpawnRange = new NumericUpDown();
			this.spnProximityRange = new NumericUpDown();
			this.spnMinRefract = new NumericUpDown();
			this.spnTODStart = new NumericUpDown();
			this.spnMaxRefract = new NumericUpDown();
			this.chkGameTOD = new CheckBox();
			this.chkRealTOD = new CheckBox();
			this.chkAllowGhost = new CheckBox();
			this.chkSmartSpawning = new CheckBox();
			this.chkSequentialSpawn = new CheckBox();
			this.chkSpawnOnTrigger = new CheckBox();
			this.spnDespawn = new NumericUpDown();
			this.spnTODEnd = new NumericUpDown();
			this.spnDuration = new NumericUpDown();
			this.spnProximitySnd = new NumericUpDown();
			this.spnKillReset = new NumericUpDown();
			this.tvwTemplates = new TreeView();
			this.chkTracking = new CheckBox();
			this.btnGo = new Button();
			this.chkInContainer = new CheckBox();
			this.spnTriggerProbability = new NumericUpDown();
			this.spnStackAmount = new NumericUpDown();
			this.chkExternalTriggering = new CheckBox();
			this.spnContainerX = new NumericUpDown();
			this.spnContainerY = new NumericUpDown();
			this.spnContainerZ = new NumericUpDown();
			this.chkLockSpawn = new CheckBox();
			this.chkDetails = new CheckBox();
			this.chkSnapRegion = new CheckBox();
			this.treeRegionView = new TreeView();
			this.treeGoView = new TreeView();
			this.checkSpawnFilter = new CheckBox();
			this.button1 = new Button();
			this.clbSpawnPack = new CheckedListBox();
			this.btnUpdateFromSpawnPack = new Button();
			this.btnAddToSpawnPack = new Button();
			this.btnUpdateSpawnPacks = new Button();
			this.tvwSpawnPacks = new TreeView();
			this.chkShade = new CheckBox();
			this.cbxShade = new ComboBox();
			this.label9 = new Label();
			this.label8 = new Label();
			this.label7 = new Label();
			this.label6 = new Label();
			this.label5 = new Label();
			this.label4 = new Label();
			this.label3 = new Label();
			this.label2 = new Label();
			this.label1 = new Label();
			this.label28 = new Label();
			this.label27 = new Label();
			this.label25 = new Label();
			this.label24 = new Label();
			this.label23 = new Label();
			this.label18 = new Label();
			this.label19 = new Label();
			this.label20 = new Label();
			this.label21 = new Label();
			this.label22 = new Label();
			this.lblMaxDelay = new Label();
			this.lblHomeRange = new Label();
			this.lblTeam = new Label();
			this.lblMaxCount = new Label();
			this.lblMinDelay = new Label();
			this.btnSendSpawn = new Button();
			this.unloadSpawners = new System.Windows.Forms.ContextMenu();
			this.mniUnloadSpawners = new MenuItem();
			this.menuItem19 = new MenuItem();
			this.label30 = new Label();
			this.btnFilterSettings = new Button();
			this.pnlControls = new Panel();
			this.lblTrkMax = new Label();
			this.lblTrkMin = new Label();
			this.tabControl3 = new TabControl();
			this.tabMapSettings = new TabPage();
			this.grpMapControl = new GroupBox();
			this.tabControl2 = new TabControl();
			this.tabPage3 = new TabPage();
			this.grpSpawnList = new GroupBox();
			this.lblTotalSpawn = new Label();
			this.tabPage4 = new TabPage();
			this.tabPage5 = new TabPage();
			this.progressBar1 = new ProgressBar();
			this.lblTransferStatus = new Label();
			this.groupTemplateList = new GroupBox();
			this.btnSaveTemplate = new Button();
			this.btnMergeTemplate = new Button();
			this.btnLoadTemplate = new Button();
			this.label29 = new Label();
			this.grpSpawnTypes = new GroupBox();
			this.lblTotalTypesLoaded = new Label();
			this.mncSpawns = new System.Windows.Forms.ContextMenu();
			this.menuItem3 = new MenuItem();
			this.mniDeleteSpawn = new MenuItem();
			this.mniDeleteAllSpawns = new MenuItem();
			this.ofdLoadFile = new OpenFileDialog();
			this.sfdSaveFile = new SaveFileDialog();
			this.stbMain = new StatusBar();
			this.grpSpawnEntries = new GroupBox();
			this.entryPer8 = new NumericUpDown();
			this.entryPer7 = new NumericUpDown();
			this.entryPer6 = new NumericUpDown();
			this.entryPer5 = new NumericUpDown();
			this.entryPer4 = new NumericUpDown();
			this.entryPer3 = new NumericUpDown();
			this.entryPer2 = new NumericUpDown();
			this.entryPer1 = new NumericUpDown();
			this.entryMaxD8 = new TextBox();
			this.entryMaxD7 = new TextBox();
			this.entryMaxD6 = new TextBox();
			this.entryMaxD5 = new TextBox();
			this.entryMaxD4 = new TextBox();
			this.entryMaxD3 = new TextBox();
			this.entryMaxD2 = new TextBox();
			this.entryMaxD1 = new TextBox();
			this.entryMinD8 = new TextBox();
			this.entryMinD7 = new TextBox();
			this.entryMinD6 = new TextBox();
			this.entryMinD5 = new TextBox();
			this.entryMinD4 = new TextBox();
			this.entryMinD3 = new TextBox();
			this.entryMinD2 = new TextBox();
			this.entryMinD1 = new TextBox();
			this.entryKills8 = new TextBox();
			this.entryKills7 = new TextBox();
			this.entryKills6 = new TextBox();
			this.entryKills5 = new TextBox();
			this.entryKills4 = new TextBox();
			this.entryKills3 = new TextBox();
			this.entryKills2 = new TextBox();
			this.entryKills1 = new TextBox();
			this.entryReset8 = new TextBox();
			this.entryReset7 = new TextBox();
			this.entryReset6 = new TextBox();
			this.entryReset5 = new TextBox();
			this.entryReset4 = new TextBox();
			this.entryReset3 = new TextBox();
			this.entryReset2 = new TextBox();
			this.entryReset1 = new TextBox();
			this.entryTo8 = new TextBox();
			this.entrySub8 = new TextBox();
			this.chkRK8 = new CheckBox();
			this.entryMax8 = new NumericUpDown();
			this.btnEntryEdit8 = new Button();
			this.entryText8 = new TextBox();
			this.deleteEntry = new System.Windows.Forms.ContextMenu();
			this.menuItem1 = new MenuItem();
			this.menuItem2 = new MenuItem();
			this.menuItem15 = new MenuItem();
			this.chkClr8 = new CheckBox();
			this.entryTo7 = new TextBox();
			this.entrySub7 = new TextBox();
			this.chkRK7 = new CheckBox();
			this.entryMax7 = new NumericUpDown();
			this.btnEntryEdit7 = new Button();
			this.entryText7 = new TextBox();
			this.chkClr7 = new CheckBox();
			this.entryTo6 = new TextBox();
			this.entrySub6 = new TextBox();
			this.chkRK6 = new CheckBox();
			this.entryMax6 = new NumericUpDown();
			this.btnEntryEdit6 = new Button();
			this.entryText6 = new TextBox();
			this.chkClr6 = new CheckBox();
			this.entryTo5 = new TextBox();
			this.entrySub5 = new TextBox();
			this.chkRK5 = new CheckBox();
			this.entryMax5 = new NumericUpDown();
			this.btnEntryEdit5 = new Button();
			this.entryText5 = new TextBox();
			this.chkClr5 = new CheckBox();
			this.entryTo4 = new TextBox();
			this.entrySub4 = new TextBox();
			this.chkRK4 = new CheckBox();
			this.entryMax4 = new NumericUpDown();
			this.btnEntryEdit4 = new Button();
			this.entryText4 = new TextBox();
			this.chkClr4 = new CheckBox();
			this.entryTo3 = new TextBox();
			this.entrySub3 = new TextBox();
			this.chkRK3 = new CheckBox();
			this.entryMax3 = new NumericUpDown();
			this.btnEntryEdit3 = new Button();
			this.entryText3 = new TextBox();
			this.chkClr3 = new CheckBox();
			this.entryTo2 = new TextBox();
			this.entrySub2 = new TextBox();
			this.chkRK2 = new CheckBox();
			this.entryMax2 = new NumericUpDown();
			this.btnEntryEdit2 = new Button();
			this.entryText2 = new TextBox();
			this.chkClr2 = new CheckBox();
			this.entryTo1 = new TextBox();
			this.vScrollBar1 = new VScrollBar();
			this.entrySub1 = new TextBox();
			this.chkRK1 = new CheckBox();
			this.entryMax1 = new NumericUpDown();
			this.btnEntryEdit1 = new Button();
			this.entryText1 = new TextBox();
			this.chkClr1 = new CheckBox();
			this.editEntryMenu1 = new System.Windows.Forms.ContextMenu();
			this.grpSpawnEdit = new GroupBox();
			this.btnSendSingleSpawner = new Button();
			this.unloadSingleSpawner = new System.Windows.Forms.ContextMenu();
			this.mniUnloadSingleSpawner = new MenuItem();
			this.menuItem23 = new MenuItem();
			this.label26 = new Label();
			this.textTrigObjectProp = new TextBox();
			this.label17 = new Label();
			this.textSkillTrigger = new TextBox();
			this.label16 = new Label();
			this.textSpeechTrigger = new TextBox();
			this.label15 = new Label();
			this.textProximityMsg = new TextBox();
			this.label14 = new Label();
			this.textPlayerTrigProp = new TextBox();
			this.label12 = new Label();
			this.textNoTriggerOnCarried = new TextBox();
			this.label11 = new Label();
			this.textTriggerOnCarried = new TextBox();
			this.mainMenu1 = new MainMenu();
			this.menuItem5 = new MenuItem();
			this.menuItem6 = new MenuItem();
			this.menuItem7 = new MenuItem();
			this.menuItem10 = new MenuItem();
			this.menuItem11 = new MenuItem();
			this.menuItem12 = new MenuItem();
			this.menuItem13 = new MenuItem();
			this.menuItem22 = new MenuItem();
			this.menuItem24 = new MenuItem();
			this.mniDeleteInSelectionWindow = new MenuItem();
			this.mniDeleteNotSelected = new MenuItem();
			this.mniToolbarDeleteAllSpawns = new MenuItem();
			this.mniDeleteAllFiltered = new MenuItem();
			this.mniDeleteAllUnfiltered = new MenuItem();
			this.menuItem25 = new MenuItem();
			this.mniModifyInSelectionWindow = new MenuItem();
			this.mniModifiedUnfiltered = new MenuItem();
			this.menuItem8 = new MenuItem();
			this.menuItem9 = new MenuItem();
			this.menuItem17 = new MenuItem();
			this.mniDisplayFilterSettings = new MenuItem();
			this.menuItem14 = new MenuItem();
			this.mniAlwaysOnTop = new MenuItem();
			this.menuItem16 = new MenuItem();
			this.menuItem18 = new MenuItem();
			this.menuItem4 = new MenuItem();
			this.panel1 = new Panel();
			this.tabControl1 = new TabControl();
			this.tabBasic = new TabPage();
			this.tabAdvanced = new TabPage();
			this.groupBox1 = new GroupBox();
			this.label44 = new Label();
			this.txtNotes = new TextBox();
			this.label37 = new Label();
			this.textRegionName = new TextBox();
			this.label36 = new Label();
			this.textWayPoint = new TextBox();
			this.label35 = new Label();
			this.textConfigFile = new TextBox();
			this.label34 = new Label();
			this.textSetObjectName = new TextBox();
			this.label33 = new Label();
			this.textTrigObjectName = new TextBox();
			this.labelContainerZ = new Label();
			this.labelContainerY = new Label();
			this.labelContainerX = new Label();
			this.label32 = new Label();
			this.label31 = new Label();
			this.label13 = new Label();
			this.textMobTriggerName = new TextBox();
			this.label10 = new Label();
			this.textMobTrigProp = new TextBox();
			this.tabSpawnTypes = new TabPage();
			this.groupBox3 = new GroupBox();
			this.groupBox2 = new GroupBox();
			this.textSpawnPackName = new TextBox();
			this.label39 = new Label();
			this.panel3 = new Panel();
			this.helpProvider1 = new HelpProvider();
			this.mcnSpawnPack = new System.Windows.Forms.ContextMenu();
			this.mniDeleteType = new MenuItem();
			this.mniDeleteAllTypes = new MenuItem();
			this.mcnSpawnPacks = new System.Windows.Forms.ContextMenu();
			this.mniDeletePack = new MenuItem();
			this.openSpawnPacks = new OpenFileDialog();
			this.saveSpawnPacks = new SaveFileDialog();
			this.exportAllSpawnTypes = new SaveFileDialog();
			this.importAllSpawnTypes = new OpenFileDialog();
			this.importMapFile = new OpenFileDialog();
			this.importMSFFile = new OpenFileDialog();
			this.axUOMap.BeginInit();
			((ISupportInitialize)this.trkZoom).BeginInit();
			((ISupportInitialize)this.spnMaxCount).BeginInit();
			((ISupportInitialize)this.spnHomeRange).BeginInit();
			((ISupportInitialize)this.spnMinDelay).BeginInit();
			((ISupportInitialize)this.spnTeam).BeginInit();
			((ISupportInitialize)this.spnMaxDelay).BeginInit();
			((ISupportInitialize)this.spnSpawnRange).BeginInit();
			((ISupportInitialize)this.spnProximityRange).BeginInit();
			((ISupportInitialize)this.spnMinRefract).BeginInit();
			((ISupportInitialize)this.spnTODStart).BeginInit();
			((ISupportInitialize)this.spnMaxRefract).BeginInit();
			((ISupportInitialize)this.spnDespawn).BeginInit();
			((ISupportInitialize)this.spnTODEnd).BeginInit();
			((ISupportInitialize)this.spnDuration).BeginInit();
			((ISupportInitialize)this.spnProximitySnd).BeginInit();
			((ISupportInitialize)this.spnKillReset).BeginInit();
			((ISupportInitialize)this.spnTriggerProbability).BeginInit();
			((ISupportInitialize)this.spnStackAmount).BeginInit();
			((ISupportInitialize)this.spnContainerX).BeginInit();
			((ISupportInitialize)this.spnContainerY).BeginInit();
			((ISupportInitialize)this.spnContainerZ).BeginInit();
			this.pnlControls.SuspendLayout();
			this.tabControl3.SuspendLayout();
			this.tabMapSettings.SuspendLayout();
			this.grpMapControl.SuspendLayout();
			this.tabControl2.SuspendLayout();
			this.tabPage3.SuspendLayout();
			this.grpSpawnList.SuspendLayout();
			this.tabPage4.SuspendLayout();
			this.tabPage5.SuspendLayout();
			this.groupTemplateList.SuspendLayout();
			this.grpSpawnTypes.SuspendLayout();
			this.grpSpawnEntries.SuspendLayout();
			((ISupportInitialize)this.entryPer8).BeginInit();
			((ISupportInitialize)this.entryPer7).BeginInit();
			((ISupportInitialize)this.entryPer6).BeginInit();
			((ISupportInitialize)this.entryPer5).BeginInit();
			((ISupportInitialize)this.entryPer4).BeginInit();
			((ISupportInitialize)this.entryPer3).BeginInit();
			((ISupportInitialize)this.entryPer2).BeginInit();
			((ISupportInitialize)this.entryPer1).BeginInit();
			((ISupportInitialize)this.entryMax8).BeginInit();
			((ISupportInitialize)this.entryMax7).BeginInit();
			((ISupportInitialize)this.entryMax6).BeginInit();
			((ISupportInitialize)this.entryMax5).BeginInit();
			((ISupportInitialize)this.entryMax4).BeginInit();
			((ISupportInitialize)this.entryMax3).BeginInit();
			((ISupportInitialize)this.entryMax2).BeginInit();
			((ISupportInitialize)this.entryMax1).BeginInit();
			this.grpSpawnEdit.SuspendLayout();
			this.panel1.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.tabBasic.SuspendLayout();
			this.tabAdvanced.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.tabSpawnTypes.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.panel3.SuspendLayout();
			base.SuspendLayout();
			this.axUOMap.AllowDrop = true;
			this.axUOMap.ContainingControl = this;
			this.axUOMap.Enabled = true;
			this.axUOMap.Location = new Point(8, 0);
			this.axUOMap.Name = "axUOMap";
			this.axUOMap.OcxState = (AxHost.State)resourceManager.GetObject("axUOMap.OcxState");
			this.axUOMap.Size = new System.Drawing.Size(472, 464);
			this.axUOMap.TabIndex = 1;
			this.axUOMap.add_MouseMoveEvent(new _DUOMapEvents_MouseMoveEventHandler(this, SpawnEditor.axUOMap_MouseMoveEvent));
			this.axUOMap.add_MouseDownEvent(new _DUOMapEvents_MouseDownEventHandler(this, SpawnEditor.axUOMap_MouseDownEvent));
			this.axUOMap.add_MouseUpEvent(new _DUOMapEvents_MouseUpEventHandler(this, SpawnEditor.axUOMap_MouseUpEvent));
			this.ttpSpawnInfo.AutoPopDelay = 5000;
			this.ttpSpawnInfo.InitialDelay = 500;
			this.ttpSpawnInfo.ReshowDelay = 100;
			this.btnSaveSpawn.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			this.btnSaveSpawn.Location = new Point(112, 32);
			this.btnSaveSpawn.Name = "btnSaveSpawn";
			this.btnSaveSpawn.Size = new System.Drawing.Size(48, 24);
			this.btnSaveSpawn.TabIndex = 2;
			this.btnSaveSpawn.Text = "&Save";
			this.ttpSpawnInfo.SetToolTip(this.btnSaveSpawn, "Saves the current spawn list.");
			this.btnSaveSpawn.Click += new EventHandler(this.btnSaveSpawn_Click);
			this.btnLoadSpawn.ContextMenu = this.mncLoad;
			this.btnLoadSpawn.Location = new Point(8, 32);
			this.btnLoadSpawn.Name = "btnLoadSpawn";
			this.btnLoadSpawn.Size = new System.Drawing.Size(40, 24);
			this.btnLoadSpawn.TabIndex = 0;
			this.btnLoadSpawn.Text = "&Load";
			this.ttpSpawnInfo.SetToolTip(this.btnLoadSpawn, "Clears the currently defined spawns, if any, and loads a spawn file.  Right-Click on the Load button to bring up a menu to force loading a spawn file into the currently selected map.  This can be used to convert a spawn file from one map to another.");
			this.btnLoadSpawn.Click += new EventHandler(this.btnLoadSpawn_Click);
			System.Windows.Forms.Menu.MenuItemCollection menuItems = this.mncLoad.MenuItems;
			MenuItem[] menuItemArray = new MenuItem[] { this.mniForceLoad, this.menuItem21 };
			menuItems.AddRange(menuItemArray);
			this.mncLoad.Popup += new EventHandler(this.mncLoad_Popup);
			this.mniForceLoad.Index = 0;
			this.mniForceLoad.Text = "Force Load Into Current Map...";
			this.mniForceLoad.Click += new EventHandler(this.mniForceLoad_Click);
			this.menuItem21.Index = 1;
			this.menuItem21.Text = "Cancel";
			this.trkZoom.AutoSize = false;
			this.trkZoom.LargeChange = 2;
			this.trkZoom.Location = new Point(16, 168);
			this.trkZoom.Maximum = 4;
			this.trkZoom.Minimum = -4;
			this.trkZoom.Name = "trkZoom";
			this.trkZoom.Size = new System.Drawing.Size(152, 32);
			this.trkZoom.TabIndex = 5;
			this.trkZoom.TickStyle = TickStyle.TopLeft;
			this.ttpSpawnInfo.SetToolTip(this.trkZoom, "Zooms in/out of map.");
			this.trkZoom.ValueChanged += new EventHandler(this.trkZoom_ValueChanged);
			this.trkZoom.Scroll += new EventHandler(this.trkZoom_Scroll);
			this.chkDrawStatics.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			this.chkDrawStatics.CheckAlign = ContentAlignment.MiddleRight;
			this.chkDrawStatics.Location = new Point(77, 8);
			this.chkDrawStatics.Name = "chkDrawStatics";
			this.chkDrawStatics.Size = new System.Drawing.Size(80, 16);
			this.chkDrawStatics.TabIndex = 1;
			this.chkDrawStatics.Text = "Statics";
			this.ttpSpawnInfo.SetToolTip(this.chkDrawStatics, "Draws static tiles on the map.");
			this.chkDrawStatics.CheckedChanged += new EventHandler(this.chkDrawStatics_CheckedChanged);
			this.radShowMobilesOnly.Location = new Point(104, 16);
			this.radShowMobilesOnly.Name = "radShowMobilesOnly";
			this.radShowMobilesOnly.Size = new System.Drawing.Size(64, 16);
			this.radShowMobilesOnly.TabIndex = 2;
			this.radShowMobilesOnly.Text = "Mobiles";
			this.ttpSpawnInfo.SetToolTip(this.radShowMobilesOnly, "Shows only mobile based spawn objects.");
			this.radShowMobilesOnly.CheckedChanged += new EventHandler(this.TypeSelectionChanged);
			this.radShowItemsOnly.Location = new Point(56, 16);
			this.radShowItemsOnly.Name = "radShowItemsOnly";
			this.radShowItemsOnly.Size = new System.Drawing.Size(64, 16);
			this.radShowItemsOnly.TabIndex = 1;
			this.radShowItemsOnly.Text = "Items";
			this.ttpSpawnInfo.SetToolTip(this.radShowItemsOnly, "Shows only item based spawn objects.");
			this.radShowItemsOnly.CheckedChanged += new EventHandler(this.TypeSelectionChanged);
			this.radShowAll.Checked = true;
			this.radShowAll.Location = new Point(8, 16);
			this.radShowAll.Name = "radShowAll";
			this.radShowAll.Size = new System.Drawing.Size(56, 16);
			this.radShowAll.TabIndex = 0;
			this.radShowAll.TabStop = true;
			this.radShowAll.Text = "All";
			this.ttpSpawnInfo.SetToolTip(this.radShowAll, "Shows all types of spawn objects (items/mobiles).");
			this.radShowAll.CheckedChanged += new EventHandler(this.TypeSelectionChanged);
			this.clbRunUOTypes.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.clbRunUOTypes.CheckOnClick = true;
			this.clbRunUOTypes.HorizontalScrollbar = true;
			this.clbRunUOTypes.IntegralHeight = false;
			this.clbRunUOTypes.Location = new Point(8, 96);
			this.clbRunUOTypes.Name = "clbRunUOTypes";
			this.clbRunUOTypes.Size = new System.Drawing.Size(160, 320);
			this.clbRunUOTypes.TabIndex = 4;
			this.clbRunUOTypes.ThreeDCheckBoxes = true;
			this.ttpSpawnInfo.SetToolTip(this.clbRunUOTypes, "List of all spawnable objects.");
			this.tvwSpawnPoints.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.tvwSpawnPoints.ImageIndex = -1;
			this.tvwSpawnPoints.Location = new Point(8, 80);
			this.tvwSpawnPoints.Name = "tvwSpawnPoints";
			this.tvwSpawnPoints.SelectedImageIndex = -1;
			this.tvwSpawnPoints.Size = new System.Drawing.Size(152, 352);
			this.tvwSpawnPoints.TabIndex = 3;
			this.ttpSpawnInfo.SetToolTip(this.tvwSpawnPoints, "List of currently defined spawns.  Right-Click for a context menu based on the currently selected spawn.");
			this.tvwSpawnPoints.MouseUp += new MouseEventHandler(this.tvwSpawnPoints_MouseUp);
			this.btnResetTypes.Location = new Point(8, 35);
			this.btnResetTypes.Name = "btnResetTypes";
			this.btnResetTypes.Size = new System.Drawing.Size(160, 20);
			this.btnResetTypes.TabIndex = 3;
			this.btnResetTypes.Text = "&Clear Selections";
			this.ttpSpawnInfo.SetToolTip(this.btnResetTypes, "Clears current selections from the type list.");
			this.btnResetTypes.Click += new EventHandler(this.btnResetTypes_Click);
			this.btnMergeSpawn.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.btnMergeSpawn.ContextMenu = this.mncMerge;
			this.btnMergeSpawn.Location = new Point(48, 32);
			this.btnMergeSpawn.Name = "btnMergeSpawn";
			this.btnMergeSpawn.Size = new System.Drawing.Size(64, 24);
			this.btnMergeSpawn.TabIndex = 1;
			this.btnMergeSpawn.Text = "&Merge";
			this.ttpSpawnInfo.SetToolTip(this.btnMergeSpawn, "Loads a spawn file WITHOUT clearing the current spawn list.  Right-Click on the Merge button to bring up a menu to force merging a spawn file into the currently selected map.  This can be used to convert a spawn file from one map to another.");
			this.btnMergeSpawn.Click += new EventHandler(this.btnMergeSpawn_Click);
			System.Windows.Forms.Menu.MenuItemCollection menuItemCollections = this.mncMerge.MenuItems;
			menuItemArray = new MenuItem[] { this.mniForceMerge, this.menuItem20 };
			menuItemCollections.AddRange(menuItemArray);
			this.mniForceMerge.Index = 0;
			this.mniForceMerge.Text = "Force Merge Into Current Map...";
			this.mniForceMerge.Click += new EventHandler(this.mniForceMerge_Click);
			this.menuItem20.Index = 1;
			this.menuItem20.Text = "Cancel";
			this.chkShowMapTip.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			this.chkShowMapTip.CheckAlign = ContentAlignment.MiddleRight;
			this.chkShowMapTip.Checked = true;
			this.chkShowMapTip.CheckState = CheckState.Checked;
			this.chkShowMapTip.Location = new Point(77, 24);
			this.chkShowMapTip.Name = "chkShowMapTip";
			this.chkShowMapTip.Size = new System.Drawing.Size(80, 16);
			this.chkShowMapTip.TabIndex = 2;
			this.chkShowMapTip.Text = "Spawn Tip";
			this.ttpSpawnInfo.SetToolTip(this.chkShowMapTip, "Turns on/off the spawn tool tip when hovering over a spawn.");
			this.chkShowSpawns.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			this.chkShowSpawns.CheckAlign = ContentAlignment.MiddleRight;
			this.chkShowSpawns.Checked = true;
			this.chkShowSpawns.CheckState = CheckState.Checked;
			this.chkShowSpawns.Location = new Point(77, 40);
			this.chkShowSpawns.Name = "chkShowSpawns";
			this.chkShowSpawns.Size = new System.Drawing.Size(80, 16);
			this.chkShowSpawns.TabIndex = 3;
			this.chkShowSpawns.Text = "Spawns";
			this.ttpSpawnInfo.SetToolTip(this.chkShowSpawns, "Turns on/off drawing of spawn points.");
			this.chkShowSpawns.CheckedChanged += new EventHandler(this.chkShowSpawns_CheckedChanged);
			this.cbxMap.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			this.cbxMap.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cbxMap.Location = new Point(85, 80);
			this.cbxMap.Name = "cbxMap";
			this.cbxMap.Size = new System.Drawing.Size(77, 21);
			this.cbxMap.TabIndex = 4;
			this.ttpSpawnInfo.SetToolTip(this.cbxMap, "Changes the current map.");
			this.cbxMap.SelectedIndexChanged += new EventHandler(this.cbxMap_SelectedIndexChanged);
			this.chkSyncUO.Location = new Point(8, 64);
			this.chkSyncUO.Name = "chkSyncUO";
			this.chkSyncUO.Size = new System.Drawing.Size(48, 16);
			this.chkSyncUO.TabIndex = 6;
			this.chkSyncUO.Text = "Sync:";
			this.ttpSpawnInfo.SetToolTip(this.chkSyncUO, "Automatically move player to spawner locations when they are selected.");
			this.chkHomeRangeIsRelative.CheckAlign = ContentAlignment.MiddleRight;
			this.chkHomeRangeIsRelative.Checked = true;
			this.chkHomeRangeIsRelative.CheckState = CheckState.Checked;
			this.chkHomeRangeIsRelative.ContextMenu = this.highlightDetail;
			this.chkHomeRangeIsRelative.Location = new Point(8, 216);
			this.chkHomeRangeIsRelative.Name = "chkHomeRangeIsRelative";
			this.chkHomeRangeIsRelative.Size = new System.Drawing.Size(104, 16);
			this.chkHomeRangeIsRelative.TabIndex = 13;
			this.chkHomeRangeIsRelative.Text = "RelativeHome:";
			this.ttpSpawnInfo.SetToolTip(this.chkHomeRangeIsRelative, "Check if the object to be spawned should set its home point base on its spawned location and not the spawners location.");
			this.highlightDetail.Popup += new EventHandler(this.highlightDetail_Popup);
			this.btnMove.Enabled = false;
			this.btnMove.Location = new Point(192, 408);
			this.btnMove.Name = "btnMove";
			this.btnMove.Size = new System.Drawing.Size(32, 23);
			this.btnMove.TabIndex = 17;
			this.btnMove.Text = "&XY";
			this.ttpSpawnInfo.SetToolTip(this.btnMove, "Adjusted the spawners boundaries.");
			this.btnMove.Click += new EventHandler(this.btnMove_Click);
			this.btnRestoreSpawnDefaults.Location = new Point(8, 408);
			this.btnRestoreSpawnDefaults.Name = "btnRestoreSpawnDefaults";
			this.btnRestoreSpawnDefaults.Size = new System.Drawing.Size(96, 23);
			this.btnRestoreSpawnDefaults.TabIndex = 14;
			this.btnRestoreSpawnDefaults.Text = "Restore Defaults";
			this.ttpSpawnInfo.SetToolTip(this.btnRestoreSpawnDefaults, "Restores the spawn details to the default values.");
			this.btnRestoreSpawnDefaults.Click += new EventHandler(this.btnRestoreSpawnDefaults_Click);
			this.btnDeleteSpawn.Enabled = false;
			this.btnDeleteSpawn.Location = new Point(104, 408);
			this.btnDeleteSpawn.Name = "btnDeleteSpawn";
			this.btnDeleteSpawn.Size = new System.Drawing.Size(88, 23);
			this.btnDeleteSpawn.TabIndex = 16;
			this.btnDeleteSpawn.Text = "&Delete Spawn";
			this.ttpSpawnInfo.SetToolTip(this.btnDeleteSpawn, "Deletes the currently selected spawn.");
			this.btnDeleteSpawn.Click += new EventHandler(this.btnDeleteSpawn_Click);
			this.btnUpdateSpawn.Enabled = false;
			this.btnUpdateSpawn.Location = new Point(8, 55);
			this.btnUpdateSpawn.Name = "btnUpdateSpawn";
			this.btnUpdateSpawn.Size = new System.Drawing.Size(160, 20);
			this.btnUpdateSpawn.TabIndex = 15;
			this.btnUpdateSpawn.Text = "&Add to Spawner";
			this.ttpSpawnInfo.SetToolTip(this.btnUpdateSpawn, "Updates the currently selected spawn with the selected types.");
			this.btnUpdateSpawn.Click += new EventHandler(this.btnUpdateSpawn_Click);
			this.chkRunning.CheckAlign = ContentAlignment.MiddleRight;
			this.chkRunning.Checked = true;
			this.chkRunning.CheckState = CheckState.Checked;
			this.chkRunning.ContextMenu = this.highlightDetail;
			this.chkRunning.Location = new Point(8, 200);
			this.chkRunning.Name = "chkRunning";
			this.chkRunning.Size = new System.Drawing.Size(104, 16);
			this.chkRunning.TabIndex = 12;
			this.chkRunning.Text = "Running:";
			this.ttpSpawnInfo.SetToolTip(this.chkRunning, "Check if the spawner should be running.");
			this.spnMaxCount.ContextMenu = this.highlightDetail;
			this.spnMaxCount.Location = new Point(96, 60);
			NumericUpDown num = this.spnMaxCount;
			int[] numArray = new int[] { 65535, 0, 0, 0 };
			num.Maximum = new decimal(numArray);
			this.spnMaxCount.Name = "spnMaxCount";
			this.spnMaxCount.Size = new System.Drawing.Size(72, 20);
			this.spnMaxCount.TabIndex = 4;
			this.ttpSpawnInfo.SetToolTip(this.spnMaxCount, "Absolute maximum number of objects to be spawned by this spawner.");
			NumericUpDown numericUpDown = this.spnMaxCount;
			numArray = new int[] { 1, 0, 0, 0 };
			numericUpDown.Value = new decimal(numArray);
			this.spnMaxCount.Enter += new EventHandler(this.TextEntryControl_Enter);
			this.txtName.ContextMenu = this.highlightDetail;
			this.txtName.Location = new Point(8, 16);
			this.txtName.Name = "txtName";
			this.txtName.Size = new System.Drawing.Size(168, 20);
			this.txtName.TabIndex = 0;
			this.txtName.Text = "Spawn";
			this.ttpSpawnInfo.SetToolTip(this.txtName, "Name of the spawner.");
			this.txtName.Leave += new EventHandler(this.txtName_Leave);
			this.txtName.MouseLeave += new EventHandler(this.txtName_MouseLeave);
			this.txtName.KeyUp += new KeyEventHandler(this.txtName_KeyUp);
			this.txtName.Enter += new EventHandler(this.TextEntryControl_Enter);
			this.spnHomeRange.BackColor = SystemColors.Window;
			this.spnHomeRange.ContextMenu = this.highlightDetail;
			this.spnHomeRange.Location = new Point(96, 40);
			NumericUpDown num1 = this.spnHomeRange;
			numArray = new int[] { 65535, 0, 0, 0 };
			num1.Maximum = new decimal(numArray);
			this.spnHomeRange.Name = "spnHomeRange";
			this.spnHomeRange.Size = new System.Drawing.Size(72, 20);
			this.spnHomeRange.TabIndex = 2;
			this.ttpSpawnInfo.SetToolTip(this.spnHomeRange, "Maximum wandering range of the spawn from its spawned location.");
			NumericUpDown numericUpDown1 = this.spnHomeRange;
			numArray = new int[] { 5, 0, 0, 0 };
			numericUpDown1.Value = new decimal(numArray);
			this.spnHomeRange.Enter += new EventHandler(this.TextEntryControl_Enter);
			this.spnMinDelay.ContextMenu = this.highlightDetail;
			this.spnMinDelay.DecimalPlaces = 1;
			this.spnMinDelay.Location = new Point(96, 80);
			NumericUpDown num2 = this.spnMinDelay;
			numArray = new int[] { 65535000, 0, 0, 0 };
			num2.Maximum = new decimal(numArray);
			this.spnMinDelay.Name = "spnMinDelay";
			this.spnMinDelay.Size = new System.Drawing.Size(72, 20);
			this.spnMinDelay.TabIndex = 6;
			this.ttpSpawnInfo.SetToolTip(this.spnMinDelay, "Minimum delay to respawn (in minutes).");
			NumericUpDown numericUpDown2 = this.spnMinDelay;
			numArray = new int[] { 5, 0, 0, 0 };
			numericUpDown2.Value = new decimal(numArray);
			this.spnMinDelay.Enter += new EventHandler(this.TextEntryControl_Enter);
			this.spnMinDelay.ValueChanged += new EventHandler(this.spnMinDelay_ValueChanged);
			this.spnTeam.ContextMenu = this.highlightDetail;
			this.spnTeam.Location = new Point(96, 120);
			NumericUpDown num3 = this.spnTeam;
			numArray = new int[] { 65535, 0, 0, 0 };
			num3.Maximum = new decimal(numArray);
			this.spnTeam.Name = "spnTeam";
			this.spnTeam.Size = new System.Drawing.Size(72, 20);
			this.spnTeam.TabIndex = 10;
			this.ttpSpawnInfo.SetToolTip(this.spnTeam, "Team that spawned object will belong to.");
			this.spnTeam.Enter += new EventHandler(this.TextEntryControl_Enter);
			this.chkGroup.CheckAlign = ContentAlignment.MiddleRight;
			this.chkGroup.ContextMenu = this.highlightDetail;
			this.chkGroup.Location = new Point(8, 184);
			this.chkGroup.Name = "chkGroup";
			this.chkGroup.Size = new System.Drawing.Size(104, 16);
			this.chkGroup.TabIndex = 11;
			this.chkGroup.Text = "Group:";
			this.ttpSpawnInfo.SetToolTip(this.chkGroup, "Check if the spawned object belongs to a group.");
			this.spnMaxDelay.ContextMenu = this.highlightDetail;
			this.spnMaxDelay.DecimalPlaces = 1;
			this.spnMaxDelay.Location = new Point(96, 100);
			NumericUpDown numericUpDown3 = this.spnMaxDelay;
			numArray = new int[] { 65535000, 0, 0, 0 };
			numericUpDown3.Maximum = new decimal(numArray);
			this.spnMaxDelay.Name = "spnMaxDelay";
			this.spnMaxDelay.Size = new System.Drawing.Size(72, 20);
			this.spnMaxDelay.TabIndex = 8;
			this.ttpSpawnInfo.SetToolTip(this.spnMaxDelay, "Maximum delay to respawn (in minutes).");
			NumericUpDown num4 = this.spnMaxDelay;
			numArray = new int[] { 10, 0, 0, 0 };
			num4.Value = new decimal(numArray);
			this.spnMaxDelay.Enter += new EventHandler(this.TextEntryControl_Enter);
			this.spnSpawnRange.ContextMenu = this.highlightDetail;
			this.spnSpawnRange.Location = new Point(96, 140);
			NumericUpDown numericUpDown4 = this.spnSpawnRange;
			numArray = new int[] { 65535, 0, 0, 0 };
			numericUpDown4.Maximum = new decimal(numArray);
			NumericUpDown num5 = this.spnSpawnRange;
			numArray = new int[] { 1, 0, 0, -2147483648 };
			num5.Minimum = new decimal(numArray);
			this.spnSpawnRange.Name = "spnSpawnRange";
			this.spnSpawnRange.Size = new System.Drawing.Size(72, 20);
			this.spnSpawnRange.TabIndex = 180;
			this.ttpSpawnInfo.SetToolTip(this.spnSpawnRange, "Maximum spawning range.  A value of -1 means the range is specified by XY.");
			NumericUpDown numericUpDown5 = this.spnSpawnRange;
			numArray = new int[] { 1, 0, 0, -2147483648 };
			numericUpDown5.Value = new decimal(numArray);
			this.spnSpawnRange.ValueChanged += new EventHandler(this.spnSpawnRange_ValueChanged);
			this.spnProximityRange.ContextMenu = this.highlightDetail;
			this.spnProximityRange.Location = new Point(96, 160);
			NumericUpDown num6 = this.spnProximityRange;
			numArray = new int[] { 65535, 0, 0, 0 };
			num6.Maximum = new decimal(numArray);
			NumericUpDown numericUpDown6 = this.spnProximityRange;
			numArray = new int[] { 2, 0, 0, -2147483648 };
			numericUpDown6.Minimum = new decimal(numArray);
			this.spnProximityRange.Name = "spnProximityRange";
			this.spnProximityRange.Size = new System.Drawing.Size(72, 20);
			this.spnProximityRange.TabIndex = 178;
			this.ttpSpawnInfo.SetToolTip(this.spnProximityRange, "Maximum range within which a player can trigger the spawner.");
			NumericUpDown num7 = this.spnProximityRange;
			numArray = new int[] { 1, 0, 0, -2147483648 };
			num7.Value = new decimal(numArray);
			this.spnMinRefract.ContextMenu = this.highlightDetail;
			this.spnMinRefract.DecimalPlaces = 1;
			this.spnMinRefract.Location = new Point(280, 60);
			NumericUpDown numericUpDown7 = this.spnMinRefract;
			numArray = new int[] { 65535000, 0, 0, 0 };
			numericUpDown7.Maximum = new decimal(numArray);
			this.spnMinRefract.Name = "spnMinRefract";
			this.spnMinRefract.Size = new System.Drawing.Size(72, 20);
			this.spnMinRefract.TabIndex = 182;
			this.ttpSpawnInfo.SetToolTip(this.spnMinRefract, "Minimum delay after triggering when the spawner can be triggered again (in minutes).");
			this.spnTODStart.ContextMenu = this.highlightDetail;
			this.spnTODStart.DecimalPlaces = 1;
			this.spnTODStart.Location = new Point(280, 100);
			NumericUpDown num8 = this.spnTODStart;
			numArray = new int[] { 65535, 0, 0, 0 };
			num8.Maximum = new decimal(numArray);
			this.spnTODStart.Name = "spnTODStart";
			this.spnTODStart.Size = new System.Drawing.Size(72, 20);
			this.spnTODStart.TabIndex = 186;
			this.ttpSpawnInfo.SetToolTip(this.spnTODStart, "Starting hour after which spawning can occur.");
			this.spnMaxRefract.ContextMenu = this.highlightDetail;
			this.spnMaxRefract.DecimalPlaces = 1;
			this.spnMaxRefract.Location = new Point(280, 80);
			NumericUpDown numericUpDown8 = this.spnMaxRefract;
			numArray = new int[] { 65535000, 0, 0, 0 };
			numericUpDown8.Maximum = new decimal(numArray);
			this.spnMaxRefract.Name = "spnMaxRefract";
			this.spnMaxRefract.Size = new System.Drawing.Size(72, 20);
			this.spnMaxRefract.TabIndex = 184;
			this.ttpSpawnInfo.SetToolTip(this.spnMaxRefract, "Maximum delay after triggering when the spawner can be triggered again (in minutes).");
			this.chkGameTOD.CheckAlign = ContentAlignment.MiddleRight;
			this.chkGameTOD.ContextMenu = this.highlightDetail;
			this.chkGameTOD.Location = new Point(128, 216);
			this.chkGameTOD.Name = "chkGameTOD";
			this.chkGameTOD.Size = new System.Drawing.Size(88, 16);
			this.chkGameTOD.TabIndex = 189;
			this.chkGameTOD.Text = "GameTOD:";
			this.ttpSpawnInfo.SetToolTip(this.chkGameTOD, "Time of Day triggering uses game world time.");
			this.chkRealTOD.CheckAlign = ContentAlignment.MiddleRight;
			this.chkRealTOD.Checked = true;
			this.chkRealTOD.CheckState = CheckState.Checked;
			this.chkRealTOD.ContextMenu = this.highlightDetail;
			this.chkRealTOD.Location = new Point(128, 200);
			this.chkRealTOD.Name = "chkRealTOD";
			this.chkRealTOD.Size = new System.Drawing.Size(88, 16);
			this.chkRealTOD.TabIndex = 188;
			this.chkRealTOD.Text = "RealTOD:";
			this.ttpSpawnInfo.SetToolTip(this.chkRealTOD, "Time of Day triggering uses real world time.");
			this.chkAllowGhost.BackColor = SystemColors.Control;
			this.chkAllowGhost.CheckAlign = ContentAlignment.MiddleRight;
			this.chkAllowGhost.ContextMenu = this.highlightDetail;
			this.chkAllowGhost.Location = new Point(128, 184);
			this.chkAllowGhost.Name = "chkAllowGhost";
			this.chkAllowGhost.Size = new System.Drawing.Size(88, 16);
			this.chkAllowGhost.TabIndex = 187;
			this.chkAllowGhost.Text = "AllowGhost:";
			this.ttpSpawnInfo.SetToolTip(this.chkAllowGhost, "Allow the spawner to be triggered by players in ghost form.");
			this.chkSmartSpawning.CheckAlign = ContentAlignment.MiddleRight;
			this.chkSmartSpawning.ContextMenu = this.highlightDetail;
			this.chkSmartSpawning.Location = new Point(232, 216);
			this.chkSmartSpawning.Name = "chkSmartSpawning";
			this.chkSmartSpawning.Size = new System.Drawing.Size(120, 16);
			this.chkSmartSpawning.TabIndex = 192;
			this.chkSmartSpawning.Text = "SmartSpawning:";
			this.ttpSpawnInfo.SetToolTip(this.chkSmartSpawning, "Enable automatic spawning/despawning based upon nearby player activity.");
			this.chkSmartSpawning.CheckedChanged += new EventHandler(this.checkBox20_CheckedChanged);
			this.chkSequentialSpawn.CheckAlign = ContentAlignment.MiddleRight;
			this.chkSequentialSpawn.ContextMenu = this.highlightDetail;
			this.chkSequentialSpawn.Location = new Point(232, 200);
			this.chkSequentialSpawn.Name = "chkSequentialSpawn";
			this.chkSequentialSpawn.Size = new System.Drawing.Size(120, 16);
			this.chkSequentialSpawn.TabIndex = 191;
			this.chkSequentialSpawn.Text = "SequentialSpawn:";
			this.ttpSpawnInfo.SetToolTip(this.chkSequentialSpawn, "Enable sequential spawning that will advance according to subgroup number.");
			this.chkSpawnOnTrigger.CheckAlign = ContentAlignment.MiddleRight;
			this.chkSpawnOnTrigger.ContextMenu = this.highlightDetail;
			this.chkSpawnOnTrigger.Location = new Point(232, 184);
			this.chkSpawnOnTrigger.Name = "chkSpawnOnTrigger";
			this.chkSpawnOnTrigger.Size = new System.Drawing.Size(120, 16);
			this.chkSpawnOnTrigger.TabIndex = 190;
			this.chkSpawnOnTrigger.Text = "SpawnOnTrigger:";
			this.ttpSpawnInfo.SetToolTip(this.chkSpawnOnTrigger, "Spawn immediately after triggering regardless of min/maxdelay.");
			this.spnDespawn.ContextMenu = this.highlightDetail;
			this.spnDespawn.DecimalPlaces = 1;
			this.spnDespawn.Location = new Point(280, 40);
			NumericUpDown num9 = this.spnDespawn;
			numArray = new int[] { 65535000, 0, 0, 0 };
			num9.Maximum = new decimal(numArray);
			this.spnDespawn.Name = "spnDespawn";
			this.spnDespawn.Size = new System.Drawing.Size(72, 20);
			this.spnDespawn.TabIndex = 194;
			this.ttpSpawnInfo.SetToolTip(this.spnDespawn, "Similar to Duration but for longer timescales.");
			this.spnDespawn.ValueChanged += new EventHandler(this.numericUpDown6_ValueChanged);
			this.spnTODEnd.ContextMenu = this.highlightDetail;
			this.spnTODEnd.DecimalPlaces = 1;
			this.spnTODEnd.Location = new Point(280, 120);
			NumericUpDown numericUpDown9 = this.spnTODEnd;
			numArray = new int[] { 65535, 0, 0, 0 };
			numericUpDown9.Maximum = new decimal(numArray);
			this.spnTODEnd.Name = "spnTODEnd";
			this.spnTODEnd.Size = new System.Drawing.Size(72, 20);
			this.spnTODEnd.TabIndex = 195;
			this.ttpSpawnInfo.SetToolTip(this.spnTODEnd, "Ending hour before which spawning can occur.");
			this.spnDuration.ContextMenu = this.highlightDetail;
			this.spnDuration.DecimalPlaces = 1;
			this.spnDuration.Location = new Point(280, 20);
			NumericUpDown num10 = this.spnDuration;
			numArray = new int[] { 65535000, 0, 0, 0 };
			num10.Maximum = new decimal(numArray);
			this.spnDuration.Name = "spnDuration";
			this.spnDuration.Size = new System.Drawing.Size(72, 20);
			this.spnDuration.TabIndex = 198;
			this.ttpSpawnInfo.SetToolTip(this.spnDuration, "Maximum duration of a spawn after which it will be deleted.");
			this.spnProximitySnd.ContextMenu = this.highlightDetail;
			this.spnProximitySnd.Location = new Point(280, 160);
			NumericUpDown numericUpDown10 = this.spnProximitySnd;
			numArray = new int[] { 65635, 0, 0, 0 };
			numericUpDown10.Maximum = new decimal(numArray);
			this.spnProximitySnd.Name = "spnProximitySnd";
			this.spnProximitySnd.Size = new System.Drawing.Size(72, 20);
			this.spnProximitySnd.TabIndex = 203;
			this.ttpSpawnInfo.SetToolTip(this.spnProximitySnd, "Sound ID used when the spawner is triggered.");
			NumericUpDown num11 = this.spnProximitySnd;
			numArray = new int[] { 500, 0, 0, 0 };
			num11.Value = new decimal(numArray);
			this.spnProximitySnd.ValueChanged += new EventHandler(this.numericUpDown10_ValueChanged);
			this.spnKillReset.ContextMenu = this.highlightDetail;
			this.spnKillReset.Location = new Point(280, 140);
			NumericUpDown numericUpDown11 = this.spnKillReset;
			numArray = new int[] { 65635, 0, 0, 0 };
			numericUpDown11.Maximum = new decimal(numArray);
			this.spnKillReset.Name = "spnKillReset";
			this.spnKillReset.Size = new System.Drawing.Size(72, 20);
			this.spnKillReset.TabIndex = 205;
			this.ttpSpawnInfo.SetToolTip(this.spnKillReset, "Number of spawner ticks until the Kill count of the spawner is reset.");
			NumericUpDown num12 = this.spnKillReset;
			numArray = new int[] { 1, 0, 0, 0 };
			num12.Value = new decimal(numArray);
			this.tvwTemplates.AllowDrop = true;
			this.tvwTemplates.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.tvwTemplates.ImageIndex = -1;
			this.tvwTemplates.Location = new Point(8, 48);
			this.tvwTemplates.Name = "tvwTemplates";
			this.tvwTemplates.SelectedImageIndex = -1;
			this.tvwTemplates.Size = new System.Drawing.Size(168, 168);
			this.tvwTemplates.Sorted = true;
			this.tvwTemplates.TabIndex = 3;
			this.ttpSpawnInfo.SetToolTip(this.tvwTemplates, "List of currently defined templates.");
			this.chkTracking.Location = new Point(8, 48);
			this.chkTracking.Name = "chkTracking";
			this.chkTracking.Size = new System.Drawing.Size(56, 16);
			this.chkTracking.TabIndex = 9;
			this.chkTracking.Text = "Track";
			this.ttpSpawnInfo.SetToolTip(this.chkTracking, "Track player movement on the map.");
			this.chkTracking.CheckedChanged += new EventHandler(this.checkBox1_CheckedChanged);
			this.btnGo.Location = new Point(8, 16);
			this.btnGo.Name = "btnGo";
			this.btnGo.Size = new System.Drawing.Size(48, 24);
			this.btnGo.TabIndex = 8;
			this.btnGo.Text = "&Go";
			this.ttpSpawnInfo.SetToolTip(this.btnGo, "Move the player to the targeted location on the map.");
			this.btnGo.Click += new EventHandler(this.btnGo_Click);
			this.chkInContainer.CheckAlign = ContentAlignment.MiddleRight;
			this.chkInContainer.ContextMenu = this.highlightDetail;
			this.chkInContainer.Location = new Point(8, 232);
			this.chkInContainer.Name = "chkInContainer";
			this.chkInContainer.Size = new System.Drawing.Size(104, 16);
			this.chkInContainer.TabIndex = 207;
			this.chkInContainer.Text = "InContainer:";
			this.ttpSpawnInfo.SetToolTip(this.chkInContainer, "Check if the spawner is in a container.");
			this.chkInContainer.CheckedChanged += new EventHandler(this.chkInContainer_CheckedChanged);
			this.spnTriggerProbability.DecimalPlaces = 1;
			NumericUpDown numericUpDown12 = this.spnTriggerProbability;
			numArray = new int[] { 1, 0, 0, 65536 };
			numericUpDown12.Increment = new decimal(numArray);
			this.spnTriggerProbability.Location = new Point(120, 56);
			NumericUpDown num13 = this.spnTriggerProbability;
			numArray = new int[] { 1, 0, 0, 0 };
			num13.Maximum = new decimal(numArray);
			this.spnTriggerProbability.Name = "spnTriggerProbability";
			this.spnTriggerProbability.Size = new System.Drawing.Size(56, 20);
			this.spnTriggerProbability.TabIndex = 200;
			this.ttpSpawnInfo.SetToolTip(this.spnTriggerProbability, "Maximum duration of a spawn after which it will be deleted.");
			NumericUpDown numericUpDown13 = this.spnTriggerProbability;
			numArray = new int[] { 1, 0, 0, 0 };
			numericUpDown13.Value = new decimal(numArray);
			this.spnStackAmount.Location = new Point(120, 32);
			NumericUpDown num14 = this.spnStackAmount;
			numArray = new int[] { 65535, 0, 0, 0 };
			num14.Maximum = new decimal(numArray);
			this.spnStackAmount.Name = "spnStackAmount";
			this.spnStackAmount.Size = new System.Drawing.Size(56, 20);
			this.spnStackAmount.TabIndex = 202;
			this.ttpSpawnInfo.SetToolTip(this.spnStackAmount, "Maximum wandering range of the spawn from its spawned location.");
			NumericUpDown numericUpDown14 = this.spnStackAmount;
			numArray = new int[] { 1, 0, 0, 0 };
			numericUpDown14.Value = new decimal(numArray);
			this.chkExternalTriggering.CheckAlign = ContentAlignment.MiddleRight;
			this.chkExternalTriggering.Location = new Point(8, 80);
			this.chkExternalTriggering.Name = "chkExternalTriggering";
			this.chkExternalTriggering.Size = new System.Drawing.Size(128, 16);
			this.chkExternalTriggering.TabIndex = 222;
			this.chkExternalTriggering.Text = "ExternalTriggering:";
			this.ttpSpawnInfo.SetToolTip(this.chkExternalTriggering, "Check if the spawned object belongs to a group.");
			this.spnContainerX.Enabled = false;
			this.spnContainerX.Location = new Point(264, 32);
			NumericUpDown num15 = this.spnContainerX;
			numArray = new int[] { 65535, 0, 0, 0 };
			num15.Maximum = new decimal(numArray);
			NumericUpDown numericUpDown15 = this.spnContainerX;
			numArray = new int[] { 65535, 0, 0, -2147483648 };
			numericUpDown15.Minimum = new decimal(numArray);
			this.spnContainerX.Name = "spnContainerX";
			this.spnContainerX.Size = new System.Drawing.Size(56, 20);
			this.spnContainerX.TabIndex = 233;
			this.ttpSpawnInfo.SetToolTip(this.spnContainerX, "Maximum wandering range of the spawn from its spawned location.");
			this.spnContainerY.Enabled = false;
			this.spnContainerY.Location = new Point(264, 56);
			NumericUpDown num16 = this.spnContainerY;
			numArray = new int[] { 65535, 0, 0, 0 };
			num16.Maximum = new decimal(numArray);
			NumericUpDown numericUpDown16 = this.spnContainerY;
			numArray = new int[] { 65535, 0, 0, -2147483648 };
			numericUpDown16.Minimum = new decimal(numArray);
			this.spnContainerY.Name = "spnContainerY";
			this.spnContainerY.Size = new System.Drawing.Size(56, 20);
			this.spnContainerY.TabIndex = 234;
			this.ttpSpawnInfo.SetToolTip(this.spnContainerY, "Maximum wandering range of the spawn from its spawned location.");
			this.spnContainerZ.Enabled = false;
			this.spnContainerZ.Location = new Point(264, 80);
			NumericUpDown num17 = this.spnContainerZ;
			numArray = new int[] { 65535, 0, 0, 0 };
			num17.Maximum = new decimal(numArray);
			NumericUpDown numericUpDown17 = this.spnContainerZ;
			numArray = new int[] { 65535, 0, 0, -2147483648 };
			numericUpDown17.Minimum = new decimal(numArray);
			this.spnContainerZ.Name = "spnContainerZ";
			this.spnContainerZ.Size = new System.Drawing.Size(56, 20);
			this.spnContainerZ.TabIndex = 235;
			this.ttpSpawnInfo.SetToolTip(this.spnContainerZ, "Maximum wandering range of the spawn from its spawned location.");
			this.chkLockSpawn.Location = new Point(8, 80);
			this.chkLockSpawn.Name = "chkLockSpawn";
			this.chkLockSpawn.Size = new System.Drawing.Size(56, 16);
			this.chkLockSpawn.TabIndex = 10;
			this.chkLockSpawn.Text = "Loc&k";
			this.ttpSpawnInfo.SetToolTip(this.chkLockSpawn, "Lock spawner location during spawn region repositioning or resizing");
			this.chkDetails.CheckAlign = ContentAlignment.MiddleRight;
			this.chkDetails.Location = new Point(77, 56);
			this.chkDetails.Name = "chkDetails";
			this.chkDetails.Size = new System.Drawing.Size(80, 16);
			this.chkDetails.TabIndex = 7;
			this.chkDetails.Text = "Details";
			this.ttpSpawnInfo.SetToolTip(this.chkDetails, "Display detailed spawn information");
			this.chkDetails.CheckedChanged += new EventHandler(this.chkDetails_CheckedChanged);
			this.chkSnapRegion.Location = new Point(8, 96);
			this.chkSnapRegion.Name = "chkSnapRegion";
			this.chkSnapRegion.Size = new System.Drawing.Size(72, 16);
			this.chkSnapRegion.TabIndex = 11;
			this.chkSnapRegion.Text = "Snap XY";
			this.ttpSpawnInfo.SetToolTip(this.chkSnapRegion, "When selecting spawners, automatically move to the center of the spawning region instead of to the spawner location");
			this.treeRegionView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.treeRegionView.CheckBoxes = true;
			this.treeRegionView.ImageIndex = -1;
			this.treeRegionView.Location = new Point(8, 8);
			this.treeRegionView.Name = "treeRegionView";
			this.treeRegionView.SelectedImageIndex = -1;
			this.treeRegionView.Size = new System.Drawing.Size(156, 448);
			this.treeRegionView.TabIndex = 0;
			this.ttpSpawnInfo.SetToolTip(this.treeRegionView, "List of regions that have been defined in RunUO Data/Regions.xml.  Move to the region Go location when selected.");
			this.treeRegionView.MouseUp += new MouseEventHandler(this.treeRegionView_MouseUp);
			this.treeGoView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.treeGoView.ImageIndex = -1;
			this.treeGoView.Location = new Point(8, 8);
			this.treeGoView.Name = "treeGoView";
			this.treeGoView.SelectedImageIndex = -1;
			this.treeGoView.Size = new System.Drawing.Size(156, 448);
			this.treeGoView.TabIndex = 0;
			this.ttpSpawnInfo.SetToolTip(this.treeGoView, "List of locations taken from RunUO Data/Locations.  Move to the locations when selected.");
			this.treeGoView.MouseUp += new MouseEventHandler(this.treeGoView_MouseUp);
			this.checkSpawnFilter.Location = new Point(8, 8);
			this.checkSpawnFilter.Name = "checkSpawnFilter";
			this.checkSpawnFilter.Size = new System.Drawing.Size(88, 16);
			this.checkSpawnFilter.TabIndex = 12;
			this.checkSpawnFilter.Text = "Apply Filter";
			this.ttpSpawnInfo.SetToolTip(this.checkSpawnFilter, "Filter the display of spawners based on the Display filter settings.");
			this.checkSpawnFilter.CheckedChanged += new EventHandler(this.checkSpawnFilter_CheckedChanged);
			this.button1.Location = new Point(8, 35);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(160, 20);
			this.button1.TabIndex = 3;
			this.button1.Text = "Clear Selections";
			this.ttpSpawnInfo.SetToolTip(this.button1, "Clears current selections from the type list.");
			this.button1.Click += new EventHandler(this.btnSpawnPackClear);
			this.clbSpawnPack.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.clbSpawnPack.CheckOnClick = true;
			this.clbSpawnPack.HorizontalScrollbar = true;
			this.clbSpawnPack.IntegralHeight = false;
			this.clbSpawnPack.Location = new Point(8, 96);
			this.clbSpawnPack.Name = "clbSpawnPack";
			this.clbSpawnPack.Size = new System.Drawing.Size(160, 168);
			this.clbSpawnPack.TabIndex = 4;
			this.clbSpawnPack.ThreeDCheckBoxes = true;
			this.ttpSpawnInfo.SetToolTip(this.clbSpawnPack, "List of spawnable objects in this spawn pack.  Right-click to delete.");
			this.clbSpawnPack.MouseUp += new MouseEventHandler(this.clbSpawnPack_MouseUp);
			this.btnUpdateFromSpawnPack.Enabled = false;
			this.btnUpdateFromSpawnPack.Location = new Point(8, 55);
			this.btnUpdateFromSpawnPack.Name = "btnUpdateFromSpawnPack";
			this.btnUpdateFromSpawnPack.Size = new System.Drawing.Size(160, 20);
			this.btnUpdateFromSpawnPack.TabIndex = 15;
			this.btnUpdateFromSpawnPack.Text = "Add to Spawner";
			this.ttpSpawnInfo.SetToolTip(this.btnUpdateFromSpawnPack, "Updates the currently selected spawn with the selected types.");
			this.btnUpdateFromSpawnPack.Click += new EventHandler(this.btnUpdateFromSpawnPack_Click);
			this.btnAddToSpawnPack.Location = new Point(8, 75);
			this.btnAddToSpawnPack.Name = "btnAddToSpawnPack";
			this.btnAddToSpawnPack.Size = new System.Drawing.Size(160, 20);
			this.btnAddToSpawnPack.TabIndex = 16;
			this.btnAddToSpawnPack.Text = "Add to Spawn Pack";
			this.ttpSpawnInfo.SetToolTip(this.btnAddToSpawnPack, "Adds the selected types to the Current Spawn Pack");
			this.btnAddToSpawnPack.Click += new EventHandler(this.btnAddToSpawnPack_Click);
			this.btnUpdateSpawnPacks.Location = new Point(8, 75);
			this.btnUpdateSpawnPacks.Name = "btnUpdateSpawnPacks";
			this.btnUpdateSpawnPacks.Size = new System.Drawing.Size(160, 20);
			this.btnUpdateSpawnPacks.TabIndex = 17;
			this.btnUpdateSpawnPacks.Text = "Update Spawn Packs";
			this.ttpSpawnInfo.SetToolTip(this.btnUpdateSpawnPacks, "Updates the Current Spawn Pack into the All Spawn Packs list.");
			this.btnUpdateSpawnPacks.Click += new EventHandler(this.btnUpdateSpawnPacks_Click);
			this.tvwSpawnPacks.ImageIndex = -1;
			this.tvwSpawnPacks.Location = new Point(8, 16);
			this.tvwSpawnPacks.Name = "tvwSpawnPacks";
			this.tvwSpawnPacks.SelectedImageIndex = -1;
			this.tvwSpawnPacks.Size = new System.Drawing.Size(160, 128);
			this.tvwSpawnPacks.TabIndex = 0;
			this.ttpSpawnInfo.SetToolTip(this.tvwSpawnPacks, "List of all available Spawn Packs.  Right-click to delete.");
			this.tvwSpawnPacks.MouseUp += new MouseEventHandler(this.tvwSpawnPacks_MouseUp);
			this.tvwSpawnPacks.AfterSelect += new TreeViewEventHandler(this.tvwSpawnPacks_AfterSelect);
			this.chkShade.Location = new Point(8, 114);
			this.chkShade.Name = "chkShade";
			this.chkShade.Size = new System.Drawing.Size(80, 16);
			this.chkShade.TabIndex = 16;
			this.chkShade.Text = "Shade by";
			this.ttpSpawnInfo.SetToolTip(this.chkShade, "Display detailed spawn information");
			this.chkShade.CheckedChanged += new EventHandler(this.chkShade_CheckedChanged);
			this.cbxShade.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			this.cbxShade.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cbxShade.Items.AddRange(new object[] { "Density", "Speed" });
			this.cbxShade.Location = new Point(85, 112);
			this.cbxShade.Name = "cbxShade";
			this.cbxShade.Size = new System.Drawing.Size(77, 21);
			this.cbxShade.TabIndex = 17;
			this.ttpSpawnInfo.SetToolTip(this.cbxShade, "Changes the current map.");
			this.cbxShade.SelectedIndexChanged += new EventHandler(this.cbxShade_SelectedIndexChanged);
			this.label9.Location = new Point(592, 16);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(24, 16);
			this.label9.TabIndex = 23;
			this.label9.Text = "Clr";
			this.ttpSpawnInfo.SetToolTip(this.label9, "ClearOnAdvance flag. When checked all entries in that subgroup will be cleared on sequential spawn advancement.");
			this.label8.Location = new Point(568, 16);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(24, 16);
			this.label8.TabIndex = 22;
			this.label8.Text = "RK";
			this.ttpSpawnInfo.SetToolTip(this.label8, "RestrictKills flag.  When checked kills of that entry will only be counted if they come from the currently active sequential subgroup.");
			this.label7.Location = new Point(512, 16);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(56, 16);
			this.label7.TabIndex = 21;
			this.label7.Text = "MaxD (m)";
			this.ttpSpawnInfo.SetToolTip(this.label7, "Individual MaxDelay for the entry.  Note that spawns cannot occur faster than the main spawner min/maxdelay.");
			this.label6.Location = new Point(464, 16);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(56, 16);
			this.label6.TabIndex = 20;
			this.label6.Text = "MinD (m)";
			this.ttpSpawnInfo.SetToolTip(this.label6, "Individual MinDelay for the entry. Note that spawns cannot occur faster than the main spawner min/maxdelay.");
			this.label5.Location = new Point(400, 16);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(24, 16);
			this.label5.TabIndex = 17;
			this.label5.Text = "To";
			this.ttpSpawnInfo.SetToolTip(this.label5, "Subgroup that the sequential spawn index will be set to when the Reset time is reached without achieving the required number of Kills for the subgroup.");
			this.label4.Location = new Point(432, 16);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(48, 16);
			this.label4.TabIndex = 11;
			this.label4.Text = "Kills";
			this.ttpSpawnInfo.SetToolTip(this.label4, "Minimum number of kills required for this subgroup in order to advance the sequential spawn index.  These kills must be completed within the number of spawner ticks given by the spawner KillReset property.");
			this.label3.Location = new Point(352, 16);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(56, 16);
			this.label3.TabIndex = 9;
			this.label3.Text = "Reset (m)";
			this.ttpSpawnInfo.SetToolTip(this.label3, "Maximum amount of time allowed to reach the number of kills required for this subgroup.  ");
			this.label2.Location = new Point(320, 16);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(32, 16);
			this.label2.TabIndex = 6;
			this.label2.Text = "Sub";
			this.ttpSpawnInfo.SetToolTip(this.label2, "Subgroup assignment for the entry.");
			this.label1.Location = new Point(216, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(32, 16);
			this.label1.TabIndex = 4;
			this.label1.Text = "Max";
			this.ttpSpawnInfo.SetToolTip(this.label1, "Maximum number of spawns for the entry.");
			this.label28.Location = new Point(192, 140);
			this.label28.Name = "label28";
			this.label28.Size = new System.Drawing.Size(64, 16);
			this.label28.TabIndex = 204;
			this.label28.Text = "KillReset:";
			this.ttpSpawnInfo.SetToolTip(this.label28, "Number of spawner ticks until the Kill count of the spawner is reset.");
			this.label27.Location = new Point(192, 160);
			this.label27.Name = "label27";
			this.label27.Size = new System.Drawing.Size(104, 16);
			this.label27.TabIndex = 202;
			this.label27.Text = "ProximitySnd:";
			this.ttpSpawnInfo.SetToolTip(this.label27, "Sound ID used when the spawner is triggered.");
			this.label25.Location = new Point(192, 20);
			this.label25.Name = "label25";
			this.label25.Size = new System.Drawing.Size(80, 20);
			this.label25.TabIndex = 197;
			this.label25.Text = "Duration (m):";
			this.ttpSpawnInfo.SetToolTip(this.label25, "Maximum duration of a spawn after which it will be deleted.");
			this.label24.Location = new Point(192, 120);
			this.label24.Name = "label24";
			this.label24.Size = new System.Drawing.Size(80, 16);
			this.label24.TabIndex = 196;
			this.label24.Text = "TODEnd (h):";
			this.ttpSpawnInfo.SetToolTip(this.label24, "Ending hour before which spawning can occur.");
			this.label23.Location = new Point(192, 40);
			this.label23.Name = "label23";
			this.label23.Size = new System.Drawing.Size(80, 20);
			this.label23.TabIndex = 193;
			this.label23.Text = "Despawn (h):";
			this.ttpSpawnInfo.SetToolTip(this.label23, "Similar to Duration but for longer timescales.");
			this.label18.Location = new Point(192, 80);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(88, 16);
			this.label18.TabIndex = 183;
			this.label18.Text = "MaxRefract (m):";
			this.ttpSpawnInfo.SetToolTip(this.label18, "Maximum delay after triggering when the spawner can be triggered again (in minutes).");
			this.label19.Location = new Point(8, 160);
			this.label19.Name = "label19";
			this.label19.Size = new System.Drawing.Size(88, 16);
			this.label19.TabIndex = 177;
			this.label19.Text = "ProximityRange:";
			this.ttpSpawnInfo.SetToolTip(this.label19, "Maximum range within which a player can trigger the spawner.  A value of -1 means that proximity triggering is disabled.");
			this.label20.Location = new Point(192, 100);
			this.label20.Name = "label20";
			this.label20.Size = new System.Drawing.Size(80, 16);
			this.label20.TabIndex = 185;
			this.label20.Text = "TODStart (h):";
			this.ttpSpawnInfo.SetToolTip(this.label20, "Starting hour after which spawning can occur.");
			this.label21.Location = new Point(8, 140);
			this.label21.Name = "label21";
			this.label21.Size = new System.Drawing.Size(80, 16);
			this.label21.TabIndex = 179;
			this.label21.Text = "SpawnRange:";
			this.ttpSpawnInfo.SetToolTip(this.label21, "Maximum spawning range.  A value of -1 means the range is specified by XY.");
			this.label22.Location = new Point(192, 60);
			this.label22.Name = "label22";
			this.label22.Size = new System.Drawing.Size(88, 16);
			this.label22.TabIndex = 181;
			this.label22.Text = "MinRefract (m):";
			this.ttpSpawnInfo.SetToolTip(this.label22, "Minimum delay after triggering when the spawner can be triggered again (in minutes).");
			this.lblMaxDelay.Location = new Point(8, 100);
			this.lblMaxDelay.Name = "lblMaxDelay";
			this.lblMaxDelay.Size = new System.Drawing.Size(80, 16);
			this.lblMaxDelay.TabIndex = 7;
			this.lblMaxDelay.Text = "MaxDelay (m)";
			this.ttpSpawnInfo.SetToolTip(this.lblMaxDelay, "Maximum delay to respawn (in minutes).");
			this.lblHomeRange.Location = new Point(8, 40);
			this.lblHomeRange.Name = "lblHomeRange";
			this.lblHomeRange.Size = new System.Drawing.Size(72, 20);
			this.lblHomeRange.TabIndex = 1;
			this.lblHomeRange.Text = "HomeRange:";
			this.ttpSpawnInfo.SetToolTip(this.lblHomeRange, "Maximum wandering range of the spawn from its spawned location.");
			this.lblTeam.Location = new Point(8, 120);
			this.lblTeam.Name = "lblTeam";
			this.lblTeam.Size = new System.Drawing.Size(80, 16);
			this.lblTeam.TabIndex = 9;
			this.lblTeam.Text = "Team:";
			this.ttpSpawnInfo.SetToolTip(this.lblTeam, "Team that spawned object will belong to.");
			this.lblMaxCount.Location = new Point(8, 60);
			this.lblMaxCount.Name = "lblMaxCount";
			this.lblMaxCount.Size = new System.Drawing.Size(64, 20);
			this.lblMaxCount.TabIndex = 3;
			this.lblMaxCount.Text = "MaxCount:";
			this.ttpSpawnInfo.SetToolTip(this.lblMaxCount, "Absolute maximum number of objects to be spawned by this spawner.");
			this.lblMinDelay.Location = new Point(8, 80);
			this.lblMinDelay.Name = "lblMinDelay";
			this.lblMinDelay.Size = new System.Drawing.Size(72, 16);
			this.lblMinDelay.TabIndex = 5;
			this.lblMinDelay.Text = "MinDelay (m)";
			this.ttpSpawnInfo.SetToolTip(this.lblMinDelay, "Minimum delay to respawn (in minutes).");
			this.lblMinDelay.Click += new EventHandler(this.lblMinDelay_Click);
			this.btnSendSpawn.ContextMenu = this.unloadSpawners;
			this.btnSendSpawn.Location = new Point(8, 56);
			this.btnSendSpawn.Name = "btnSendSpawn";
			this.btnSendSpawn.Size = new System.Drawing.Size(152, 23);
			this.btnSendSpawn.TabIndex = 206;
			this.btnSendSpawn.Text = "Send to Server";
			this.ttpSpawnInfo.SetToolTip(this.btnSendSpawn, "Send all spawners on the list to the Transfer Server.  Right-click to unload them from the server.");
			this.btnSendSpawn.Click += new EventHandler(this.btnSendSpawn_Click);
			System.Windows.Forms.Menu.MenuItemCollection menuItems1 = this.unloadSpawners.MenuItems;
			menuItemArray = new MenuItem[] { this.mniUnloadSpawners, this.menuItem19 };
			menuItems1.AddRange(menuItemArray);
			this.unloadSpawners.Popup += new EventHandler(this.unloadSpawner_Popup);
			this.mniUnloadSpawners.Index = 0;
			this.mniUnloadSpawners.Text = "Unload Spawners from Server";
			this.mniUnloadSpawners.Click += new EventHandler(this.mniUnloadSpawners_Click);
			this.menuItem19.Index = 1;
			this.menuItem19.Text = "Cancel";
			this.label30.Location = new Point(272, 16);
			this.label30.Name = "label30";
			this.label30.Size = new System.Drawing.Size(40, 16);
			this.label30.TabIndex = 137;
			this.label30.Text = "Per";
			this.ttpSpawnInfo.SetToolTip(this.label30, "Number of spawns of this type created when the entry is spawned.");
			this.btnFilterSettings.Location = new Point(88, 8);
			this.btnFilterSettings.Name = "btnFilterSettings";
			this.btnFilterSettings.Size = new System.Drawing.Size(72, 24);
			this.btnFilterSettings.TabIndex = 207;
			this.btnFilterSettings.Text = "Settings";
			this.ttpSpawnInfo.SetToolTip(this.btnFilterSettings, "Open the Display filter settings window.");
			this.btnFilterSettings.Click += new EventHandler(this.btnFilterSettings_Click);
			this.pnlControls.Controls.Add(this.lblTrkMax);
			this.pnlControls.Controls.Add(this.lblTrkMin);
			this.pnlControls.Controls.Add(this.trkZoom);
			this.pnlControls.Controls.Add(this.tabControl3);
			this.pnlControls.Controls.Add(this.tabControl2);
			this.pnlControls.Controls.Add(this.progressBar1);
			this.pnlControls.Controls.Add(this.lblTransferStatus);
			this.pnlControls.Dock = DockStyle.Left;
			this.pnlControls.Location = new Point(0, 0);
			this.pnlControls.Name = "pnlControls";
			this.pnlControls.Size = new System.Drawing.Size(176, 682);
			this.pnlControls.TabIndex = 0;
			this.lblTrkMax.Location = new Point(136, 168);
			this.lblTrkMax.Name = "lblTrkMax";
			this.lblTrkMax.Size = new System.Drawing.Size(29, 16);
			this.lblTrkMax.TabIndex = 15;
			this.lblTrkMax.Text = "max";
			this.lblTrkMin.Location = new Point(8, 168);
			this.lblTrkMin.Name = "lblTrkMin";
			this.lblTrkMin.Size = new System.Drawing.Size(29, 16);
			this.lblTrkMin.TabIndex = 14;
			this.lblTrkMin.Text = "min";
			this.tabControl3.Controls.Add(this.tabMapSettings);
			this.tabControl3.Location = new Point(0, 0);
			this.tabControl3.Name = "tabControl3";
			this.tabControl3.SelectedIndex = 0;
			this.tabControl3.Size = new System.Drawing.Size(173, 168);
			this.tabControl3.TabIndex = 7;
			this.tabMapSettings.Controls.Add(this.grpMapControl);
			this.tabMapSettings.Location = new Point(4, 22);
			this.tabMapSettings.Name = "tabMapSettings";
			this.tabMapSettings.Size = new System.Drawing.Size(165, 142);
			this.tabMapSettings.TabIndex = 0;
			this.tabMapSettings.Text = "Map Settings";
			this.grpMapControl.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.grpMapControl.Controls.Add(this.cbxMap);
			this.grpMapControl.Controls.Add(this.cbxShade);
			this.grpMapControl.Controls.Add(this.chkShade);
			this.grpMapControl.Controls.Add(this.chkSnapRegion);
			this.grpMapControl.Controls.Add(this.chkLockSpawn);
			this.grpMapControl.Controls.Add(this.chkTracking);
			this.grpMapControl.Controls.Add(this.btnGo);
			this.grpMapControl.Controls.Add(this.chkDetails);
			this.grpMapControl.Controls.Add(this.chkShowSpawns);
			this.grpMapControl.Controls.Add(this.chkShowMapTip);
			this.grpMapControl.Controls.Add(this.chkDrawStatics);
			this.grpMapControl.Controls.Add(this.chkSyncUO);
			this.grpMapControl.Location = new Point(0, 0);
			this.grpMapControl.Name = "grpMapControl";
			this.grpMapControl.Size = new System.Drawing.Size(189, 176);
			this.grpMapControl.TabIndex = 0;
			this.grpMapControl.TabStop = false;
			this.tabControl2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.tabControl2.Controls.Add(this.tabPage3);
			this.tabControl2.Controls.Add(this.tabPage4);
			this.tabControl2.Controls.Add(this.tabPage5);
			this.tabControl2.Location = new Point(0, 200);
			this.tabControl2.Name = "tabControl2";
			this.tabControl2.SelectedIndex = 0;
			this.tabControl2.Size = new System.Drawing.Size(176, 488);
			this.tabControl2.TabIndex = 6;
			this.tabPage3.Controls.Add(this.grpSpawnList);
			this.tabPage3.Location = new Point(4, 22);
			this.tabPage3.Name = "tabPage3";
			this.tabPage3.Size = new System.Drawing.Size(168, 462);
			this.tabPage3.TabIndex = 0;
			this.tabPage3.Text = "Spawners";
			this.grpSpawnList.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.grpSpawnList.Controls.Add(this.btnFilterSettings);
			this.grpSpawnList.Controls.Add(this.tvwSpawnPoints);
			this.grpSpawnList.Controls.Add(this.btnLoadSpawn);
			this.grpSpawnList.Controls.Add(this.btnMergeSpawn);
			this.grpSpawnList.Controls.Add(this.btnSaveSpawn);
			this.grpSpawnList.Controls.Add(this.lblTotalSpawn);
			this.grpSpawnList.Controls.Add(this.checkSpawnFilter);
			this.grpSpawnList.Controls.Add(this.btnSendSpawn);
			this.grpSpawnList.Location = new Point(0, 0);
			this.grpSpawnList.Name = "grpSpawnList";
			this.grpSpawnList.Size = new System.Drawing.Size(168, 464);
			this.grpSpawnList.TabIndex = 1;
			this.grpSpawnList.TabStop = false;
			this.lblTotalSpawn.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.lblTotalSpawn.BorderStyle = BorderStyle.Fixed3D;
			this.lblTotalSpawn.Location = new Point(8, 440);
			this.lblTotalSpawn.Name = "lblTotalSpawn";
			this.lblTotalSpawn.Size = new System.Drawing.Size(152, 16);
			this.lblTotalSpawn.TabIndex = 4;
			this.tabPage4.Controls.Add(this.treeRegionView);
			this.tabPage4.Location = new Point(4, 22);
			this.tabPage4.Name = "tabPage4";
			this.tabPage4.Size = new System.Drawing.Size(168, 462);
			this.tabPage4.TabIndex = 1;
			this.tabPage4.Text = "Regions";
			this.tabPage4.ToolTipText = "Currently defined region locations.  Select one to automatically move to its Go location.";
			this.tabPage5.Controls.Add(this.treeGoView);
			this.tabPage5.Location = new Point(4, 22);
			this.tabPage5.Name = "tabPage5";
			this.tabPage5.Size = new System.Drawing.Size(168, 462);
			this.tabPage5.TabIndex = 2;
			this.tabPage5.Text = "Go";
			this.progressBar1.Location = new Point(8, 184);
			this.progressBar1.Name = "progressBar1";
			this.progressBar1.Size = new System.Drawing.Size(152, 16);
			this.progressBar1.TabIndex = 16;
			this.progressBar1.Visible = false;
			this.lblTransferStatus.Location = new Point(8, 168);
			this.lblTransferStatus.Name = "lblTransferStatus";
			this.lblTransferStatus.Size = new System.Drawing.Size(152, 16);
			this.lblTransferStatus.TabIndex = 238;
			this.lblTransferStatus.Text = "Status";
			this.lblTransferStatus.Visible = false;
			this.groupTemplateList.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.groupTemplateList.Controls.Add(this.btnSaveTemplate);
			this.groupTemplateList.Controls.Add(this.btnMergeTemplate);
			this.groupTemplateList.Controls.Add(this.btnLoadTemplate);
			this.groupTemplateList.Controls.Add(this.tvwTemplates);
			this.groupTemplateList.Controls.Add(this.label29);
			this.groupTemplateList.Enabled = false;
			this.groupTemplateList.Location = new Point(8, 0);
			this.groupTemplateList.Name = "groupTemplateList";
			this.groupTemplateList.Size = new System.Drawing.Size(184, 248);
			this.groupTemplateList.TabIndex = 5;
			this.groupTemplateList.TabStop = false;
			this.groupTemplateList.Text = "Spawn Templates";
			this.btnSaveTemplate.Location = new Point(120, 16);
			this.btnSaveTemplate.Name = "btnSaveTemplate";
			this.btnSaveTemplate.Size = new System.Drawing.Size(56, 24);
			this.btnSaveTemplate.TabIndex = 7;
			this.btnSaveTemplate.Text = "Save";
			this.btnMergeTemplate.Location = new Point(64, 16);
			this.btnMergeTemplate.Name = "btnMergeTemplate";
			this.btnMergeTemplate.Size = new System.Drawing.Size(56, 24);
			this.btnMergeTemplate.TabIndex = 6;
			this.btnMergeTemplate.Text = "Merge";
			this.btnLoadTemplate.Location = new Point(8, 16);
			this.btnLoadTemplate.Name = "btnLoadTemplate";
			this.btnLoadTemplate.Size = new System.Drawing.Size(56, 24);
			this.btnLoadTemplate.TabIndex = 5;
			this.btnLoadTemplate.Text = "Load";
			this.label29.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.label29.BorderStyle = BorderStyle.Fixed3D;
			this.label29.Location = new Point(8, 216);
			this.label29.Name = "label29";
			this.label29.Size = new System.Drawing.Size(168, 16);
			this.label29.TabIndex = 4;
			this.grpSpawnTypes.Controls.Add(this.btnAddToSpawnPack);
			this.grpSpawnTypes.Controls.Add(this.radShowMobilesOnly);
			this.grpSpawnTypes.Controls.Add(this.radShowItemsOnly);
			this.grpSpawnTypes.Controls.Add(this.radShowAll);
			this.grpSpawnTypes.Controls.Add(this.btnResetTypes);
			this.grpSpawnTypes.Controls.Add(this.clbRunUOTypes);
			this.grpSpawnTypes.Controls.Add(this.lblTotalTypesLoaded);
			this.grpSpawnTypes.Controls.Add(this.btnUpdateSpawn);
			this.grpSpawnTypes.Location = new Point(0, 0);
			this.grpSpawnTypes.Name = "grpSpawnTypes";
			this.grpSpawnTypes.Size = new System.Drawing.Size(176, 440);
			this.grpSpawnTypes.TabIndex = 1;
			this.grpSpawnTypes.TabStop = false;
			this.grpSpawnTypes.Text = "All Spawn Types";
			this.lblTotalTypesLoaded.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.lblTotalTypesLoaded.BorderStyle = BorderStyle.Fixed3D;
			this.lblTotalTypesLoaded.Location = new Point(8, 416);
			this.lblTotalTypesLoaded.Name = "lblTotalTypesLoaded";
			this.lblTotalTypesLoaded.Size = new System.Drawing.Size(160, 16);
			this.lblTotalTypesLoaded.TabIndex = 5;
			System.Windows.Forms.Menu.MenuItemCollection menuItemCollections1 = this.mncSpawns.MenuItems;
			menuItemArray = new MenuItem[] { this.menuItem3, this.mniDeleteSpawn, this.mniDeleteAllSpawns };
			menuItemCollections1.AddRange(menuItemArray);
			this.mncSpawns.Popup += new EventHandler(this.mncSpawns_Popup);
			this.menuItem3.Index = 0;
			this.menuItem3.Text = "-";
			this.mniDeleteSpawn.Index = 1;
			this.mniDeleteSpawn.Text = "&Delete";
			this.mniDeleteSpawn.Click += new EventHandler(this.mniDeleteSpawn_Click);
			this.mniDeleteAllSpawns.Index = 2;
			this.mniDeleteAllSpawns.Text = "Delete &All";
			this.mniDeleteAllSpawns.Click += new EventHandler(this.mniDeleteAllSpawns_Click);
			this.ofdLoadFile.DefaultExt = "xml";
			this.ofdLoadFile.Filter = "Spawn Files (*.xml)|*.xml|All Files (*.*)|*.*";
			this.ofdLoadFile.Title = "Load Spawn File";
			this.sfdSaveFile.DefaultExt = "xml";
			this.sfdSaveFile.FileName = "Spawns";
			this.sfdSaveFile.Filter = "Spawn Files (*.xml)|*.xml|All Files (*.*)|*.*";
			this.sfdSaveFile.Title = "Save Spawn File";
			this.stbMain.Location = new Point(0, 682);
			this.stbMain.Name = "stbMain";
			this.stbMain.Size = new System.Drawing.Size(1016, 16);
			this.stbMain.TabIndex = 3;
			this.stbMain.Text = "Spawn Editor";
			this.grpSpawnEntries.Controls.Add(this.entryPer8);
			this.grpSpawnEntries.Controls.Add(this.entryPer7);
			this.grpSpawnEntries.Controls.Add(this.entryPer6);
			this.grpSpawnEntries.Controls.Add(this.entryPer5);
			this.grpSpawnEntries.Controls.Add(this.entryPer4);
			this.grpSpawnEntries.Controls.Add(this.entryPer3);
			this.grpSpawnEntries.Controls.Add(this.entryPer2);
			this.grpSpawnEntries.Controls.Add(this.entryPer1);
			this.grpSpawnEntries.Controls.Add(this.label30);
			this.grpSpawnEntries.Controls.Add(this.entryMaxD8);
			this.grpSpawnEntries.Controls.Add(this.entryMaxD7);
			this.grpSpawnEntries.Controls.Add(this.entryMaxD6);
			this.grpSpawnEntries.Controls.Add(this.entryMaxD5);
			this.grpSpawnEntries.Controls.Add(this.entryMaxD4);
			this.grpSpawnEntries.Controls.Add(this.entryMaxD3);
			this.grpSpawnEntries.Controls.Add(this.entryMaxD2);
			this.grpSpawnEntries.Controls.Add(this.entryMaxD1);
			this.grpSpawnEntries.Controls.Add(this.entryMinD8);
			this.grpSpawnEntries.Controls.Add(this.entryMinD7);
			this.grpSpawnEntries.Controls.Add(this.entryMinD6);
			this.grpSpawnEntries.Controls.Add(this.entryMinD5);
			this.grpSpawnEntries.Controls.Add(this.entryMinD4);
			this.grpSpawnEntries.Controls.Add(this.entryMinD3);
			this.grpSpawnEntries.Controls.Add(this.entryMinD2);
			this.grpSpawnEntries.Controls.Add(this.entryMinD1);
			this.grpSpawnEntries.Controls.Add(this.entryKills8);
			this.grpSpawnEntries.Controls.Add(this.entryKills7);
			this.grpSpawnEntries.Controls.Add(this.entryKills6);
			this.grpSpawnEntries.Controls.Add(this.entryKills5);
			this.grpSpawnEntries.Controls.Add(this.entryKills4);
			this.grpSpawnEntries.Controls.Add(this.entryKills3);
			this.grpSpawnEntries.Controls.Add(this.entryKills2);
			this.grpSpawnEntries.Controls.Add(this.entryKills1);
			this.grpSpawnEntries.Controls.Add(this.entryReset8);
			this.grpSpawnEntries.Controls.Add(this.entryReset7);
			this.grpSpawnEntries.Controls.Add(this.entryReset6);
			this.grpSpawnEntries.Controls.Add(this.entryReset5);
			this.grpSpawnEntries.Controls.Add(this.entryReset4);
			this.grpSpawnEntries.Controls.Add(this.entryReset3);
			this.grpSpawnEntries.Controls.Add(this.entryReset2);
			this.grpSpawnEntries.Controls.Add(this.entryReset1);
			this.grpSpawnEntries.Controls.Add(this.entryTo8);
			this.grpSpawnEntries.Controls.Add(this.entrySub8);
			this.grpSpawnEntries.Controls.Add(this.chkRK8);
			this.grpSpawnEntries.Controls.Add(this.entryMax8);
			this.grpSpawnEntries.Controls.Add(this.btnEntryEdit8);
			this.grpSpawnEntries.Controls.Add(this.entryText8);
			this.grpSpawnEntries.Controls.Add(this.chkClr8);
			this.grpSpawnEntries.Controls.Add(this.entryTo7);
			this.grpSpawnEntries.Controls.Add(this.entrySub7);
			this.grpSpawnEntries.Controls.Add(this.chkRK7);
			this.grpSpawnEntries.Controls.Add(this.entryMax7);
			this.grpSpawnEntries.Controls.Add(this.btnEntryEdit7);
			this.grpSpawnEntries.Controls.Add(this.entryText7);
			this.grpSpawnEntries.Controls.Add(this.chkClr7);
			this.grpSpawnEntries.Controls.Add(this.entryTo6);
			this.grpSpawnEntries.Controls.Add(this.entrySub6);
			this.grpSpawnEntries.Controls.Add(this.chkRK6);
			this.grpSpawnEntries.Controls.Add(this.entryMax6);
			this.grpSpawnEntries.Controls.Add(this.btnEntryEdit6);
			this.grpSpawnEntries.Controls.Add(this.entryText6);
			this.grpSpawnEntries.Controls.Add(this.chkClr6);
			this.grpSpawnEntries.Controls.Add(this.entryTo5);
			this.grpSpawnEntries.Controls.Add(this.entrySub5);
			this.grpSpawnEntries.Controls.Add(this.chkRK5);
			this.grpSpawnEntries.Controls.Add(this.entryMax5);
			this.grpSpawnEntries.Controls.Add(this.btnEntryEdit5);
			this.grpSpawnEntries.Controls.Add(this.entryText5);
			this.grpSpawnEntries.Controls.Add(this.chkClr5);
			this.grpSpawnEntries.Controls.Add(this.entryTo4);
			this.grpSpawnEntries.Controls.Add(this.entrySub4);
			this.grpSpawnEntries.Controls.Add(this.chkRK4);
			this.grpSpawnEntries.Controls.Add(this.entryMax4);
			this.grpSpawnEntries.Controls.Add(this.btnEntryEdit4);
			this.grpSpawnEntries.Controls.Add(this.entryText4);
			this.grpSpawnEntries.Controls.Add(this.chkClr4);
			this.grpSpawnEntries.Controls.Add(this.entryTo3);
			this.grpSpawnEntries.Controls.Add(this.entrySub3);
			this.grpSpawnEntries.Controls.Add(this.chkRK3);
			this.grpSpawnEntries.Controls.Add(this.entryMax3);
			this.grpSpawnEntries.Controls.Add(this.btnEntryEdit3);
			this.grpSpawnEntries.Controls.Add(this.entryText3);
			this.grpSpawnEntries.Controls.Add(this.chkClr3);
			this.grpSpawnEntries.Controls.Add(this.entryTo2);
			this.grpSpawnEntries.Controls.Add(this.entrySub2);
			this.grpSpawnEntries.Controls.Add(this.chkRK2);
			this.grpSpawnEntries.Controls.Add(this.entryMax2);
			this.grpSpawnEntries.Controls.Add(this.btnEntryEdit2);
			this.grpSpawnEntries.Controls.Add(this.entryText2);
			this.grpSpawnEntries.Controls.Add(this.chkClr2);
			this.grpSpawnEntries.Controls.Add(this.label9);
			this.grpSpawnEntries.Controls.Add(this.label8);
			this.grpSpawnEntries.Controls.Add(this.label7);
			this.grpSpawnEntries.Controls.Add(this.label6);
			this.grpSpawnEntries.Controls.Add(this.label5);
			this.grpSpawnEntries.Controls.Add(this.entryTo1);
			this.grpSpawnEntries.Controls.Add(this.vScrollBar1);
			this.grpSpawnEntries.Controls.Add(this.entrySub1);
			this.grpSpawnEntries.Controls.Add(this.label4);
			this.grpSpawnEntries.Controls.Add(this.label3);
			this.grpSpawnEntries.Controls.Add(this.chkRK1);
			this.grpSpawnEntries.Controls.Add(this.label2);
			this.grpSpawnEntries.Controls.Add(this.label1);
			this.grpSpawnEntries.Controls.Add(this.entryMax1);
			this.grpSpawnEntries.Controls.Add(this.btnEntryEdit1);
			this.grpSpawnEntries.Controls.Add(this.entryText1);
			this.grpSpawnEntries.Controls.Add(this.chkClr1);
			this.grpSpawnEntries.Location = new Point(200, 0);
			this.grpSpawnEntries.Name = "grpSpawnEntries";
			this.grpSpawnEntries.Size = new System.Drawing.Size(644, 224);
			this.grpSpawnEntries.TabIndex = 3;
			this.grpSpawnEntries.TabStop = false;
			this.grpSpawnEntries.Text = "Spawn Entries";
			this.grpSpawnEntries.Enter += new EventHandler(this.grpSpawnEntries_Enter);
			this.grpSpawnEntries.Leave += new EventHandler(this.grpSpawnEntries_Leave);
			this.entryPer8.Location = new Point(272, 200);
			NumericUpDown num18 = this.entryPer8;
			numArray = new int[] { 65535, 0, 0, 0 };
			num18.Maximum = new decimal(numArray);
			this.entryPer8.Name = "entryPer8";
			this.entryPer8.Size = new System.Drawing.Size(48, 20);
			this.entryPer8.TabIndex = 145;
			this.entryPer7.Location = new Point(272, 176);
			NumericUpDown numericUpDown18 = this.entryPer7;
			numArray = new int[] { 65535, 0, 0, 0 };
			numericUpDown18.Maximum = new decimal(numArray);
			this.entryPer7.Name = "entryPer7";
			this.entryPer7.Size = new System.Drawing.Size(48, 20);
			this.entryPer7.TabIndex = 144;
			this.entryPer6.Location = new Point(272, 152);
			NumericUpDown num19 = this.entryPer6;
			numArray = new int[] { 65535, 0, 0, 0 };
			num19.Maximum = new decimal(numArray);
			this.entryPer6.Name = "entryPer6";
			this.entryPer6.Size = new System.Drawing.Size(48, 20);
			this.entryPer6.TabIndex = 143;
			this.entryPer5.Location = new Point(272, 128);
			NumericUpDown numericUpDown19 = this.entryPer5;
			numArray = new int[] { 65535, 0, 0, 0 };
			numericUpDown19.Maximum = new decimal(numArray);
			this.entryPer5.Name = "entryPer5";
			this.entryPer5.Size = new System.Drawing.Size(48, 20);
			this.entryPer5.TabIndex = 142;
			this.entryPer4.Location = new Point(272, 104);
			NumericUpDown num20 = this.entryPer4;
			numArray = new int[] { 65535, 0, 0, 0 };
			num20.Maximum = new decimal(numArray);
			this.entryPer4.Name = "entryPer4";
			this.entryPer4.Size = new System.Drawing.Size(48, 20);
			this.entryPer4.TabIndex = 141;
			this.entryPer3.Location = new Point(272, 80);
			NumericUpDown numericUpDown20 = this.entryPer3;
			numArray = new int[] { 65535, 0, 0, 0 };
			numericUpDown20.Maximum = new decimal(numArray);
			this.entryPer3.Name = "entryPer3";
			this.entryPer3.Size = new System.Drawing.Size(48, 20);
			this.entryPer3.TabIndex = 140;
			this.entryPer2.Location = new Point(272, 56);
			NumericUpDown num21 = this.entryPer2;
			numArray = new int[] { 65535, 0, 0, 0 };
			num21.Maximum = new decimal(numArray);
			this.entryPer2.Name = "entryPer2";
			this.entryPer2.Size = new System.Drawing.Size(48, 20);
			this.entryPer2.TabIndex = 139;
			this.entryPer1.Location = new Point(272, 32);
			NumericUpDown numericUpDown21 = this.entryPer1;
			numArray = new int[] { 65535, 0, 0, 0 };
			numericUpDown21.Maximum = new decimal(numArray);
			this.entryPer1.Name = "entryPer1";
			this.entryPer1.Size = new System.Drawing.Size(48, 20);
			this.entryPer1.TabIndex = 138;
			this.entryMaxD8.Location = new Point(512, 200);
			this.entryMaxD8.Name = "entryMaxD8";
			this.entryMaxD8.Size = new System.Drawing.Size(48, 20);
			this.entryMaxD8.TabIndex = 136;
			this.entryMaxD8.Text = "";
			this.entryMaxD7.Location = new Point(512, 176);
			this.entryMaxD7.Name = "entryMaxD7";
			this.entryMaxD7.Size = new System.Drawing.Size(48, 20);
			this.entryMaxD7.TabIndex = 135;
			this.entryMaxD7.Text = "";
			this.entryMaxD6.Location = new Point(512, 152);
			this.entryMaxD6.Name = "entryMaxD6";
			this.entryMaxD6.Size = new System.Drawing.Size(48, 20);
			this.entryMaxD6.TabIndex = 134;
			this.entryMaxD6.Text = "";
			this.entryMaxD5.Location = new Point(512, 128);
			this.entryMaxD5.Name = "entryMaxD5";
			this.entryMaxD5.Size = new System.Drawing.Size(48, 20);
			this.entryMaxD5.TabIndex = 133;
			this.entryMaxD5.Text = "";
			this.entryMaxD4.Location = new Point(512, 104);
			this.entryMaxD4.Name = "entryMaxD4";
			this.entryMaxD4.Size = new System.Drawing.Size(48, 20);
			this.entryMaxD4.TabIndex = 132;
			this.entryMaxD4.Text = "";
			this.entryMaxD3.Location = new Point(512, 80);
			this.entryMaxD3.Name = "entryMaxD3";
			this.entryMaxD3.Size = new System.Drawing.Size(48, 20);
			this.entryMaxD3.TabIndex = 131;
			this.entryMaxD3.Text = "";
			this.entryMaxD2.Location = new Point(512, 56);
			this.entryMaxD2.Name = "entryMaxD2";
			this.entryMaxD2.Size = new System.Drawing.Size(48, 20);
			this.entryMaxD2.TabIndex = 130;
			this.entryMaxD2.Text = "";
			this.entryMaxD1.Location = new Point(512, 32);
			this.entryMaxD1.Name = "entryMaxD1";
			this.entryMaxD1.Size = new System.Drawing.Size(48, 20);
			this.entryMaxD1.TabIndex = 129;
			this.entryMaxD1.Text = "";
			this.entryMinD8.Location = new Point(464, 200);
			this.entryMinD8.Name = "entryMinD8";
			this.entryMinD8.Size = new System.Drawing.Size(48, 20);
			this.entryMinD8.TabIndex = 128;
			this.entryMinD8.Text = "";
			this.entryMinD7.Location = new Point(464, 176);
			this.entryMinD7.Name = "entryMinD7";
			this.entryMinD7.Size = new System.Drawing.Size(48, 20);
			this.entryMinD7.TabIndex = 127;
			this.entryMinD7.Text = "";
			this.entryMinD6.Location = new Point(464, 152);
			this.entryMinD6.Name = "entryMinD6";
			this.entryMinD6.Size = new System.Drawing.Size(48, 20);
			this.entryMinD6.TabIndex = 126;
			this.entryMinD6.Text = "";
			this.entryMinD5.Location = new Point(464, 128);
			this.entryMinD5.Name = "entryMinD5";
			this.entryMinD5.Size = new System.Drawing.Size(48, 20);
			this.entryMinD5.TabIndex = 125;
			this.entryMinD5.Text = "";
			this.entryMinD4.Location = new Point(464, 104);
			this.entryMinD4.Name = "entryMinD4";
			this.entryMinD4.Size = new System.Drawing.Size(48, 20);
			this.entryMinD4.TabIndex = 124;
			this.entryMinD4.Text = "";
			this.entryMinD3.Location = new Point(464, 80);
			this.entryMinD3.Name = "entryMinD3";
			this.entryMinD3.Size = new System.Drawing.Size(48, 20);
			this.entryMinD3.TabIndex = 123;
			this.entryMinD3.Text = "";
			this.entryMinD2.Location = new Point(464, 56);
			this.entryMinD2.Name = "entryMinD2";
			this.entryMinD2.Size = new System.Drawing.Size(48, 20);
			this.entryMinD2.TabIndex = 122;
			this.entryMinD2.Text = "";
			this.entryMinD1.Location = new Point(464, 32);
			this.entryMinD1.Name = "entryMinD1";
			this.entryMinD1.Size = new System.Drawing.Size(48, 20);
			this.entryMinD1.TabIndex = 121;
			this.entryMinD1.Text = "";
			this.entryKills8.Location = new Point(432, 200);
			this.entryKills8.Name = "entryKills8";
			this.entryKills8.Size = new System.Drawing.Size(32, 20);
			this.entryKills8.TabIndex = 120;
			this.entryKills8.Text = "";
			this.entryKills7.Location = new Point(432, 176);
			this.entryKills7.Name = "entryKills7";
			this.entryKills7.Size = new System.Drawing.Size(32, 20);
			this.entryKills7.TabIndex = 119;
			this.entryKills7.Text = "";
			this.entryKills6.Location = new Point(432, 152);
			this.entryKills6.Name = "entryKills6";
			this.entryKills6.Size = new System.Drawing.Size(32, 20);
			this.entryKills6.TabIndex = 118;
			this.entryKills6.Text = "";
			this.entryKills5.Location = new Point(432, 128);
			this.entryKills5.Name = "entryKills5";
			this.entryKills5.Size = new System.Drawing.Size(32, 20);
			this.entryKills5.TabIndex = 117;
			this.entryKills5.Text = "";
			this.entryKills4.Location = new Point(432, 104);
			this.entryKills4.Name = "entryKills4";
			this.entryKills4.Size = new System.Drawing.Size(32, 20);
			this.entryKills4.TabIndex = 116;
			this.entryKills4.Text = "";
			this.entryKills3.Location = new Point(432, 80);
			this.entryKills3.Name = "entryKills3";
			this.entryKills3.Size = new System.Drawing.Size(32, 20);
			this.entryKills3.TabIndex = 115;
			this.entryKills3.Text = "";
			this.entryKills2.Location = new Point(432, 56);
			this.entryKills2.Name = "entryKills2";
			this.entryKills2.Size = new System.Drawing.Size(32, 20);
			this.entryKills2.TabIndex = 114;
			this.entryKills2.Text = "";
			this.entryKills1.Location = new Point(432, 32);
			this.entryKills1.Name = "entryKills1";
			this.entryKills1.Size = new System.Drawing.Size(32, 20);
			this.entryKills1.TabIndex = 113;
			this.entryKills1.Text = "";
			this.entryReset8.Location = new Point(352, 200);
			this.entryReset8.Name = "entryReset8";
			this.entryReset8.Size = new System.Drawing.Size(48, 20);
			this.entryReset8.TabIndex = 112;
			this.entryReset8.Text = "";
			this.entryReset7.Location = new Point(352, 176);
			this.entryReset7.Name = "entryReset7";
			this.entryReset7.Size = new System.Drawing.Size(48, 20);
			this.entryReset7.TabIndex = 111;
			this.entryReset7.Text = "";
			this.entryReset6.Location = new Point(352, 152);
			this.entryReset6.Name = "entryReset6";
			this.entryReset6.Size = new System.Drawing.Size(48, 20);
			this.entryReset6.TabIndex = 110;
			this.entryReset6.Text = "";
			this.entryReset5.Location = new Point(352, 128);
			this.entryReset5.Name = "entryReset5";
			this.entryReset5.Size = new System.Drawing.Size(48, 20);
			this.entryReset5.TabIndex = 109;
			this.entryReset5.Text = "";
			this.entryReset4.Location = new Point(352, 104);
			this.entryReset4.Name = "entryReset4";
			this.entryReset4.Size = new System.Drawing.Size(48, 20);
			this.entryReset4.TabIndex = 108;
			this.entryReset4.Text = "";
			this.entryReset3.Location = new Point(352, 80);
			this.entryReset3.Name = "entryReset3";
			this.entryReset3.Size = new System.Drawing.Size(48, 20);
			this.entryReset3.TabIndex = 107;
			this.entryReset3.Text = "";
			this.entryReset2.Location = new Point(352, 56);
			this.entryReset2.Name = "entryReset2";
			this.entryReset2.Size = new System.Drawing.Size(48, 20);
			this.entryReset2.TabIndex = 106;
			this.entryReset2.Text = "";
			this.entryReset1.Location = new Point(352, 32);
			this.entryReset1.Name = "entryReset1";
			this.entryReset1.Size = new System.Drawing.Size(48, 20);
			this.entryReset1.TabIndex = 105;
			this.entryReset1.Text = "";
			this.entryTo8.Location = new Point(400, 200);
			this.entryTo8.Name = "entryTo8";
			this.entryTo8.Size = new System.Drawing.Size(32, 20);
			this.entryTo8.TabIndex = 103;
			this.entryTo8.Text = "";
			this.entrySub8.Location = new Point(320, 200);
			this.entrySub8.Name = "entrySub8";
			this.entrySub8.Size = new System.Drawing.Size(32, 20);
			this.entrySub8.TabIndex = 102;
			this.entrySub8.Text = "";
			this.chkRK8.Location = new Point(568, 204);
			this.chkRK8.Name = "chkRK8";
			this.chkRK8.Size = new System.Drawing.Size(16, 16);
			this.chkRK8.TabIndex = 99;
			this.chkRK8.Text = "checkBox15";
			this.entryMax8.Location = new Point(216, 200);
			NumericUpDown num22 = this.entryMax8;
			numArray = new int[] { 65535, 0, 0, 0 };
			num22.Maximum = new decimal(numArray);
			this.entryMax8.Name = "entryMax8";
			this.entryMax8.Size = new System.Drawing.Size(56, 20);
			this.entryMax8.TabIndex = 98;
			this.entryMax8.Click += new EventHandler(this.entryMax8_Click);
			this.entryMax8.KeyUp += new KeyEventHandler(this.entryMax8_KeyUp);
			this.entryMax8.ValueChanged += new EventHandler(this.entryMax8_ValueChanged);
			this.entryMax8.Leave += new EventHandler(this.entryMax8_Leave);
			this.btnEntryEdit8.Location = new Point(192, 200);
			this.btnEntryEdit8.Name = "btnEntryEdit8";
			this.btnEntryEdit8.Size = new System.Drawing.Size(20, 20);
			this.btnEntryEdit8.TabIndex = 97;
			this.btnEntryEdit8.Text = "?";
			this.btnEntryEdit8.Click += new EventHandler(this.btnEntryEdit8_Click);
			this.entryText8.ContextMenu = this.deleteEntry;
			this.entryText8.Location = new Point(8, 200);
			this.entryText8.Name = "entryText8";
			this.entryText8.Size = new System.Drawing.Size(184, 20);
			this.entryText8.TabIndex = 95;
			this.entryText8.Text = "";
			this.entryText8.TextChanged += new EventHandler(this.entryText8_TextChanged);
			this.entryText8.MouseLeave += new EventHandler(this.entryText8_MouseLeave);
			this.entryText8.KeyUp += new KeyEventHandler(this.entryText8_KeyUp);
			System.Windows.Forms.Menu.MenuItemCollection menuItems2 = this.deleteEntry.MenuItems;
			menuItemArray = new MenuItem[] { this.menuItem1, this.menuItem2, this.menuItem15 };
			menuItems2.AddRange(menuItemArray);
			this.menuItem1.Index = 0;
			this.menuItem1.Text = "Delete Entry";
			this.menuItem1.Click += new EventHandler(this.menuItem1_Click);
			this.menuItem2.Index = 1;
			this.menuItem2.Text = "Delete All Entries";
			this.menuItem2.Click += new EventHandler(this.menuItem2_Click);
			this.menuItem15.Index = 2;
			this.menuItem15.Text = "Add to SpawnPack";
			this.menuItem15.Click += new EventHandler(this.menuItem15_Click);
			this.chkClr8.Location = new Point(592, 204);
			this.chkClr8.Name = "chkClr8";
			this.chkClr8.Size = new System.Drawing.Size(16, 16);
			this.chkClr8.TabIndex = 96;
			this.entryTo7.Location = new Point(400, 176);
			this.entryTo7.Name = "entryTo7";
			this.entryTo7.Size = new System.Drawing.Size(32, 20);
			this.entryTo7.TabIndex = 92;
			this.entryTo7.Text = "";
			this.entrySub7.Location = new Point(320, 176);
			this.entrySub7.Name = "entrySub7";
			this.entrySub7.Size = new System.Drawing.Size(32, 20);
			this.entrySub7.TabIndex = 91;
			this.entrySub7.Text = "";
			this.entrySub7.TextChanged += new EventHandler(this.entrySub7_TextChanged);
			this.chkRK7.Location = new Point(568, 176);
			this.chkRK7.Name = "chkRK7";
			this.chkRK7.Size = new System.Drawing.Size(16, 24);
			this.chkRK7.TabIndex = 88;
			this.chkRK7.Text = "checkBox13";
			this.entryMax7.Location = new Point(216, 176);
			NumericUpDown numericUpDown22 = this.entryMax7;
			numArray = new int[] { 65535, 0, 0, 0 };
			numericUpDown22.Maximum = new decimal(numArray);
			this.entryMax7.Name = "entryMax7";
			this.entryMax7.Size = new System.Drawing.Size(56, 20);
			this.entryMax7.TabIndex = 87;
			this.entryMax7.Click += new EventHandler(this.entryMax7_Click);
			this.entryMax7.KeyUp += new KeyEventHandler(this.entryMax7_KeyUp);
			this.entryMax7.Leave += new EventHandler(this.entryMax7_Leave);
			this.btnEntryEdit7.Location = new Point(192, 176);
			this.btnEntryEdit7.Name = "btnEntryEdit7";
			this.btnEntryEdit7.Size = new System.Drawing.Size(20, 20);
			this.btnEntryEdit7.TabIndex = 86;
			this.btnEntryEdit7.Text = "?";
			this.btnEntryEdit7.Click += new EventHandler(this.btnEntryEdit7_Click);
			this.entryText7.ContextMenu = this.deleteEntry;
			this.entryText7.Location = new Point(8, 176);
			this.entryText7.Name = "entryText7";
			this.entryText7.Size = new System.Drawing.Size(184, 20);
			this.entryText7.TabIndex = 84;
			this.entryText7.Text = "";
			this.entryText7.TextChanged += new EventHandler(this.entryText7_TextChanged);
			this.entryText7.MouseLeave += new EventHandler(this.entryText7_MouseLeave);
			this.entryText7.KeyUp += new KeyEventHandler(this.entryText7_KeyUp);
			this.chkClr7.Location = new Point(592, 176);
			this.chkClr7.Name = "chkClr7";
			this.chkClr7.Size = new System.Drawing.Size(16, 24);
			this.chkClr7.TabIndex = 85;
			this.entryTo6.Location = new Point(400, 152);
			this.entryTo6.Name = "entryTo6";
			this.entryTo6.Size = new System.Drawing.Size(32, 20);
			this.entryTo6.TabIndex = 81;
			this.entryTo6.Text = "";
			this.entrySub6.Location = new Point(320, 152);
			this.entrySub6.Name = "entrySub6";
			this.entrySub6.Size = new System.Drawing.Size(32, 20);
			this.entrySub6.TabIndex = 80;
			this.entrySub6.Text = "";
			this.entrySub6.TextChanged += new EventHandler(this.entrySub6_TextChanged);
			this.chkRK6.Location = new Point(568, 152);
			this.chkRK6.Name = "chkRK6";
			this.chkRK6.Size = new System.Drawing.Size(16, 24);
			this.chkRK6.TabIndex = 77;
			this.chkRK6.Text = "checkBox11";
			this.entryMax6.Location = new Point(216, 152);
			NumericUpDown num23 = this.entryMax6;
			numArray = new int[] { 65535, 0, 0, 0 };
			num23.Maximum = new decimal(numArray);
			this.entryMax6.Name = "entryMax6";
			this.entryMax6.Size = new System.Drawing.Size(56, 20);
			this.entryMax6.TabIndex = 76;
			this.entryMax6.Click += new EventHandler(this.entryMax6_Click);
			this.entryMax6.KeyUp += new KeyEventHandler(this.entryMax6_KeyUp);
			this.entryMax6.Leave += new EventHandler(this.entryMax6_Leave);
			this.btnEntryEdit6.Location = new Point(192, 152);
			this.btnEntryEdit6.Name = "btnEntryEdit6";
			this.btnEntryEdit6.Size = new System.Drawing.Size(20, 20);
			this.btnEntryEdit6.TabIndex = 75;
			this.btnEntryEdit6.Text = "?";
			this.btnEntryEdit6.Click += new EventHandler(this.btnEntryEdit6_Click);
			this.entryText6.ContextMenu = this.deleteEntry;
			this.entryText6.Location = new Point(8, 152);
			this.entryText6.Name = "entryText6";
			this.entryText6.Size = new System.Drawing.Size(184, 20);
			this.entryText6.TabIndex = 73;
			this.entryText6.Text = "";
			this.entryText6.TextChanged += new EventHandler(this.entryText6_TextChanged);
			this.entryText6.MouseLeave += new EventHandler(this.entryText6_MouseLeave);
			this.entryText6.KeyUp += new KeyEventHandler(this.entryText6_KeyUp);
			this.chkClr6.Location = new Point(592, 152);
			this.chkClr6.Name = "chkClr6";
			this.chkClr6.Size = new System.Drawing.Size(16, 24);
			this.chkClr6.TabIndex = 74;
			this.entryTo5.Location = new Point(400, 128);
			this.entryTo5.Name = "entryTo5";
			this.entryTo5.Size = new System.Drawing.Size(32, 20);
			this.entryTo5.TabIndex = 70;
			this.entryTo5.Text = "";
			this.entrySub5.Location = new Point(320, 128);
			this.entrySub5.Name = "entrySub5";
			this.entrySub5.Size = new System.Drawing.Size(32, 20);
			this.entrySub5.TabIndex = 69;
			this.entrySub5.Text = "";
			this.entrySub5.TextChanged += new EventHandler(this.entrySub5_TextChanged);
			this.chkRK5.Location = new Point(568, 128);
			this.chkRK5.Name = "chkRK5";
			this.chkRK5.Size = new System.Drawing.Size(16, 24);
			this.chkRK5.TabIndex = 66;
			this.chkRK5.Text = "checkBox9";
			this.entryMax5.Location = new Point(216, 128);
			NumericUpDown numericUpDown23 = this.entryMax5;
			numArray = new int[] { 65535, 0, 0, 0 };
			numericUpDown23.Maximum = new decimal(numArray);
			this.entryMax5.Name = "entryMax5";
			this.entryMax5.Size = new System.Drawing.Size(56, 20);
			this.entryMax5.TabIndex = 65;
			this.entryMax5.Click += new EventHandler(this.entryMax5_Click);
			this.entryMax5.KeyUp += new KeyEventHandler(this.entryMax5_KeyUp);
			this.entryMax5.Leave += new EventHandler(this.entryMax5_Leave);
			this.btnEntryEdit5.Location = new Point(192, 128);
			this.btnEntryEdit5.Name = "btnEntryEdit5";
			this.btnEntryEdit5.Size = new System.Drawing.Size(20, 20);
			this.btnEntryEdit5.TabIndex = 64;
			this.btnEntryEdit5.Text = "?";
			this.btnEntryEdit5.Click += new EventHandler(this.btnEntryEdit5_Click);
			this.entryText5.ContextMenu = this.deleteEntry;
			this.entryText5.Location = new Point(8, 128);
			this.entryText5.Name = "entryText5";
			this.entryText5.Size = new System.Drawing.Size(184, 20);
			this.entryText5.TabIndex = 62;
			this.entryText5.Text = "";
			this.entryText5.TextChanged += new EventHandler(this.entryText5_TextChanged);
			this.entryText5.MouseLeave += new EventHandler(this.entryText5_MouseLeave);
			this.entryText5.KeyUp += new KeyEventHandler(this.entryText5_KeyUp);
			this.chkClr5.Location = new Point(592, 128);
			this.chkClr5.Name = "chkClr5";
			this.chkClr5.Size = new System.Drawing.Size(16, 24);
			this.chkClr5.TabIndex = 63;
			this.entryTo4.Location = new Point(400, 104);
			this.entryTo4.Name = "entryTo4";
			this.entryTo4.Size = new System.Drawing.Size(32, 20);
			this.entryTo4.TabIndex = 59;
			this.entryTo4.Text = "";
			this.entrySub4.Location = new Point(320, 104);
			this.entrySub4.Name = "entrySub4";
			this.entrySub4.Size = new System.Drawing.Size(32, 20);
			this.entrySub4.TabIndex = 58;
			this.entrySub4.Text = "";
			this.entrySub4.TextChanged += new EventHandler(this.entrySub4_TextChanged);
			this.chkRK4.Location = new Point(568, 104);
			this.chkRK4.Name = "chkRK4";
			this.chkRK4.Size = new System.Drawing.Size(16, 24);
			this.chkRK4.TabIndex = 55;
			this.chkRK4.Text = "checkBox7";
			this.entryMax4.Location = new Point(216, 104);
			NumericUpDown num24 = this.entryMax4;
			numArray = new int[] { 65535, 0, 0, 0 };
			num24.Maximum = new decimal(numArray);
			this.entryMax4.Name = "entryMax4";
			this.entryMax4.Size = new System.Drawing.Size(56, 20);
			this.entryMax4.TabIndex = 54;
			this.entryMax4.Click += new EventHandler(this.entryMax4_Click);
			this.entryMax4.KeyUp += new KeyEventHandler(this.entryMax4_KeyUp);
			this.entryMax4.Leave += new EventHandler(this.entryMax4_Leave);
			this.btnEntryEdit4.Location = new Point(192, 104);
			this.btnEntryEdit4.Name = "btnEntryEdit4";
			this.btnEntryEdit4.Size = new System.Drawing.Size(20, 20);
			this.btnEntryEdit4.TabIndex = 53;
			this.btnEntryEdit4.Text = "?";
			this.btnEntryEdit4.Click += new EventHandler(this.btnEntryEdit4_Click);
			this.entryText4.ContextMenu = this.deleteEntry;
			this.entryText4.Location = new Point(8, 104);
			this.entryText4.Name = "entryText4";
			this.entryText4.Size = new System.Drawing.Size(184, 20);
			this.entryText4.TabIndex = 51;
			this.entryText4.Text = "";
			this.entryText4.TextChanged += new EventHandler(this.entryText4_TextChanged);
			this.entryText4.MouseLeave += new EventHandler(this.entryText4_MouseLeave);
			this.entryText4.KeyUp += new KeyEventHandler(this.entryText4_KeyUp);
			this.chkClr4.Location = new Point(592, 104);
			this.chkClr4.Name = "chkClr4";
			this.chkClr4.Size = new System.Drawing.Size(16, 24);
			this.chkClr4.TabIndex = 52;
			this.entryTo3.Location = new Point(400, 80);
			this.entryTo3.Name = "entryTo3";
			this.entryTo3.Size = new System.Drawing.Size(32, 20);
			this.entryTo3.TabIndex = 48;
			this.entryTo3.Text = "";
			this.entrySub3.Location = new Point(320, 80);
			this.entrySub3.Name = "entrySub3";
			this.entrySub3.Size = new System.Drawing.Size(32, 20);
			this.entrySub3.TabIndex = 47;
			this.entrySub3.Text = "";
			this.entrySub3.TextChanged += new EventHandler(this.entrySub3_TextChanged);
			this.chkRK3.Location = new Point(568, 80);
			this.chkRK3.Name = "chkRK3";
			this.chkRK3.Size = new System.Drawing.Size(16, 24);
			this.chkRK3.TabIndex = 44;
			this.chkRK3.Text = "checkBox5";
			this.entryMax3.Location = new Point(216, 80);
			NumericUpDown numericUpDown24 = this.entryMax3;
			numArray = new int[] { 65535, 0, 0, 0 };
			numericUpDown24.Maximum = new decimal(numArray);
			this.entryMax3.Name = "entryMax3";
			this.entryMax3.Size = new System.Drawing.Size(56, 20);
			this.entryMax3.TabIndex = 43;
			this.entryMax3.Click += new EventHandler(this.entryMax3_Click);
			this.entryMax3.KeyUp += new KeyEventHandler(this.entryMax3_KeyUp);
			this.entryMax3.Leave += new EventHandler(this.entryMax3_Leave);
			this.btnEntryEdit3.Location = new Point(192, 80);
			this.btnEntryEdit3.Name = "btnEntryEdit3";
			this.btnEntryEdit3.Size = new System.Drawing.Size(20, 20);
			this.btnEntryEdit3.TabIndex = 42;
			this.btnEntryEdit3.Text = "?";
			this.btnEntryEdit3.Click += new EventHandler(this.btnEntryEdit3_Click);
			this.entryText3.ContextMenu = this.deleteEntry;
			this.entryText3.Location = new Point(8, 80);
			this.entryText3.Name = "entryText3";
			this.entryText3.Size = new System.Drawing.Size(184, 20);
			this.entryText3.TabIndex = 40;
			this.entryText3.Text = "";
			this.entryText3.TextChanged += new EventHandler(this.entryText3_TextChanged);
			this.entryText3.MouseLeave += new EventHandler(this.entryText3_MouseLeave);
			this.entryText3.KeyUp += new KeyEventHandler(this.entryText3_KeyUp);
			this.chkClr3.Location = new Point(592, 80);
			this.chkClr3.Name = "chkClr3";
			this.chkClr3.Size = new System.Drawing.Size(16, 24);
			this.chkClr3.TabIndex = 41;
			this.entryTo2.Location = new Point(400, 56);
			this.entryTo2.Name = "entryTo2";
			this.entryTo2.Size = new System.Drawing.Size(32, 20);
			this.entryTo2.TabIndex = 36;
			this.entryTo2.Text = "";
			this.entrySub2.Location = new Point(320, 56);
			this.entrySub2.Name = "entrySub2";
			this.entrySub2.Size = new System.Drawing.Size(32, 20);
			this.entrySub2.TabIndex = 35;
			this.entrySub2.Text = "";
			this.entrySub2.TextChanged += new EventHandler(this.entrySub2_TextChanged);
			this.chkRK2.Location = new Point(568, 56);
			this.chkRK2.Name = "chkRK2";
			this.chkRK2.Size = new System.Drawing.Size(16, 24);
			this.chkRK2.TabIndex = 30;
			this.chkRK2.Text = "checkBox3";
			this.entryMax2.Location = new Point(216, 56);
			NumericUpDown num25 = this.entryMax2;
			numArray = new int[] { 65535, 0, 0, 0 };
			num25.Maximum = new decimal(numArray);
			this.entryMax2.Name = "entryMax2";
			this.entryMax2.Size = new System.Drawing.Size(56, 20);
			this.entryMax2.TabIndex = 27;
			this.entryMax2.Click += new EventHandler(this.entryMax2_Click);
			this.entryMax2.KeyUp += new KeyEventHandler(this.entryMax2_KeyUp);
			this.entryMax2.ValueChanged += new EventHandler(this.entryMax2_ValueChanged_1);
			this.entryMax2.Leave += new EventHandler(this.entryMax2_Leave);
			this.btnEntryEdit2.Location = new Point(192, 56);
			this.btnEntryEdit2.Name = "btnEntryEdit2";
			this.btnEntryEdit2.Size = new System.Drawing.Size(20, 20);
			this.btnEntryEdit2.TabIndex = 26;
			this.btnEntryEdit2.Text = "?";
			this.btnEntryEdit2.Click += new EventHandler(this.btnEntryEdit2_Click);
			this.entryText2.ContextMenu = this.deleteEntry;
			this.entryText2.Location = new Point(8, 56);
			this.entryText2.Name = "entryText2";
			this.entryText2.Size = new System.Drawing.Size(184, 20);
			this.entryText2.TabIndex = 24;
			this.entryText2.Text = "";
			this.entryText2.TextChanged += new EventHandler(this.entryText2_TextChanged);
			this.entryText2.MouseLeave += new EventHandler(this.entryText2_MouseLeave);
			this.entryText2.KeyUp += new KeyEventHandler(this.entryText2_KeyUp);
			this.chkClr2.Location = new Point(592, 56);
			this.chkClr2.Name = "chkClr2";
			this.chkClr2.Size = new System.Drawing.Size(16, 24);
			this.chkClr2.TabIndex = 25;
			this.entryTo1.Location = new Point(400, 32);
			this.entryTo1.Name = "entryTo1";
			this.entryTo1.Size = new System.Drawing.Size(32, 20);
			this.entryTo1.TabIndex = 16;
			this.entryTo1.Text = "";
			this.vScrollBar1.LargeChange = 9;
			this.vScrollBar1.Location = new Point(616, 16);
			this.vScrollBar1.Maximum = 8;
			this.vScrollBar1.Name = "vScrollBar1";
			this.vScrollBar1.Size = new System.Drawing.Size(16, 200);
			this.vScrollBar1.TabIndex = 15;
			this.vScrollBar1.MouseEnter += new EventHandler(this.vScrollBar1_MouseEnter);
			this.vScrollBar1.Scroll += new ScrollEventHandler(this.vScrollBar1_Scroll);
			this.entrySub1.Location = new Point(320, 32);
			this.entrySub1.Name = "entrySub1";
			this.entrySub1.Size = new System.Drawing.Size(32, 20);
			this.entrySub1.TabIndex = 13;
			this.entrySub1.Text = "";
			this.entrySub1.TextChanged += new EventHandler(this.entrySub1_TextChanged);
			this.chkRK1.Location = new Point(568, 32);
			this.chkRK1.Name = "chkRK1";
			this.chkRK1.Size = new System.Drawing.Size(16, 24);
			this.chkRK1.TabIndex = 8;
			this.chkRK1.Text = "checkBox2";
			this.entryMax1.Location = new Point(216, 32);
			NumericUpDown numericUpDown25 = this.entryMax1;
			numArray = new int[] { 65535, 0, 0, 0 };
			numericUpDown25.Maximum = new decimal(numArray);
			this.entryMax1.Name = "entryMax1";
			this.entryMax1.Size = new System.Drawing.Size(56, 20);
			this.entryMax1.TabIndex = 3;
			this.entryMax1.Click += new EventHandler(this.entryMax1_Click);
			this.entryMax1.KeyUp += new KeyEventHandler(this.entryMax1_KeyUp);
			this.entryMax1.Leave += new EventHandler(this.entryMax1_Leave);
			this.btnEntryEdit1.Location = new Point(192, 32);
			this.btnEntryEdit1.Name = "btnEntryEdit1";
			this.btnEntryEdit1.Size = new System.Drawing.Size(20, 20);
			this.btnEntryEdit1.TabIndex = 2;
			this.btnEntryEdit1.Text = "?";
			this.btnEntryEdit1.Click += new EventHandler(this.btnEntryEdit1_Click);
			this.entryText1.ContextMenu = this.deleteEntry;
			this.entryText1.Location = new Point(8, 32);
			this.entryText1.Name = "entryText1";
			this.entryText1.Size = new System.Drawing.Size(184, 20);
			this.entryText1.TabIndex = 0;
			this.entryText1.Text = "";
			this.entryText1.TextChanged += new EventHandler(this.entryText1_TextChanged);
			this.entryText1.MouseLeave += new EventHandler(this.entryText1_MouseLeave);
			this.entryText1.KeyUp += new KeyEventHandler(this.entryText1_KeyUp);
			this.chkClr1.Location = new Point(592, 32);
			this.chkClr1.Name = "chkClr1";
			this.chkClr1.Size = new System.Drawing.Size(16, 24);
			this.chkClr1.TabIndex = 1;
			this.grpSpawnEdit.Anchor = AnchorStyles.Left;
			this.grpSpawnEdit.Controls.Add(this.btnSendSingleSpawner);
			this.grpSpawnEdit.Controls.Add(this.chkInContainer);
			this.grpSpawnEdit.Controls.Add(this.spnKillReset);
			this.grpSpawnEdit.Controls.Add(this.label28);
			this.grpSpawnEdit.Controls.Add(this.spnProximitySnd);
			this.grpSpawnEdit.Controls.Add(this.label27);
			this.grpSpawnEdit.Controls.Add(this.label26);
			this.grpSpawnEdit.Controls.Add(this.textTrigObjectProp);
			this.grpSpawnEdit.Controls.Add(this.spnDuration);
			this.grpSpawnEdit.Controls.Add(this.label25);
			this.grpSpawnEdit.Controls.Add(this.label24);
			this.grpSpawnEdit.Controls.Add(this.spnTODEnd);
			this.grpSpawnEdit.Controls.Add(this.spnDespawn);
			this.grpSpawnEdit.Controls.Add(this.label23);
			this.grpSpawnEdit.Controls.Add(this.spnMaxRefract);
			this.grpSpawnEdit.Controls.Add(this.spnMinRefract);
			this.grpSpawnEdit.Controls.Add(this.spnSpawnRange);
			this.grpSpawnEdit.Controls.Add(this.spnProximityRange);
			this.grpSpawnEdit.Controls.Add(this.spnTODStart);
			this.grpSpawnEdit.Controls.Add(this.spnTeam);
			this.grpSpawnEdit.Controls.Add(this.spnMaxDelay);
			this.grpSpawnEdit.Controls.Add(this.chkSmartSpawning);
			this.grpSpawnEdit.Controls.Add(this.chkSequentialSpawn);
			this.grpSpawnEdit.Controls.Add(this.chkSpawnOnTrigger);
			this.grpSpawnEdit.Controls.Add(this.chkGameTOD);
			this.grpSpawnEdit.Controls.Add(this.chkRealTOD);
			this.grpSpawnEdit.Controls.Add(this.chkAllowGhost);
			this.grpSpawnEdit.Controls.Add(this.label18);
			this.grpSpawnEdit.Controls.Add(this.label19);
			this.grpSpawnEdit.Controls.Add(this.label20);
			this.grpSpawnEdit.Controls.Add(this.label21);
			this.grpSpawnEdit.Controls.Add(this.label22);
			this.grpSpawnEdit.Controls.Add(this.label17);
			this.grpSpawnEdit.Controls.Add(this.textSkillTrigger);
			this.grpSpawnEdit.Controls.Add(this.label16);
			this.grpSpawnEdit.Controls.Add(this.textSpeechTrigger);
			this.grpSpawnEdit.Controls.Add(this.label15);
			this.grpSpawnEdit.Controls.Add(this.textProximityMsg);
			this.grpSpawnEdit.Controls.Add(this.label14);
			this.grpSpawnEdit.Controls.Add(this.textPlayerTrigProp);
			this.grpSpawnEdit.Controls.Add(this.label12);
			this.grpSpawnEdit.Controls.Add(this.textNoTriggerOnCarried);
			this.grpSpawnEdit.Controls.Add(this.label11);
			this.grpSpawnEdit.Controls.Add(this.textTriggerOnCarried);
			this.grpSpawnEdit.Controls.Add(this.chkHomeRangeIsRelative);
			this.grpSpawnEdit.Controls.Add(this.btnMove);
			this.grpSpawnEdit.Controls.Add(this.btnRestoreSpawnDefaults);
			this.grpSpawnEdit.Controls.Add(this.btnDeleteSpawn);
			this.grpSpawnEdit.Controls.Add(this.lblMaxDelay);
			this.grpSpawnEdit.Controls.Add(this.chkRunning);
			this.grpSpawnEdit.Controls.Add(this.lblHomeRange);
			this.grpSpawnEdit.Controls.Add(this.spnMaxCount);
			this.grpSpawnEdit.Controls.Add(this.txtName);
			this.grpSpawnEdit.Controls.Add(this.spnHomeRange);
			this.grpSpawnEdit.Controls.Add(this.lblTeam);
			this.grpSpawnEdit.Controls.Add(this.lblMaxCount);
			this.grpSpawnEdit.Controls.Add(this.spnMinDelay);
			this.grpSpawnEdit.Controls.Add(this.chkGroup);
			this.grpSpawnEdit.Controls.Add(this.lblMinDelay);
			this.grpSpawnEdit.Location = new Point(5, 0);
			this.grpSpawnEdit.Name = "grpSpawnEdit";
			this.grpSpawnEdit.Size = new System.Drawing.Size(360, 440);
			this.grpSpawnEdit.TabIndex = 0;
			this.grpSpawnEdit.TabStop = false;
			this.grpSpawnEdit.Text = "Spawn Details";
			this.grpSpawnEdit.Leave += new EventHandler(this.grpSpawnEdit_Leave);
			this.btnSendSingleSpawner.ContextMenu = this.unloadSingleSpawner;
			this.btnSendSingleSpawner.Enabled = false;
			this.btnSendSingleSpawner.Location = new Point(224, 408);
			this.btnSendSingleSpawner.Name = "btnSendSingleSpawner";
			this.btnSendSingleSpawner.Size = new System.Drawing.Size(120, 23);
			this.btnSendSingleSpawner.TabIndex = 208;
			this.btnSendSingleSpawner.Text = "Send to Server";
			this.btnSendSingleSpawner.Click += new EventHandler(this.btnSendSpawn_Click);
			System.Windows.Forms.Menu.MenuItemCollection menuItemCollections2 = this.unloadSingleSpawner.MenuItems;
			menuItemArray = new MenuItem[] { this.mniUnloadSingleSpawner, this.menuItem23 };
			menuItemCollections2.AddRange(menuItemArray);
			this.unloadSingleSpawner.Popup += new EventHandler(this.unloadSingleSpawner_Popup);
			this.mniUnloadSingleSpawner.Index = 0;
			this.mniUnloadSingleSpawner.Text = "Unload Spawner from Server";
			this.mniUnloadSingleSpawner.Click += new EventHandler(this.mniUnloadSingleSpawner_Click_1);
			this.menuItem23.Index = 1;
			this.menuItem23.Text = "Cancel";
			this.label26.Location = new Point(8, 340);
			this.label26.Name = "label26";
			this.label26.Size = new System.Drawing.Size(112, 20);
			this.label26.TabIndex = 200;
			this.label26.Text = "TrigObjectProp";
			this.textTrigObjectProp.Location = new Point(120, 340);
			this.textTrigObjectProp.Name = "textTrigObjectProp";
			this.textTrigObjectProp.Size = new System.Drawing.Size(232, 20);
			this.textTrigObjectProp.TabIndex = 199;
			this.textTrigObjectProp.Text = "";
			this.label17.Location = new Point(8, 260);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(112, 20);
			this.label17.TabIndex = 175;
			this.label17.Text = "SkillTrigger";
			this.textSkillTrigger.Location = new Point(120, 260);
			this.textSkillTrigger.Name = "textSkillTrigger";
			this.textSkillTrigger.Size = new System.Drawing.Size(232, 20);
			this.textSkillTrigger.TabIndex = 174;
			this.textSkillTrigger.Text = "";
			this.label16.Location = new Point(8, 280);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(112, 16);
			this.label16.TabIndex = 172;
			this.label16.Text = "SpeechTrigger";
			this.textSpeechTrigger.Location = new Point(120, 280);
			this.textSpeechTrigger.Name = "textSpeechTrigger";
			this.textSpeechTrigger.Size = new System.Drawing.Size(232, 20);
			this.textSpeechTrigger.TabIndex = 171;
			this.textSpeechTrigger.Text = "";
			this.label15.Location = new Point(8, 300);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(112, 20);
			this.label15.TabIndex = 169;
			this.label15.Text = "ProximityMsg";
			this.textProximityMsg.Location = new Point(120, 300);
			this.textProximityMsg.Name = "textProximityMsg";
			this.textProximityMsg.Size = new System.Drawing.Size(232, 20);
			this.textProximityMsg.TabIndex = 168;
			this.textProximityMsg.Text = "";
			this.label14.Location = new Point(8, 320);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(112, 16);
			this.label14.TabIndex = 160;
			this.label14.Text = "PlayerTrigProp";
			this.textPlayerTrigProp.Location = new Point(120, 320);
			this.textPlayerTrigProp.Name = "textPlayerTrigProp";
			this.textPlayerTrigProp.Size = new System.Drawing.Size(232, 20);
			this.textPlayerTrigProp.TabIndex = 159;
			this.textPlayerTrigProp.Text = "";
			this.label12.Location = new Point(8, 380);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(112, 20);
			this.label12.TabIndex = 154;
			this.label12.Text = "NoTriggerOnCarried";
			this.textNoTriggerOnCarried.Location = new Point(120, 380);
			this.textNoTriggerOnCarried.Name = "textNoTriggerOnCarried";
			this.textNoTriggerOnCarried.Size = new System.Drawing.Size(232, 20);
			this.textNoTriggerOnCarried.TabIndex = 153;
			this.textNoTriggerOnCarried.Text = "";
			this.label11.Location = new Point(8, 360);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(112, 16);
			this.label11.TabIndex = 151;
			this.label11.Text = "TriggerOnCarried";
			this.textTriggerOnCarried.Location = new Point(120, 360);
			this.textTriggerOnCarried.Name = "textTriggerOnCarried";
			this.textTriggerOnCarried.Size = new System.Drawing.Size(232, 20);
			this.textTriggerOnCarried.TabIndex = 150;
			this.textTriggerOnCarried.Text = "";
			System.Windows.Forms.Menu.MenuItemCollection menuItems3 = this.mainMenu1.MenuItems;
			menuItemArray = new MenuItem[] { this.menuItem5, this.menuItem22, this.menuItem8, this.menuItem14, this.menuItem16 };
			menuItems3.AddRange(menuItemArray);
			this.menuItem5.Index = 0;
			System.Windows.Forms.Menu.MenuItemCollection menuItemCollections3 = this.menuItem5.MenuItems;
			menuItemArray = new MenuItem[] { this.menuItem6, this.menuItem7, this.menuItem10, this.menuItem11, this.menuItem12, this.menuItem13 };
			menuItemCollections3.AddRange(menuItemArray);
			this.menuItem5.Text = "File";
			this.menuItem6.Index = 0;
			this.menuItem6.Text = "Load Spawn Packs";
			this.menuItem6.Click += new EventHandler(this.menuItem6_Click);
			this.menuItem7.Index = 1;
			this.menuItem7.Text = "Save Spawn Packs";
			this.menuItem7.Click += new EventHandler(this.menuItem7_Click);
			this.menuItem10.Index = 2;
			this.menuItem10.Text = "Import All Spawn Types";
			this.menuItem10.Click += new EventHandler(this.menuItem10_Click);
			this.menuItem11.Index = 3;
			this.menuItem11.Text = "Export All Spawn Types";
			this.menuItem11.Click += new EventHandler(this.menuItem11_Click);
			this.menuItem12.Index = 4;
			this.menuItem12.Text = "Import .map file";
			this.menuItem12.Click += new EventHandler(this.menuItem12_Click);
			this.menuItem13.Index = 5;
			this.menuItem13.Text = "Import .msf file";
			this.menuItem13.Click += new EventHandler(this.menuItem13_Click);
			this.menuItem22.Index = 1;
			System.Windows.Forms.Menu.MenuItemCollection menuItems4 = this.menuItem22.MenuItems;
			menuItemArray = new MenuItem[] { this.menuItem24, this.menuItem25 };
			menuItems4.AddRange(menuItemArray);
			this.menuItem22.Text = "Edit";
			this.menuItem24.Index = 0;
			System.Windows.Forms.Menu.MenuItemCollection menuItemCollections4 = this.menuItem24.MenuItems;
			menuItemArray = new MenuItem[] { this.mniDeleteInSelectionWindow, this.mniDeleteNotSelected, this.mniToolbarDeleteAllSpawns, this.mniDeleteAllFiltered, this.mniDeleteAllUnfiltered };
			menuItemCollections4.AddRange(menuItemArray);
			this.menuItem24.Text = "Delete";
			this.mniDeleteInSelectionWindow.Index = 0;
			this.mniDeleteInSelectionWindow.Text = "Spawns in Selection Window";
			this.mniDeleteInSelectionWindow.Click += new EventHandler(this.mniDeleteInSelectionWindow_Click);
			this.mniDeleteNotSelected.Index = 1;
			this.mniDeleteNotSelected.Text = "Spawns NOT in Selection Window";
			this.mniDeleteNotSelected.Click += new EventHandler(this.mniDeleteNotSelected_Click);
			this.mniToolbarDeleteAllSpawns.Index = 2;
			this.mniToolbarDeleteAllSpawns.Text = "All Spawns";
			this.mniToolbarDeleteAllSpawns.Click += new EventHandler(this.mniToolbarDeleteAllSpawns_Click);
			this.mniDeleteAllFiltered.Index = 3;
			this.mniDeleteAllFiltered.Text = "Filtered Spawns (gray, not displayed) ";
			this.mniDeleteAllFiltered.Click += new EventHandler(this.mniDeleteAllFiltered_Click);
			this.mniDeleteAllUnfiltered.Index = 4;
			this.mniDeleteAllUnfiltered.Text = "Un-Filtered Spawns (black, displayed) ";
			this.mniDeleteAllUnfiltered.Click += new EventHandler(this.mniDeleteAllUnfiltered_Click);
			this.menuItem25.Index = 1;
			System.Windows.Forms.Menu.MenuItemCollection menuItems5 = this.menuItem25.MenuItems;
			menuItemArray = new MenuItem[] { this.mniModifyInSelectionWindow, this.mniModifiedUnfiltered };
			menuItems5.AddRange(menuItemArray);
			this.menuItem25.Text = "Modify Properties";
			this.mniModifyInSelectionWindow.Index = 0;
			this.mniModifyInSelectionWindow.Text = "of Spawns in Selection Window";
			this.mniModifyInSelectionWindow.Click += new EventHandler(this.mniModifyInSelectionWindow_Click);
			this.mniModifiedUnfiltered.Index = 1;
			this.mniModifiedUnfiltered.Text = "of Un-Filtered Spawns (black, displayed)";
			this.mniModifiedUnfiltered.Click += new EventHandler(this.mniModifiedUnfiltered_Click);
			this.menuItem8.Index = 2;
			System.Windows.Forms.Menu.MenuItemCollection menuItemCollections5 = this.menuItem8.MenuItems;
			menuItemArray = new MenuItem[] { this.menuItem9, this.menuItem17, this.mniDisplayFilterSettings };
			menuItemCollections5.AddRange(menuItemArray);
			this.menuItem8.Text = "Tools";
			this.menuItem9.Index = 0;
			this.menuItem9.Text = "Setup";
			this.menuItem9.Click += new EventHandler(this.menuItem9_Click);
			this.menuItem17.Index = 1;
			this.menuItem17.Text = "Transfer Server Settings";
			this.menuItem17.Click += new EventHandler(this.menuItem17_Click);
			this.mniDisplayFilterSettings.Index = 2;
			this.mniDisplayFilterSettings.Text = "Display Filter Settings";
			this.mniDisplayFilterSettings.Click += new EventHandler(this.mniDisplayFilterSettings_Click);
			this.menuItem14.Index = 3;
			System.Windows.Forms.Menu.MenuItemCollection menuItems6 = this.menuItem14.MenuItems;
			menuItemArray = new MenuItem[] { this.mniAlwaysOnTop };
			menuItems6.AddRange(menuItemArray);
			this.menuItem14.Text = "Options";
			this.mniAlwaysOnTop.Index = 0;
			this.mniAlwaysOnTop.Text = "Always On Top";
			this.mniAlwaysOnTop.Click += new EventHandler(this.mniAlwaysOnTop_Click);
			this.menuItem16.Index = 4;
			System.Windows.Forms.Menu.MenuItemCollection menuItemCollections6 = this.menuItem16.MenuItems;
			menuItemArray = new MenuItem[] { this.menuItem18, this.menuItem4 };
			menuItemCollections6.AddRange(menuItemArray);
			this.menuItem16.Text = "Help";
			this.menuItem18.Index = 0;
			this.menuItem18.Text = "Help";
			this.menuItem18.Click += new EventHandler(this.menuItem18_Click);
			this.menuItem4.Index = 1;
			this.menuItem4.Text = "About";
			this.menuItem4.Click += new EventHandler(this.menuItem4_Click);
			this.panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.panel1.Controls.Add(this.tabControl1);
			this.panel1.Controls.Add(this.axUOMap);
			this.panel1.Controls.Add(this.panel3);
			this.panel1.Cursor = Cursors.Default;
			this.panel1.Location = new Point(172, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(848, 749);
			this.panel1.TabIndex = 5;
			this.tabControl1.Controls.Add(this.tabBasic);
			this.tabControl1.Controls.Add(this.tabAdvanced);
			this.tabControl1.Controls.Add(this.tabSpawnTypes);
			this.tabControl1.Location = new Point(480, 1);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(376, 465);
			this.tabControl1.TabIndex = 8;
			this.tabControl1.Leave += new EventHandler(this.tabControl1_Leave);
			this.tabBasic.Controls.Add(this.grpSpawnEdit);
			this.tabBasic.Location = new Point(4, 22);
			this.tabBasic.Name = "tabBasic";
			this.tabBasic.Size = new System.Drawing.Size(368, 439);
			this.tabBasic.TabIndex = 0;
			this.tabBasic.Text = "Basic";
			this.tabAdvanced.Controls.Add(this.groupBox1);
			this.tabAdvanced.Location = new Point(4, 22);
			this.tabAdvanced.Name = "tabAdvanced";
			this.tabAdvanced.Size = new System.Drawing.Size(368, 439);
			this.tabAdvanced.TabIndex = 1;
			this.tabAdvanced.Text = "Advanced";
			this.groupBox1.Controls.Add(this.label44);
			this.groupBox1.Controls.Add(this.txtNotes);
			this.groupBox1.Controls.Add(this.spnContainerZ);
			this.groupBox1.Controls.Add(this.spnContainerY);
			this.groupBox1.Controls.Add(this.spnContainerX);
			this.groupBox1.Controls.Add(this.label37);
			this.groupBox1.Controls.Add(this.textRegionName);
			this.groupBox1.Controls.Add(this.label36);
			this.groupBox1.Controls.Add(this.textWayPoint);
			this.groupBox1.Controls.Add(this.label35);
			this.groupBox1.Controls.Add(this.textConfigFile);
			this.groupBox1.Controls.Add(this.label34);
			this.groupBox1.Controls.Add(this.textSetObjectName);
			this.groupBox1.Controls.Add(this.label33);
			this.groupBox1.Controls.Add(this.textTrigObjectName);
			this.groupBox1.Controls.Add(this.chkExternalTriggering);
			this.groupBox1.Controls.Add(this.labelContainerZ);
			this.groupBox1.Controls.Add(this.labelContainerY);
			this.groupBox1.Controls.Add(this.labelContainerX);
			this.groupBox1.Controls.Add(this.label32);
			this.groupBox1.Controls.Add(this.spnStackAmount);
			this.groupBox1.Controls.Add(this.spnTriggerProbability);
			this.groupBox1.Controls.Add(this.label31);
			this.groupBox1.Controls.Add(this.label13);
			this.groupBox1.Controls.Add(this.textMobTriggerName);
			this.groupBox1.Controls.Add(this.label10);
			this.groupBox1.Controls.Add(this.textMobTrigProp);
			this.groupBox1.Location = new Point(5, 0);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(360, 440);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Spawn Details";
			this.groupBox1.Leave += new EventHandler(this.groupBox1_Leave);
			this.label44.Location = new Point(8, 344);
			this.label44.Name = "label44";
			this.label44.Size = new System.Drawing.Size(64, 16);
			this.label44.TabIndex = 237;
			this.label44.Text = "Notes:";
			this.txtNotes.Location = new Point(8, 360);
			this.txtNotes.Multiline = true;
			this.txtNotes.Name = "txtNotes";
			this.txtNotes.Size = new System.Drawing.Size(344, 72);
			this.txtNotes.TabIndex = 236;
			this.txtNotes.Text = "";
			this.label37.Location = new Point(8, 128);
			this.label37.Name = "label37";
			this.label37.Size = new System.Drawing.Size(112, 16);
			this.label37.TabIndex = 232;
			this.label37.Text = "RegionName:";
			this.textRegionName.Location = new Point(120, 128);
			this.textRegionName.Name = "textRegionName";
			this.textRegionName.Size = new System.Drawing.Size(232, 20);
			this.textRegionName.TabIndex = 231;
			this.textRegionName.Text = "";
			this.label36.Location = new Point(8, 152);
			this.label36.Name = "label36";
			this.label36.Size = new System.Drawing.Size(112, 16);
			this.label36.TabIndex = 230;
			this.label36.Text = "WaypointName:";
			this.textWayPoint.Location = new Point(120, 152);
			this.textWayPoint.Name = "textWayPoint";
			this.textWayPoint.Size = new System.Drawing.Size(232, 20);
			this.textWayPoint.TabIndex = 229;
			this.textWayPoint.Text = "";
			this.label35.Location = new Point(8, 176);
			this.label35.Name = "label35";
			this.label35.Size = new System.Drawing.Size(112, 16);
			this.label35.TabIndex = 228;
			this.label35.Text = "ConfigFile:";
			this.textConfigFile.Location = new Point(120, 176);
			this.textConfigFile.Name = "textConfigFile";
			this.textConfigFile.Size = new System.Drawing.Size(232, 20);
			this.textConfigFile.TabIndex = 227;
			this.textConfigFile.Text = "";
			this.label34.Location = new Point(8, 272);
			this.label34.Name = "label34";
			this.label34.Size = new System.Drawing.Size(112, 16);
			this.label34.TabIndex = 226;
			this.label34.Text = "SetObjectName:";
			this.textSetObjectName.Location = new Point(120, 272);
			this.textSetObjectName.Name = "textSetObjectName";
			this.textSetObjectName.Size = new System.Drawing.Size(232, 20);
			this.textSetObjectName.TabIndex = 225;
			this.textSetObjectName.Text = "";
			this.label33.Location = new Point(8, 248);
			this.label33.Name = "label33";
			this.label33.Size = new System.Drawing.Size(112, 16);
			this.label33.TabIndex = 224;
			this.label33.Text = "TrigObjectName:";
			this.textTrigObjectName.Location = new Point(120, 248);
			this.textTrigObjectName.Name = "textTrigObjectName";
			this.textTrigObjectName.Size = new System.Drawing.Size(232, 20);
			this.textTrigObjectName.TabIndex = 223;
			this.textTrigObjectName.Text = "";
			this.labelContainerZ.Enabled = false;
			this.labelContainerZ.Location = new Point(232, 80);
			this.labelContainerZ.Name = "labelContainerZ";
			this.labelContainerZ.Size = new System.Drawing.Size(16, 16);
			this.labelContainerZ.TabIndex = 219;
			this.labelContainerZ.Text = "Z:";
			this.labelContainerY.Enabled = false;
			this.labelContainerY.Location = new Point(232, 56);
			this.labelContainerY.Name = "labelContainerY";
			this.labelContainerY.Size = new System.Drawing.Size(16, 16);
			this.labelContainerY.TabIndex = 217;
			this.labelContainerY.Text = "Y:";
			this.labelContainerX.Enabled = false;
			this.labelContainerX.Location = new Point(184, 32);
			this.labelContainerX.Name = "labelContainerX";
			this.labelContainerX.Size = new System.Drawing.Size(72, 16);
			this.labelContainerX.TabIndex = 215;
			this.labelContainerX.Text = "Container X:";
			this.label32.Location = new Point(8, 32);
			this.label32.Name = "label32";
			this.label32.Size = new System.Drawing.Size(88, 20);
			this.label32.TabIndex = 201;
			this.label32.Text = "StackAmount:";
			this.label31.Location = new Point(8, 56);
			this.label31.Name = "label31";
			this.label31.Size = new System.Drawing.Size(104, 20);
			this.label31.TabIndex = 199;
			this.label31.Text = "TriggerProbability:";
			this.label13.Location = new Point(8, 200);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(112, 16);
			this.label13.TabIndex = 170;
			this.label13.Text = "MobTriggerName:";
			this.textMobTriggerName.Location = new Point(120, 200);
			this.textMobTriggerName.Name = "textMobTriggerName";
			this.textMobTriggerName.Size = new System.Drawing.Size(232, 20);
			this.textMobTriggerName.TabIndex = 169;
			this.textMobTriggerName.Text = "";
			this.label10.Location = new Point(8, 224);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(112, 20);
			this.label10.TabIndex = 168;
			this.label10.Text = "MobTrigProp:";
			this.textMobTrigProp.Location = new Point(120, 224);
			this.textMobTrigProp.Name = "textMobTrigProp";
			this.textMobTrigProp.Size = new System.Drawing.Size(232, 20);
			this.textMobTrigProp.TabIndex = 167;
			this.textMobTrigProp.Text = "";
			this.tabSpawnTypes.Controls.Add(this.groupBox3);
			this.tabSpawnTypes.Controls.Add(this.groupBox2);
			this.tabSpawnTypes.Controls.Add(this.grpSpawnTypes);
			this.tabSpawnTypes.Location = new Point(4, 22);
			this.tabSpawnTypes.Name = "tabSpawnTypes";
			this.tabSpawnTypes.Size = new System.Drawing.Size(368, 439);
			this.tabSpawnTypes.TabIndex = 2;
			this.tabSpawnTypes.Text = "SpawnTypes";
			this.groupBox3.Controls.Add(this.tvwSpawnPacks);
			this.groupBox3.Location = new Point(184, 288);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(176, 152);
			this.groupBox3.TabIndex = 3;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "All Spawn Packs";
			this.groupBox2.Controls.Add(this.btnUpdateSpawnPacks);
			this.groupBox2.Controls.Add(this.textSpawnPackName);
			this.groupBox2.Controls.Add(this.button1);
			this.groupBox2.Controls.Add(this.clbSpawnPack);
			this.groupBox2.Controls.Add(this.label39);
			this.groupBox2.Controls.Add(this.btnUpdateFromSpawnPack);
			this.groupBox2.Location = new Point(184, 0);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(176, 288);
			this.groupBox2.TabIndex = 2;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Current Spawn Pack";
			this.textSpawnPackName.Location = new Point(8, 16);
			this.textSpawnPackName.Name = "textSpawnPackName";
			this.textSpawnPackName.Size = new System.Drawing.Size(160, 20);
			this.textSpawnPackName.TabIndex = 16;
			this.textSpawnPackName.Text = "";
			this.label39.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.label39.BorderStyle = BorderStyle.Fixed3D;
			this.label39.Location = new Point(8, 264);
			this.label39.Name = "label39";
			this.label39.Size = new System.Drawing.Size(160, 16);
			this.label39.TabIndex = 5;
			this.panel3.Controls.Add(this.grpSpawnEntries);
			this.panel3.Controls.Add(this.groupTemplateList);
			this.panel3.Location = new Point(8, 472);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(848, 248);
			this.panel3.TabIndex = 7;
			this.panel3.Visible = false;
			System.Windows.Forms.Menu.MenuItemCollection menuItems7 = this.mcnSpawnPack.MenuItems;
			menuItemArray = new MenuItem[] { this.mniDeleteType, this.mniDeleteAllTypes };
			menuItems7.AddRange(menuItemArray);
			this.mcnSpawnPack.Popup += new EventHandler(this.mcnSpawnPack_Popup);
			this.mniDeleteType.Index = 0;
			this.mniDeleteType.Text = "Delete Type";
			this.mniDeleteType.Click += new EventHandler(this.mniDeleteType_Click);
			this.mniDeleteAllTypes.Index = 1;
			this.mniDeleteAllTypes.Text = "Delete Alll Types";
			this.mniDeleteAllTypes.Click += new EventHandler(this.mniDeleteAllTypes_Click);
			System.Windows.Forms.Menu.MenuItemCollection menuItemCollections7 = this.mcnSpawnPacks.MenuItems;
			menuItemArray = new MenuItem[] { this.mniDeletePack };
			menuItemCollections7.AddRange(menuItemArray);
			this.mniDeletePack.Index = 0;
			this.mniDeletePack.Text = "Delete Pack";
			this.mniDeletePack.Click += new EventHandler(this.mniDeletePack_Click);
			this.openSpawnPacks.FileName = "SpawnPacks.dat";
			this.openSpawnPacks.InitialDirectory = ".";
			this.saveSpawnPacks.FileName = "SpawnPacks.dat";
			this.saveSpawnPacks.InitialDirectory = ".";
			this.exportAllSpawnTypes.FileName = "SpawnTypes.std";
			this.exportAllSpawnTypes.InitialDirectory = ".";
			this.importAllSpawnTypes.FileName = "SpawnTypes.std";
			this.importAllSpawnTypes.InitialDirectory = ".";
			this.importMapFile.Filter = ".map | *.map";
			this.importMSFFile.Filter = ".msf | *.msf";
			base.AutoScale = false;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			base.ClientSize = new System.Drawing.Size(1016, 698);
			base.Controls.Add(this.panel1);
			base.Controls.Add(this.pnlControls);
			base.Controls.Add(this.stbMain);
			base.Icon = (System.Drawing.Icon)resourceManager.GetObject("$this.Icon");
			base.Menu = this.mainMenu1;
			base.Name = "SpawnEditor";
			base.StartPosition = FormStartPosition.CenterScreen;
			this.Text = "Spawn Editor 2";
			base.Closing += new CancelEventHandler(this.SpawnEditor_Closing);
			base.Load += new EventHandler(this.SpawnEditor_Load);
			this.axUOMap.EndInit();
			((ISupportInitialize)this.trkZoom).EndInit();
			((ISupportInitialize)this.spnMaxCount).EndInit();
			((ISupportInitialize)this.spnHomeRange).EndInit();
			((ISupportInitialize)this.spnMinDelay).EndInit();
			((ISupportInitialize)this.spnTeam).EndInit();
			((ISupportInitialize)this.spnMaxDelay).EndInit();
			((ISupportInitialize)this.spnSpawnRange).EndInit();
			((ISupportInitialize)this.spnProximityRange).EndInit();
			((ISupportInitialize)this.spnMinRefract).EndInit();
			((ISupportInitialize)this.spnTODStart).EndInit();
			((ISupportInitialize)this.spnMaxRefract).EndInit();
			((ISupportInitialize)this.spnDespawn).EndInit();
			((ISupportInitialize)this.spnTODEnd).EndInit();
			((ISupportInitialize)this.spnDuration).EndInit();
			((ISupportInitialize)this.spnProximitySnd).EndInit();
			((ISupportInitialize)this.spnKillReset).EndInit();
			((ISupportInitialize)this.spnTriggerProbability).EndInit();
			((ISupportInitialize)this.spnStackAmount).EndInit();
			((ISupportInitialize)this.spnContainerX).EndInit();
			((ISupportInitialize)this.spnContainerY).EndInit();
			((ISupportInitialize)this.spnContainerZ).EndInit();
			this.pnlControls.ResumeLayout(false);
			this.tabControl3.ResumeLayout(false);
			this.tabMapSettings.ResumeLayout(false);
			this.grpMapControl.ResumeLayout(false);
			this.tabControl2.ResumeLayout(false);
			this.tabPage3.ResumeLayout(false);
			this.grpSpawnList.ResumeLayout(false);
			this.tabPage4.ResumeLayout(false);
			this.tabPage5.ResumeLayout(false);
			this.groupTemplateList.ResumeLayout(false);
			this.grpSpawnTypes.ResumeLayout(false);
			this.grpSpawnEntries.ResumeLayout(false);
			((ISupportInitialize)this.entryPer8).EndInit();
			((ISupportInitialize)this.entryPer7).EndInit();
			((ISupportInitialize)this.entryPer6).EndInit();
			((ISupportInitialize)this.entryPer5).EndInit();
			((ISupportInitialize)this.entryPer4).EndInit();
			((ISupportInitialize)this.entryPer3).EndInit();
			((ISupportInitialize)this.entryPer2).EndInit();
			((ISupportInitialize)this.entryPer1).EndInit();
			((ISupportInitialize)this.entryMax8).EndInit();
			((ISupportInitialize)this.entryMax7).EndInit();
			((ISupportInitialize)this.entryMax6).EndInit();
			((ISupportInitialize)this.entryMax5).EndInit();
			((ISupportInitialize)this.entryMax4).EndInit();
			((ISupportInitialize)this.entryMax3).EndInit();
			((ISupportInitialize)this.entryMax2).EndInit();
			((ISupportInitialize)this.entryMax1).EndInit();
			this.grpSpawnEdit.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.tabControl1.ResumeLayout(false);
			this.tabBasic.ResumeLayout(false);
			this.tabAdvanced.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.tabSpawnTypes.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.panel3.ResumeLayout(false);
			base.ResumeLayout(false);
		}

		private void InitializeMapCenters()
		{
			this.MapLoc[1] = new MapLocation();
			this.MapLoc[1].X = 3072;
			this.MapLoc[1].Y = 2048;
			this.MapLoc[0] = new MapLocation();
			this.MapLoc[0].X = 3072;
			this.MapLoc[0].Y = 2048;
			this.MapLoc[2] = new MapLocation();
			this.MapLoc[2].X = 1150;
			this.MapLoc[2].Y = 800;
			this.MapLoc[3] = new MapLocation();
			this.MapLoc[3].X = 1280;
			this.MapLoc[3].Y = 1024;
			this.MapLoc[4] = new MapLocation();
			this.MapLoc[4].X = 700;
			this.MapLoc[4].Y = 700;
		}

		private void LargeWindow()
		{
			base.MinimumSize = new System.Drawing.Size(660, 520);
			base.MaximumSize = new System.Drawing.Size(1024, 768);
			base.Size = new System.Drawing.Size(1024, 768);
			this.panel1.Visible = true;
			this.tabControl1.Visible = true;
			this.panel3.Visible = true;
			this.axUOMap.Anchor = AnchorStyles.Top | AnchorStyles.Left;
			this.axUOMap.Size = new System.Drawing.Size(472, 464);
			this.tabControl2.Size = new System.Drawing.Size(176, 500);
		}

		private void lblMinDelay_Click(object sender, EventArgs e)
		{
		}

		private void LoadCustomAssemblies(string rootPath)
		{
			if (rootPath == null || rootPath.Length == 0)
			{
				return;
			}
			string str = Path.Combine(rootPath, "Data/Assemblies.cfg");
			if (File.Exists(str))
			{
				try
				{
					using (StreamReader streamReader = new StreamReader(str))
					{
						while (true)
						{
							string str1 = streamReader.ReadLine();
							string str2 = str1;
							if (str1 == null)
							{
								break;
							}
							str2.Trim();
							if (str2 != null && !(str2.ToLower() == "ultima.dll") && !(str2.ToLower() == "uomaplib.dll") && !(str2.ToLower() == "axuomaplib.dll"))
							{
								string str3 = Path.Combine(rootPath, str2);
								if (File.Exists(str3))
								{
									Assembly.LoadFrom(str3);
								}
							}
						}
						streamReader.Close();
					}
				}
				catch
				{
				}
			}
		}

		private void LoadDefaultSpawnValues()
		{
			this.txtName.Text = string.Concat(this._CfgDialog.CfgSpawnNameValue, this.tvwSpawnPoints.Nodes.Count);
			this.spnHomeRange.Value = this._CfgDialog.CfgSpawnHomeRangeValue;
			this.spnMaxCount.Value = this._CfgDialog.CfgSpawnMaxCountValue;
			this.spnMinDelay.Value = this._CfgDialog.CfgSpawnMinDelayValue;
			this.spnMaxDelay.Value = this._CfgDialog.CfgSpawnMaxDelayValue;
			this.spnTeam.Value = this._CfgDialog.CfgSpawnTeamValue;
			this.chkGroup.Checked = this._CfgDialog.CfgSpawnGroupValue;
			this.chkRunning.Checked = this._CfgDialog.CfgSpawnRunningValue;
			this.chkHomeRangeIsRelative.Checked = this._CfgDialog.CfgSpawnRelativeHomeValue;
			this.spnSpawnRange.Value = new decimal(-1);
			this.spnProximityRange.Value = new decimal(-1);
			this.spnDuration.Value = new decimal(0);
			this.spnDespawn.Value = new decimal(0);
			this.spnMinRefract.Value = new decimal(0);
			this.spnMaxRefract.Value = new decimal(0);
			this.spnTODStart.Value = new decimal(0);
			this.spnTODEnd.Value = new decimal(0);
			this.spnKillReset.Value = new decimal(1);
			this.spnProximitySnd.Value = new decimal(500);
			this.chkAllowGhost.Checked = false;
			this.chkSpawnOnTrigger.Checked = false;
			this.chkSequentialSpawn.Checked = false;
			this.chkSmartSpawning.Checked = false;
			this.chkInContainer.Checked = false;
			this.chkRealTOD.Checked = true;
			this.chkGameTOD.Checked = false;
			this.textSkillTrigger.Text = null;
			this.textSpeechTrigger.Text = null;
			this.textProximityMsg.Text = null;
			this.textMobTriggerName.Text = null;
			this.textMobTrigProp.Text = null;
			this.textPlayerTrigProp.Text = null;
			this.textTrigObjectProp.Text = null;
			this.textTriggerOnCarried.Text = null;
			this.textNoTriggerOnCarried.Text = null;
			this.spnTriggerProbability.Value = new decimal(1);
			this.spnStackAmount.Value = new decimal(1);
			this.spnContainerX.Value = new decimal(0);
			this.spnContainerY.Value = new decimal(0);
			this.spnContainerZ.Value = new decimal(0);
			this.chkExternalTriggering.Checked = false;
			this.textTrigObjectName.Text = null;
			this.textSetObjectName.Text = null;
			this.textRegionName.Text = null;
			this.textConfigFile.Text = null;
			this.textWayPoint.Text = null;
		}

		internal void LoadSpawnFile(string FilePath, WorldMap ForceMap)
		{
			if (File.Exists(FilePath))
			{
				FileStream fileStream = null;
				try
				{
					fileStream = File.Open(FilePath, FileMode.Open, FileAccess.Read);
				}
				catch (Exception exception1)
				{
					Exception exception = exception1;
					string[] filePath = new string[] { "Failed to open file [", FilePath, "] for the following reason:", Environment.NewLine, this.ExceptionMessage(exception) };
					MessageBox.Show(this, string.Concat(filePath), "Load Failure", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
				this.LoadSpawnFile(fileStream, FilePath, ForceMap);
				try
				{
					fileStream.Close();
				}
				catch
				{
				}
			}
		}

		internal void LoadSpawnFile(Stream stream, string FilePath, WorldMap ForceMap)
		{
			if (stream == null)
			{
				MessageBox.Show(this, "Unable to Load Spawns: Empty Stream.", "Read Failure", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return;
			}
			try
			{
				this.tvwSpawnPoints.Sorted = false;
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.Load(stream);
				XmlElement item = xmlDocument["Spawns"];
				if (item != null)
				{
					RectangleConverter rectangleConverter = new RectangleConverter();
					int num = 0;
					XmlNodeList elementsByTagName = item.GetElementsByTagName("Points");
					this.progressBar1.Visible = true;
					this.lblTransferStatus.Visible = true;
					this.trkZoom.Visible = false;
					this.lblTrkMin.Visible = false;
					this.lblTrkMax.Visible = false;
					this.lblTransferStatus.Text = "Processing Spawners...";
					this.lblTransferStatus.Refresh();
					this.progressBar1.Maximum = elementsByTagName.Count;
					this.tvwSpawnPoints.BeginUpdate();
					foreach (XmlElement xmlElement in elementsByTagName)
					{
						num++;
						this.progressBar1.Value = num;
						bool flag = false;
						if (ForceMap != WorldMap.Internal)
						{
							flag = true;
						}
						SpawnPointNode spawnPointNode = new SpawnPointNode(new SpawnPoint(xmlElement, ForceMap, flag));
						this.tvwSpawnPoints.Nodes.Add(spawnPointNode);
					}
					this.tvwSpawnPoints.Sorted = true;
					this.tvwSpawnPoints.EndUpdate();
					this.lblTotalSpawn.Text = string.Concat("Total Spawns = ", this.tvwSpawnPoints.Nodes.Count);
					this.txtName.Text = string.Concat(this._CfgDialog.CfgSpawnNameValue, this.tvwSpawnPoints.Nodes.Count);
					this.RefreshSpawnPoints();
				}
				else
				{
					MessageBox.Show(this, "Invalid XML root.  Probably not an XmlSpawner file.", "Read Failure", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					return;
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				string[] filePath = new string[] { "Failed to load file [", FilePath, "] for the following reason:", Environment.NewLine, this.ExceptionMessage(exception) };
				MessageBox.Show(this, string.Concat(filePath), "Load Failure", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
			this.progressBar1.Visible = false;
			this.lblTransferStatus.Visible = false;
			this.trkZoom.Visible = true;
			this.lblTrkMin.Visible = true;
			this.lblTrkMax.Visible = true;
			this.progressBar1.Refresh();
			this.lblTransferStatus.Refresh();
			this.trkZoom.Refresh();
			this.lblTrkMin.Refresh();
			this.lblTrkMax.Refresh();
		}

		private void LoadSpawnPacks()
		{
			this.ReadSpawnPacks(this.SpawnPackFile);
		}

		private void LoadTypes()
		{
			this.clbRunUOTypes.BeginUpdate();
			this.clbRunUOTypes.Sorted = false;
			this.clbRunUOTypes.Items.Clear();
			Type[] typeArray = this._RunUOScriptTypes;
			for (int i = 0; i < (int)typeArray.Length; i++)
			{
				Type type = typeArray[i];
				if (!type.IsAbstract && type.IsPublic && type.IsClass)
				{
					ConstructorInfo constructor = type.GetConstructor(Type.EmptyTypes);
					if (constructor != null)
					{
						object[] customAttributes = constructor.GetCustomAttributes(true);
						bool flag = false;
						object[] objArray = customAttributes;
						int num = 0;
						while (num < (int)objArray.Length)
						{
							if (string.Compare(((Attribute)objArray[num]).GetType().Name, "ConstructableAttribute", true) != 0)
							{
								num++;
							}
							else
							{
								flag = true;
								break;
							}
						}
						if (flag && (this.radShowAll.Checked || this.radShowItemsOnly.Checked) && type.BaseType != null && type.BaseType.FullName.StartsWith("Server.Item"))
						{
							this.clbRunUOTypes.Items.Add(type.Name);
						}
						if (flag && (this.radShowAll.Checked || this.radShowMobilesOnly.Checked) && type.BaseType != null && type.BaseType.FullName.StartsWith("Server.Mobile"))
						{
							this.clbRunUOTypes.Items.Add(type.Name);
						}
					}
				}
			}
			this.clbRunUOTypes.Sorted = true;
			this.clbRunUOTypes.EndUpdate();
			this.lblTotalTypesLoaded.Text = string.Concat("Types Loaded = ", this.clbRunUOTypes.Items.Count);
			this.SetSelectedSpawnTypes();
		}

		[STAThread]
		private static void Main(string[] args)
		{
			for (int i = 0; i < (int)args.Length; i++)
			{
				if (args[i].ToLower() == "-debug")
				{
					SpawnEditor._Debug = true;
				}
			}
			Application.ThreadException += new ThreadExceptionEventHandler(new SpawnEditor.CustomExceptionHandler().OnThreadException);
			Application.Run(new SpawnEditor());
		}

		private void mcnSpawnPack_Popup(object sender, EventArgs e)
		{
			if (this.mcnSpawnPack.SourceControl == this.clbSpawnPack)
			{
				foreach (MenuItem menuItem in this.mcnSpawnPack.MenuItems)
				{
					menuItem.Visible = false;
				}
				if (this.clbSpawnPack.SelectedItem is string)
				{
					this.mniDeleteType.Visible = true;
				}
				if (this.clbSpawnPack.Items.Count > 0)
				{
					this.mniDeleteAllTypes.Visible = true;
				}
			}
		}

		private void menuItem1_Click(object sender, EventArgs e)
		{
			if (this.SelectedSpawn == null || this.SelectedSpawn.SpawnObjects == null)
			{
				return;
			}
			string name = this.menuItem1.GetContextMenu().SourceControl.Name;
			int index = -1;
			if (name == "entryText1")
			{
				index = this.GetIndex(this.SelectedSpawn, 1);
			}
			else if (name == "entryText2")
			{
				index = this.GetIndex(this.SelectedSpawn, 2);
			}
			else if (name == "entryText3")
			{
				index = this.GetIndex(this.SelectedSpawn, 3);
			}
			else if (name == "entryText4")
			{
				index = this.GetIndex(this.SelectedSpawn, 4);
			}
			else if (name == "entryText5")
			{
				index = this.GetIndex(this.SelectedSpawn, 5);
			}
			else if (name == "entryText6")
			{
				index = this.GetIndex(this.SelectedSpawn, 6);
			}
			else if (name == "entryText7")
			{
				index = this.GetIndex(this.SelectedSpawn, 7);
			}
			else if (name == "entryText8")
			{
				index = this.GetIndex(this.SelectedSpawn, 8);
			}
			if (index >= 0 && index < this.SelectedSpawn.SpawnObjects.Count && MessageBox.Show(this, string.Concat("Are you sure you want to delete entry [", ((SpawnObject)this.SelectedSpawn.SpawnObjects[index]).TypeName, "] from the spawn?"), "Delete Spawn Object", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
			{
				this.SelectedSpawn.SpawnObjects.RemoveAt(index);
				this.DisplaySpawnEntries();
				this.UpdateSpawnNode();
			}
		}

		private void menuItem10_Click(object sender, EventArgs e)
		{
			try
			{
				this.importAllSpawnTypes.Title = "Import All Spawn Types";
				if (this.importAllSpawnTypes.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
				{
					this.ImportSpawnTypes(this.importAllSpawnTypes.FileName);
				}
			}
			catch
			{
			}
		}

		private void menuItem11_Click(object sender, EventArgs e)
		{
			try
			{
				this.exportAllSpawnTypes.Title = "Export All Spawn Types";
				if (this.exportAllSpawnTypes.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
				{
					this.ExportSpawnTypes(this.exportAllSpawnTypes.FileName);
				}
			}
			catch
			{
			}
		}

		private void menuItem12_Click(object sender, EventArgs e)
		{
			int num;
			int num1;
			try
			{
				this.importMapFile.Title = "Import from .map file";
				if (this.importMapFile.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
				{
					ImportMap importMap = new ImportMap(this);
					importMap.DoImportMap(this.importMapFile.FileName, out num, out num1);
					this.lblTotalSpawn.Text = string.Concat("Total Spawns = ", this.tvwSpawnPoints.Nodes.Count);
					this.checkSpawnFilter.Checked = false;
					this.RefreshSpawnPoints();
				}
			}
			catch
			{
			}
		}

		private void menuItem13_Click(object sender, EventArgs e)
		{
			try
			{
				this.importMSFFile.Title = "Import from .msf file";
				if (this.importMSFFile.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
				{
					(new ImportMSF(this)).DoImportMSF(this.importMSFFile.FileName);
					this.lblTotalSpawn.Text = string.Concat("Total Spawns = ", this.tvwSpawnPoints.Nodes.Count);
					this.checkSpawnFilter.Checked = false;
					this.RefreshSpawnPoints();
				}
			}
			catch
			{
			}
		}

		private void menuItem15_Click(object sender, EventArgs e)
		{
			if (this.SelectedSpawn == null || this.SelectedSpawn.SpawnObjects == null)
			{
				return;
			}
			string name = this.menuItem15.GetContextMenu().SourceControl.Name;
			int index = -1;
			if (name == "entryText1")
			{
				index = this.GetIndex(this.SelectedSpawn, 1);
			}
			else if (name == "entryText2")
			{
				index = this.GetIndex(this.SelectedSpawn, 2);
			}
			else if (name == "entryText3")
			{
				index = this.GetIndex(this.SelectedSpawn, 3);
			}
			else if (name == "entryText4")
			{
				index = this.GetIndex(this.SelectedSpawn, 4);
			}
			else if (name == "entryText5")
			{
				index = this.GetIndex(this.SelectedSpawn, 5);
			}
			else if (name == "entryText6")
			{
				index = this.GetIndex(this.SelectedSpawn, 6);
			}
			else if (name == "entryText7")
			{
				index = this.GetIndex(this.SelectedSpawn, 7);
			}
			else if (name == "entryText8")
			{
				index = this.GetIndex(this.SelectedSpawn, 8);
			}
			if (index >= 0 && index < this.SelectedSpawn.SpawnObjects.Count)
			{
				this.clbSpawnPack.Items.Add(((SpawnObject)this.SelectedSpawn.SpawnObjects[index]).TypeName);
			}
		}

		private void menuItem17_Click(object sender, EventArgs e)
		{
			this._TransferDialog.Show();
			this._TransferDialog.BringToFront();
		}

		private void menuItem18_Click(object sender, EventArgs e)
		{
			try
			{
				this.OpenHelp();
			}
			catch
			{
				MessageBox.Show("Unable to open help file.");
			}
		}

		private void menuItem2_Click(object sender, EventArgs e)
		{
			if (this.SelectedSpawn == null || this.SelectedSpawn.SpawnObjects == null)
			{
				return;
			}
			if (MessageBox.Show(this, string.Concat("Are you sure you want to delete all entries from spawn [", this.SelectedSpawn.SpawnName, "]?"), "Delete All Spawn Objects", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
			{
				this.SelectedSpawn.SpawnObjects.Clear();
				this.DisplaySpawnEntries();
				this.UpdateSpawnNode();
			}
		}

		private void menuItem22_Click(object sender, EventArgs e)
		{
		}

		private void menuItem4_Click(object sender, EventArgs e)
		{
			(new AboutForm(this)).Show();
		}

		private void menuItem6_Click(object sender, EventArgs e)
		{
			try
			{
				this.openSpawnPacks.Title = "Load SpawnPacks";
				if (this.openSpawnPacks.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
				{
					this.ReadSpawnPacks(this.openSpawnPacks.FileName);
				}
			}
			catch
			{
			}
		}

		private void menuItem7_Click(object sender, EventArgs e)
		{
			try
			{
				this.saveSpawnPacks.Title = "Save SpawnPacks";
				if (this.saveSpawnPacks.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
				{
					this.WriteSpawnPacks(this.saveSpawnPacks.FileName);
				}
			}
			catch
			{
			}
		}

		private void menuItem9_Click(object sender, EventArgs e)
		{
			this._CfgDialog.ShowDialog();
		}

		private void mncLoad_Popup(object sender, EventArgs e)
		{
		}

		private void mncSpawns_Popup(object sender, EventArgs e)
		{
			if (this.mncSpawns.SourceControl == this.tvwSpawnPoints)
			{
				foreach (MenuItem menuItem in this.mncSpawns.MenuItems)
				{
					menuItem.Visible = false;
				}
				if (this.tvwSpawnPoints.SelectedNode is SpawnPointNode)
				{
					this.mniDeleteSpawn.Visible = true;
				}
				else if (this.tvwSpawnPoints.SelectedNode is SpawnObjectNode)
				{
					this.mniDeleteSpawn.Visible = true;
				}
				if (this.tvwSpawnPoints.Nodes.Count > 0)
				{
					this.mniDeleteAllSpawns.Visible = true;
				}
			}
		}

		private void mniAlwaysOnTop_Click(object sender, EventArgs e)
		{
			if (!this.mniAlwaysOnTop.Checked)
			{
				this.mniAlwaysOnTop.Checked = true;
				base.TopMost = true;
				return;
			}
			this.mniAlwaysOnTop.Checked = false;
			base.TopMost = false;
		}

		private void mniDeleteAllFiltered_Click(object sender, EventArgs e)
		{
			ArrayList arrayLists = new ArrayList();
			int num = 0;
			foreach (SpawnPointNode node in this.tvwSpawnPoints.Nodes)
			{
				SpawnPoint spawn = node.Spawn;
				if (!node.Filtered)
				{
					continue;
				}
				arrayLists.Add(node);
				num++;
			}
			if (MessageBox.Show(this, string.Format("Delete {0} spawners?", num), "Delete Filtered Spawners", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.OK)
			{
				this.tvwSpawnPoints.BeginUpdate();
				this.tvwSpawnPoints.Sorted = false;
				foreach (SpawnPointNode arrayList in arrayLists)
				{
					this.tvwSpawnPoints.Nodes.Remove(arrayList);
				}
				this.tvwSpawnPoints.Sorted = true;
				this.tvwSpawnPoints.EndUpdate();
			}
			this.RefreshSpawnPoints();
		}

		private void mniDeleteAllSpawns_Click(object sender, EventArgs e)
		{
			TreeNode selectedNode = this.tvwSpawnPoints.SelectedNode;
			SpawnPointNode parent = selectedNode as SpawnPointNode;
			if (!(selectedNode is SpawnObjectNode))
			{
				this.DeleteAllSpawns();
			}
			else
			{
				parent = (SpawnPointNode)selectedNode.Parent;
				if (MessageBox.Show(this, string.Concat("Are you sure you want to delete all objects from spawn [", parent.Spawn.SpawnName, "]?"), "Delete All Spawn Objects", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
				{
					parent.Nodes.Clear();
					if (parent.Spawn.SpawnObjects != null)
					{
						parent.Spawn.SpawnObjects.Clear();
					}
				}
			}
			this.SetSelectedSpawnTypes();
			this.RefreshSpawnPoints();
		}

		private void mniDeleteAllTypes_Click(object sender, EventArgs e)
		{
			if (this.clbSpawnPack.SelectedItem is string && MessageBox.Show(this, string.Concat("Are you sure you want to delete all types in [", this.textSpawnPackName.Text, "]?"), "Delete All Types", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
			{
				this.clbSpawnPack.Items.Clear();
			}
		}

		private void mniDeleteAllUnfiltered_Click(object sender, EventArgs e)
		{
			ArrayList arrayLists = new ArrayList();
			int num = 0;
			foreach (SpawnPointNode node in this.tvwSpawnPoints.Nodes)
			{
				SpawnPoint spawn = node.Spawn;
				if (node.Filtered)
				{
					continue;
				}
				arrayLists.Add(node);
				num++;
			}
			if (MessageBox.Show(this, string.Format("Delete {0} spawners?", num), "Delete Unfiltered Spawners", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.OK)
			{
				this.tvwSpawnPoints.BeginUpdate();
				this.tvwSpawnPoints.Sorted = false;
				foreach (SpawnPointNode arrayList in arrayLists)
				{
					this.tvwSpawnPoints.Nodes.Remove(arrayList);
				}
				this.tvwSpawnPoints.Sorted = true;
				this.tvwSpawnPoints.EndUpdate();
			}
			this.RefreshSpawnPoints();
		}

		private void mniDeleteInSelectionWindow_Click(object sender, EventArgs e)
		{
			if (this._SelectionWindow == null)
			{
				return;
			}
			ArrayList arrayLists = new ArrayList();
			int num = 0;
			foreach (SpawnPointNode node in this.tvwSpawnPoints.Nodes)
			{
				SpawnPoint spawn = node.Spawn;
				if (node.Filtered || spawn.CentreX < this._SelectionWindow.X || spawn.CentreX > this._SelectionWindow.X + this._SelectionWindow.Width || spawn.CentreY < this._SelectionWindow.Y || spawn.CentreY > this._SelectionWindow.Y + this._SelectionWindow.Height)
				{
					continue;
				}
				arrayLists.Add(node);
				num++;
				node.Highlighted = true;
			}
			this.RefreshSpawnPoints();
			if (MessageBox.Show(this, string.Format("Delete {0} spawners?", num), "Delete Spawners", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.OK)
			{
				this.tvwSpawnPoints.BeginUpdate();
				this.tvwSpawnPoints.Sorted = false;
				foreach (SpawnPointNode arrayList in arrayLists)
				{
					this.tvwSpawnPoints.Nodes.Remove(arrayList);
				}
				this.tvwSpawnPoints.Sorted = true;
				this.tvwSpawnPoints.EndUpdate();
			}
			foreach (SpawnPointNode spawnPointNode in this.tvwSpawnPoints.Nodes)
			{
				spawnPointNode.Highlighted = false;
			}
			this.RefreshSpawnPoints();
		}

		private void mniDeleteNotSelected_Click(object sender, EventArgs e)
		{
			if (this._SelectionWindow == null)
			{
				return;
			}
			ArrayList arrayLists = new ArrayList();
			int num = 0;
			foreach (SpawnPointNode node in this.tvwSpawnPoints.Nodes)
			{
				SpawnPoint spawn = node.Spawn;
				if (node.Filtered || spawn.CentreX >= this._SelectionWindow.X && spawn.CentreX <= this._SelectionWindow.X + this._SelectionWindow.Width && spawn.CentreY >= this._SelectionWindow.Y && spawn.CentreY <= this._SelectionWindow.Y + this._SelectionWindow.Height)
				{
					continue;
				}
				arrayLists.Add(node);
				num++;
				node.Highlighted = true;
			}
			this.RefreshSpawnPoints();
			if (MessageBox.Show(this, string.Format("Delete {0} spawners?", num), "Delete Spawners", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.OK)
			{
				this.tvwSpawnPoints.BeginUpdate();
				this.tvwSpawnPoints.Sorted = false;
				foreach (SpawnPointNode arrayList in arrayLists)
				{
					this.tvwSpawnPoints.Nodes.Remove(arrayList);
				}
				this.tvwSpawnPoints.Sorted = true;
				this.tvwSpawnPoints.EndUpdate();
			}
			foreach (SpawnPointNode spawnPointNode in this.tvwSpawnPoints.Nodes)
			{
				spawnPointNode.Highlighted = false;
			}
			this.RefreshSpawnPoints();
		}

		private void mniDeletePack_Click(object sender, EventArgs e)
		{
			TreeNode selectedNode = this.tvwSpawnPacks.SelectedNode;
			SpawnPackNode parent = selectedNode as SpawnPackNode;
			if (selectedNode is SpawnPackSubNode)
			{
				parent = selectedNode.Parent as SpawnPackNode;
			}
			if (parent != null && MessageBox.Show(this, string.Concat("Are you sure you want to remove SpawnPack [", parent.PackName, "] ?"), "Remove SpawnPack", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
			{
				parent.Remove();
			}
		}

		private void mniDeleteSpawn_Click(object sender, EventArgs e)
		{
			TreeNode selectedNode = this.tvwSpawnPoints.SelectedNode;
			if (selectedNode is SpawnPointNode)
			{
				SpawnPointNode spawnPointNode = (SpawnPointNode)selectedNode;
				if (MessageBox.Show(this, string.Concat("Are you sure you want to delete spawn [", spawnPointNode.Spawn.SpawnName, "] from the list?"), "Delete Spawn", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
				{
					spawnPointNode.Remove();
					this.SelectedSpawn = null;
					this.LoadDefaultSpawnValues();
				}
			}
			else if (selectedNode is SpawnObjectNode)
			{
				SpawnObjectNode spawnObjectNode = (SpawnObjectNode)selectedNode;
				if (MessageBox.Show(this, string.Concat("Are you sure you want to delete object [", spawnObjectNode.SpawnObject.TypeName, "] from the spawn?"), "Delete Spawn Object", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
				{
					((SpawnPointNode)spawnObjectNode.Parent).Spawn.SpawnObjects.Remove(spawnObjectNode.SpawnObject);
					spawnObjectNode.Remove();
				}
			}
			this.SetSelectedSpawnTypes();
			this.RefreshSpawnPoints();
		}

		private void mniDeleteType_Click(object sender, EventArgs e)
		{
			if (this.clbSpawnPack.SelectedItem is string)
			{
				int selectedIndex = this.clbSpawnPack.SelectedIndex;
				if (selectedIndex >= 0 && MessageBox.Show(this, string.Concat("Are you sure you want to delete type [", this.clbSpawnPack.SelectedItem, "] from the list?"), "Delete Type", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
				{
					this.clbSpawnPack.Items.RemoveAt(selectedIndex);
				}
			}
		}

		private void mniDisplayFilterSettings_Click(object sender, EventArgs e)
		{
			this._SpawnerFilters.Show();
			this._SpawnerFilters.BringToFront();
		}

		private void mniForceLoad_Click(object sender, EventArgs e)
		{
			try
			{
				WorldMap selectedItem = (WorldMap)((int)((WorldMap)this.cbxMap.SelectedItem));
				this.ofdLoadFile.Title = string.Concat("Force Load Spawn File Into ", selectedItem.ToString());
				if (this.ofdLoadFile.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
				{
					this.Refresh();
					this.stbMain.Text = string.Format("Loading {0} into {1}...", this.ofdLoadFile.FileName, selectedItem.ToString());
					this.tvwSpawnPoints.Nodes.Clear();
					this.LoadSpawnFile(this.ofdLoadFile.FileName, selectedItem);
				}
			}
			finally
			{
				this.stbMain.Text = "Finished loading spawn file.";
			}
		}

		private void mniForceMerge_Click(object sender, EventArgs e)
		{
			try
			{
				WorldMap selectedItem = (WorldMap)((int)((WorldMap)this.cbxMap.SelectedItem));
				this.ofdLoadFile.Title = string.Concat("Merge Spawn File Into ", selectedItem.ToString());
				if (this.ofdLoadFile.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
				{
					this.Refresh();
					this.stbMain.Text = string.Format("Merging {0} into {1}...", this.ofdLoadFile.FileName, selectedItem.ToString());
					this.LoadSpawnFile(this.ofdLoadFile.FileName, selectedItem);
				}
			}
			finally
			{
				this.stbMain.Text = "Finished merging spawn file.";
			}
		}

		private void mniModifiedUnfiltered_Click(object sender, EventArgs e)
		{
			ArrayList arrayLists = new ArrayList();
			int num = 0;
			foreach (SpawnPointNode node in this.tvwSpawnPoints.Nodes)
			{
				SpawnPoint spawn = node.Spawn;
				if (node.Filtered)
				{
					continue;
				}
				arrayLists.Add(node);
				num++;
				node.Highlighted = true;
			}
			this.RefreshSpawnPoints();
			if (MessageBox.Show(this, string.Format("Modify {0} spawners?", num), "Modify Spawners", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.OK)
			{
				this.tvwSpawnPoints.BeginUpdate();
				this.tvwSpawnPoints.Sorted = false;
				foreach (SpawnPointNode arrayList in arrayLists)
				{
					this.ApplyModifications(arrayList.Spawn);
				}
				this.tvwSpawnPoints.Sorted = true;
				this.tvwSpawnPoints.EndUpdate();
			}
			foreach (SpawnPointNode spawnPointNode in this.tvwSpawnPoints.Nodes)
			{
				spawnPointNode.Highlighted = false;
			}
			this.RefreshSpawnPoints();
		}

		private void mniModifyInSelectionWindow_Click(object sender, EventArgs e)
		{
			if (this._SelectionWindow == null)
			{
				return;
			}
			ArrayList arrayLists = new ArrayList();
			int num = 0;
			foreach (SpawnPointNode node in this.tvwSpawnPoints.Nodes)
			{
				SpawnPoint spawn = node.Spawn;
				if (node.Filtered || spawn.CentreX < this._SelectionWindow.X || spawn.CentreX > this._SelectionWindow.X + this._SelectionWindow.Width || spawn.CentreY < this._SelectionWindow.Y || spawn.CentreY > this._SelectionWindow.Y + this._SelectionWindow.Height)
				{
					continue;
				}
				arrayLists.Add(node);
				num++;
				node.Highlighted = true;
			}
			this.RefreshSpawnPoints();
			if (MessageBox.Show(this, string.Format("Modify {0} spawners?", num), "Modify Spawners", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.OK)
			{
				this.tvwSpawnPoints.BeginUpdate();
				this.tvwSpawnPoints.Sorted = false;
				foreach (SpawnPointNode arrayList in arrayLists)
				{
					this.ApplyModifications(arrayList.Spawn);
				}
				this.tvwSpawnPoints.Sorted = true;
				this.tvwSpawnPoints.EndUpdate();
			}
			foreach (SpawnPointNode spawnPointNode in this.tvwSpawnPoints.Nodes)
			{
				spawnPointNode.Highlighted = false;
			}
			this.RefreshSpawnPoints();
		}

		private void mniSetSpawnAmount_Click(object sender, EventArgs e)
		{
			SpawnObjectNode selectedNode = this.tvwSpawnPoints.SelectedNode as SpawnObjectNode;
			if (selectedNode != null)
			{
				Amount amount = new Amount(selectedNode.SpawnObject.TypeName, selectedNode.SpawnObject.Count);
				if (amount.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
				{
					selectedNode.SpawnObject.Count = amount.SpawnAmount;
					selectedNode.UpdateNode();
				}
			}
		}

		private void mniToolbarDeleteAllSpawns_Click(object sender, EventArgs e)
		{
			this.DeleteAllSpawns();
		}

		private void mniUnloadSingleSpawner_Click_1(object sender, EventArgs e)
		{
			if (this.tvwSpawnPoints.Nodes == null || this.tvwSpawnPoints.Nodes.Count <= 0)
			{
				return;
			}
			this.DoUnloadSpawners(this.SelectedSpawn);
		}

		private void mniUnloadSpawners_Click(object sender, EventArgs e)
		{
			if (this.tvwSpawnPoints.Nodes == null || this.tvwSpawnPoints.Nodes.Count <= 0)
			{
				return;
			}
			this.DoUnloadSpawners(null);
		}

		private void numericUpDown1_ValueChanged(object sender, EventArgs e)
		{
			this.DisplaySpawnEntries();
		}

		private void numericUpDown10_ValueChanged(object sender, EventArgs e)
		{
		}

		private void numericUpDown6_ValueChanged(object sender, EventArgs e)
		{
		}

		protected override void OnClosing(CancelEventArgs e)
		{
			this.Tracking = false;
			base.OnClosing(e);
		}

		private void OpenHelp()
		{
			Process.Start(string.Concat("file://", Path.Combine(this.StartingDirectory, this.HelpFile)));
		}

		private int RandomColor(int val)
		{
			Random random = new Random(339);
			int num = 0;
			for (int i = 0; i < val; i++)
			{
				num = random.Next(16777215);
			}
			return num;
		}

		public void ReadSpawnPacks(string filePath)
		{
			if (filePath == null || filePath.Length == 0)
			{
				return;
			}
			if (File.Exists(filePath))
			{
				try
				{
					XmlDocument xmlDocument = new XmlDocument();
					xmlDocument.Load(filePath);
					XmlElement item = xmlDocument["SpawnPacks"];
					if (item != null)
					{
						foreach (XmlElement elementsByTagName in item.GetElementsByTagName("Pack"))
						{
							string value = elementsByTagName.Attributes.GetNamedItem("name").Value;
							ArrayList arrayLists = new ArrayList();
							foreach (XmlElement xmlElement in elementsByTagName.GetElementsByTagName("T"))
							{
								arrayLists.Add(xmlElement.InnerText);
							}
							this.tvwSpawnPacks.Nodes.Add(new SpawnPackNode(value, arrayLists));
						}
					}
				}
				catch (Exception exception1)
				{
					Exception exception = exception1;
					string[] strArrays = new string[] { "Failed to read SpawnPack file [", filePath, "] for the following reason:", Environment.NewLine, this.ExceptionMessage(exception) };
					MessageBox.Show(this, string.Concat(strArrays), "Read Failure", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
			}
		}

		private void RefreshRegionView()
		{
			foreach (RegionFacetNode node in this.treeRegionView.Nodes)
			{
				foreach (RegionNode regionNode in node.Nodes)
				{
					SpawnEditor2.Region region = regionNode.Region;
					if (!regionNode.Checked || !(region != null) || (int)region.Map != (int)((WorldMap)this.cbxMap.SelectedItem))
					{
						continue;
					}
					foreach (Rectangle coord in region.Coords)
					{
						this.axUOMap.AddDrawRect((short)coord.X, (short)coord.Y, (short)coord.Width, (short)coord.Height, 1, 32512);
					}
				}
			}
		}

		internal void RefreshSpawnPoints()
		{
			short centreX;
			short centreY;
			short spawnHomeRange;
			short height;
			short x;
			short y;
			short width;
			short num;
			Rectangle bounds;
			this.axUOMap.RemoveDrawRects();
			this.axUOMap.RemoveDrawObjects();
			this.RefreshRegionView();
			this.DisplayMyLocation();
			if (this.MobLocArray != null && this._TransferDialog.chkShowCreatures.Checked)
			{
				short value = (short)(5 + (short)this.trkZoom.Value);
				for (int i = 0; i < (int)this.MobLocArray.Length; i++)
				{
					short x1 = (short)this.MobLocArray[i].X;
					short y1 = (short)this.MobLocArray[i].Y;
					if ((int)((WorldMap)this.cbxMap.SelectedItem) == this.MobLocArray[i].Map)
					{
						this.axUOMap.AddDrawObject(x1, y1, 1, value, 16776960);
					}
				}
			}
			if (this.PlayerLocArray != null && this._TransferDialog.chkShowPlayers.Checked)
			{
				short value1 = (short)(5 + (short)this.trkZoom.Value);
				for (int j = 0; j < (int)this.PlayerLocArray.Length; j++)
				{
					short num1 = (short)this.PlayerLocArray[j].X;
					short y2 = (short)this.PlayerLocArray[j].Y;
					if ((int)((WorldMap)this.cbxMap.SelectedItem) == this.PlayerLocArray[j].Map)
					{
						this.axUOMap.AddDrawObject(num1, y2, 2, value1, 65535);
					}
				}
			}
			if (this.ItemLocArray != null && this._TransferDialog.chkShowItems.Checked)
			{
				short value2 = (short)(5 + (short)this.trkZoom.Value);
				for (int k = 0; k < (int)this.ItemLocArray.Length; k++)
				{
					short x2 = (short)this.ItemLocArray[k].X;
					short num2 = (short)this.ItemLocArray[k].Y;
					if ((int)((WorldMap)this.cbxMap.SelectedItem) == this.ItemLocArray[k].Map)
					{
						this.axUOMap.AddDrawObject(x2, num2, 1, value2, 65280);
					}
				}
			}
			bool flag = false;
			int num3 = 0;
			foreach (SpawnPointNode node in this.tvwSpawnPoints.Nodes)
			{
				if (node.Spawn.IsSelected || node.Highlighted)
				{
					if (!node.Filtered)
					{
						node.ForeColor = this.tvwSpawnPoints.ForeColor;
						num3++;
					}
					else
					{
						node.ForeColor = Color.LightGray;
					}
					if ((int)node.Spawn.Map == (int)((WorldMap)this.cbxMap.SelectedItem) && this.chkShowSpawns.Checked)
					{
						if (this.chkShade.Checked && this.cbxShade.SelectedIndex == 0)
						{
							if (!node.Spawn.SpawnHomeRangeIsRelative)
							{
								centreX = (short)(node.Spawn.CentreX - node.Spawn.SpawnHomeRange);
								centreY = (short)(node.Spawn.CentreY - node.Spawn.SpawnHomeRange);
								spawnHomeRange = (short)(node.Spawn.SpawnHomeRange * 2);
								height = (short)(node.Spawn.SpawnHomeRange * 2);
							}
							else
							{
								bounds = node.Spawn.Bounds;
								centreX = (short)(bounds.X - node.Spawn.SpawnHomeRange);
								bounds = node.Spawn.Bounds;
								centreY = (short)(bounds.Y - node.Spawn.SpawnHomeRange);
								bounds = node.Spawn.Bounds;
								spawnHomeRange = (short)(bounds.Width + 2 * node.Spawn.SpawnHomeRange);
								bounds = node.Spawn.Bounds;
								height = (short)(bounds.Height + 2 * node.Spawn.SpawnHomeRange);
							}
							this.axUOMap.AddDrawRect(centreX, centreY, spawnHomeRange, height, 1, SpawnEditor.ComputeDensityColor(node.Spawn));
						}
						else if (this.chkShade.Checked && this.cbxShade.SelectedIndex == 1)
						{
							bounds = node.Spawn.Bounds;
							short x3 = (short)bounds.X;
							bounds = node.Spawn.Bounds;
							short y3 = (short)bounds.Y;
							bounds = node.Spawn.Bounds;
							short width1 = (short)bounds.Width;
							bounds = node.Spawn.Bounds;
							short height1 = (short)bounds.Height;
							this.axUOMap.AddDrawRect(x3, y3, width1, height1, 1, SpawnEditor.ComputeSpeedColor(node.Spawn));
						}
						SpawnPoint spawn = node.Spawn;
						AxUOMap axUOMap = this.axUOMap;
						bounds = node.Spawn.Bounds;
						short x4 = (short)bounds.X;
						bounds = node.Spawn.Bounds;
						short y4 = (short)bounds.Y;
						bounds = node.Spawn.Bounds;
						short width2 = (short)bounds.Width;
						bounds = node.Spawn.Bounds;
						spawn.Index = axUOMap.AddDrawRect(x4, y4, width2, (short)bounds.Height, 2, 16776960);
						short value3 = (short)(7 + (short)this.trkZoom.Value);
						if (!node.Spawn.SpawnInContainer)
						{
							this.axUOMap.AddDrawObject(node.Spawn.CentreX, node.Spawn.CentreY, 3, value3, 16711680);
						}
						else
						{
							this.axUOMap.AddDrawObject(node.Spawn.CentreX, node.Spawn.CentreY, 6, value3, 16711935);
						}
					}
					flag = true;
					if (this.tvwSpawnPoints.SelectedNode != null && (this.tvwSpawnPoints.SelectedNode.Parent == null || this.tvwSpawnPoints.SelectedNode.Parent != node))
					{
						this.tvwSpawnPoints.SelectedNode = node;
						node.BackColor = Color.Yellow;
						node.EnsureVisible();
					}
					this.SelectedSpawn = node.Spawn;
				}
				else
				{
					node.BackColor = this.tvwSpawnPoints.BackColor;
					if (!node.Filtered)
					{
						node.ForeColor = this.tvwSpawnPoints.ForeColor;
						num3++;
						if ((int)node.Spawn.Map != (int)((WorldMap)this.cbxMap.SelectedItem) || !this.chkShowSpawns.Checked)
						{
							continue;
						}
						if (this.chkShade.Checked && this.cbxShade.SelectedIndex == 0)
						{
							if (!node.Spawn.SpawnHomeRangeIsRelative)
							{
								x = (short)(node.Spawn.CentreX - node.Spawn.SpawnHomeRange);
								y = (short)(node.Spawn.CentreY - node.Spawn.SpawnHomeRange);
								width = (short)(node.Spawn.SpawnHomeRange * 2);
								num = (short)(node.Spawn.SpawnHomeRange * 2);
							}
							else
							{
								bounds = node.Spawn.Bounds;
								x = (short)(bounds.X - node.Spawn.SpawnHomeRange);
								bounds = node.Spawn.Bounds;
								y = (short)(bounds.Y - node.Spawn.SpawnHomeRange);
								bounds = node.Spawn.Bounds;
								width = (short)(bounds.Width + 2 * node.Spawn.SpawnHomeRange);
								bounds = node.Spawn.Bounds;
								num = (short)(bounds.Height + 2 * node.Spawn.SpawnHomeRange);
							}
							this.axUOMap.AddDrawRect(x, y, width, num, 1, SpawnEditor.ComputeDensityColor(node.Spawn));
						}
						else if (this.chkShade.Checked && this.cbxShade.SelectedIndex == 1)
						{
							bounds = node.Spawn.Bounds;
							short num4 = (short)bounds.X;
							bounds = node.Spawn.Bounds;
							short y5 = (short)bounds.Y;
							bounds = node.Spawn.Bounds;
							short width3 = (short)bounds.Width;
							bounds = node.Spawn.Bounds;
							short height2 = (short)bounds.Height;
							this.axUOMap.AddDrawRect(num4, y5, width3, height2, 1, SpawnEditor.ComputeSpeedColor(node.Spawn));
						}
						SpawnPoint spawnPoint = node.Spawn;
						AxUOMap axUOMap1 = this.axUOMap;
						bounds = node.Spawn.Bounds;
						short x5 = (short)bounds.X;
						bounds = node.Spawn.Bounds;
						short num5 = (short)bounds.Y;
						bounds = node.Spawn.Bounds;
						short width4 = (short)bounds.Width;
						bounds = node.Spawn.Bounds;
						spawnPoint.Index = axUOMap1.AddDrawRect(x5, num5, width4, (short)bounds.Height, 2, 255);
					}
					else
					{
						node.ForeColor = Color.LightGray;
					}
				}
			}
			this.lblTotalSpawn.Text = string.Concat("Total Spawns = ", num3);
			this.DisplaySpawnEntries();
			if (!flag)
			{
				this.btnUpdateSpawn.Enabled = false;
				this.btnUpdateFromSpawnPack.Enabled = false;
				this.btnDeleteSpawn.Enabled = false;
				this.btnSendSingleSpawner.Enabled = false;
				this.btnMove.Enabled = false;
			}
			else
			{
				this.btnUpdateSpawn.Enabled = true;
				this.btnUpdateFromSpawnPack.Enabled = true;
				this.btnDeleteSpawn.Enabled = true;
				this.btnSendSingleSpawner.Enabled = true;
				this.btnMove.Enabled = true;
			}
			if (this._SelectionWindow != null)
			{
				this._SelectionWindow.Index = this.axUOMap.AddDrawRect(this._SelectionWindow.X, this._SelectionWindow.Y, this._SelectionWindow.Width, this._SelectionWindow.Height, 2, 16777215);
			}
			this.axUOMap.Refresh();
		}

		internal void SaveSpawnFile(string FilePath)
		{
			FileStream fileStream = null;
			try
			{
				fileStream = File.Open(FilePath, FileMode.Create, FileAccess.Write);
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				string[] filePath = new string[] { "Failed to create file [", FilePath, "] for the following reason:", Environment.NewLine, this.ExceptionMessage(exception) };
				MessageBox.Show(this, string.Concat(filePath), "Save Failure", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return;
			}
			if (fileStream != null)
			{
				this.SaveSpawnFile(fileStream, FilePath, null);
				try
				{
					fileStream.Close();
				}
				catch
				{
				}
				return;
			}
			MessageBox.Show(this, string.Concat("Could not save file [", FilePath, "]"), "Save Failure", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}

		private void SaveSpawnFile(Stream fs, string FilePath, SpawnPoint selectedspawn)
		{
			try
			{
				DataSet dataSet = new DataSet("Spawns");
				dataSet.Tables.Add("Points");
				dataSet.Tables["Points"].Columns.Add("Name");
				dataSet.Tables["Points"].Columns.Add("UniqueId");
				dataSet.Tables["Points"].Columns.Add("Map");
				dataSet.Tables["Points"].Columns.Add("X");
				dataSet.Tables["Points"].Columns.Add("Y");
				dataSet.Tables["Points"].Columns.Add("Width");
				dataSet.Tables["Points"].Columns.Add("Height");
				dataSet.Tables["Points"].Columns.Add("CentreX");
				dataSet.Tables["Points"].Columns.Add("CentreY");
				dataSet.Tables["Points"].Columns.Add("CentreZ");
				dataSet.Tables["Points"].Columns.Add("Range");
				dataSet.Tables["Points"].Columns.Add("MaxCount");
				dataSet.Tables["Points"].Columns.Add("MinDelay");
				dataSet.Tables["Points"].Columns.Add("MaxDelay");
				dataSet.Tables["Points"].Columns.Add("Team");
				dataSet.Tables["Points"].Columns.Add("IsGroup");
				dataSet.Tables["Points"].Columns.Add("IsRunning");
				dataSet.Tables["Points"].Columns.Add("IsHomeRangeRelative");
				dataSet.Tables["Points"].Columns.Add("DelayInSec");
				dataSet.Tables["Points"].Columns.Add("Duration");
				dataSet.Tables["Points"].Columns.Add("DespawnTime");
				dataSet.Tables["Points"].Columns.Add("ProximityRange");
				dataSet.Tables["Points"].Columns.Add("ProximityTriggerSound");
				dataSet.Tables["Points"].Columns.Add("ProximityTriggerMessage");
				dataSet.Tables["Points"].Columns.Add("ObjectPropertyName");
				dataSet.Tables["Points"].Columns.Add("ObjectPropertyItemName");
				dataSet.Tables["Points"].Columns.Add("SetPropertyItemName");
				dataSet.Tables["Points"].Columns.Add("ItemTriggerName");
				dataSet.Tables["Points"].Columns.Add("NoItemTriggerName");
				dataSet.Tables["Points"].Columns.Add("MobTriggerName");
				dataSet.Tables["Points"].Columns.Add("MobPropertyName");
				dataSet.Tables["Points"].Columns.Add("PlayerPropertyName");
				dataSet.Tables["Points"].Columns.Add("TriggerProbability");
				dataSet.Tables["Points"].Columns.Add("SpeechTrigger");
				dataSet.Tables["Points"].Columns.Add("SkillTrigger");
				dataSet.Tables["Points"].Columns.Add("InContainer");
				dataSet.Tables["Points"].Columns.Add("ContainerX");
				dataSet.Tables["Points"].Columns.Add("ContainerY");
				dataSet.Tables["Points"].Columns.Add("ContainerZ");
				dataSet.Tables["Points"].Columns.Add("MinRefractory");
				dataSet.Tables["Points"].Columns.Add("MaxRefractory");
				dataSet.Tables["Points"].Columns.Add("TODStart");
				dataSet.Tables["Points"].Columns.Add("TODEnd");
				dataSet.Tables["Points"].Columns.Add("TODMode");
				dataSet.Tables["Points"].Columns.Add("KillReset");
				dataSet.Tables["Points"].Columns.Add("ExternalTriggering");
				dataSet.Tables["Points"].Columns.Add("SequentialSpawning");
				dataSet.Tables["Points"].Columns.Add("RegionName");
				dataSet.Tables["Points"].Columns.Add("AllowGhostTriggering");
				dataSet.Tables["Points"].Columns.Add("SpawnOnTrigger");
				dataSet.Tables["Points"].Columns.Add("ConfigFile");
				dataSet.Tables["Points"].Columns.Add("SmartSpawning");
				dataSet.Tables["Points"].Columns.Add("WayPoint");
				dataSet.Tables["Points"].Columns.Add("Amount");
				dataSet.Tables["Points"].Columns.Add("Notes");
				dataSet.Tables["Points"].Columns.Add("Objects2");
				foreach (SpawnPointNode node in this.tvwSpawnPoints.Nodes)
				{
					if (node.Filtered && selectedspawn == null)
					{
						continue;
					}
					SpawnPoint spawn = node.Spawn;
					if (selectedspawn != null && spawn != selectedspawn)
					{
						continue;
					}
					DataRow spawnName = dataSet.Tables["Points"].NewRow();
					spawnName["Name"] = spawn.SpawnName;
					spawnName["UniqueId"] = spawn.UnqiueId.ToString();
					spawnName["Map"] = spawn.Map.ToString();
					Rectangle bounds = spawn.Bounds;
					spawnName["X"] = bounds.X;
					bounds = spawn.Bounds;
					spawnName["Y"] = bounds.Y;
					bounds = spawn.Bounds;
					spawnName["Width"] = bounds.Width;
					bounds = spawn.Bounds;
					spawnName["Height"] = bounds.Height;
					spawnName["CentreX"] = spawn.CentreX;
					spawnName["CentreY"] = spawn.CentreY;
					spawnName["CentreZ"] = spawn.CentreZ;
					spawnName["Range"] = spawn.SpawnHomeRange;
					spawnName["MaxCount"] = spawn.SpawnMaxCount;
					spawnName["MinDelay"] = (int)(spawn.SpawnMinDelay * 60);
					spawnName["MaxDelay"] = (int)(spawn.SpawnMaxDelay * 60);
					spawnName["Team"] = spawn.SpawnTeam;
					spawnName["IsGroup"] = spawn.SpawnIsGroup;
					spawnName["IsRunning"] = spawn.SpawnIsRunning;
					spawnName["IsHomeRangeRelative"] = spawn.SpawnHomeRangeIsRelative;
					spawnName["DelayInSec"] = true;
					spawnName["Duration"] = spawn.SpawnDuration;
					spawnName["DespawnTime"] = spawn.SpawnDespawn;
					spawnName["ProximityRange"] = spawn.SpawnProximityRange;
					spawnName["ProximityTriggerSound"] = spawn.SpawnProximitySnd;
					spawnName["ProximityTriggerMessage"] = spawn.SpawnProximityMsg;
					spawnName["ObjectPropertyName"] = spawn.SpawnTrigObjectProp;
					spawnName["ObjectPropertyItemName"] = spawn.SpawnObjectPropertyItemName;
					spawnName["SetPropertyItemName"] = spawn.SpawnSetPropertyItemName;
					spawnName["ItemTriggerName"] = spawn.SpawnTriggerOnCarried;
					spawnName["NoItemTriggerName"] = spawn.SpawnNoTriggerOnCarried;
					spawnName["MobTriggerName"] = spawn.SpawnMobTriggerName;
					spawnName["MobPropertyName"] = spawn.SpawnMobTrigProp;
					spawnName["PlayerPropertyName"] = spawn.SpawnPlayerTrigProp;
					spawnName["TriggerProbability"] = spawn.SpawnTriggerProbability;
					spawnName["SpeechTrigger"] = spawn.SpawnSpeechTrigger;
					spawnName["SkillTrigger"] = spawn.SpawnSkillTrigger;
					spawnName["InContainer"] = spawn.SpawnInContainer;
					if (spawn.SpawnInContainer)
					{
						spawnName["ContainerX"] = spawn.SpawnContainerX;
						spawnName["ContainerY"] = spawn.SpawnContainerY;
						spawnName["ContainerZ"] = spawn.SpawnContainerZ;
					}
					spawnName["MinRefractory"] = spawn.SpawnMinRefract;
					spawnName["MaxRefractory"] = spawn.SpawnMaxRefract;
					spawnName["TODStart"] = spawn.SpawnTODStart * 60;
					spawnName["TODEnd"] = spawn.SpawnTODEnd * 60;
					spawnName["TODMode"] = spawn.SpawnTODMode;
					spawnName["KillReset"] = spawn.SpawnKillReset;
					spawnName["ExternalTriggering"] = spawn.SpawnExternalTriggering;
					spawnName["SequentialSpawning"] = spawn.SpawnSequentialSpawn;
					spawnName["RegionName"] = spawn.SpawnRegionName;
					spawnName["AllowGhostTriggering"] = spawn.SpawnAllowGhost;
					spawnName["SpawnOnTrigger"] = spawn.SpawnSpawnOnTrigger;
					spawnName["ConfigFile"] = spawn.SpawnConfigFile;
					spawnName["SmartSpawning"] = spawn.SpawnSmartSpawning;
					spawnName["WayPoint"] = spawn.SpawnWaypoint;
					spawnName["Amount"] = spawn.SpawnStackAmount;
					if (spawn.SpawnNotes != null && spawn.SpawnNotes.Trim().Length > 0)
					{
						spawnName["Notes"] = spawn.SpawnNotes;
					}
					spawnName["Objects2"] = spawn.GetSerializedObjectList2();
					dataSet.Tables["Points"].Rows.Add(spawnName);
				}
				dataSet.WriteXml(fs);
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				string[] filePath = new string[] { "Failed to save file [", FilePath, "] for the following reason:", Environment.NewLine, this.ExceptionMessage(exception) };
				MessageBox.Show(this, string.Concat(filePath), "Save Failure", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}

		public void SendAuthCommand(Guid id)
		{
			int num = SpawnEditor.FindWindow(this._CfgDialog.CfgUoClientWindowValue, null);
			if (num <= 0)
			{
				MessageBox.Show(string.Format("{0} could not be found. Make sure the client is started and that the 'Client Window' option in Setup is correct.", string.Concat(this._CfgDialog.CfgUoClientWindowValue, " Not Found"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation));
				return;
			}
			string str = string.Format("{0}XTS auth {1}", this._CfgDialog.CfgRunUoCmdPrefix, id.ToString());
			SpawnEditor.SendMessage(num, 258, 13, 0);
			for (int i = 0; i < str.Length; i++)
			{
				SpawnEditor.SendMessage(num, 256, 69, 1);
				SpawnEditor.SendMessage(num, 258, str[i], 1);
				SpawnEditor.SendMessage(num, 257, 69, 1);
			}
			SpawnEditor.SendMessage(num, 258, 13, 0);
		}

		public void SendGoCommand(SpawnPoint Spawn)
		{
			short centreX = Spawn.CentreX;
			short centreY = Spawn.CentreY;
			short centreZ = Spawn.CentreZ;
			if (this.chkSnapRegion.Checked)
			{
				int x = Spawn.Bounds.X;
				Rectangle bounds = Spawn.Bounds;
				centreX = (short)(x + bounds.Width / 2);
				int y = Spawn.Bounds.Y;
				bounds = Spawn.Bounds;
				centreY = (short)(y + bounds.Height / 2);
				centreZ = -32768;
			}
			if (this.chkSyncUO.Checked)
			{
				this.SendGoCommand(centreX, centreY, centreZ, Spawn.Map);
			}
			this.AssignCenter(centreX, centreY, (short)Spawn.Map);
		}

		public void SendGoCommand(short X, short Y, short Z, WorldMap Map)
		{
			object[] cfgRunUoCmdPrefix;
			int num = SpawnEditor.FindWindow(this._CfgDialog.CfgUoClientWindowValue, null);
			if (num <= 0)
			{
				MessageBox.Show(string.Format("{0} could not be found. Make sure the client is started and that the 'Client Window' option in Setup is correct.", string.Concat(this._CfgDialog.CfgUoClientWindowValue, " Not Found"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation));
				this.chkSyncUO.Checked = false;
				return;
			}
			string empty = string.Empty;
			if (Z != -32768)
			{
				cfgRunUoCmdPrefix = new object[] { this._CfgDialog.CfgRunUoCmdPrefix, Map, X, Y, Z };
				empty = string.Format("{0}XmlGo {1} {2} {3} {4}", cfgRunUoCmdPrefix);
			}
			else
			{
				cfgRunUoCmdPrefix = new object[] { this._CfgDialog.CfgRunUoCmdPrefix, Map, X, Y };
				empty = string.Format("{0}XmlGo {1} {2} {3}", cfgRunUoCmdPrefix);
			}
			SpawnEditor.SendMessage(num, 258, 13, 0);
			for (int i = 0; i < empty.Length; i++)
			{
				SpawnEditor.SendMessage(num, 256, 69, 1);
				SpawnEditor.SendMessage(num, 258, empty[i], 1);
				SpawnEditor.SendMessage(num, 257, 69, 1);
			}
			SpawnEditor.SendMessage(num, 258, 13, 0);
			this.MyLocation.X = X;
			this.MyLocation.Y = Y;
			this.MyLocation.Z = Z;
			this.MyLocation.Facet = (int)Map;
		}

		[DllImport("User32.dll", CharSet=CharSet.None, EntryPoint="SendMessageA", ExactSpelling=false)]
		public static extern int SendMessage(int _WindowHandler, int _WM_USER, int _data, int _id);

		[DllImport("User32.dll", CharSet=CharSet.None, ExactSpelling=false)]
		public static extern bool SetForegroundWindow(int hWnd);

		private void SetSelectedSpawnTypes()
		{
			if (this.tvwSpawnPoints.SelectedNode == null)
			{
				this.clbRunUOTypes.ClearSelected();
				for (int i = 0; i < this.clbRunUOTypes.Items.Count; i++)
				{
					this.clbRunUOTypes.SetItemChecked(i, false);
				}
				return;
			}
			this.SelectedSpawnNode = this.tvwSpawnPoints.SelectedNode as SpawnPointNode;
			SpawnObjectNode selectedNode = this.tvwSpawnPoints.SelectedNode as SpawnObjectNode;
			if (selectedNode != null)
			{
				this.SelectedSpawnNode = (SpawnPointNode)selectedNode.Parent;
			}
			this.clbRunUOTypes.ClearSelected();
			for (int j = 0; j < this.clbRunUOTypes.Items.Count; j++)
			{
				bool flag = false;
				foreach (SpawnObject spawnObject in this.SelectedSpawnNode.Spawn.SpawnObjects)
				{
					if (spawnObject.TypeName.ToUpper() != this.clbRunUOTypes.Items[j].ToString().ToUpper())
					{
						continue;
					}
					flag = true;
					break;
				}
				this.clbRunUOTypes.SetItemChecked(j, flag);
			}
		}

		private void SetSpawn(SpawnPointNode SpawnNode, bool IsUpdate)
		{
			this.UpdateSpawnDetails(SpawnNode.Spawn);
			foreach (string checkedItem in this.clbRunUOTypes.CheckedItems)
			{
				bool flag = false;
				foreach (SpawnObject spawnObject in SpawnNode.Spawn.SpawnObjects)
				{
					if (checkedItem.ToUpper() != spawnObject.TypeName.ToUpper())
					{
						continue;
					}
					flag = true;
					break;
				}
				if (flag)
				{
					continue;
				}
				SpawnNode.Spawn.SpawnObjects.Add(new SpawnObject(checkedItem, 1));
			}
			this.UpdateSpawnerMaxCount();
			SpawnNode.UpdateNode();
		}

		private void SetSpawnFromSpawnPack(SpawnPointNode SpawnNode, bool IsUpdate)
		{
			this.UpdateSpawnDetails(SpawnNode.Spawn);
			foreach (string checkedItem in this.clbSpawnPack.CheckedItems)
			{
				bool flag = false;
				foreach (SpawnObject spawnObject in SpawnNode.Spawn.SpawnObjects)
				{
					if (checkedItem.ToUpper() != spawnObject.TypeName.ToUpper())
					{
						continue;
					}
					flag = true;
					break;
				}
				if (flag)
				{
					continue;
				}
				SpawnNode.Spawn.SpawnObjects.Add(new SpawnObject(checkedItem, 1));
			}
			this.UpdateSpawnerMaxCount();
			SpawnNode.UpdateNode();
		}

		private void SmallWindow()
		{
			base.MinimumSize = new System.Drawing.Size(0, 0);
			base.MaximumSize = new System.Drawing.Size(0, 0);
			if (this.savewindowsize.IsEmpty || this.savepanelsize.IsEmpty)
			{
				base.Size = new System.Drawing.Size(660, 520);
				this.panel1.Size = new System.Drawing.Size(480, 517);
			}
			else
			{
				base.Size = this.savewindowsize;
				this.panel1.Size = this.savepanelsize;
			}
			this.panel1.Visible = true;
			this.tabControl1.Visible = false;
			this.panel3.Visible = false;
			this.axUOMap.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			if (!this.savemapsize.IsEmpty && !this.savelistsize.IsEmpty)
			{
				this.axUOMap.Size = this.savemapsize;
				this.tabControl2.Size = this.savelistsize;
				return;
			}
			this.axUOMap.Size = new System.Drawing.Size(472, 464);
			this.tabControl2.Size = new System.Drawing.Size(176, 264);
		}

		private void SpawnEditor_Closing(object sender, CancelEventArgs e)
		{
			if (MessageBox.Show(this, "Are you sure you want to quit?    ", "Spawn Editor 2", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
			{
				e.Cancel = true;
				return;
			}
			Environment.CurrentDirectory = this.StartingDirectory;
			this.WriteSpawnPacks(this.SpawnPackFile);
			this._CfgDialog.SaveWindowConfiguration();
			this._CfgDialog.SaveTransferServerConfiguration();
		}

		private void SpawnEditor_Load(object sender, EventArgs e)
		{
			SpawnEditor.Debug("Loading");
			this.StartingDirectory = Directory.GetCurrentDirectory();
			if (!this._CfgDialog.IsValidConfiguration)
			{
				SpawnEditor.Debug("OpeningConfiguration");
				this._CfgDialog.ShowDialog();
				if (!this._CfgDialog.IsValidConfiguration)
				{
					MessageBox.Show(this, string.Concat("Spawn Editor has not been configured properly.", Environment.NewLine, "Exiting..."), "Configuration Failure", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					Application.Exit();
				}
			}
			try
			{
				this.cbxShade.SelectedIndex = 0;
				foreach (object value in Enum.GetValues(typeof(WorldMap)))
				{
					WorldMap worldMap = (WorldMap)((int)((WorldMap)value));
					if (worldMap == WorldMap.Internal)
					{
						continue;
					}
					this.cbxMap.Items.Add(worldMap);
				}
				this.axUOMap.SetClientPath(string.Concat(Path.GetDirectoryName(this._CfgDialog.CfgUoClientPathValue), "\\"));
				this.axUOMap.set_ZoomLevel(this._CfgDialog.CfgZoomLevelValue);
				this.trkZoom.Value = this.axUOMap.get_ZoomLevel();
				Assembly assembly = Assembly.LoadFrom(this._CfgDialog.CfgRunUoPathValue);
				if (assembly != null)
				{
					SpawnEditor.AssemblyList.Add(assembly);
				}
				Assembly assembly1 = null;
				ArrayList arrayLists = new ArrayList();
				string directoryName = Path.GetDirectoryName(this._CfgDialog.CfgRunUoPathValue);
				this.LoadCustomAssemblies(directoryName);
				if (File.Exists(string.Concat(directoryName, "\\Scripts\\Output\\Scripts.dll")))
				{
					assembly1 = Assembly.LoadFrom(string.Concat(directoryName, "\\Scripts\\Output\\Scripts.dll"));
					if (assembly1 != null)
					{
						arrayLists.AddRange(assembly1.GetTypes());
						SpawnEditor.AssemblyList.Add(assembly1);
					}
				}
				if (File.Exists(string.Concat(directoryName, "\\Scripts\\Output\\Scripts.CS.dll")))
				{
					assembly1 = Assembly.LoadFrom(string.Concat(directoryName, "\\Scripts\\Output\\Scripts.CS.dll"));
					if (assembly1 != null)
					{
						arrayLists.AddRange(assembly1.GetTypes());
						SpawnEditor.AssemblyList.Add(assembly1);
					}
				}
				if (File.Exists(string.Concat(directoryName, "\\Scripts\\Output\\Scripts.VB.dll")))
				{
					assembly1 = Assembly.LoadFrom(string.Concat(directoryName, "\\Scripts\\Output\\Scripts.VB.dll"));
					if (assembly1 != null)
					{
						arrayLists.AddRange(assembly1.GetTypes());
						SpawnEditor.AssemblyList.Add(assembly1);
					}
				}
				this._RunUOScriptTypes = (Type[])arrayLists.ToArray(typeof(Type));
				this.LoadTypes();
				this.LoadSpawnPacks();
				this.lblTotalSpawn.Text = string.Concat("Total Spawns = ", this.tvwSpawnPoints.Nodes.Count);
				this.LoadDefaultSpawnValues();
				if (Directory.Exists(Path.GetDirectoryName(this._CfgDialog.CfgRunUoPathValue)))
				{
					this.ofdLoadFile.InitialDirectory = this._CfgDialog.CfgRunUoPathValue;
					this.sfdSaveFile.InitialDirectory = this._CfgDialog.CfgRunUoPathValue;
					SpawnEditor2.Region.Editor = this;
					SpawnEditor2.Region.Load(Path.GetDirectoryName(this._CfgDialog.CfgRunUoPathValue));
					this.FillRegionTree();
					this.treeRegionView.Refresh();
					this.FillGoTree(Path.GetDirectoryName(this._CfgDialog.CfgRunUoPathValue));
					this.treeGoView.Refresh();
				}
				this.cbxMap.SelectedIndex = (int)this._CfgDialog.CfgStartingMapValue;
				this.chkDrawStatics.Checked = this._CfgDialog.CfgStartingStaticsValue;
				this.mniAlwaysOnTop.Checked = this._CfgDialog.CfgStartingOnTopValue;
				base.TopMost = this.mniAlwaysOnTop.Checked;
				if (this._CfgDialog.CfgStartingXValue >= 0 && this._CfgDialog.CfgStartingYValue >= 0)
				{
					base.Location = new Point(this._CfgDialog.CfgStartingXValue, this._CfgDialog.CfgStartingYValue);
				}
				if (this._CfgDialog.CfgStartingWidthValue >= 0 && this._CfgDialog.CfgStartingHeightValue >= 0)
				{
					base.Size = new System.Drawing.Size(this._CfgDialog.CfgStartingWidthValue, this._CfgDialog.CfgStartingHeightValue);
				}
				this.chkDetails.Checked = this._CfgDialog.CfgStartingDetailsValue;
				this._CfgDialog.ConfigureTransferServer();
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				MessageBox.Show(this, string.Concat("Error loading the required RunUO executables. Please check that the paths specified in Setup are valid.", Environment.NewLine, exception.ToString()), "Configuration Error!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}

		private void spnMinDelay_ValueChanged(object sender, EventArgs e)
		{
		}

		private void spnSpawnRange_ValueChanged(object sender, EventArgs e)
		{
			if (this.SelectedSpawn != null && (int)this.spnSpawnRange.Value >= 0)
			{
				int value = (int)this.spnSpawnRange.Value * 2;
				int centreX = this.SelectedSpawn.CentreX - value / 2;
				int centreY = this.SelectedSpawn.CentreY - value / 2;
				this.SelectedSpawn.Bounds = new Rectangle(centreX, centreY, value, value);
				this.RefreshSpawnPoints();
			}
		}

		private void tabControl1_Leave(object sender, EventArgs e)
		{
			this.UpdateSpawnDetails(this.SelectedSpawn);
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

		private void treeGoView_MouseUp(object sender, MouseEventArgs e)
		{
			TreeNode nodeAt = this.treeGoView.GetNodeAt(e.X, e.Y);
			if (nodeAt is LocationSubNode)
			{
				foreach (TreeNode node in this.treeGoView.Nodes)
				{
					this.ClearTreeColor(node, this.treeGoView.BackColor);
				}
				LocationSubNode yellow = nodeAt as LocationSubNode;
				if (yellow.Node is ChildNode)
				{
					MapLocation location = ((ChildNode)yellow.Node).Location;
					WorldMap map = yellow.Map;
					yellow.BackColor = Color.Yellow;
					this.cbxMap.SelectedItem = map;
					this.AssignCenter((short)location.X, (short)location.Y, (short)map);
					if (this.chkSyncUO.Checked)
					{
						this.SendGoCommand((short)location.X, (short)location.Y, (short)location.Z, map);
					}
				}
			}
		}

		private void treeRegionView_MouseUp(object sender, MouseEventArgs e)
		{
			TreeNode nodeAt = this.treeRegionView.GetNodeAt(e.X, e.Y);
			if (nodeAt is RegionNode && nodeAt.Checked)
			{
				this.ClearTreeFacetSelection();
				nodeAt.Parent.Checked = true;
				SpawnEditor2.Region region = (nodeAt as RegionNode).Region;
				if (region != null)
				{
					MapLocation goLocation = region.GoLocation;
					this.cbxMap.SelectedItem = region.Map;
					this.AssignCenter((short)goLocation.X, (short)goLocation.Y, (short)region.Map);
					if (this.chkSyncUO.Checked)
					{
						this.SendGoCommand((short)goLocation.X, (short)goLocation.Y, (short)goLocation.Z, (WorldMap)goLocation.Facet);
					}
				}
			}
			else if (nodeAt is RegionFacetNode)
			{
				this.ClearTreeFacetSelection();
				nodeAt.Checked = true;
				this.cbxMap.SelectedItem = ((RegionFacetNode)nodeAt).Facet;
			}
			this.RefreshSpawnPoints();
		}

		private string TrimmedString(string val)
		{
			if (val == null)
			{
				return null;
			}
			string str = val.Trim();
			if (str != null && str.Length == 0)
			{
				str = null;
			}
			return str;
		}

		private void trkZoom_Scroll(object sender, EventArgs e)
		{
		}

		private void trkZoom_ValueChanged(object sender, EventArgs e)
		{
			this.axUOMap.set_ZoomLevel((short)this.trkZoom.Value);
			this.stbMain.Text = string.Concat(this.DefaultZoomLevelText, this.axUOMap.get_ZoomLevel());
			this.RefreshSpawnPoints();
		}

		private void tvwSpawnPacks_AfterSelect(object sender, TreeViewEventArgs e)
		{
			TreeNode node = e.Node;
			SpawnPackNode parent = node as SpawnPackNode;
			if (node is SpawnPackSubNode)
			{
				parent = (SpawnPackNode)node.Parent;
			}
			if (parent != null)
			{
				this.tvwSpawnPacks.SelectedNode = parent;
				this.clbSpawnPack.Items.Clear();
				this.clbSpawnPack.Sorted = false;
				foreach (SpawnPackSubNode spawnPackSubNode in parent.Nodes)
				{
					this.clbSpawnPack.Items.Add(spawnPackSubNode.Text);
				}
				this.textSpawnPackName.Text = parent.PackName;
				this.clbSpawnPack.Sorted = true;
			}
		}

		private void tvwSpawnPacks_MouseUp(object sender, MouseEventArgs e)
		{
			TreeNode nodeAt = this.tvwSpawnPacks.GetNodeAt(e.X, e.Y);
			if (nodeAt != null)
			{
				SpawnPackNode parent = nodeAt as SpawnPackNode;
				if (nodeAt is SpawnPackSubNode)
				{
					parent = (SpawnPackNode)nodeAt.Parent;
				}
				if (parent != null && e.Button == System.Windows.Forms.MouseButtons.Right)
				{
					this.tvwSpawnPacks.SelectedNode = parent;
					this.mcnSpawnPacks.Show(this.tvwSpawnPacks, new Point(e.X, e.Y));
				}
			}
		}

		private void tvwSpawnPoints_MouseUp(object sender, MouseEventArgs e)
		{
			TreeNode nodeAt = this.tvwSpawnPoints.GetNodeAt(e.X, e.Y);
			if (nodeAt != null)
			{
				this.tvwSpawnPoints.Refresh();
				this.SelectedSpawnNode = nodeAt as SpawnPointNode;
				SpawnObjectNode spawnObjectNode = nodeAt as SpawnObjectNode;
				if (spawnObjectNode != null)
				{
					this.SelectedSpawnNode = (SpawnPointNode)spawnObjectNode.Parent;
				}
				if (this.SelectedSpawnNode != null)
				{
					this.SelectedSpawn = this.SelectedSpawnNode.Spawn;
					foreach (SpawnPointNode node in this.tvwSpawnPoints.Nodes)
					{
						node.Spawn.IsSelected = false;
					}
					this.SelectedSpawn.IsSelected = true;
					this.SendGoCommand(this.SelectedSpawn);
					if ((int)this.SelectedSpawn.Map != (int)((WorldMap)this.cbxMap.SelectedItem))
					{
						this.cbxMap.SelectedItem = this.SelectedSpawn.Map;
					}
					this.DisplaySpawnDetails(this.SelectedSpawn);
					this.DisplaySpawnEntries();
					this.RefreshSpawnPoints();
				}
				if (e.Button == System.Windows.Forms.MouseButtons.Right)
				{
					this.tvwSpawnPoints.SelectedNode = nodeAt;
					this.mncSpawns.Show(this.tvwSpawnPoints, new Point(e.X, e.Y));
				}
				this.SetSelectedSpawnTypes();
			}
		}

		private void txtName_KeyUp(object sender, KeyEventArgs e)
		{
			this.namechanged = true;
			this.changednamestring = this.txtName.Text;
		}

		private void txtName_Leave(object sender, EventArgs e)
		{
			if (this.namechanged && this.SelectedSpawn != null)
			{
				this.SelectedSpawn.SpawnName = this.changednamestring;
				this.UpdateSpawnNode();
			}
			this.namechanged = false;
		}

		private void txtName_MouseLeave(object sender, EventArgs e)
		{
			if (this.namechanged && this.SelectedSpawn != null)
			{
				this.SelectedSpawn.SpawnName = this.changednamestring;
				this.UpdateSpawnNode();
			}
			this.namechanged = false;
		}

		private void TypeSelectionChanged(object sender, EventArgs e)
		{
			if (sender is RadioButton && ((RadioButton)sender).Checked)
			{
				this.LoadTypes();
			}
		}

		private void unloadSingleSpawner_Popup(object sender, EventArgs e)
		{
		}

		private void unloadSpawner_Popup(object sender, EventArgs e)
		{
		}

		public void UpdateMyLocation()
		{
			int num = 0;
			int num1 = 0;
			int num2 = 0;
			int num3 = -1;
			Client.Calibrate();
			if (!Client.FindLocation(ref num, ref num1, ref num2, ref num3))
			{
				MessageBox.Show(string.Format("{0} could not be found. Make sure the client is started and that the 'Client Window' option in Setup is correct.", string.Concat(this._CfgDialog.CfgUoClientWindowValue, " Not Found"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation));
			}
			else
			{
				this.cbxMap.SelectedIndex = num3;
				this.AssignCenter((short)num, (short)num1, (short)num3);
			}
			this.MyLocation.X = num;
			this.MyLocation.Y = num1;
			this.MyLocation.Z = num2;
			this.MyLocation.Facet = num3;
		}

		private void UpdateSpawnDetails(SpawnPoint Spawn)
		{
			if (Spawn == null)
			{
				return;
			}
			this.txtName.Text = this.txtName.Text.Trim();
			if (this.txtName.Text.Length == 0)
			{
				MessageBox.Show(this, "You must specify a name for the spawner!", "Spawn Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return;
			}
			Spawn.SpawnName = this.txtName.Text;
			Spawn.SpawnHomeRangeIsRelative = this.chkHomeRangeIsRelative.Checked;
			Spawn.SpawnHomeRange = (int)this.spnHomeRange.Value;
			Spawn.SpawnIsGroup = this.chkGroup.Checked;
			Spawn.SpawnIsRunning = this.chkRunning.Checked;
			Spawn.SpawnMaxCount = (int)this.spnMaxCount.Value;
			Spawn.SpawnMaxDelay = (double)((double)this.spnMaxDelay.Value);
			Spawn.SpawnMinDelay = (double)((double)this.spnMinDelay.Value);
			Spawn.SpawnTeam = (int)this.spnTeam.Value;
			Spawn.SpawnSpawnRange = (int)this.spnSpawnRange.Value;
			Spawn.SpawnProximityRange = (int)this.spnProximityRange.Value;
			Spawn.SpawnDuration = (double)((double)this.spnDuration.Value);
			Spawn.SpawnDespawn = (double)((double)this.spnDespawn.Value);
			Spawn.SpawnMinRefract = (double)((double)this.spnMinRefract.Value);
			Spawn.SpawnMaxRefract = (double)((double)this.spnMaxRefract.Value);
			Spawn.SpawnTODStart = (double)((double)this.spnTODStart.Value);
			Spawn.SpawnTODEnd = (double)((double)this.spnTODEnd.Value);
			Spawn.SpawnKillReset = (int)this.spnKillReset.Value;
			Spawn.SpawnProximitySnd = (int)this.spnProximitySnd.Value;
			Spawn.SpawnAllowGhost = this.chkAllowGhost.Checked;
			Spawn.SpawnSpawnOnTrigger = this.chkSpawnOnTrigger.Checked;
			if (!this.chkSequentialSpawn.Checked)
			{
				Spawn.SpawnSequentialSpawn = -1;
			}
			else
			{
				Spawn.SpawnSequentialSpawn = 0;
			}
			Spawn.SpawnSmartSpawning = this.chkSmartSpawning.Checked;
			if (!this.chkRealTOD.Checked)
			{
				Spawn.SpawnTODMode = 1;
			}
			else
			{
				Spawn.SpawnTODMode = 0;
			}
			Spawn.SpawnInContainer = this.chkInContainer.Checked;
			Spawn.SpawnSkillTrigger = this.TrimmedString(this.textSkillTrigger.Text);
			Spawn.SpawnSpeechTrigger = this.TrimmedString(this.textSpeechTrigger.Text);
			Spawn.SpawnProximityMsg = this.TrimmedString(this.textProximityMsg.Text);
			Spawn.SpawnMobTriggerName = this.TrimmedString(this.textMobTriggerName.Text);
			Spawn.SpawnMobTrigProp = this.TrimmedString(this.textMobTrigProp.Text);
			Spawn.SpawnPlayerTrigProp = this.TrimmedString(this.textPlayerTrigProp.Text);
			Spawn.SpawnTrigObjectProp = this.TrimmedString(this.textTrigObjectProp.Text);
			Spawn.SpawnTriggerOnCarried = this.TrimmedString(this.textTriggerOnCarried.Text);
			Spawn.SpawnNoTriggerOnCarried = this.TrimmedString(this.textNoTriggerOnCarried.Text);
			Spawn.SpawnTriggerProbability = (double)((double)this.spnTriggerProbability.Value);
			Spawn.SpawnStackAmount = (int)this.spnStackAmount.Value;
			Spawn.SpawnNotes = this.txtNotes.Text;
			Spawn.SpawnContainerX = (int)this.spnContainerX.Value;
			Spawn.SpawnContainerY = (int)this.spnContainerY.Value;
			Spawn.SpawnContainerZ = (int)this.spnContainerZ.Value;
			Spawn.SpawnExternalTriggering = this.chkExternalTriggering.Checked;
			Spawn.SpawnObjectPropertyItemName = this.TrimmedString(this.textTrigObjectName.Text);
			Spawn.SpawnSetPropertyItemName = this.TrimmedString(this.textSetObjectName.Text);
			Spawn.SpawnRegionName = this.TrimmedString(this.textRegionName.Text);
			Spawn.SpawnConfigFile = this.TrimmedString(this.textConfigFile.Text);
			Spawn.SpawnWaypoint = this.TrimmedString(this.textWayPoint.Text);
		}

		private void UpdateSpawnEntries()
		{
			int num;
			if (this.SelectedSpawn == null || this.SelectedSpawn.SpawnObjects == null)
			{
				return;
			}
			int value = this.vScrollBar1.Value;
			if (this.SelectedSpawn.SpawnObjects.Count > 7)
			{
				this.vScrollBar1.Maximum = this.SelectedSpawn.SpawnObjects.Count + 2;
			}
			int num1 = 0;
			int num2 = 0;
			IEnumerator enumerator = this.SelectedSpawn.SpawnObjects.GetEnumerator();
			try
			{
				do
				{
				Label0:
					if (!enumerator.MoveNext())
					{
						break;
					}
					SpawnObject current = (SpawnObject)enumerator.Current;
					int num3 = num1;
					num1 = num3 + 1;
					if (num3 >= value)
					{
						switch (num2)
						{
							case 0:
							{
								current.TypeName = this.entryText1.Text;
								current.Count = (int)this.entryMax1.Value;
								current.SpawnsPerTick = (int)this.entryPer1.Value;
								if (this.entrySub1.Text == null || this.entrySub1.Text.Length <= 0)
								{
									current.SubGroup = 0;
								}
								else
								{
									try
									{
										current.SubGroup = int.Parse(this.entrySub1.Text);
									}
									catch
									{
									}
								}
								if (this.entryReset1.Text == null || this.entryReset1.Text.Length <= 0)
								{
									current.SequentialResetTime = 0;
								}
								else
								{
									try
									{
										current.SequentialResetTime = (double)int.Parse(this.entryReset1.Text);
									}
									catch
									{
									}
								}
								if (this.entryTo1.Text == null || this.entryTo1.Text.Length <= 0)
								{
									current.SequentialResetTo = 0;
								}
								else
								{
									try
									{
										current.SequentialResetTo = int.Parse(this.entryTo1.Text);
									}
									catch
									{
									}
								}
								if (this.entryKills1.Text == null || this.entryKills1.Text.Length <= 0)
								{
									current.KillsNeeded = 0;
								}
								else
								{
									try
									{
										current.KillsNeeded = int.Parse(this.entryKills1.Text);
									}
									catch
									{
									}
								}
								if (this.entryMinD1.Text == null || this.entryMinD1.Text.Length <= 0)
								{
									current.MinDelay = -1;
								}
								else
								{
									try
									{
										current.MinDelay = (double)int.Parse(this.entryMinD1.Text);
									}
									catch
									{
									}
								}
								if (this.entryMaxD1.Text == null || this.entryMaxD1.Text.Length <= 0)
								{
									current.MaxDelay = -1;
								}
								else
								{
									try
									{
										current.MaxDelay = (double)int.Parse(this.entryMaxD1.Text);
									}
									catch
									{
									}
								}
								current.RestrictKillsToSubgroup = this.chkRK1.Checked;
								current.ClearOnAdvance = this.chkClr1.Checked;
								break;
							}
							case 1:
							{
								current.TypeName = this.entryText2.Text;
								current.Count = (int)this.entryMax2.Value;
								current.SpawnsPerTick = (int)this.entryPer2.Value;
								if (this.entrySub2.Text == null || this.entrySub2.Text.Length <= 0)
								{
									current.SubGroup = 0;
								}
								else
								{
									try
									{
										current.SubGroup = int.Parse(this.entrySub2.Text);
									}
									catch
									{
									}
								}
								if (this.entryReset2.Text == null || this.entryReset2.Text.Length <= 0)
								{
									current.SequentialResetTime = 0;
								}
								else
								{
									try
									{
										current.SequentialResetTime = (double)int.Parse(this.entryReset2.Text);
									}
									catch
									{
									}
								}
								if (this.entryTo2.Text == null || this.entryTo2.Text.Length <= 0)
								{
									current.SequentialResetTo = 0;
								}
								else
								{
									try
									{
										current.SequentialResetTo = int.Parse(this.entryTo2.Text);
									}
									catch
									{
									}
								}
								if (this.entryKills2.Text == null || this.entryKills2.Text.Length <= 0)
								{
									current.KillsNeeded = 0;
								}
								else
								{
									try
									{
										current.KillsNeeded = int.Parse(this.entryKills2.Text);
									}
									catch
									{
									}
								}
								if (this.entryMinD2.Text == null || this.entryMinD2.Text.Length <= 0)
								{
									current.MinDelay = -1;
								}
								else
								{
									try
									{
										current.MinDelay = (double)int.Parse(this.entryMinD2.Text);
									}
									catch
									{
									}
								}
								if (this.entryMaxD2.Text == null || this.entryMaxD2.Text.Length <= 0)
								{
									current.MaxDelay = -1;
								}
								else
								{
									try
									{
										current.MaxDelay = (double)int.Parse(this.entryMaxD2.Text);
									}
									catch
									{
									}
								}
								current.RestrictKillsToSubgroup = this.chkRK2.Checked;
								current.ClearOnAdvance = this.chkClr2.Checked;
								break;
							}
							case 2:
							{
								current.TypeName = this.entryText3.Text;
								current.Count = (int)this.entryMax3.Value;
								current.SpawnsPerTick = (int)this.entryPer3.Value;
								if (this.entrySub3.Text == null || this.entrySub3.Text.Length <= 0)
								{
									current.SubGroup = 0;
								}
								else
								{
									try
									{
										current.SubGroup = int.Parse(this.entrySub3.Text);
									}
									catch
									{
									}
								}
								if (this.entryReset3.Text == null || this.entryReset3.Text.Length <= 0)
								{
									current.SequentialResetTime = 0;
								}
								else
								{
									try
									{
										current.SequentialResetTime = (double)int.Parse(this.entryReset3.Text);
									}
									catch
									{
									}
								}
								if (this.entryTo3.Text == null || this.entryTo3.Text.Length <= 0)
								{
									current.SequentialResetTo = 0;
								}
								else
								{
									try
									{
										current.SequentialResetTo = int.Parse(this.entryTo3.Text);
									}
									catch
									{
									}
								}
								if (this.entryKills3.Text == null || this.entryKills3.Text.Length <= 0)
								{
									current.KillsNeeded = 0;
								}
								else
								{
									try
									{
										current.KillsNeeded = int.Parse(this.entryKills3.Text);
									}
									catch
									{
									}
								}
								if (this.entryMinD3.Text == null || this.entryMinD3.Text.Length <= 0)
								{
									current.MinDelay = -1;
								}
								else
								{
									try
									{
										current.MinDelay = (double)int.Parse(this.entryMinD3.Text);
									}
									catch
									{
									}
								}
								if (this.entryMaxD3.Text == null || this.entryMaxD3.Text.Length <= 0)
								{
									current.MaxDelay = -1;
								}
								else
								{
									try
									{
										current.MaxDelay = (double)int.Parse(this.entryMaxD3.Text);
									}
									catch
									{
									}
								}
								current.RestrictKillsToSubgroup = this.chkRK3.Checked;
								current.ClearOnAdvance = this.chkClr3.Checked;
								break;
							}
							case 3:
							{
								current.TypeName = this.entryText4.Text;
								current.Count = (int)this.entryMax4.Value;
								current.SpawnsPerTick = (int)this.entryPer4.Value;
								if (this.entrySub4.Text == null || this.entrySub4.Text.Length <= 0)
								{
									current.SubGroup = 0;
								}
								else
								{
									try
									{
										current.SubGroup = int.Parse(this.entrySub4.Text);
									}
									catch
									{
									}
								}
								if (this.entryReset4.Text == null || this.entryReset4.Text.Length <= 0)
								{
									current.SequentialResetTime = 0;
								}
								else
								{
									try
									{
										current.SequentialResetTime = (double)int.Parse(this.entryReset4.Text);
									}
									catch
									{
									}
								}
								if (this.entryTo4.Text == null || this.entryTo4.Text.Length <= 0)
								{
									current.SequentialResetTo = 0;
								}
								else
								{
									try
									{
										current.SequentialResetTo = int.Parse(this.entryTo4.Text);
									}
									catch
									{
									}
								}
								if (this.entryKills4.Text == null || this.entryKills4.Text.Length <= 0)
								{
									current.KillsNeeded = 0;
								}
								else
								{
									try
									{
										current.KillsNeeded = int.Parse(this.entryKills4.Text);
									}
									catch
									{
									}
								}
								if (this.entryMinD4.Text == null || this.entryMinD4.Text.Length <= 0)
								{
									current.MinDelay = -1;
								}
								else
								{
									try
									{
										current.MinDelay = (double)int.Parse(this.entryMinD4.Text);
									}
									catch
									{
									}
								}
								if (this.entryMaxD4.Text == null || this.entryMaxD4.Text.Length <= 0)
								{
									current.MaxDelay = -1;
								}
								else
								{
									try
									{
										current.MaxDelay = (double)int.Parse(this.entryMaxD4.Text);
									}
									catch
									{
									}
								}
								current.RestrictKillsToSubgroup = this.chkRK4.Checked;
								current.ClearOnAdvance = this.chkClr4.Checked;
								break;
							}
							case 4:
							{
								current.TypeName = this.entryText5.Text;
								current.Count = (int)this.entryMax5.Value;
								current.SpawnsPerTick = (int)this.entryPer5.Value;
								if (this.entrySub5.Text == null || this.entrySub5.Text.Length <= 0)
								{
									current.SubGroup = 0;
								}
								else
								{
									try
									{
										current.SubGroup = int.Parse(this.entrySub5.Text);
									}
									catch
									{
									}
								}
								if (this.entryReset5.Text == null || this.entryReset5.Text.Length <= 0)
								{
									current.SequentialResetTime = 0;
								}
								else
								{
									try
									{
										current.SequentialResetTime = (double)int.Parse(this.entryReset5.Text);
									}
									catch
									{
									}
								}
								if (this.entryTo5.Text == null || this.entryTo5.Text.Length <= 0)
								{
									current.SequentialResetTo = 0;
								}
								else
								{
									try
									{
										current.SequentialResetTo = int.Parse(this.entryTo5.Text);
									}
									catch
									{
									}
								}
								if (this.entryKills5.Text == null || this.entryKills5.Text.Length <= 0)
								{
									current.KillsNeeded = 0;
								}
								else
								{
									try
									{
										current.KillsNeeded = int.Parse(this.entryKills5.Text);
									}
									catch
									{
									}
								}
								if (this.entryMinD5.Text == null || this.entryMinD5.Text.Length <= 0)
								{
									current.MinDelay = -1;
								}
								else
								{
									try
									{
										current.MinDelay = (double)int.Parse(this.entryMinD5.Text);
									}
									catch
									{
									}
								}
								if (this.entryMaxD5.Text == null || this.entryMaxD5.Text.Length <= 0)
								{
									current.MaxDelay = -1;
								}
								else
								{
									try
									{
										current.MaxDelay = (double)int.Parse(this.entryMaxD5.Text);
									}
									catch
									{
									}
								}
								current.RestrictKillsToSubgroup = this.chkRK5.Checked;
								current.ClearOnAdvance = this.chkClr5.Checked;
								break;
							}
							case 5:
							{
								current.TypeName = this.entryText6.Text;
								current.Count = (int)this.entryMax6.Value;
								current.SpawnsPerTick = (int)this.entryPer6.Value;
								if (this.entrySub6.Text == null || this.entrySub6.Text.Length <= 0)
								{
									current.SubGroup = 0;
								}
								else
								{
									try
									{
										current.SubGroup = int.Parse(this.entrySub6.Text);
									}
									catch
									{
									}
								}
								if (this.entryReset6.Text == null || this.entryReset6.Text.Length <= 0)
								{
									current.SequentialResetTime = 0;
								}
								else
								{
									try
									{
										current.SequentialResetTime = (double)int.Parse(this.entryReset6.Text);
									}
									catch
									{
									}
								}
								if (this.entryTo6.Text == null || this.entryTo6.Text.Length <= 0)
								{
									current.SequentialResetTo = 0;
								}
								else
								{
									try
									{
										current.SequentialResetTo = int.Parse(this.entryTo6.Text);
									}
									catch
									{
									}
								}
								if (this.entryKills6.Text == null || this.entryKills6.Text.Length <= 0)
								{
									current.KillsNeeded = 0;
								}
								else
								{
									try
									{
										current.KillsNeeded = int.Parse(this.entryKills6.Text);
									}
									catch
									{
									}
								}
								if (this.entryMinD6.Text == null || this.entryMinD6.Text.Length <= 0)
								{
									current.MinDelay = -1;
								}
								else
								{
									try
									{
										current.MinDelay = (double)int.Parse(this.entryMinD6.Text);
									}
									catch
									{
									}
								}
								if (this.entryMaxD6.Text == null || this.entryMaxD6.Text.Length <= 0)
								{
									current.MaxDelay = -1;
								}
								else
								{
									try
									{
										current.MaxDelay = (double)int.Parse(this.entryMaxD6.Text);
									}
									catch
									{
									}
								}
								current.RestrictKillsToSubgroup = this.chkRK6.Checked;
								current.ClearOnAdvance = this.chkClr6.Checked;
								break;
							}
							case 6:
							{
								current.TypeName = this.entryText7.Text;
								current.Count = (int)this.entryMax7.Value;
								current.SpawnsPerTick = (int)this.entryPer7.Value;
								if (this.entrySub7.Text == null || this.entrySub7.Text.Length <= 0)
								{
									current.SubGroup = 0;
								}
								else
								{
									try
									{
										current.SubGroup = int.Parse(this.entrySub7.Text);
									}
									catch
									{
									}
								}
								if (this.entryReset7.Text == null || this.entryReset7.Text.Length <= 0)
								{
									current.SequentialResetTime = 0;
								}
								else
								{
									try
									{
										current.SequentialResetTime = (double)int.Parse(this.entryReset7.Text);
									}
									catch
									{
									}
								}
								if (this.entryTo7.Text == null || this.entryTo7.Text.Length <= 0)
								{
									current.SequentialResetTo = 0;
								}
								else
								{
									try
									{
										current.SequentialResetTo = int.Parse(this.entryTo7.Text);
									}
									catch
									{
									}
								}
								if (this.entryKills7.Text == null || this.entryKills7.Text.Length <= 0)
								{
									current.KillsNeeded = 0;
								}
								else
								{
									try
									{
										current.KillsNeeded = int.Parse(this.entryKills7.Text);
									}
									catch
									{
									}
								}
								if (this.entryMinD7.Text == null || this.entryMinD7.Text.Length <= 0)
								{
									current.MinDelay = -1;
								}
								else
								{
									try
									{
										current.MinDelay = (double)int.Parse(this.entryMinD7.Text);
									}
									catch
									{
									}
								}
								if (this.entryMaxD7.Text == null || this.entryMaxD7.Text.Length <= 0)
								{
									current.MaxDelay = -1;
								}
								else
								{
									try
									{
										current.MaxDelay = (double)int.Parse(this.entryMaxD7.Text);
									}
									catch
									{
									}
								}
								current.RestrictKillsToSubgroup = this.chkRK7.Checked;
								current.ClearOnAdvance = this.chkClr7.Checked;
								break;
							}
							case 7:
							{
								current.TypeName = this.entryText8.Text;
								current.Count = (int)this.entryMax8.Value;
								current.SpawnsPerTick = (int)this.entryPer8.Value;
								if (this.entrySub8.Text == null || this.entrySub8.Text.Length <= 0)
								{
									current.SubGroup = 0;
								}
								else
								{
									try
									{
										current.SubGroup = int.Parse(this.entrySub8.Text);
									}
									catch
									{
									}
								}
								if (this.entryReset8.Text == null || this.entryReset8.Text.Length <= 0)
								{
									current.SequentialResetTime = 0;
								}
								else
								{
									try
									{
										current.SequentialResetTime = (double)int.Parse(this.entryReset8.Text);
									}
									catch
									{
									}
								}
								if (this.entryTo8.Text == null || this.entryTo8.Text.Length <= 0)
								{
									current.SequentialResetTo = 0;
								}
								else
								{
									try
									{
										current.SequentialResetTo = int.Parse(this.entryTo8.Text);
									}
									catch
									{
									}
								}
								if (this.entryKills8.Text == null || this.entryKills8.Text.Length <= 0)
								{
									current.KillsNeeded = 0;
								}
								else
								{
									try
									{
										current.KillsNeeded = int.Parse(this.entryKills8.Text);
									}
									catch
									{
									}
								}
								if (this.entryMinD8.Text == null || this.entryMinD8.Text.Length <= 0)
								{
									current.MinDelay = -1;
								}
								else
								{
									try
									{
										current.MinDelay = (double)int.Parse(this.entryMinD8.Text);
									}
									catch
									{
									}
								}
								if (this.entryMaxD8.Text == null || this.entryMaxD8.Text.Length <= 0)
								{
									current.MaxDelay = -1;
								}
								else
								{
									try
									{
										current.MaxDelay = (double)int.Parse(this.entryMaxD8.Text);
									}
									catch
									{
									}
								}
								current.RestrictKillsToSubgroup = this.chkRK8.Checked;
								current.ClearOnAdvance = this.chkClr8.Checked;
								break;
							}
						}
						num = num2 + 1;
						num2 = num;
					}
					else
					{
						goto Label0;
					}
				}
				while (num <= 7);
			}
			finally
			{
				IDisposable disposable = enumerator as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}
		}

		public void UpdateSpawnerMaxCount()
		{
			if (this.SelectedSpawn != null && this.SelectedSpawn.SpawnObjects != null)
			{
				int count = 0;
				foreach (SpawnObject spawnObject in this.SelectedSpawn.SpawnObjects)
				{
					count = count + spawnObject.Count;
				}
				this.SelectedSpawn.SpawnMaxCount = count;
				this.spnMaxCount.Value = count;
			}
		}

		private void UpdateSpawnNode()
		{
			if (this.SelectedSpawnNode != null)
			{
				this.SelectedSpawnNode.UpdateNode();
			}
			this.tvwSpawnPoints.Update();
		}

		private void UpdateSpawnPacks(string packName, CheckedListBox.ObjectCollection items)
		{
			if (packName == null || packName.Length == 0 || items == null || items.Count == 0)
			{
				return;
			}
			bool flag = false;
			foreach (TreeNode node in this.tvwSpawnPacks.Nodes)
			{
				if (!(node is SpawnPackNode))
				{
					continue;
				}
				SpawnPackNode spawnPackNode = (SpawnPackNode)node;
				if (spawnPackNode.PackName != packName)
				{
					continue;
				}
				spawnPackNode.UpdateNode(items);
				flag = true;
				break;
			}
			if (!flag)
			{
				this.tvwSpawnPacks.Nodes.Add(new SpawnPackNode(packName, items));
			}
			this.tvwSpawnPacks.Update();
		}

		private void vScrollBar1_MouseEnter(object sender, EventArgs e)
		{
			this.UpdateSpawnEntries();
			this.UpdateSpawnNode();
		}

		private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
		{
			this.DisplaySpawnEntries();
		}

		public void WriteSpawnPacks(string filename)
		{
			string str = filename;
			try
			{
				XmlTextWriter xmlTextWriter = new XmlTextWriter(new StreamWriter(str))
				{
					Formatting = Formatting.Indented
				};
				xmlTextWriter.WriteStartDocument(true);
				xmlTextWriter.WriteStartElement("SpawnPacks");
				foreach (SpawnPackNode node in this.tvwSpawnPacks.Nodes)
				{
					xmlTextWriter.WriteStartElement("Pack");
					xmlTextWriter.WriteAttributeString("name", node.PackName);
					for (int i = 0; i < node.Nodes.Count; i++)
					{
						xmlTextWriter.WriteStartElement("T");
						xmlTextWriter.WriteString(node.Nodes[i].Text);
						xmlTextWriter.WriteEndElement();
					}
					xmlTextWriter.WriteEndElement();
				}
				xmlTextWriter.WriteEndElement();
				xmlTextWriter.Close();
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				string[] newLine = new string[] { "Failed to save SpawnPack file [", str, "] for the following reason:", Environment.NewLine, this.ExceptionMessage(exception) };
				MessageBox.Show(this, string.Concat(newLine), "Save Failure", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}

		internal class CustomExceptionHandler
		{
			public CustomExceptionHandler()
			{
			}

			public void OnThreadException(object sender, ThreadExceptionEventArgs t)
			{
				System.Windows.Forms.DialogResult dialogResult = System.Windows.Forms.DialogResult.Cancel;
				try
				{
					dialogResult = this.ShowThreadExceptionDialog(t.Exception);
				}
				catch
				{
					try
					{
						MessageBox.Show("Fatal Error", "Fatal Error", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Hand);
					}
					finally
					{
						Application.Exit();
					}
				}
				if (dialogResult == System.Windows.Forms.DialogResult.Abort)
				{
					Application.Exit();
				}
			}

			private System.Windows.Forms.DialogResult ShowThreadExceptionDialog(Exception e)
			{
				string str = "An error occurred:\n\n";
				str = string.Concat(str, e.Message, "\n\nStack Trace:\n", e.StackTrace);
				SpawnEditor.Debug(str);
				return MessageBox.Show(str, "Application Error", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Hand);
			}
		}

		public class SelectionWindow
		{
			public int Index;

			public short X;

			public short Y;

			public short SX;

			public short SY;

			public short Width;

			public short Height;

			public Rectangle Bounds
			{
				get
				{
					return new Rectangle(this.X, this.Y, this.Width, this.Height);
				}
			}

			public SelectionWindow()
			{
			}

			public bool IsWithinWindow(short MapX, short MapY)
			{
				Rectangle rectangle = new Rectangle(this.X, this.Y, this.Width, this.Height);
				return rectangle.Contains(MapX, MapY);
			}
		}

		public class TrackerThread
		{
			private SpawnEditor Editor;

			public TrackerThread(SpawnEditor editor)
			{
				this.Editor = editor;
			}

			public void TrackerThreadMain()
			{
				int num = 0;
				int num1 = 0;
				int num2 = -1;
				bool flag = false;
				while (this.Editor != null && this.Editor.Tracking)
				{
					Thread.Sleep(250);
					int num3 = 0;
					int num4 = 0;
					int num5 = 0;
					int num6 = -1;
					if (!Client.FindLocation(ref num3, ref num4, ref num5, ref num6))
					{
						MessageBox.Show(string.Format("{0}. Make sure the client is started and that the 'Client Window' option in Setup is correct.", string.Concat(this.Editor._CfgDialog.CfgUoClientWindowValue, " Not Found"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation));
						this.Editor.Tracking = false;
						this.Editor.chkTracking.Checked = false;
					}
					else if (num6 != num2 || num3 != num || num4 != num1)
					{
						this.Editor.MyLocation.X = num3;
						this.Editor.MyLocation.Y = num4;
						this.Editor.MyLocation.Z = num5;
						this.Editor.MyLocation.Facet = num6;
						this.Editor.cbxMap.SelectedIndex = num6;
						this.Editor.AssignCenter((short)num3, (short)num4, (short)num6);
						this.Editor.DisplayMyLocation();
						num = num3;
						num1 = num4;
						num2 = num6;
						flag = false;
					}
					else
					{
						if (flag)
						{
							continue;
						}
						flag = true;
					}
				}
			}
		}
	}
}