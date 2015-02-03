using System;
using System.Windows.Forms;

namespace SpawnEditor2
{
	public class SpawnPackSubNode : TreeNode
	{
		private string _Item;

		public SpawnPackSubNode(string item)
		{
			this._Item = item;
			base.Text = item;
		}
	}
}