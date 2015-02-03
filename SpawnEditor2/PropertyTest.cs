using System;
using System.Collections;
using System.Reflection;

namespace SpawnEditor2
{
	public class PropertyTest
	{
		public static ArrayList PropertyInfoList;

		private static Type typeofTimeSpan;

		private static Type[] m_ParseTypes;

		private static object[] m_ParseParams;

		private static Type[] m_NumericTypes;

		private static Type typeofType;

		private static Type typeofChar;

		private static Type typeofString;

		static PropertyTest()
		{
			PropertyTest.PropertyInfoList = new ArrayList();
			PropertyTest.typeofTimeSpan = typeof(TimeSpan);
			Type[] typeArray = new Type[] { typeof(string) };
			PropertyTest.m_ParseTypes = typeArray;
			PropertyTest.m_ParseParams = new object[1];
			typeArray = new Type[] { typeof(byte), typeof(sbyte), typeof(short), typeof(ushort), typeof(int), typeof(uint), typeof(long), typeof(ulong) };
			PropertyTest.m_NumericTypes = typeArray;
			PropertyTest.typeofType = typeof(Type);
			PropertyTest.typeofChar = typeof(char);
			PropertyTest.typeofString = typeof(string);
		}

		public PropertyTest()
		{
		}

		public static bool CheckPropertyString(object o, string testString, out string status_str)
		{
			status_str = null;
			if (o == null)
			{
				return false;
			}
			if (testString == null || testString.Length < 1)
			{
				status_str = "Null property test string";
				return false;
			}
			string[] strArrays = PropertyTest.ParseString(testString, 2, "&|");
			if ((int)strArrays.Length < 2)
			{
				return PropertyTest.CheckSingleProperty(o, testString, out status_str);
			}
			bool flag = PropertyTest.CheckSingleProperty(o, strArrays[0], out status_str);
			bool flag1 = PropertyTest.CheckPropertyString(o, strArrays[1], out status_str);
			int num = testString.IndexOf("&");
			int num1 = testString.IndexOf("|");
			if (num > 0 && num1 <= 0 || num > 0 && num < num1)
			{
				if (flag)
				{
					return flag1;
				}
				return false;
			}
			if ((num1 <= 0 || num > 0) && (num1 <= 0 || num1 >= num))
			{
				return false;
			}
			if (!flag)
			{
				return flag1;
			}
			return true;
		}

