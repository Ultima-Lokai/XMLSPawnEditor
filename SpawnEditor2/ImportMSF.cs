using System;
using System.Collections;
using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace SpawnEditor2
{
	public class ImportMSF
	{
		private SpawnEditor _Editor;

		public ImportMSF(SpawnEditor editor)
		{
			this._Editor = editor;
		}

		public void DoImportMSF(string filePath)
		{
			if (File.Exists(filePath))
			{
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.Load(filePath);
				XmlElement item = xmlDocument["MegaSpawners"];
				if (item == null)
				{
					MessageBox.Show(this._Editor, "Invalid .msf file. No MegaSpawners node found", "Import MSF Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
				else
				{
					int num = 0;
					int num1 = 0;
					foreach (XmlElement elementsByTagName in item.GetElementsByTagName("MegaSpawner"))
					{
						try
						{
							this.ImportMegaSpawner(elementsByTagName);
							num++;
						}
						catch
						{
							num1++;
						}
					}
				}
			}
		}

		private static string GetText(XmlElement node, string defaultValue)
		{
			if (node == null)
			{
				return defaultValue;
			}
			return node.InnerText;
		}

		private void ImportMegaSpawner(XmlElement node)
		{
			string text = ImportMSF.GetText(node["Name"], "MegaSpawner");
			bool.Parse(ImportMSF.GetText(node["Active"], "True"));
			MapLocation mapLocation = MapLocation.Parse(ImportMSF.GetText(node["Location"], "Error"));
			WorldMap worldMap = (WorldMap)((int)((WorldMap)Enum.Parse(typeof(WorldMap), ImportMSF.GetText(node["Map"], "Error"))));
			string str = Path.Combine(this._Editor.StartingDirectory, "import.log");
			bool flag = false;
			int num = 0;
			int num1 = 4;
			int num2 = 4;
			TimeSpan timeSpan = TimeSpan.FromMinutes(10);
			TimeSpan timeSpan1 = TimeSpan.FromMinutes(5);
			XmlElement item = node["EntryLists"];
			int num3 = 0;
			SpawnObject[] spawnObject = null;
			if (item != null)
			{
				if (item.HasAttributes)
				{
					XmlAttributeCollection attributes = item.Attributes;
					num3 = int.Parse(attributes.GetNamedItem("count").Value);
				}
				if (num3 > 0)
				{
					spawnObject = new SpawnObject[num3];
					int num4 = 0;
					bool flag1 = false;
					foreach (XmlElement elementsByTagName in item.GetElementsByTagName("EntryList"))
					{
						if (elementsByTagName == null)
						{
							continue;
						}
						if (num4 != 0)
						{
							if (flag != bool.Parse(ImportMSF.GetText(elementsByTagName["GroupSpawn"], "False")))
							{
								flag1 = true;
								try
								{
									using (StreamWriter streamWriter = new StreamWriter(str, true))
									{
										streamWriter.WriteLine("MSFimport : individual group entry difference: {0} vs {1}", ImportMSF.GetText(elementsByTagName["GroupSpawn"], "False"), flag);
									}
								}
								catch
								{
								}
							}
							if (timeSpan1 != TimeSpan.FromSeconds((double)int.Parse(ImportMSF.GetText(elementsByTagName["MinDelay"], "05:00"))))
							{
								flag1 = true;
								try
								{
									using (StreamWriter streamWriter1 = new StreamWriter(str, true))
									{
										streamWriter1.WriteLine("MSFimport : individual mindelay entry difference: {0} vs {1}", ImportMSF.GetText(elementsByTagName["MinDelay"], "05:00"), timeSpan1);
									}
								}
								catch
								{
								}
							}
							if (timeSpan != TimeSpan.FromSeconds((double)int.Parse(ImportMSF.GetText(elementsByTagName["MaxDelay"], "10:00"))))
							{
								flag1 = true;
								try
								{
									using (StreamWriter streamWriter2 = new StreamWriter(str, true))
									{
										streamWriter2.WriteLine("MSFimport : individual maxdelay entry difference: {0} vs {1}", ImportMSF.GetText(elementsByTagName["MaxDelay"], "10:00"), timeSpan);
									}
								}
								catch
								{
								}
							}
							if (num1 != int.Parse(ImportMSF.GetText(elementsByTagName["WalkRange"], "10")))
							{
								flag1 = true;
								try
								{
									using (StreamWriter streamWriter3 = new StreamWriter(str, true))
									{
										streamWriter3.WriteLine("MSFimport : individual homerange entry difference: {0} vs {1}", ImportMSF.GetText(elementsByTagName["WalkRange"], "10"), num1);
									}
								}
								catch
								{
								}
							}
							if (num2 != int.Parse(ImportMSF.GetText(elementsByTagName["SpawnRange"], "4")))
							{
								flag1 = true;
								try
								{
									using (StreamWriter streamWriter4 = new StreamWriter(str, true))
									{
										streamWriter4.WriteLine("MSFimport : individual spawnrange entry difference: {0} vs {1}", ImportMSF.GetText(elementsByTagName["SpawnRange"], "4"), num2);
									}
								}
								catch
								{
								}
							}
						}
						else
						{
							flag = bool.Parse(ImportMSF.GetText(elementsByTagName["GroupSpawn"], "False"));
							timeSpan = TimeSpan.FromSeconds((double)int.Parse(ImportMSF.GetText(elementsByTagName["MaxDelay"], "10:00")));
							timeSpan1 = TimeSpan.FromSeconds((double)int.Parse(ImportMSF.GetText(elementsByTagName["MinDelay"], "05:00")));
							num1 = int.Parse(ImportMSF.GetText(elementsByTagName["WalkRange"], "10"));
							num2 = int.Parse(ImportMSF.GetText(elementsByTagName["SpawnRange"], "4"));
						}
						int num5 = int.Parse(ImportMSF.GetText(elementsByTagName["Amount"], "1"));
						string text1 = ImportMSF.GetText(elementsByTagName["EntryType"], "");
						num = num + num5;
						spawnObject[num4] = new SpawnObject(text1, num5);
						num4++;
						if (num4 <= num3)
						{
							continue;
						}
						try
						{
							using (StreamWriter streamWriter5 = new StreamWriter(str, true))
							{
								streamWriter5.WriteLine("{0} MSFImport Error; inconsistent entry count {1} {2}", DateTime.Now, mapLocation, worldMap);
								streamWriter5.WriteLine();
							}
							break;
						}
						catch
						{
							break;
						}
					}
					if (flag1)
					{
						try
						{
							using (StreamWriter streamWriter6 = new StreamWriter(str, true))
							{
								streamWriter6.WriteLine("{0} MSFImport: Individual entry setting differences listed above from spawner at {1} {2}", DateTime.Now, mapLocation, worldMap);
								streamWriter6.WriteLine();
							}
						}
						catch
						{
						}
					}
				}
			}
			if (mapLocation.Z == -999)
			{
				mapLocation.Z = -32768;
			}
			Guid guid = Guid.NewGuid();
			SpawnPoint spawnPoint = new SpawnPoint(guid, worldMap, (short)mapLocation.X, (short)mapLocation.Y, (short)(num2 * 2), (short)(num2 * 2))
			{
				SpawnName = text,
				SpawnHomeRange = num1,
				CentreZ = (short)mapLocation.Z,
				SpawnMinDelay = timeSpan1.TotalMinutes,
				SpawnMaxDelay = timeSpan.TotalMinutes,
				SpawnMaxCount = num,
				SpawnIsGroup = flag,
				IsSelected = false
			};
			for (int i = 0; i < (int)spawnObject.Length; i++)
			{
				spawnPoint.SpawnObjects.Add(spawnObject[i]);
			}
			SpawnPointNode spawnPointNode = new SpawnPointNode(spawnPoint);
			this._Editor.tvwSpawnPoints.Nodes.Add(spawnPointNode);
		}
	}
}