using System;

namespace Server.Engines.XmlSpawner2
{
	[Serializable]
	public class ReturnSpawnerSaveStatus : TransferMessage
	{
		public int ProcessedMaps;

		public int ProcessedSpawners;

		public ReturnSpawnerSaveStatus(int nspawners, int nmaps)
		{
			this.ProcessedMaps = nmaps;
			this.ProcessedSpawners = nspawners;
		}

		public ReturnSpawnerSaveStatus()
		{
		}
	}
}