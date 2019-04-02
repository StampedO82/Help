using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuxiliaryFeature.Utility
{
    public static class ObjectCollector
    {
        static System.Collections.Generic.Dictionary<Type, object> objects = new Dictionary<Type, object>();

        public static int? Counter()
        {
            if (objects == null)
                return null;
            else
                return objects.Count;
        }

        public static void Add<T>(T obj)
        {
            if (!objects.ContainsKey(typeof(T)))
                objects.Add(typeof(T), obj);
        }

        public static void Add<T>(T obj, bool overrideObj = false)
        {
            if (!objects.ContainsKey(typeof(T)))
            {
                objects.Add(typeof(T), obj);
                return;
            }

            if (overrideObj)
            {
                objects[typeof(T)] = obj;
            }
        }

        public static void RemoveAt(int index)
        {
            int i = 0;
            Type t = null;
            foreach (var k in objects.Keys)
            {
                if (i == index)
                {
                    t = k;
                    break;
                }
                i++;
            }


            if (t != null && objects.ContainsKey(t))
                objects.Remove(t);
        }

        public static void RemoveAll()
        {
            objects.Clear();
        }


        public static void Remove<T>(T obj)
        {
            if (objects.ContainsKey(typeof(T)))
                objects.Remove(typeof(T));
        }

        public static void Replace<T1, T2>(T1 oldObj, T2 newObj)
        {
            Remove<T1>(oldObj);
            Add<T2>(newObj);
        }

        public static T Get<T>()
        {
            if (objects.ContainsKey(typeof(T)))
                return (T)objects[typeof(T)];
            else
                return default(T);
        }
    }
}
