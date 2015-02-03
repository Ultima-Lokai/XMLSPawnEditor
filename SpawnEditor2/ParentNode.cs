using System;
using System.Collections;
using System.Xml;

namespace SpawnEditor2
{
	public class ParentNode
	{
		private ParentNode m_Parent;

		private object[] m_Children;

		private string m_Name;

		public object[] Children
		{
			get
			{
				return this.m_Children;
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

		public ParentNode(XmlTextReader xml, ParentNode parent)
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
			if (xml.IsEmptyElement)
			{
				this.m_Children = new object[0];
				return;
			}
			ArrayList arrayLists = new ArrayList();
			while (xml.Read() && xml.NodeType == XmlNodeType.Element)
			{
				if (xml.Name != "child")
				{
					arrayLists.Add(new ParentNode(xml, this));
				}
				else
				{
					arrayLists.Add(new ChildNode(xml, this));
				}
			}
			this.m_Children = arrayLists.ToArray();
		}
	}
}