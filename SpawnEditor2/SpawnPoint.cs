using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Text;
using System.Xml;

namespace SpawnEditor2
{
	public class SpawnPoint
	{
		public short CentreX;

		public short CentreY;

		public short CentreZ;

		public short Range;

		public int Index;

		private Rectangle _Bounds;

		public bool IsSelected;

		public string XmlFileName;

		public string SpawnName;

		private int _SpawnHomeRange;

		public bool SpawnHomeRangeIsRelative;

		public int SpawnMaxCount;

		public double SpawnMinDelay;

		public double SpawnMaxDelay;

		public int SpawnTeam;

		public bool SpawnIsGroup;

		public bool SpawnIsRunning = true;

		public WorldMap Map;

		public Guid UnqiueId;

		public ArrayList SpawnObjects = new ArrayList();

		public int SpawnProximityRange = -1;

		public double SpawnDuration;

		public double SpawnDespawn;

		public double SpawnMinRefract;

		public double SpawnMaxRefract;

		public double SpawnTODStart;

		public double SpawnTODEnd;

		public int SpawnKillReset = 1;

		public int SpawnProximitySnd = 500;

		public bool SpawnAllowGhost;

		public bool SpawnSpawnOnTrigger;

		public int SpawnSequentialSpawn = -1;

		public bool SpawnSmartSpawning;

		public int SpawnTODMode;

		public string SpawnSkillTrigger;

		public string SpawnSpeechTrigger;

		public string SpawnProximityMsg;

		public string SpawnMobTriggerName;

		public string SpawnMobTrigProp;

		public string SpawnPlayerTrigProp;

		public string SpawnTrigObjectProp;

		public string SpawnTriggerOnCarried;

		public string SpawnNoTriggerOnCarried;

		public bool SpawnInContainer;

		public double SpawnTriggerProbability = 1;

		public string SpawnRegionName;

		public string SpawnConfigFile;

		public string SpawnObjectPropertyItemName;

		public string SpawnSetPropertyItemName;

		public int SpawnStackAmount = 1;

		public bool SpawnExternalTriggering;

		public string SpawnWaypoint;

		public int SpawnContainerX;

		public int SpawnContainerY;

		public int SpawnContainerZ;

		public string SpawnNotes;

		public bool AllowGhost
		{
			get
			{
				return this.SpawnAllowGhost;
			}
			set
			{
				this.SpawnAllowGhost = value;
			}
		}

		public int Area
		{
			get
			{
				return this.Bounds.Width * this.Bounds.Height;
			}
		}

		public Rectangle Bounds
		{
			get
			{
				return this._Bounds;
			}
			set
			{
				this._Bounds = value;
			}
		}

		public string ConfigFile
		{
			get
			{
				return this.SpawnConfigFile;
			}
			set
			{
				this.SpawnConfigFile = value;
			}
		}

		public int ContainerX
		{
			get
			{
				return this.SpawnContainerX;
			}
			set
			{
				this.SpawnContainerX = value;
			}
		}

		public int ContainerY
		{
			get
			{
				return this.SpawnContainerY;
			}
			set
			{
				this.SpawnContainerY = value;
			}
		}

		public int ContainerZ
		{
			get
			{
				return this.SpawnContainerZ;
			}
			set
			{
				this.SpawnContainerZ = value;
			}
		}

		public double Despawn
		{
			get
			{
				return this.SpawnDespawn;
			}
			set
			{
				this.SpawnDespawn = value;
			}
		}

		public double Duration
		{
			get
			{
				return this.SpawnDuration;
			}
			set
			{
				this.SpawnDuration = value;
			}
		}

		public bool ExternalTriggering
		{
			get
			{
				return this.SpawnExternalTriggering;
			}
			set
			{
				this.SpawnExternalTriggering = value;
			}
		}

		public bool GameTOD
		{
			get
			{
				return this.SpawnTODMode == 1;
			}
			set
			{
				if (value)
				{
					this.SpawnTODMode = 1;
					return;
				}
				this.SpawnTODMode = 0;
			}
		}

		public bool Group
		{
			get
			{
				return this.SpawnIsGroup;
			}
			set
			{
				this.SpawnIsGroup = value;
			}
		}

		public int HomeRange
		{
			get
			{
				return this.SpawnHomeRange;
			}
			set
			{
				this.SpawnHomeRange = value;
			}
		}

		public bool InContainer
		{
			get
			{
				return this.SpawnInContainer;
			}
			set
			{
				this.SpawnInContainer = value;
			}
		}

		public int KillReset
		{
			get
			{
				return this.SpawnKillReset;
			}
			set
			{
				this.SpawnKillReset = value;
			}
		}

		public int MaxCount
		{
			get
			{
				return this.SpawnMaxCount;
			}
			set
			{
				this.SpawnMaxCount = value;
			}
		}

		public double MaxDelay
		{
			get
			{
				return this.SpawnMaxDelay;
			}
			set
			{
				this.SpawnMaxDelay = value;
			}
		}

		public double MaxRefract
		{
			get
			{
				return this.SpawnMaxRefract;
			}
			set
			{
				this.SpawnMaxRefract = value;
			}
		}

		public double MinDelay
		{
			get
			{
				return this.SpawnMinDelay;
			}
			set
			{
				this.SpawnMinDelay = value;
			}
		}

