using System;

namespace Server.Engines.XmlSpawner2
{
	[Serializable]
	public class GetObjectData : TransferMessage
	{
		public int SelectedMap;

		public string ObjectType;

		public int ItemID;

		public short Statics;

		public short Visible;

		public short Movable;

		public short InContainers;

		public short Carried;

		public short Blessed;

		public short Innocent;

		public short Controlled;

		public short Access;

		public short Criminal;

		public GetObjectData()
		{
		}

		public GetObjectData(int map)
		{
			this.SelectedMap = map;
		}
	}
}