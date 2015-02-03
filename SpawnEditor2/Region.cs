using System;
using System.Collections;
using System.Drawing;
using System.IO;
using System.Xml;

namespace SpawnEditor2
{
	public class Region : IComparable
	{
		public const int LowestPriority = 0;

		public const int HighestPriority = 150;

		public const int TownPriority = 50;

		public const int HousePriority = 150;

		public const int InnPriority = 51;

		private int m_Priority;

		private ArrayList m_Coords;

		private ArrayList m_InnBounds;

		private WorldMap m_Map;

		private string m_Name;

		private string m_Prefix;

		private MapLocation m_GoLoc;

		private int m_UId;

		private bool m_Load;

		private MusicName m_Music = MusicName.Invalid;

		private ArrayList m_LoadCoords;

		private int m_MinZ = -32768;

		private int m_MaxZ = 32767;

		private static int m_RegionUID;

		private static bool m_SupressXmlWarnings;

		public static SpawnEditor Editor;

		private static ArrayList m_Regions;

		public ArrayList Coords
		{
			get
			{
				return this.m_Coords;
			}
			set
			{
				if (this.m_Coords != value)
				{
					SpawnEditor2.Region.RemoveRegion(this);
					this.m_Coords = value;
					SpawnEditor2.Region.AddRegion(this);
				}
			}
		}

		public MapLocation GoLocation
		{
			get
			{
				return this.m_GoLoc;
			}
			set
			{
				this.m_GoLoc = value;
			}
		}

		public ArrayList InnBounds
		{
			get
			{
				return this.m_InnBounds;
			}
			set
			{
				this.m_InnBounds = value;
			}
		}

		public bool LoadFromXml
		{
			get
			{
				return this.m_Load;
			}
			set
			{
				this.m_Load = value;
			}
		}

		public WorldMap Map
		{
			get
			{
				return this.m_Map;
			}
			set
			{
				SpawnEditor2.Region.RemoveRegion(this);
				this.m_Map = value;
				SpawnEditor2.Region.AddRegion(this);
			}
		}

		public int MaxZ
		{
			get
			{
				return this.m_MaxZ;
			}
			set
			{
				SpawnEditor2.Region.RemoveRegion(this);
				this.m_MaxZ = value;
				SpawnEditor2.Region.AddRegion(this);
			}
		}

		public int MinZ
		{
			get
			{
				return this.m_MinZ;
			}
			set
			{
				SpawnEditor2.Region.RemoveRegion(this);
				this.m_MinZ = value;
				SpawnEditor2.Region.AddRegion(this);
			}
		}

		public MusicName Music
		{
			get
			{
				return this.m_Music;
			}
			set
			{
				this.m_Music = value;
			}
		}

		public string Name
		{
			get
			{
				return this.m_Name;
			}
			set
			{
				this.m_Name = value;
			}
		}

		public string Prefix
		{
			get
			{
				return this.m_Prefix;
			}
			set
			{
				this.m_Prefix = value;
			}
		}

		public int Priority
		{
			get
			{
				return this.m_Priority;
			}
			set
			{
				if (value != this.m_Priority)
				{
					this.m_Priority = value;
				}
			}
		}

		public static ArrayList Regions
		{
			get
			{
				return SpawnEditor2.Region.m_Regions;
			}
		}

		public static bool SupressXmlWarnings
		{
			get
			{
				return SpawnEditor2.Region.m_SupressXmlWarnings;
			}
			set
			{
				SpawnEditor2.Region.m_SupressXmlWarnings = value;
			}
		}

		public int UId
		{
			get
			{
				return this.m_UId;
			}
		}

		static Region()
		{
			SpawnEditor2.Region.m_RegionUID = 1;
			SpawnEditor2.Region.m_SupressXmlWarnings = true;
			SpawnEditor2.Region.m_Regions = new ArrayList();
		}

		public Region(string prefix, string name, WorldMap map, int uid) : this(prefix, name, map)
		{
			this.m_UId = uid | 1073741824;
		}

		public Region(string prefix, string name, WorldMap map)
		{
			this.m_Prefix = prefix;
			this.m_Name = name;
			this.m_Map = map;
			this.m_Priority = 0;
			this.m_GoLoc = MapLocation.Zero;
			this.m_Load = true;
			int mRegionUID = SpawnEditor2.Region.m_RegionUID;
			SpawnEditor2.Region.m_RegionUID = mRegionUID + 1;
			this.m_UId = mRegionUID;
		}

		public static void AddRegion(SpawnEditor2.Region region)
		{
			SpawnEditor2.Region.m_Regions.Add(region);
		}

		public int CompareTo(object o)
		{
			if (!(o is SpawnEditor2.Region))
			{
				return 0;
			}
			int mPriority = ((SpawnEditor2.Region)o).m_Priority;
			int num = this.m_Priority;
			if (mPriority < num)
			{
				return -1;
			}
			if (mPriority > num)
			{
				return 1;
			}
			return 0;
		}