		public double MinRefract
		{
			get
			{
				return this.SpawnMinRefract;
			}
			set
			{
				this.SpawnMinRefract = value;
			}
		}

		public string MobTriggerName
		{
			get
			{
				return this.SpawnMobTriggerName;
			}
			set
			{
				this.SpawnMobTriggerName = value;
			}
		}

		public string MobTrigProp
		{
			get
			{
				return this.SpawnMobTrigProp;
			}
			set
			{
				this.SpawnMobTrigProp = value;
			}
		}

		public string NoTriggerOnCarried
		{
			get
			{
				return this.SpawnNoTriggerOnCarried;
			}
			set
			{
				this.SpawnNoTriggerOnCarried = value;
			}
		}

		public string PlayerTrigProp
		{
			get
			{
				return this.SpawnPlayerTrigProp;
			}
			set
			{
				this.SpawnPlayerTrigProp = value;
			}
		}

		public string ProximityMsg
		{
			get
			{
				return this.SpawnProximityMsg;
			}
			set
			{
				this.SpawnProximityMsg = value;
			}
		}

		public int ProximityRange
		{
			get
			{
				return this.SpawnProximityRange;
			}
			set
			{
				this.SpawnProximityRange = value;
			}
		}

		public int ProximitySnd
		{
			get
			{
				return this.SpawnProximitySnd;
			}
			set
			{
				this.SpawnProximitySnd = value;
			}
		}

		public bool RealTOD
		{
			get
			{
				return this.SpawnTODMode == 0;
			}
			set
			{
				if (value)
				{
					this.SpawnTODMode = 0;
					return;
				}
				this.SpawnTODMode = 1;
			}
		}

		public string RegionName
		{
			get
			{
				return this.SpawnRegionName;
			}
			set
			{
				this.SpawnRegionName = value;
			}
		}

		public bool RelativeHome
		{
			get
			{
				return this.SpawnHomeRangeIsRelative;
			}
			set
			{
				this.SpawnHomeRangeIsRelative = value;
			}
		}

		public bool Running
		{
			get
			{
				return this.SpawnIsRunning;
			}
			set
			{
				this.SpawnIsRunning = value;
			}
		}

		public bool SequentialSpawn
		{
			get
			{
				return this.SpawnSequentialSpawn >= 0;
			}
			set
			{
				if (value)
				{
					this.SpawnSequentialSpawn = 0;
					return;
				}
				this.SpawnSequentialSpawn = -1;
			}
		}

		public string SetObjectName
		{
			get
			{
				return this.SpawnSetPropertyItemName;
			}
			set
			{
				this.SpawnSetPropertyItemName = value;
			}
		}

		public string SkillTrigger
		{
			get
			{
				return this.SpawnSkillTrigger;
			}
			set
			{
				this.SpawnSkillTrigger = value;
			}
		}

		public bool SmartSpawning
		{
			get
			{
				return this.SpawnSmartSpawning;
			}
			set
			{
				this.SpawnSmartSpawning = value;
			}
		}

		public int SpawnHomeRange
		{
			get
			{
				return this._SpawnHomeRange;
			}
			set
			{
				this._SpawnHomeRange = value;
			}
		}

		public bool SpawnOnTrigger
		{
			get
			{
				return this.SpawnSpawnOnTrigger;
			}
			set
			{
				this.SpawnSpawnOnTrigger = value;
			}
		}

		public int SpawnRange
		{
			get
			{
				return this.SpawnSpawnRange;
			}
			set
			{
				this.SpawnSpawnRange = value;
			}
		}

		public int SpawnSpawnRange
		{
			get
			{
				if (this.Bounds.Width != this.Bounds.Height || this.CentreX != this.Bounds.X + this.Bounds.Width / 2 || this.CentreY != this.Bounds.Y + this.Bounds.Height / 2)
				{
					return -1;
				}
				return this.Bounds.Width / 2;
			}
			set
			{
				if (value >= 0)
				{
					int centreX = this.CentreX - value;
					int centreY = this.CentreY - value;
					this.Bounds = new Rectangle(centreX, centreY, value * 2, value * 2);
				}
			}
		}

		public string SpeechTrigger
		{
			get
			{
				return this.SpawnSpeechTrigger;
			}
			set
			{
				this.SpawnSpeechTrigger = value;
			}
		}

		public int StackAmount
		{
			get
			{
				return this.SpawnStackAmount;
			}
			set
			{
				this.SpawnStackAmount = value;
			}
		}

		public int Team
		{
			get
			{
				return this.SpawnTeam;
			}
			set
			{
				this.SpawnTeam = value;
			}
		}

		public double TODEnd
		{
			get
			{
				return this.SpawnTODEnd;
			}
			set
			{
				this.SpawnTODEnd = value;
			}
		}

		public int TODMode
		{
			get
			{
				return this.SpawnTODMode;
			}
			set
			{
				this.SpawnTODMode = value;
			}
		}

		public double TODStart
		{
			get
			{
				return this.SpawnTODStart;
			}
			set
			{
				this.SpawnTODStart = value;
			}
		}

		public string TriggerOnCarried
		{
			get
			{
				return this.SpawnTriggerOnCarried;
			}
			set
			{
				this.SpawnTriggerOnCarried = value;
			}
		}

