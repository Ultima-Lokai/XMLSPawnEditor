using System;
using System.Windows.Forms;

namespace SpawnEditor2
{
	public class SpawnObjectNode : TreeNode
	{
		private SpawnEditor2.SpawnObject _Object;

		public SpawnEditor2.SpawnObject SpawnObject
		{
			get
			{
				return this._Object;
			}
		}

		public SpawnObjectNode(SpawnEditor2.SpawnObject SpawnObject)
		{
			this._Object = SpawnObject;
			this.UpdateNode();
		}

		public void UpdateNode()
		{
			base.Text = this._Object.ToString();
		}
	}
}