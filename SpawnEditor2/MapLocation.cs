using System;

namespace SpawnEditor2
{
	public class MapLocation
	{
		public int X;

		public int Y;

		public int Z;

		public int Facet = -1;

		public readonly static MapLocation Zero;

		static MapLocation()
		{
			MapLocation.Zero = new MapLocation(0, 0, 0);
		}

		public MapLocation(int x, int y, int z)
		{
			this.X = x;
			this.Y = y;
			this.Z = z;
		}

		public MapLocation()
		{
		}

		public static MapLocation Parse(string value)
		{
			int num = value.IndexOf('(');
			int num1 = value.IndexOf(',', num + 1);
			string str = value.Substring(num + 1, num1 - (num + 1)).Trim();
			num = num1;
			num1 = value.IndexOf(',', num + 1);
			string str1 = value.Substring(num + 1, num1 - (num + 1)).Trim();
			num = num1;
			num1 = value.IndexOf(')', num + 1);
			string str2 = value.Substring(num + 1, num1 - (num + 1)).Trim();
			return new MapLocation(Convert.ToInt32(str), Convert.ToInt32(str1), Convert.ToInt32(str2));
		}

		public override string ToString()
		{
			return string.Format("({0}, {1}, {2})", this.X, this.Y, this.Z);
		}
	}
}