		public double TriggerProbability
		{
			get
			{
				return this.SpawnTriggerProbability;
			}
			set
			{
				this.SpawnTriggerProbability = value;
			}
		}

		public string TrigObjectName
		{
			get
			{
				return this.SpawnObjectPropertyItemName;
			}
			set
			{
				this.SpawnObjectPropertyItemName = value;
			}
		}

		public string TrigObjectProp
		{
			get
			{
				return this.SpawnTrigObjectProp;
			}
			set
			{
				this.SpawnTrigObjectProp = value;
			}
		}

		public string WaypointName
		{
			get
			{
				return this.SpawnWaypoint;
			}
			set
			{
				this.SpawnWaypoint = value;
			}
		}

		public SpawnPoint(Guid unqiueId, WorldMap Map, short MapX, short MapY, short MapWidth, short MapHeight)
		{
			this.UnqiueId = unqiueId;
			this.Map = Map;
			this.Index = -1;
			this.IsSelected = true;
			this.Bounds = new Rectangle(MapX - MapWidth / 2, MapY - MapHeight / 2, MapWidth, MapHeight);
			this.CentreX = MapX;
			this.CentreY = MapY;
			Rectangle bounds = this.Bounds;
			this.SpawnName = string.Concat("Spawn Point ", bounds.ToString());
		}

		public SpawnPoint(Guid uniqueId, WorldMap Map, Rectangle SpawnBounds)
		{
			this.UnqiueId = uniqueId;
			this.Map = Map;
			this.Index = -1;
			this.IsSelected = true;
			this.Bounds = SpawnBounds;
			int x = this.Bounds.X;
			Rectangle bounds = this.Bounds;
			this.CentreX = (short)(x + bounds.Width / 2);
			int y = this.Bounds.Y;
			bounds = this.Bounds;
			this.CentreY = (short)(y + bounds.Height / 2);
			bounds = this.Bounds;
			this.SpawnName = string.Concat("Spawn Point ", bounds.ToString());
		}

