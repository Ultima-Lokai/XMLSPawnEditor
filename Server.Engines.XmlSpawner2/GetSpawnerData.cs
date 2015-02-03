using System;

namespace Server.Engines.XmlSpawner2
{
	[Serializable]
	public class GetSpawnerData : TransferMessage
	{
		private int m_Map;

		public int X;

		public int Y;

		public int Width;

		public int Height;

		public string NameFilter;

		public string EntryFilter;

		public short ContainerFilter;

		public short SequentialFilter;

		public short SmartSpawnFilter;

		public bool NameCase;

		public bool EntryCase;

		public short Modified;

		public short Proximity;

		public short Running;

		public DateTime ModifiedDate;

		public short SpawnTime;

		public double AvgSpawnTime;

		public string ModifiedName;

		public short ModifiedBy;

		public int SelectedMap
		{
			get
			{
				return this.m_Map;
			}
			set
			{
				this.m_Map = value;
			}
		}

		public GetSpawnerData()
		{
		}

		public GetSpawnerData(int map)
		{
			this.m_Map = map;
		}

		public GetSpawnerData(int map, int x, int y, int w, int h)
		{
			this.m_Map = map;
			this.X = x;
			this.Y = y;
			this.Width = w;
			this.Height = h;
		}
	}
}