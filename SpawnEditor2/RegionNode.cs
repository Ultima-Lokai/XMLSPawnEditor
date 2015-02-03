using System;
using System.Windows.Forms;

namespace SpawnEditor2
{
	public class RegionNode : TreeNode
	{
		private SpawnEditor2.Region _Region;

		public SpawnEditor2.Region Region
		{
			get
			{
				return this._Region;
			}
		}

		public RegionNode(SpawnEditor2.Region region)
		{
			this._Region = region;
			this.UpdateNode();
		}

		public void UpdateNode()
		{
			base.Text = this._Region.Name;
		}
	}
}