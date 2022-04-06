using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Collections.Generic;
using LockIntegralType = System.Int32;
using System.Threading;
using Grasshopper.Kernel;
using System.Windows.Forms;
namespace GH_CMD
{
	 public  class ALIEN_REFLECT
	{
        private Type type => instance.GetType();
        private Object instance;
        public  ALIEN_REFLECT(object ins)
        {
            this.instance = ins;
        }
        public  T GetField<T>(string fieldname,bool ip)
        {
            BindingFlags flag = BindingFlags.Instance | BindingFlags.NonPublic;
            FieldInfo field = null;
            if (ip)
                field = type.GetField(fieldname, flag);
            else
                field = type.GetField(fieldname);
            return (T)field.GetValue(instance);
        }
    }
}
