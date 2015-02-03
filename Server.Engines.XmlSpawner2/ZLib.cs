using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Xml.Serialization;

namespace Server.Engines.XmlSpawner2
{
	public class ZLib
	{
		public ZLib()
		{
		}

		public static bool CheckVersion()
		{
			bool flag;
			string[] strArrays = null;
			try
			{
				strArrays = ZLib.zlibVersion().Split(new char[] { '.' });
				return strArrays[0] == "1";
			}
			catch (DllNotFoundException dllNotFoundException)
			{
				flag = false;
			}
			return flag;
		}

		[DllImport("zlib", CharSet=CharSet.None, ExactSpelling=false)]
		private static extern ZLib.ZLibError compress(byte[] dest, ref int destLength, byte[] source, int sourceLength);

		public static byte[] Compress(object source)
		{
			byte[] numArray;
			try
			{
				XmlSerializer xmlSerializer = new XmlSerializer(source.GetType());
				MemoryStream memoryStream = new MemoryStream();
				xmlSerializer.Serialize(memoryStream, source);
				byte[] array = memoryStream.ToArray();
				memoryStream.Close();
				int length = (int)array.Length;
				int num = (int)array.Length + 1;
				byte[] numArray1 = new byte[num];
				if (ZLib.compress2(numArray1, ref num, array, (int)array.Length, ZLib.ZLibCompressionLevel.Z_BEST_COMPRESSION) == ZLib.ZLibError.Z_OK)
				{
					byte[] numArray2 = new byte[num + 4];
					Array.Copy(numArray1, 0, numArray2, 4, num);
					numArray2[0] = (byte)length;
					numArray2[1] = (byte)(length >> 8);
					numArray2[2] = (byte)(length >> 16);
					numArray2[3] = (byte)(length >> 24);
					numArray = numArray2;
				}
				else
				{
					numArray = new byte[0];
				}
			}
			catch (Exception exception)
			{
				numArray = new byte[0];
			}
			return numArray;
		}

		public static byte[] Compress(byte[] data)
		{
			int length = (int)data.Length;
			int num = (int)data.Length;
			byte[] numArray = new byte[(int)data.Length];
			if (ZLib.compress(numArray, ref num, data, (int)data.Length) != ZLib.ZLibError.Z_OK)
			{
				return null;
			}
			byte[] numArray1 = new byte[num + 4];
			Array.Copy(numArray, 0, numArray1, 4, num);
			numArray1[0] = (byte)(length & 255);
			numArray1[1] = (byte)(length >> 8 & 255);
			numArray1[2] = (byte)(length >> 16 & 255);
			numArray1[3] = (byte)(length >> 24 & 255);
			return numArray1;
		}

		[DllImport("zlib", CharSet=CharSet.None, ExactSpelling=false)]
		private static extern ZLib.ZLibError compress2(byte[] dest, ref int destLength, byte[] source, int sourceLength, ZLib.ZLibCompressionLevel level);

		public static object Decompress(byte[] data, Type type)
		{
			object obj;
			try
			{
				int num = data[0] | data[1] << 8 | data[2] << 16 | data[3] << 24;
				byte[] numArray = new byte[(int)data.Length - 4];
				Array.Copy(data, 4, numArray, 0, (int)data.Length - 4);
				byte[] numArray1 = new byte[num];
				if (ZLib.uncompress(numArray1, ref num, numArray, (int)numArray.Length) == ZLib.ZLibError.Z_OK)
				{
					MemoryStream memoryStream = new MemoryStream(numArray1);
					object obj1 = (new XmlSerializer(type)).Deserialize(memoryStream);
					memoryStream.Close();
					obj = obj1;
				}
				else
				{
					obj = null;
				}
			}
			catch
			{
				obj = null;
			}
			return obj;
		}

		public static byte[] Decompress(byte[] data)
		{
			int num = data[0] | data[1] << 8 | data[2] << 16 | data[3] << 24;
			byte[] numArray = new byte[(int)data.Length - 4];
			Array.Copy(data, 4, numArray, 0, (int)data.Length - 4);
			byte[] numArray1 = new byte[num];
			if (ZLib.uncompress(numArray1, ref num, numArray, (int)numArray.Length) != ZLib.ZLibError.Z_OK)
			{
				return null;
			}
			return numArray1;
		}

		[DllImport("zlib", CharSet=CharSet.None, ExactSpelling=false)]
		private static extern ZLib.ZLibError uncompress(byte[] dest, ref int destLen, byte[] source, int sourceLen);

		[DllImport("zlib", CharSet=CharSet.None, ExactSpelling=false)]
		private static extern string zlibVersion();

		private enum ZLibCompressionLevel
		{
			Z_DEFAULT_COMPRESSION = -1,
			Z_NO_COMPRESSION = 0,
			Z_BEST_SPEED = 1,
			Z_BEST_COMPRESSION = 9
		}

		private enum ZLibError
		{
			Z_VERSION_ERROR = -6,
			Z_BUF_ERROR = -5,
			Z_MEM_ERROR = -4,
			Z_DATA_ERROR = -3,
			Z_STREAM_ERROR = -2,
			Z_ERRNO = -1,
			Z_OK = 0,
			Z_STREAM_END = 1,
			Z_NEED_DICT = 2
		}
	}
}