using System;
using System.Windows.Forms;

namespace SpawnEditor2
{
	public class LocationSubNode : TreeNode
	{
		private object _Node;

		private WorldMap _Map;

		public WorldMap Map
		{
			get
			{
				return this._Map;
			}
		}

		public object Node
		{
			get
			{
				return this._Node;
			}
		}

		public LocationSubNode(object node, WorldMap map)
		{
			this._Node = node;
			this._Map = map;
			this.UpdateNode();
		}

		public void UpdateNode()
		{
			base.Nodes.Clear();
			if (this._Node is ChildNode)
			{
				base.Text = (this._Node as ChildNode).Name;
				return;
			}
			if (this._Node is ParentNode)
			{
				ParentNode parentNode = this._Node as ParentNode;
				base.Text = parentNode.Name;
				if (parentNode.Children != null)
				{
					object[] children = parentNode.Children;
					for (int i = 0; i < (int)children.Length; i++)
					{
						object obj = children[i];
						base.Nodes.Add(new LocationSubNode(obj, this.Map));
					}
				}
			}
		}
	}
}