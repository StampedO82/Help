using System;
using System.Collections.Generic;
using System.Reflection;

namespace AuxiliaryFeature.Utility
{
    class ObjectConverter
    {
        public T Cast<T>(object o)
        {
            return (T)o;
        }

        public T TryCast<T>(object o)
        {
            object obj = null;
            try
            {
                obj = Convert.ChangeType(o, typeof(T));
            }
            catch
            {
                obj = null;
            }
            //var obj = o as T;
            if (obj != null)
            {
                return (T)o;
            }
            return default(T);
        }

        public dynamic ConvertTo(dynamic obj, Type toType)
        {
            if (obj == null)
                return null;

            MethodInfo CanCastMethod = this.GetType().GetMethod("TryCast").MakeGenericMethod(toType);
            object castedObject = CanCastMethod.Invoke(this, new dynamic[] { obj });

            //MethodInfo castMethod = this.GetType().GetMethod("Cast").MakeGenericMethod(toType);
            //dynamic castedObject = castMethod.Invoke(this, new dynamic[] { obj });
            return castedObject;
        }

        public IList<T> BuildListDynamic<T>()
        {
            List<T> list = new List<T>();
            return list;
        }

        public dynamic CreateListDynamically(Type t)
        {
            MethodInfo method = this.GetType().GetMethod("BuildListDynamic");
            method = method.MakeGenericMethod(new Type[] { t });
            dynamic t2 = method.Invoke(this, null);
            return t2;
        }
    }
}
