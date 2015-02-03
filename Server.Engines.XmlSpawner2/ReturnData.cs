using System;

namespace Server.Engines.XmlSpawner2
{
	[Serializable]
	public class ReturnData : TransferMessage
	{
		private object m_Data;

		private string m_Typename;

		public object Data
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

		public string Typename
		{
			get
			{
				return this.m_Typename;
			}
			set
			{
				this.m_Typename = value;
			}
		}

		public ReturnData(object data, string type)
		{
			this.m_Data = data;
			this.m_Typename = type;
		}

		public ReturnData(object data)
		{
			this.m_Data = data;
		}

		public ReturnData()
		{
		}
	}
}