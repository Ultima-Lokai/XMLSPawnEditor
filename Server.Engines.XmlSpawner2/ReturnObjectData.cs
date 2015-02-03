using System;

namespace Server.Engines.XmlSpawner2
{
	[Serializable]
	public class ReturnObjectData : TransferMessage
	{
		private ObjectData[] m_Data;

		public ObjectData[] Data
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

		public ReturnObjectData(ObjectData[] data)
		{
			this.m_Data = data;
		}

		public ReturnObjectData()
		{
		}
	}
}