		public SpawnPoint(XmlElement node, WorldMap ForceMap, bool ForceGuid)
		{
			int num = 0;
			Guid guid = Guid.NewGuid();
			if (!ForceGuid)
			{
				string text = SpawnPoint.GetText(node["UniqueId"], "Error");
				if (text != "Error")
				{
					guid = new Guid(text);
				}
			}
			WorldMap forceMap = ForceMap;
			if (ForceMap == WorldMap.Internal)
			{
				forceMap = WorldMap.Trammel;
				try
				{
					forceMap = (WorldMap)((int)((WorldMap)Enum.Parse(typeof(WorldMap), SpawnPoint.GetText(node["Map"], "Trammel"))));
				}
				catch
				{
				}
			}
			bool flag = false;
			try
			{
				flag = bool.Parse(SpawnPoint.GetText(node["IsHomeRangeRelative"], "false"));
			}
			catch
			{
				num++;
			}
			int num1 = int.Parse(SpawnPoint.GetText(node["X"], "0"));
			int num2 = int.Parse(SpawnPoint.GetText(node["Y"], "0"));
			int num3 = int.Parse(SpawnPoint.GetText(node["Width"], "0"));
			int num4 = int.Parse(SpawnPoint.GetText(node["Height"], "0"));
			this.UnqiueId = guid;
			this.Map = forceMap;
			this._Bounds = new Rectangle(num1, num2, num3, num4);
			this.SpawnName = SpawnPoint.GetText(node["Name"], "Spawner");
			this.CentreX = short.Parse(SpawnPoint.GetText(node["CentreX"], "0"));
			this.CentreY = short.Parse(SpawnPoint.GetText(node["CentreY"], "0"));
			this.CentreZ = short.Parse(SpawnPoint.GetText(node["CentreZ"], "0"));
			this._SpawnHomeRange = int.Parse(SpawnPoint.GetText(node["Range"], "0"));
			this.SpawnMaxCount = int.Parse(SpawnPoint.GetText(node["MaxCount"], "0"));
			bool flag1 = false;
			try
			{
				flag1 = bool.Parse(SpawnPoint.GetText(node["DelayInSec"], "false"));
			}
			catch
			{
				num++;
			}
			if (!flag1)
			{
				this.SpawnMinDelay = double.Parse(SpawnPoint.GetText(node["MinDelay"], "0"));
				this.SpawnMaxDelay = double.Parse(SpawnPoint.GetText(node["MaxDelay"], "0"));
			}
			else
			{
				this.SpawnMinDelay = double.Parse(SpawnPoint.GetText(node["MinDelay"], "0")) / 60;
				this.SpawnMaxDelay = double.Parse(SpawnPoint.GetText(node["MaxDelay"], "0")) / 60;
			}
			this.SpawnTeam = int.Parse(SpawnPoint.GetText(node["Team"], "0"));
			this.SpawnIsGroup = bool.Parse(SpawnPoint.GetText(node["IsGroup"], "false"));
			this.SpawnIsRunning = bool.Parse(SpawnPoint.GetText(node["IsRunning"], "false"));
			this.SpawnHomeRangeIsRelative = flag;
			this.SpawnProximityRange = -1;
			try
			{
				this.SpawnProximityRange = int.Parse(SpawnPoint.GetText(node["ProximityRange"], "-1"));
			}
			catch
			{
				num++;
			}
			try
			{
				this.SpawnDuration = double.Parse(SpawnPoint.GetText(node["Duration"], "0"));
			}
			catch
			{
				num++;
			}
			try
			{
				this.SpawnDespawn = double.Parse(SpawnPoint.GetText(node["DespawnTime"], "0"));
			}
			catch
			{
				num++;
			}
			try
			{
				this.SpawnMinRefract = double.Parse(SpawnPoint.GetText(node["MinRefractory"], "0"));
			}
			catch
			{
				num++;
			}
			try
			{
				this.SpawnMaxRefract = double.Parse(SpawnPoint.GetText(node["MaxRefractory"], "0"));
			}
			catch
			{
				num++;
			}
			try
			{
				this.SpawnTODStart = double.Parse(SpawnPoint.GetText(node["TODStart"], "0")) / 60;
			}
			catch
			{
				num++;
			}
			try
			{
				this.SpawnTODEnd = double.Parse(SpawnPoint.GetText(node["TODEnd"], "0")) / 60;
			}
			catch
			{
				num++;
			}
			this.SpawnKillReset = 1;
			try
			{
				this.SpawnKillReset = int.Parse(SpawnPoint.GetText(node["KillReset"], "1"));
			}
			catch
			{
				num++;
			}
			try
			{
				this.SpawnProximitySnd = int.Parse(SpawnPoint.GetText(node["ProximityTriggerSound"], "500"));
			}
			catch
			{
				num++;
			}
			try
			{
				this.SpawnAllowGhost = bool.Parse(SpawnPoint.GetText(node["AllowGhostTriggering"], "false"));
			}
			catch
			{
				num++;
			}
			try
			{
				this.SpawnSpawnOnTrigger = bool.Parse(SpawnPoint.GetText(node["SpawnOnTrigger"], "false"));
			}
			catch
			{
				num++;
			}
			try
			{
				this.SpawnSequentialSpawn = int.Parse(SpawnPoint.GetText(node["SequentialSpawning"], "-1"));
			}
			catch
			{
				num++;
			}
			try
			{
				this.SpawnSmartSpawning = bool.Parse(SpawnPoint.GetText(node["SmartSpawning"], "false"));
			}
			catch
			{
				num++;
			}
			try
			{
				this.SpawnTODMode = int.Parse(SpawnPoint.GetText(node["TODMode"], "0"));
			}
			catch
			{
				num++;
			}
			try
			{
				this.SpawnSkillTrigger = SpawnPoint.GetText(node["SkillTrigger"], null);
			}
			catch
			{
				num++;
			}
			try
			{
				this.SpawnSpeechTrigger = SpawnPoint.GetText(node["SpeechTrigger"], null);
			}
			catch
			{
				num++;
			}
			try
			{
				this.SpawnProximityMsg = SpawnPoint.GetText(node["ProximityTriggerMessage"], null);
			}
			catch
			{
				num++;
			}
			try
			{
				this.SpawnMobTriggerName = SpawnPoint.GetText(node["MobTriggerName"], null);
			}
			catch
			{
				num++;
			}
			try
			{
				this.SpawnMobTrigProp = SpawnPoint.GetText(node["MobPropertyName"], null);
			}
			catch
			{
				num++;
			}
			try
			{
				this.SpawnPlayerTrigProp = SpawnPoint.GetText(node["PlayerPropertyName"], null);
			}
			catch
			{
				num++;
			}
			try
			{
				this.SpawnTrigObjectProp = SpawnPoint.GetText(node["ObjectPropertyName"], null);
			}
			catch
			{
				num++;
			}
			try
			{
				this.SpawnTriggerOnCarried = SpawnPoint.GetText(node["ItemTriggerName"], null);
			}
			catch
			{
				num++;
			}
			try
			{
				this.SpawnNoTriggerOnCarried = SpawnPoint.GetText(node["NoItemTriggerName"], null);
			}
			catch
			{
				num++;
			}
			this.SpawnInContainer = false;
			this.SpawnContainerX = 0;
			this.SpawnContainerY = 0;
			this.SpawnContainerZ = 0;
			try
			{
				this.SpawnInContainer = bool.Parse(SpawnPoint.GetText(node["InContainer"], "false"));
			}
			catch
			{
				num++;
			}
			if (this.SpawnInContainer)
			{
				try
				{
					this.SpawnContainerX = int.Parse(SpawnPoint.GetText(node["ContainerX"], "0"));
				}
				catch
				{
					num++;
				}
				try
				{
					this.SpawnContainerY = int.Parse(SpawnPoint.GetText(node["ContainerY"], "0"));
				}
				catch
				{
					num++;
				}
				try
				{
					this.SpawnContainerZ = int.Parse(SpawnPoint.GetText(node["ContainerZ"], "0"));
				}
				catch
				{
					num++;
				}
			}
			this.SpawnTriggerProbability = 1;
			try
			{
				this.SpawnTriggerProbability = double.Parse(SpawnPoint.GetText(node["TriggerProbability"], "1"));
			}
			catch
			{
				num++;
			}
			this.SpawnRegionName = null;
			try
			{
				this.SpawnRegionName = SpawnPoint.GetText(node["RegionName"], null);
			}
			catch
			{
				num++;
			}
			this.SpawnConfigFile = null;
			try
			{
				this.SpawnConfigFile = SpawnPoint.GetText(node["ConfigFile"], null);
			}
			catch
			{
				num++;
			}
			this.SpawnObjectPropertyItemName = null;
			try
			{
				this.SpawnObjectPropertyItemName = SpawnPoint.GetText(node["ObjectPropertyItemName"], null);
			}
			catch
			{
				num++;
			}
			this.SpawnSetPropertyItemName = null;
			try
			{
				this.SpawnSetPropertyItemName = SpawnPoint.GetText(node["SetPropertyItemName"], null);
			}
			catch
			{
				num++;
			}
			this.SpawnStackAmount = 1;
			try
			{
				this.SpawnStackAmount = int.Parse(SpawnPoint.GetText(node["Amount"], "1"));
			}
			catch
			{
				num++;
			}
			this.SpawnExternalTriggering = false;
			try
			{
				this.SpawnExternalTriggering = bool.Parse(SpawnPoint.GetText(node["ExternalTriggering"], "false"));
			}
			catch
			{
				num++;
			}
			this.SpawnWaypoint = null;
			try
			{
				this.SpawnWaypoint = SpawnPoint.GetText(node["Waypoint"], null);
			}
			catch
			{
				num++;
			}
			bool flag2 = true;
			try
			{
				string str = SpawnPoint.GetText(node["Objects2"], null);
				if (str == null)
				{
					flag2 = false;
				}
				else
				{
					this.LoadSpawnObjectsFromString2(str);
				}
			}
			catch
			{
				flag2 = false;
			}
			if (!flag2)
			{
				try
				{
					this.LoadSpawnObjectsFromString(SpawnPoint.GetText(node["Objects"], null));
				}
				catch
				{
				}
			}
			try
			{
				this.SpawnNotes = SpawnPoint.GetText(node["Notes"], null);
			}
			catch
			{
				num++;
			}
			this.IsSelected = false;
		}

