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
using System.Linq;
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
        private AxUOMap axUOMap;

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

		private NumericUpDown spnProximitySnd;

        private NumericUpDown spnKillReset;

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
        private GroupBox grpSpawnEntries;
        private NumericUpDown entryPer8;
        private NumericUpDown entryPer7;
        private NumericUpDown entryPer6;
        private NumericUpDown entryPer5;
        private NumericUpDown entryPer4;
        private NumericUpDown entryPer3;
        private NumericUpDown entryPer2;
        private NumericUpDown entryPer1;
        private Label label30;
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
        private Label label9;
        private Label label8;
        private Label label7;
        private Label label6;
        private Label label5;
        private TextBox entryTo1;
        private VScrollBar vScrollBar1;
        private TextBox entrySub1;
        private Label label4;
        private Label label3;
        private CheckBox chkRK1;
        private Label label2;
        private Label label1;
        private NumericUpDown entryMax1;
        private Button btnEntryEdit1;
        private TextBox entryText1;
        private CheckBox chkClr1;
        private GroupBox groupTemplateList;
        private Button btnSaveTemplate;
        private Button btnMergeTemplate;
        private Button btnLoadTemplate;
        private TreeView tvwTemplates;
        private Label label29;

		private Hashtable ControlModHash = new Hashtable();

		public bool SpawnLocationLocked
		{
			get
			{
				return chkLockSpawn.Checked;
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
			InitializeMapCenters();
			InitializeComponent();
			SpawnEditor.Debug("Initialized");
			SmallWindow();
			SpawnEditor.Debug("WindowConfigured");
			_CfgDialog = new Configure(this);
			SpawnEditor.Debug("ConfigurationDialog");
			_TransferDialog = new TransferServerSettings(this);
			SpawnEditor.Debug("TransferDialog");
			_SpawnerFilters = new SpawnerFilters(this);
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
			if (entrychanged > 0)
			{
				if (HasEntry(SelectedSpawn, entrychanged))
				{
					UpdateSpawnEntries();
					UpdateSpawnNode();
				}
				else
				{
					UpdateSpawnEntries();
					UpdateSpawnNode();
					if (SelectedSpawn != null)
					{
						SelectedSpawn.SpawnObjects.Add(new SpawnObject(changedentrystring, 1));
					}
					UpdateSpawnerMaxCount();
					DisplaySpawnEntries();
					UpdateSpawnNode();
				}
				entrychanged = 0;
				changedentrystring = null;
			}
		}

		private void ApplyModifications(SpawnPoint spawn)
		{
			if (ControlHasBeenSelected(txtName.Name))
			{
				spawn.SpawnName = txtName.Text;
			}
			if (ControlHasBeenSelected(spnHomeRange.Name))
			{
				spawn.HomeRange = (int)spnHomeRange.Value;
			}
			if (ControlHasBeenSelected(spnMaxCount.Name))
			{
				spawn.MaxCount = (int)spnMaxCount.Value;
			}
			if (ControlHasBeenSelected(spnMinDelay.Name))
			{
				spawn.MinDelay = (double)((double)spnMinDelay.Value);
			}
			if (ControlHasBeenSelected(spnMaxDelay.Name))
			{
				spawn.MaxDelay = (double)((double)spnMaxDelay.Value);
			}
			if (ControlHasBeenSelected(spnTeam.Name))
			{
				spawn.Team = (int)spnTeam.Value;
			}
			if (ControlHasBeenSelected(spnSpawnRange.Name))
			{
				spawn.SpawnRange = (int)spnSpawnRange.Value;
			}
			if (ControlHasBeenSelected(spnProximityRange.Name))
			{
				spawn.ProximityRange = (int)spnProximityRange.Value;
			}
			if (ControlHasBeenSelected(spnDuration.Name))
			{
				spawn.Duration = (double)((double)spnDuration.Value);
			}
			if (ControlHasBeenSelected(spnDespawn.Name))
			{
				spawn.Despawn = (double)((double)spnDespawn.Value);
			}
			if (ControlHasBeenSelected(spnMinRefract.Name))
			{
				spawn.MinRefract = (double)((double)spnMinRefract.Value);
			}
			if (ControlHasBeenSelected(spnMaxRefract.Name))
			{
				spawn.MaxRefract = (double)((double)spnMaxRefract.Value);
			}
			if (ControlHasBeenSelected(spnTODStart.Name))
			{
				spawn.TODStart = (double)((double)spnTODStart.Value);
			}
			if (ControlHasBeenSelected(spnTODEnd.Name))
			{
				spawn.TODEnd = (double)((double)spnTODEnd.Value);
			}
			if (ControlHasBeenSelected(spnKillReset.Name))
			{
				spawn.KillReset = (int)spnKillReset.Value;
			}
			if (ControlHasBeenSelected(spnProximitySnd.Name))
			{
				spawn.ProximitySnd = (int)spnProximitySnd.Value;
			}
			if (ControlHasBeenSelected(chkGroup.Name))
			{
				spawn.Group = chkGroup.Checked;
			}
			if (ControlHasBeenSelected(chkRunning.Name))
			{
				spawn.Running = chkRunning.Checked;
			}
			if (ControlHasBeenSelected(chkHomeRangeIsRelative.Name))
			{
				spawn.RelativeHome = chkHomeRangeIsRelative.Checked;
			}
			if (ControlHasBeenSelected(chkInContainer.Name))
			{
				spawn.InContainer = chkInContainer.Checked;
			}
			if (ControlHasBeenSelected(chkRealTOD.Name))
			{
				spawn.RealTOD = chkRealTOD.Checked;
			}
			if (ControlHasBeenSelected(chkGameTOD.Name))
			{
				spawn.GameTOD = chkGameTOD.Checked;
			}
			if (ControlHasBeenSelected(chkSpawnOnTrigger.Name))
			{
				spawn.SpawnOnTrigger = chkSpawnOnTrigger.Checked;
			}
			if (ControlHasBeenSelected(chkSequentialSpawn.Name))
			{
				spawn.SequentialSpawn = chkSequentialSpawn.Checked;
			}
			if (ControlHasBeenSelected(chkSmartSpawning.Name))
			{
				spawn.SmartSpawning = chkSmartSpawning.Checked;
			}
		}

		internal void ApplySpawnFilter()
		{
			if (tvwSpawnPoints == null || tvwSpawnPoints.Nodes == null)
			{
				return;
			}
			foreach (SpawnPointNode node in tvwSpawnPoints.Nodes)
			{
				if (_SpawnerFilters.HasMatch(node.Spawn))
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
			MapLoc[facet].X = X;
			MapLoc[facet].Y = Y;
			axUOMap.SetCenter(X, Y);
		}

	    private void axUOMap_MouseDownEvent(object sender, _DUOMapEvents_MouseDownEvent e)
		{
			short mapX = axUOMap.CtrlToMapX((short)e.x);
			short mapY = axUOMap.CtrlToMapY((short)e.y);
			short mapHeight = axUOMap.GetMapHeight(mapX, mapY);
			RightMouseDown = false;
			RightMouseDownStart = DateTime.MaxValue;
			if (e.button == 1)
			{
				if (GoToSelected)
				{
					SendGoCommand(mapX, mapY, mapHeight, (WorldMap)((int)((WorldMap)cbxMap.SelectedItem)));
					GoToSelected = false;
				}
				if (_SelectionWindow != null && _SelectionWindow.Index > -1)
				{
					axUOMap.RemoveDrawRectAt(_SelectionWindow.Index);
					ClearSelectionWindow();
				}
				_SelectionWindow = new SpawnEditor.SelectionWindow()
				{
					X = mapX,
					Y = mapY,
					SX = mapX,
					SY = mapY,
					Index = axUOMap.AddDrawRect(_SelectionWindow.X, _SelectionWindow.Y, 1, 1, 2, 16777215)
				};
				EnableSelectionWindowOption(true);
			}
			else if (e.button == 2)
			{
				RightMouseDown = true;
				RightMouseDownStart = DateTime.Now;
			}
			RefreshSpawnPoints();
		}

		private void axUOMap_MouseMoveEvent(object sender, _DUOMapEvents_MouseMoveEvent e)
		{
			Rectangle bounds;
			short mapX = axUOMap.CtrlToMapX((short)e.x);
			short mapY = axUOMap.CtrlToMapY((short)e.y);
			short mapHeight = axUOMap.GetMapHeight(mapX, mapY);
			WorldMap selectedItem = (WorldMap)((int)((WorldMap)cbxMap.SelectedItem));
			if (e.button == 0)
			{
				trkZoom.Focus();
				MouseResize = false;
				string empty = string.Empty;
				bool flag = false;
				short value = (short)(6 - (short)trkZoom.Value);
				if (_TransferDialog.chkShowTips.Checked)
				{
					if (_TransferDialog.chkShowCreatures.Checked && MobLocArray != null)
					{
						int num = 0;
						while (num < (int)MobLocArray.Length)
						{
							if (MobLocArray[num].Map != (int)selectedItem || mapX >= MobLocArray[num].X + value || mapX <= MobLocArray[num].X - value || mapY >= MobLocArray[num].Y + value || mapY <= MobLocArray[num].Y - value)
							{
								num++;
							}
							else
							{
								empty = MobLocArray[num].Name;
								flag = true;
								break;
							}
						}
					}
					if (_TransferDialog.chkShowPlayers.Checked && PlayerLocArray != null && !flag)
					{
						int num1 = 0;
						while (num1 < (int)PlayerLocArray.Length)
						{
							if (PlayerLocArray[num1].Map != (int)selectedItem || mapX >= PlayerLocArray[num1].X + value || mapX <= PlayerLocArray[num1].X - value || mapY >= PlayerLocArray[num1].Y + value || mapY <= PlayerLocArray[num1].Y - value)
							{
								num1++;
							}
							else
							{
								empty = PlayerLocArray[num1].Name;
								flag = true;
								break;
							}
						}
					}
					if (_TransferDialog.chkShowItems.Checked && ItemLocArray != null && !flag)
					{
						int num2 = 0;
						while (num2 < (int)ItemLocArray.Length)
						{
							if (ItemLocArray[num2].Map != (int)selectedItem || mapX >= ItemLocArray[num2].X + value || mapX <= ItemLocArray[num2].X - value || mapY >= ItemLocArray[num2].Y + value || mapY <= ItemLocArray[num2].Y - value)
							{
								num2++;
							}
							else
							{
								empty = ItemLocArray[num2].Name;
								flag = true;
								break;
							}
						}
					}
				}
				if (chkShowMapTip.Checked && chkShowSpawns.Checked && !flag)
				{
					ArrayList arrayLists = new ArrayList(tvwSpawnPoints.Nodes);
					arrayLists.Sort(new SpawnPointAreaComparer());
					foreach (SpawnPointNode arrayList in arrayLists)
					{
						if ((int)arrayList.Spawn.Map != (int)((WorldMap)cbxMap.SelectedItem) || arrayList.Filtered || !arrayList.Spawn.IsSameArea(mapX, mapY, 1))
						{
							continue;
						}
						AxUOMap axUOMap0 = axUOMap;
						bounds = arrayList.Spawn.Bounds;
						int ctrlX = axUOMap0.MapToCtrlX((short)bounds.X);
						AxUOMap axUOMap1 = axUOMap;
						bounds = arrayList.Spawn.Bounds;
						int ctrlY = axUOMap1.MapToCtrlY((short)bounds.Y);
						AxUOMap axUOMap2 = axUOMap;
						bounds = arrayList.Spawn.Bounds;
						int x = bounds.X;
						bounds = arrayList.Spawn.Bounds;
						int ctrlX1 = axUOMap2.MapToCtrlX((short)(x + bounds.Width)) - ctrlX;
						AxUOMap axUOMap3 = axUOMap;
						bounds = arrayList.Spawn.Bounds;
						int y = bounds.Y;
						bounds = arrayList.Spawn.Bounds;
						int ctrlY1 = axUOMap3.MapToCtrlY((short)(y + bounds.Height)) - ctrlY;
						if (arrayList.Spawn != SelectedSpawn || (double)e.x <= (double)ctrlX + (double)ctrlX1 * 0.8 || e.x >= ctrlX + ctrlX1 || (double)e.y <= (double)ctrlY + (double)ctrlY1 * 0.8 || e.y >= ctrlY + ctrlY1)
						{
							empty = arrayList.Spawn.ToString();
							break;
						}
						else
						{
							empty = "Resize";
							MouseResize = true;
							break;
						}
					}
				}
				ttpSpawnInfo.SetToolTip(axUOMap, empty);
				if (_SelectionWindow == null)
				{
					stbMain.Text = string.Format("[X={0} Y={1} H={2}]", mapX, mapY, mapHeight);
					return;
				}
			}
			else if (e.button == 1)
			{
				if (_SelectionWindow != null)
				{
					if (_SelectionWindow.Index > -1)
					{
						axUOMap.RemoveDrawRectAt(_SelectionWindow.Index);
						_SelectionWindow.Index = -1;
					}
					short sX = (short)(mapX - _SelectionWindow.SX);
					short sY = (short)(mapY - _SelectionWindow.SY);
					_SelectionWindow.Width = Math.Abs(sX);
					_SelectionWindow.Height = Math.Abs(sY);
					_SelectionWindow.X = _SelectionWindow.SX;
					_SelectionWindow.Y = _SelectionWindow.SY;
					if (sY < 0)
					{
						_SelectionWindow.Y = (short)(_SelectionWindow.SY + sY);
					}
					if (sX < 0)
					{
						_SelectionWindow.X = (short)(_SelectionWindow.SX + sX);
					}
					foreach (SpawnPointNode node in tvwSpawnPoints.Nodes)
					{
						node.Spawn.IsSelected = false;
					}
					txtName.Text = string.Concat(_CfgDialog.CfgSpawnNameValue, tvwSpawnPoints.Nodes.Count);
					txtName.Refresh();
					_SelectionWindow.Index = axUOMap.AddDrawRect(_SelectionWindow.X, _SelectionWindow.Y, _SelectionWindow.Width, _SelectionWindow.Height, 2, 16777215);
					StatusBar statusBar = stbMain;
					object[] objArray = new object[] { _SelectionWindow.X, _SelectionWindow.Y, (int)(_SelectionWindow.X + _SelectionWindow.Width), (int)(_SelectionWindow.Y + _SelectionWindow.Height), _SelectionWindow.Width, _SelectionWindow.Height };
					statusBar.Text = string.Format("[X1={0} Y1={1}] TO [X2={2} Y2={3}] (Width={4}, Height={5})", objArray);
					return;
				}
			}
			else if (e.button == 2)
			{
				SpawnPoint spawn = null;
				foreach (SpawnPointNode spawnPointNode in tvwSpawnPoints.Nodes)
				{
					if ((int)spawnPointNode.Spawn.Map != (int)((WorldMap)cbxMap.SelectedItem) || !spawnPointNode.Spawn.IsSelected)
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
					if (MouseResize)
					{
						spawn.Bounds = new Rectangle(x1, y1, mapX - x1 + 1, mapY - y1 + 1);
						if (!SpawnLocationLocked)
						{
							int x2 = spawn.Bounds.X;
							bounds = spawn.Bounds;
							spawn.CentreX = (short)(x2 + bounds.Width / 2);
							int y2 = spawn.Bounds.Y;
							bounds = spawn.Bounds;
							spawn.CentreY = (short)(y2 + bounds.Height / 2);
						}
						spnSpawnRange.Value = new decimal(-1);
					}
					else if ((DateTime.Now - RightMouseDownStart) > TimeSpan.FromSeconds(0.4))
					{
						spawn.Bounds = new Rectangle(mapX - width / 2, mapY - height / 2, width, height);
						if (!SpawnLocationLocked)
						{
							int x3 = spawn.Bounds.X;
							bounds = spawn.Bounds;
							spawn.CentreX = (short)(x3 + bounds.Width / 2);
							int y3 = spawn.Bounds.Y;
							bounds = spawn.Bounds;
							spawn.CentreY = (short)(y3 + bounds.Height / 2);
						}
					}
					RefreshSpawnPoints();
				}
			}
		}

		private void axUOMap_MouseUpEvent(object sender, _DUOMapEvents_MouseUpEvent e)
		{
			short mapX = axUOMap.CtrlToMapX((short)e.x);
			short mapY = axUOMap.CtrlToMapY((short)e.y);
			axUOMap.GetMapHeight(mapX, mapY);
			if (RightMouseDown)
			{
				if (_SelectionWindow != null && _SelectionWindow.IsWithinWindow(mapX, mapY))
				{
					txtName.Text = txtName.Text.Trim();
					spnSpawnRange.Value = new decimal(-1);
					if (txtName.Text.Length == 0)
					{
						MessageBox.Show(this, "You must specify a name for the spawner!", "Spawn Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
						return;
					}
					foreach (SpawnPointNode node in tvwSpawnPoints.Nodes)
					{
						node.Spawn.IsSelected = false;
					}
					SpawnPointNode spawnPointNode = new SpawnPointNode(new SpawnPoint(Guid.NewGuid(), (WorldMap)((int)((WorldMap)cbxMap.SelectedItem)), _SelectionWindow.Bounds));
					SetSpawn(spawnPointNode, false);
					spawnPointNode.Spawn.CentreZ = -32768;
					tvwSpawnPoints.Nodes.Add(spawnPointNode);
					lblTotalSpawn.Text = string.Concat("Total Spawns = ", tvwSpawnPoints.Nodes.Count);
					ClearSelectionWindow();
					SelectedSpawn = spawnPointNode.Spawn;
					DisplaySpawnDetails(SelectedSpawn);
				}
				foreach (SpawnPointNode node1 in tvwSpawnPoints.Nodes)
				{
					node1.Spawn.IsSelected = false;
				}
				ArrayList arrayLists = new ArrayList(tvwSpawnPoints.Nodes);
				arrayLists.Sort(new SpawnPointAreaComparer());
				foreach (SpawnPointNode arrayList in arrayLists)
				{
					if ((int)arrayList.Spawn.Map != (int)((WorldMap)cbxMap.SelectedItem) || arrayList.Filtered || !arrayList.Spawn.IsSameArea(mapX, mapY))
					{
						continue;
					}
					arrayList.Spawn.IsSelected = true;
					SelectedSpawn = arrayList.Spawn;
					SendGoCommand(arrayList.Spawn);
					DisplaySpawnDetails(SelectedSpawn);
					DisplaySpawnEntries();
					tvwSpawnPoints.SelectedNode = arrayList;
					tvwSpawnPoints.SelectedNode.EnsureVisible();
					SetSelectedSpawnTypes();
					break;
				}
				RefreshSpawnPoints();
			}
			else if (_SelectionWindow != null && _SelectionWindow.SX == mapX && _SelectionWindow.SY == mapY)
			{
				if (_SelectionWindow.Index > -1)
				{
					axUOMap.RemoveDrawRectAt(_SelectionWindow.Index);
					ClearSelectionWindow();
				}
				AssignCenter(mapX, mapY, (short)cbxMap.SelectedIndex);
				RefreshSpawnPoints();
			}
			trkZoom.Focus();
		}

		private void btnAddToSpawnPack_Click(object sender, EventArgs e)
		{
			clbSpawnPack.Sorted = false;
			foreach (string checkedItem in clbRunUOTypes.CheckedItems)
			{
				bool flag = false;
				foreach (string item in clbSpawnPack.Items)
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
				clbSpawnPack.Items.Add(checkedItem);
			}
			clbSpawnPack.Sorted = true;
		}

		private void btnConfigure_Click(object sender, EventArgs e)
		{
			UpdateMyLocation();
			RefreshSpawnPoints();
		}

		private void btnDeleteSpawn_Click(object sender, EventArgs e)
		{
			mniDeleteSpawn_Click(sender, e);
		}

		private void btnEntryEdit1_Click(object sender, EventArgs e)
		{
			EntryEdit entryEdit = new EntryEdit(this)
			{
				Text = base.Name
			};
			entryEdit.textEntryEdit.Text = entryText1.Text;
			if (entryEdit.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
			{
				entryText1.Text = entryEdit.textEntryEdit.Text;
			}
		}

		private void btnEntryEdit2_Click(object sender, EventArgs e)
		{
			EntryEdit entryEdit = new EntryEdit(this)
			{
				Text = base.Name
			};
			entryEdit.textEntryEdit.Text = entryText2.Text;
			if (entryEdit.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
			{
				entryText2.Text = entryEdit.textEntryEdit.Text;
			}
		}

		private void btnEntryEdit3_Click(object sender, EventArgs e)
		{
			EntryEdit entryEdit = new EntryEdit(this)
			{
				Text = base.Name
			};
			entryEdit.textEntryEdit.Text = entryText3.Text;
			if (entryEdit.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
			{
				entryText3.Text = entryEdit.textEntryEdit.Text;
			}
		}

		private void btnEntryEdit4_Click(object sender, EventArgs e)
		{
			EntryEdit entryEdit = new EntryEdit(this)
			{
				Text = base.Name
			};
			entryEdit.textEntryEdit.Text = entryText4.Text;
			if (entryEdit.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
			{
				entryText4.Text = entryEdit.textEntryEdit.Text;
			}
		}

		private void btnEntryEdit5_Click(object sender, EventArgs e)
		{
			EntryEdit entryEdit = new EntryEdit(this)
			{
				Text = base.Name
			};
			entryEdit.textEntryEdit.Text = entryText5.Text;
			if (entryEdit.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
			{
				entryText5.Text = entryEdit.textEntryEdit.Text;
			}
		}

		private void btnEntryEdit6_Click(object sender, EventArgs e)
		{
			EntryEdit entryEdit = new EntryEdit(this)
			{
				Text = base.Name
			};
			entryEdit.textEntryEdit.Text = entryText6.Text;
			if (entryEdit.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
			{
				entryText6.Text = entryEdit.textEntryEdit.Text;
			}
		}

		private void btnEntryEdit7_Click(object sender, EventArgs e)
		{
			EntryEdit entryEdit = new EntryEdit(this)
			{
				Text = base.Name
			};
			entryEdit.textEntryEdit.Text = entryText7.Text;
			if (entryEdit.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
			{
				entryText7.Text = entryEdit.textEntryEdit.Text;
			}
		}

		private void btnEntryEdit8_Click(object sender, EventArgs e)
		{
			EntryEdit entryEdit = new EntryEdit(this)
			{
				Text = base.Name
			};
			entryEdit.textEntryEdit.Text = entryText8.Text;
			if (entryEdit.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
			{
				entryText8.Text = entryEdit.textEntryEdit.Text;
			}
		}

		private void btnFilterSettings_Click(object sender, EventArgs e)
		{
			_SpawnerFilters.Show();
		}

		private void btnGo_Click(object sender, EventArgs e)
		{
			GoToSelected = true;
		}

		private void btnLoadSpawn_Click(object sender, EventArgs e)
		{
			try
			{
				ofdLoadFile.Title = "Load Spawn File";
				if (ofdLoadFile.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
				{
					Text = string.Concat("Spawn Editor 2 - ", ofdLoadFile.FileName);
					stbMain.Text = string.Format("Loading {0}...", ofdLoadFile.FileName);
					tvwSpawnPoints.Nodes.Clear();
					Refresh();
					LoadSpawnFile(ofdLoadFile.FileName, WorldMap.Internal);
				}
			}
			finally
			{
				stbMain.Text = "Finished loading spawn file.";
			}
			checkSpawnFilter.Checked = false;
		}

		private void btnMergeSpawn_Click(object sender, EventArgs e)
		{
			try
			{
				ofdLoadFile.Title = "Merge Spawn File";
				if (ofdLoadFile.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
				{
					Text = string.Concat("Spawn Editor 2 - ", ofdLoadFile.FileName);
					stbMain.Text = string.Format("Merging {0}...", ofdLoadFile.FileName);
					Refresh();
					LoadSpawnFile(ofdLoadFile.FileName, WorldMap.Internal);
				}
			}
			finally
			{
				stbMain.Text = "Finished merging spawn file.";
			}
			checkSpawnFilter.Checked = false;
		}

		private void btnMove_Click(object sender, EventArgs e)
		{
			SelectedSpawnNode = tvwSpawnPoints.SelectedNode as SpawnPointNode;
			SpawnObjectNode selectedNode = tvwSpawnPoints.SelectedNode as SpawnObjectNode;
			if (selectedNode != null)
			{
				SelectedSpawnNode = (SpawnPointNode)selectedNode.Parent;
			}
			if (SelectedSpawnNode != null)
			{
				Area area = new Area(SelectedSpawnNode.Spawn, this);
				area.ShowDialog(this);
			}
		}

		private void btnResetTypes_Click(object sender, EventArgs e)
		{
			clbRunUOTypes.ClearSelected();
			for (int i = 0; i < clbRunUOTypes.Items.Count; i++)
			{
				clbRunUOTypes.SetItemChecked(i, false);
			}
		}

		private void btnRestoreSpawnDefaults_Click(object sender, EventArgs e)
		{
			LoadDefaultSpawnValues();
		}

		private void btnSaveSpawn_Click(object sender, EventArgs e)
		{
			try
			{
				if (sfdSaveFile.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
				{
					Text = string.Concat("Spawn Editor 2 - ", sfdSaveFile.FileName);
					stbMain.Text = string.Format("Saving {0}...", ofdLoadFile.FileName);
					Refresh();
					SaveSpawnFile(sfdSaveFile.FileName);
				}
			}
			finally
			{
				stbMain.Text = "Finished saving spawn file.";
			}
		}

		private void btnSendSpawn_Click(object sender, EventArgs e)
		{
			if (tvwSpawnPoints.Nodes == null || tvwSpawnPoints.Nodes.Count <= 0)
			{
				return;
			}
			SpawnPoint selectedSpawn = null;
			int num = CountUnfilteredNodes();
			if (sender == btnSendSingleSpawner)
			{
				selectedSpawn = SelectedSpawn;
				num = 1;
			}
			UpdateSpawnDetails(SelectedSpawn);
			if (MessageBox.Show(this, string.Format("Send {0} spawners to Server {1}?", num, _TransferDialog.txtTransferServerAddress.Text), "Send Spawners to Server", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != System.Windows.Forms.DialogResult.OK)
			{
				return;
			}
			SaveSpawnerData saveSpawnerDatum = new SaveSpawnerData();
			MemoryStream memoryStream = new MemoryStream();
			SaveSpawnFile(memoryStream, "Memory Stream", selectedSpawn);
			saveSpawnerDatum.Data = memoryStream.GetBuffer();
			if (saveSpawnerDatum.Data == null)
			{
				MessageBox.Show(this, "No Spawners found.", "Empty Send", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return;
			}
			saveSpawnerDatum.AuthenticationID = SessionID;
			saveSpawnerDatum.UseMainThread = true;
			string text = _TransferDialog.txtTransferServerAddress.Text;
			int num1 = -1;
			try
			{
				num1 = int.Parse(_TransferDialog.txtTransferServerPort.Text);
			}
			catch
			{
			}
			_TransferDialog.DisplayStatusIndicator("Sending Spawners...");
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
			_TransferDialog.HideStatusIndicator();
		}

		private void btnSpawnPackClear(object sender, EventArgs e)
		{
			clbSpawnPack.ClearSelected();
			for (int i = 0; i < clbSpawnPack.Items.Count; i++)
			{
				clbSpawnPack.SetItemChecked(i, false);
			}
		}

		private void btnUpdateFromSpawnPack_Click(object sender, EventArgs e)
		{
			TreeNode selectedNode = tvwSpawnPoints.SelectedNode;
			SelectedSpawnNode = selectedNode as SpawnPointNode;
			SpawnObjectNode spawnObjectNode = selectedNode as SpawnObjectNode;
			if (spawnObjectNode != null)
			{
				SelectedSpawnNode = spawnObjectNode.Parent as SpawnPointNode;
			}
			if (SelectedSpawnNode != null)
			{
				SetSpawnFromSpawnPack(SelectedSpawnNode, true);
			}
			RefreshSpawnPoints();
		}

		private void btnUpdateSpawn_Click(object sender, EventArgs e)
		{
			TreeNode selectedNode = tvwSpawnPoints.SelectedNode;
			SelectedSpawnNode = selectedNode as SpawnPointNode;
			SpawnObjectNode spawnObjectNode = selectedNode as SpawnObjectNode;
			if (spawnObjectNode != null)
			{
				SelectedSpawnNode = spawnObjectNode.Parent as SpawnPointNode;
			}
			if (SelectedSpawnNode != null)
			{
				SetSpawn(SelectedSpawnNode, true);
			}
			RefreshSpawnPoints();
		}

		private void btnUpdateSpawnPacks_Click(object sender, EventArgs e)
		{
			UpdateSpawnPacks(textSpawnPackName.Text, clbSpawnPack.Items);
		}

		private void cbxMap_SelectedIndexChanged(object sender, EventArgs e)
		{
			ClearSelectionWindow();
			stbMain.Text = string.Concat(cbxMap.SelectedItem.ToString(), " Map Selected");
			stbMain.Refresh();
			int x = 0;
			int y = 0;
			switch ((int)((WorldMap)cbxMap.SelectedItem))
			{
				case 0:
				{
					axUOMap.MapFile = 0;
					x = MapLoc[0].X;
					y = MapLoc[0].Y;
					break;
				}
				case 1:
				{
                    axUOMap.MapFile = 1;
					x = MapLoc[1].X;
					y = MapLoc[1].Y;
					break;
				}
				case 2:
				{
                    axUOMap.MapFile = 2;
					x = MapLoc[2].X;
					y = MapLoc[2].Y;
					break;
				}
				case 3:
				{
                    axUOMap.MapFile = 3;
					x = MapLoc[3].X;
					y = MapLoc[3].Y;
					break;
				}
				case 4:
				{
                    axUOMap.MapFile = 4;
					x = MapLoc[4].X;
					y = MapLoc[4].Y;
					break;
				}
			}
			axUOMap.SetCenter((short)x, (short)y);
			//axUOMap.set_xCenter((short)x);
			//axUOMap.set_yCenter((short)y);
			RefreshSpawnPoints();
		}

		private void cbxShade_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (chkShade.Checked)
			{
				RefreshSpawnPoints();
			}
		}

		private void checkBox1_CheckedChanged(object sender, EventArgs e)
		{
			if (chkTracking.Checked)
			{
				Tracking = true;
				ActivateTracking();
				return;
			}
			Tracking = false;
			axUOMap.RemoveDrawObjects();
			RefreshSpawnPoints();
		}

		private void checkBox20_CheckedChanged(object sender, EventArgs e)
		{
		}

		private void checkSpawnFilter_CheckedChanged(object sender, EventArgs e)
		{
			if (checkSpawnFilter.Checked)
			{
				ApplySpawnFilter();
			}
			else
			{
				ClearSpawnFilter();
			}
			RefreshSpawnPoints();
		}

		private void chkDetails_CheckedChanged(object sender, EventArgs e)
		{
			if (!chkDetails.Checked)
			{
				SmallWindow();
				return;
			}
			if (axUOMap != null && tabControl2 != null && panel1 != null)
			{
				savewindowsize = base.Size;
				savemapsize = axUOMap.Size;
				savelistsize = tabControl2.Size;
				savepanelsize = panel1.Size;
			}
			LargeWindow();
		}

		private void chkDrawStatics_CheckedChanged(object sender, EventArgs e)
		{
			axUOMap.DrawStatics = chkDrawStatics.Checked;
		}

		private void chkInContainer_CheckedChanged(object sender, EventArgs e)
		{
			if (chkInContainer.Checked)
			{
				spnContainerX.Enabled = true;
				spnContainerY.Enabled = true;
				spnContainerZ.Enabled = true;
				labelContainerX.Enabled = true;
				labelContainerY.Enabled = true;
				labelContainerZ.Enabled = true;
				return;
			}
			spnContainerX.Enabled = false;
			spnContainerY.Enabled = false;
			spnContainerZ.Enabled = false;
			labelContainerX.Enabled = false;
			labelContainerY.Enabled = false;
			labelContainerZ.Enabled = false;
		}

		private void chkShade_CheckedChanged(object sender, EventArgs e)
		{
			RefreshSpawnPoints();
		}

		private void chkShowCreatures_CheckedChanged(object sender, EventArgs e)
		{
			RefreshSpawnPoints();
		}

		private void chkShowItems_CheckedChanged(object sender, EventArgs e)
		{
			RefreshSpawnPoints();
		}

		private void chkShowPlayers_CheckedChanged(object sender, EventArgs e)
		{
			RefreshSpawnPoints();
		}

		private void chkShowSpawns_CheckedChanged(object sender, EventArgs e)
		{
			RefreshSpawnPoints();
		}

		private void clbSpawnPack_MouseUp(object sender, MouseEventArgs e)
		{
			int num = clbSpawnPack.IndexFromPoint(e.X, e.Y);
			if (num >= 0)
			{
				clbSpawnPack.SelectedItem = clbSpawnPack.Items[num];
			}
			if (clbSpawnPack.SelectedItem is string && e.Button == System.Windows.Forms.MouseButtons.Right)
			{
				mcnSpawnPack.Show(clbSpawnPack, new Point(e.X, e.Y));
			}
		}

		private void ClearEntries()
		{
			entryText1.Text = null;
			entryText2.Text = null;
			entryText3.Text = null;
			entryText4.Text = null;
			entryText5.Text = null;
			entryText6.Text = null;
			entryText7.Text = null;
			entryText8.Text = null;
			entryMax1.Value = new decimal(0);
			entryMax2.Value = new decimal(0);
			entryMax3.Value = new decimal(0);
			entryMax4.Value = new decimal(0);
			entryMax5.Value = new decimal(0);
			entryMax6.Value = new decimal(0);
			entryMax7.Value = new decimal(0);
			entryMax8.Value = new decimal(0);
			entryPer1.Value = new decimal(0);
			entryPer2.Value = new decimal(0);
			entryPer3.Value = new decimal(0);
			entryPer4.Value = new decimal(0);
			entryPer5.Value = new decimal(0);
			entryPer6.Value = new decimal(0);
			entryPer7.Value = new decimal(0);
			entryPer8.Value = new decimal(0);
			entrySub1.Text = null;
			entrySub2.Text = null;
			entrySub3.Text = null;
			entrySub4.Text = null;
			entrySub5.Text = null;
			entrySub6.Text = null;
			entrySub7.Text = null;
			entrySub8.Text = null;
			entryReset1.Text = null;
			entryReset2.Text = null;
			entryReset3.Text = null;
			entryReset4.Text = null;
			entryReset5.Text = null;
			entryReset6.Text = null;
			entryReset7.Text = null;
			entryReset8.Text = null;
			entryTo1.Text = null;
			entryTo2.Text = null;
			entryTo3.Text = null;
			entryTo4.Text = null;
			entryTo5.Text = null;
			entryTo6.Text = null;
			entryTo7.Text = null;
			entryTo8.Text = null;
			entryKills1.Text = null;
			entryKills2.Text = null;
			entryKills3.Text = null;
			entryKills4.Text = null;
			entryKills5.Text = null;
			entryKills6.Text = null;
			entryKills7.Text = null;
			entryKills8.Text = null;
			entryMinD1.Text = null;
			entryMinD2.Text = null;
			entryMinD3.Text = null;
			entryMinD4.Text = null;
			entryMinD5.Text = null;
			entryMinD6.Text = null;
			entryMinD7.Text = null;
			entryMinD8.Text = null;
			entryMaxD1.Text = null;
			entryMaxD2.Text = null;
			entryMaxD3.Text = null;
			entryMaxD4.Text = null;
			entryMaxD5.Text = null;
			entryMaxD6.Text = null;
			entryMaxD7.Text = null;
			entryMaxD8.Text = null;
			chkRK1.Checked = false;
			chkRK2.Checked = false;
			chkRK3.Checked = false;
			chkRK4.Checked = false;
			chkRK5.Checked = false;
			chkRK6.Checked = false;
			chkRK7.Checked = false;
			chkRK8.Checked = false;
			chkClr1.Checked = false;
			chkClr2.Checked = false;
			chkClr3.Checked = false;
			chkClr4.Checked = false;
			chkClr5.Checked = false;
			chkClr6.Checked = false;
			chkClr7.Checked = false;
			chkClr8.Checked = false;
		}

		private void ClearSelectionWindow()
		{
			_SelectionWindow = null;
			EnableSelectionWindowOption(false);
		}

		internal void ClearSpawnFilter()
		{
			if (tvwSpawnPoints == null || tvwSpawnPoints.Nodes == null)
			{
				return;
			}
			foreach (SpawnPointNode node in tvwSpawnPoints.Nodes)
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
						ClearTreeColor(node, c);
					}
				}
			}
		}

		private void ClearTreeFacetSelection()
		{
			foreach (TreeNode node in treeRegionView.Nodes)
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
			if (!ControlModHash.Contains(key))
			{
				return false;
			}
			return (bool)ControlModHash[key];
		}

		private int CountUnfilteredNodes()
		{
			if (tvwSpawnPoints.Nodes == null || tvwSpawnPoints.Nodes.Count <= 0)
			{
				return 0;
			}
			int num = 0;
			for (int i = 0; i < tvwSpawnPoints.Nodes.Count; i++)
			{
				if (!((SpawnPointNode)tvwSpawnPoints.Nodes[i]).Filtered)
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
			if (tvwSpawnPoints.Nodes != null && MessageBox.Show(this, string.Format("Are you sure you want to delete ALL {0} spawns?", tvwSpawnPoints.Nodes.Count), "Delete All Spawns", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
			{
				tvwSpawnPoints.Nodes.Clear();
				SelectedSpawn = null;
				LoadDefaultSpawnValues();
				RefreshSpawnPoints();
			}
		}

		public void DisplayMyLocation()
		{
			if (cbxMap.SelectedIndex == MyLocation.Facet)
			{
				axUOMap.RemoveDrawObjects();
				axUOMap.AddDrawObject((short)MyLocation.X, (short)MyLocation.Y, 1, 12, 65280);
				axUOMap.AddDrawObject((short)MyLocation.X, (short)MyLocation.Y, 3, 2, 255);
			}
		}

		private void DisplaySpawnDetails(SpawnPoint Spawn)
		{
			if (Spawn == null)
			{
				return;
			}
			txtName.Text = Spawn.SpawnName;
			spnHomeRange.Value = Spawn.SpawnHomeRange;
			spnMaxCount.Value = Spawn.SpawnMaxCount;
			spnMinDelay.Value = (decimal)((double)Spawn.SpawnMinDelay);
			spnMaxDelay.Value = (decimal)((double)Spawn.SpawnMaxDelay);
			spnTeam.Value = Spawn.SpawnTeam;
			chkGroup.Checked = Spawn.SpawnIsGroup;
			chkRunning.Checked = Spawn.SpawnIsRunning;
			chkHomeRangeIsRelative.Checked = Spawn.SpawnHomeRangeIsRelative;
			spnSpawnRange.Value = Spawn.SpawnSpawnRange;
			spnProximityRange.Value = Spawn.SpawnProximityRange;
			spnDuration.Value = (decimal)((double)Spawn.SpawnDuration);
			spnDespawn.Value = (decimal)((double)Spawn.SpawnDespawn);
			spnMinRefract.Value = (decimal)((double)Spawn.SpawnMinRefract);
			spnMaxRefract.Value = (decimal)((double)Spawn.SpawnMaxRefract);
			spnTODStart.Value = (decimal)((double)Spawn.SpawnTODStart);
			spnTODEnd.Value = (decimal)((double)Spawn.SpawnTODEnd);
			spnKillReset.Value = Spawn.SpawnKillReset;
			spnProximitySnd.Value = Spawn.SpawnProximitySnd;
			chkAllowGhost.Checked = Spawn.SpawnAllowGhost;
			chkSpawnOnTrigger.Checked = Spawn.SpawnSpawnOnTrigger;
			chkSequentialSpawn.Checked = Spawn.SpawnSequentialSpawn > -1;
			chkSmartSpawning.Checked = Spawn.SpawnSmartSpawning;
			if (Spawn.SpawnTODMode != 0)
			{
				chkRealTOD.Checked = false;
				chkGameTOD.Checked = true;
			}
			else
			{
				chkRealTOD.Checked = true;
				chkGameTOD.Checked = false;
			}
			chkInContainer.Checked = Spawn.SpawnInContainer;
			textSkillTrigger.Text = Spawn.SpawnSkillTrigger;
			textSpeechTrigger.Text = Spawn.SpawnSpeechTrigger;
			textProximityMsg.Text = Spawn.SpawnProximityMsg;
			textMobTriggerName.Text = Spawn.SpawnMobTriggerName;
			textMobTrigProp.Text = Spawn.SpawnMobTrigProp;
			textPlayerTrigProp.Text = Spawn.SpawnPlayerTrigProp;
			textTrigObjectProp.Text = Spawn.SpawnTrigObjectProp;
			textTriggerOnCarried.Text = Spawn.SpawnTriggerOnCarried;
			textNoTriggerOnCarried.Text = Spawn.SpawnNoTriggerOnCarried;
			spnTriggerProbability.Value = (decimal)((double)Spawn.SpawnTriggerProbability);
			spnStackAmount.Value = Spawn.SpawnStackAmount;
			txtNotes.Text = Spawn.SpawnNotes;
			spnContainerX.Value = Spawn.SpawnContainerX;
			spnContainerY.Value = Spawn.SpawnContainerY;
			spnContainerZ.Value = Spawn.SpawnContainerZ;
			chkExternalTriggering.Checked = Spawn.SpawnExternalTriggering;
			textTrigObjectName.Text = Spawn.SpawnObjectPropertyItemName;
			textSetObjectName.Text = Spawn.SpawnSetPropertyItemName;
			textRegionName.Text = Spawn.SpawnRegionName;
			textConfigFile.Text = Spawn.SpawnConfigFile;
			textWayPoint.Text = Spawn.SpawnWaypoint;
		}

		private void DisplaySpawnEntries()
		{
			int num;
			ClearEntries();
			if (SelectedSpawn == null)
			{
				vScrollBar1.Maximum = 8;
				return;
			}
			if (SelectedSpawn.SpawnObjects == null)
			{
				vScrollBar1.Maximum = 8;
				return;
			}
			int value = vScrollBar1.Value;
			if (SelectedSpawn.SpawnObjects.Count > 7)
			{
				vScrollBar1.Maximum = SelectedSpawn.SpawnObjects.Count + 2;
			}
			int num1 = 0;
			int num2 = 0;
			IEnumerator enumerator = SelectedSpawn.SpawnObjects.GetEnumerator();
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
								entryText1.Text = current.TypeName;
								entryMax1.Value = current.Count;
								entryPer1.Value = current.SpawnsPerTick;
								entrySub1.Text = current.SubGroup.ToString();
								entryReset1.Text = current.SequentialResetTime.ToString();
								entryTo1.Text = current.SequentialResetTo.ToString();
								entryKills1.Text = current.KillsNeeded.ToString();
								if (current.MinDelay < 0)
								{
									entryMinD1.Text = null;
								}
								else
								{
									entryMinD1.Text = current.MinDelay.ToString();
								}
								if (current.MaxDelay < 0)
								{
									entryMaxD1.Text = null;
								}
								else
								{
									entryMaxD1.Text = current.MaxDelay.ToString();
								}
								chkRK1.Checked = current.RestrictKillsToSubgroup;
								chkClr1.Checked = current.ClearOnAdvance;
								break;
							}
							case 1:
							{
								entryText2.Text = current.TypeName;
								entryMax2.Value = current.Count;
								entryPer2.Value = current.SpawnsPerTick;
								entrySub2.Text = current.SubGroup.ToString();
								entryReset2.Text = current.SequentialResetTime.ToString();
								entryTo2.Text = current.SequentialResetTo.ToString();
								entryKills2.Text = current.KillsNeeded.ToString();
								if (current.MinDelay < 0)
								{
									entryMinD2.Text = null;
								}
								else
								{
									entryMinD2.Text = current.MinDelay.ToString();
								}
								if (current.MaxDelay < 0)
								{
									entryMaxD2.Text = null;
								}
								else
								{
									entryMaxD2.Text = current.MaxDelay.ToString();
								}
								chkRK2.Checked = current.RestrictKillsToSubgroup;
								chkClr2.Checked = current.ClearOnAdvance;
								break;
							}
							case 2:
							{
								entryText3.Text = current.TypeName;
								entryMax3.Value = current.Count;
								entryPer3.Value = current.SpawnsPerTick;
								entrySub3.Text = current.SubGroup.ToString();
								entryReset3.Text = current.SequentialResetTime.ToString();
								entryTo3.Text = current.SequentialResetTo.ToString();
								entryKills3.Text = current.KillsNeeded.ToString();
								if (current.MinDelay < 0)
								{
									entryMinD3.Text = null;
								}
								else
								{
									entryMinD3.Text = current.MinDelay.ToString();
								}
								if (current.MaxDelay < 0)
								{
									entryMaxD3.Text = null;
								}
								else
								{
									entryMaxD3.Text = current.MaxDelay.ToString();
								}
								chkRK3.Checked = current.RestrictKillsToSubgroup;
								chkClr3.Checked = current.ClearOnAdvance;
								break;
							}
							case 3:
							{
								entryText4.Text = current.TypeName;
								entryMax4.Value = current.Count;
								entryPer4.Value = current.SpawnsPerTick;
								entrySub4.Text = current.SubGroup.ToString();
								entryReset4.Text = current.SequentialResetTime.ToString();
								entryTo4.Text = current.SequentialResetTo.ToString();
								entryKills4.Text = current.KillsNeeded.ToString();
								if (current.MinDelay < 0)
								{
									entryMinD4.Text = null;
								}
								else
								{
									entryMinD4.Text = current.MinDelay.ToString();
								}
								if (current.MaxDelay < 0)
								{
									entryMaxD4.Text = null;
								}
								else
								{
									entryMaxD4.Text = current.MaxDelay.ToString();
								}
								chkRK4.Checked = current.RestrictKillsToSubgroup;
								chkClr4.Checked = current.ClearOnAdvance;
								break;
							}
							case 4:
							{
								entryText5.Text = current.TypeName;
								entryMax5.Value = current.Count;
								entryPer5.Value = current.SpawnsPerTick;
								entrySub5.Text = current.SubGroup.ToString();
								entryReset5.Text = current.SequentialResetTime.ToString();
								entryTo5.Text = current.SequentialResetTo.ToString();
								entryKills5.Text = current.KillsNeeded.ToString();
								if (current.MinDelay < 0)
								{
									entryMinD5.Text = null;
								}
								else
								{
									entryMinD5.Text = current.MinDelay.ToString();
								}
								if (current.MaxDelay < 0)
								{
									entryMaxD5.Text = null;
								}
								else
								{
									entryMaxD5.Text = current.MaxDelay.ToString();
								}
								chkRK5.Checked = current.RestrictKillsToSubgroup;
								chkClr5.Checked = current.ClearOnAdvance;
								break;
							}
							case 5:
							{
								entryText6.Text = current.TypeName;
								entryMax6.Value = current.Count;
								entryPer6.Value = current.SpawnsPerTick;
								entrySub6.Text = current.SubGroup.ToString();
								entryReset6.Text = current.SequentialResetTime.ToString();
								entryTo6.Text = current.SequentialResetTo.ToString();
								entryKills6.Text = current.KillsNeeded.ToString();
								if (current.MinDelay < 0)
								{
									entryMinD6.Text = null;
								}
								else
								{
									entryMinD6.Text = current.MinDelay.ToString();
								}
								if (current.MaxDelay < 0)
								{
									entryMaxD6.Text = null;
								}
								else
								{
									entryMaxD6.Text = current.MaxDelay.ToString();
								}
								chkRK6.Checked = current.RestrictKillsToSubgroup;
								chkClr6.Checked = current.ClearOnAdvance;
								break;
							}
							case 6:
							{
								entryText7.Text = current.TypeName;
								entryMax7.Value = current.Count;
								entryPer7.Value = current.SpawnsPerTick;
								entrySub7.Text = current.SubGroup.ToString();
								entryReset7.Text = current.SequentialResetTime.ToString();
								entryTo7.Text = current.SequentialResetTo.ToString();
								entryKills7.Text = current.KillsNeeded.ToString();
								if (current.MinDelay < 0)
								{
									entryMinD7.Text = null;
								}
								else
								{
									entryMinD7.Text = current.MinDelay.ToString();
								}
								if (current.MaxDelay < 0)
								{
									entryMaxD7.Text = null;
								}
								else
								{
									entryMaxD7.Text = current.MaxDelay.ToString();
								}
								chkRK7.Checked = current.RestrictKillsToSubgroup;
								chkClr7.Checked = current.ClearOnAdvance;
								break;
							}
							case 7:
							{
								entryText8.Text = current.TypeName;
								entryMax8.Value = current.Count;
								entryPer8.Value = current.SpawnsPerTick;
								entrySub8.Text = current.SubGroup.ToString();
								entryReset8.Text = current.SequentialResetTime.ToString();
								entryTo8.Text = current.SequentialResetTo.ToString();
								entryKills8.Text = current.KillsNeeded.ToString();
								if (current.MinDelay < 0)
								{
									entryMinD8.Text = null;
								}
								else
								{
									entryMinD8.Text = current.MinDelay.ToString();
								}
								if (current.MaxDelay < 0)
								{
									entryMaxD8.Text = null;
								}
								else
								{
									entryMaxD8.Text = current.MaxDelay.ToString();
								}
								chkRK8.Checked = current.RestrictKillsToSubgroup;
								chkClr8.Checked = current.ClearOnAdvance;
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
			if (disposing && components != null)
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void DoUnloadSpawners(SpawnPoint selectedspawn)
		{
			int num = CountUnfilteredNodes();
			if (selectedspawn != null)
			{
				num = 1;
			}
			if (MessageBox.Show(this, string.Format("Unload {0} spawners from Server {1}?", num, _TransferDialog.txtTransferServerAddress.Text), "Unload Spawners from Server", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != System.Windows.Forms.DialogResult.OK)
			{
				return;
			}
			UnloadSpawnerData unloadSpawnerDatum = new UnloadSpawnerData();
			MemoryStream memoryStream = new MemoryStream();
			SaveSpawnFile(memoryStream, "Memory Stream", selectedspawn);
			unloadSpawnerDatum.Data = memoryStream.GetBuffer();
			if (unloadSpawnerDatum.Data == null)
			{
				MessageBox.Show(this, "No Spawners found.", "Empty Unload", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return;
			}
			unloadSpawnerDatum.AuthenticationID = SessionID;
			unloadSpawnerDatum.UseMainThread = true;
			string text = _TransferDialog.txtTransferServerAddress.Text;
			int num1 = -1;
			try
			{
				num1 = int.Parse(_TransferDialog.txtTransferServerPort.Text);
			}
			catch
			{
			}
			_TransferDialog.DisplayStatusIndicator("Unloading Spawners...");
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
			_TransferDialog.HideStatusIndicator();
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
				tvwSpawnPoints.Sorted = false;
				DataSet dataSet = new DataSet("Spawns");
				dataSet.ReadXml(stream);
				RectangleConverter rectangleConverter = new RectangleConverter();
				progressBar1.Maximum = dataSet.Tables["Points"].Rows.Count;
				int num = 0;
				foreach (DataRow row in dataSet.Tables["Points"].Rows)
				{
					num++;
					progressBar1.Value = num;
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
					tvwSpawnPoints.Nodes.Add(spawnPointNode);
				}
				lblTotalSpawn.Text = string.Concat("Total Spawns = ", tvwSpawnPoints.Nodes.Count);
				txtName.Text = string.Concat(_CfgDialog.CfgSpawnNameValue, tvwSpawnPoints.Nodes.Count);
				tvwSpawnPoints.Sorted = true;
				RefreshSpawnPoints();
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				string[] filePath = new string[] { "Failed to load file [", FilePath, "] for the following reason:", Environment.NewLine, ExceptionMessage(exception) };
				MessageBox.Show(this, string.Concat(filePath), "Load Failure", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}

		internal void EnableSelectionWindowOption(bool enabled)
		{
			if (enabled)
			{
				_TransferDialog.chkSpawnerWithinSelectionWindow.Checked = true;
				_TransferDialog.chkSpawnerWithinSelectionWindow.Enabled = true;
				mniDeleteInSelectionWindow.Enabled = true;
				mniDeleteNotSelected.Enabled = true;
				mniModifyInSelectionWindow.Enabled = true;
				return;
			}
			_TransferDialog.chkSpawnerWithinSelectionWindow.Checked = false;
			_TransferDialog.chkSpawnerWithinSelectionWindow.Enabled = false;
			mniDeleteInSelectionWindow.Enabled = false;
			mniDeleteNotSelected.Enabled = false;
			mniModifyInSelectionWindow.Enabled = false;
		}

		private void entryMax1_Click(object sender, EventArgs e)
		{
			maxvaluechanged = true;
			changedmaxvalue = (int)entryMax2.Value;
		}

		private void entryMax1_KeyUp(object sender, KeyEventArgs e)
		{
			maxvaluechanged = true;
			changedmaxvalue = (int)entryMax1.Value;
		}

		private void entryMax1_Leave(object sender, EventArgs e)
		{
			if (maxvaluechanged)
			{
				UpdateSpawnEntries();
				UpdateSpawnerMaxCount();
				maxvaluechanged = false;
			}
		}

		private void entryMax1_ValueChanged(object sender, EventArgs e)
		{
		}

		private void entryMax2_Click(object sender, EventArgs e)
		{
			maxvaluechanged = true;
			changedmaxvalue = (int)entryMax2.Value;
		}

		private void entryMax2_KeyUp(object sender, KeyEventArgs e)
		{
			maxvaluechanged = true;
			changedmaxvalue = (int)entryMax1.Value;
		}

		private void entryMax2_Leave(object sender, EventArgs e)
		{
			if (maxvaluechanged)
			{
				UpdateSpawnEntries();
				UpdateSpawnerMaxCount();
				maxvaluechanged = false;
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
			maxvaluechanged = true;
			changedmaxvalue = (int)entryMax3.Value;
		}

		private void entryMax3_KeyUp(object sender, KeyEventArgs e)
		{
			maxvaluechanged = true;
			changedmaxvalue = (int)entryMax3.Value;
		}

		private void entryMax3_Leave(object sender, EventArgs e)
		{
			if (maxvaluechanged)
			{
				UpdateSpawnEntries();
				UpdateSpawnerMaxCount();
				maxvaluechanged = false;
			}
		}

		private void entryMax4_Click(object sender, EventArgs e)
		{
			maxvaluechanged = true;
			changedmaxvalue = (int)entryMax4.Value;
		}

		private void entryMax4_KeyUp(object sender, KeyEventArgs e)
		{
			maxvaluechanged = true;
			changedmaxvalue = (int)entryMax4.Value;
		}

		private void entryMax4_Leave(object sender, EventArgs e)
		{
			if (maxvaluechanged)
			{
				UpdateSpawnEntries();
				UpdateSpawnerMaxCount();
				maxvaluechanged = false;
			}
		}

		private void entryMax5_Click(object sender, EventArgs e)
		{
			maxvaluechanged = true;
			changedmaxvalue = (int)entryMax5.Value;
		}

		private void entryMax5_KeyUp(object sender, KeyEventArgs e)
		{
			maxvaluechanged = true;
			changedmaxvalue = (int)entryMax5.Value;
		}

		private void entryMax5_Leave(object sender, EventArgs e)
		{
			if (maxvaluechanged)
			{
				UpdateSpawnEntries();
				UpdateSpawnerMaxCount();
				maxvaluechanged = false;
			}
		}

		private void entryMax6_Click(object sender, EventArgs e)
		{
			maxvaluechanged = true;
			changedmaxvalue = (int)entryMax6.Value;
		}

		private void entryMax6_KeyUp(object sender, KeyEventArgs e)
		{
			maxvaluechanged = true;
			changedmaxvalue = (int)entryMax6.Value;
		}

		private void entryMax6_Leave(object sender, EventArgs e)
		{
			if (maxvaluechanged)
			{
				UpdateSpawnEntries();
				UpdateSpawnerMaxCount();
				maxvaluechanged = false;
			}
		}

		private void entryMax7_Click(object sender, EventArgs e)
		{
			maxvaluechanged = true;
			changedmaxvalue = (int)entryMax7.Value;
		}

		private void entryMax7_KeyUp(object sender, KeyEventArgs e)
		{
			maxvaluechanged = true;
			changedmaxvalue = (int)entryMax7.Value;
		}

		private void entryMax7_Leave(object sender, EventArgs e)
		{
			if (maxvaluechanged)
			{
				UpdateSpawnEntries();
				UpdateSpawnerMaxCount();
				maxvaluechanged = false;
			}
		}

		private void entryMax8_Click(object sender, EventArgs e)
		{
			maxvaluechanged = true;
			changedmaxvalue = (int)entryMax8.Value;
		}

		private void entryMax8_KeyUp(object sender, KeyEventArgs e)
		{
			maxvaluechanged = true;
			changedmaxvalue = (int)entryMax8.Value;
		}

		private void entryMax8_Leave(object sender, EventArgs e)
		{
			if (maxvaluechanged)
			{
				UpdateSpawnEntries();
				UpdateSpawnerMaxCount();
				maxvaluechanged = false;
			}
		}

		private void entryMax8_ValueChanged(object sender, EventArgs e)
		{
			if (entrySub8.Text == null || entrySub8.Text.Length == 0)
			{
				return;
			}
			int num = 0;
			try
			{
				num = int.Parse(entrySub8.Text);
			}
			catch
			{
			}
			if (num <= 0)
			{
				entryText8.ForeColor = grpSpawnEntries.ForeColor;
				entrySub8.ForeColor = grpSpawnEntries.ForeColor;
				return;
			}
			entryText8.ForeColor = Color.FromArgb(RandomColor(num));
			entrySub8.ForeColor = entryText8.ForeColor;
		}

		private void entrySub1_TextChanged(object sender, EventArgs e)
		{
			if (entrySub1.Text == null || entrySub1.Text.Length == 0)
			{
				return;
			}
			int num = 0;
			try
			{
				num = int.Parse(entrySub1.Text);
			}
			catch
			{
			}
			if (num <= 0)
			{
				entryText1.ForeColor = grpSpawnEntries.ForeColor;
				entrySub1.ForeColor = grpSpawnEntries.ForeColor;
				return;
			}
			entryText1.ForeColor = Color.FromArgb(RandomColor(num));
			entrySub1.ForeColor = entryText1.ForeColor;
		}

		private void entrySub2_TextChanged(object sender, EventArgs e)
		{
			if (entrySub2.Text == null || entrySub2.Text.Length == 0)
			{
				return;
			}
			int num = 0;
			try
			{
				num = int.Parse(entrySub2.Text);
			}
			catch
			{
			}
			if (num <= 0)
			{
				entryText2.ForeColor = grpSpawnEntries.ForeColor;
				entrySub2.ForeColor = grpSpawnEntries.ForeColor;
				return;
			}
			entryText2.ForeColor = Color.FromArgb(RandomColor(num));
			entrySub2.ForeColor = entryText2.ForeColor;
		}

		private void entrySub3_TextChanged(object sender, EventArgs e)
		{
			if (entrySub3.Text == null || entrySub3.Text.Length == 0)
			{
				return;
			}
			int num = 0;
			try
			{
				num = int.Parse(entrySub3.Text);
			}
			catch
			{
			}
			if (num <= 0)
			{
				entryText3.ForeColor = grpSpawnEntries.ForeColor;
				entrySub3.ForeColor = grpSpawnEntries.ForeColor;
				return;
			}
			entryText3.ForeColor = Color.FromArgb(RandomColor(num));
			entrySub3.ForeColor = entryText3.ForeColor;
		}

		private void entrySub4_TextChanged(object sender, EventArgs e)
		{
			if (entrySub4.Text == null || entrySub4.Text.Length == 0)
			{
				return;
			}
			int num = 0;
			try
			{
				num = int.Parse(entrySub4.Text);
			}
			catch
			{
			}
			if (num <= 0)
			{
				entryText4.ForeColor = grpSpawnEntries.ForeColor;
				entrySub4.ForeColor = grpSpawnEntries.ForeColor;
				return;
			}
			entryText4.ForeColor = Color.FromArgb(RandomColor(num));
			entrySub4.ForeColor = entryText4.ForeColor;
		}

		private void entrySub5_TextChanged(object sender, EventArgs e)
		{
			if (entrySub5.Text == null || entrySub5.Text.Length == 0)
			{
				return;
			}
			int num = 0;
			try
			{
				num = int.Parse(entrySub5.Text);
			}
			catch
			{
			}
			if (num <= 0)
			{
				entryText5.ForeColor = grpSpawnEntries.ForeColor;
				entrySub5.ForeColor = grpSpawnEntries.ForeColor;
				return;
			}
			entryText5.ForeColor = Color.FromArgb(RandomColor(num));
			entrySub5.ForeColor = entryText5.ForeColor;
		}

		private void entrySub6_TextChanged(object sender, EventArgs e)
		{
			if (entrySub6.Text == null || entrySub6.Text.Length == 0)
			{
				return;
			}
			int num = 0;
			try
			{
				num = int.Parse(entrySub6.Text);
			}
			catch
			{
			}
			if (num <= 0)
			{
				entryText6.ForeColor = grpSpawnEntries.ForeColor;
				entrySub6.ForeColor = grpSpawnEntries.ForeColor;
				return;
			}
			entryText6.ForeColor = Color.FromArgb(RandomColor(num));
			entrySub6.ForeColor = entryText6.ForeColor;
		}

		private void entrySub7_TextChanged(object sender, EventArgs e)
		{
			if (entrySub7.Text == null || entrySub7.Text.Length == 0)
			{
				return;
			}
			int num = 0;
			try
			{
				num = int.Parse(entrySub7.Text);
			}
			catch
			{
			}
			if (num <= 0)
			{
				entryText7.ForeColor = grpSpawnEntries.ForeColor;
				entrySub7.ForeColor = grpSpawnEntries.ForeColor;
				return;
			}
			entryText7.ForeColor = Color.FromArgb(RandomColor(num));
			entrySub7.ForeColor = entryText7.ForeColor;
		}

		private void entryText1_KeyUp(object sender, KeyEventArgs e)
		{
			entrychanged = 1;
			changedentrystring = entryText1.Text;
		}

		private void entryText1_MouseLeave(object sender, EventArgs e)
		{
			AddEntryOnChange();
		}

		private void entryText1_TextChanged(object sender, EventArgs e)
		{
		}

		private void entryText2_KeyUp(object sender, KeyEventArgs e)
		{
			entrychanged = 2;
			changedentrystring = entryText2.Text;
		}

		private void entryText2_MouseLeave(object sender, EventArgs e)
		{
			AddEntryOnChange();
		}

		private void entryText2_TextChanged(object sender, EventArgs e)
		{
		}

		private void entryText3_KeyUp(object sender, KeyEventArgs e)
		{
			entrychanged = 3;
			changedentrystring = entryText3.Text;
		}

		private void entryText3_MouseLeave(object sender, EventArgs e)
		{
			AddEntryOnChange();
		}

		private void entryText3_TextChanged(object sender, EventArgs e)
		{
		}

		private void entryText4_KeyUp(object sender, KeyEventArgs e)
		{
			entrychanged = 4;
			changedentrystring = entryText4.Text;
		}

		private void entryText4_MouseLeave(object sender, EventArgs e)
		{
			AddEntryOnChange();
		}

		private void entryText4_TextChanged(object sender, EventArgs e)
		{
		}

		private void entryText5_KeyUp(object sender, KeyEventArgs e)
		{
			entrychanged = 5;
			changedentrystring = entryText5.Text;
		}

		private void entryText5_MouseLeave(object sender, EventArgs e)
		{
			AddEntryOnChange();
		}

		private void entryText5_TextChanged(object sender, EventArgs e)
		{
		}

		private void entryText6_KeyUp(object sender, KeyEventArgs e)
		{
			entrychanged = 6;
			changedentrystring = entryText6.Text;
		}

		private void entryText6_MouseLeave(object sender, EventArgs e)
		{
			AddEntryOnChange();
		}

		private void entryText6_TextChanged(object sender, EventArgs e)
		{
		}

		private void entryText7_KeyUp(object sender, KeyEventArgs e)
		{
			entrychanged = 7;
			changedentrystring = entryText7.Text;
		}

		private void entryText7_MouseLeave(object sender, EventArgs e)
		{
			AddEntryOnChange();
		}

		private void entryText7_TextChanged(object sender, EventArgs e)
		{
		}

		private void entryText8_KeyUp(object sender, KeyEventArgs e)
		{
			entrychanged = 8;
			changedentrystring = entryText8.Text;
		}

		private void entryText8_MouseLeave(object sender, EventArgs e)
		{
			AddEntryOnChange();
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
			if (_ExtendedDiagnostics)
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
				for (int i = 0; i < clbRunUOTypes.Items.Count; i++)
				{
					xmlTextWriter.WriteStartElement("T");
					xmlTextWriter.WriteString(clbRunUOTypes.Items[i].ToString());
					xmlTextWriter.WriteEndElement();
				}
				xmlTextWriter.WriteEndElement();
				xmlTextWriter.Close();
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				string[] newLine = new string[] { "Failed to save Spawn Types file [", str, "] for the following reason:", Environment.NewLine, ExceptionMessage(exception) };
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
			treeGoView.Nodes.Add(new LocationNode(locationTree1));
			treeGoView.Nodes.Add(new LocationNode(locationTree));
			treeGoView.Nodes.Add(new LocationNode(locationTree2));
			treeGoView.Nodes.Add(new LocationNode(locationTree3));
			treeGoView.Nodes.Add(new LocationNode(locationTree4));
		}

		private void FillRegionTree()
		{
			treeRegionView.Nodes.Clear();
			for (int i = 0; i < 5; i++)
			{
				treeRegionView.Nodes.Add(new RegionFacetNode((WorldMap)i));
			}
			foreach (SpawnEditor2.Region region in SpawnEditor2.Region.Regions)
			{
				foreach (RegionFacetNode node in treeRegionView.Nodes)
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
					if (types.Length == 0)
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
							break;
						}
					}
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
			return vScrollBar1.Value + entrynum - 1;
		}

		private short GetZoomAdjustedSize(short defaultSize)
		{
			if (axUOMap.ZoomLevel == 0)
			{
                return defaultSize;
			}
			if (axUOMap.ZoomLevel > 0)
			{
                short num = (short)(Math.Pow(2, (double)axUOMap.ZoomLevel) * (double)defaultSize);
				return num;
			}
            short num1 = (short)(Math.Pow(2, (double)axUOMap.ZoomLevel) * (double)defaultSize);
			if (num1 > 0)
			{
				return num1;
			}
			return 1;
		}

		private void groupBox1_Leave(object sender, EventArgs e)
		{
			UpdateSpawnDetails(SelectedSpawn);
		}

		private void grpSpawnEdit_Leave(object sender, EventArgs e)
		{
			UpdateSpawnDetails(SelectedSpawn);
		}

		private void grpSpawnEntries_Enter(object sender, EventArgs e)
		{
		}

		private void grpSpawnEntries_Leave(object sender, EventArgs e)
		{
			UpdateSpawnEntries();
			UpdateSpawnNode();
		}

		private bool HasEntry(SpawnPoint Spawn, int entrynum)
		{
			if (Spawn == null || Spawn.SpawnObjects == null)
			{
				return false;
			}
			return vScrollBar1.Value + entrynum - 1 < Spawn.SpawnObjects.Count;
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
				if (!ControlModHash.Contains(name))
				{
					item = false;
					ControlModHash.Add(name, item);
				}
				else
				{
					item = (bool)ControlModHash[name];
				}
				ControlModHash[name] = !item;
				Color window = SystemColors.Window;
				if (sourceControl is CheckBox)
				{
					window = tabControl1.BackColor;
				}
				if ((bool)ControlModHash[name])
				{
					sourceControl.BackColor = Color.Yellow;
					return;
				}
				sourceControl.BackColor = window;
			}
		}

		public void ImportSpawnTypes(string filePath)
		{
			if (String.IsNullOrEmpty(filePath))
			{
				return;
			}
			if (File.Exists(filePath))
			{
				clbRunUOTypes.BeginUpdate();
				clbRunUOTypes.Sorted = false;
				clbRunUOTypes.Items.Clear();
				try
				{
					XmlDocument xmlDocument = new XmlDocument();
					xmlDocument.Load(filePath);
					XmlElement item = xmlDocument["SpawnTypes"];
					if (item != null)
					{
						foreach (XmlElement elementsByTagName in item.GetElementsByTagName("T"))
						{
							clbRunUOTypes.Items.Add(elementsByTagName.InnerText);
						}
					}
				}
				catch (Exception exception1)
				{
					Exception exception = exception1;
					string[] strArrays = new string[] { "Failed to read Spawn Types file [", filePath, "] for the following reason:", Environment.NewLine, ExceptionMessage(exception) };
					MessageBox.Show(this, string.Concat(strArrays), "Read Failure", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
				clbRunUOTypes.Sorted = true;
				clbRunUOTypes.EndUpdate();
				clbRunUOTypes.Refresh();
				lblTotalTypesLoaded.Text = string.Concat("Types Loaded = ", clbRunUOTypes.Items.Count);
			}
		}

		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SpawnEditor));
            this.axUOMap = new AxUOMAPLib.AxUOMap();
            this.ttpSpawnInfo = new System.Windows.Forms.ToolTip(this.components);
            this.btnSaveSpawn = new System.Windows.Forms.Button();
            this.btnLoadSpawn = new System.Windows.Forms.Button();
            this.mncLoad = new System.Windows.Forms.ContextMenu();
            this.trkZoom = new System.Windows.Forms.TrackBar();
            this.chkDrawStatics = new System.Windows.Forms.CheckBox();
            this.radShowMobilesOnly = new System.Windows.Forms.RadioButton();
            this.radShowItemsOnly = new System.Windows.Forms.RadioButton();
            this.radShowAll = new System.Windows.Forms.RadioButton();
            this.clbRunUOTypes = new System.Windows.Forms.CheckedListBox();
            this.tvwSpawnPoints = new System.Windows.Forms.TreeView();
            this.btnResetTypes = new System.Windows.Forms.Button();
            this.btnMergeSpawn = new System.Windows.Forms.Button();
            this.mncMerge = new System.Windows.Forms.ContextMenu();
            this.chkShowMapTip = new System.Windows.Forms.CheckBox();
            this.chkShowSpawns = new System.Windows.Forms.CheckBox();
            this.cbxMap = new System.Windows.Forms.ComboBox();
            this.chkSyncUO = new System.Windows.Forms.CheckBox();
            this.chkHomeRangeIsRelative = new System.Windows.Forms.CheckBox();
            this.highlightDetail = new System.Windows.Forms.ContextMenu();
            this.btnMove = new System.Windows.Forms.Button();
            this.btnRestoreSpawnDefaults = new System.Windows.Forms.Button();
            this.btnDeleteSpawn = new System.Windows.Forms.Button();
            this.btnUpdateSpawn = new System.Windows.Forms.Button();
            this.chkRunning = new System.Windows.Forms.CheckBox();
            this.spnMaxCount = new System.Windows.Forms.NumericUpDown();
            this.txtName = new System.Windows.Forms.TextBox();
            this.spnHomeRange = new System.Windows.Forms.NumericUpDown();
            this.spnMinDelay = new System.Windows.Forms.NumericUpDown();
            this.spnTeam = new System.Windows.Forms.NumericUpDown();
            this.chkGroup = new System.Windows.Forms.CheckBox();
            this.spnMaxDelay = new System.Windows.Forms.NumericUpDown();
            this.spnSpawnRange = new System.Windows.Forms.NumericUpDown();
            this.spnProximityRange = new System.Windows.Forms.NumericUpDown();
            this.spnMinRefract = new System.Windows.Forms.NumericUpDown();
            this.spnTODStart = new System.Windows.Forms.NumericUpDown();
            this.spnMaxRefract = new System.Windows.Forms.NumericUpDown();
            this.chkGameTOD = new System.Windows.Forms.CheckBox();
            this.chkRealTOD = new System.Windows.Forms.CheckBox();
            this.chkAllowGhost = new System.Windows.Forms.CheckBox();
            this.chkSmartSpawning = new System.Windows.Forms.CheckBox();
            this.chkSequentialSpawn = new System.Windows.Forms.CheckBox();
            this.chkSpawnOnTrigger = new System.Windows.Forms.CheckBox();
            this.spnDespawn = new System.Windows.Forms.NumericUpDown();
            this.spnTODEnd = new System.Windows.Forms.NumericUpDown();
            this.spnDuration = new System.Windows.Forms.NumericUpDown();
            this.spnProximitySnd = new System.Windows.Forms.NumericUpDown();
            this.spnKillReset = new System.Windows.Forms.NumericUpDown();
            this.chkTracking = new System.Windows.Forms.CheckBox();
            this.btnGo = new System.Windows.Forms.Button();
            this.chkInContainer = new System.Windows.Forms.CheckBox();
            this.spnTriggerProbability = new System.Windows.Forms.NumericUpDown();
            this.spnStackAmount = new System.Windows.Forms.NumericUpDown();
            this.chkExternalTriggering = new System.Windows.Forms.CheckBox();
            this.spnContainerX = new System.Windows.Forms.NumericUpDown();
            this.spnContainerY = new System.Windows.Forms.NumericUpDown();
            this.spnContainerZ = new System.Windows.Forms.NumericUpDown();
            this.chkLockSpawn = new System.Windows.Forms.CheckBox();
            this.chkDetails = new System.Windows.Forms.CheckBox();
            this.chkSnapRegion = new System.Windows.Forms.CheckBox();
            this.treeRegionView = new System.Windows.Forms.TreeView();
            this.treeGoView = new System.Windows.Forms.TreeView();
            this.checkSpawnFilter = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.clbSpawnPack = new System.Windows.Forms.CheckedListBox();
            this.btnUpdateFromSpawnPack = new System.Windows.Forms.Button();
            this.btnAddToSpawnPack = new System.Windows.Forms.Button();
            this.btnUpdateSpawnPacks = new System.Windows.Forms.Button();
            this.tvwSpawnPacks = new System.Windows.Forms.TreeView();
            this.chkShade = new System.Windows.Forms.CheckBox();
            this.cbxShade = new System.Windows.Forms.ComboBox();
            this.label28 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.lblMaxDelay = new System.Windows.Forms.Label();
            this.lblHomeRange = new System.Windows.Forms.Label();
            this.lblTeam = new System.Windows.Forms.Label();
            this.lblMaxCount = new System.Windows.Forms.Label();
            this.lblMinDelay = new System.Windows.Forms.Label();
            this.btnSendSpawn = new System.Windows.Forms.Button();
            this.unloadSpawners = new System.Windows.Forms.ContextMenu();
            this.btnFilterSettings = new System.Windows.Forms.Button();
            this.mniForceLoad = new System.Windows.Forms.MenuItem();
            this.menuItem21 = new System.Windows.Forms.MenuItem();
            this.mniForceMerge = new System.Windows.Forms.MenuItem();
            this.menuItem20 = new System.Windows.Forms.MenuItem();
            this.mniUnloadSpawners = new System.Windows.Forms.MenuItem();
            this.menuItem19 = new System.Windows.Forms.MenuItem();
            this.pnlControls = new System.Windows.Forms.Panel();
            this.lblTrkMax = new System.Windows.Forms.Label();
            this.lblTrkMin = new System.Windows.Forms.Label();
            this.tabControl3 = new System.Windows.Forms.TabControl();
            this.tabMapSettings = new System.Windows.Forms.TabPage();
            this.grpMapControl = new System.Windows.Forms.GroupBox();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.grpSpawnList = new System.Windows.Forms.GroupBox();
            this.lblTotalSpawn = new System.Windows.Forms.Label();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.lblTransferStatus = new System.Windows.Forms.Label();
            this.grpSpawnTypes = new System.Windows.Forms.GroupBox();
            this.lblTotalTypesLoaded = new System.Windows.Forms.Label();
            this.mncSpawns = new System.Windows.Forms.ContextMenu();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.mniDeleteSpawn = new System.Windows.Forms.MenuItem();
            this.mniDeleteAllSpawns = new System.Windows.Forms.MenuItem();
            this.ofdLoadFile = new System.Windows.Forms.OpenFileDialog();
            this.sfdSaveFile = new System.Windows.Forms.SaveFileDialog();
            this.stbMain = new System.Windows.Forms.StatusBar();
            this.deleteEntry = new System.Windows.Forms.ContextMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.menuItem15 = new System.Windows.Forms.MenuItem();
            this.editEntryMenu1 = new System.Windows.Forms.ContextMenu();
            this.grpSpawnEdit = new System.Windows.Forms.GroupBox();
            this.btnSendSingleSpawner = new System.Windows.Forms.Button();
            this.unloadSingleSpawner = new System.Windows.Forms.ContextMenu();
            this.label26 = new System.Windows.Forms.Label();
            this.textTrigObjectProp = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.textSkillTrigger = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.textSpeechTrigger = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.textProximityMsg = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.textPlayerTrigProp = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.textNoTriggerOnCarried = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.textTriggerOnCarried = new System.Windows.Forms.TextBox();
            this.mniUnloadSingleSpawner = new System.Windows.Forms.MenuItem();
            this.menuItem23 = new System.Windows.Forms.MenuItem();
            this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
            this.menuItem5 = new System.Windows.Forms.MenuItem();
            this.menuItem6 = new System.Windows.Forms.MenuItem();
            this.menuItem7 = new System.Windows.Forms.MenuItem();
            this.menuItem10 = new System.Windows.Forms.MenuItem();
            this.menuItem11 = new System.Windows.Forms.MenuItem();
            this.menuItem12 = new System.Windows.Forms.MenuItem();
            this.menuItem13 = new System.Windows.Forms.MenuItem();
            this.menuItem22 = new System.Windows.Forms.MenuItem();
            this.menuItem24 = new System.Windows.Forms.MenuItem();
            this.mniDeleteInSelectionWindow = new System.Windows.Forms.MenuItem();
            this.mniDeleteNotSelected = new System.Windows.Forms.MenuItem();
            this.mniToolbarDeleteAllSpawns = new System.Windows.Forms.MenuItem();
            this.mniDeleteAllFiltered = new System.Windows.Forms.MenuItem();
            this.mniDeleteAllUnfiltered = new System.Windows.Forms.MenuItem();
            this.menuItem25 = new System.Windows.Forms.MenuItem();
            this.mniModifyInSelectionWindow = new System.Windows.Forms.MenuItem();
            this.mniModifiedUnfiltered = new System.Windows.Forms.MenuItem();
            this.menuItem8 = new System.Windows.Forms.MenuItem();
            this.menuItem9 = new System.Windows.Forms.MenuItem();
            this.menuItem17 = new System.Windows.Forms.MenuItem();
            this.mniDisplayFilterSettings = new System.Windows.Forms.MenuItem();
            this.menuItem14 = new System.Windows.Forms.MenuItem();
            this.mniAlwaysOnTop = new System.Windows.Forms.MenuItem();
            this.menuItem16 = new System.Windows.Forms.MenuItem();
            this.menuItem18 = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabBasic = new System.Windows.Forms.TabPage();
            this.tabAdvanced = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label44 = new System.Windows.Forms.Label();
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.label37 = new System.Windows.Forms.Label();
            this.textRegionName = new System.Windows.Forms.TextBox();
            this.label36 = new System.Windows.Forms.Label();
            this.textWayPoint = new System.Windows.Forms.TextBox();
            this.label35 = new System.Windows.Forms.Label();
            this.textConfigFile = new System.Windows.Forms.TextBox();
            this.label34 = new System.Windows.Forms.Label();
            this.textSetObjectName = new System.Windows.Forms.TextBox();
            this.label33 = new System.Windows.Forms.Label();
            this.textTrigObjectName = new System.Windows.Forms.TextBox();
            this.labelContainerZ = new System.Windows.Forms.Label();
            this.labelContainerY = new System.Windows.Forms.Label();
            this.labelContainerX = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.textMobTriggerName = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.textMobTrigProp = new System.Windows.Forms.TextBox();
            this.tabSpawnTypes = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textSpawnPackName = new System.Windows.Forms.TextBox();
            this.label39 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.mcnSpawnPack = new System.Windows.Forms.ContextMenu();
            this.mniDeleteType = new System.Windows.Forms.MenuItem();
            this.mniDeleteAllTypes = new System.Windows.Forms.MenuItem();
            this.mcnSpawnPacks = new System.Windows.Forms.ContextMenu();
            this.mniDeletePack = new System.Windows.Forms.MenuItem();
            this.openSpawnPacks = new System.Windows.Forms.OpenFileDialog();
            this.saveSpawnPacks = new System.Windows.Forms.SaveFileDialog();
            this.exportAllSpawnTypes = new System.Windows.Forms.SaveFileDialog();
            this.importAllSpawnTypes = new System.Windows.Forms.OpenFileDialog();
            this.importMapFile = new System.Windows.Forms.OpenFileDialog();
            this.importMSFFile = new System.Windows.Forms.OpenFileDialog();
            this.grpSpawnEntries = new System.Windows.Forms.GroupBox();
            this.entryPer8 = new System.Windows.Forms.NumericUpDown();
            this.entryPer7 = new System.Windows.Forms.NumericUpDown();
            this.entryPer6 = new System.Windows.Forms.NumericUpDown();
            this.entryPer5 = new System.Windows.Forms.NumericUpDown();
            this.entryPer4 = new System.Windows.Forms.NumericUpDown();
            this.entryPer3 = new System.Windows.Forms.NumericUpDown();
            this.entryPer2 = new System.Windows.Forms.NumericUpDown();
            this.entryPer1 = new System.Windows.Forms.NumericUpDown();
            this.label30 = new System.Windows.Forms.Label();
            this.entryMaxD8 = new System.Windows.Forms.TextBox();
            this.entryMaxD7 = new System.Windows.Forms.TextBox();
            this.entryMaxD6 = new System.Windows.Forms.TextBox();
            this.entryMaxD5 = new System.Windows.Forms.TextBox();
            this.entryMaxD4 = new System.Windows.Forms.TextBox();
            this.entryMaxD3 = new System.Windows.Forms.TextBox();
            this.entryMaxD2 = new System.Windows.Forms.TextBox();
            this.entryMaxD1 = new System.Windows.Forms.TextBox();
            this.entryMinD8 = new System.Windows.Forms.TextBox();
            this.entryMinD7 = new System.Windows.Forms.TextBox();
            this.entryMinD6 = new System.Windows.Forms.TextBox();
            this.entryMinD5 = new System.Windows.Forms.TextBox();
            this.entryMinD4 = new System.Windows.Forms.TextBox();
            this.entryMinD3 = new System.Windows.Forms.TextBox();
            this.entryMinD2 = new System.Windows.Forms.TextBox();
            this.entryMinD1 = new System.Windows.Forms.TextBox();
            this.entryKills8 = new System.Windows.Forms.TextBox();
            this.entryKills7 = new System.Windows.Forms.TextBox();
            this.entryKills6 = new System.Windows.Forms.TextBox();
            this.entryKills5 = new System.Windows.Forms.TextBox();
            this.entryKills4 = new System.Windows.Forms.TextBox();
            this.entryKills3 = new System.Windows.Forms.TextBox();
            this.entryKills2 = new System.Windows.Forms.TextBox();
            this.entryKills1 = new System.Windows.Forms.TextBox();
            this.entryReset8 = new System.Windows.Forms.TextBox();
            this.entryReset7 = new System.Windows.Forms.TextBox();
            this.entryReset6 = new System.Windows.Forms.TextBox();
            this.entryReset5 = new System.Windows.Forms.TextBox();
            this.entryReset4 = new System.Windows.Forms.TextBox();
            this.entryReset3 = new System.Windows.Forms.TextBox();
            this.entryReset2 = new System.Windows.Forms.TextBox();
            this.entryReset1 = new System.Windows.Forms.TextBox();
            this.entryTo8 = new System.Windows.Forms.TextBox();
            this.entrySub8 = new System.Windows.Forms.TextBox();
            this.chkRK8 = new System.Windows.Forms.CheckBox();
            this.entryMax8 = new System.Windows.Forms.NumericUpDown();
            this.btnEntryEdit8 = new System.Windows.Forms.Button();
            this.entryText8 = new System.Windows.Forms.TextBox();
            this.chkClr8 = new System.Windows.Forms.CheckBox();
            this.entryTo7 = new System.Windows.Forms.TextBox();
            this.entrySub7 = new System.Windows.Forms.TextBox();
            this.chkRK7 = new System.Windows.Forms.CheckBox();
            this.entryMax7 = new System.Windows.Forms.NumericUpDown();
            this.btnEntryEdit7 = new System.Windows.Forms.Button();
            this.entryText7 = new System.Windows.Forms.TextBox();
            this.chkClr7 = new System.Windows.Forms.CheckBox();
            this.entryTo6 = new System.Windows.Forms.TextBox();
            this.entrySub6 = new System.Windows.Forms.TextBox();
            this.chkRK6 = new System.Windows.Forms.CheckBox();
            this.entryMax6 = new System.Windows.Forms.NumericUpDown();
            this.btnEntryEdit6 = new System.Windows.Forms.Button();
            this.entryText6 = new System.Windows.Forms.TextBox();
            this.chkClr6 = new System.Windows.Forms.CheckBox();
            this.entryTo5 = new System.Windows.Forms.TextBox();
            this.entrySub5 = new System.Windows.Forms.TextBox();
            this.chkRK5 = new System.Windows.Forms.CheckBox();
            this.entryMax5 = new System.Windows.Forms.NumericUpDown();
            this.btnEntryEdit5 = new System.Windows.Forms.Button();
            this.entryText5 = new System.Windows.Forms.TextBox();
            this.chkClr5 = new System.Windows.Forms.CheckBox();
            this.entryTo4 = new System.Windows.Forms.TextBox();
            this.entrySub4 = new System.Windows.Forms.TextBox();
            this.chkRK4 = new System.Windows.Forms.CheckBox();
            this.entryMax4 = new System.Windows.Forms.NumericUpDown();
            this.btnEntryEdit4 = new System.Windows.Forms.Button();
            this.entryText4 = new System.Windows.Forms.TextBox();
            this.chkClr4 = new System.Windows.Forms.CheckBox();
            this.entryTo3 = new System.Windows.Forms.TextBox();
            this.entrySub3 = new System.Windows.Forms.TextBox();
            this.chkRK3 = new System.Windows.Forms.CheckBox();
            this.entryMax3 = new System.Windows.Forms.NumericUpDown();
            this.btnEntryEdit3 = new System.Windows.Forms.Button();
            this.entryText3 = new System.Windows.Forms.TextBox();
            this.chkClr3 = new System.Windows.Forms.CheckBox();
            this.entryTo2 = new System.Windows.Forms.TextBox();
            this.entrySub2 = new System.Windows.Forms.TextBox();
            this.chkRK2 = new System.Windows.Forms.CheckBox();
            this.entryMax2 = new System.Windows.Forms.NumericUpDown();
            this.btnEntryEdit2 = new System.Windows.Forms.Button();
            this.entryText2 = new System.Windows.Forms.TextBox();
            this.chkClr2 = new System.Windows.Forms.CheckBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.entryTo1 = new System.Windows.Forms.TextBox();
            this.vScrollBar1 = new System.Windows.Forms.VScrollBar();
            this.entrySub1 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.chkRK1 = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.entryMax1 = new System.Windows.Forms.NumericUpDown();
            this.btnEntryEdit1 = new System.Windows.Forms.Button();
            this.entryText1 = new System.Windows.Forms.TextBox();
            this.chkClr1 = new System.Windows.Forms.CheckBox();
            this.groupTemplateList = new System.Windows.Forms.GroupBox();
            this.btnSaveTemplate = new System.Windows.Forms.Button();
            this.btnMergeTemplate = new System.Windows.Forms.Button();
            this.btnLoadTemplate = new System.Windows.Forms.Button();
            this.tvwTemplates = new System.Windows.Forms.TreeView();
            this.label29 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.axUOMap)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkZoom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spnMaxCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spnHomeRange)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spnMinDelay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spnTeam)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spnMaxDelay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spnSpawnRange)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spnProximityRange)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spnMinRefract)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spnTODStart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spnMaxRefract)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spnDespawn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spnTODEnd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spnDuration)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spnProximitySnd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spnKillReset)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spnTriggerProbability)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spnStackAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spnContainerX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spnContainerY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spnContainerZ)).BeginInit();
            this.pnlControls.SuspendLayout();
            this.tabControl3.SuspendLayout();
            this.tabMapSettings.SuspendLayout();
            this.grpMapControl.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.grpSpawnList.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.grpSpawnTypes.SuspendLayout();
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
            this.grpSpawnEntries.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.entryPer8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.entryPer7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.entryPer6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.entryPer5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.entryPer4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.entryPer3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.entryPer2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.entryPer1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.entryMax8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.entryMax7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.entryMax6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.entryMax5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.entryMax4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.entryMax3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.entryMax2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.entryMax1)).BeginInit();
            this.groupTemplateList.SuspendLayout();
            this.SuspendLayout();
            // 
            // axUOMap
            // 
            this.axUOMap.Enabled = true;
            this.axUOMap.Location = new System.Drawing.Point(0, 0);
            this.axUOMap.Name = "axUOMap";
            this.axUOMap.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axUOMap.OcxState")));
            this.axUOMap.Size = new System.Drawing.Size(100, 50);
            this.axUOMap.TabIndex = 9;
            // 
            // ttpSpawnInfo
            // 
            this.ttpSpawnInfo.AutoPopDelay = 5000;
            this.ttpSpawnInfo.InitialDelay = 500;
            this.ttpSpawnInfo.ReshowDelay = 100;
            // 
            // btnSaveSpawn
            // 
            this.btnSaveSpawn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveSpawn.Location = new System.Drawing.Point(112, 32);
            this.btnSaveSpawn.Name = "btnSaveSpawn";
            this.btnSaveSpawn.Size = new System.Drawing.Size(48, 24);
            this.btnSaveSpawn.TabIndex = 2;
            this.btnSaveSpawn.Text = "&Save";
            this.ttpSpawnInfo.SetToolTip(this.btnSaveSpawn, "Saves the current spawn list.");
            this.btnSaveSpawn.Click += new System.EventHandler(this.btnSaveSpawn_Click);
            // 
            // btnLoadSpawn
            // 
            this.btnLoadSpawn.ContextMenu = this.mncLoad;
            this.btnLoadSpawn.Location = new System.Drawing.Point(8, 32);
            this.btnLoadSpawn.Name = "btnLoadSpawn";
            this.btnLoadSpawn.Size = new System.Drawing.Size(40, 24);
            this.btnLoadSpawn.TabIndex = 0;
            this.btnLoadSpawn.Text = "&Load";
            this.ttpSpawnInfo.SetToolTip(this.btnLoadSpawn, resources.GetString("btnLoadSpawn.ToolTip"));
            this.btnLoadSpawn.Click += new System.EventHandler(this.btnLoadSpawn_Click);
            // 
            // mncLoad
            // 
            this.mncLoad.Popup += new System.EventHandler(this.mncLoad_Popup);
            // 
            // trkZoom
            // 
            this.trkZoom.AutoSize = false;
            this.trkZoom.LargeChange = 2;
            this.trkZoom.Location = new System.Drawing.Point(16, 168);
            this.trkZoom.Maximum = 4;
            this.trkZoom.Minimum = -4;
            this.trkZoom.Name = "trkZoom";
            this.trkZoom.Size = new System.Drawing.Size(152, 32);
            this.trkZoom.TabIndex = 5;
            this.trkZoom.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.ttpSpawnInfo.SetToolTip(this.trkZoom, "Zooms in/out of map.");
            this.trkZoom.Scroll += new System.EventHandler(this.trkZoom_Scroll);
            this.trkZoom.ValueChanged += new System.EventHandler(this.trkZoom_ValueChanged);
            // 
            // chkDrawStatics
            // 
            this.chkDrawStatics.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkDrawStatics.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkDrawStatics.Location = new System.Drawing.Point(77, 8);
            this.chkDrawStatics.Name = "chkDrawStatics";
            this.chkDrawStatics.Size = new System.Drawing.Size(80, 16);
            this.chkDrawStatics.TabIndex = 1;
            this.chkDrawStatics.Text = "Statics";
            this.ttpSpawnInfo.SetToolTip(this.chkDrawStatics, "Draws static tiles on the map.");
            this.chkDrawStatics.CheckedChanged += new System.EventHandler(this.chkDrawStatics_CheckedChanged);
            // 
            // radShowMobilesOnly
            // 
            this.radShowMobilesOnly.Location = new System.Drawing.Point(104, 16);
            this.radShowMobilesOnly.Name = "radShowMobilesOnly";
            this.radShowMobilesOnly.Size = new System.Drawing.Size(64, 16);
            this.radShowMobilesOnly.TabIndex = 2;
            this.radShowMobilesOnly.Text = "Mobiles";
            this.ttpSpawnInfo.SetToolTip(this.radShowMobilesOnly, "Shows only mobile based spawn objects.");
            this.radShowMobilesOnly.CheckedChanged += new System.EventHandler(this.TypeSelectionChanged);
            // 
            // radShowItemsOnly
            // 
            this.radShowItemsOnly.Location = new System.Drawing.Point(56, 16);
            this.radShowItemsOnly.Name = "radShowItemsOnly";
            this.radShowItemsOnly.Size = new System.Drawing.Size(64, 16);
            this.radShowItemsOnly.TabIndex = 1;
            this.radShowItemsOnly.Text = "Items";
            this.ttpSpawnInfo.SetToolTip(this.radShowItemsOnly, "Shows only item based spawn objects.");
            this.radShowItemsOnly.CheckedChanged += new System.EventHandler(this.TypeSelectionChanged);
            // 
            // radShowAll
            // 
            this.radShowAll.Checked = true;
            this.radShowAll.Location = new System.Drawing.Point(8, 16);
            this.radShowAll.Name = "radShowAll";
            this.radShowAll.Size = new System.Drawing.Size(56, 16);
            this.radShowAll.TabIndex = 0;
            this.radShowAll.TabStop = true;
            this.radShowAll.Text = "All";
            this.ttpSpawnInfo.SetToolTip(this.radShowAll, "Shows all types of spawn objects (items/mobiles).");
            this.radShowAll.CheckedChanged += new System.EventHandler(this.TypeSelectionChanged);
            // 
            // clbRunUOTypes
            // 
            this.clbRunUOTypes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.clbRunUOTypes.CheckOnClick = true;
            this.clbRunUOTypes.HorizontalScrollbar = true;
            this.clbRunUOTypes.IntegralHeight = false;
            this.clbRunUOTypes.Location = new System.Drawing.Point(8, 96);
            this.clbRunUOTypes.Name = "clbRunUOTypes";
            this.clbRunUOTypes.Size = new System.Drawing.Size(160, 320);
            this.clbRunUOTypes.TabIndex = 4;
            this.clbRunUOTypes.ThreeDCheckBoxes = true;
            this.ttpSpawnInfo.SetToolTip(this.clbRunUOTypes, "List of all spawnable objects.");
            // 
            // tvwSpawnPoints
            // 
            this.tvwSpawnPoints.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tvwSpawnPoints.Location = new System.Drawing.Point(8, 80);
            this.tvwSpawnPoints.Name = "tvwSpawnPoints";
            this.tvwSpawnPoints.Size = new System.Drawing.Size(152, 348);
            this.tvwSpawnPoints.TabIndex = 3;
            this.ttpSpawnInfo.SetToolTip(this.tvwSpawnPoints, "List of currently defined spawns.  Right-Click for a context menu based on the cu" +
        "rrently selected spawn.");
            this.tvwSpawnPoints.MouseUp += new System.Windows.Forms.MouseEventHandler(this.tvwSpawnPoints_MouseUp);
            // 
            // btnResetTypes
            // 
            this.btnResetTypes.Location = new System.Drawing.Point(8, 35);
            this.btnResetTypes.Name = "btnResetTypes";
            this.btnResetTypes.Size = new System.Drawing.Size(160, 20);
            this.btnResetTypes.TabIndex = 3;
            this.btnResetTypes.Text = "&Clear Selections";
            this.ttpSpawnInfo.SetToolTip(this.btnResetTypes, "Clears current selections from the type list.");
            this.btnResetTypes.Click += new System.EventHandler(this.btnResetTypes_Click);
            // 
            // btnMergeSpawn
            // 
            this.btnMergeSpawn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMergeSpawn.ContextMenu = this.mncMerge;
            this.btnMergeSpawn.Location = new System.Drawing.Point(48, 32);
            this.btnMergeSpawn.Name = "btnMergeSpawn";
            this.btnMergeSpawn.Size = new System.Drawing.Size(64, 24);
            this.btnMergeSpawn.TabIndex = 1;
            this.btnMergeSpawn.Text = "&Merge";
            this.ttpSpawnInfo.SetToolTip(this.btnMergeSpawn, resources.GetString("btnMergeSpawn.ToolTip"));
            this.btnMergeSpawn.Click += new System.EventHandler(this.btnMergeSpawn_Click);
            // 
            // chkShowMapTip
            // 
            this.chkShowMapTip.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkShowMapTip.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkShowMapTip.Checked = true;
            this.chkShowMapTip.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowMapTip.Location = new System.Drawing.Point(77, 24);
            this.chkShowMapTip.Name = "chkShowMapTip";
            this.chkShowMapTip.Size = new System.Drawing.Size(80, 16);
            this.chkShowMapTip.TabIndex = 2;
            this.chkShowMapTip.Text = "Spawn Tip";
            this.ttpSpawnInfo.SetToolTip(this.chkShowMapTip, "Turns on/off the spawn tool tip when hovering over a spawn.");
            // 
            // chkShowSpawns
            // 
            this.chkShowSpawns.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkShowSpawns.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkShowSpawns.Checked = true;
            this.chkShowSpawns.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowSpawns.Location = new System.Drawing.Point(77, 40);
            this.chkShowSpawns.Name = "chkShowSpawns";
            this.chkShowSpawns.Size = new System.Drawing.Size(80, 16);
            this.chkShowSpawns.TabIndex = 3;
            this.chkShowSpawns.Text = "Spawns";
            this.ttpSpawnInfo.SetToolTip(this.chkShowSpawns, "Turns on/off drawing of spawn points.");
            this.chkShowSpawns.CheckedChanged += new System.EventHandler(this.chkShowSpawns_CheckedChanged);
            // 
            // cbxMap
            // 
            this.cbxMap.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxMap.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxMap.Location = new System.Drawing.Point(85, 80);
            this.cbxMap.Name = "cbxMap";
            this.cbxMap.Size = new System.Drawing.Size(77, 21);
            this.cbxMap.TabIndex = 4;
            this.ttpSpawnInfo.SetToolTip(this.cbxMap, "Changes the current map.");
            this.cbxMap.SelectedIndexChanged += new System.EventHandler(this.cbxMap_SelectedIndexChanged);
            // 
            // chkSyncUO
            // 
            this.chkSyncUO.Location = new System.Drawing.Point(8, 64);
            this.chkSyncUO.Name = "chkSyncUO";
            this.chkSyncUO.Size = new System.Drawing.Size(48, 16);
            this.chkSyncUO.TabIndex = 6;
            this.chkSyncUO.Text = "Sync:";
            this.ttpSpawnInfo.SetToolTip(this.chkSyncUO, "Automatically move player to spawner locations when they are selected.");
            // 
            // chkHomeRangeIsRelative
            // 
            this.chkHomeRangeIsRelative.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkHomeRangeIsRelative.Checked = true;
            this.chkHomeRangeIsRelative.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkHomeRangeIsRelative.ContextMenu = this.highlightDetail;
            this.chkHomeRangeIsRelative.Location = new System.Drawing.Point(8, 216);
            this.chkHomeRangeIsRelative.Name = "chkHomeRangeIsRelative";
            this.chkHomeRangeIsRelative.Size = new System.Drawing.Size(104, 16);
            this.chkHomeRangeIsRelative.TabIndex = 13;
            this.chkHomeRangeIsRelative.Text = "RelativeHome:";
            this.ttpSpawnInfo.SetToolTip(this.chkHomeRangeIsRelative, "Check if the object to be spawned should set its home point base on its spawned l" +
        "ocation and not the spawners location.");
            // 
            // highlightDetail
            // 
            this.highlightDetail.Popup += new System.EventHandler(this.highlightDetail_Popup);
            // 
            // btnMove
            // 
            this.btnMove.Enabled = false;
            this.btnMove.Location = new System.Drawing.Point(192, 408);
            this.btnMove.Name = "btnMove";
            this.btnMove.Size = new System.Drawing.Size(32, 23);
            this.btnMove.TabIndex = 17;
            this.btnMove.Text = "&XY";
            this.ttpSpawnInfo.SetToolTip(this.btnMove, "Adjusted the spawners boundaries.");
            this.btnMove.Click += new System.EventHandler(this.btnMove_Click);
            // 
            // btnRestoreSpawnDefaults
            // 
            this.btnRestoreSpawnDefaults.Location = new System.Drawing.Point(8, 408);
            this.btnRestoreSpawnDefaults.Name = "btnRestoreSpawnDefaults";
            this.btnRestoreSpawnDefaults.Size = new System.Drawing.Size(96, 23);
            this.btnRestoreSpawnDefaults.TabIndex = 14;
            this.btnRestoreSpawnDefaults.Text = "Restore Defaults";
            this.ttpSpawnInfo.SetToolTip(this.btnRestoreSpawnDefaults, "Restores the spawn details to the default values.");
            this.btnRestoreSpawnDefaults.Click += new System.EventHandler(this.btnRestoreSpawnDefaults_Click);
            // 
            // btnDeleteSpawn
            // 
            this.btnDeleteSpawn.Enabled = false;
            this.btnDeleteSpawn.Location = new System.Drawing.Point(104, 408);
            this.btnDeleteSpawn.Name = "btnDeleteSpawn";
            this.btnDeleteSpawn.Size = new System.Drawing.Size(88, 23);
            this.btnDeleteSpawn.TabIndex = 16;
            this.btnDeleteSpawn.Text = "&Delete Spawn";
            this.ttpSpawnInfo.SetToolTip(this.btnDeleteSpawn, "Deletes the currently selected spawn.");
            this.btnDeleteSpawn.Click += new System.EventHandler(this.btnDeleteSpawn_Click);
            // 
            // btnUpdateSpawn
            // 
            this.btnUpdateSpawn.Enabled = false;
            this.btnUpdateSpawn.Location = new System.Drawing.Point(8, 55);
            this.btnUpdateSpawn.Name = "btnUpdateSpawn";
            this.btnUpdateSpawn.Size = new System.Drawing.Size(160, 20);
            this.btnUpdateSpawn.TabIndex = 15;
            this.btnUpdateSpawn.Text = "&Add to Spawner";
            this.ttpSpawnInfo.SetToolTip(this.btnUpdateSpawn, "Updates the currently selected spawn with the selected types.");
            this.btnUpdateSpawn.Click += new System.EventHandler(this.btnUpdateSpawn_Click);
            // 
            // chkRunning
            // 
            this.chkRunning.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkRunning.Checked = true;
            this.chkRunning.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkRunning.ContextMenu = this.highlightDetail;
            this.chkRunning.Location = new System.Drawing.Point(8, 200);
            this.chkRunning.Name = "chkRunning";
            this.chkRunning.Size = new System.Drawing.Size(104, 16);
            this.chkRunning.TabIndex = 12;
            this.chkRunning.Text = "Running:";
            this.ttpSpawnInfo.SetToolTip(this.chkRunning, "Check if the spawner should be running.");
            // 
            // spnMaxCount
            // 
            this.spnMaxCount.ContextMenu = this.highlightDetail;
            this.spnMaxCount.Location = new System.Drawing.Point(96, 60);
            this.spnMaxCount.Name = "spnMaxCount";
            this.spnMaxCount.Size = new System.Drawing.Size(72, 20);
            this.spnMaxCount.TabIndex = 4;
            this.ttpSpawnInfo.SetToolTip(this.spnMaxCount, "Absolute maximum number of objects to be spawned by this spawner.");
            this.spnMaxCount.Enter += new System.EventHandler(this.TextEntryControl_Enter);
            // 
            // txtName
            // 
            this.txtName.ContextMenu = this.highlightDetail;
            this.txtName.Location = new System.Drawing.Point(8, 16);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(168, 20);
            this.txtName.TabIndex = 0;
            this.txtName.Text = "Spawn";
            this.ttpSpawnInfo.SetToolTip(this.txtName, "Name of the spawner.");
            this.txtName.Enter += new System.EventHandler(this.TextEntryControl_Enter);
            this.txtName.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtName_KeyUp);
            this.txtName.Leave += new System.EventHandler(this.txtName_Leave);
            this.txtName.MouseLeave += new System.EventHandler(this.txtName_MouseLeave);
            // 
            // spnHomeRange
            // 
            this.spnHomeRange.BackColor = System.Drawing.SystemColors.Window;
            this.spnHomeRange.ContextMenu = this.highlightDetail;
            this.spnHomeRange.Location = new System.Drawing.Point(96, 40);
            this.spnHomeRange.Name = "spnHomeRange";
            this.spnHomeRange.Size = new System.Drawing.Size(72, 20);
            this.spnHomeRange.TabIndex = 2;
            this.ttpSpawnInfo.SetToolTip(this.spnHomeRange, "Maximum wandering range of the spawn from its spawned location.");
            this.spnHomeRange.Enter += new System.EventHandler(this.TextEntryControl_Enter);
            // 
            // spnMinDelay
            // 
            this.spnMinDelay.ContextMenu = this.highlightDetail;
            this.spnMinDelay.DecimalPlaces = 1;
            this.spnMinDelay.Location = new System.Drawing.Point(96, 80);
            this.spnMinDelay.Name = "spnMinDelay";
            this.spnMinDelay.Size = new System.Drawing.Size(72, 20);
            this.spnMinDelay.TabIndex = 6;
            this.ttpSpawnInfo.SetToolTip(this.spnMinDelay, "Minimum delay to respawn (in minutes).");
            this.spnMinDelay.ValueChanged += new System.EventHandler(this.spnMinDelay_ValueChanged);
            this.spnMinDelay.Enter += new System.EventHandler(this.TextEntryControl_Enter);
            // 
            // spnTeam
            // 
            this.spnTeam.ContextMenu = this.highlightDetail;
            this.spnTeam.Location = new System.Drawing.Point(96, 120);
            this.spnTeam.Name = "spnTeam";
            this.spnTeam.Size = new System.Drawing.Size(72, 20);
            this.spnTeam.TabIndex = 10;
            this.ttpSpawnInfo.SetToolTip(this.spnTeam, "Team that spawned object will belong to.");
            this.spnTeam.Enter += new System.EventHandler(this.TextEntryControl_Enter);
            // 
            // chkGroup
            // 
            this.chkGroup.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkGroup.ContextMenu = this.highlightDetail;
            this.chkGroup.Location = new System.Drawing.Point(8, 184);
            this.chkGroup.Name = "chkGroup";
            this.chkGroup.Size = new System.Drawing.Size(104, 16);
            this.chkGroup.TabIndex = 11;
            this.chkGroup.Text = "Group:";
            this.ttpSpawnInfo.SetToolTip(this.chkGroup, "Check if the spawned object belongs to a group.");
            // 
            // spnMaxDelay
            // 
            this.spnMaxDelay.ContextMenu = this.highlightDetail;
            this.spnMaxDelay.DecimalPlaces = 1;
            this.spnMaxDelay.Location = new System.Drawing.Point(96, 100);
            this.spnMaxDelay.Name = "spnMaxDelay";
            this.spnMaxDelay.Size = new System.Drawing.Size(72, 20);
            this.spnMaxDelay.TabIndex = 8;
            this.ttpSpawnInfo.SetToolTip(this.spnMaxDelay, "Maximum delay to respawn (in minutes).");
            this.spnMaxDelay.Enter += new System.EventHandler(this.TextEntryControl_Enter);
            // 
            // spnSpawnRange
            // 
            this.spnSpawnRange.ContextMenu = this.highlightDetail;
            this.spnSpawnRange.Location = new System.Drawing.Point(96, 140);
            this.spnSpawnRange.Name = "spnSpawnRange";
            this.spnSpawnRange.Size = new System.Drawing.Size(72, 20);
            this.spnSpawnRange.TabIndex = 180;
            this.ttpSpawnInfo.SetToolTip(this.spnSpawnRange, "Maximum spawning range.  A value of -1 means the range is specified by XY.");
            this.spnSpawnRange.ValueChanged += new System.EventHandler(this.spnSpawnRange_ValueChanged);
            // 
            // spnProximityRange
            // 
            this.spnProximityRange.ContextMenu = this.highlightDetail;
            this.spnProximityRange.Location = new System.Drawing.Point(96, 160);
            this.spnProximityRange.Name = "spnProximityRange";
            this.spnProximityRange.Size = new System.Drawing.Size(72, 20);
            this.spnProximityRange.TabIndex = 178;
            this.ttpSpawnInfo.SetToolTip(this.spnProximityRange, "Maximum range within which a player can trigger the spawner.");
            // 
            // spnMinRefract
            // 
            this.spnMinRefract.ContextMenu = this.highlightDetail;
            this.spnMinRefract.DecimalPlaces = 1;
            this.spnMinRefract.Location = new System.Drawing.Point(280, 60);
            this.spnMinRefract.Name = "spnMinRefract";
            this.spnMinRefract.Size = new System.Drawing.Size(72, 20);
            this.spnMinRefract.TabIndex = 182;
            this.ttpSpawnInfo.SetToolTip(this.spnMinRefract, "Minimum delay after triggering when the spawner can be triggered again (in minute" +
        "s).");
            // 
            // spnTODStart
            // 
            this.spnTODStart.ContextMenu = this.highlightDetail;
            this.spnTODStart.DecimalPlaces = 1;
            this.spnTODStart.Location = new System.Drawing.Point(280, 100);
            this.spnTODStart.Name = "spnTODStart";
            this.spnTODStart.Size = new System.Drawing.Size(72, 20);
            this.spnTODStart.TabIndex = 186;
            this.ttpSpawnInfo.SetToolTip(this.spnTODStart, "Starting hour after which spawning can occur.");
            // 
            // spnMaxRefract
            // 
            this.spnMaxRefract.ContextMenu = this.highlightDetail;
            this.spnMaxRefract.DecimalPlaces = 1;
            this.spnMaxRefract.Location = new System.Drawing.Point(280, 80);
            this.spnMaxRefract.Name = "spnMaxRefract";
            this.spnMaxRefract.Size = new System.Drawing.Size(72, 20);
            this.spnMaxRefract.TabIndex = 184;
            this.ttpSpawnInfo.SetToolTip(this.spnMaxRefract, "Maximum delay after triggering when the spawner can be triggered again (in minute" +
        "s).");
            // 
            // chkGameTOD
            // 
            this.chkGameTOD.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkGameTOD.ContextMenu = this.highlightDetail;
            this.chkGameTOD.Location = new System.Drawing.Point(128, 216);
            this.chkGameTOD.Name = "chkGameTOD";
            this.chkGameTOD.Size = new System.Drawing.Size(88, 16);
            this.chkGameTOD.TabIndex = 189;
            this.chkGameTOD.Text = "GameTOD:";
            this.ttpSpawnInfo.SetToolTip(this.chkGameTOD, "Time of Day triggering uses game world time.");
            // 
            // chkRealTOD
            // 
            this.chkRealTOD.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkRealTOD.Checked = true;
            this.chkRealTOD.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkRealTOD.ContextMenu = this.highlightDetail;
            this.chkRealTOD.Location = new System.Drawing.Point(128, 200);
            this.chkRealTOD.Name = "chkRealTOD";
            this.chkRealTOD.Size = new System.Drawing.Size(88, 16);
            this.chkRealTOD.TabIndex = 188;
            this.chkRealTOD.Text = "RealTOD:";
            this.ttpSpawnInfo.SetToolTip(this.chkRealTOD, "Time of Day triggering uses real world time.");
            // 
            // chkAllowGhost
            // 
            this.chkAllowGhost.BackColor = System.Drawing.SystemColors.Control;
            this.chkAllowGhost.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkAllowGhost.ContextMenu = this.highlightDetail;
            this.chkAllowGhost.Location = new System.Drawing.Point(128, 184);
            this.chkAllowGhost.Name = "chkAllowGhost";
            this.chkAllowGhost.Size = new System.Drawing.Size(88, 16);
            this.chkAllowGhost.TabIndex = 187;
            this.chkAllowGhost.Text = "AllowGhost:";
            this.ttpSpawnInfo.SetToolTip(this.chkAllowGhost, "Allow the spawner to be triggered by players in ghost form.");
            this.chkAllowGhost.UseVisualStyleBackColor = false;
            // 
            // chkSmartSpawning
            // 
            this.chkSmartSpawning.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkSmartSpawning.ContextMenu = this.highlightDetail;
            this.chkSmartSpawning.Location = new System.Drawing.Point(232, 216);
            this.chkSmartSpawning.Name = "chkSmartSpawning";
            this.chkSmartSpawning.Size = new System.Drawing.Size(120, 16);
            this.chkSmartSpawning.TabIndex = 192;
            this.chkSmartSpawning.Text = "SmartSpawning:";
            this.ttpSpawnInfo.SetToolTip(this.chkSmartSpawning, "Enable automatic spawning/despawning based upon nearby player activity.");
            this.chkSmartSpawning.CheckedChanged += new System.EventHandler(this.checkBox20_CheckedChanged);
            // 
            // chkSequentialSpawn
            // 
            this.chkSequentialSpawn.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkSequentialSpawn.ContextMenu = this.highlightDetail;
            this.chkSequentialSpawn.Location = new System.Drawing.Point(232, 200);
            this.chkSequentialSpawn.Name = "chkSequentialSpawn";
            this.chkSequentialSpawn.Size = new System.Drawing.Size(120, 16);
            this.chkSequentialSpawn.TabIndex = 191;
            this.chkSequentialSpawn.Text = "SequentialSpawn:";
            this.ttpSpawnInfo.SetToolTip(this.chkSequentialSpawn, "Enable sequential spawning that will advance according to subgroup number.");
            // 
            // chkSpawnOnTrigger
            // 
            this.chkSpawnOnTrigger.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkSpawnOnTrigger.ContextMenu = this.highlightDetail;
            this.chkSpawnOnTrigger.Location = new System.Drawing.Point(232, 184);
            this.chkSpawnOnTrigger.Name = "chkSpawnOnTrigger";
            this.chkSpawnOnTrigger.Size = new System.Drawing.Size(120, 16);
            this.chkSpawnOnTrigger.TabIndex = 190;
            this.chkSpawnOnTrigger.Text = "SpawnOnTrigger:";
            this.ttpSpawnInfo.SetToolTip(this.chkSpawnOnTrigger, "Spawn immediately after triggering regardless of min/maxdelay.");
            // 
            // spnDespawn
            // 
            this.spnDespawn.ContextMenu = this.highlightDetail;
            this.spnDespawn.DecimalPlaces = 1;
            this.spnDespawn.Location = new System.Drawing.Point(280, 40);
            this.spnDespawn.Name = "spnDespawn";
            this.spnDespawn.Size = new System.Drawing.Size(72, 20);
            this.spnDespawn.TabIndex = 194;
            this.ttpSpawnInfo.SetToolTip(this.spnDespawn, "Similar to Duration but for longer timescales.");
            this.spnDespawn.ValueChanged += new System.EventHandler(this.numericUpDown6_ValueChanged);
            // 
            // spnTODEnd
            // 
            this.spnTODEnd.ContextMenu = this.highlightDetail;
            this.spnTODEnd.DecimalPlaces = 1;
            this.spnTODEnd.Location = new System.Drawing.Point(280, 120);
            this.spnTODEnd.Name = "spnTODEnd";
            this.spnTODEnd.Size = new System.Drawing.Size(72, 20);
            this.spnTODEnd.TabIndex = 195;
            this.ttpSpawnInfo.SetToolTip(this.spnTODEnd, "Ending hour before which spawning can occur.");
            // 
            // spnDuration
            // 
            this.spnDuration.ContextMenu = this.highlightDetail;
            this.spnDuration.DecimalPlaces = 1;
            this.spnDuration.Location = new System.Drawing.Point(280, 20);
            this.spnDuration.Name = "spnDuration";
            this.spnDuration.Size = new System.Drawing.Size(72, 20);
            this.spnDuration.TabIndex = 198;
            this.ttpSpawnInfo.SetToolTip(this.spnDuration, "Maximum duration of a spawn after which it will be deleted.");
            // 
            // spnProximitySnd
            // 
            this.spnProximitySnd.ContextMenu = this.highlightDetail;
            this.spnProximitySnd.Location = new System.Drawing.Point(280, 160);
            this.spnProximitySnd.Name = "spnProximitySnd";
            this.spnProximitySnd.Size = new System.Drawing.Size(72, 20);
            this.spnProximitySnd.TabIndex = 203;
            this.ttpSpawnInfo.SetToolTip(this.spnProximitySnd, "Sound ID used when the spawner is triggered.");
            this.spnProximitySnd.ValueChanged += new System.EventHandler(this.numericUpDown10_ValueChanged);
            // 
            // spnKillReset
            // 
            this.spnKillReset.ContextMenu = this.highlightDetail;
            this.spnKillReset.Location = new System.Drawing.Point(280, 140);
            this.spnKillReset.Name = "spnKillReset";
            this.spnKillReset.Size = new System.Drawing.Size(72, 20);
            this.spnKillReset.TabIndex = 205;
            this.ttpSpawnInfo.SetToolTip(this.spnKillReset, "Number of spawner ticks until the Kill count of the spawner is reset.");
            // 
            // chkTracking
            // 
            this.chkTracking.Location = new System.Drawing.Point(8, 48);
            this.chkTracking.Name = "chkTracking";
            this.chkTracking.Size = new System.Drawing.Size(56, 16);
            this.chkTracking.TabIndex = 9;
            this.chkTracking.Text = "Track";
            this.ttpSpawnInfo.SetToolTip(this.chkTracking, "Track player movement on the map.");
            this.chkTracking.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // btnGo
            // 
            this.btnGo.Location = new System.Drawing.Point(8, 16);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(48, 24);
            this.btnGo.TabIndex = 8;
            this.btnGo.Text = "&Go";
            this.ttpSpawnInfo.SetToolTip(this.btnGo, "Move the player to the targeted location on the map.");
            this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
            // 
            // chkInContainer
            // 
            this.chkInContainer.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkInContainer.ContextMenu = this.highlightDetail;
            this.chkInContainer.Location = new System.Drawing.Point(8, 232);
            this.chkInContainer.Name = "chkInContainer";
            this.chkInContainer.Size = new System.Drawing.Size(104, 16);
            this.chkInContainer.TabIndex = 207;
            this.chkInContainer.Text = "InContainer:";
            this.ttpSpawnInfo.SetToolTip(this.chkInContainer, "Check if the spawner is in a container.");
            this.chkInContainer.CheckedChanged += new System.EventHandler(this.chkInContainer_CheckedChanged);
            // 
            // spnTriggerProbability
            // 
            this.spnTriggerProbability.DecimalPlaces = 1;
            this.spnTriggerProbability.Location = new System.Drawing.Point(120, 56);
            this.spnTriggerProbability.Name = "spnTriggerProbability";
            this.spnTriggerProbability.Size = new System.Drawing.Size(56, 20);
            this.spnTriggerProbability.TabIndex = 200;
            this.ttpSpawnInfo.SetToolTip(this.spnTriggerProbability, "Maximum duration of a spawn after which it will be deleted.");
            // 
            // spnStackAmount
            // 
            this.spnStackAmount.Location = new System.Drawing.Point(120, 32);
            this.spnStackAmount.Name = "spnStackAmount";
            this.spnStackAmount.Size = new System.Drawing.Size(56, 20);
            this.spnStackAmount.TabIndex = 202;
            this.ttpSpawnInfo.SetToolTip(this.spnStackAmount, "Maximum wandering range of the spawn from its spawned location.");
            // 
            // chkExternalTriggering
            // 
            this.chkExternalTriggering.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkExternalTriggering.Location = new System.Drawing.Point(8, 80);
            this.chkExternalTriggering.Name = "chkExternalTriggering";
            this.chkExternalTriggering.Size = new System.Drawing.Size(128, 16);
            this.chkExternalTriggering.TabIndex = 222;
            this.chkExternalTriggering.Text = "ExternalTriggering:";
            this.ttpSpawnInfo.SetToolTip(this.chkExternalTriggering, "Check if the spawned object belongs to a group.");
            // 
            // spnContainerX
            // 
            this.spnContainerX.Enabled = false;
            this.spnContainerX.Location = new System.Drawing.Point(264, 32);
            this.spnContainerX.Name = "spnContainerX";
            this.spnContainerX.Size = new System.Drawing.Size(56, 20);
            this.spnContainerX.TabIndex = 233;
            this.ttpSpawnInfo.SetToolTip(this.spnContainerX, "Maximum wandering range of the spawn from its spawned location.");
            // 
            // spnContainerY
            // 
            this.spnContainerY.Enabled = false;
            this.spnContainerY.Location = new System.Drawing.Point(264, 56);
            this.spnContainerY.Name = "spnContainerY";
            this.spnContainerY.Size = new System.Drawing.Size(56, 20);
            this.spnContainerY.TabIndex = 234;
            this.ttpSpawnInfo.SetToolTip(this.spnContainerY, "Maximum wandering range of the spawn from its spawned location.");
            // 
            // spnContainerZ
            // 
            this.spnContainerZ.Enabled = false;
            this.spnContainerZ.Location = new System.Drawing.Point(264, 80);
            this.spnContainerZ.Name = "spnContainerZ";
            this.spnContainerZ.Size = new System.Drawing.Size(56, 20);
            this.spnContainerZ.TabIndex = 235;
            this.ttpSpawnInfo.SetToolTip(this.spnContainerZ, "Maximum wandering range of the spawn from its spawned location.");
            // 
            // chkLockSpawn
            // 
            this.chkLockSpawn.Location = new System.Drawing.Point(8, 80);
            this.chkLockSpawn.Name = "chkLockSpawn";
            this.chkLockSpawn.Size = new System.Drawing.Size(56, 16);
            this.chkLockSpawn.TabIndex = 10;
            this.chkLockSpawn.Text = "Loc&k";
            this.ttpSpawnInfo.SetToolTip(this.chkLockSpawn, "Lock spawner location during spawn region repositioning or resizing");
            // 
            // chkDetails
            // 
            this.chkDetails.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkDetails.Location = new System.Drawing.Point(77, 56);
            this.chkDetails.Name = "chkDetails";
            this.chkDetails.Size = new System.Drawing.Size(80, 16);
            this.chkDetails.TabIndex = 7;
            this.chkDetails.Text = "Details";
            this.ttpSpawnInfo.SetToolTip(this.chkDetails, "Display detailed spawn information");
            this.chkDetails.CheckedChanged += new System.EventHandler(this.chkDetails_CheckedChanged);
            // 
            // chkSnapRegion
            // 
            this.chkSnapRegion.Location = new System.Drawing.Point(8, 96);
            this.chkSnapRegion.Name = "chkSnapRegion";
            this.chkSnapRegion.Size = new System.Drawing.Size(72, 16);
            this.chkSnapRegion.TabIndex = 11;
            this.chkSnapRegion.Text = "Snap XY";
            this.ttpSpawnInfo.SetToolTip(this.chkSnapRegion, "When selecting spawners, automatically move to the center of the spawning region " +
        "instead of to the spawner location");
            // 
            // treeRegionView
            // 
            this.treeRegionView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeRegionView.CheckBoxes = true;
            this.treeRegionView.Location = new System.Drawing.Point(8, 8);
            this.treeRegionView.Name = "treeRegionView";
            this.treeRegionView.Size = new System.Drawing.Size(156, 448);
            this.treeRegionView.TabIndex = 0;
            this.ttpSpawnInfo.SetToolTip(this.treeRegionView, "List of regions that have been defined in RunUO Data/Regions.xml.  Move to the re" +
        "gion Go location when selected.");
            this.treeRegionView.MouseUp += new System.Windows.Forms.MouseEventHandler(this.treeRegionView_MouseUp);
            // 
            // treeGoView
            // 
            this.treeGoView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeGoView.Location = new System.Drawing.Point(8, 8);
            this.treeGoView.Name = "treeGoView";
            this.treeGoView.Size = new System.Drawing.Size(156, 448);
            this.treeGoView.TabIndex = 0;
            this.ttpSpawnInfo.SetToolTip(this.treeGoView, "List of locations taken from RunUO Data/Locations.  Move to the locations when se" +
        "lected.");
            this.treeGoView.MouseUp += new System.Windows.Forms.MouseEventHandler(this.treeGoView_MouseUp);
            // 
            // checkSpawnFilter
            // 
            this.checkSpawnFilter.Location = new System.Drawing.Point(8, 8);
            this.checkSpawnFilter.Name = "checkSpawnFilter";
            this.checkSpawnFilter.Size = new System.Drawing.Size(88, 16);
            this.checkSpawnFilter.TabIndex = 12;
            this.checkSpawnFilter.Text = "Apply Filter";
            this.ttpSpawnInfo.SetToolTip(this.checkSpawnFilter, "Filter the display of spawners based on the Display filter settings.");
            this.checkSpawnFilter.CheckedChanged += new System.EventHandler(this.checkSpawnFilter_CheckedChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(8, 35);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(160, 20);
            this.button1.TabIndex = 3;
            this.button1.Text = "Clear Selections";
            this.ttpSpawnInfo.SetToolTip(this.button1, "Clears current selections from the type list.");
            this.button1.Click += new System.EventHandler(this.btnSpawnPackClear);
            // 
            // clbSpawnPack
            // 
            this.clbSpawnPack.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.clbSpawnPack.CheckOnClick = true;
            this.clbSpawnPack.HorizontalScrollbar = true;
            this.clbSpawnPack.IntegralHeight = false;
            this.clbSpawnPack.Location = new System.Drawing.Point(8, 96);
            this.clbSpawnPack.Name = "clbSpawnPack";
            this.clbSpawnPack.Size = new System.Drawing.Size(160, 168);
            this.clbSpawnPack.TabIndex = 4;
            this.clbSpawnPack.ThreeDCheckBoxes = true;
            this.ttpSpawnInfo.SetToolTip(this.clbSpawnPack, "List of spawnable objects in this spawn pack.  Right-click to delete.");
            this.clbSpawnPack.MouseUp += new System.Windows.Forms.MouseEventHandler(this.clbSpawnPack_MouseUp);
            // 
            // btnUpdateFromSpawnPack
            // 
            this.btnUpdateFromSpawnPack.Enabled = false;
            this.btnUpdateFromSpawnPack.Location = new System.Drawing.Point(8, 55);
            this.btnUpdateFromSpawnPack.Name = "btnUpdateFromSpawnPack";
            this.btnUpdateFromSpawnPack.Size = new System.Drawing.Size(160, 20);
            this.btnUpdateFromSpawnPack.TabIndex = 15;
            this.btnUpdateFromSpawnPack.Text = "Add to Spawner";
            this.ttpSpawnInfo.SetToolTip(this.btnUpdateFromSpawnPack, "Updates the currently selected spawn with the selected types.");
            this.btnUpdateFromSpawnPack.Click += new System.EventHandler(this.btnUpdateFromSpawnPack_Click);
            // 
            // btnAddToSpawnPack
            // 
            this.btnAddToSpawnPack.Location = new System.Drawing.Point(8, 75);
            this.btnAddToSpawnPack.Name = "btnAddToSpawnPack";
            this.btnAddToSpawnPack.Size = new System.Drawing.Size(160, 20);
            this.btnAddToSpawnPack.TabIndex = 16;
            this.btnAddToSpawnPack.Text = "Add to Spawn Pack";
            this.ttpSpawnInfo.SetToolTip(this.btnAddToSpawnPack, "Adds the selected types to the Current Spawn Pack");
            this.btnAddToSpawnPack.Click += new System.EventHandler(this.btnAddToSpawnPack_Click);
            // 
            // btnUpdateSpawnPacks
            // 
            this.btnUpdateSpawnPacks.Location = new System.Drawing.Point(8, 75);
            this.btnUpdateSpawnPacks.Name = "btnUpdateSpawnPacks";
            this.btnUpdateSpawnPacks.Size = new System.Drawing.Size(160, 20);
            this.btnUpdateSpawnPacks.TabIndex = 17;
            this.btnUpdateSpawnPacks.Text = "Update Spawn Packs";
            this.ttpSpawnInfo.SetToolTip(this.btnUpdateSpawnPacks, "Updates the Current Spawn Pack into the All Spawn Packs list.");
            this.btnUpdateSpawnPacks.Click += new System.EventHandler(this.btnUpdateSpawnPacks_Click);
            // 
            // tvwSpawnPacks
            // 
            this.tvwSpawnPacks.Location = new System.Drawing.Point(8, 16);
            this.tvwSpawnPacks.Name = "tvwSpawnPacks";
            this.tvwSpawnPacks.Size = new System.Drawing.Size(160, 128);
            this.tvwSpawnPacks.TabIndex = 0;
            this.ttpSpawnInfo.SetToolTip(this.tvwSpawnPacks, "List of all available Spawn Packs.  Right-click to delete.");
            this.tvwSpawnPacks.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvwSpawnPacks_AfterSelect);
            this.tvwSpawnPacks.MouseUp += new System.Windows.Forms.MouseEventHandler(this.tvwSpawnPacks_MouseUp);
            // 
            // chkShade
            // 
            this.chkShade.Location = new System.Drawing.Point(8, 114);
            this.chkShade.Name = "chkShade";
            this.chkShade.Size = new System.Drawing.Size(80, 16);
            this.chkShade.TabIndex = 16;
            this.chkShade.Text = "Shade by";
            this.ttpSpawnInfo.SetToolTip(this.chkShade, "Display detailed spawn information");
            this.chkShade.CheckedChanged += new System.EventHandler(this.chkShade_CheckedChanged);
            // 
            // cbxShade
            // 
            this.cbxShade.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxShade.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxShade.Items.AddRange(new object[] {
            "Density",
            "Speed"});
            this.cbxShade.Location = new System.Drawing.Point(85, 112);
            this.cbxShade.Name = "cbxShade";
            this.cbxShade.Size = new System.Drawing.Size(77, 21);
            this.cbxShade.TabIndex = 17;
            this.ttpSpawnInfo.SetToolTip(this.cbxShade, "Changes the current map.");
            this.cbxShade.SelectedIndexChanged += new System.EventHandler(this.cbxShade_SelectedIndexChanged);
            // 
            // label28
            // 
            this.label28.Location = new System.Drawing.Point(192, 140);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(64, 16);
            this.label28.TabIndex = 204;
            this.label28.Text = "KillReset:";
            this.ttpSpawnInfo.SetToolTip(this.label28, "Number of spawner ticks until the Kill count of the spawner is reset.");
            // 
            // label27
            // 
            this.label27.Location = new System.Drawing.Point(192, 160);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(104, 16);
            this.label27.TabIndex = 202;
            this.label27.Text = "ProximitySnd:";
            this.ttpSpawnInfo.SetToolTip(this.label27, "Sound ID used when the spawner is triggered.");
            // 
            // label25
            // 
            this.label25.Location = new System.Drawing.Point(192, 20);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(80, 20);
            this.label25.TabIndex = 197;
            this.label25.Text = "Duration (m):";
            this.ttpSpawnInfo.SetToolTip(this.label25, "Maximum duration of a spawn after which it will be deleted.");
            // 
            // label24
            // 
            this.label24.Location = new System.Drawing.Point(192, 120);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(80, 16);
            this.label24.TabIndex = 196;
            this.label24.Text = "TODEnd (h):";
            this.ttpSpawnInfo.SetToolTip(this.label24, "Ending hour before which spawning can occur.");
            // 
            // label23
            // 
            this.label23.Location = new System.Drawing.Point(192, 40);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(80, 20);
            this.label23.TabIndex = 193;
            this.label23.Text = "Despawn (h):";
            this.ttpSpawnInfo.SetToolTip(this.label23, "Similar to Duration but for longer timescales.");
            // 
            // label18
            // 
            this.label18.Location = new System.Drawing.Point(192, 80);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(88, 16);
            this.label18.TabIndex = 183;
            this.label18.Text = "MaxRefract (m):";
            this.ttpSpawnInfo.SetToolTip(this.label18, "Maximum delay after triggering when the spawner can be triggered again (in minute" +
        "s).");
            // 
            // label19
            // 
            this.label19.Location = new System.Drawing.Point(8, 160);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(88, 16);
            this.label19.TabIndex = 177;
            this.label19.Text = "ProximityRange:";
            this.ttpSpawnInfo.SetToolTip(this.label19, "Maximum range within which a player can trigger the spawner.  A value of -1 means" +
        " that proximity triggering is disabled.");
            // 
            // label20
            // 
            this.label20.Location = new System.Drawing.Point(192, 100);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(80, 16);
            this.label20.TabIndex = 185;
            this.label20.Text = "TODStart (h):";
            this.ttpSpawnInfo.SetToolTip(this.label20, "Starting hour after which spawning can occur.");
            // 
            // label21
            // 
            this.label21.Location = new System.Drawing.Point(8, 140);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(80, 16);
            this.label21.TabIndex = 179;
            this.label21.Text = "SpawnRange:";
            this.ttpSpawnInfo.SetToolTip(this.label21, "Maximum spawning range.  A value of -1 means the range is specified by XY.");
            // 
            // label22
            // 
            this.label22.Location = new System.Drawing.Point(192, 60);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(88, 16);
            this.label22.TabIndex = 181;
            this.label22.Text = "MinRefract (m):";
            this.ttpSpawnInfo.SetToolTip(this.label22, "Minimum delay after triggering when the spawner can be triggered again (in minute" +
        "s).");
            // 
            // lblMaxDelay
            // 
            this.lblMaxDelay.Location = new System.Drawing.Point(8, 100);
            this.lblMaxDelay.Name = "lblMaxDelay";
            this.lblMaxDelay.Size = new System.Drawing.Size(80, 16);
            this.lblMaxDelay.TabIndex = 7;
            this.lblMaxDelay.Text = "MaxDelay (m)";
            this.ttpSpawnInfo.SetToolTip(this.lblMaxDelay, "Maximum delay to respawn (in minutes).");
            // 
            // lblHomeRange
            // 
            this.lblHomeRange.Location = new System.Drawing.Point(8, 40);
            this.lblHomeRange.Name = "lblHomeRange";
            this.lblHomeRange.Size = new System.Drawing.Size(72, 20);
            this.lblHomeRange.TabIndex = 1;
            this.lblHomeRange.Text = "HomeRange:";
            this.ttpSpawnInfo.SetToolTip(this.lblHomeRange, "Maximum wandering range of the spawn from its spawned location.");
            // 
            // lblTeam
            // 
            this.lblTeam.Location = new System.Drawing.Point(8, 120);
            this.lblTeam.Name = "lblTeam";
            this.lblTeam.Size = new System.Drawing.Size(80, 16);
            this.lblTeam.TabIndex = 9;
            this.lblTeam.Text = "Team:";
            this.ttpSpawnInfo.SetToolTip(this.lblTeam, "Team that spawned object will belong to.");
            // 
            // lblMaxCount
            // 
            this.lblMaxCount.Location = new System.Drawing.Point(8, 60);
            this.lblMaxCount.Name = "lblMaxCount";
            this.lblMaxCount.Size = new System.Drawing.Size(64, 20);
            this.lblMaxCount.TabIndex = 3;
            this.lblMaxCount.Text = "MaxCount:";
            this.ttpSpawnInfo.SetToolTip(this.lblMaxCount, "Absolute maximum number of objects to be spawned by this spawner.");
            // 
            // lblMinDelay
            // 
            this.lblMinDelay.Location = new System.Drawing.Point(8, 80);
            this.lblMinDelay.Name = "lblMinDelay";
            this.lblMinDelay.Size = new System.Drawing.Size(72, 16);
            this.lblMinDelay.TabIndex = 5;
            this.lblMinDelay.Text = "MinDelay (m)";
            this.ttpSpawnInfo.SetToolTip(this.lblMinDelay, "Minimum delay to respawn (in minutes).");
            this.lblMinDelay.Click += new System.EventHandler(this.lblMinDelay_Click);
            // 
            // btnSendSpawn
            // 
            this.btnSendSpawn.ContextMenu = this.unloadSpawners;
            this.btnSendSpawn.Location = new System.Drawing.Point(8, 56);
            this.btnSendSpawn.Name = "btnSendSpawn";
            this.btnSendSpawn.Size = new System.Drawing.Size(152, 23);
            this.btnSendSpawn.TabIndex = 206;
            this.btnSendSpawn.Text = "Send to Server";
            this.ttpSpawnInfo.SetToolTip(this.btnSendSpawn, "Send all spawners on the list to the Transfer Server.  Right-click to unload them" +
        " from the server.");
            this.btnSendSpawn.Click += new System.EventHandler(this.btnSendSpawn_Click);
            // 
            // unloadSpawners
            // 
            this.unloadSpawners.Popup += new System.EventHandler(this.unloadSpawner_Popup);
            // 
            // btnFilterSettings
            // 
            this.btnFilterSettings.Location = new System.Drawing.Point(88, 8);
            this.btnFilterSettings.Name = "btnFilterSettings";
            this.btnFilterSettings.Size = new System.Drawing.Size(72, 24);
            this.btnFilterSettings.TabIndex = 207;
            this.btnFilterSettings.Text = "Settings";
            this.ttpSpawnInfo.SetToolTip(this.btnFilterSettings, "Open the Display filter settings window.");
            this.btnFilterSettings.Click += new System.EventHandler(this.btnFilterSettings_Click);
            // 
            // mniForceLoad
            // 
            this.mniForceLoad.Index = -1;
            this.mniForceLoad.Text = "Force Load Into Current Map...";
            this.mniForceLoad.Click += new System.EventHandler(this.mniForceLoad_Click);
            // 
            // menuItem21
            // 
            this.menuItem21.Index = -1;
            this.menuItem21.Text = "Cancel";
            // 
            // mniForceMerge
            // 
            this.mniForceMerge.Index = -1;
            this.mniForceMerge.Text = "Force Merge Into Current Map...";
            this.mniForceMerge.Click += new System.EventHandler(this.mniForceMerge_Click);
            // 
            // menuItem20
            // 
            this.menuItem20.Index = -1;
            this.menuItem20.Text = "Cancel";
            // 
            // mniUnloadSpawners
            // 
            this.mniUnloadSpawners.Index = -1;
            this.mniUnloadSpawners.Text = "Unload Spawners from Server";
            this.mniUnloadSpawners.Click += new System.EventHandler(this.mniUnloadSpawners_Click);
            // 
            // menuItem19
            // 
            this.menuItem19.Index = -1;
            this.menuItem19.Text = "Cancel";
            // 
            // pnlControls
            // 
            this.pnlControls.Controls.Add(this.lblTrkMax);
            this.pnlControls.Controls.Add(this.lblTrkMin);
            this.pnlControls.Controls.Add(this.trkZoom);
            this.pnlControls.Controls.Add(this.tabControl3);
            this.pnlControls.Controls.Add(this.tabControl2);
            this.pnlControls.Controls.Add(this.progressBar1);
            this.pnlControls.Controls.Add(this.lblTransferStatus);
            this.pnlControls.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlControls.Location = new System.Drawing.Point(0, 0);
            this.pnlControls.Name = "pnlControls";
            this.pnlControls.Size = new System.Drawing.Size(176, 717);
            this.pnlControls.TabIndex = 0;
            // 
            // lblTrkMax
            // 
            this.lblTrkMax.Location = new System.Drawing.Point(136, 168);
            this.lblTrkMax.Name = "lblTrkMax";
            this.lblTrkMax.Size = new System.Drawing.Size(29, 16);
            this.lblTrkMax.TabIndex = 15;
            this.lblTrkMax.Text = "max";
            // 
            // lblTrkMin
            // 
            this.lblTrkMin.Location = new System.Drawing.Point(8, 168);
            this.lblTrkMin.Name = "lblTrkMin";
            this.lblTrkMin.Size = new System.Drawing.Size(29, 16);
            this.lblTrkMin.TabIndex = 14;
            this.lblTrkMin.Text = "min";
            // 
            // tabControl3
            // 
            this.tabControl3.Controls.Add(this.tabMapSettings);
            this.tabControl3.Location = new System.Drawing.Point(0, 0);
            this.tabControl3.Name = "tabControl3";
            this.tabControl3.SelectedIndex = 0;
            this.tabControl3.Size = new System.Drawing.Size(173, 168);
            this.tabControl3.TabIndex = 7;
            // 
            // tabMapSettings
            // 
            this.tabMapSettings.Controls.Add(this.grpMapControl);
            this.tabMapSettings.Location = new System.Drawing.Point(4, 22);
            this.tabMapSettings.Name = "tabMapSettings";
            this.tabMapSettings.Size = new System.Drawing.Size(165, 142);
            this.tabMapSettings.TabIndex = 0;
            this.tabMapSettings.Text = "Map Settings";
            // 
            // grpMapControl
            // 
            this.grpMapControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
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
            this.grpMapControl.Location = new System.Drawing.Point(0, 0);
            this.grpMapControl.Name = "grpMapControl";
            this.grpMapControl.Size = new System.Drawing.Size(189, 176);
            this.grpMapControl.TabIndex = 0;
            this.grpMapControl.TabStop = false;
            // 
            // tabControl2
            // 
            this.tabControl2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl2.Controls.Add(this.tabPage3);
            this.tabControl2.Controls.Add(this.tabPage4);
            this.tabControl2.Controls.Add(this.tabPage5);
            this.tabControl2.Location = new System.Drawing.Point(0, 239);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(176, 484);
            this.tabControl2.TabIndex = 6;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.grpSpawnList);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(168, 458);
            this.tabPage3.TabIndex = 0;
            this.tabPage3.Text = "Spawners";
            // 
            // grpSpawnList
            // 
            this.grpSpawnList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpSpawnList.Controls.Add(this.btnFilterSettings);
            this.grpSpawnList.Controls.Add(this.tvwSpawnPoints);
            this.grpSpawnList.Controls.Add(this.btnLoadSpawn);
            this.grpSpawnList.Controls.Add(this.btnMergeSpawn);
            this.grpSpawnList.Controls.Add(this.btnSaveSpawn);
            this.grpSpawnList.Controls.Add(this.lblTotalSpawn);
            this.grpSpawnList.Controls.Add(this.checkSpawnFilter);
            this.grpSpawnList.Controls.Add(this.btnSendSpawn);
            this.grpSpawnList.Location = new System.Drawing.Point(0, 0);
            this.grpSpawnList.Name = "grpSpawnList";
            this.grpSpawnList.Size = new System.Drawing.Size(168, 460);
            this.grpSpawnList.TabIndex = 1;
            this.grpSpawnList.TabStop = false;
            // 
            // lblTotalSpawn
            // 
            this.lblTotalSpawn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTotalSpawn.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblTotalSpawn.Location = new System.Drawing.Point(8, 436);
            this.lblTotalSpawn.Name = "lblTotalSpawn";
            this.lblTotalSpawn.Size = new System.Drawing.Size(152, 16);
            this.lblTotalSpawn.TabIndex = 4;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.treeRegionView);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(168, 462);
            this.tabPage4.TabIndex = 1;
            this.tabPage4.Text = "Regions";
            this.tabPage4.ToolTipText = "Currently defined region locations.  Select one to automatically move to its Go l" +
    "ocation.";
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.treeGoView);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Size = new System.Drawing.Size(168, 462);
            this.tabPage5.TabIndex = 2;
            this.tabPage5.Text = "Go";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(8, 184);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(152, 16);
            this.progressBar1.TabIndex = 16;
            this.progressBar1.Visible = false;
            // 
            // lblTransferStatus
            // 
            this.lblTransferStatus.Location = new System.Drawing.Point(8, 168);
            this.lblTransferStatus.Name = "lblTransferStatus";
            this.lblTransferStatus.Size = new System.Drawing.Size(152, 16);
            this.lblTransferStatus.TabIndex = 238;
            this.lblTransferStatus.Text = "Status";
            this.lblTransferStatus.Visible = false;
            // 
            // grpSpawnTypes
            // 
            this.grpSpawnTypes.Controls.Add(this.btnAddToSpawnPack);
            this.grpSpawnTypes.Controls.Add(this.radShowMobilesOnly);
            this.grpSpawnTypes.Controls.Add(this.radShowItemsOnly);
            this.grpSpawnTypes.Controls.Add(this.radShowAll);
            this.grpSpawnTypes.Controls.Add(this.btnResetTypes);
            this.grpSpawnTypes.Controls.Add(this.clbRunUOTypes);
            this.grpSpawnTypes.Controls.Add(this.lblTotalTypesLoaded);
            this.grpSpawnTypes.Controls.Add(this.btnUpdateSpawn);
            this.grpSpawnTypes.Location = new System.Drawing.Point(0, 0);
            this.grpSpawnTypes.Name = "grpSpawnTypes";
            this.grpSpawnTypes.Size = new System.Drawing.Size(176, 440);
            this.grpSpawnTypes.TabIndex = 1;
            this.grpSpawnTypes.TabStop = false;
            this.grpSpawnTypes.Text = "All Spawn Types";
            // 
            // lblTotalTypesLoaded
            // 
            this.lblTotalTypesLoaded.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTotalTypesLoaded.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblTotalTypesLoaded.Location = new System.Drawing.Point(8, 416);
            this.lblTotalTypesLoaded.Name = "lblTotalTypesLoaded";
            this.lblTotalTypesLoaded.Size = new System.Drawing.Size(160, 16);
            this.lblTotalTypesLoaded.TabIndex = 5;
            // 
            // mncSpawns
            // 
            this.mncSpawns.Popup += new System.EventHandler(this.mncSpawns_Popup);
            // 
            // menuItem3
            // 
            this.menuItem3.Index = -1;
            this.menuItem3.Text = "-";
            // 
            // mniDeleteSpawn
            // 
            this.mniDeleteSpawn.Index = -1;
            this.mniDeleteSpawn.Text = "&Delete";
            this.mniDeleteSpawn.Click += new System.EventHandler(this.mniDeleteSpawn_Click);
            // 
            // mniDeleteAllSpawns
            // 
            this.mniDeleteAllSpawns.Index = -1;
            this.mniDeleteAllSpawns.Text = "Delete &All";
            this.mniDeleteAllSpawns.Click += new System.EventHandler(this.mniDeleteAllSpawns_Click);
            // 
            // ofdLoadFile
            // 
            this.ofdLoadFile.DefaultExt = "xml";
            this.ofdLoadFile.Filter = "Spawn Files (*.xml)|*.xml|All Files (*.*)|*.*";
            this.ofdLoadFile.Title = "Load Spawn File";
            // 
            // sfdSaveFile
            // 
            this.sfdSaveFile.DefaultExt = "xml";
            this.sfdSaveFile.FileName = "Spawns";
            this.sfdSaveFile.Filter = "Spawn Files (*.xml)|*.xml|All Files (*.*)|*.*";
            this.sfdSaveFile.Title = "Save Spawn File";
            // 
            // stbMain
            // 
            this.stbMain.Location = new System.Drawing.Point(0, 717);
            this.stbMain.Name = "stbMain";
            this.stbMain.Size = new System.Drawing.Size(1113, 16);
            this.stbMain.TabIndex = 3;
            this.stbMain.Text = "Spawn Editor";
            // 
            // menuItem1
            // 
            this.menuItem1.Index = -1;
            this.menuItem1.Text = "Delete Entry";
            this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
            // 
            // menuItem2
            // 
            this.menuItem2.Index = -1;
            this.menuItem2.Text = "Delete All Entries";
            this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
            // 
            // menuItem15
            // 
            this.menuItem15.Index = -1;
            this.menuItem15.Text = "Add to SpawnPack";
            this.menuItem15.Click += new System.EventHandler(this.menuItem15_Click);
            // 
            // grpSpawnEdit
            // 
            this.grpSpawnEdit.Anchor = System.Windows.Forms.AnchorStyles.Left;
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
            this.grpSpawnEdit.Location = new System.Drawing.Point(5, 0);
            this.grpSpawnEdit.Name = "grpSpawnEdit";
            this.grpSpawnEdit.Size = new System.Drawing.Size(360, 440);
            this.grpSpawnEdit.TabIndex = 0;
            this.grpSpawnEdit.TabStop = false;
            this.grpSpawnEdit.Text = "Spawn Details";
            this.grpSpawnEdit.Leave += new System.EventHandler(this.grpSpawnEdit_Leave);
            // 
            // btnSendSingleSpawner
            // 
            this.btnSendSingleSpawner.ContextMenu = this.unloadSingleSpawner;
            this.btnSendSingleSpawner.Enabled = false;
            this.btnSendSingleSpawner.Location = new System.Drawing.Point(224, 408);
            this.btnSendSingleSpawner.Name = "btnSendSingleSpawner";
            this.btnSendSingleSpawner.Size = new System.Drawing.Size(120, 23);
            this.btnSendSingleSpawner.TabIndex = 208;
            this.btnSendSingleSpawner.Text = "Send to Server";
            this.btnSendSingleSpawner.Click += new System.EventHandler(this.btnSendSpawn_Click);
            // 
            // unloadSingleSpawner
            // 
            this.unloadSingleSpawner.Popup += new System.EventHandler(this.unloadSingleSpawner_Popup);
            // 
            // label26
            // 
            this.label26.Location = new System.Drawing.Point(8, 340);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(112, 20);
            this.label26.TabIndex = 200;
            this.label26.Text = "TrigObjectProp";
            // 
            // textTrigObjectProp
            // 
            this.textTrigObjectProp.Location = new System.Drawing.Point(120, 340);
            this.textTrigObjectProp.Name = "textTrigObjectProp";
            this.textTrigObjectProp.Size = new System.Drawing.Size(232, 20);
            this.textTrigObjectProp.TabIndex = 199;
            // 
            // label17
            // 
            this.label17.Location = new System.Drawing.Point(8, 260);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(112, 20);
            this.label17.TabIndex = 175;
            this.label17.Text = "SkillTrigger";
            // 
            // textSkillTrigger
            // 
            this.textSkillTrigger.Location = new System.Drawing.Point(120, 260);
            this.textSkillTrigger.Name = "textSkillTrigger";
            this.textSkillTrigger.Size = new System.Drawing.Size(232, 20);
            this.textSkillTrigger.TabIndex = 174;
            // 
            // label16
            // 
            this.label16.Location = new System.Drawing.Point(8, 280);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(112, 16);
            this.label16.TabIndex = 172;
            this.label16.Text = "SpeechTrigger";
            // 
            // textSpeechTrigger
            // 
            this.textSpeechTrigger.Location = new System.Drawing.Point(120, 280);
            this.textSpeechTrigger.Name = "textSpeechTrigger";
            this.textSpeechTrigger.Size = new System.Drawing.Size(232, 20);
            this.textSpeechTrigger.TabIndex = 171;
            // 
            // label15
            // 
            this.label15.Location = new System.Drawing.Point(8, 300);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(112, 20);
            this.label15.TabIndex = 169;
            this.label15.Text = "ProximityMsg";
            // 
            // textProximityMsg
            // 
            this.textProximityMsg.Location = new System.Drawing.Point(120, 300);
            this.textProximityMsg.Name = "textProximityMsg";
            this.textProximityMsg.Size = new System.Drawing.Size(232, 20);
            this.textProximityMsg.TabIndex = 168;
            // 
            // label14
            // 
            this.label14.Location = new System.Drawing.Point(8, 320);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(112, 16);
            this.label14.TabIndex = 160;
            this.label14.Text = "PlayerTrigProp";
            // 
            // textPlayerTrigProp
            // 
            this.textPlayerTrigProp.Location = new System.Drawing.Point(120, 320);
            this.textPlayerTrigProp.Name = "textPlayerTrigProp";
            this.textPlayerTrigProp.Size = new System.Drawing.Size(232, 20);
            this.textPlayerTrigProp.TabIndex = 159;
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(8, 380);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(112, 20);
            this.label12.TabIndex = 154;
            this.label12.Text = "NoTriggerOnCarried";
            // 
            // textNoTriggerOnCarried
            // 
            this.textNoTriggerOnCarried.Location = new System.Drawing.Point(120, 380);
            this.textNoTriggerOnCarried.Name = "textNoTriggerOnCarried";
            this.textNoTriggerOnCarried.Size = new System.Drawing.Size(232, 20);
            this.textNoTriggerOnCarried.TabIndex = 153;
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(8, 360);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(112, 16);
            this.label11.TabIndex = 151;
            this.label11.Text = "TriggerOnCarried";
            // 
            // textTriggerOnCarried
            // 
            this.textTriggerOnCarried.Location = new System.Drawing.Point(120, 360);
            this.textTriggerOnCarried.Name = "textTriggerOnCarried";
            this.textTriggerOnCarried.Size = new System.Drawing.Size(232, 20);
            this.textTriggerOnCarried.TabIndex = 150;
            // 
            // mniUnloadSingleSpawner
            // 
            this.mniUnloadSingleSpawner.Index = -1;
            this.mniUnloadSingleSpawner.Text = "Unload Spawner from Server";
            this.mniUnloadSingleSpawner.Click += new System.EventHandler(this.mniUnloadSingleSpawner_Click_1);
            // 
            // menuItem23
            // 
            this.menuItem23.Index = -1;
            this.menuItem23.Text = "Cancel";
            // 
            // menuItem5
            // 
            this.menuItem5.Index = -1;
            this.menuItem5.Text = "File";
            // 
            // menuItem6
            // 
            this.menuItem6.Index = -1;
            this.menuItem6.Text = "Load Spawn Packs";
            this.menuItem6.Click += new System.EventHandler(this.menuItem6_Click);
            // 
            // menuItem7
            // 
            this.menuItem7.Index = -1;
            this.menuItem7.Text = "Save Spawn Packs";
            this.menuItem7.Click += new System.EventHandler(this.menuItem7_Click);
            // 
            // menuItem10
            // 
            this.menuItem10.Index = -1;
            this.menuItem10.Text = "Import All Spawn Types";
            this.menuItem10.Click += new System.EventHandler(this.menuItem10_Click);
            // 
            // menuItem11
            // 
            this.menuItem11.Index = -1;
            this.menuItem11.Text = "Export All Spawn Types";
            this.menuItem11.Click += new System.EventHandler(this.menuItem11_Click);
            // 
            // menuItem12
            // 
            this.menuItem12.Index = -1;
            this.menuItem12.Text = "Import .map file";
            this.menuItem12.Click += new System.EventHandler(this.menuItem12_Click);
            // 
            // menuItem13
            // 
            this.menuItem13.Index = -1;
            this.menuItem13.Text = "Import .msf file";
            this.menuItem13.Click += new System.EventHandler(this.menuItem13_Click);
            // 
            // menuItem22
            // 
            this.menuItem22.Index = -1;
            this.menuItem22.Text = "Edit";
            // 
            // menuItem24
            // 
            this.menuItem24.Index = -1;
            this.menuItem24.Text = "Delete";
            // 
            // mniDeleteInSelectionWindow
            // 
            this.mniDeleteInSelectionWindow.Index = -1;
            this.mniDeleteInSelectionWindow.Text = "Spawns in Selection Window";
            this.mniDeleteInSelectionWindow.Click += new System.EventHandler(this.mniDeleteInSelectionWindow_Click);
            // 
            // mniDeleteNotSelected
            // 
            this.mniDeleteNotSelected.Index = -1;
            this.mniDeleteNotSelected.Text = "Spawns NOT in Selection Window";
            this.mniDeleteNotSelected.Click += new System.EventHandler(this.mniDeleteNotSelected_Click);
            // 
            // mniToolbarDeleteAllSpawns
            // 
            this.mniToolbarDeleteAllSpawns.Index = -1;
            this.mniToolbarDeleteAllSpawns.Text = "All Spawns";
            this.mniToolbarDeleteAllSpawns.Click += new System.EventHandler(this.mniToolbarDeleteAllSpawns_Click);
            // 
            // mniDeleteAllFiltered
            // 
            this.mniDeleteAllFiltered.Index = -1;
            this.mniDeleteAllFiltered.Text = "Filtered Spawns (gray, not displayed) ";
            this.mniDeleteAllFiltered.Click += new System.EventHandler(this.mniDeleteAllFiltered_Click);
            // 
            // mniDeleteAllUnfiltered
            // 
            this.mniDeleteAllUnfiltered.Index = -1;
            this.mniDeleteAllUnfiltered.Text = "Un-Filtered Spawns (black, displayed) ";
            this.mniDeleteAllUnfiltered.Click += new System.EventHandler(this.mniDeleteAllUnfiltered_Click);
            // 
            // menuItem25
            // 
            this.menuItem25.Index = -1;
            this.menuItem25.Text = "Modify Properties";
            // 
            // mniModifyInSelectionWindow
            // 
            this.mniModifyInSelectionWindow.Index = -1;
            this.mniModifyInSelectionWindow.Text = "of Spawns in Selection Window";
            this.mniModifyInSelectionWindow.Click += new System.EventHandler(this.mniModifyInSelectionWindow_Click);
            // 
            // mniModifiedUnfiltered
            // 
            this.mniModifiedUnfiltered.Index = -1;
            this.mniModifiedUnfiltered.Text = "of Un-Filtered Spawns (black, displayed)";
            this.mniModifiedUnfiltered.Click += new System.EventHandler(this.mniModifiedUnfiltered_Click);
            // 
            // menuItem8
            // 
            this.menuItem8.Index = -1;
            this.menuItem8.Text = "Tools";
            // 
            // menuItem9
            // 
            this.menuItem9.Index = -1;
            this.menuItem9.Text = "Setup";
            this.menuItem9.Click += new System.EventHandler(this.menuItem9_Click);
            // 
            // menuItem17
            // 
            this.menuItem17.Index = -1;
            this.menuItem17.Text = "Transfer Server Settings";
            this.menuItem17.Click += new System.EventHandler(this.menuItem17_Click);
            // 
            // mniDisplayFilterSettings
            // 
            this.mniDisplayFilterSettings.Index = -1;
            this.mniDisplayFilterSettings.Text = "Display Filter Settings";
            this.mniDisplayFilterSettings.Click += new System.EventHandler(this.mniDisplayFilterSettings_Click);
            // 
            // menuItem14
            // 
            this.menuItem14.Index = -1;
            this.menuItem14.Text = "Options";
            // 
            // mniAlwaysOnTop
            // 
            this.mniAlwaysOnTop.Index = -1;
            this.mniAlwaysOnTop.Text = "Always On Top";
            this.mniAlwaysOnTop.Click += new System.EventHandler(this.mniAlwaysOnTop_Click);
            // 
            // menuItem16
            // 
            this.menuItem16.Index = -1;
            this.menuItem16.Text = "Help";
            // 
            // menuItem18
            // 
            this.menuItem18.Index = -1;
            this.menuItem18.Text = "Help";
            this.menuItem18.Click += new System.EventHandler(this.menuItem18_Click);
            // 
            // menuItem4
            // 
            this.menuItem4.Index = -1;
            this.menuItem4.Text = "About";
            this.menuItem4.Click += new System.EventHandler(this.menuItem4_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.tabControl1);
            this.panel1.Controls.Add(this.axUOMap);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Cursor = System.Windows.Forms.Cursors.Default;
            this.panel1.Location = new System.Drawing.Point(172, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(945, 723);
            this.panel1.TabIndex = 5;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabBasic);
            this.tabControl1.Controls.Add(this.tabAdvanced);
            this.tabControl1.Controls.Add(this.tabSpawnTypes);
            this.tabControl1.Location = new System.Drawing.Point(480, 1);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(376, 465);
            this.tabControl1.TabIndex = 8;
            this.tabControl1.Leave += new System.EventHandler(this.tabControl1_Leave);
            // 
            // tabBasic
            // 
            this.tabBasic.Controls.Add(this.grpSpawnEdit);
            this.tabBasic.Location = new System.Drawing.Point(4, 22);
            this.tabBasic.Name = "tabBasic";
            this.tabBasic.Size = new System.Drawing.Size(368, 439);
            this.tabBasic.TabIndex = 0;
            this.tabBasic.Text = "Basic";
            // 
            // tabAdvanced
            // 
            this.tabAdvanced.Controls.Add(this.groupBox1);
            this.tabAdvanced.Location = new System.Drawing.Point(4, 22);
            this.tabAdvanced.Name = "tabAdvanced";
            this.tabAdvanced.Size = new System.Drawing.Size(368, 439);
            this.tabAdvanced.TabIndex = 1;
            this.tabAdvanced.Text = "Advanced";
            // 
            // groupBox1
            // 
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
            this.groupBox1.Location = new System.Drawing.Point(5, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(360, 440);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Spawn Details";
            this.groupBox1.Leave += new System.EventHandler(this.groupBox1_Leave);
            // 
            // label44
            // 
            this.label44.Location = new System.Drawing.Point(8, 344);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(64, 16);
            this.label44.TabIndex = 237;
            this.label44.Text = "Notes:";
            // 
            // txtNotes
            // 
            this.txtNotes.Location = new System.Drawing.Point(8, 360);
            this.txtNotes.Multiline = true;
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.Size = new System.Drawing.Size(344, 72);
            this.txtNotes.TabIndex = 236;
            // 
            // label37
            // 
            this.label37.Location = new System.Drawing.Point(8, 128);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(112, 16);
            this.label37.TabIndex = 232;
            this.label37.Text = "RegionName:";
            // 
            // textRegionName
            // 
            this.textRegionName.Location = new System.Drawing.Point(120, 128);
            this.textRegionName.Name = "textRegionName";
            this.textRegionName.Size = new System.Drawing.Size(232, 20);
            this.textRegionName.TabIndex = 231;
            // 
            // label36
            // 
            this.label36.Location = new System.Drawing.Point(8, 152);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(112, 16);
            this.label36.TabIndex = 230;
            this.label36.Text = "WaypointName:";
            // 
            // textWayPoint
            // 
            this.textWayPoint.Location = new System.Drawing.Point(120, 152);
            this.textWayPoint.Name = "textWayPoint";
            this.textWayPoint.Size = new System.Drawing.Size(232, 20);
            this.textWayPoint.TabIndex = 229;
            // 
            // label35
            // 
            this.label35.Location = new System.Drawing.Point(8, 176);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(112, 16);
            this.label35.TabIndex = 228;
            this.label35.Text = "ConfigFile:";
            // 
            // textConfigFile
            // 
            this.textConfigFile.Location = new System.Drawing.Point(120, 176);
            this.textConfigFile.Name = "textConfigFile";
            this.textConfigFile.Size = new System.Drawing.Size(232, 20);
            this.textConfigFile.TabIndex = 227;
            // 
            // label34
            // 
            this.label34.Location = new System.Drawing.Point(8, 272);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(112, 16);
            this.label34.TabIndex = 226;
            this.label34.Text = "SetObjectName:";
            // 
            // textSetObjectName
            // 
            this.textSetObjectName.Location = new System.Drawing.Point(120, 272);
            this.textSetObjectName.Name = "textSetObjectName";
            this.textSetObjectName.Size = new System.Drawing.Size(232, 20);
            this.textSetObjectName.TabIndex = 225;
            // 
            // label33
            // 
            this.label33.Location = new System.Drawing.Point(8, 248);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(112, 16);
            this.label33.TabIndex = 224;
            this.label33.Text = "TrigObjectName:";
            // 
            // textTrigObjectName
            // 
            this.textTrigObjectName.Location = new System.Drawing.Point(120, 248);
            this.textTrigObjectName.Name = "textTrigObjectName";
            this.textTrigObjectName.Size = new System.Drawing.Size(232, 20);
            this.textTrigObjectName.TabIndex = 223;
            // 
            // labelContainerZ
            // 
            this.labelContainerZ.Enabled = false;
            this.labelContainerZ.Location = new System.Drawing.Point(232, 80);
            this.labelContainerZ.Name = "labelContainerZ";
            this.labelContainerZ.Size = new System.Drawing.Size(16, 16);
            this.labelContainerZ.TabIndex = 219;
            this.labelContainerZ.Text = "Z:";
            // 
            // labelContainerY
            // 
            this.labelContainerY.Enabled = false;
            this.labelContainerY.Location = new System.Drawing.Point(232, 56);
            this.labelContainerY.Name = "labelContainerY";
            this.labelContainerY.Size = new System.Drawing.Size(16, 16);
            this.labelContainerY.TabIndex = 217;
            this.labelContainerY.Text = "Y:";
            // 
            // labelContainerX
            // 
            this.labelContainerX.Enabled = false;
            this.labelContainerX.Location = new System.Drawing.Point(184, 32);
            this.labelContainerX.Name = "labelContainerX";
            this.labelContainerX.Size = new System.Drawing.Size(72, 16);
            this.labelContainerX.TabIndex = 215;
            this.labelContainerX.Text = "Container X:";
            // 
            // label32
            // 
            this.label32.Location = new System.Drawing.Point(8, 32);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(88, 20);
            this.label32.TabIndex = 201;
            this.label32.Text = "StackAmount:";
            // 
            // label31
            // 
            this.label31.Location = new System.Drawing.Point(8, 56);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(104, 20);
            this.label31.TabIndex = 199;
            this.label31.Text = "TriggerProbability:";
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(8, 200);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(112, 16);
            this.label13.TabIndex = 170;
            this.label13.Text = "MobTriggerName:";
            // 
            // textMobTriggerName
            // 
            this.textMobTriggerName.Location = new System.Drawing.Point(120, 200);
            this.textMobTriggerName.Name = "textMobTriggerName";
            this.textMobTriggerName.Size = new System.Drawing.Size(232, 20);
            this.textMobTriggerName.TabIndex = 169;
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(8, 224);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(112, 20);
            this.label10.TabIndex = 168;
            this.label10.Text = "MobTrigProp:";
            // 
            // textMobTrigProp
            // 
            this.textMobTrigProp.Location = new System.Drawing.Point(120, 224);
            this.textMobTrigProp.Name = "textMobTrigProp";
            this.textMobTrigProp.Size = new System.Drawing.Size(232, 20);
            this.textMobTrigProp.TabIndex = 167;
            // 
            // tabSpawnTypes
            // 
            this.tabSpawnTypes.Controls.Add(this.groupBox3);
            this.tabSpawnTypes.Controls.Add(this.groupBox2);
            this.tabSpawnTypes.Controls.Add(this.grpSpawnTypes);
            this.tabSpawnTypes.Location = new System.Drawing.Point(4, 22);
            this.tabSpawnTypes.Name = "tabSpawnTypes";
            this.tabSpawnTypes.Size = new System.Drawing.Size(368, 439);
            this.tabSpawnTypes.TabIndex = 2;
            this.tabSpawnTypes.Text = "SpawnTypes";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tvwSpawnPacks);
            this.groupBox3.Location = new System.Drawing.Point(184, 288);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(176, 152);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "All Spawn Packs";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnUpdateSpawnPacks);
            this.groupBox2.Controls.Add(this.textSpawnPackName);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Controls.Add(this.clbSpawnPack);
            this.groupBox2.Controls.Add(this.label39);
            this.groupBox2.Controls.Add(this.btnUpdateFromSpawnPack);
            this.groupBox2.Location = new System.Drawing.Point(184, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(176, 288);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Current Spawn Pack";
            // 
            // textSpawnPackName
            // 
            this.textSpawnPackName.Location = new System.Drawing.Point(8, 16);
            this.textSpawnPackName.Name = "textSpawnPackName";
            this.textSpawnPackName.Size = new System.Drawing.Size(160, 20);
            this.textSpawnPackName.TabIndex = 16;
            // 
            // label39
            // 
            this.label39.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label39.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label39.Location = new System.Drawing.Point(8, 264);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(160, 16);
            this.label39.TabIndex = 5;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.grpSpawnEntries);
            this.panel3.Location = new System.Drawing.Point(8, 472);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(848, 248);
            this.panel3.TabIndex = 7;
            this.panel3.Visible = false;
            // 
            // mcnSpawnPack
            // 
            this.mcnSpawnPack.Popup += new System.EventHandler(this.mcnSpawnPack_Popup);
            // 
            // mniDeleteType
            // 
            this.mniDeleteType.Index = -1;
            this.mniDeleteType.Text = "Delete Type";
            this.mniDeleteType.Click += new System.EventHandler(this.mniDeleteType_Click);
            // 
            // mniDeleteAllTypes
            // 
            this.mniDeleteAllTypes.Index = -1;
            this.mniDeleteAllTypes.Text = "Delete Alll Types";
            this.mniDeleteAllTypes.Click += new System.EventHandler(this.mniDeleteAllTypes_Click);
            // 
            // mniDeletePack
            // 
            this.mniDeletePack.Index = -1;
            this.mniDeletePack.Text = "Delete Pack";
            this.mniDeletePack.Click += new System.EventHandler(this.mniDeletePack_Click);
            // 
            // openSpawnPacks
            // 
            this.openSpawnPacks.FileName = "SpawnPacks.dat";
            this.openSpawnPacks.InitialDirectory = ".";
            // 
            // saveSpawnPacks
            // 
            this.saveSpawnPacks.FileName = "SpawnPacks.dat";
            this.saveSpawnPacks.InitialDirectory = ".";
            // 
            // exportAllSpawnTypes
            // 
            this.exportAllSpawnTypes.FileName = "SpawnTypes.std";
            this.exportAllSpawnTypes.InitialDirectory = ".";
            // 
            // importAllSpawnTypes
            // 
            this.importAllSpawnTypes.FileName = "SpawnTypes.std";
            this.importAllSpawnTypes.InitialDirectory = ".";
            // 
            // importMapFile
            // 
            this.importMapFile.Filter = ".map | *.map";
            // 
            // importMSFFile
            // 
            this.importMSFFile.Filter = ".msf | *.msf";
            // 
            // grpSpawnEntries
            // 
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
            this.grpSpawnEntries.Location = new System.Drawing.Point(239, 21);
            this.grpSpawnEntries.Name = "grpSpawnEntries";
            this.grpSpawnEntries.Size = new System.Drawing.Size(644, 224);
            this.grpSpawnEntries.TabIndex = 10;
            this.grpSpawnEntries.TabStop = false;
            this.grpSpawnEntries.Text = "Spawn Entries";
            // 
            // entryPer8
            // 
            this.entryPer8.Location = new System.Drawing.Point(272, 200);
            this.entryPer8.Name = "entryPer8";
            this.entryPer8.Size = new System.Drawing.Size(48, 20);
            this.entryPer8.TabIndex = 145;
            // 
            // entryPer7
            // 
            this.entryPer7.Location = new System.Drawing.Point(272, 176);
            this.entryPer7.Name = "entryPer7";
            this.entryPer7.Size = new System.Drawing.Size(48, 20);
            this.entryPer7.TabIndex = 144;
            // 
            // entryPer6
            // 
            this.entryPer6.Location = new System.Drawing.Point(272, 152);
            this.entryPer6.Name = "entryPer6";
            this.entryPer6.Size = new System.Drawing.Size(48, 20);
            this.entryPer6.TabIndex = 143;
            // 
            // entryPer5
            // 
            this.entryPer5.Location = new System.Drawing.Point(272, 128);
            this.entryPer5.Name = "entryPer5";
            this.entryPer5.Size = new System.Drawing.Size(48, 20);
            this.entryPer5.TabIndex = 142;
            // 
            // entryPer4
            // 
            this.entryPer4.Location = new System.Drawing.Point(272, 104);
            this.entryPer4.Name = "entryPer4";
            this.entryPer4.Size = new System.Drawing.Size(48, 20);
            this.entryPer4.TabIndex = 141;
            // 
            // entryPer3
            // 
            this.entryPer3.Location = new System.Drawing.Point(272, 80);
            this.entryPer3.Name = "entryPer3";
            this.entryPer3.Size = new System.Drawing.Size(48, 20);
            this.entryPer3.TabIndex = 140;
            // 
            // entryPer2
            // 
            this.entryPer2.Location = new System.Drawing.Point(272, 56);
            this.entryPer2.Name = "entryPer2";
            this.entryPer2.Size = new System.Drawing.Size(48, 20);
            this.entryPer2.TabIndex = 139;
            // 
            // entryPer1
            // 
            this.entryPer1.Location = new System.Drawing.Point(272, 32);
            this.entryPer1.Name = "entryPer1";
            this.entryPer1.Size = new System.Drawing.Size(48, 20);
            this.entryPer1.TabIndex = 138;
            // 
            // label30
            // 
            this.label30.Location = new System.Drawing.Point(272, 16);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(40, 16);
            this.label30.TabIndex = 137;
            this.label30.Text = "Per";
            this.ttpSpawnInfo.SetToolTip(this.label30, "Number of spawns of this type created when the entry is spawned.");
            // 
            // entryMaxD8
            // 
            this.entryMaxD8.Location = new System.Drawing.Point(512, 200);
            this.entryMaxD8.Name = "entryMaxD8";
            this.entryMaxD8.Size = new System.Drawing.Size(48, 20);
            this.entryMaxD8.TabIndex = 136;
            // 
            // entryMaxD7
            // 
            this.entryMaxD7.Location = new System.Drawing.Point(512, 176);
            this.entryMaxD7.Name = "entryMaxD7";
            this.entryMaxD7.Size = new System.Drawing.Size(48, 20);
            this.entryMaxD7.TabIndex = 135;
            // 
            // entryMaxD6
            // 
            this.entryMaxD6.Location = new System.Drawing.Point(512, 152);
            this.entryMaxD6.Name = "entryMaxD6";
            this.entryMaxD6.Size = new System.Drawing.Size(48, 20);
            this.entryMaxD6.TabIndex = 134;
            // 
            // entryMaxD5
            // 
            this.entryMaxD5.Location = new System.Drawing.Point(512, 128);
            this.entryMaxD5.Name = "entryMaxD5";
            this.entryMaxD5.Size = new System.Drawing.Size(48, 20);
            this.entryMaxD5.TabIndex = 133;
            // 
            // entryMaxD4
            // 
            this.entryMaxD4.Location = new System.Drawing.Point(512, 104);
            this.entryMaxD4.Name = "entryMaxD4";
            this.entryMaxD4.Size = new System.Drawing.Size(48, 20);
            this.entryMaxD4.TabIndex = 132;
            // 
            // entryMaxD3
            // 
            this.entryMaxD3.Location = new System.Drawing.Point(512, 80);
            this.entryMaxD3.Name = "entryMaxD3";
            this.entryMaxD3.Size = new System.Drawing.Size(48, 20);
            this.entryMaxD3.TabIndex = 131;
            // 
            // entryMaxD2
            // 
            this.entryMaxD2.Location = new System.Drawing.Point(512, 56);
            this.entryMaxD2.Name = "entryMaxD2";
            this.entryMaxD2.Size = new System.Drawing.Size(48, 20);
            this.entryMaxD2.TabIndex = 130;
            // 
            // entryMaxD1
            // 
            this.entryMaxD1.Location = new System.Drawing.Point(512, 32);
            this.entryMaxD1.Name = "entryMaxD1";
            this.entryMaxD1.Size = new System.Drawing.Size(48, 20);
            this.entryMaxD1.TabIndex = 129;
            // 
            // entryMinD8
            // 
            this.entryMinD8.Location = new System.Drawing.Point(464, 200);
            this.entryMinD8.Name = "entryMinD8";
            this.entryMinD8.Size = new System.Drawing.Size(48, 20);
            this.entryMinD8.TabIndex = 128;
            // 
            // entryMinD7
            // 
            this.entryMinD7.Location = new System.Drawing.Point(464, 176);
            this.entryMinD7.Name = "entryMinD7";
            this.entryMinD7.Size = new System.Drawing.Size(48, 20);
            this.entryMinD7.TabIndex = 127;
            // 
            // entryMinD6
            // 
            this.entryMinD6.Location = new System.Drawing.Point(464, 152);
            this.entryMinD6.Name = "entryMinD6";
            this.entryMinD6.Size = new System.Drawing.Size(48, 20);
            this.entryMinD6.TabIndex = 126;
            // 
            // entryMinD5
            // 
            this.entryMinD5.Location = new System.Drawing.Point(464, 128);
            this.entryMinD5.Name = "entryMinD5";
            this.entryMinD5.Size = new System.Drawing.Size(48, 20);
            this.entryMinD5.TabIndex = 125;
            // 
            // entryMinD4
            // 
            this.entryMinD4.Location = new System.Drawing.Point(464, 104);
            this.entryMinD4.Name = "entryMinD4";
            this.entryMinD4.Size = new System.Drawing.Size(48, 20);
            this.entryMinD4.TabIndex = 124;
            // 
            // entryMinD3
            // 
            this.entryMinD3.Location = new System.Drawing.Point(464, 80);
            this.entryMinD3.Name = "entryMinD3";
            this.entryMinD3.Size = new System.Drawing.Size(48, 20);
            this.entryMinD3.TabIndex = 123;
            // 
            // entryMinD2
            // 
            this.entryMinD2.Location = new System.Drawing.Point(464, 56);
            this.entryMinD2.Name = "entryMinD2";
            this.entryMinD2.Size = new System.Drawing.Size(48, 20);
            this.entryMinD2.TabIndex = 122;
            // 
            // entryMinD1
            // 
            this.entryMinD1.Location = new System.Drawing.Point(464, 32);
            this.entryMinD1.Name = "entryMinD1";
            this.entryMinD1.Size = new System.Drawing.Size(48, 20);
            this.entryMinD1.TabIndex = 121;
            // 
            // entryKills8
            // 
            this.entryKills8.Location = new System.Drawing.Point(432, 200);
            this.entryKills8.Name = "entryKills8";
            this.entryKills8.Size = new System.Drawing.Size(32, 20);
            this.entryKills8.TabIndex = 120;
            // 
            // entryKills7
            // 
            this.entryKills7.Location = new System.Drawing.Point(432, 176);
            this.entryKills7.Name = "entryKills7";
            this.entryKills7.Size = new System.Drawing.Size(32, 20);
            this.entryKills7.TabIndex = 119;
            // 
            // entryKills6
            // 
            this.entryKills6.Location = new System.Drawing.Point(432, 152);
            this.entryKills6.Name = "entryKills6";
            this.entryKills6.Size = new System.Drawing.Size(32, 20);
            this.entryKills6.TabIndex = 118;
            // 
            // entryKills5
            // 
            this.entryKills5.Location = new System.Drawing.Point(432, 128);
            this.entryKills5.Name = "entryKills5";
            this.entryKills5.Size = new System.Drawing.Size(32, 20);
            this.entryKills5.TabIndex = 117;
            // 
            // entryKills4
            // 
            this.entryKills4.Location = new System.Drawing.Point(432, 104);
            this.entryKills4.Name = "entryKills4";
            this.entryKills4.Size = new System.Drawing.Size(32, 20);
            this.entryKills4.TabIndex = 116;
            // 
            // entryKills3
            // 
            this.entryKills3.Location = new System.Drawing.Point(432, 80);
            this.entryKills3.Name = "entryKills3";
            this.entryKills3.Size = new System.Drawing.Size(32, 20);
            this.entryKills3.TabIndex = 115;
            // 
            // entryKills2
            // 
            this.entryKills2.Location = new System.Drawing.Point(432, 56);
            this.entryKills2.Name = "entryKills2";
            this.entryKills2.Size = new System.Drawing.Size(32, 20);
            this.entryKills2.TabIndex = 114;
            // 
            // entryKills1
            // 
            this.entryKills1.Location = new System.Drawing.Point(432, 32);
            this.entryKills1.Name = "entryKills1";
            this.entryKills1.Size = new System.Drawing.Size(32, 20);
            this.entryKills1.TabIndex = 113;
            // 
            // entryReset8
            // 
            this.entryReset8.Location = new System.Drawing.Point(352, 200);
            this.entryReset8.Name = "entryReset8";
            this.entryReset8.Size = new System.Drawing.Size(48, 20);
            this.entryReset8.TabIndex = 112;
            // 
            // entryReset7
            // 
            this.entryReset7.Location = new System.Drawing.Point(352, 176);
            this.entryReset7.Name = "entryReset7";
            this.entryReset7.Size = new System.Drawing.Size(48, 20);
            this.entryReset7.TabIndex = 111;
            // 
            // entryReset6
            // 
            this.entryReset6.Location = new System.Drawing.Point(352, 152);
            this.entryReset6.Name = "entryReset6";
            this.entryReset6.Size = new System.Drawing.Size(48, 20);
            this.entryReset6.TabIndex = 110;
            // 
            // entryReset5
            // 
            this.entryReset5.Location = new System.Drawing.Point(352, 128);
            this.entryReset5.Name = "entryReset5";
            this.entryReset5.Size = new System.Drawing.Size(48, 20);
            this.entryReset5.TabIndex = 109;
            // 
            // entryReset4
            // 
            this.entryReset4.Location = new System.Drawing.Point(352, 104);
            this.entryReset4.Name = "entryReset4";
            this.entryReset4.Size = new System.Drawing.Size(48, 20);
            this.entryReset4.TabIndex = 108;
            // 
            // entryReset3
            // 
            this.entryReset3.Location = new System.Drawing.Point(352, 80);
            this.entryReset3.Name = "entryReset3";
            this.entryReset3.Size = new System.Drawing.Size(48, 20);
            this.entryReset3.TabIndex = 107;
            // 
            // entryReset2
            // 
            this.entryReset2.Location = new System.Drawing.Point(352, 56);
            this.entryReset2.Name = "entryReset2";
            this.entryReset2.Size = new System.Drawing.Size(48, 20);
            this.entryReset2.TabIndex = 106;
            // 
            // entryReset1
            // 
            this.entryReset1.Location = new System.Drawing.Point(352, 32);
            this.entryReset1.Name = "entryReset1";
            this.entryReset1.Size = new System.Drawing.Size(48, 20);
            this.entryReset1.TabIndex = 105;
            // 
            // entryTo8
            // 
            this.entryTo8.Location = new System.Drawing.Point(400, 200);
            this.entryTo8.Name = "entryTo8";
            this.entryTo8.Size = new System.Drawing.Size(32, 20);
            this.entryTo8.TabIndex = 103;
            // 
            // entrySub8
            // 
            this.entrySub8.Location = new System.Drawing.Point(320, 200);
            this.entrySub8.Name = "entrySub8";
            this.entrySub8.Size = new System.Drawing.Size(32, 20);
            this.entrySub8.TabIndex = 102;
            // 
            // chkRK8
            // 
            this.chkRK8.Location = new System.Drawing.Point(568, 204);
            this.chkRK8.Name = "chkRK8";
            this.chkRK8.Size = new System.Drawing.Size(16, 16);
            this.chkRK8.TabIndex = 99;
            this.chkRK8.Text = "checkBox15";
            // 
            // entryMax8
            // 
            this.entryMax8.Location = new System.Drawing.Point(216, 200);
            this.entryMax8.Name = "entryMax8";
            this.entryMax8.Size = new System.Drawing.Size(56, 20);
            this.entryMax8.TabIndex = 98;
            // 
            // btnEntryEdit8
            // 
            this.btnEntryEdit8.Location = new System.Drawing.Point(192, 200);
            this.btnEntryEdit8.Name = "btnEntryEdit8";
            this.btnEntryEdit8.Size = new System.Drawing.Size(20, 20);
            this.btnEntryEdit8.TabIndex = 97;
            this.btnEntryEdit8.Text = "?";
            // 
            // entryText8
            // 
            this.entryText8.ContextMenu = this.deleteEntry;
            this.entryText8.Location = new System.Drawing.Point(8, 200);
            this.entryText8.Name = "entryText8";
            this.entryText8.Size = new System.Drawing.Size(184, 20);
            this.entryText8.TabIndex = 95;
            // 
            // chkClr8
            // 
            this.chkClr8.Location = new System.Drawing.Point(592, 204);
            this.chkClr8.Name = "chkClr8";
            this.chkClr8.Size = new System.Drawing.Size(16, 16);
            this.chkClr8.TabIndex = 96;
            // 
            // entryTo7
            // 
            this.entryTo7.Location = new System.Drawing.Point(400, 176);
            this.entryTo7.Name = "entryTo7";
            this.entryTo7.Size = new System.Drawing.Size(32, 20);
            this.entryTo7.TabIndex = 92;
            // 
            // entrySub7
            // 
            this.entrySub7.Location = new System.Drawing.Point(320, 176);
            this.entrySub7.Name = "entrySub7";
            this.entrySub7.Size = new System.Drawing.Size(32, 20);
            this.entrySub7.TabIndex = 91;
            // 
            // chkRK7
            // 
            this.chkRK7.Location = new System.Drawing.Point(568, 176);
            this.chkRK7.Name = "chkRK7";
            this.chkRK7.Size = new System.Drawing.Size(16, 24);
            this.chkRK7.TabIndex = 88;
            this.chkRK7.Text = "checkBox13";
            // 
            // entryMax7
            // 
            this.entryMax7.Location = new System.Drawing.Point(216, 176);
            this.entryMax7.Name = "entryMax7";
            this.entryMax7.Size = new System.Drawing.Size(56, 20);
            this.entryMax7.TabIndex = 87;
            // 
            // btnEntryEdit7
            // 
            this.btnEntryEdit7.Location = new System.Drawing.Point(192, 176);
            this.btnEntryEdit7.Name = "btnEntryEdit7";
            this.btnEntryEdit7.Size = new System.Drawing.Size(20, 20);
            this.btnEntryEdit7.TabIndex = 86;
            this.btnEntryEdit7.Text = "?";
            // 
            // entryText7
            // 
            this.entryText7.ContextMenu = this.deleteEntry;
            this.entryText7.Location = new System.Drawing.Point(8, 176);
            this.entryText7.Name = "entryText7";
            this.entryText7.Size = new System.Drawing.Size(184, 20);
            this.entryText7.TabIndex = 84;
            // 
            // chkClr7
            // 
            this.chkClr7.Location = new System.Drawing.Point(592, 176);
            this.chkClr7.Name = "chkClr7";
            this.chkClr7.Size = new System.Drawing.Size(16, 24);
            this.chkClr7.TabIndex = 85;
            // 
            // entryTo6
            // 
            this.entryTo6.Location = new System.Drawing.Point(400, 152);
            this.entryTo6.Name = "entryTo6";
            this.entryTo6.Size = new System.Drawing.Size(32, 20);
            this.entryTo6.TabIndex = 81;
            // 
            // entrySub6
            // 
            this.entrySub6.Location = new System.Drawing.Point(320, 152);
            this.entrySub6.Name = "entrySub6";
            this.entrySub6.Size = new System.Drawing.Size(32, 20);
            this.entrySub6.TabIndex = 80;
            // 
            // chkRK6
            // 
            this.chkRK6.Location = new System.Drawing.Point(568, 152);
            this.chkRK6.Name = "chkRK6";
            this.chkRK6.Size = new System.Drawing.Size(16, 24);
            this.chkRK6.TabIndex = 77;
            this.chkRK6.Text = "checkBox11";
            // 
            // entryMax6
            // 
            this.entryMax6.Location = new System.Drawing.Point(216, 152);
            this.entryMax6.Name = "entryMax6";
            this.entryMax6.Size = new System.Drawing.Size(56, 20);
            this.entryMax6.TabIndex = 76;
            // 
            // btnEntryEdit6
            // 
            this.btnEntryEdit6.Location = new System.Drawing.Point(192, 152);
            this.btnEntryEdit6.Name = "btnEntryEdit6";
            this.btnEntryEdit6.Size = new System.Drawing.Size(20, 20);
            this.btnEntryEdit6.TabIndex = 75;
            this.btnEntryEdit6.Text = "?";
            // 
            // entryText6
            // 
            this.entryText6.ContextMenu = this.deleteEntry;
            this.entryText6.Location = new System.Drawing.Point(8, 152);
            this.entryText6.Name = "entryText6";
            this.entryText6.Size = new System.Drawing.Size(184, 20);
            this.entryText6.TabIndex = 73;
            // 
            // chkClr6
            // 
            this.chkClr6.Location = new System.Drawing.Point(592, 152);
            this.chkClr6.Name = "chkClr6";
            this.chkClr6.Size = new System.Drawing.Size(16, 24);
            this.chkClr6.TabIndex = 74;
            // 
            // entryTo5
            // 
            this.entryTo5.Location = new System.Drawing.Point(400, 128);
            this.entryTo5.Name = "entryTo5";
            this.entryTo5.Size = new System.Drawing.Size(32, 20);
            this.entryTo5.TabIndex = 70;
            // 
            // entrySub5
            // 
            this.entrySub5.Location = new System.Drawing.Point(320, 128);
            this.entrySub5.Name = "entrySub5";
            this.entrySub5.Size = new System.Drawing.Size(32, 20);
            this.entrySub5.TabIndex = 69;
            // 
            // chkRK5
            // 
            this.chkRK5.Location = new System.Drawing.Point(568, 128);
            this.chkRK5.Name = "chkRK5";
            this.chkRK5.Size = new System.Drawing.Size(16, 24);
            this.chkRK5.TabIndex = 66;
            this.chkRK5.Text = "checkBox9";
            // 
            // entryMax5
            // 
            this.entryMax5.Location = new System.Drawing.Point(216, 128);
            this.entryMax5.Name = "entryMax5";
            this.entryMax5.Size = new System.Drawing.Size(56, 20);
            this.entryMax5.TabIndex = 65;
            // 
            // btnEntryEdit5
            // 
            this.btnEntryEdit5.Location = new System.Drawing.Point(192, 128);
            this.btnEntryEdit5.Name = "btnEntryEdit5";
            this.btnEntryEdit5.Size = new System.Drawing.Size(20, 20);
            this.btnEntryEdit5.TabIndex = 64;
            this.btnEntryEdit5.Text = "?";
            // 
            // entryText5
            // 
            this.entryText5.ContextMenu = this.deleteEntry;
            this.entryText5.Location = new System.Drawing.Point(8, 128);
            this.entryText5.Name = "entryText5";
            this.entryText5.Size = new System.Drawing.Size(184, 20);
            this.entryText5.TabIndex = 62;
            // 
            // chkClr5
            // 
            this.chkClr5.Location = new System.Drawing.Point(592, 128);
            this.chkClr5.Name = "chkClr5";
            this.chkClr5.Size = new System.Drawing.Size(16, 24);
            this.chkClr5.TabIndex = 63;
            // 
            // entryTo4
            // 
            this.entryTo4.Location = new System.Drawing.Point(400, 104);
            this.entryTo4.Name = "entryTo4";
            this.entryTo4.Size = new System.Drawing.Size(32, 20);
            this.entryTo4.TabIndex = 59;
            // 
            // entrySub4
            // 
            this.entrySub4.Location = new System.Drawing.Point(320, 104);
            this.entrySub4.Name = "entrySub4";
            this.entrySub4.Size = new System.Drawing.Size(32, 20);
            this.entrySub4.TabIndex = 58;
            // 
            // chkRK4
            // 
            this.chkRK4.Location = new System.Drawing.Point(568, 104);
            this.chkRK4.Name = "chkRK4";
            this.chkRK4.Size = new System.Drawing.Size(16, 24);
            this.chkRK4.TabIndex = 55;
            this.chkRK4.Text = "checkBox7";
            // 
            // entryMax4
            // 
            this.entryMax4.Location = new System.Drawing.Point(216, 104);
            this.entryMax4.Name = "entryMax4";
            this.entryMax4.Size = new System.Drawing.Size(56, 20);
            this.entryMax4.TabIndex = 54;
            // 
            // btnEntryEdit4
            // 
            this.btnEntryEdit4.Location = new System.Drawing.Point(192, 104);
            this.btnEntryEdit4.Name = "btnEntryEdit4";
            this.btnEntryEdit4.Size = new System.Drawing.Size(20, 20);
            this.btnEntryEdit4.TabIndex = 53;
            this.btnEntryEdit4.Text = "?";
            // 
            // entryText4
            // 
            this.entryText4.ContextMenu = this.deleteEntry;
            this.entryText4.Location = new System.Drawing.Point(8, 104);
            this.entryText4.Name = "entryText4";
            this.entryText4.Size = new System.Drawing.Size(184, 20);
            this.entryText4.TabIndex = 51;
            // 
            // chkClr4
            // 
            this.chkClr4.Location = new System.Drawing.Point(592, 104);
            this.chkClr4.Name = "chkClr4";
            this.chkClr4.Size = new System.Drawing.Size(16, 24);
            this.chkClr4.TabIndex = 52;
            // 
            // entryTo3
            // 
            this.entryTo3.Location = new System.Drawing.Point(400, 80);
            this.entryTo3.Name = "entryTo3";
            this.entryTo3.Size = new System.Drawing.Size(32, 20);
            this.entryTo3.TabIndex = 48;
            // 
            // entrySub3
            // 
            this.entrySub3.Location = new System.Drawing.Point(320, 80);
            this.entrySub3.Name = "entrySub3";
            this.entrySub3.Size = new System.Drawing.Size(32, 20);
            this.entrySub3.TabIndex = 47;
            // 
            // chkRK3
            // 
            this.chkRK3.Location = new System.Drawing.Point(568, 80);
            this.chkRK3.Name = "chkRK3";
            this.chkRK3.Size = new System.Drawing.Size(16, 24);
            this.chkRK3.TabIndex = 44;
            this.chkRK3.Text = "checkBox5";
            // 
            // entryMax3
            // 
            this.entryMax3.Location = new System.Drawing.Point(216, 80);
            this.entryMax3.Name = "entryMax3";
            this.entryMax3.Size = new System.Drawing.Size(56, 20);
            this.entryMax3.TabIndex = 43;
            // 
            // btnEntryEdit3
            // 
            this.btnEntryEdit3.Location = new System.Drawing.Point(192, 80);
            this.btnEntryEdit3.Name = "btnEntryEdit3";
            this.btnEntryEdit3.Size = new System.Drawing.Size(20, 20);
            this.btnEntryEdit3.TabIndex = 42;
            this.btnEntryEdit3.Text = "?";
            // 
            // entryText3
            // 
            this.entryText3.ContextMenu = this.deleteEntry;
            this.entryText3.Location = new System.Drawing.Point(8, 80);
            this.entryText3.Name = "entryText3";
            this.entryText3.Size = new System.Drawing.Size(184, 20);
            this.entryText3.TabIndex = 40;
            // 
            // chkClr3
            // 
            this.chkClr3.Location = new System.Drawing.Point(592, 80);
            this.chkClr3.Name = "chkClr3";
            this.chkClr3.Size = new System.Drawing.Size(16, 24);
            this.chkClr3.TabIndex = 41;
            // 
            // entryTo2
            // 
            this.entryTo2.Location = new System.Drawing.Point(400, 56);
            this.entryTo2.Name = "entryTo2";
            this.entryTo2.Size = new System.Drawing.Size(32, 20);
            this.entryTo2.TabIndex = 36;
            // 
            // entrySub2
            // 
            this.entrySub2.Location = new System.Drawing.Point(320, 56);
            this.entrySub2.Name = "entrySub2";
            this.entrySub2.Size = new System.Drawing.Size(32, 20);
            this.entrySub2.TabIndex = 35;
            // 
            // chkRK2
            // 
            this.chkRK2.Location = new System.Drawing.Point(568, 56);
            this.chkRK2.Name = "chkRK2";
            this.chkRK2.Size = new System.Drawing.Size(16, 24);
            this.chkRK2.TabIndex = 30;
            this.chkRK2.Text = "checkBox3";
            // 
            // entryMax2
            // 
            this.entryMax2.Location = new System.Drawing.Point(216, 56);
            this.entryMax2.Name = "entryMax2";
            this.entryMax2.Size = new System.Drawing.Size(56, 20);
            this.entryMax2.TabIndex = 27;
            // 
            // btnEntryEdit2
            // 
            this.btnEntryEdit2.Location = new System.Drawing.Point(192, 56);
            this.btnEntryEdit2.Name = "btnEntryEdit2";
            this.btnEntryEdit2.Size = new System.Drawing.Size(20, 20);
            this.btnEntryEdit2.TabIndex = 26;
            this.btnEntryEdit2.Text = "?";
            // 
            // entryText2
            // 
            this.entryText2.ContextMenu = this.deleteEntry;
            this.entryText2.Location = new System.Drawing.Point(8, 56);
            this.entryText2.Name = "entryText2";
            this.entryText2.Size = new System.Drawing.Size(184, 20);
            this.entryText2.TabIndex = 24;
            // 
            // chkClr2
            // 
            this.chkClr2.Location = new System.Drawing.Point(592, 56);
            this.chkClr2.Name = "chkClr2";
            this.chkClr2.Size = new System.Drawing.Size(16, 24);
            this.chkClr2.TabIndex = 25;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(592, 16);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(24, 16);
            this.label9.TabIndex = 23;
            this.label9.Text = "Clr";
            this.ttpSpawnInfo.SetToolTip(this.label9, "ClearOnAdvance flag. When checked all entries in that subgroup will be cleared on" +
        " sequential spawn advancement.");
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(568, 16);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(24, 16);
            this.label8.TabIndex = 22;
            this.label8.Text = "RK";
            this.ttpSpawnInfo.SetToolTip(this.label8, "RestrictKills flag.  When checked kills of that entry will only be counted if the" +
        "y come from the currently active sequential subgroup.");
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(512, 16);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(56, 16);
            this.label7.TabIndex = 21;
            this.label7.Text = "MaxD (m)";
            this.ttpSpawnInfo.SetToolTip(this.label7, "Individual MaxDelay for the entry.  Note that spawns cannot occur faster than the" +
        " main spawner min/maxdelay.");
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(464, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 16);
            this.label6.TabIndex = 20;
            this.label6.Text = "MinD (m)";
            this.ttpSpawnInfo.SetToolTip(this.label6, "Individual MinDelay for the entry. Note that spawns cannot occur faster than the " +
        "main spawner min/maxdelay.");
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(400, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(24, 16);
            this.label5.TabIndex = 17;
            this.label5.Text = "To";
            this.ttpSpawnInfo.SetToolTip(this.label5, "Subgroup that the sequential spawn index will be set to when the Reset time is re" +
        "ached without achieving the required number of Kills for the subgroup.");
            // 
            // entryTo1
            // 
            this.entryTo1.Location = new System.Drawing.Point(400, 32);
            this.entryTo1.Name = "entryTo1";
            this.entryTo1.Size = new System.Drawing.Size(32, 20);
            this.entryTo1.TabIndex = 16;
            // 
            // vScrollBar1
            // 
            this.vScrollBar1.LargeChange = 9;
            this.vScrollBar1.Location = new System.Drawing.Point(616, 16);
            this.vScrollBar1.Maximum = 8;
            this.vScrollBar1.Name = "vScrollBar1";
            this.vScrollBar1.Size = new System.Drawing.Size(16, 200);
            this.vScrollBar1.TabIndex = 15;
            // 
            // entrySub1
            // 
            this.entrySub1.Location = new System.Drawing.Point(320, 32);
            this.entrySub1.Name = "entrySub1";
            this.entrySub1.Size = new System.Drawing.Size(32, 20);
            this.entrySub1.TabIndex = 13;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(432, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 16);
            this.label4.TabIndex = 11;
            this.label4.Text = "Kills";
            this.ttpSpawnInfo.SetToolTip(this.label4, resources.GetString("label4.ToolTip"));
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(352, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 16);
            this.label3.TabIndex = 9;
            this.label3.Text = "Reset (m)";
            this.ttpSpawnInfo.SetToolTip(this.label3, "Maximum amount of time allowed to reach the number of kills required for this sub" +
        "group.  ");
            // 
            // chkRK1
            // 
            this.chkRK1.Location = new System.Drawing.Point(568, 32);
            this.chkRK1.Name = "chkRK1";
            this.chkRK1.Size = new System.Drawing.Size(16, 24);
            this.chkRK1.TabIndex = 8;
            this.chkRK1.Text = "checkBox2";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(320, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 16);
            this.label2.TabIndex = 6;
            this.label2.Text = "Sub";
            this.ttpSpawnInfo.SetToolTip(this.label2, "Subgroup assignment for the entry.");
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(216, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "Max";
            this.ttpSpawnInfo.SetToolTip(this.label1, "Maximum number of spawns for the entry.");
            // 
            // entryMax1
            // 
            this.entryMax1.Location = new System.Drawing.Point(216, 32);
            this.entryMax1.Name = "entryMax1";
            this.entryMax1.Size = new System.Drawing.Size(56, 20);
            this.entryMax1.TabIndex = 3;
            // 
            // btnEntryEdit1
            // 
            this.btnEntryEdit1.Location = new System.Drawing.Point(192, 32);
            this.btnEntryEdit1.Name = "btnEntryEdit1";
            this.btnEntryEdit1.Size = new System.Drawing.Size(20, 20);
            this.btnEntryEdit1.TabIndex = 2;
            this.btnEntryEdit1.Text = "?";
            // 
            // entryText1
            // 
            this.entryText1.ContextMenu = this.deleteEntry;
            this.entryText1.Location = new System.Drawing.Point(8, 32);
            this.entryText1.Name = "entryText1";
            this.entryText1.Size = new System.Drawing.Size(184, 20);
            this.entryText1.TabIndex = 0;
            // 
            // chkClr1
            // 
            this.chkClr1.Location = new System.Drawing.Point(592, 32);
            this.chkClr1.Name = "chkClr1";
            this.chkClr1.Size = new System.Drawing.Size(16, 24);
            this.chkClr1.TabIndex = 1;
            // 
            // groupTemplateList
            // 
            this.groupTemplateList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupTemplateList.Controls.Add(this.btnSaveTemplate);
            this.groupTemplateList.Controls.Add(this.btnMergeTemplate);
            this.groupTemplateList.Controls.Add(this.btnLoadTemplate);
            this.groupTemplateList.Controls.Add(this.tvwTemplates);
            this.groupTemplateList.Controls.Add(this.label29);
            this.groupTemplateList.Enabled = false;
            this.groupTemplateList.Location = new System.Drawing.Point(177, 472);
            this.groupTemplateList.Name = "groupTemplateList";
            this.groupTemplateList.Size = new System.Drawing.Size(236, 248);
            this.groupTemplateList.TabIndex = 11;
            this.groupTemplateList.TabStop = false;
            this.groupTemplateList.Text = "Spawn Templates";
            // 
            // btnSaveTemplate
            // 
            this.btnSaveTemplate.Location = new System.Drawing.Point(120, 16);
            this.btnSaveTemplate.Name = "btnSaveTemplate";
            this.btnSaveTemplate.Size = new System.Drawing.Size(56, 24);
            this.btnSaveTemplate.TabIndex = 7;
            this.btnSaveTemplate.Text = "Save";
            // 
            // btnMergeTemplate
            // 
            this.btnMergeTemplate.Location = new System.Drawing.Point(64, 16);
            this.btnMergeTemplate.Name = "btnMergeTemplate";
            this.btnMergeTemplate.Size = new System.Drawing.Size(56, 24);
            this.btnMergeTemplate.TabIndex = 6;
            this.btnMergeTemplate.Text = "Merge";
            // 
            // btnLoadTemplate
            // 
            this.btnLoadTemplate.Location = new System.Drawing.Point(8, 16);
            this.btnLoadTemplate.Name = "btnLoadTemplate";
            this.btnLoadTemplate.Size = new System.Drawing.Size(56, 24);
            this.btnLoadTemplate.TabIndex = 5;
            this.btnLoadTemplate.Text = "Load";
            // 
            // tvwTemplates
            // 
            this.tvwTemplates.AllowDrop = true;
            this.tvwTemplates.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tvwTemplates.Location = new System.Drawing.Point(8, 48);
            this.tvwTemplates.Name = "tvwTemplates";
            this.tvwTemplates.Size = new System.Drawing.Size(220, 168);
            this.tvwTemplates.Sorted = true;
            this.tvwTemplates.TabIndex = 3;
            this.ttpSpawnInfo.SetToolTip(this.tvwTemplates, "List of currently defined templates.");
            // 
            // label29
            // 
            this.label29.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label29.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label29.Location = new System.Drawing.Point(8, 216);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(220, 16);
            this.label29.TabIndex = 4;
            // 
            // SpawnEditor
            // 
            this.ClientSize = new System.Drawing.Size(1113, 733);
            this.Controls.Add(this.groupTemplateList);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnlControls);
            this.Controls.Add(this.stbMain);
            this.Menu = this.mainMenu1;
            this.Name = "SpawnEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Spawn Editor 2";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.SpawnEditor_Closing);
            this.Load += new System.EventHandler(this.SpawnEditor_Load);
            ((System.ComponentModel.ISupportInitialize)(this.axUOMap)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkZoom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spnMaxCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spnHomeRange)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spnMinDelay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spnTeam)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spnMaxDelay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spnSpawnRange)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spnProximityRange)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spnMinRefract)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spnTODStart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spnMaxRefract)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spnDespawn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spnTODEnd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spnDuration)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spnProximitySnd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spnKillReset)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spnTriggerProbability)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spnStackAmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spnContainerX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spnContainerY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spnContainerZ)).EndInit();
            this.pnlControls.ResumeLayout(false);
            this.tabControl3.ResumeLayout(false);
            this.tabMapSettings.ResumeLayout(false);
            this.grpMapControl.ResumeLayout(false);
            this.tabControl2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.grpSpawnList.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage5.ResumeLayout(false);
            this.grpSpawnTypes.ResumeLayout(false);
            this.grpSpawnEdit.ResumeLayout(false);
            this.grpSpawnEdit.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabBasic.ResumeLayout(false);
            this.tabAdvanced.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabSpawnTypes.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.grpSpawnEntries.ResumeLayout(false);
            this.grpSpawnEntries.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.entryPer8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.entryPer7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.entryPer6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.entryPer5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.entryPer4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.entryPer3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.entryPer2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.entryPer1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.entryMax8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.entryMax7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.entryMax6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.entryMax5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.entryMax4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.entryMax3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.entryMax2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.entryMax1)).EndInit();
            this.groupTemplateList.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		private void InitializeMapCenters()
		{
			MapLoc[1] = new MapLocation();
			MapLoc[1].X = 3072;
			MapLoc[1].Y = 2048;
			MapLoc[0] = new MapLocation();
			MapLoc[0].X = 3072;
			MapLoc[0].Y = 2048;
			MapLoc[2] = new MapLocation();
			MapLoc[2].X = 1150;
			MapLoc[2].Y = 800;
			MapLoc[3] = new MapLocation();
			MapLoc[3].X = 1280;
			MapLoc[3].Y = 1024;
			MapLoc[4] = new MapLocation();
			MapLoc[4].X = 700;
			MapLoc[4].Y = 700;
		}

		private void LargeWindow()
		{
			base.MinimumSize = new System.Drawing.Size(660, 520);
			base.MaximumSize = new System.Drawing.Size(1024, 768);
			base.Size = new System.Drawing.Size(1024, 768);
			panel1.Visible = true;
			tabControl1.Visible = true;
			panel3.Visible = true;
			axUOMap.Anchor = AnchorStyles.Top | AnchorStyles.Left;
			axUOMap.Size = new System.Drawing.Size(472, 464);
			tabControl2.Size = new System.Drawing.Size(176, 500);
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
			txtName.Text = string.Concat(_CfgDialog.CfgSpawnNameValue, tvwSpawnPoints.Nodes.Count);
			spnHomeRange.Value = _CfgDialog.CfgSpawnHomeRangeValue;
			spnMaxCount.Value = _CfgDialog.CfgSpawnMaxCountValue;
			spnMinDelay.Value = _CfgDialog.CfgSpawnMinDelayValue;
			spnMaxDelay.Value = _CfgDialog.CfgSpawnMaxDelayValue;
			spnTeam.Value = _CfgDialog.CfgSpawnTeamValue;
			chkGroup.Checked = _CfgDialog.CfgSpawnGroupValue;
			chkRunning.Checked = _CfgDialog.CfgSpawnRunningValue;
			chkHomeRangeIsRelative.Checked = _CfgDialog.CfgSpawnRelativeHomeValue;
			spnSpawnRange.Value = new decimal(-1);
			spnProximityRange.Value = new decimal(-1);
			spnDuration.Value = new decimal(0);
			spnDespawn.Value = new decimal(0);
			spnMinRefract.Value = new decimal(0);
			spnMaxRefract.Value = new decimal(0);
			spnTODStart.Value = new decimal(0);
			spnTODEnd.Value = new decimal(0);
			spnKillReset.Value = new decimal(1);
			spnProximitySnd.Value = new decimal(500);
			chkAllowGhost.Checked = false;
			chkSpawnOnTrigger.Checked = false;
			chkSequentialSpawn.Checked = false;
			chkSmartSpawning.Checked = false;
			chkInContainer.Checked = false;
			chkRealTOD.Checked = true;
			chkGameTOD.Checked = false;
			textSkillTrigger.Text = null;
			textSpeechTrigger.Text = null;
			textProximityMsg.Text = null;
			textMobTriggerName.Text = null;
			textMobTrigProp.Text = null;
			textPlayerTrigProp.Text = null;
			textTrigObjectProp.Text = null;
			textTriggerOnCarried.Text = null;
			textNoTriggerOnCarried.Text = null;
			spnTriggerProbability.Value = new decimal(1);
			spnStackAmount.Value = new decimal(1);
			spnContainerX.Value = new decimal(0);
			spnContainerY.Value = new decimal(0);
			spnContainerZ.Value = new decimal(0);
			chkExternalTriggering.Checked = false;
			textTrigObjectName.Text = null;
			textSetObjectName.Text = null;
			textRegionName.Text = null;
			textConfigFile.Text = null;
			textWayPoint.Text = null;
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
					string[] filePath = new string[] { "Failed to open file [", FilePath, "] for the following reason:", Environment.NewLine, ExceptionMessage(exception) };
					MessageBox.Show(this, string.Concat(filePath), "Load Failure", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
				LoadSpawnFile(fileStream, FilePath, ForceMap);
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
				tvwSpawnPoints.Sorted = false;
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.Load(stream);
				XmlElement item = xmlDocument["Spawns"];
				if (item != null)
				{
					RectangleConverter rectangleConverter = new RectangleConverter();
					int num = 0;
					XmlNodeList elementsByTagName = item.GetElementsByTagName("Points");
					progressBar1.Visible = true;
					lblTransferStatus.Visible = true;
					trkZoom.Visible = false;
					lblTrkMin.Visible = false;
					lblTrkMax.Visible = false;
					lblTransferStatus.Text = "Processing Spawners...";
					lblTransferStatus.Refresh();
					progressBar1.Maximum = elementsByTagName.Count;
					tvwSpawnPoints.BeginUpdate();
					foreach (XmlElement xmlElement in elementsByTagName)
					{
						num++;
						progressBar1.Value = num;
						bool flag = false;
						if (ForceMap != WorldMap.Internal)
						{
							flag = true;
						}
						SpawnPointNode spawnPointNode = new SpawnPointNode(new SpawnPoint(xmlElement, ForceMap, flag));
						tvwSpawnPoints.Nodes.Add(spawnPointNode);
					}
					tvwSpawnPoints.Sorted = true;
					tvwSpawnPoints.EndUpdate();
					lblTotalSpawn.Text = string.Concat("Total Spawns = ", tvwSpawnPoints.Nodes.Count);
					txtName.Text = string.Concat(_CfgDialog.CfgSpawnNameValue, tvwSpawnPoints.Nodes.Count);
					RefreshSpawnPoints();
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
				string[] filePath = new string[] { "Failed to load file [", FilePath, "] for the following reason:", Environment.NewLine, ExceptionMessage(exception) };
				MessageBox.Show(this, string.Concat(filePath), "Load Failure", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
			progressBar1.Visible = false;
			lblTransferStatus.Visible = false;
			trkZoom.Visible = true;
			lblTrkMin.Visible = true;
			lblTrkMax.Visible = true;
			progressBar1.Refresh();
			lblTransferStatus.Refresh();
			trkZoom.Refresh();
			lblTrkMin.Refresh();
			lblTrkMax.Refresh();
		}

		private void LoadSpawnPacks()
		{
			ReadSpawnPacks(SpawnPackFile);
		}

		private void LoadTypes()
		{
			clbRunUOTypes.BeginUpdate();
			clbRunUOTypes.Sorted = false;
			clbRunUOTypes.Items.Clear();
			Type[] typeArray = _RunUOScriptTypes;
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
						if (flag && (radShowAll.Checked || radShowItemsOnly.Checked) && type.BaseType != null && type.BaseType.FullName.StartsWith("Server.Item"))
						{
							clbRunUOTypes.Items.Add(type.Name);
						}
						if (flag && (radShowAll.Checked || radShowMobilesOnly.Checked) && type.BaseType != null && type.BaseType.FullName.StartsWith("Server.Mobile"))
						{
							clbRunUOTypes.Items.Add(type.Name);
						}
					}
				}
			}
			clbRunUOTypes.Sorted = true;
			clbRunUOTypes.EndUpdate();
			lblTotalTypesLoaded.Text = string.Concat("Types Loaded = ", clbRunUOTypes.Items.Count);
			SetSelectedSpawnTypes();
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
			if (mcnSpawnPack.SourceControl == clbSpawnPack)
			{
				foreach (MenuItem menuItem in mcnSpawnPack.MenuItems)
				{
					menuItem.Visible = false;
				}
				if (clbSpawnPack.SelectedItem is string)
				{
					mniDeleteType.Visible = true;
				}
				if (clbSpawnPack.Items.Count > 0)
				{
					mniDeleteAllTypes.Visible = true;
				}
			}
		}

		private void menuItem1_Click(object sender, EventArgs e)
		{
			if (SelectedSpawn == null || SelectedSpawn.SpawnObjects == null)
			{
				return;
			}
			string name = menuItem1.GetContextMenu().SourceControl.Name;
			int index = -1;
			if (name == "entryText1")
			{
				index = GetIndex(SelectedSpawn, 1);
			}
			else if (name == "entryText2")
			{
				index = GetIndex(SelectedSpawn, 2);
			}
			else if (name == "entryText3")
			{
				index = GetIndex(SelectedSpawn, 3);
			}
			else if (name == "entryText4")
			{
				index = GetIndex(SelectedSpawn, 4);
			}
			else if (name == "entryText5")
			{
				index = GetIndex(SelectedSpawn, 5);
			}
			else if (name == "entryText6")
			{
				index = GetIndex(SelectedSpawn, 6);
			}
			else if (name == "entryText7")
			{
				index = GetIndex(SelectedSpawn, 7);
			}
			else if (name == "entryText8")
			{
				index = GetIndex(SelectedSpawn, 8);
			}
			if (index >= 0 && index < SelectedSpawn.SpawnObjects.Count && MessageBox.Show(this, string.Concat("Are you sure you want to delete entry [", ((SpawnObject)SelectedSpawn.SpawnObjects[index]).TypeName, "] from the spawn?"), "Delete Spawn Object", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
			{
				SelectedSpawn.SpawnObjects.RemoveAt(index);
				DisplaySpawnEntries();
				UpdateSpawnNode();
			}
		}

		private void menuItem10_Click(object sender, EventArgs e)
		{
			try
			{
				importAllSpawnTypes.Title = "Import All Spawn Types";
				if (importAllSpawnTypes.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
				{
					ImportSpawnTypes(importAllSpawnTypes.FileName);
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
				exportAllSpawnTypes.Title = "Export All Spawn Types";
				if (exportAllSpawnTypes.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
				{
					ExportSpawnTypes(exportAllSpawnTypes.FileName);
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
				importMapFile.Title = "Import from .map file";
				if (importMapFile.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
				{
					ImportMap importMap = new ImportMap(this);
					importMap.DoImportMap(importMapFile.FileName, out num, out num1);
					lblTotalSpawn.Text = string.Concat("Total Spawns = ", tvwSpawnPoints.Nodes.Count);
					checkSpawnFilter.Checked = false;
					RefreshSpawnPoints();
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
				importMSFFile.Title = "Import from .msf file";
				if (importMSFFile.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
				{
					(new ImportMSF(this)).DoImportMSF(importMSFFile.FileName);
					lblTotalSpawn.Text = string.Concat("Total Spawns = ", tvwSpawnPoints.Nodes.Count);
					checkSpawnFilter.Checked = false;
					RefreshSpawnPoints();
				}
			}
			catch
			{
			}
		}

		private void menuItem15_Click(object sender, EventArgs e)
		{
			if (SelectedSpawn == null || SelectedSpawn.SpawnObjects == null)
			{
				return;
			}
			string name = menuItem15.GetContextMenu().SourceControl.Name;
			int index = -1;
			if (name == "entryText1")
			{
				index = GetIndex(SelectedSpawn, 1);
			}
			else if (name == "entryText2")
			{
				index = GetIndex(SelectedSpawn, 2);
			}
			else if (name == "entryText3")
			{
				index = GetIndex(SelectedSpawn, 3);
			}
			else if (name == "entryText4")
			{
				index = GetIndex(SelectedSpawn, 4);
			}
			else if (name == "entryText5")
			{
				index = GetIndex(SelectedSpawn, 5);
			}
			else if (name == "entryText6")
			{
				index = GetIndex(SelectedSpawn, 6);
			}
			else if (name == "entryText7")
			{
				index = GetIndex(SelectedSpawn, 7);
			}
			else if (name == "entryText8")
			{
				index = GetIndex(SelectedSpawn, 8);
			}
			if (index >= 0 && index < SelectedSpawn.SpawnObjects.Count)
			{
				clbSpawnPack.Items.Add(((SpawnObject)SelectedSpawn.SpawnObjects[index]).TypeName);
			}
		}

		private void menuItem17_Click(object sender, EventArgs e)
		{
			_TransferDialog.Show();
			_TransferDialog.BringToFront();
		}

		private void menuItem18_Click(object sender, EventArgs e)
		{
			try
			{
				OpenHelp();
			}
			catch
			{
				MessageBox.Show("Unable to open help file.");
			}
		}

		private void menuItem2_Click(object sender, EventArgs e)
		{
			if (SelectedSpawn == null || SelectedSpawn.SpawnObjects == null)
			{
				return;
			}
			if (MessageBox.Show(this, string.Concat("Are you sure you want to delete all entries from spawn [", SelectedSpawn.SpawnName, "]?"), "Delete All Spawn Objects", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
			{
				SelectedSpawn.SpawnObjects.Clear();
				DisplaySpawnEntries();
				UpdateSpawnNode();
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
				openSpawnPacks.Title = "Load SpawnPacks";
				if (openSpawnPacks.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
				{
					ReadSpawnPacks(openSpawnPacks.FileName);
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
				saveSpawnPacks.Title = "Save SpawnPacks";
				if (saveSpawnPacks.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
				{
					WriteSpawnPacks(saveSpawnPacks.FileName);
				}
			}
			catch
			{
			}
		}

		private void menuItem9_Click(object sender, EventArgs e)
		{
			_CfgDialog.ShowDialog();
		}

		private void mncLoad_Popup(object sender, EventArgs e)
		{
		}

		private void mncSpawns_Popup(object sender, EventArgs e)
		{
			if (mncSpawns.SourceControl == tvwSpawnPoints)
			{
				foreach (MenuItem menuItem in mncSpawns.MenuItems)
				{
					menuItem.Visible = false;
				}
				if (tvwSpawnPoints.SelectedNode is SpawnPointNode)
				{
					mniDeleteSpawn.Visible = true;
				}
				else if (tvwSpawnPoints.SelectedNode is SpawnObjectNode)
				{
					mniDeleteSpawn.Visible = true;
				}
				if (tvwSpawnPoints.Nodes.Count > 0)
				{
					mniDeleteAllSpawns.Visible = true;
				}
			}
		}

		private void mniAlwaysOnTop_Click(object sender, EventArgs e)
		{
			if (!mniAlwaysOnTop.Checked)
			{
				mniAlwaysOnTop.Checked = true;
				base.TopMost = true;
				return;
			}
			mniAlwaysOnTop.Checked = false;
			base.TopMost = false;
		}

		private void mniDeleteAllFiltered_Click(object sender, EventArgs e)
		{
			ArrayList arrayLists = new ArrayList();
			int num = 0;
			foreach (SpawnPointNode node in tvwSpawnPoints.Nodes)
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
				tvwSpawnPoints.BeginUpdate();
				tvwSpawnPoints.Sorted = false;
				foreach (SpawnPointNode arrayList in arrayLists)
				{
					tvwSpawnPoints.Nodes.Remove(arrayList);
				}
				tvwSpawnPoints.Sorted = true;
				tvwSpawnPoints.EndUpdate();
			}
			RefreshSpawnPoints();
		}

		private void mniDeleteAllSpawns_Click(object sender, EventArgs e)
		{
			TreeNode selectedNode = tvwSpawnPoints.SelectedNode;
			SpawnPointNode parent = selectedNode as SpawnPointNode;
			if (!(selectedNode is SpawnObjectNode))
			{
				DeleteAllSpawns();
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
			SetSelectedSpawnTypes();
			RefreshSpawnPoints();
		}

		private void mniDeleteAllTypes_Click(object sender, EventArgs e)
		{
			if (clbSpawnPack.SelectedItem is string && MessageBox.Show(this, string.Concat("Are you sure you want to delete all types in [", textSpawnPackName.Text, "]?"), "Delete All Types", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
			{
				clbSpawnPack.Items.Clear();
			}
		}

		private void mniDeleteAllUnfiltered_Click(object sender, EventArgs e)
		{
			ArrayList arrayLists = new ArrayList();
			int num = 0;
			foreach (SpawnPointNode node in tvwSpawnPoints.Nodes)
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
				tvwSpawnPoints.BeginUpdate();
				tvwSpawnPoints.Sorted = false;
				foreach (SpawnPointNode arrayList in arrayLists)
				{
					tvwSpawnPoints.Nodes.Remove(arrayList);
				}
				tvwSpawnPoints.Sorted = true;
				tvwSpawnPoints.EndUpdate();
			}
			RefreshSpawnPoints();
		}

		private void mniDeleteInSelectionWindow_Click(object sender, EventArgs e)
		{
			if (_SelectionWindow == null)
			{
				return;
			}
			ArrayList arrayLists = new ArrayList();
			int num = 0;
			foreach (SpawnPointNode node in tvwSpawnPoints.Nodes)
			{
				SpawnPoint spawn = node.Spawn;
				if (node.Filtered || spawn.CentreX < _SelectionWindow.X || spawn.CentreX > _SelectionWindow.X + _SelectionWindow.Width || spawn.CentreY < _SelectionWindow.Y || spawn.CentreY > _SelectionWindow.Y + _SelectionWindow.Height)
				{
					continue;
				}
				arrayLists.Add(node);
				num++;
				node.Highlighted = true;
			}
			RefreshSpawnPoints();
			if (MessageBox.Show(this, string.Format("Delete {0} spawners?", num), "Delete Spawners", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.OK)
			{
				tvwSpawnPoints.BeginUpdate();
				tvwSpawnPoints.Sorted = false;
				foreach (SpawnPointNode arrayList in arrayLists)
				{
					tvwSpawnPoints.Nodes.Remove(arrayList);
				}
				tvwSpawnPoints.Sorted = true;
				tvwSpawnPoints.EndUpdate();
			}
			foreach (SpawnPointNode spawnPointNode in tvwSpawnPoints.Nodes)
			{
				spawnPointNode.Highlighted = false;
			}
			RefreshSpawnPoints();
		}

		private void mniDeleteNotSelected_Click(object sender, EventArgs e)
		{
			if (_SelectionWindow == null)
			{
				return;
			}
			ArrayList arrayLists = new ArrayList();
			int num = 0;
			foreach (SpawnPointNode node in tvwSpawnPoints.Nodes)
			{
				SpawnPoint spawn = node.Spawn;
				if (node.Filtered || spawn.CentreX >= _SelectionWindow.X && spawn.CentreX <= _SelectionWindow.X + _SelectionWindow.Width && spawn.CentreY >= _SelectionWindow.Y && spawn.CentreY <= _SelectionWindow.Y + _SelectionWindow.Height)
				{
					continue;
				}
				arrayLists.Add(node);
				num++;
				node.Highlighted = true;
			}
			RefreshSpawnPoints();
			if (MessageBox.Show(this, string.Format("Delete {0} spawners?", num), "Delete Spawners", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.OK)
			{
				tvwSpawnPoints.BeginUpdate();
				tvwSpawnPoints.Sorted = false;
				foreach (SpawnPointNode arrayList in arrayLists)
				{
					tvwSpawnPoints.Nodes.Remove(arrayList);
				}
				tvwSpawnPoints.Sorted = true;
				tvwSpawnPoints.EndUpdate();
			}
			foreach (SpawnPointNode spawnPointNode in tvwSpawnPoints.Nodes)
			{
				spawnPointNode.Highlighted = false;
			}
			RefreshSpawnPoints();
		}

		private void mniDeletePack_Click(object sender, EventArgs e)
		{
			TreeNode selectedNode = tvwSpawnPacks.SelectedNode;
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
			TreeNode selectedNode = tvwSpawnPoints.SelectedNode;
			if (selectedNode is SpawnPointNode)
			{
				SpawnPointNode spawnPointNode = (SpawnPointNode)selectedNode;
				if (MessageBox.Show(this, string.Concat("Are you sure you want to delete spawn [", spawnPointNode.Spawn.SpawnName, "] from the list?"), "Delete Spawn", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
				{
					spawnPointNode.Remove();
					SelectedSpawn = null;
					LoadDefaultSpawnValues();
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
			SetSelectedSpawnTypes();
			RefreshSpawnPoints();
		}

		private void mniDeleteType_Click(object sender, EventArgs e)
		{
			if (clbSpawnPack.SelectedItem is string)
			{
				int selectedIndex = clbSpawnPack.SelectedIndex;
				if (selectedIndex >= 0 && MessageBox.Show(this, string.Concat("Are you sure you want to delete type [", clbSpawnPack.SelectedItem, "] from the list?"), "Delete Type", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
				{
					clbSpawnPack.Items.RemoveAt(selectedIndex);
				}
			}
		}

		private void mniDisplayFilterSettings_Click(object sender, EventArgs e)
		{
			_SpawnerFilters.Show();
			_SpawnerFilters.BringToFront();
		}

		private void mniForceLoad_Click(object sender, EventArgs e)
		{
			try
			{
				WorldMap selectedItem = (WorldMap)((int)((WorldMap)cbxMap.SelectedItem));
				ofdLoadFile.Title = string.Concat("Force Load Spawn File Into ", selectedItem.ToString());
				if (ofdLoadFile.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
				{
					Refresh();
					stbMain.Text = string.Format("Loading {0} into {1}...", ofdLoadFile.FileName, selectedItem.ToString());
					tvwSpawnPoints.Nodes.Clear();
					LoadSpawnFile(ofdLoadFile.FileName, selectedItem);
				}
			}
			finally
			{
				stbMain.Text = "Finished loading spawn file.";
			}
		}

		private void mniForceMerge_Click(object sender, EventArgs e)
		{
			try
			{
				WorldMap selectedItem = (WorldMap)((int)((WorldMap)cbxMap.SelectedItem));
				ofdLoadFile.Title = string.Concat("Merge Spawn File Into ", selectedItem.ToString());
				if (ofdLoadFile.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
				{
					Refresh();
					stbMain.Text = string.Format("Merging {0} into {1}...", ofdLoadFile.FileName, selectedItem.ToString());
					LoadSpawnFile(ofdLoadFile.FileName, selectedItem);
				}
			}
			finally
			{
				stbMain.Text = "Finished merging spawn file.";
			}
		}

		private void mniModifiedUnfiltered_Click(object sender, EventArgs e)
		{
			ArrayList arrayLists = new ArrayList();
			int num = 0;
			foreach (SpawnPointNode node in tvwSpawnPoints.Nodes)
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
			RefreshSpawnPoints();
			if (MessageBox.Show(this, string.Format("Modify {0} spawners?", num), "Modify Spawners", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.OK)
			{
				tvwSpawnPoints.BeginUpdate();
				tvwSpawnPoints.Sorted = false;
				foreach (SpawnPointNode arrayList in arrayLists)
				{
					ApplyModifications(arrayList.Spawn);
				}
				tvwSpawnPoints.Sorted = true;
				tvwSpawnPoints.EndUpdate();
			}
			foreach (SpawnPointNode spawnPointNode in tvwSpawnPoints.Nodes)
			{
				spawnPointNode.Highlighted = false;
			}
			RefreshSpawnPoints();
		}

		private void mniModifyInSelectionWindow_Click(object sender, EventArgs e)
		{
			if (_SelectionWindow == null)
			{
				return;
			}
			ArrayList arrayLists = new ArrayList();
			int num = 0;
			foreach (SpawnPointNode node in tvwSpawnPoints.Nodes)
			{
				SpawnPoint spawn = node.Spawn;
				if (node.Filtered || spawn.CentreX < _SelectionWindow.X || spawn.CentreX > _SelectionWindow.X + _SelectionWindow.Width || spawn.CentreY < _SelectionWindow.Y || spawn.CentreY > _SelectionWindow.Y + _SelectionWindow.Height)
				{
					continue;
				}
				arrayLists.Add(node);
				num++;
				node.Highlighted = true;
			}
			RefreshSpawnPoints();
			if (MessageBox.Show(this, string.Format("Modify {0} spawners?", num), "Modify Spawners", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.OK)
			{
				tvwSpawnPoints.BeginUpdate();
				tvwSpawnPoints.Sorted = false;
				foreach (SpawnPointNode arrayList in arrayLists)
				{
					ApplyModifications(arrayList.Spawn);
				}
				tvwSpawnPoints.Sorted = true;
				tvwSpawnPoints.EndUpdate();
			}
			foreach (SpawnPointNode spawnPointNode in tvwSpawnPoints.Nodes)
			{
				spawnPointNode.Highlighted = false;
			}
			RefreshSpawnPoints();
		}

		private void mniSetSpawnAmount_Click(object sender, EventArgs e)
		{
			SpawnObjectNode selectedNode = tvwSpawnPoints.SelectedNode as SpawnObjectNode;
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
			DeleteAllSpawns();
		}

		private void mniUnloadSingleSpawner_Click_1(object sender, EventArgs e)
		{
			if (tvwSpawnPoints.Nodes == null || tvwSpawnPoints.Nodes.Count <= 0)
			{
				return;
			}
			DoUnloadSpawners(SelectedSpawn);
		}

		private void mniUnloadSpawners_Click(object sender, EventArgs e)
		{
			if (tvwSpawnPoints.Nodes == null || tvwSpawnPoints.Nodes.Count <= 0)
			{
				return;
			}
			DoUnloadSpawners(null);
		}

		private void numericUpDown1_ValueChanged(object sender, EventArgs e)
		{
			DisplaySpawnEntries();
		}

		private void numericUpDown10_ValueChanged(object sender, EventArgs e)
		{
		}

		private void numericUpDown6_ValueChanged(object sender, EventArgs e)
		{
		}

		protected override void OnClosing(CancelEventArgs e)
		{
			Tracking = false;
			base.OnClosing(e);
		}

		private void OpenHelp()
		{
			Process.Start(string.Concat("file://", Path.Combine(StartingDirectory, HelpFile)));
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
							tvwSpawnPacks.Nodes.Add(new SpawnPackNode(value, arrayLists));
						}
					}
				}
				catch (Exception exception1)
				{
					Exception exception = exception1;
					string[] strArrays = new string[] { "Failed to read SpawnPack file [", filePath, "] for the following reason:", Environment.NewLine, ExceptionMessage(exception) };
					MessageBox.Show(this, string.Concat(strArrays), "Read Failure", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
			}
		}

		private void RefreshRegionView()
		{
			foreach (RegionFacetNode node in treeRegionView.Nodes)
			{
				foreach (RegionNode regionNode in node.Nodes)
				{
					SpawnEditor2.Region region = regionNode.Region;
					if (!regionNode.Checked || !(region != null) || (int)region.Map != (int)((WorldMap)cbxMap.SelectedItem))
					{
						continue;
					}
					foreach (Rectangle coord in region.Coords)
					{
						axUOMap.AddDrawRect((short)coord.X, (short)coord.Y, (short)coord.Width, (short)coord.Height, 1, 32512);
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
			axUOMap.RemoveDrawRects();
			axUOMap.RemoveDrawObjects();
			RefreshRegionView();
			DisplayMyLocation();
			if (MobLocArray != null && _TransferDialog.chkShowCreatures.Checked)
			{
				short value = (short)(5 + (short)trkZoom.Value);
				for (int i = 0; i < (int)MobLocArray.Length; i++)
				{
					short x1 = (short)MobLocArray[i].X;
					short y1 = (short)MobLocArray[i].Y;
					if ((int)((WorldMap)cbxMap.SelectedItem) == MobLocArray[i].Map)
					{
						axUOMap.AddDrawObject(x1, y1, 1, value, 16776960);
					}
				}
			}
			if (PlayerLocArray != null && _TransferDialog.chkShowPlayers.Checked)
			{
				short value1 = (short)(5 + (short)trkZoom.Value);
				for (int j = 0; j < (int)PlayerLocArray.Length; j++)
				{
					short num1 = (short)PlayerLocArray[j].X;
					short y2 = (short)PlayerLocArray[j].Y;
					if ((int)((WorldMap)cbxMap.SelectedItem) == PlayerLocArray[j].Map)
					{
						axUOMap.AddDrawObject(num1, y2, 2, value1, 65535);
					}
				}
			}
			if (ItemLocArray != null && _TransferDialog.chkShowItems.Checked)
			{
				short value2 = (short)(5 + (short)trkZoom.Value);
				for (int k = 0; k < (int)ItemLocArray.Length; k++)
				{
					short x2 = (short)ItemLocArray[k].X;
					short num2 = (short)ItemLocArray[k].Y;
					if ((int)((WorldMap)cbxMap.SelectedItem) == ItemLocArray[k].Map)
					{
						axUOMap.AddDrawObject(x2, num2, 1, value2, 65280);
					}
				}
			}
			bool flag = false;
			int num3 = 0;
			foreach (SpawnPointNode node in tvwSpawnPoints.Nodes)
			{
				if (node.Spawn.IsSelected || node.Highlighted)
				{
					if (!node.Filtered)
					{
						node.ForeColor = tvwSpawnPoints.ForeColor;
						num3++;
					}
					else
					{
						node.ForeColor = Color.LightGray;
					}
					if ((int)node.Spawn.Map == (int)((WorldMap)cbxMap.SelectedItem) && chkShowSpawns.Checked)
					{
						if (chkShade.Checked && cbxShade.SelectedIndex == 0)
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
							axUOMap.AddDrawRect(centreX, centreY, spawnHomeRange, height, 1, SpawnEditor.ComputeDensityColor(node.Spawn));
						}
						else if (chkShade.Checked && cbxShade.SelectedIndex == 1)
						{
							bounds = node.Spawn.Bounds;
							short x3 = (short)bounds.X;
							bounds = node.Spawn.Bounds;
							short y3 = (short)bounds.Y;
							bounds = node.Spawn.Bounds;
							short width1 = (short)bounds.Width;
							bounds = node.Spawn.Bounds;
							short height1 = (short)bounds.Height;
							axUOMap.AddDrawRect(x3, y3, width1, height1, 1, SpawnEditor.ComputeSpeedColor(node.Spawn));
						}
						SpawnPoint spawn = node.Spawn;
						AxUOMap axUOMap0 = axUOMap;
						bounds = node.Spawn.Bounds;
						short x4 = (short)bounds.X;
						bounds = node.Spawn.Bounds;
						short y4 = (short)bounds.Y;
						bounds = node.Spawn.Bounds;
						short width2 = (short)bounds.Width;
						bounds = node.Spawn.Bounds;
						spawn.Index = axUOMap0.AddDrawRect(x4, y4, width2, (short)bounds.Height, 2, 16776960);
						short value3 = (short)(7 + (short)trkZoom.Value);
						if (!node.Spawn.SpawnInContainer)
						{
							axUOMap0.AddDrawObject(node.Spawn.CentreX, node.Spawn.CentreY, 3, value3, 16711680);
						}
						else
						{
							axUOMap0.AddDrawObject(node.Spawn.CentreX, node.Spawn.CentreY, 6, value3, 16711935);
						}
					}
					flag = true;
					if (tvwSpawnPoints.SelectedNode != null && (tvwSpawnPoints.SelectedNode.Parent == null || tvwSpawnPoints.SelectedNode.Parent != node))
					{
						tvwSpawnPoints.SelectedNode = node;
						node.BackColor = Color.Yellow;
						node.EnsureVisible();
					}
					SelectedSpawn = node.Spawn;
				}
				else
				{
					node.BackColor = tvwSpawnPoints.BackColor;
					if (!node.Filtered)
					{
						node.ForeColor = tvwSpawnPoints.ForeColor;
						num3++;
						if ((int)node.Spawn.Map != (int)((WorldMap)cbxMap.SelectedItem) || !chkShowSpawns.Checked)
						{
							continue;
						}
						if (chkShade.Checked && cbxShade.SelectedIndex == 0)
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
							axUOMap.AddDrawRect(x, y, width, num, 1, SpawnEditor.ComputeDensityColor(node.Spawn));
						}
						else if (chkShade.Checked && cbxShade.SelectedIndex == 1)
						{
							bounds = node.Spawn.Bounds;
							short num4 = (short)bounds.X;
							bounds = node.Spawn.Bounds;
							short y5 = (short)bounds.Y;
							bounds = node.Spawn.Bounds;
							short width3 = (short)bounds.Width;
							bounds = node.Spawn.Bounds;
							short height2 = (short)bounds.Height;
							axUOMap.AddDrawRect(num4, y5, width3, height2, 1, SpawnEditor.ComputeSpeedColor(node.Spawn));
						}
						SpawnPoint spawnPoint = node.Spawn;
						AxUOMap axUOMap1 = axUOMap;
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
			lblTotalSpawn.Text = string.Concat("Total Spawns = ", num3);
			DisplaySpawnEntries();
			if (!flag)
			{
				btnUpdateSpawn.Enabled = false;
				btnUpdateFromSpawnPack.Enabled = false;
				btnDeleteSpawn.Enabled = false;
				btnSendSingleSpawner.Enabled = false;
				btnMove.Enabled = false;
			}
			else
			{
				btnUpdateSpawn.Enabled = true;
				btnUpdateFromSpawnPack.Enabled = true;
				btnDeleteSpawn.Enabled = true;
				btnSendSingleSpawner.Enabled = true;
				btnMove.Enabled = true;
			}
			if (_SelectionWindow != null)
			{
				_SelectionWindow.Index = axUOMap.AddDrawRect(_SelectionWindow.X, _SelectionWindow.Y, _SelectionWindow.Width, _SelectionWindow.Height, 2, 16777215);
			}
			axUOMap.Refresh();
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
				string[] filePath = new string[] { "Failed to create file [", FilePath, "] for the following reason:", Environment.NewLine, ExceptionMessage(exception) };
				MessageBox.Show(this, string.Concat(filePath), "Save Failure", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return;
			}
			if (fileStream != null)
			{
				SaveSpawnFile(fileStream, FilePath, null);
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
				foreach (SpawnPointNode node in tvwSpawnPoints.Nodes)
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
				string[] filePath = new string[] { "Failed to save file [", FilePath, "] for the following reason:", Environment.NewLine, ExceptionMessage(exception) };
				MessageBox.Show(this, string.Concat(filePath), "Save Failure", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}

		public void SendAuthCommand(Guid id)
		{
			int num = SpawnEditor.FindWindow(_CfgDialog.CfgUoClientWindowValue, null);
			if (num <= 0)
			{
				MessageBox.Show(string.Format("{0} could not be found. Make sure the client is started and that the 'Client Window' option in Setup is correct.", string.Concat(_CfgDialog.CfgUoClientWindowValue, " Not Found"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation));
				return;
			}
			string str = string.Format("{0}XTS auth {1}", _CfgDialog.CfgRunUoCmdPrefix, id.ToString());
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
			if (chkSnapRegion.Checked)
			{
				int x = Spawn.Bounds.X;
				Rectangle bounds = Spawn.Bounds;
				centreX = (short)(x + bounds.Width / 2);
				int y = Spawn.Bounds.Y;
				bounds = Spawn.Bounds;
				centreY = (short)(y + bounds.Height / 2);
				centreZ = -32768;
			}
			if (chkSyncUO.Checked)
			{
				SendGoCommand(centreX, centreY, centreZ, Spawn.Map);
			}
			AssignCenter(centreX, centreY, (short)Spawn.Map);
		}

		public void SendGoCommand(short X, short Y, short Z, WorldMap Map)
		{
			object[] cfgRunUoCmdPrefix;
			int num = SpawnEditor.FindWindow(_CfgDialog.CfgUoClientWindowValue, null);
			if (num <= 0)
			{
				MessageBox.Show(string.Format("{0} could not be found. Make sure the client is started and that the 'Client Window' option in Setup is correct.", string.Concat(_CfgDialog.CfgUoClientWindowValue, " Not Found"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation));
				chkSyncUO.Checked = false;
				return;
			}
			string empty = string.Empty;
			if (Z != -32768)
			{
				cfgRunUoCmdPrefix = new object[] { _CfgDialog.CfgRunUoCmdPrefix, Map, X, Y, Z };
				empty = string.Format("{0}XmlGo {1} {2} {3} {4}", cfgRunUoCmdPrefix);
			}
			else
			{
				cfgRunUoCmdPrefix = new object[] { _CfgDialog.CfgRunUoCmdPrefix, Map, X, Y };
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
			MyLocation.X = X;
			MyLocation.Y = Y;
			MyLocation.Z = Z;
			MyLocation.Facet = (int)Map;
		}

		[DllImport("User32.dll", CharSet=CharSet.None, EntryPoint="SendMessageA", ExactSpelling=false)]
		public static extern int SendMessage(int _WindowHandler, int _WM_USER, int _data, int _id);

		[DllImport("User32.dll", CharSet=CharSet.None, ExactSpelling=false)]
		public static extern bool SetForegroundWindow(int hWnd);

		private void SetSelectedSpawnTypes()
		{
			if (tvwSpawnPoints.SelectedNode == null)
			{
				clbRunUOTypes.ClearSelected();
				for (int i = 0; i < clbRunUOTypes.Items.Count; i++)
				{
					clbRunUOTypes.SetItemChecked(i, false);
				}
				return;
			}
			SelectedSpawnNode = tvwSpawnPoints.SelectedNode as SpawnPointNode;
			SpawnObjectNode selectedNode = tvwSpawnPoints.SelectedNode as SpawnObjectNode;
			if (selectedNode != null)
			{
				SelectedSpawnNode = (SpawnPointNode)selectedNode.Parent;
			}
			clbRunUOTypes.ClearSelected();
			for (int j = 0; j < clbRunUOTypes.Items.Count; j++)
			{
				bool flag = false;
				foreach (SpawnObject spawnObject in SelectedSpawnNode.Spawn.SpawnObjects)
				{
					if (spawnObject.TypeName.ToUpper() != clbRunUOTypes.Items[j].ToString().ToUpper())
					{
						continue;
					}
					flag = true;
					break;
				}
				clbRunUOTypes.SetItemChecked(j, flag);
			}
		}

		private void SetSpawn(SpawnPointNode SpawnNode, bool IsUpdate)
		{
			UpdateSpawnDetails(SpawnNode.Spawn);
			foreach (string checkedItem in clbRunUOTypes.CheckedItems)
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
			UpdateSpawnerMaxCount();
			SpawnNode.UpdateNode();
		}

		private void SetSpawnFromSpawnPack(SpawnPointNode SpawnNode, bool IsUpdate)
		{
			UpdateSpawnDetails(SpawnNode.Spawn);
			foreach (string checkedItem in clbSpawnPack.CheckedItems)
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
			UpdateSpawnerMaxCount();
			SpawnNode.UpdateNode();
		}

		private void SmallWindow()
		{
			base.MinimumSize = new System.Drawing.Size(0, 0);
			base.MaximumSize = new System.Drawing.Size(0, 0);
			if (savewindowsize.IsEmpty || savepanelsize.IsEmpty)
			{
				base.Size = new System.Drawing.Size(660, 520);
				panel1.Size = new System.Drawing.Size(480, 517);
			}
			else
			{
				base.Size = savewindowsize;
				panel1.Size = savepanelsize;
			}
			panel1.Visible = true;
			tabControl1.Visible = false;
			panel3.Visible = false;
			axUOMap.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			if (!savemapsize.IsEmpty && !savelistsize.IsEmpty)
			{
				axUOMap.Size = savemapsize;
				tabControl2.Size = savelistsize;
				return;
			}
			axUOMap.Size = new System.Drawing.Size(472, 464);
			tabControl2.Size = new System.Drawing.Size(176, 264);
		}

		private void SpawnEditor_Closing(object sender, CancelEventArgs e)
		{
			if (MessageBox.Show(this, "Are you sure you want to quit?    ", "Spawn Editor 2", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
			{
				e.Cancel = true;
				return;
			}
			Environment.CurrentDirectory = StartingDirectory;
			WriteSpawnPacks(SpawnPackFile);
			_CfgDialog.SaveWindowConfiguration();
			_CfgDialog.SaveTransferServerConfiguration();
		}

		private void SpawnEditor_Load(object sender, EventArgs e)
		{
			SpawnEditor.Debug("Loading");
			StartingDirectory = Directory.GetCurrentDirectory();
			if (!_CfgDialog.IsValidConfiguration)
			{
				SpawnEditor.Debug("OpeningConfiguration");
				_CfgDialog.ShowDialog();
				if (!_CfgDialog.IsValidConfiguration)
				{
					MessageBox.Show(this, string.Concat("Spawn Editor has not been configured properly.", Environment.NewLine, "Exiting..."), "Configuration Failure", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					Application.Exit();
				}
			}
			try
			{
				cbxShade.SelectedIndex = 0;
				foreach (object value in Enum.GetValues(typeof(WorldMap)))
				{
					WorldMap worldMap = (WorldMap)((int)((WorldMap)value));
					if (worldMap == WorldMap.Internal)
					{
						continue;
					}
					cbxMap.Items.Add(worldMap);
				}
				axUOMap.SetClientPath(string.Concat(Path.GetDirectoryName(_CfgDialog.CfgUoClientPathValue), "\\"));
				axUOMap.ZoomLevel = (_CfgDialog.CfgZoomLevelValue);
				trkZoom.Value = axUOMap.ZoomLevel;
				Assembly assembly = Assembly.LoadFrom(_CfgDialog.CfgRunUoPathValue);
				if (assembly != null)
				{
					SpawnEditor.AssemblyList.Add(assembly);
				}
				Assembly assembly1 = null;
				ArrayList arrayLists = new ArrayList();
				string directoryName = Path.GetDirectoryName(_CfgDialog.CfgRunUoPathValue);
				LoadCustomAssemblies(directoryName);
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
				_RunUOScriptTypes = (Type[])arrayLists.ToArray(typeof(Type));
				LoadTypes();
				LoadSpawnPacks();
				lblTotalSpawn.Text = string.Concat("Total Spawns = ", tvwSpawnPoints.Nodes.Count);
				LoadDefaultSpawnValues();
				if (Directory.Exists(Path.GetDirectoryName(_CfgDialog.CfgRunUoPathValue)))
				{
					ofdLoadFile.InitialDirectory = _CfgDialog.CfgRunUoPathValue;
					sfdSaveFile.InitialDirectory = _CfgDialog.CfgRunUoPathValue;
					SpawnEditor2.Region.Editor = this;
					SpawnEditor2.Region.Load(Path.GetDirectoryName(_CfgDialog.CfgRunUoPathValue));
					FillRegionTree();
					treeRegionView.Refresh();
					FillGoTree(Path.GetDirectoryName(_CfgDialog.CfgRunUoPathValue));
					treeGoView.Refresh();
				}
				cbxMap.SelectedIndex = (int)_CfgDialog.CfgStartingMapValue;
				chkDrawStatics.Checked = _CfgDialog.CfgStartingStaticsValue;
				mniAlwaysOnTop.Checked = _CfgDialog.CfgStartingOnTopValue;
				base.TopMost = mniAlwaysOnTop.Checked;
				if (_CfgDialog.CfgStartingXValue >= 0 && _CfgDialog.CfgStartingYValue >= 0)
				{
					base.Location = new Point(_CfgDialog.CfgStartingXValue, _CfgDialog.CfgStartingYValue);
				}
				if (_CfgDialog.CfgStartingWidthValue >= 0 && _CfgDialog.CfgStartingHeightValue >= 0)
				{
					base.Size = new System.Drawing.Size(_CfgDialog.CfgStartingWidthValue, _CfgDialog.CfgStartingHeightValue);
				}
				chkDetails.Checked = _CfgDialog.CfgStartingDetailsValue;
				_CfgDialog.ConfigureTransferServer();
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
			if (SelectedSpawn != null && (int)spnSpawnRange.Value >= 0)
			{
				int value = (int)spnSpawnRange.Value * 2;
				int centreX = SelectedSpawn.CentreX - value / 2;
				int centreY = SelectedSpawn.CentreY - value / 2;
				SelectedSpawn.Bounds = new Rectangle(centreX, centreY, value, value);
				RefreshSpawnPoints();
			}
		}

		private void tabControl1_Leave(object sender, EventArgs e)
		{
			UpdateSpawnDetails(SelectedSpawn);
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
			TreeNode nodeAt = treeGoView.GetNodeAt(e.X, e.Y);
			if (nodeAt is LocationSubNode)
			{
				foreach (TreeNode node in treeGoView.Nodes)
				{
					ClearTreeColor(node, treeGoView.BackColor);
				}
				LocationSubNode yellow = nodeAt as LocationSubNode;
				if (yellow.Node is ChildNode)
				{
					MapLocation location = ((ChildNode)yellow.Node).Location;
					WorldMap map = yellow.Map;
					yellow.BackColor = Color.Yellow;
					cbxMap.SelectedItem = map;
					AssignCenter((short)location.X, (short)location.Y, (short)map);
					if (chkSyncUO.Checked)
					{
						SendGoCommand((short)location.X, (short)location.Y, (short)location.Z, map);
					}
				}
			}
		}

		private void treeRegionView_MouseUp(object sender, MouseEventArgs e)
		{
			TreeNode nodeAt = treeRegionView.GetNodeAt(e.X, e.Y);
			if (nodeAt is RegionNode && nodeAt.Checked)
			{
				ClearTreeFacetSelection();
				nodeAt.Parent.Checked = true;
				SpawnEditor2.Region region = (nodeAt as RegionNode).Region;
				if (region != null)
				{
					MapLocation goLocation = region.GoLocation;
					cbxMap.SelectedItem = region.Map;
					AssignCenter((short)goLocation.X, (short)goLocation.Y, (short)region.Map);
					if (chkSyncUO.Checked)
					{
						SendGoCommand((short)goLocation.X, (short)goLocation.Y, (short)goLocation.Z, (WorldMap)goLocation.Facet);
					}
				}
			}
			else if (nodeAt is RegionFacetNode)
			{
				ClearTreeFacetSelection();
				nodeAt.Checked = true;
				cbxMap.SelectedItem = ((RegionFacetNode)nodeAt).Facet;
			}
			RefreshSpawnPoints();
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
			axUOMap.ZoomLevel = ((short)trkZoom.Value);
			stbMain.Text = string.Concat(DefaultZoomLevelText, axUOMap.ZoomLevel);
			RefreshSpawnPoints();
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
				tvwSpawnPacks.SelectedNode = parent;
				clbSpawnPack.Items.Clear();
				clbSpawnPack.Sorted = false;
				foreach (SpawnPackSubNode spawnPackSubNode in parent.Nodes)
				{
					clbSpawnPack.Items.Add(spawnPackSubNode.Text);
				}
				textSpawnPackName.Text = parent.PackName;
				clbSpawnPack.Sorted = true;
			}
		}

		private void tvwSpawnPacks_MouseUp(object sender, MouseEventArgs e)
		{
			TreeNode nodeAt = tvwSpawnPacks.GetNodeAt(e.X, e.Y);
			if (nodeAt != null)
			{
				SpawnPackNode parent = nodeAt as SpawnPackNode;
				if (nodeAt is SpawnPackSubNode)
				{
					parent = (SpawnPackNode)nodeAt.Parent;
				}
				if (parent != null && e.Button == System.Windows.Forms.MouseButtons.Right)
				{
					tvwSpawnPacks.SelectedNode = parent;
					mcnSpawnPacks.Show(tvwSpawnPacks, new Point(e.X, e.Y));
				}
			}
		}

		private void tvwSpawnPoints_MouseUp(object sender, MouseEventArgs e)
		{
			TreeNode nodeAt = tvwSpawnPoints.GetNodeAt(e.X, e.Y);
			if (nodeAt != null)
			{
				tvwSpawnPoints.Refresh();
				SelectedSpawnNode = nodeAt as SpawnPointNode;
				SpawnObjectNode spawnObjectNode = nodeAt as SpawnObjectNode;
				if (spawnObjectNode != null)
				{
					SelectedSpawnNode = (SpawnPointNode)spawnObjectNode.Parent;
				}
				if (SelectedSpawnNode != null)
				{
					SelectedSpawn = SelectedSpawnNode.Spawn;
					foreach (SpawnPointNode node in tvwSpawnPoints.Nodes)
					{
						node.Spawn.IsSelected = false;
					}
					SelectedSpawn.IsSelected = true;
					SendGoCommand(SelectedSpawn);
					if ((int)SelectedSpawn.Map != (int)((WorldMap)cbxMap.SelectedItem))
					{
						cbxMap.SelectedItem = SelectedSpawn.Map;
					}
					DisplaySpawnDetails(SelectedSpawn);
					DisplaySpawnEntries();
					RefreshSpawnPoints();
				}
				if (e.Button == System.Windows.Forms.MouseButtons.Right)
				{
					tvwSpawnPoints.SelectedNode = nodeAt;
					mncSpawns.Show(tvwSpawnPoints, new Point(e.X, e.Y));
				}
				SetSelectedSpawnTypes();
			}
		}

		private void txtName_KeyUp(object sender, KeyEventArgs e)
		{
			namechanged = true;
			changednamestring = txtName.Text;
		}

		private void txtName_Leave(object sender, EventArgs e)
		{
			if (namechanged && SelectedSpawn != null)
			{
				SelectedSpawn.SpawnName = changednamestring;
				UpdateSpawnNode();
			}
			namechanged = false;
		}

		private void txtName_MouseLeave(object sender, EventArgs e)
		{
			if (namechanged && SelectedSpawn != null)
			{
				SelectedSpawn.SpawnName = changednamestring;
				UpdateSpawnNode();
			}
			namechanged = false;
		}

		private void TypeSelectionChanged(object sender, EventArgs e)
		{
			if (sender is RadioButton && ((RadioButton)sender).Checked)
			{
				LoadTypes();
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
				MessageBox.Show(string.Format("{0} could not be found. Make sure the client is started and that the 'Client Window' option in Setup is correct.", string.Concat(_CfgDialog.CfgUoClientWindowValue, " Not Found"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation));
			}
			else
			{
				cbxMap.SelectedIndex = num3;
				AssignCenter((short)num, (short)num1, (short)num3);
			}
			MyLocation.X = num;
			MyLocation.Y = num1;
			MyLocation.Z = num2;
			MyLocation.Facet = num3;
		}

		private void UpdateSpawnDetails(SpawnPoint Spawn)
		{
			if (Spawn == null)
			{
				return;
			}
			txtName.Text = txtName.Text.Trim();
			if (txtName.Text.Length == 0)
			{
				MessageBox.Show(this, "You must specify a name for the spawner!", "Spawn Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return;
			}
			Spawn.SpawnName = txtName.Text;
			Spawn.SpawnHomeRangeIsRelative = chkHomeRangeIsRelative.Checked;
			Spawn.SpawnHomeRange = (int)spnHomeRange.Value;
			Spawn.SpawnIsGroup = chkGroup.Checked;
			Spawn.SpawnIsRunning = chkRunning.Checked;
			Spawn.SpawnMaxCount = (int)spnMaxCount.Value;
			Spawn.SpawnMaxDelay = (double)((double)spnMaxDelay.Value);
			Spawn.SpawnMinDelay = (double)((double)spnMinDelay.Value);
			Spawn.SpawnTeam = (int)spnTeam.Value;
			Spawn.SpawnSpawnRange = (int)spnSpawnRange.Value;
			Spawn.SpawnProximityRange = (int)spnProximityRange.Value;
			Spawn.SpawnDuration = (double)((double)spnDuration.Value);
			Spawn.SpawnDespawn = (double)((double)spnDespawn.Value);
			Spawn.SpawnMinRefract = (double)((double)spnMinRefract.Value);
			Spawn.SpawnMaxRefract = (double)((double)spnMaxRefract.Value);
			Spawn.SpawnTODStart = (double)((double)spnTODStart.Value);
			Spawn.SpawnTODEnd = (double)((double)spnTODEnd.Value);
			Spawn.SpawnKillReset = (int)spnKillReset.Value;
			Spawn.SpawnProximitySnd = (int)spnProximitySnd.Value;
			Spawn.SpawnAllowGhost = chkAllowGhost.Checked;
			Spawn.SpawnSpawnOnTrigger = chkSpawnOnTrigger.Checked;
			if (!chkSequentialSpawn.Checked)
			{
				Spawn.SpawnSequentialSpawn = -1;
			}
			else
			{
				Spawn.SpawnSequentialSpawn = 0;
			}
			Spawn.SpawnSmartSpawning = chkSmartSpawning.Checked;
			if (!chkRealTOD.Checked)
			{
				Spawn.SpawnTODMode = 1;
			}
			else
			{
				Spawn.SpawnTODMode = 0;
			}
			Spawn.SpawnInContainer = chkInContainer.Checked;
			Spawn.SpawnSkillTrigger = TrimmedString(textSkillTrigger.Text);
			Spawn.SpawnSpeechTrigger = TrimmedString(textSpeechTrigger.Text);
			Spawn.SpawnProximityMsg = TrimmedString(textProximityMsg.Text);
			Spawn.SpawnMobTriggerName = TrimmedString(textMobTriggerName.Text);
			Spawn.SpawnMobTrigProp = TrimmedString(textMobTrigProp.Text);
			Spawn.SpawnPlayerTrigProp = TrimmedString(textPlayerTrigProp.Text);
			Spawn.SpawnTrigObjectProp = TrimmedString(textTrigObjectProp.Text);
			Spawn.SpawnTriggerOnCarried = TrimmedString(textTriggerOnCarried.Text);
			Spawn.SpawnNoTriggerOnCarried = TrimmedString(textNoTriggerOnCarried.Text);
			Spawn.SpawnTriggerProbability = (double)((double)spnTriggerProbability.Value);
			Spawn.SpawnStackAmount = (int)spnStackAmount.Value;
			Spawn.SpawnNotes = txtNotes.Text;
			Spawn.SpawnContainerX = (int)spnContainerX.Value;
			Spawn.SpawnContainerY = (int)spnContainerY.Value;
			Spawn.SpawnContainerZ = (int)spnContainerZ.Value;
			Spawn.SpawnExternalTriggering = chkExternalTriggering.Checked;
			Spawn.SpawnObjectPropertyItemName = TrimmedString(textTrigObjectName.Text);
			Spawn.SpawnSetPropertyItemName = TrimmedString(textSetObjectName.Text);
			Spawn.SpawnRegionName = TrimmedString(textRegionName.Text);
			Spawn.SpawnConfigFile = TrimmedString(textConfigFile.Text);
			Spawn.SpawnWaypoint = TrimmedString(textWayPoint.Text);
		}

		private void UpdateSpawnEntries()
		{
			int num;
			if (SelectedSpawn == null || SelectedSpawn.SpawnObjects == null)
			{
				return;
			}
			int value = vScrollBar1.Value;
			if (SelectedSpawn.SpawnObjects.Count > 7)
			{
				vScrollBar1.Maximum = SelectedSpawn.SpawnObjects.Count + 2;
			}
			int num1 = 0;
			int num2 = 0;
			IEnumerator enumerator = SelectedSpawn.SpawnObjects.GetEnumerator();
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
								current.TypeName = entryText1.Text;
								current.Count = (int)entryMax1.Value;
								current.SpawnsPerTick = (int)entryPer1.Value;
								if (entrySub1.Text == null || entrySub1.Text.Length <= 0)
								{
									current.SubGroup = 0;
								}
								else
								{
									try
									{
										current.SubGroup = int.Parse(entrySub1.Text);
									}
									catch
									{
									}
								}
								if (entryReset1.Text == null || entryReset1.Text.Length <= 0)
								{
									current.SequentialResetTime = 0;
								}
								else
								{
									try
									{
										current.SequentialResetTime = (double)int.Parse(entryReset1.Text);
									}
									catch
									{
									}
								}
								if (entryTo1.Text == null || entryTo1.Text.Length <= 0)
								{
									current.SequentialResetTo = 0;
								}
								else
								{
									try
									{
										current.SequentialResetTo = int.Parse(entryTo1.Text);
									}
									catch
									{
									}
								}
								if (entryKills1.Text == null || entryKills1.Text.Length <= 0)
								{
									current.KillsNeeded = 0;
								}
								else
								{
									try
									{
										current.KillsNeeded = int.Parse(entryKills1.Text);
									}
									catch
									{
									}
								}
								if (entryMinD1.Text == null || entryMinD1.Text.Length <= 0)
								{
									current.MinDelay = -1;
								}
								else
								{
									try
									{
										current.MinDelay = (double)int.Parse(entryMinD1.Text);
									}
									catch
									{
									}
								}
								if (entryMaxD1.Text == null || entryMaxD1.Text.Length <= 0)
								{
									current.MaxDelay = -1;
								}
								else
								{
									try
									{
										current.MaxDelay = (double)int.Parse(entryMaxD1.Text);
									}
									catch
									{
									}
								}
								current.RestrictKillsToSubgroup = chkRK1.Checked;
								current.ClearOnAdvance = chkClr1.Checked;
								break;
							}
							case 1:
							{
								current.TypeName = entryText2.Text;
								current.Count = (int)entryMax2.Value;
								current.SpawnsPerTick = (int)entryPer2.Value;
								if (entrySub2.Text == null || entrySub2.Text.Length <= 0)
								{
									current.SubGroup = 0;
								}
								else
								{
									try
									{
										current.SubGroup = int.Parse(entrySub2.Text);
									}
									catch
									{
									}
								}
								if (entryReset2.Text == null || entryReset2.Text.Length <= 0)
								{
									current.SequentialResetTime = 0;
								}
								else
								{
									try
									{
										current.SequentialResetTime = (double)int.Parse(entryReset2.Text);
									}
									catch
									{
									}
								}
								if (entryTo2.Text == null || entryTo2.Text.Length <= 0)
								{
									current.SequentialResetTo = 0;
								}
								else
								{
									try
									{
										current.SequentialResetTo = int.Parse(entryTo2.Text);
									}
									catch
									{
									}
								}
								if (entryKills2.Text == null || entryKills2.Text.Length <= 0)
								{
									current.KillsNeeded = 0;
								}
								else
								{
									try
									{
										current.KillsNeeded = int.Parse(entryKills2.Text);
									}
									catch
									{
									}
								}
								if (entryMinD2.Text == null || entryMinD2.Text.Length <= 0)
								{
									current.MinDelay = -1;
								}
								else
								{
									try
									{
										current.MinDelay = (double)int.Parse(entryMinD2.Text);
									}
									catch
									{
									}
								}
								if (entryMaxD2.Text == null || entryMaxD2.Text.Length <= 0)
								{
									current.MaxDelay = -1;
								}
								else
								{
									try
									{
										current.MaxDelay = (double)int.Parse(entryMaxD2.Text);
									}
									catch
									{
									}
								}
								current.RestrictKillsToSubgroup = chkRK2.Checked;
								current.ClearOnAdvance = chkClr2.Checked;
								break;
							}
							case 2:
							{
								current.TypeName = entryText3.Text;
								current.Count = (int)entryMax3.Value;
								current.SpawnsPerTick = (int)entryPer3.Value;
								if (entrySub3.Text == null || entrySub3.Text.Length <= 0)
								{
									current.SubGroup = 0;
								}
								else
								{
									try
									{
										current.SubGroup = int.Parse(entrySub3.Text);
									}
									catch
									{
									}
								}
								if (entryReset3.Text == null || entryReset3.Text.Length <= 0)
								{
									current.SequentialResetTime = 0;
								}
								else
								{
									try
									{
										current.SequentialResetTime = (double)int.Parse(entryReset3.Text);
									}
									catch
									{
									}
								}
								if (entryTo3.Text == null || entryTo3.Text.Length <= 0)
								{
									current.SequentialResetTo = 0;
								}
								else
								{
									try
									{
										current.SequentialResetTo = int.Parse(entryTo3.Text);
									}
									catch
									{
									}
								}
								if (entryKills3.Text == null || entryKills3.Text.Length <= 0)
								{
									current.KillsNeeded = 0;
								}
								else
								{
									try
									{
										current.KillsNeeded = int.Parse(entryKills3.Text);
									}
									catch
									{
									}
								}
								if (entryMinD3.Text == null || entryMinD3.Text.Length <= 0)
								{
									current.MinDelay = -1;
								}
								else
								{
									try
									{
										current.MinDelay = (double)int.Parse(entryMinD3.Text);
									}
									catch
									{
									}
								}
								if (entryMaxD3.Text == null || entryMaxD3.Text.Length <= 0)
								{
									current.MaxDelay = -1;
								}
								else
								{
									try
									{
										current.MaxDelay = (double)int.Parse(entryMaxD3.Text);
									}
									catch
									{
									}
								}
								current.RestrictKillsToSubgroup = chkRK3.Checked;
								current.ClearOnAdvance = chkClr3.Checked;
								break;
							}
							case 3:
							{
								current.TypeName = entryText4.Text;
								current.Count = (int)entryMax4.Value;
								current.SpawnsPerTick = (int)entryPer4.Value;
								if (entrySub4.Text == null || entrySub4.Text.Length <= 0)
								{
									current.SubGroup = 0;
								}
								else
								{
									try
									{
										current.SubGroup = int.Parse(entrySub4.Text);
									}
									catch
									{
									}
								}
								if (entryReset4.Text == null || entryReset4.Text.Length <= 0)
								{
									current.SequentialResetTime = 0;
								}
								else
								{
									try
									{
										current.SequentialResetTime = (double)int.Parse(entryReset4.Text);
									}
									catch
									{
									}
								}
								if (entryTo4.Text == null || entryTo4.Text.Length <= 0)
								{
									current.SequentialResetTo = 0;
								}
								else
								{
									try
									{
										current.SequentialResetTo = int.Parse(entryTo4.Text);
									}
									catch
									{
									}
								}
								if (entryKills4.Text == null || entryKills4.Text.Length <= 0)
								{
									current.KillsNeeded = 0;
								}
								else
								{
									try
									{
										current.KillsNeeded = int.Parse(entryKills4.Text);
									}
									catch
									{
									}
								}
								if (entryMinD4.Text == null || entryMinD4.Text.Length <= 0)
								{
									current.MinDelay = -1;
								}
								else
								{
									try
									{
										current.MinDelay = (double)int.Parse(entryMinD4.Text);
									}
									catch
									{
									}
								}
								if (entryMaxD4.Text == null || entryMaxD4.Text.Length <= 0)
								{
									current.MaxDelay = -1;
								}
								else
								{
									try
									{
										current.MaxDelay = (double)int.Parse(entryMaxD4.Text);
									}
									catch
									{
									}
								}
								current.RestrictKillsToSubgroup = chkRK4.Checked;
								current.ClearOnAdvance = chkClr4.Checked;
								break;
							}
							case 4:
							{
								current.TypeName = entryText5.Text;
								current.Count = (int)entryMax5.Value;
								current.SpawnsPerTick = (int)entryPer5.Value;
								if (entrySub5.Text == null || entrySub5.Text.Length <= 0)
								{
									current.SubGroup = 0;
								}
								else
								{
									try
									{
										current.SubGroup = int.Parse(entrySub5.Text);
									}
									catch
									{
									}
								}
								if (entryReset5.Text == null || entryReset5.Text.Length <= 0)
								{
									current.SequentialResetTime = 0;
								}
								else
								{
									try
									{
										current.SequentialResetTime = (double)int.Parse(entryReset5.Text);
									}
									catch
									{
									}
								}
								if (entryTo5.Text == null || entryTo5.Text.Length <= 0)
								{
									current.SequentialResetTo = 0;
								}
								else
								{
									try
									{
										current.SequentialResetTo = int.Parse(entryTo5.Text);
									}
									catch
									{
									}
								}
								if (entryKills5.Text == null || entryKills5.Text.Length <= 0)
								{
									current.KillsNeeded = 0;
								}
								else
								{
									try
									{
										current.KillsNeeded = int.Parse(entryKills5.Text);
									}
									catch
									{
									}
								}
								if (entryMinD5.Text == null || entryMinD5.Text.Length <= 0)
								{
									current.MinDelay = -1;
								}
								else
								{
									try
									{
										current.MinDelay = (double)int.Parse(entryMinD5.Text);
									}
									catch
									{
									}
								}
								if (entryMaxD5.Text == null || entryMaxD5.Text.Length <= 0)
								{
									current.MaxDelay = -1;
								}
								else
								{
									try
									{
										current.MaxDelay = (double)int.Parse(entryMaxD5.Text);
									}
									catch
									{
									}
								}
								current.RestrictKillsToSubgroup = chkRK5.Checked;
								current.ClearOnAdvance = chkClr5.Checked;
								break;
							}
							case 5:
							{
								current.TypeName = entryText6.Text;
								current.Count = (int)entryMax6.Value;
								current.SpawnsPerTick = (int)entryPer6.Value;
								if (entrySub6.Text == null || entrySub6.Text.Length <= 0)
								{
									current.SubGroup = 0;
								}
								else
								{
									try
									{
										current.SubGroup = int.Parse(entrySub6.Text);
									}
									catch
									{
									}
								}
								if (entryReset6.Text == null || entryReset6.Text.Length <= 0)
								{
									current.SequentialResetTime = 0;
								}
								else
								{
									try
									{
										current.SequentialResetTime = (double)int.Parse(entryReset6.Text);
									}
									catch
									{
									}
								}
								if (entryTo6.Text == null || entryTo6.Text.Length <= 0)
								{
									current.SequentialResetTo = 0;
								}
								else
								{
									try
									{
										current.SequentialResetTo = int.Parse(entryTo6.Text);
									}
									catch
									{
									}
								}
								if (entryKills6.Text == null || entryKills6.Text.Length <= 0)
								{
									current.KillsNeeded = 0;
								}
								else
								{
									try
									{
										current.KillsNeeded = int.Parse(entryKills6.Text);
									}
									catch
									{
									}
								}
								if (entryMinD6.Text == null || entryMinD6.Text.Length <= 0)
								{
									current.MinDelay = -1;
								}
								else
								{
									try
									{
										current.MinDelay = (double)int.Parse(entryMinD6.Text);
									}
									catch
									{
									}
								}
								if (entryMaxD6.Text == null || entryMaxD6.Text.Length <= 0)
								{
									current.MaxDelay = -1;
								}
								else
								{
									try
									{
										current.MaxDelay = (double)int.Parse(entryMaxD6.Text);
									}
									catch
									{
									}
								}
								current.RestrictKillsToSubgroup = chkRK6.Checked;
								current.ClearOnAdvance = chkClr6.Checked;
								break;
							}
							case 6:
							{
								current.TypeName = entryText7.Text;
								current.Count = (int)entryMax7.Value;
								current.SpawnsPerTick = (int)entryPer7.Value;
								if (entrySub7.Text == null || entrySub7.Text.Length <= 0)
								{
									current.SubGroup = 0;
								}
								else
								{
									try
									{
										current.SubGroup = int.Parse(entrySub7.Text);
									}
									catch
									{
									}
								}
								if (entryReset7.Text == null || entryReset7.Text.Length <= 0)
								{
									current.SequentialResetTime = 0;
								}
								else
								{
									try
									{
										current.SequentialResetTime = (double)int.Parse(entryReset7.Text);
									}
									catch
									{
									}
								}
								if (entryTo7.Text == null || entryTo7.Text.Length <= 0)
								{
									current.SequentialResetTo = 0;
								}
								else
								{
									try
									{
										current.SequentialResetTo = int.Parse(entryTo7.Text);
									}
									catch
									{
									}
								}
								if (entryKills7.Text == null || entryKills7.Text.Length <= 0)
								{
									current.KillsNeeded = 0;
								}
								else
								{
									try
									{
										current.KillsNeeded = int.Parse(entryKills7.Text);
									}
									catch
									{
									}
								}
								if (entryMinD7.Text == null || entryMinD7.Text.Length <= 0)
								{
									current.MinDelay = -1;
								}
								else
								{
									try
									{
										current.MinDelay = (double)int.Parse(entryMinD7.Text);
									}
									catch
									{
									}
								}
								if (entryMaxD7.Text == null || entryMaxD7.Text.Length <= 0)
								{
									current.MaxDelay = -1;
								}
								else
								{
									try
									{
										current.MaxDelay = (double)int.Parse(entryMaxD7.Text);
									}
									catch
									{
									}
								}
								current.RestrictKillsToSubgroup = chkRK7.Checked;
								current.ClearOnAdvance = chkClr7.Checked;
								break;
							}
							case 7:
							{
								current.TypeName = entryText8.Text;
								current.Count = (int)entryMax8.Value;
								current.SpawnsPerTick = (int)entryPer8.Value;
								if (entrySub8.Text == null || entrySub8.Text.Length <= 0)
								{
									current.SubGroup = 0;
								}
								else
								{
									try
									{
										current.SubGroup = int.Parse(entrySub8.Text);
									}
									catch
									{
									}
								}
								if (entryReset8.Text == null || entryReset8.Text.Length <= 0)
								{
									current.SequentialResetTime = 0;
								}
								else
								{
									try
									{
										current.SequentialResetTime = (double)int.Parse(entryReset8.Text);
									}
									catch
									{
									}
								}
								if (entryTo8.Text == null || entryTo8.Text.Length <= 0)
								{
									current.SequentialResetTo = 0;
								}
								else
								{
									try
									{
										current.SequentialResetTo = int.Parse(entryTo8.Text);
									}
									catch
									{
									}
								}
								if (entryKills8.Text == null || entryKills8.Text.Length <= 0)
								{
									current.KillsNeeded = 0;
								}
								else
								{
									try
									{
										current.KillsNeeded = int.Parse(entryKills8.Text);
									}
									catch
									{
									}
								}
								if (entryMinD8.Text == null || entryMinD8.Text.Length <= 0)
								{
									current.MinDelay = -1;
								}
								else
								{
									try
									{
										current.MinDelay = (double)int.Parse(entryMinD8.Text);
									}
									catch
									{
									}
								}
								if (entryMaxD8.Text == null || entryMaxD8.Text.Length <= 0)
								{
									current.MaxDelay = -1;
								}
								else
								{
									try
									{
										current.MaxDelay = (double)int.Parse(entryMaxD8.Text);
									}
									catch
									{
									}
								}
								current.RestrictKillsToSubgroup = chkRK8.Checked;
								current.ClearOnAdvance = chkClr8.Checked;
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
			if (SelectedSpawn != null && SelectedSpawn.SpawnObjects != null)
			{
				int count = 0;
				foreach (SpawnObject spawnObject in SelectedSpawn.SpawnObjects)
				{
					count = count + spawnObject.Count;
				}
				SelectedSpawn.SpawnMaxCount = count;
				spnMaxCount.Value = count;
			}
		}

		private void UpdateSpawnNode()
		{
			if (SelectedSpawnNode != null)
			{
				SelectedSpawnNode.UpdateNode();
			}
			tvwSpawnPoints.Update();
		}

		private void UpdateSpawnPacks(string packName, CheckedListBox.ObjectCollection items)
		{
			if (packName == null || packName.Length == 0 || items == null || items.Count == 0)
			{
				return;
			}
			bool flag = false;
			foreach (TreeNode node in tvwSpawnPacks.Nodes)
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
				tvwSpawnPacks.Nodes.Add(new SpawnPackNode(packName, items));
			}
			tvwSpawnPacks.Update();
		}

		private void vScrollBar1_MouseEnter(object sender, EventArgs e)
		{
			UpdateSpawnEntries();
			UpdateSpawnNode();
		}

		private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
		{
			DisplaySpawnEntries();
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
				foreach (SpawnPackNode node in tvwSpawnPacks.Nodes)
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
				string[] newLine = new string[] { "Failed to save SpawnPack file [", str, "] for the following reason:", Environment.NewLine, ExceptionMessage(exception) };
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
					dialogResult = ShowThreadExceptionDialog(t.Exception);
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
					return new Rectangle(X, Y, Width, Height);
				}
			}

			public SelectionWindow()
			{
			}

			public bool IsWithinWindow(short MapX, short MapY)
			{
				Rectangle rectangle = new Rectangle(X, Y, Width, Height);
				return rectangle.Contains(MapX, MapY);
			}
		}

		public class TrackerThread
		{
			private SpawnEditor Editor;

			public TrackerThread(SpawnEditor editor)
			{
				Editor = editor;
			}

			public void TrackerThreadMain()
			{
				int num = 0;
				int num1 = 0;
				int num2 = -1;
				bool flag = false;
				while (Editor != null && Editor.Tracking)
				{
					Thread.Sleep(250);
					int num3 = 0;
					int num4 = 0;
					int num5 = 0;
					int num6 = -1;
					if (!Client.FindLocation(ref num3, ref num4, ref num5, ref num6))
					{
						MessageBox.Show(string.Format("{0}. Make sure the client is started and that the 'Client Window' option in Setup is correct.", string.Concat(Editor._CfgDialog.CfgUoClientWindowValue, " Not Found"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation));
						Editor.Tracking = false;
						Editor.chkTracking.Checked = false;
					}
					else if (num6 != num2 || num3 != num || num4 != num1)
					{
						Editor.MyLocation.X = num3;
						Editor.MyLocation.Y = num4;
						Editor.MyLocation.Z = num5;
						Editor.MyLocation.Facet = num6;
						Editor.cbxMap.SelectedIndex = num6;
						Editor.AssignCenter((short)num3, (short)num4, (short)num6);
						Editor.DisplayMyLocation();
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