		public virtual bool Contains(MapLocation p)
		{
			if (this.m_Coords == null)
			{
				return false;
			}
			for (int i = 0; i < this.m_Coords.Count; i++)
			{
				object item = this.m_Coords[i];
				if (item is Rectangle && ((Rectangle)item).Contains(p.X, p.Y))
				{
					return true;
				}
			}
			return false;
		}

		public override bool Equals(object o)
		{
			if (!(o is SpawnEditor2.Region) || o == null)
			{
				return false;
			}
			return (SpawnEditor2.Region)o == this;
		}

		public static SpawnEditor2.Region Find(MapLocation p, WorldMap map)
		{
			if (map == WorldMap.Internal)
			{
				return null;
			}
			for (int i = 0; i < SpawnEditor2.Region.m_Regions.Count; i++)
			{
				SpawnEditor2.Region item = (SpawnEditor2.Region)SpawnEditor2.Region.m_Regions[i];
				if (item.Map == map && item.Contains(p))
				{
					return item;
				}
			}
			return null;
		}

		public static SpawnEditor2.Region FindByUId(int uid)
		{
			for (int i = 0; i < SpawnEditor2.Region.m_Regions.Count; i++)
			{
				SpawnEditor2.Region item = (SpawnEditor2.Region)SpawnEditor2.Region.m_Regions[i];
				if (item.UId == uid)
				{
					return item;
				}
			}
			return null;
		}

		public static SpawnEditor2.Region GetByName(string name, WorldMap map)
		{
			for (int i = 0; i < SpawnEditor2.Region.m_Regions.Count; i++)
			{
				SpawnEditor2.Region item = (SpawnEditor2.Region)SpawnEditor2.Region.m_Regions[i];
				if (item.Map == map && item.Name == name)
				{
					return item;
				}
			}
			return null;
		}

		public override int GetHashCode()
		{
			return this.m_UId;
		}

		public static bool IsNull(SpawnEditor2.Region r)
		{
			return object.ReferenceEquals(r, null);
		}

		public static void Load(string basedir)
		{
			string str = Path.Combine(basedir, "Data\\Regions.xml");
			if (!File.Exists(str))
			{
				return;
			}
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.Load(str);
			foreach (XmlElement elementsByTagName in xmlDocument["ServerRegions"].GetElementsByTagName("Facet"))
			{
				string attribute = elementsByTagName.GetAttribute("name");
				WorldMap worldMap = WorldMap.Internal;
				try
				{
					worldMap = (WorldMap)((int)((WorldMap)Enum.Parse(typeof(WorldMap), attribute, true)));
				}
				catch
				{
				}
				if (worldMap != WorldMap.Internal)
				{
					foreach (XmlElement xmlElement in elementsByTagName.GetElementsByTagName("region"))
					{
						string attribute1 = xmlElement.GetAttribute("name");
						if (attribute1 == null || attribute1.Length <= 0)
						{
							continue;
						}
						SpawnEditor2.Region region = new SpawnEditor2.Region("", attribute1, worldMap);
						SpawnEditor2.Region.AddRegion(region);
						try
						{
							region.Priority = int.Parse(xmlElement.GetAttribute("priority"));
						}
						catch
						{
							if (!SpawnEditor2.Region.m_SupressXmlWarnings)
							{
								Console.WriteLine("Regions.xml: Could not parse priority for region '{0}' (assuming TownPriority)", region.Name);
							}
							region.Priority = 50;
						}
						XmlElement item = xmlElement["go"];
						if (item != null)
						{
							try
							{
								region.GoLocation = MapLocation.Parse(item.GetAttribute("location"));
								region.GoLocation.Facet = (int)worldMap;
							}
							catch
							{
								if (!SpawnEditor2.Region.m_SupressXmlWarnings)
								{
									Console.WriteLine("Regions.xml: Could not parse go location for region '{0}'", region.Name);
								}
							}
						}
						item = xmlElement["music"];
						if (item != null)
						{
							try
							{
								region.Music = (MusicName)((int)((MusicName)Enum.Parse(typeof(MusicName), item.GetAttribute("name"), true)));
							}
							catch
							{
								if (!SpawnEditor2.Region.m_SupressXmlWarnings)
								{
									Console.WriteLine("Regions.xml: Could not parse music for region '{0}'", region.Name);
								}
							}
						}
						item = xmlElement["zrange"];
						if (item != null)
						{
							string str1 = item.GetAttribute("min");
							if (str1 != null && str1 != "")
							{
								try
								{
									region.MinZ = int.Parse(str1);
								}
								catch
								{
									if (!SpawnEditor2.Region.m_SupressXmlWarnings)
									{
										Console.WriteLine("Regions.xml: Could not parse zrange:min for region '{0}'", region.Name);
									}
								}
							}
							str1 = item.GetAttribute("max");
							if (str1 != null && str1 != "")
							{
								try
								{
									region.MaxZ = int.Parse(str1);
								}
								catch
								{
									if (!SpawnEditor2.Region.m_SupressXmlWarnings)
									{
										Console.WriteLine("Regions.xml: Could not parse zrange:max for region '{0}'", region.Name);
									}
								}
							}
						}
						foreach (XmlElement elementsByTagName1 in xmlElement.GetElementsByTagName("rect"))
						{
							try
							{
								if (region.m_LoadCoords == null)
								{
									region.m_LoadCoords = new ArrayList(1);
								}
								region.m_LoadCoords.Add(SpawnEditor2.Region.ParseRectangle(elementsByTagName1, true));
							}
							catch
							{
								if (!SpawnEditor2.Region.m_SupressXmlWarnings)
								{
									Console.WriteLine("Regions.xml: Error parsing rect for region '{0}'", region.Name);
								}
							}
						}
						foreach (XmlElement xmlElement1 in xmlElement.GetElementsByTagName("inn"))
						{
							try
							{
								if (region.InnBounds == null)
								{
									region.InnBounds = new ArrayList(1);
								}
								region.InnBounds.Add(SpawnEditor2.Region.ParseRectangle(xmlElement1, false));
							}
							catch
							{
								if (!SpawnEditor2.Region.m_SupressXmlWarnings)
								{
									Console.WriteLine("Regions.xml: Error parsing inn for region '{0}'", region.Name);
								}
							}
						}
					}
				}
				else
				{
					if (SpawnEditor2.Region.m_SupressXmlWarnings)
					{
						continue;
					}
					Console.WriteLine("Regions.xml: Invalid facet name '{0}'", attribute);
				}
			}
			ArrayList arrayLists = new ArrayList(SpawnEditor2.Region.m_Regions);
			for (int i = 0; i < arrayLists.Count; i++)
			{
				SpawnEditor2.Region mLoadCoords = (SpawnEditor2.Region)arrayLists[i];
				if (!mLoadCoords.LoadFromXml && mLoadCoords.m_Coords == null)
				{
					mLoadCoords.Coords = new ArrayList();
				}
				else if (mLoadCoords.LoadFromXml)
				{
					if (mLoadCoords.m_LoadCoords == null)
					{
						mLoadCoords.m_LoadCoords = new ArrayList();
					}
					mLoadCoords.Coords = mLoadCoords.m_LoadCoords;
				}
			}
		}

