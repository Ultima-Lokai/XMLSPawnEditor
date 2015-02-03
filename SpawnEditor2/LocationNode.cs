using System;
using System.Windows.Forms;

namespace SpawnEditor2
{
	public class LocationNode : TreeNode
	{
		private LocationTree _LTree;

		public LocationNode(LocationTree ltree)
		{
			this._LTree = ltree;
			this.UpdateNode();
		}

		public void UpdateNode()
		{
			base.Text = this._LTree.Map.ToString();
			base.Nodes.Clear();
			ParentNode root = this._LTree.Root;
			if (root != null && root.Children != null)
			{
				object[] children = root.Children;
				for (int i = 0; i < (int)children.Length; i++)
				{
					object obj = children[i];
					base.Nodes.Add(new LocationSubNode(obj, this._LTree.Map));
				}
			}
		}
	}
}