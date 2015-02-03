using System;
using System.Xml;

namespace SpawnEditor2
{
	public class ChildNode
	{
		private ParentNode m_Parent;

		private string m_Name;

		private MapLocation m_Location;

		public MapLocation Location
		{
			get
			{
				return this.m_Location;
			}
		}

		public string Name
		{
			get
			{
				return this.m_Name;
			}
		}

		public ParentNode Parent
		{
			get
			{
				return this.m_Parent;
			}
		}

		public ChildNode(XmlTextReader xml, ParentNode parent)
		{
			this.m_Parent = parent;
			this.Parse(xml);
		}

		private void Parse(XmlTextReader xml)
		{
			if (!xml.MoveToAttribute("name"))
			{
				this.m_Name = "empty";
			}
			else
			{
				this.m_Name = xml.Value;
			}
			int num = 0;
			int num1 = 0;
			int num2 = 0;
			if (xml.MoveToAttribute("x"))
			{
				try
				{
					num = int.Parse(xml.Value);
				}
				catch
				{
				}
			}
			if (xml.MoveToAttribute("y"))
			{
				try
				{
					num1 = int.Parse(xml.Value);
				}
				catch
				{
				}
			}
			if (xml.MoveToAttribute("z"))
			{
				try
				{
					num2 = int.Parse(xml.Value);
				}
				catch
				{
				}
			}
			this.m_Location = new MapLocation(num, num1, num2);
		}
	}
}