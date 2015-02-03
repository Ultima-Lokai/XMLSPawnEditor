using System;

namespace Server.Engines.XmlSpawner2
{
	[Serializable]
	public class ReturnSpawnerUnloadStatus : TransferMessage
	{
		public int ProcessedMaps;

		public int ProcessedSpawners;

		public ReturnSpawnerUnloadStatus(int nspawners, int nmaps)
		{
			this.ProcessedMaps = nmaps;
			this.ProcessedSpawners = nspawners;
		}

		public ReturnSpawnerUnloadStatus()
		{
		}
	}
}