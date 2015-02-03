using System;
using System.Collections;
using System.IO;
using System.Xml;

namespace SpawnEditor2
{
	public class LocationTree
	{
		private WorldMap m_Map;

		private ParentNode m_Root;

		private Hashtable m_LastBranch;

		public Hashtable LastBranch
		{
			get
			{
				return this.m_LastBranch;
			}
		}

		public WorldMap Map
		{
			get
			{
				return this.m_Map;
			}
		}

		public ParentNode Root
		{
			get
			{
				return this.m_Root;
			}
		}

		public LocationTree(string dirname, string fileName, WorldMap map)
		{
			this.m_LastBranch = new Hashtable();
			this.m_Map = map;
			string str = Path.Combine(dirname, "Data\\Locations\\");
			string str1 = Path.Combine(str, fileName);
			if (File.Exists(str1))
			{
				XmlTextReader xmlTextReader = new XmlTextReader(new StreamReader(str1))
				{
					WhitespaceHandling = WhitespaceHandling.None
				};
				this.m_Root = this.Parse(xmlTextReader);
				xmlTextReader.Close();
			}
		}

		private ParentNode Parse(XmlTextReader xml)
		{
			xml.Read();
			xml.Read();
			xml.Read();
			return new ParentNode(xml, null);
		}
	}
}