		public static bool CheckSingleProperty(object o, string testString, out string status_str)
		{
			Type type;
			Type type1;
			bool flag;
			status_str = null;
			if (o == null)
			{
				return false;
			}
			string[] strArrays = PropertyTest.ParseString(testString, 2, "=><!");
			if ((int)strArrays.Length < 2)
			{
				status_str = string.Concat("invalid property string : ", testString);
				return false;
			}
			bool flag1 = false;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = false;
			if (testString.IndexOf("=") > 0)
			{
				flag1 = true;
			}
			else if (testString.IndexOf("!") > 0)
			{
				flag2 = true;
			}
			else if (testString.IndexOf(">") > 0)
			{
				flag3 = true;
			}
			else if (testString.IndexOf("<") > 0)
			{
				flag4 = true;
			}
			if (!flag1 && !flag3 && !flag4 && !flag2)
			{
				return false;
			}
			string str = PropertyTest.ParseForKeywords(o, strArrays[0].Trim(), false, out type);
			if (type == null)
			{
				status_str = string.Concat(strArrays[0], " : ", str);
				return false;
			}
			string str1 = PropertyTest.ParseForKeywords(o, strArrays[1].Trim(), false, out type1);
			if (type1 == null)
			{
				status_str = string.Concat(strArrays[1], " : ", str1);
				return false;
			}
			int num = 10;
			int num1 = 10;
			if (PropertyTest.IsNumeric(type) && str.StartsWith("0x"))
			{
				num = 16;
			}
			if (PropertyTest.IsNumeric(type1) && str1.StartsWith("0x"))
			{
				num1 = 16;
			}
			if (type1 == typeof(TimeSpan) || type == typeof(TimeSpan))
			{
				if (flag1)
				{
					try
					{
						if (TimeSpan.Parse(str) == TimeSpan.Parse(str1))
						{
							flag = true;
							return flag;
						}
					}
					catch
					{
						status_str = string.Concat("invalid timespan comparison : {0}", testString);
					}
				}
				else if (flag2)
				{
					try
					{
						if (TimeSpan.Parse(str) != TimeSpan.Parse(str1))
						{
							flag = true;
							return flag;
						}
					}
					catch
					{
						status_str = string.Concat("invalid timespan comparison : {0}", testString);
					}
				}
				else if (flag3)
				{
					try
					{
						if (TimeSpan.Parse(str) > TimeSpan.Parse(str1))
						{
							flag = true;
							return flag;
						}
					}
					catch
					{
						status_str = string.Concat("invalid timespan comparison : {0}", testString);
					}
				}
				else if (flag4)
				{
					try
					{
						if (TimeSpan.Parse(str) < TimeSpan.Parse(str1))
						{
							flag = true;
							return flag;
						}
					}
					catch
					{
						status_str = string.Concat("invalid timespan comparison : {0}", testString);
					}
				}
			}
			else if (type1 == typeof(DateTime) || type == typeof(DateTime))
			{
				if (flag1)
				{
					try
					{
						if (DateTime.Parse(str) == DateTime.Parse(str1))
						{
							flag = true;
							return flag;
						}
					}
					catch
					{
						status_str = string.Concat("invalid DateTime comparison : {0}", testString);
					}
				}
				else if (flag2)
				{
					try
					{
						if (DateTime.Parse(str) != DateTime.Parse(str1))
						{
							flag = true;
							return flag;
						}
					}
					catch
					{
						status_str = string.Concat("invalid DateTime comparison : {0}", testString);
					}
				}
				else if (flag3)
				{
					try
					{
						if (DateTime.Parse(str) > DateTime.Parse(str1))
						{
							flag = true;
							return flag;
						}
					}
					catch
					{
						status_str = string.Concat("invalid DateTime comparison : {0}", testString);
					}
				}
				else if (flag4)
				{
					try
					{
						if (DateTime.Parse(str) < DateTime.Parse(str1))
						{
							flag = true;
							return flag;
						}
					}
					catch
					{
						status_str = string.Concat("invalid DateTime comparison : {0}", testString);
					}
				}
			}
			else if (PropertyTest.IsNumeric(type1) && PropertyTest.IsNumeric(type))
			{
				if (flag1)
				{
					try
					{
						if (Convert.ToInt64(str, num) == Convert.ToInt64(str1, num1))
						{
							flag = true;
							return flag;
						}
					}
					catch
					{
						status_str = string.Concat("invalid int comparison : {0}", testString);
					}
				}
				else if (flag2)
				{
					try
					{
						if (Convert.ToInt64(str, num) != Convert.ToInt64(str1, num1))
						{
							flag = true;
							return flag;
						}
					}
					catch
					{
						status_str = string.Concat("invalid int comparison : {0}", testString);
					}
				}
				else if (flag3)
				{
					try
					{
						if (Convert.ToInt64(str, num) > Convert.ToInt64(str1, num1))
						{
							flag = true;
							return flag;
						}
					}
					catch
					{
						status_str = string.Concat("invalid int comparison : {0}", testString);
					}
				}
				else if (flag4)
				{
					try
					{
						if (Convert.ToInt64(str, num) < Convert.ToInt64(str1, num1))
						{
							flag = true;
							return flag;
						}
					}
					catch
					{
						status_str = string.Concat("invalid int comparison : {0}", testString);
					}
				}
			}
			else if (type1 == typeof(double) && PropertyTest.IsNumeric(type))
			{
				if (flag1)
				{
					try
					{
						if ((double)Convert.ToInt64(str, num) == double.Parse(str1))
						{
							flag = true;
							return flag;
						}
					}
					catch
					{
						status_str = string.Concat("invalid int comparison : {0}", testString);
					}
				}
				else if (flag2)
				{
					try
					{
						if ((double)Convert.ToInt64(str, num) != double.Parse(str1))
						{
							flag = true;
							return flag;
						}
					}
					catch
					{
						status_str = string.Concat("invalid int comparison : {0}", testString);
					}
				}
				else if (flag3)
				{
					try
					{
						if ((double)Convert.ToInt64(str, num) > double.Parse(str1))
						{
							flag = true;
							return flag;
						}
					}
					catch
					{
						status_str = string.Concat("invalid int comparison : {0}", testString);
					}
				}
				else if (flag4)
				{
					try
					{
						if ((double)Convert.ToInt64(str, num) < double.Parse(str1))
						{
							flag = true;
							return flag;
						}
					}
					catch
					{
						status_str = string.Concat("invalid int comparison : {0}", testString);
					}
				}
			}
			else if (type == typeof(double) && PropertyTest.IsNumeric(type1))
			{
				if (flag1)
				{
					try
					{
						if (double.Parse(str) == (double)Convert.ToInt64(str1, num1))
						{
							flag = true;
							return flag;
						}
					}
					catch
					{
						status_str = string.Concat("invalid int comparison : {0}", testString);
					}
				}
				else if (flag2)
				{
					try
					{
						if (double.Parse(str) != (double)Convert.ToInt64(str1, num1))
						{
							flag = true;
							return flag;
						}
					}
					catch
					{
						status_str = string.Concat("invalid int comparison : {0}", testString);
					}
				}
				else if (flag3)
				{
					try
					{
						if (double.Parse(str) > (double)Convert.ToInt64(str1, num1))
						{
							flag = true;
							return flag;
						}
					}
					catch
					{
						status_str = string.Concat("invalid int comparison : {0}", testString);
					}
				}
				else if (flag4)
				{
					try
					{
						if (double.Parse(str) < (double)Convert.ToInt64(str1, num1))
						{
							flag = true;
							return flag;
						}
					}
					catch
					{
						status_str = string.Concat("invalid int comparison : {0}", testString);
					}
				}
			}
			else if (type == typeof(double) && type1 == typeof(double))
			{
				if (flag1)
				{
					try
					{
						if (double.Parse(str) == double.Parse(str1))
						{
							flag = true;
							return flag;
						}
					}
					catch
					{
						status_str = string.Concat("invalid int comparison : {0}", testString);
					}
				}
				else if (flag2)
				{
					try
					{
						if (double.Parse(str) != double.Parse(str1))
						{
							flag = true;
							return flag;
						}
					}
					catch
					{
						status_str = string.Concat("invalid int comparison : {0}", testString);
					}
				}
				else if (flag3)
				{
					try
					{
						if (double.Parse(str) > double.Parse(str1))
						{
							flag = true;
							return flag;
						}
					}
					catch
					{
						status_str = string.Concat("invalid int comparison : {0}", testString);
					}
				}
				else if (flag4)
				{
					try
					{
						if (double.Parse(str) < double.Parse(str1))
						{
							flag = true;
							return flag;
						}
					}
					catch
					{
						status_str = string.Concat("invalid int comparison : {0}", testString);
					}
				}
			}
			else if (type1 == typeof(bool) && type == typeof(bool))
			{
				try
				{
					if (Convert.ToBoolean(str) == Convert.ToBoolean(str1))
					{
						flag = true;
						return flag;
					}
				}
				catch
				{
					status_str = string.Concat("invalid bool comparison : {0}", testString);
				}
			}
			else if (type1 != typeof(double) && type1 != typeof(double))
			{
				if (flag1)
				{
					if (str == str1)
					{
						return true;
					}
				}
				else if (flag2 && str != str1)
				{
					return true;
				}
			}
			else if (flag1)
			{
				try
				{
					if (Convert.ToDouble(str) == Convert.ToDouble(str1))
					{
						flag = true;
						return flag;
					}
				}
				catch
				{
					status_str = string.Concat("invalid double comparison : {0}", testString);
				}
			}
			else if (flag2)
			{
				try
				{
					if (Convert.ToDouble(str) != Convert.ToDouble(str1))
					{
						flag = true;
						return flag;
					}
				}
				catch
				{
					status_str = string.Concat("invalid double comparison : {0}", testString);
				}
			}
			else if (flag3)
			{
				try
				{
					if (Convert.ToDouble(str) > Convert.ToDouble(str1))
					{
						flag = true;
						return flag;
					}
				}
				catch
				{
					status_str = string.Concat("invalid double comparison : {0}", testString);
				}
			}
			else if (flag4)
			{
				try
				{
					if (Convert.ToDouble(str) < Convert.ToDouble(str1))
					{
						flag = true;
						return flag;
					}
				}
				catch
				{
					status_str = string.Concat("invalid double comparison : {0}", testString);
				}
			}
			return false;
		}

