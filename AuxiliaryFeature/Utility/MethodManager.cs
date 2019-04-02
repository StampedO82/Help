using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuxiliaryFeature.Utility
{
    public static class MethodManager
    {
        static Dictionary<Type, List<DelegateMethod>> _tabControlMethodCollection = new Dictionary<Type, List<DelegateMethod>>();

        static List<DelegateMethod> GetClassTypeMethods<T>()
        {
            return _tabControlMethodCollection[typeof(T)];
        }

        public static bool RefreshProperties<T>(int index)
        {
            var selectedClassTypeMethods = GetClassTypeMethods<T>();
            if (selectedClassTypeMethods != null)
            {
                if (selectedClassTypeMethods[index].RefreshExecuteMethod != null)
                {
                    return selectedClassTypeMethods[index].RefreshExecuteMethod();
                }
                else
                    return true;
            }
            else
                return false;
        }

        public static bool Execute<T>(int index)
        {
            var selectedClassTypeMethods = GetClassTypeMethods<T>();
            if (selectedClassTypeMethods != null)
            {
                if (selectedClassTypeMethods[index].PreExecuteMethod == null && selectedClassTypeMethods[index].ExecuteMethod == null)
                {
                    return true;
                }

                if (selectedClassTypeMethods[index].PreExecuteMethod == null && selectedClassTypeMethods[index].ExecuteMethod != null)
                {
                    return selectedClassTypeMethods[index].ExecuteMethod();
                }

                if (selectedClassTypeMethods[index].PreExecuteMethod != null)
                {
                    if (selectedClassTypeMethods[index].PreExecuteMethod())
                    {
                        if (selectedClassTypeMethods[index].ExecuteMethod != null)
                            return selectedClassTypeMethods[index].ExecuteMethod();
                        else
                            return true;
                    }
                    else
                        return false;
                }
            }
            return false;
        }

        public static bool AfterExecute<T>(int index)
        {
            var selectedClassTypeMethods = GetClassTypeMethods<T>();
            if (selectedClassTypeMethods != null)
            {
                if (selectedClassTypeMethods[index].AfterExecuteMethod == null)
                {
                    return true;
                }
                return selectedClassTypeMethods[index].AfterExecuteMethod();
            }
            return false;
        }

        public static void InitializeClassTypeMethods<T>(int countDelegateMethod)
        {
            if (_tabControlMethodCollection.ContainsKey(typeof(T)))
                _tabControlMethodCollection.Remove(typeof(T));

            List<DelegateMethod> delegateMethodCollection = new List<DelegateMethod>();
            for (int i = 0; i < countDelegateMethod; i++)
            {
                DelegateMethod delegateMethod = new DelegateMethod();
                delegateMethodCollection.Add(delegateMethod);
            }
            _tabControlMethodCollection.Add(typeof(T), delegateMethodCollection);
        }

        public static void RegisterClassTypeMethod<T>(int tabIndex, RefreshExecuteMethod refreshmethod, PreExecuteMethod preMethod, ExecuteMethod method, AfterExecuteMethod afterMethod = null) where T : class
        {
            var selectedClassTypeMethods = GetClassTypeMethods<T>();
            selectedClassTypeMethods[tabIndex].RefreshExecuteMethod = refreshmethod;
            selectedClassTypeMethods[tabIndex].PreExecuteMethod = preMethod;
            selectedClassTypeMethods[tabIndex].ExecuteMethod = method;
            selectedClassTypeMethods[tabIndex].AfterExecuteMethod = afterMethod;
        }

        public static void UnregisterClassTypeMethods<T>()
        {
            _tabControlMethodCollection.Remove(typeof(T));
        }
    }
}
