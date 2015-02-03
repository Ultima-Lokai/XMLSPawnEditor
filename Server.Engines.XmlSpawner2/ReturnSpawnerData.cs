using System;

namespace Server.Engines.XmlSpawner2
{
	[Serializable]
	public class ReturnSpawnerData : TransferMessage
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

		public ReturnSpawnerData(byte[] stream)
		{
			this.m_Data = stream;
		}

		public ReturnSpawnerData()
		{
		}
	}
}