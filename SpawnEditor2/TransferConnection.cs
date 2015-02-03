using Server.Engines.XmlSpawner2;
using System;
using System.Reflection;
using System.Windows.Forms;

namespace SpawnEditor2
{
	public class TransferConnection
	{
		private static RemoteMessaging m_Remote;

		static TransferConnection()
		{
			TransferConnection.m_Remote = null;
		}

		public TransferConnection()
		{
		}

		public static bool HasErrors(TransferMessage msg, Type t, string rtype)
		{
			if (msg == null)
			{
				MessageBox.Show(string.Format("No Message Data Received from Remote Server for {0} ({1})", t, rtype));
				return true;
			}
			if (!(msg is ErrorMessage))
			{
				return false;
			}
			MessageBox.Show((msg as ErrorMessage).Message);
			return true;
		}

		public static TransferMessage ProcessMessage(string Address, int Port, TransferMessage msg)
		{
			TransferMessage transferMessage;
			TransferMessage transferMessage1 = null;
			byte[] numArray = msg.Compress();
			string str = null;
			string str1 = string.Format("tcp://{0}:{1}/RemoteMessaging", Address, Port);
			try
			{
				TransferConnection.m_Remote = Activator.GetObject(typeof(RemoteMessaging), str1) as RemoteMessaging;
			}
			catch
			{
				MessageBox.Show(string.Format("Failed to connect to remote server {0} : {1}", Address, Port));
				transferMessage = null;
				return transferMessage;
			}
			try
			{
				byte[] numArray1 = TransferConnection.m_Remote.PerformRemoteRequest(msg.GetType().FullName, numArray, out str);
				if (numArray1 != null)
				{
					Type type = Type.GetType(str);
					if (type == null)
					{
						Assembly assembly = Assembly.GetAssembly(typeof(TransferMessage));
						if (assembly != null)
						{
							type = assembly.GetType(str);
						}
					}
					transferMessage1 = TransferMessage.Decompress(numArray1, type);
					if (TransferConnection.HasErrors(transferMessage1, type, str))
					{
						transferMessage1 = null;
					}
				}
				else
				{
					MessageBox.Show(string.Concat("No Data Received from Remote Server for ", msg.GetType().FullName));
					transferMessage = null;
					return transferMessage;
				}
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message);
				transferMessage1 = null;
			}
			return transferMessage1;
		}
	}
}