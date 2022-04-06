using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Collections.Generic;
using LockIntegralType = System.Int32;
using System.Threading;
using Grasshopper.Kernel;
using System.Windows.Forms;
namespace UI.GEOS
{
	public  class P_REFLECTION
	{
        #region field
        private Type m_type;
        private Object m_instance;
        //private BindingFlags flag => BindingFlags.Instance | BindingFlags.NonPublic;
        #endregion
        public P_REFLECTION(object ins)
        {
            this.m_instance = ins;
            this.m_type = m_instance.GetType();
        }
        public P_REFLECTION(Type ins)
        {
            this.m_instance = ins;
        }
        public P_REFLECTION(Type type, object ins)
        {
            this.m_instance = ins;
            this.m_type =type;
        }

        public  T GetMethod<T>(string method,object[] values, BindingFlags flag)
        {
            MethodInfo methodinfo = m_type.GetMethod(method, flag, null, get_types(values), null);
            return  (T)(methodinfo.Invoke(m_instance, values));
        }

        public  T GetMethodT<T>(string method,Type[] ts, object[] values, BindingFlags flag)
        {
            MethodInfo methodinfo = m_type.GetMethod(method, flag, null, get_types(values), null).MakeGenericMethod(ts);
            return (T)(methodinfo.Invoke(m_instance, values));
        }
        public void GetMethodT(string method, Type[] ts, object[] values, BindingFlags flag)
        {
            MethodInfo methodinfo = m_type.GetMethod(method, flag, null, get_types(values), null).MakeGenericMethod(ts);
            methodinfo.Invoke(m_instance, values);
        }
        public  object GetMethodRef(Type type, string method,object[] values, BindingFlags flag)
        {
            MethodInfo methodinfo = type.GetMethod(method, flag);
            methodinfo.Invoke(m_instance, values);
            return values[0];
        }
        public  void GetMethod(Type type, string method,object[] values, BindingFlags flag)
        {
            MethodInfo methodinfo = type.GetMethod(method,flag,null, get_types(values),null);
            methodinfo.Invoke(m_instance, values);
        }
        public  T GetField<T>(string fieldname,bool ip)
        {
            BindingFlags flag = BindingFlags.Instance | BindingFlags.NonPublic;
            FieldInfo field = null;
            if (ip)
                field = m_type.GetField(fieldname, flag);
            else
                field = m_type.GetField(fieldname);
            return (T)field.GetValue(m_instance);
        }
        public  void SetField(string fieldname, object value,bool ip, BindingFlags flag)
        {
            FieldInfo field =null;
            field = m_type.GetField(fieldname, flag);
            field.SetValue(m_instance, value);
        }
        public  T GetProperty<T>(string propertyname, BindingFlags flag)
        {
            PropertyInfo field = null;
            field = m_type.GetProperty(propertyname, flag);
            return (T)field.GetValue(m_instance, null);
        }
        public  void SetProperty(object instance,string propertyname, object value, BindingFlags flag)
        {
            PropertyInfo field = null;
            field = m_type.GetProperty(propertyname, flag);
            field.SetValue(instance, value, null);
        }
        private Type[] get_types(object[] objs)
        {
            int num = objs.Length;
            Type[] ts = new Type[num];
            for (int i = 0; i < num; i++)
            {
                ts[i] = objs[i].GetType();
            }
            return ts;
        }
    }
}