		public static string GetPropertyValue(object o, string name, out Type ptype)
		{
			string propertyValue;
			PropertyInfo[] propertyInfoArray;
			int num;
			ptype = null;
			if (o == null || name == null)
			{
				return null;
			}
			Type type = o.GetType();
			object value = null;
			PropertyInfo[] properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
			string[] strArrays = PropertyTest.ParseString(name, 2, ".");
			string str = strArrays[0];
			PropertyTest.ParseString(str, 4, ",");
			string str1 = strArrays[0];
			char[] chrArray = new char[] { '[' };
			string[] strArrays1 = str1.Split(chrArray);
			int num1 = 0;
			if ((int)strArrays1.Length > 1)
			{
				str = strArrays1[0];
				string str2 = strArrays1[1];
				chrArray = new char[] { ']' };
				string[] strArrays2 = str2.Split(chrArray);
				if ((int)strArrays2.Length > 0)
				{
					try
					{
						num1 = int.Parse(strArrays2[0]);
					}
					catch
					{
					}
				}
			}
			if ((int)strArrays.Length != 2)
			{
				PropertyInfo propertyInfo = PropertyTest.LookupPropertyInfo(type, str);
				if (propertyInfo != null)
				{
					if (!propertyInfo.CanRead)
					{
						return "Property is write only.";
					}
					ptype = propertyInfo.PropertyType;
					return PropertyTest.InternalGetValue(o, propertyInfo, num1);
				}
				propertyInfoArray = properties;
				num = 0;
				while (num < (int)propertyInfoArray.Length)
				{
					PropertyInfo propertyInfo1 = propertyInfoArray[num];
					if (!PropertyTest.Insensitive.Equals(propertyInfo1.Name, str))
					{
						num++;
					}
					else if (propertyInfo1.CanRead)
					{
						ptype = propertyInfo1.PropertyType;
						propertyValue = PropertyTest.InternalGetValue(o, propertyInfo1, num1);
						return propertyValue;
					}
					else
					{
						propertyValue = "Property is write only.";
						return propertyValue;
					}
				}
				return "Property not found.";
			}
			else
			{
				PropertyInfo propertyInfo2 = PropertyTest.LookupPropertyInfo(type, str);
				if (propertyInfo2 != null)
				{
					if (!propertyInfo2.CanWrite)
					{
						return "Property is read only.";
					}
					ptype = propertyInfo2.PropertyType;
					if (!ptype.IsArray)
					{
						value = propertyInfo2.GetValue(o, null);
					}
					else
					{
						try
						{
							object value1 = propertyInfo2.GetValue(o, null);
							int lowerBound = ((Array)value1).GetLowerBound(0);
							int upperBound = ((Array)value1).GetUpperBound(0);
							if (num1 <= lowerBound && num1 <= upperBound)
							{
								value = ((Array)value1).GetValue(num1);
							}
						}
						catch
						{
						}
					}
					return PropertyTest.GetPropertyValue(value, strArrays[1], out ptype);
				}
				propertyInfoArray = properties;
				num = 0;
				while (num < (int)propertyInfoArray.Length)
				{
					PropertyInfo propertyInfo3 = propertyInfoArray[num];
					if (!PropertyTest.Insensitive.Equals(propertyInfo3.Name, str))
					{
						num++;
					}
					else if (propertyInfo3.CanWrite)
					{
						ptype = propertyInfo3.PropertyType;
						if (!ptype.IsArray)
						{
							value = propertyInfo3.GetValue(o, null);
						}
						else
						{
							try
							{
								object value2 = propertyInfo3.GetValue(o, null);
								int lowerBound1 = ((Array)value2).GetLowerBound(0);
								int upperBound1 = ((Array)value2).GetUpperBound(0);
								if (num1 <= lowerBound1 && num1 <= upperBound1)
								{
									value = ((Array)value2).GetValue(num1);
								}
							}
							catch
							{
							}
						}
						propertyValue = PropertyTest.GetPropertyValue(value, strArrays[1], out ptype);
						return propertyValue;
					}
					else
					{
						propertyValue = "Property is read only.";
						return propertyValue;
					}
				}
				return "Property not found.";
			}
			return propertyValue;
		}