		public static bool operator ==(SpawnEditor2.Region l, SpawnEditor2.Region r)
		{
			if (SpawnEditor2.Region.IsNull(l))
			{
				return SpawnEditor2.Region.IsNull(r);
			}
			if (SpawnEditor2.Region.IsNull(r))
			{
				return false;
			}
			return l.UId == r.UId;
		}

		public static bool operator >(SpawnEditor2.Region l, SpawnEditor2.Region r)
		{
			if (SpawnEditor2.Region.IsNull(l) && SpawnEditor2.Region.IsNull(r))
			{
				return false;
			}
			if (SpawnEditor2.Region.IsNull(l))
			{
				return false;
			}
			if (SpawnEditor2.Region.IsNull(r))
			{
				return true;
			}
			return l.Priority < r.Priority;
		}

		public static bool operator !=(SpawnEditor2.Region l, SpawnEditor2.Region r)
		{
			if (SpawnEditor2.Region.IsNull(l))
			{
				return !SpawnEditor2.Region.IsNull(r);
			}
			if (SpawnEditor2.Region.IsNull(r))
			{
				return true;
			}
			return l.UId != r.UId;
		}

		public static bool operator <(SpawnEditor2.Region l, SpawnEditor2.Region r)
		{
			if (SpawnEditor2.Region.IsNull(l) && SpawnEditor2.Region.IsNull(r))
			{
				return false;
			}
			if (SpawnEditor2.Region.IsNull(l))
			{
				return true;
			}
			if (SpawnEditor2.Region.IsNull(r))
			{
				return false;
			}
			return l.Priority > r.Priority;
		}

		public static object ParseRectangle(XmlElement rect, bool supports3d)
		{
			int num;
			int num1;
			int num2;
			int num3;
			if (!rect.HasAttribute("x") || !rect.HasAttribute("y") || !rect.HasAttribute("width") || !rect.HasAttribute("height"))
			{
				if (!rect.HasAttribute("x1") || !rect.HasAttribute("y1") || !rect.HasAttribute("x2") || !rect.HasAttribute("y2"))
				{
					throw new ArgumentException("Wrong attributes specified.");
				}
				num = int.Parse(rect.GetAttribute("x1"));
				num1 = int.Parse(rect.GetAttribute("y1"));
				num2 = int.Parse(rect.GetAttribute("x2"));
				num3 = int.Parse(rect.GetAttribute("y2"));
			}
			else
			{
				num = int.Parse(rect.GetAttribute("x"));
				num1 = int.Parse(rect.GetAttribute("y"));
				num2 = num + int.Parse(rect.GetAttribute("width"));
				num3 = num1 + int.Parse(rect.GetAttribute("height"));
			}
			return new Rectangle(num, num1, num2 - num, num3 - num1);
		}

		public static void RemoveRegion(SpawnEditor2.Region region)
		{
			SpawnEditor2.Region.m_Regions.Remove(region);
		}

		public override string ToString()
		{
			if (this.Prefix == "")
			{
				return this.Name;
			}
			return string.Format("{0} {1}", this.Prefix, this.Name);
		}
	}
}