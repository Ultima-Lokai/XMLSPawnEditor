using System;

namespace Server.Engines.XmlSpawner2
{
	[Serializable]
	public class ErrorMessage : TransferMessage
	{
		private string m_Message;

		public string Message
		{
			get
			{
				return this.m_Message;
			}
			set
			{
				this.m_Message = value;
			}
		}

		public ErrorMessage(string message)
		{
			this.m_Message = message;
		}

		public ErrorMessage(string message, params string[] args)
		{
			this.m_Message = string.Format(message, args);
		}

		public ErrorMessage()
		{
		}
	}
}