		public SpawnPoint(DataRow SpawnRow)
		{
			int num = 0;
			Guid guid = Guid.NewGuid();
			try
			{
				guid = new Guid((string)SpawnRow["UniqueId"]);
			}
			catch
			{
			}
			WorldMap worldMap = WorldMap.Trammel;
			try
			{
				worldMap = (WorldMap)((int)((WorldMap)Enum.Parse(typeof(WorldMap), (string)SpawnRow["Map"], true)));
			}
			catch
			{
				num++;
			}
			bool flag = false;
			try
			{
				flag = bool.Parse((string)SpawnRow["IsHomeRangeRelative"]);
			}
			catch
			{
				num++;
			}
			int num1 = int.Parse((string)SpawnRow["X"]);
			int num2 = int.Parse((string)SpawnRow["Y"]);
			int num3 = int.Parse((string)SpawnRow["Width"]);
			int num4 = int.Parse((string)SpawnRow["Height"]);
			this.UnqiueId = guid;
			this.Map = worldMap;
			this._Bounds = new Rectangle(num1, num2, num3, num4);
			this.SpawnName = (string)SpawnRow["Name"];
			this.CentreX = short.Parse((string)SpawnRow["CentreX"]);
			this.CentreY = short.Parse((string)SpawnRow["CentreY"]);
			this.CentreZ = short.Parse((string)SpawnRow["CentreZ"]);
			this._SpawnHomeRange = int.Parse((string)SpawnRow["Range"]);
			this.SpawnMaxCount = int.Parse((string)SpawnRow["MaxCount"]);
			bool flag1 = false;
			try
			{
				flag1 = bool.Parse((string)SpawnRow["DelayInSec"]);
			}
			catch
			{
				num++;
			}
			if (!flag1)
			{
				this.SpawnMinDelay = double.Parse((string)SpawnRow["MinDelay"]);
				this.SpawnMaxDelay = double.Parse((string)SpawnRow["MaxDelay"]);
			}
			else
			{
				this.SpawnMinDelay = double.Parse((string)SpawnRow["MinDelay"]) / 60;
				this.SpawnMaxDelay = double.Parse((string)SpawnRow["MaxDelay"]) / 60;
			}
			this.SpawnTeam = int.Parse((string)SpawnRow["Team"]);
			this.SpawnIsGroup = bool.Parse((string)SpawnRow["IsGroup"]);
			this.SpawnIsRunning = bool.Parse((string)SpawnRow["IsRunning"]);
			this.SpawnHomeRangeIsRelative = flag;
			this.SpawnProximityRange = -1;
			try
			{
				this.SpawnProximityRange = int.Parse((string)SpawnRow["ProximityRange"]);
			}
			catch
			{
				num++;
			}
			try
			{
				this.SpawnDuration = double.Parse((string)SpawnRow["Duration"]);
			}
			catch
			{
				num++;
			}
			try
			{
				this.SpawnDespawn = double.Parse((string)SpawnRow["DespawnTime"]);
			}
			catch
			{
				num++;
			}
			try
			{
				this.SpawnMinRefract = double.Parse((string)SpawnRow["MinRefractory"]);
			}
			catch
			{
				num++;
			}
			try
			{
				this.SpawnMaxRefract = double.Parse((string)SpawnRow["MaxRefractory"]);
			}
			catch
			{
				num++;
			}
			try
			{
				this.SpawnTODStart = double.Parse((string)SpawnRow["TODStart"]) / 60;
			}
			catch
			{
				num++;
			}
			try
			{
				this.SpawnTODEnd = double.Parse((string)SpawnRow["TODEnd"]) / 60;
			}
			catch
			{
				num++;
			}
			this.SpawnKillReset = 1;
			try
			{
				this.SpawnKillReset = int.Parse((string)SpawnRow["KillReset"]);
			}
			catch
			{
				num++;
			}
			try
			{
				this.SpawnProximitySnd = int.Parse((string)SpawnRow["ProximityTriggerSound"]);
			}
			catch
			{
				num++;
			}
			try
			{
				this.SpawnAllowGhost = bool.Parse((string)SpawnRow["AllowGhostTriggering"]);
			}
			catch
			{
				num++;
			}
			try
			{
				this.SpawnSpawnOnTrigger = bool.Parse((string)SpawnRow["SpawnOnTrigger"]);
			}
			catch
			{
				num++;
			}
			try
			{
				this.SpawnSequentialSpawn = int.Parse((string)SpawnRow["SequentialSpawning"]);
			}
			catch
			{
				num++;
			}
			try
			{
				this.SpawnSmartSpawning = bool.Parse((string)SpawnRow["SmartSpawning"]);
			}
			catch
			{
				num++;
			}
			try
			{
				if (SpawnRow.Table.Columns.Contains("TODMode"))
				{
					this.SpawnTODMode = int.Parse((string)SpawnRow["TODMode"]);
				}
			}
			catch
			{
				num++;
			}
			try
			{
				if (SpawnRow.Table.Columns.Contains("SkillTrigger"))
				{
					this.SpawnSkillTrigger = (string)SpawnRow["SkillTrigger"];
				}
			}
			catch
			{
				num++;
			}
			try
			{
				if (SpawnRow.Table.Columns.Contains("SpeechTrigger"))
				{
					this.SpawnSpeechTrigger = (string)SpawnRow["SpeechTrigger"];
				}
			}
			catch
			{
				num++;
			}
			try
			{
				if (SpawnRow.Table.Columns.Contains("ProximityTriggerMessage"))
				{
					this.SpawnProximityMsg = (string)SpawnRow["ProximityTriggerMessage"];
				}
			}
			catch
			{
				num++;
			}
			try
			{
				if (SpawnRow.Table.Columns.Contains("MobTriggerName"))
				{
					this.SpawnMobTriggerName = (string)SpawnRow["MobTriggerName"];
				}
			}
			catch
			{
				num++;
			}
			try
			{
				if (SpawnRow.Table.Columns.Contains("MobPropertyName"))
				{
					this.SpawnMobTrigProp = (string)SpawnRow["MobPropertyName"];
				}
			}
			catch
			{
				num++;
			}
			try
			{
				if (SpawnRow.Table.Columns.Contains("PlayerPropertyName"))
				{
					this.SpawnPlayerTrigProp = (string)SpawnRow["PlayerPropertyName"];
				}
			}
			catch
			{
				num++;
			}
			try
			{
				if (SpawnRow.Table.Columns.Contains("ObjectPropertyName"))
				{
					this.SpawnTrigObjectProp = (string)SpawnRow["ObjectPropertyName"];
				}
			}
			catch
			{
				num++;
			}
			try
			{
				if (SpawnRow.Table.Columns.Contains("ItemTriggerName"))
				{
					this.SpawnTriggerOnCarried = (string)SpawnRow["ItemTriggerName"];
				}
			}
			catch
			{
				num++;
			}
			try
			{
				if (SpawnRow.Table.Columns.Contains("NoItemTriggerName"))
				{
					this.SpawnNoTriggerOnCarried = (string)SpawnRow["NoItemTriggerName"];
				}
			}
			catch
			{
				num++;
			}
			this.SpawnInContainer = false;
			this.SpawnContainerX = 0;
			this.SpawnContainerY = 0;
			this.SpawnContainerZ = 0;
			try
			{
				if (SpawnRow.Table.Columns.Contains("InContainer"))
				{
					this.SpawnInContainer = bool.Parse((string)SpawnRow["InContainer"]);
				}
			}
			catch
			{
				num++;
			}
			if (this.SpawnInContainer)
			{
				try
				{
					this.SpawnContainerX = int.Parse((string)SpawnRow["ContainerX"]);
				}
				catch
				{
					num++;
				}
				try
				{
					this.SpawnContainerY = int.Parse((string)SpawnRow["ContainerY"]);
				}
				catch
				{
					num++;
				}
				try
				{
					this.SpawnContainerZ = int.Parse((string)SpawnRow["ContainerZ"]);
				}
				catch
				{
					num++;
				}
			}
			this.SpawnTriggerProbability = 1;
			try
			{
				if (SpawnRow.Table.Columns.Contains("TriggerProbability"))
				{
					this.SpawnTriggerProbability = double.Parse((string)SpawnRow["TriggerProbability"]);
				}
			}
			catch
			{
				num++;
			}
			this.SpawnRegionName = null;
			try
			{
				if (SpawnRow.Table.Columns.Contains("RegionName"))
				{
					this.SpawnRegionName = (string)SpawnRow["RegionName"];
				}
			}
			catch
			{
				num++;
			}
			this.SpawnConfigFile = null;
			try
			{
				if (SpawnRow.Table.Columns.Contains("ConfigFile"))
				{
					this.SpawnConfigFile = (string)SpawnRow["ConfigFile"];
				}
			}
			catch
			{
				num++;
			}
			this.SpawnObjectPropertyItemName = null;
			try
			{
				if (SpawnRow.Table.Columns.Contains("ObjectPropertyItemName"))
				{
					this.SpawnObjectPropertyItemName = (string)SpawnRow["ObjectPropertyItemName"];
				}
			}
			catch
			{
				num++;
			}
			this.SpawnSetPropertyItemName = null;
			try
			{
				if (SpawnRow.Table.Columns.Contains("SetPropertyItemName"))
				{
					this.SpawnSetPropertyItemName = (string)SpawnRow["SetPropertyItemName"];
				}
			}
			catch
			{
				num++;
			}
			this.SpawnStackAmount = 1;
			try
			{
				if (SpawnRow.Table.Columns.Contains("Amount"))
				{
					this.SpawnStackAmount = int.Parse((string)SpawnRow["Amount"]);
				}
			}
			catch
			{
				num++;
			}
			this.SpawnExternalTriggering = false;
			try
			{
				if (SpawnRow.Table.Columns.Contains("ExternalTriggering"))
				{
					this.SpawnExternalTriggering = bool.Parse((string)SpawnRow["ExternalTriggering"]);
				}
			}
			catch
			{
				num++;
			}
			this.SpawnWaypoint = null;
			try
			{
				if (SpawnRow.Table.Columns.Contains("Waypoint"))
				{
					this.SpawnWaypoint = (string)SpawnRow["Waypoint"];
				}
			}
			catch
			{
				num++;
			}
			bool flag2 = true;
			try
			{
				if (!SpawnRow.Table.Columns.Contains("Objects2"))
				{
					flag2 = false;
				}
				else
				{
					this.LoadSpawnObjectsFromString2((string)SpawnRow["Objects2"]);
				}
			}
			catch
			{
				flag2 = false;
			}
			if (!flag2)
			{
				try
				{
					this.LoadSpawnObjectsFromString((string)SpawnRow["Objects"]);
				}
				catch
				{
				}
			}
			try
			{
				if (SpawnRow.Table.Columns.Contains("Notes"))
				{
					this.SpawnNotes = (string)SpawnRow["Notes"];
				}
			}
			catch
			{
				num++;
			}
			this.IsSelected = false;
		}

