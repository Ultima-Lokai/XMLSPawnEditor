using System;

namespace Server.Engines.XmlSpawner2
{
	[Serializable]
	public class UnloadSpawnerData : TransferMessage
	{
		private byte[] m_Data;

		public byte[] Data
		{
			get
			{
				return this.m_Data;
			}
			set
			{
				this.m_Data = value;
			}
		}

		public UnloadSpawnerData(byte[] data)
		{
			this.Data = data;
		}

		public UnloadSpawnerData()
		{
		}
	}
}