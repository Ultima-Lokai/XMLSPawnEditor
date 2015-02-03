using System;
using System.Windows.Forms;

namespace SpawnEditor2
{
	public class RegionFacetNode : TreeNode
	{
		private WorldMap _Facet;

		public WorldMap Facet
		{
			get
			{
				return this._Facet;
			}
		}

		public RegionFacetNode(WorldMap facet)
		{
			this._Facet = facet;
			this.UpdateNode();
		}

		public void UpdateNode()
		{
			base.Text = this._Facet.ToString();
		}
	}
}