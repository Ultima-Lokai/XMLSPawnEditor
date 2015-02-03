using System;
using System.Collections;
using System.IO;
using System.Windows.Forms;

namespace SpawnEditor2
{
	public class ImportMap
	{
		private SpawnEditor _Editor;

		public ImportMap(SpawnEditor editor)
		{
			this._Editor = editor;
		}

		public void DoImportMap(string filename, out int processedmaps, out int processedspawners)
		{
			int i;
			string[] strArrays;
			processedmaps = 0;
			processedspawners = 0;
			int num = 0;
			int num1 = 0;
			if (filename == null || filename.Length <= 0)
			{
				return;
			}
			if (!File.Exists(filename))
			{
				if (Directory.Exists(filename))
				{
					string[] files = null;
					try
					{
						files = Directory.GetFiles(filename, "*.map");
					}
					catch
					{
					}
					if (files != null && (int)files.Length > 0)
					{
						strArrays = files;
						for (i = 0; i < (int)strArrays.Length; i++)
						{
							this.DoImportMap(strArrays[i], out processedmaps, out processedspawners);
							num = num + processedmaps;
							num1 = num1 + processedspawners;
						}
					}
					string[] directories = null;
					try
					{
						directories = Directory.GetDirectories(filename);
					}
					catch
					{
					}
					if (directories != null && (int)directories.Length > 0)
					{
						strArrays = directories;
						for (i = 0; i < (int)strArrays.Length; i++)
						{
							this.DoImportMap(strArrays[i], out processedmaps, out processedspawners);
							num = num + processedmaps;
							num1 = num1 + processedspawners;
						}
					}
					processedmaps = num;
					processedspawners = num1;
				}
				return;
			}
			string fileName = Path.GetFileName(filename);
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			int num5 = -1;
			try
			{
				using (StreamReader streamReader = new StreamReader(filename))
				{
					while (true)
					{
						string str = streamReader.ReadLine();
						string str1 = str;
						if (str == null)
						{
							break;
						}
						num4++;
						string str2 = str1.Trim();
						char[] chrArray = new char[] { ' ' };
						string[] strArrays1 = str2.Split(chrArray);
						if ((int)strArrays1.Length == 2 && strArrays1[0].ToLower() == "overridemap")
						{
							try
							{
								num5 = int.Parse(strArrays1[1]);
							}
							catch
							{
							}
						}
						if ((int)strArrays1.Length > 0 && strArrays1[0] == "*")
						{
							bool flag = false;
							int num6 = 0;
							int num7 = 0;
							int num8 = 0;
							int num9 = 0;
							int num10 = 0;
							int num11 = 0;
							int num12 = 0;
							int num13 = 0;
							int num14 = 0;
							string[] strArrays2 = null;
							if ((int)strArrays1.Length == 11 || (int)strArrays1.Length == 12)
							{
								string str3 = strArrays1[1];
								chrArray = new char[] { ':' };
								strArrays2 = str3.Split(chrArray);
								if ((int)strArrays1.Length == 11)
								{
									try
									{
										num6 = int.Parse(strArrays1[2]);
										num7 = int.Parse(strArrays1[3]);
										num8 = int.Parse(strArrays1[4]);
										num9 = int.Parse(strArrays1[5]);
										num10 = int.Parse(strArrays1[6]);
										num11 = int.Parse(strArrays1[7]);
										num12 = int.Parse(strArrays1[8]);
										num13 = int.Parse(strArrays1[9]);
										num14 = int.Parse(strArrays1[10]);
									}
									catch
									{
										flag = true;
									}
								}
								else if ((int)strArrays1.Length == 12)
								{
									try
									{
										num6 = int.Parse(strArrays1[2]);
										num7 = int.Parse(strArrays1[3]);
										num8 = int.Parse(strArrays1[4]);
										num9 = int.Parse(strArrays1[5]);
										num10 = int.Parse(strArrays1[6]);
										num11 = int.Parse(strArrays1[7]);
										num12 = int.Parse(strArrays1[8]);
										num13 = int.Parse(strArrays1[9]);
										int.Parse(strArrays1[10]);
										num14 = int.Parse(strArrays1[11]);
									}
									catch
									{
										flag = true;
									}
								}
							}
							else
							{
								flag = true;
							}
							if (flag || strArrays2 == null || (int)strArrays2.Length <= 0)
							{
								num3++;
							}
							else
							{
								if (num5 >= 0)
								{
									num9 = num5;
								}
								WorldMap worldMap = WorldMap.Internal;
								i = num9;
								switch (i)
								{
									case 0:
									{
										worldMap = WorldMap.Felucca;
										break;
									}
									case 1:
									{
										worldMap = WorldMap.Felucca;
										break;
									}
									case 2:
									{
										worldMap = WorldMap.Trammel;
										break;
									}
									case 3:
									{
										worldMap = WorldMap.Ilshenar;
										break;
									}
									case 4:
									{
										worldMap = WorldMap.Malas;
										break;
									}
									case 5:
									{
										try
										{
											worldMap = WorldMap.Tokuno;
											break;
										}
										catch
										{
											break;
										}
										break;
									}
								}
								if (worldMap != WorldMap.Internal)
								{
									Guid guid = Guid.NewGuid();
									SpawnPoint spawnPoint = new SpawnPoint(guid, worldMap, (short)num6, (short)num7, (short)(num13 * 2), (short)(num13 * 2))
									{
										SpawnName = string.Format("{0}#{1}", fileName, num2),
										SpawnHomeRange = num12,
										CentreZ = (short)num8,
										SpawnMinDelay = (double)num10,
										SpawnMaxDelay = (double)num11,
										SpawnMaxCount = num14
									};
									Type type = SpawnEditor.FindRunUOType("BaseVendor");
									bool flag1 = false;
									for (int j = 0; j < (int)strArrays2.Length; j++)
									{
										Type type1 = SpawnEditor.FindRunUOType(strArrays2[j]);
										if (type1 != null && type != null && (type1 == type || type1.IsSubclassOf(type)))
										{
											flag1 = true;
										}
										spawnPoint.SpawnObjects.Add(new SpawnObject(strArrays2[j], num14));
									}
									spawnPoint.IsSelected = false;
									if (flag1)
									{
										spawnPoint.SpawnSpawnRange = 0;
									}
									SpawnPointNode spawnPointNode = new SpawnPointNode(spawnPoint);
									this._Editor.tvwSpawnPoints.Nodes.Add(spawnPointNode);
									num2++;
									if (num9 == 0)
									{
										worldMap = WorldMap.Trammel;
										guid = Guid.NewGuid();
										spawnPoint = new SpawnPoint(guid, worldMap, (short)num6, (short)num7, (short)(num13 * 2), (short)(num13 * 2))
										{
											SpawnName = string.Format("{0}#{1}", fileName, num2),
											SpawnHomeRange = num12,
											CentreZ = (short)num8,
											SpawnMinDelay = (double)num10,
											SpawnMaxDelay = (double)num11,
											SpawnMaxCount = num14
										};
										for (int k = 0; k < (int)strArrays2.Length; k++)
										{
											spawnPoint.SpawnObjects.Add(new SpawnObject(strArrays2[k], num14));
										}
										spawnPoint.IsSelected = false;
										if (flag1)
										{
											spawnPoint.SpawnSpawnRange = 0;
										}
										spawnPointNode = new SpawnPointNode(spawnPoint);
										this._Editor.tvwSpawnPoints.Nodes.Add(spawnPointNode);
										num2++;
									}
								}
								else
								{
									num3++;
								}
							}
						}
					}
					streamReader.Close();
				}
			}
			catch
			{
			}
			processedmaps = 1;
			processedspawners = num2;
		}
	}
}