		private static string InternalGetValue(object o, PropertyInfo p, int index)
		{
			string str;
			Type propertyType = p.PropertyType;
			object value = null;
			if (!propertyType.IsArray)
			{
				value = p.GetValue(o, null);
			}
			else
			{
				try
				{
					object obj = p.GetValue(o, null);
					int lowerBound = ((Array)obj).GetLowerBound(0);
					int upperBound = ((Array)obj).GetUpperBound(0);
					if (index <= lowerBound && index <= upperBound)
					{
						value = ((Array)obj).GetValue(index);
					}
				}
				catch
				{
				}
			}
			if (value == null)
			{
				str = "(-null-)";
			}
			else if (PropertyTest.IsNumeric(propertyType))
			{
				str = string.Format("{0} (0x{0:X})", value);
			}
			else if (!PropertyTest.IsChar(propertyType))
			{
				str = (!PropertyTest.IsString(propertyType) ? value.ToString() : string.Format("\"{0}\"", value));
			}
			else
			{
				str = string.Format("'{0}' ({1} [0x{1:X}])", value, (int)value);
			}
			return string.Format("{0} = {1}", p.Name, str);
		}

		private static bool IsChar(Type t)
		{
			return t == PropertyTest.typeofChar;
		}

		private static bool IsEnum(Type t)
		{
			return t.IsEnum;
		}

