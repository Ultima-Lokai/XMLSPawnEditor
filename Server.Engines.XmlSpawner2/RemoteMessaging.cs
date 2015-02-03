using System;

namespace Server.Engines.XmlSpawner2
{
	public class RemoteMessaging : MarshalByRefObject
	{
		private static int n_instances;

		public RemoteMessaging()
		{
			RemoteMessaging.n_instances = RemoteMessaging.n_instances + 1;
		}

        /* This was originally entered as protected override void Finalize()
         * It would not compile, saying not to override Object.Finalize, to use a desctructor instead
         * I took a shot at making it its own virtual, and now apears to be clean by the compiler.
         * -Dian
         *  */
		protected virtual void Finalize()
		{
			try
			{
				RemoteMessaging.n_instances = RemoteMessaging.n_instances - 1;
			}
			finally
			{
				Finalize();
			}
		}

		public byte[] PerformRemoteRequest(string typeName, byte[] data, out string answerType)
		{
			answerType = null;
			if (RemoteMessaging.OnReceiveMessage == null)
			{
				return null;
			}
			return RemoteMessaging.OnReceiveMessage(typeName, data, out answerType);
		}

		public static event RemoteMessaging.Message OnReceiveMessage;

		public delegate byte[] Message(string typeName, byte[] data, out string answerType);
	}
}