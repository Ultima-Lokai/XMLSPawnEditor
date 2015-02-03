using System;

namespace Server.Engines.XmlSpawner2
{
	[Serializable]
	public class ObjectData
	{
		public int X;

		public int Y;

		public int Map;

		public string Name;

		public ObjectData(int x, int y, int map, string name)
		{
			this.X = x;
			this.Y = y;
			this.Map = map;
			this.Name = name;
		}

		public ObjectData()
		{
		}

		public override string ToString()
		{
			return this.Name;
		}
	}
}