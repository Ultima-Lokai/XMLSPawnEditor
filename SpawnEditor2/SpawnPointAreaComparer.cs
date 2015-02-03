using System;
using System.Collections;

namespace SpawnEditor2
{
	public class SpawnPointAreaComparer : IComparer
	{
		public SpawnPointAreaComparer()
		{
		}

		public int Compare(object A, object B)
		{
			if (!(A is SpawnPointNode) || !(B is SpawnPointNode))
			{
				return 0;
			}
			SpawnPoint spawn = ((SpawnPointNode)A).Spawn;
			SpawnPoint spawnPoint = ((SpawnPointNode)B).Spawn;
			return spawn.Area - spawnPoint.Area;
		}
	}
}