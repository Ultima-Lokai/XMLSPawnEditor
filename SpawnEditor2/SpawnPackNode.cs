using System;
using System.Collections;
using System.Windows.Forms;

namespace SpawnEditor2
{
	public class SpawnPackNode : TreeNode
	{
		private string _packName;

		public string PackName
		{
			get
			{
				return this._packName;
			}
			set
			{
				this._packName = value;
				base.Text = this._packName;
			}
		}

		public SpawnPackNode(string packName, CheckedListBox.ObjectCollection items)
		{
			this._packName = packName;
			this.UpdateNode(items);
		}

		public SpawnPackNode(string packName, ArrayList items)
		{
			this._packName = packName;
			this.UpdateNode(items);
		}

		public void UpdateNode(CheckedListBox.ObjectCollection items)
		{
			base.Text = this._packName;
			base.Nodes.Clear();
			if (items != null && items.Count > 0)
			{
				for (int i = 0; i < items.Count; i++)
				{
					base.Nodes.Add(new SpawnPackSubNode((string)items[i]));
				}
			}
		}

		public void UpdateNode(ArrayList items)
		{
			base.Text = this._packName;
			base.Nodes.Clear();
			if (items != null && items.Count > 0)
			{
				for (int i = 0; i < items.Count; i++)
				{
					base.Nodes.Add(new SpawnPackSubNode((string)items[i]));
				}
			}
		}
	}
}