		public string GetSerializedObjectList()
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (SpawnObject spawnObject in this.SpawnObjects)
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(':');
				}
				stringBuilder.AppendFormat("{0}={1}", spawnObject.TypeName, spawnObject.Count);
			}
			return stringBuilder.ToString();
		}

		public string GetSerializedObjectList2()
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (SpawnObject spawnObject in this.SpawnObjects)
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(":OBJ=");
				}
				StringBuilder stringBuilder1 = stringBuilder;
				object[] typeName = new object[] { spawnObject.TypeName, spawnObject.Count, spawnObject.SubGroup, spawnObject.SequentialResetTime, spawnObject.SequentialResetTo, spawnObject.KillsNeeded, null, null, null, null, null };
				typeName[6] = (spawnObject.RestrictKillsToSubgroup ? 1 : 0);
				typeName[7] = (spawnObject.ClearOnAdvance ? 1 : 0);
				typeName[8] = spawnObject.MinDelay;
				typeName[9] = spawnObject.MaxDelay;
				typeName[10] = spawnObject.SpawnsPerTick;
				stringBuilder1.AppendFormat("{0}:MX={1}:SB={2}:RT={3}:TO={4}:KL={5}:RK={6}:CA={7}:DN={8}:DX={9}:SP={10}", typeName);
			}
			return stringBuilder.ToString();
		}

		private static string GetText(XmlElement node, string defaultValue)
		{
			if (node == null)
			{
				return defaultValue;
			}
			return node.InnerText;
		}

		public bool IsSameArea(short MapX, short MapY, short Range)
		{
			Rectangle rectangle = new Rectangle(MapX - Range, MapY - Range, Range * 2, Range * 2);
			if (this.Bounds.IntersectsWith(rectangle))
			{
				return true;
			}
			return rectangle.Contains(this.CentreX, this.CentreY);
		}

		public bool IsSameArea(short MapX, short MapY)
		{
			int num = 2;
			Rectangle rectangle = new Rectangle(MapX - num, MapY - num, num * 2, num * 2);
			if (this.Bounds.Contains(MapX, MapY))
			{
				return true;
			}
			return rectangle.Contains(this.CentreX, this.CentreY);
		}

		public void LoadSpawnObjectsFromString(string SerializedObjectList)
		{
			this.SpawnObjects.Clear();
			if (SerializedObjectList.Length > 0)
			{
				char[] chrArray = new char[] { ':' };
				string[] strArrays = SerializedObjectList.Split(chrArray);
				for (int i = 0; i < (int)strArrays.Length; i++)
				{
					string str = strArrays[i];
					chrArray = new char[] { '=' };
					string[] strArrays1 = str.Split(chrArray);
					if ((int)strArrays1.Length == 2 && strArrays1[0].Length > 0 && strArrays1[1].Length > 0)
					{
						int num = 1;
						try
						{
							num = int.Parse(strArrays1[1]);
						}
						catch (Exception exception)
						{
						}
						SpawnObject spawnObject = new SpawnObject(strArrays1[0], num);
						this.SpawnObjects.Add(spawnObject);
					}
				}
			}
		}

		public void LoadSpawnObjectsFromString2(string SerializedObjectList)
		{
			this.SpawnObjects.Clear();
			if (SerializedObjectList != null && SerializedObjectList.Length > 0)
			{
				string[] strArrays = SpawnObject.SplitString(SerializedObjectList, ":OBJ=");
				for (int i = 0; i < (int)strArrays.Length; i++)
				{
					string str = strArrays[i];
					string[] strArrays1 = SpawnObject.SplitString(str, ":MX=");
					if ((int)strArrays1.Length == 2 && strArrays1[0].Length > 0 && strArrays1[1].Length > 0)
					{
						string parm = SpawnObject.GetParm(str, ":MX=");
						int num = 1;
						try
						{
							num = int.Parse(parm);
						}
						catch
						{
						}
						parm = SpawnObject.GetParm(str, ":SB=");
						int num1 = 0;
						try
						{
							num1 = int.Parse(parm);
						}
						catch
						{
						}
						parm = SpawnObject.GetParm(str, ":RT=");
						double num2 = 0;
						try
						{
							num2 = double.Parse(parm);
						}
						catch
						{
						}
						parm = SpawnObject.GetParm(str, ":TO=");
						int num3 = 0;
						try
						{
							num3 = int.Parse(parm);
						}
						catch
						{
						}
						parm = SpawnObject.GetParm(str, ":KL=");
						int num4 = 0;
						try
						{
							num4 = int.Parse(parm);
						}
						catch
						{
						}
						parm = SpawnObject.GetParm(str, ":RK=");
						bool flag = false;
						if (parm != null)
						{
							try
							{
								flag = int.Parse(parm) == 1;
							}
							catch
							{
							}
						}
						parm = SpawnObject.GetParm(str, ":CA=");
						bool flag1 = true;
						if (num4 == 0)
						{
							flag1 = false;
						}
						if (parm != null)
						{
							try
							{
								flag1 = int.Parse(parm) == 1;
							}
							catch
							{
							}
						}
						parm = SpawnObject.GetParm(str, ":DN=");
						double num5 = -1;
						try
						{
							num5 = double.Parse(parm);
						}
						catch
						{
						}
						parm = SpawnObject.GetParm(str, ":DX=");
						double num6 = -1;
						try
						{
							num6 = double.Parse(parm);
						}
						catch
						{
						}
						parm = SpawnObject.GetParm(str, ":SP=");
						int num7 = 1;
						try
						{
							num7 = int.Parse(parm);
						}
						catch
						{
						}
						SpawnObject spawnObject = new SpawnObject(strArrays1[0], num, num1, num2, num3, num4, flag, flag1, num5, num6, num7);
						this.SpawnObjects.Add(spawnObject);
					}
				}
			}
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(this.SpawnName);
			stringBuilder.Append(Environment.NewLine);
			stringBuilder.Append("==============================");
			stringBuilder.Append(Environment.NewLine);
			stringBuilder.Append(this.Bounds.ToString());
			stringBuilder.Append(Environment.NewLine);
			stringBuilder.AppendFormat("Home Range: {0}", this.SpawnHomeRange);
			stringBuilder.Append(Environment.NewLine);
			stringBuilder.AppendFormat("Maximum: {0}", this.SpawnMaxCount);
			stringBuilder.Append(Environment.NewLine);
			stringBuilder.AppendFormat("Delay: {0}m - {1}m", this.SpawnMinDelay, this.SpawnMaxDelay);
			stringBuilder.Append(Environment.NewLine);
			stringBuilder.AppendFormat("Team: {0}", this.SpawnTeam);
			stringBuilder.Append(Environment.NewLine);
			stringBuilder.AppendFormat("Grouped [{0}]", (this.SpawnIsGroup ? "True" : "False"));
			stringBuilder.Append(Environment.NewLine);
			stringBuilder.AppendFormat("Running [{0}]", (this.SpawnIsRunning ? "True" : "False"));
			stringBuilder.Append(Environment.NewLine);
			stringBuilder.AppendFormat("Relative Home Range [{0}]", (this.SpawnHomeRangeIsRelative ? "True" : "False"));
			stringBuilder.Append(Environment.NewLine);
			stringBuilder.AppendFormat("Avg. Spawns per 32x32 area [{0:###.####}]", SpawnEditor.ComputeDensity(this) * 1024);
			stringBuilder.Append(Environment.NewLine);
			stringBuilder.Append("==============================");
			if (this.SpawnNotes != null && this.SpawnNotes.Length > 0)
			{
				stringBuilder.Append(Environment.NewLine);
				stringBuilder.Append(this.SpawnNotes);
				stringBuilder.Append(Environment.NewLine);
				stringBuilder.Append("==============================");
			}
			for (int i = 0; i < this.SpawnObjects.Count; i++)
			{
				SpawnObject item = this.SpawnObjects[i] as SpawnObject;
				if (item != null)
				{
					stringBuilder.Append(Environment.NewLine);
					stringBuilder.AppendFormat("{0} [Max:{1}]", item.TypeName, item.Count);
				}
			}
			return stringBuilder.ToString();
		}
	}
}