		public static bool IsNumeric(Type t)
		{
			return Array.IndexOf(PropertyTest.m_NumericTypes, t) >= 0;
		}

		private static bool IsParsable(Type t)
		{
			return t == PropertyTest.typeofTimeSpan;
		}

		private static bool IsString(Type t)
		{
			return t == PropertyTest.typeofString;
		}

		private static bool IsType(Type t)
		{
			return t == PropertyTest.typeofType;
		}

		public static PropertyInfo LookupPropertyInfo(Type type, string propname)
		{
			if (type == null || propname == null)
			{
				return null;
			}
			PropertyInfo propertyInfo = null;
			PropertyTest.TypeInfo typeInfo = null;
			foreach (PropertyTest.TypeInfo propertyInfoList in PropertyTest.PropertyInfoList)
			{
				if (propertyInfoList.t != type)
				{
					continue;
				}
				typeInfo = propertyInfoList;
				foreach (PropertyInfo propertyInfo1 in propertyInfoList.plist)
				{
					if (!PropertyTest.Insensitive.Equals(propertyInfo1.Name, propname))
					{
						continue;
					}
					propertyInfo = propertyInfo1;
				}
			}
			if (propertyInfo != null)
			{
				return propertyInfo;
			}
			PropertyInfo[] properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
			for (int i = 0; i < (int)properties.Length; i++)
			{
				PropertyInfo propertyInfo2 = properties[i];
				if (PropertyTest.Insensitive.Equals(propertyInfo2.Name, propname))
				{
					if (typeInfo == null)
					{
						typeInfo = new PropertyTest.TypeInfo()
						{
							t = type
						};
						PropertyTest.PropertyInfoList.Add(typeInfo);
					}
					typeInfo.plist.Add(propertyInfo2);
					return propertyInfo2;
				}
			}
			return null;
		}

		private static object Parse(object o, Type t, string value)
		{
			MethodInfo method = t.GetMethod("Parse", PropertyTest.m_ParseTypes);
			PropertyTest.m_ParseParams[0] = value;
			return method.Invoke(o, PropertyTest.m_ParseParams);
		}

