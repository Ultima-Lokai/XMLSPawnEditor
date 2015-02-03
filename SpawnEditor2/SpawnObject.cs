using System;
using System.Collections;

namespace SpawnEditor2
{
	public class SpawnObject
	{
		public string TypeName;

		public int Count;

		public int SubGroup;

		public double SequentialResetTime;

		public int SequentialResetTo;

		public int KillsNeeded;

		public bool RestrictKillsToSubgroup = false;

		public bool ClearOnAdvance = true;

		public double MinDelay = -1;

		public double MaxDelay = -1;

		public int SpawnsPerTick = 1;

		public SpawnObject(string name, int maxamount)
		{
			this.TypeName = name;
			this.Count = maxamount;
			this.SubGroup = 0;
			this.SequentialResetTime = 0;
			this.SequentialResetTo = 0;
			this.KillsNeeded = 0;
			this.RestrictKillsToSubgroup = false;
			this.ClearOnAdvance = true;
		}

		public SpawnObject(string name, int maxamount, int subgroup, double sequentialresettime, int sequentialresetto, int killsneeded, bool restrictkills, bool clearadvance, double mindelay, double maxdelay, int spawnsper)
		{
			this.TypeName = name;
			this.Count = maxamount;
			this.SubGroup = subgroup;
			this.SequentialResetTime = sequentialresettime;
			this.SequentialResetTo = sequentialresetto;
			this.KillsNeeded = killsneeded;
			this.RestrictKillsToSubgroup = restrictkills;
			this.ClearOnAdvance = clearadvance;
			this.MinDelay = mindelay;
			this.MaxDelay = maxdelay;
			this.SpawnsPerTick = spawnsper;
		}

		internal static string GetParm(string str, string separator)
		{
			string[] strArrays = SpawnObject.SplitString(str, separator);
			if ((int)strArrays.Length > 1)
			{
				string[] strArrays1 = strArrays[1].Split(new char[] { ':' });
				if ((int)strArrays1.Length > 0)
				{
					return strArrays1[0];
				}
			}
			return null;
		}

		public static string[] SplitString(string str, string separator)
		{
			if (str == null || separator == null)
			{
				return null;
			}
			int num = 0;
			int num1 = 0;
			ArrayList arrayLists = new ArrayList();
			while (num1 >= 0)
			{
				num1 = str.IndexOf(separator);
				if (num1 >= 0)
				{
					arrayLists.Add(str.Substring(num, num1));
					str = str.Substring(num1 + separator.Length, str.Length - (num1 + separator.Length));
				}
				else
				{
					arrayLists.Add(str);
					break;
				}
			}
			string[] item = new string[arrayLists.Count];
			for (int i = 0; i < arrayLists.Count; i++)
			{
				item[i] = (string)arrayLists[i];
			}
			return item;
		}

		public override string ToString()
		{
			return string.Concat(this.TypeName, "=", this.Count);
		}
	}
}