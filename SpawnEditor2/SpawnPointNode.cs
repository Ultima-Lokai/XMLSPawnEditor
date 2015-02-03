using System;
using System.Collections;
using System.Windows.Forms;

namespace SpawnEditor2
{
	public class SpawnPointNode : TreeNode
	{
		private SpawnPoint _Spawn;

		public bool Filtered;

		public bool Highlighted;

		public SpawnPoint Spawn
		{
			get
			{
				return this._Spawn;
			}
		}

		public SpawnPointNode(SpawnPoint Spawn)
		{
			this._Spawn = Spawn;
			this.UpdateNode();
		}

		public void UpdateNode()
		{
			base.Text = this._Spawn.SpawnName;
			base.Nodes.Clear();
			foreach (SpawnObject spawnObject in this._Spawn.SpawnObjects)
			{
				base.Nodes.Add(new SpawnObjectNode(spawnObject));
			}
		}
	}
}