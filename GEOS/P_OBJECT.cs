using GH_IO.Serialization;
using Grasshopper;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Data;
using Rhino;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace UI.GEOS
{
	[Serializable]
	public  class P_OBJECT<T, P>
	{
		public virtual bool P_FAILURE
		{
			get;
			set;
		}

		public virtual string P_NAME
		{
			get;
			set;
		}

		public virtual string P_EXPRESION
		{
			get;
			set;
		}

		public virtual P P_TAG
		{
			get;
			set;
		}

		public virtual List<P> P_TAGS
		{
			get;
			set;
		}

		public virtual DataTree<P> P_TAGT
		{
			get;
			set;
		}

		public virtual T P_VALUE
		{
			get;
			set;
		}

		public virtual List<T> P_VALUES
		{
			get;
			set;
		}

		public virtual DataTree<T> P_VALUET
		{
			get;
			set;
		}

		internal P_OBJECT()
		{
		}

		internal P_OBJECT(T data)
		{
		
			P_VALUE = data;
		}

		internal P_OBJECT(T data, P index)
		{
			P_VALUE = data;
			P_TAG = index;
		}

		internal P_OBJECT(List<T> datas)
		{
			P_VALUES = datas;
         
		}

		internal P_OBJECT(List<T> datas, List<P> tags)
		{
			P_VALUES = datas;
			P_TAGS = tags;
		}

		internal P_OBJECT(P_OBJECT<T, P> data)
		{
			P_FAILURE = data.P_FAILURE;
			P_TAG = data.P_TAG;
			P_NAME = data.P_NAME;
			P_EXPRESION = data.P_EXPRESION;
			P_VALUE = data.P_VALUE;
			P_VALUES = data.P_VALUES;
			P_VALUET = data.P_VALUET;
		}

		public virtual Guid ADD(RhinoDoc activeDoc)
		{
			return Guid.Empty;
		}

		public virtual bool Write(GH_IWriter writer)
		{
			return true;
		}

		public virtual bool Read(GH_IReader reader)
		{
			return true;
		}

		public List<U> CONVERT_ALL<U>(Func<T, int, U> func)
		{
			List<U> list = new List<U>();
			int count = this.P_VALUES.Count;
			for (int i = 0; i < count; i++)
			{
				list.Add(func(this.P_VALUES[i], i));
			}
			return list;
		}

		internal List<U> CONVERT_ALL<U, N>(List<N> list, Func<N, int, U> func)
		{
			List<U> list2 = new List<U>();
			int count = list.Count;
			for (int i = 0; i < count; i++)
			{
				list2.Add(func(list[i], i));
			}
			return list2;
		}

		internal void CONVERT_ALL<U>(List<U> listu, Func<U, int, T> func)
		{
			int count = listu.Count;
			for (int i = 0; i < count; i++)
			{
				this.P_VALUES.Add(func(listu[i], i));
			}
		}

		internal void GROUP(double tolerance, Func<T, T, double, bool> func)
		{
			int count = P_VALUES.Count;
			int num = 0;
			while (count > 0)
			{
                P_VALUET.Add(P_VALUES[0], new GH_Path(new int[2]
                {
                        0,
                        num
                }));
                P_TAGT.Add(P_TAGS[0], new GH_Path(new int[2]
                {
                        0,
                        num
                }));
                for (int i = 1; i < count; i++)
				{
				
					if (func(P_VALUES[0], P_VALUES[i], tolerance))
					{
						P_VALUET.Add(P_VALUES[i], new GH_Path(new int[2]
						{
							0,
							num
						}));
						P_TAGT.Add(P_TAGS[i], new GH_Path(new int[2]
						{
							0,
							num
						}));
						P_VALUES.RemoveAt(i);
						P_TAGS.RemoveAt(i);
						i--;
                        count = P_VALUES.Count;
                    }				
				}
                P_VALUES.RemoveAt(0);
                P_TAGS.RemoveAt(0);
                count = P_VALUES.Count;
                num++;
			}
		}
        internal void GROUP(double[] tolerance, Func<T, T, double[], bool> func)
        {
            int count = P_VALUES.Count;
            int num = 0;
            while (count > 0)
            {
                P_VALUET.Add(P_VALUES[0], new GH_Path(new int[2]
                {
                        0,
                        num
                }));
                P_TAGT.Add(P_TAGS[0], new GH_Path(new int[2]
                {
                        0,
                        num
                }));
                for (int i = 1; i < count; i++)
                {

                    if (func(P_VALUES[0], P_VALUES[i], tolerance))
                    {
                        P_VALUET.Add(P_VALUES[i], new GH_Path(new int[2]
                        {
                            0,
                            num
                        }));
                        P_TAGT.Add(P_TAGS[i], new GH_Path(new int[2]
                        {
                            0,
                            num
                        }));
                        P_VALUES.RemoveAt(i);
                        P_TAGS.RemoveAt(i);
                        i--;
                        count = P_VALUES.Count;
                    }
                }
                P_VALUES.RemoveAt(0);
                P_TAGS.RemoveAt(0);
                count = P_VALUES.Count;
                num++;
            }
        }
        public void CULLD(double t)
		{
			try
			{
				List<T> list = new List<T>();
				List<P> list2 = new List<P>();
                int count = P_VALUES.Count;
                while ( count > 0)
				{
					for (int i = 1; i < count; i++)
					{
						if (DUPLICATE(t,i))
						{
							P_VALUES.RemoveAt(i);
							P_TAGS.RemoveAt(i);
                            count = P_VALUES.Count;
                            i--;							
						}
					}
					list.Add(P_VALUES[0]);
					list2.Add(P_TAGS[0]);
					P_VALUES.RemoveAt(0);
					P_TAGS.RemoveAt(0);
                    count = P_VALUES.Count;
                }
				P_VALUES = list;
				P_TAGS = list2;
			}
			catch(Exception ex)
			{
                throw ex;
			}
		}

        public virtual bool DUPLICATE(double t,int i)
        {
            return false;
        }

		internal Q CONVERT_TO<Q, O>(O o)
		{
			object obj = o;
			return (Q)obj;
		}

		internal Q CONVERT_FROM<Q, O>(O o)
		{
			object obj = o;
			return (Q)obj;
		}

		internal static void MATCH_COUNT<U, V>(List<U> n, List<V> v) where U : new()
		{
			int count = v.Count;
			int count2 = n.Count;
			int num = count2 - count;
			while (num > 0)
			{
				n.RemoveAt(n.Count - 1);
			}
			while (num < 0)
			{
				n.Add(new U());
			}
		}

		public static T Deserialize(string data)
		{
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(data));
			data = null;
			object obj = binaryFormatter.Deserialize(memoryStream);
			memoryStream.Dispose();
			return (T)obj;
		}

		public  static string Serialize(T data)
		{
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			MemoryStream memoryStream = new MemoryStream();
			binaryFormatter.Serialize(memoryStream, data);
			string result = Convert.ToBase64String(memoryStream.GetBuffer());
			memoryStream.Dispose();
			return result;
		}

		public  static T DOC_GETVALUE(string method, GH_DocumentObject obj, string k, T v)
		{
			P_REFLECTION pANDA_REFLECTION = new P_REFLECTION(typeof(GH_DocumentObject),obj);
			try
			{
				return pANDA_REFLECTION.GetMethod<T>(method, new object[2]
				{
					k,
					v
				}, BindingFlags.Instance | BindingFlags.NonPublic);
			}
			catch (Exception ex)
			{
				MessageBox.Show("GETVALUE: " + ex.Message);
				return default(T);
			}
		}

		public  static void DOC_SETVALUE(string method, GH_DocumentObject obj, string k, T v)
		{
			P_REFLECTION pANDA_REFLECTION = new P_REFLECTION(obj);
			try
			{
				pANDA_REFLECTION.GetMethod(typeof(GH_DocumentObject), method, new object[2]
				{
					k,
					v
				}, BindingFlags.Instance | BindingFlags.NonPublic);
			}
			catch (Exception ex)
			{
				MessageBox.Show("SETVALUE: " + ex.Message);
			}
		}

		internal static GH_GetterResult PROMPT_SINGULAR(IGH_Param obj, ref T goo)
		{
			P_REFLECTION pANDA_REFLECTION = new P_REFLECTION(obj);
			try
			{
				return pANDA_REFLECTION.GetMethod<GH_GetterResult>("Prompt_Singular", new object[1]
				{
					goo
				}, BindingFlags.Instance | BindingFlags.NonPublic);
			}
			catch (Exception)
			{
				return GH_GetterResult.cancel;
			}
		}

		public static T PROMPT_PLURAL(IGH_Param obj)
		{
			P_REFLECTION pANDA_REFLECTION = new P_REFLECTION(obj);
			try
			{
				Assembly assembly = Assembly.Load("mscorlib");
				Type type = assembly.GetType("System.Collections.Generic.List`1").MakeGenericType(obj.Type);
				object obj2 = Activator.CreateInstance(type);
				object[] values = new object[1]
				{
					obj2
				};
				return (T)pANDA_REFLECTION.GetMethodRef(((object)obj).GetType(), "Prompt_Plural", values, BindingFlags.Instance | BindingFlags.NonPublic);
			}
			catch (Exception ex)
			{
				MessageBox.Show("PROMPT_PLURAL: " + ex.Message);
				return default(T);
			}
		}
	}
}
