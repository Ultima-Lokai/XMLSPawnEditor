using System;

namespace Server.Engines.XmlSpawner2
{
	[Serializable]
	public class SaveSpawnerData : TransferMessage
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

		public SaveSpawnerData(byte[] data)
		{
			this.Data = data;
		}

		public SaveSpawnerData()
		{
		}
	}
}