		public static string ParseForKeywords(object o, string valstr, bool literal, out Type ptype)
		{
			ptype = null;
			if (valstr == null || valstr.Length <= 0)
			{
				return null;
			}
			string str = valstr.Trim();
			string[] strArrays = PropertyTest.ParseString(str, 2, "[");
			string[] matchingParen = null;
			string str1 = null;
			if ((int)strArrays.Length > 1)
			{
				matchingParen = PropertyTest.ParseToMatchingParen(strArrays[1], '[', ']');
				str1 = matchingParen[0];
			}
			string[] strArrays1 = strArrays[0].Trim().Split(new char[] { ',' });
			if (str1 != null && str1.Length > 0 && strArrays1 != null && (int)strArrays1.Length > 0)
			{
				strArrays1[(int)strArrays1.Length - 1] = str1;
			}
			string str2 = strArrays1[0].Trim();
			char chr = str[0];
			if (chr == '.' || chr == '-' || chr == '+' || chr >= '0' && chr <= '9')
			{
				if (str.IndexOf(".") < 0)
				{
					ptype = typeof(int);
				}
				else
				{
					ptype = typeof(double);
				}
				return str;
			}
			if (chr == '\"' || chr == '(')
			{
				ptype = typeof(string);
				return str;
			}
			if (chr == '#')
			{
				ptype = typeof(string);
				return str.Substring(1);
			}
			if (str.ToLower() == "true" || str.ToLower() == "false")
			{
				ptype = typeof(bool);
				return str;
			}
			if (literal)
			{
				ptype = typeof(string);
				return str;
			}
			string propertyValue = PropertyTest.GetPropertyValue(o, str2, out ptype);
			return PropertyTest.ParseGetValue(propertyValue, ptype);
		}

		public static string ParseGetValue(string str, Type ptype)
		{
			if (str == null)
			{
				return null;
			}
			string[] strArrays = str.Split("=".ToCharArray(), 2);
			if ((int)strArrays.Length <= 1)
			{
				return null;
			}
			if (!PropertyTest.IsNumeric(ptype))
			{
				return strArrays[1].Trim();
			}
			string[] strArrays1 = strArrays[1].Trim().Split(" ".ToCharArray(), 2);
			return strArrays1[0];
		}

		public static string[] ParseString(string str, int nitems, string delimstr)
		{
			if (str == null || delimstr == null)
			{
				return null;
			}
			char[] charArray = delimstr.ToCharArray();
			str = str.Trim();
			return str.Split(charArray, nitems);
		}

		public static string[] ParseToMatchingParen(string str, char opendelim, char closedelim)
		{
			int num = 1;
			int num1 = 0;
			int length = str.Length;
			int num2 = 0;
			while (num2 < str.Length)
			{
				if (str[num2] == opendelim)
				{
					num++;
				}
				if (str[num2] == closedelim)
				{
					num1++;
				}
				if (num != num1)
				{
					num2++;
				}
				else
				{
					length = num2;
					break;
				}
			}
			string[] strArrays = new string[] { str.Substring(0, length), "" };
			if (length + 1 < str.Length)
			{
				strArrays[1] = str.Substring(length + 1, str.Length - length - 1);
			}
			return strArrays;
		}

		public class Insensitive
		{
			private static IComparer m_Comparer;

			public static IComparer Comparer
			{
				get
				{
					return PropertyTest.Insensitive.m_Comparer;
				}
			}

			static Insensitive()
			{
				PropertyTest.Insensitive.m_Comparer = CaseInsensitiveComparer.Default;
			}

			private Insensitive()
			{
			}

			public static int Compare(string a, string b)
			{
				return PropertyTest.Insensitive.m_Comparer.Compare(a, b);
			}

			public static bool Contains(string a, string b)
			{
				if (a == null || b == null || a.Length < b.Length)
				{
					return false;
				}
				a = a.ToLower();
				b = b.ToLower();
				return a.IndexOf(b) >= 0;
			}

			public static bool EndsWith(string a, string b)
			{
				if (a == null || b == null || a.Length < b.Length)
				{
					return false;
				}
				return PropertyTest.Insensitive.m_Comparer.Compare(a.Substring(a.Length - b.Length), b) == 0;
			}

			public static bool Equals(string a, string b)
			{
				if (a == null && b == null)
				{
					return true;
				}
				if (a == null || b == null || a.Length != b.Length)
				{
					return false;
				}
				return PropertyTest.Insensitive.m_Comparer.Compare(a, b) == 0;
			}

			public static bool StartsWith(string a, string b)
			{
				if (a == null || b == null || a.Length < b.Length)
				{
					return false;
				}
				return PropertyTest.Insensitive.m_Comparer.Compare(a.Substring(0, b.Length), b) == 0;
			}
		}

		public class TypeInfo
		{
			public ArrayList plist;

			public Type t;

			public TypeInfo()
			{
			}
		}
	}
}