using AuxiliaryFeature.Exceptions;
using AuxiliaryFeature.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AuxiliaryFeature.Utility
{
    public static class MvvmManager
    {
        //public static Dispatcher STADispatcher;
        public static Window Window { get; set; }
        static Dictionary<Type, object> registeredViews = new Dictionary<Type, object>();
        static Dictionary<Type, object> registeredViewModels = new Dictionary<Type, object>();

        public static void RegisterView<T>(T obj)
        {
            string viewTypeName = "not defined";
            try
            {
                if (!registeredViews.ContainsKey(typeof(T)))
                {
                    registeredViews.Add(typeof(T), obj);
                }
                else
                {
                    T view = FindView<T>();
                    obj = view;
                }
            }
            catch (Exception exc)
            {
                viewTypeName = typeof(T).Name;
                throw new BaseException("Failed to register view " + viewTypeName, "Unable to register view.", exc);
            }
        }

        public static void UnRegisterView<T>()
        {
            string viewTypeName = "not defined";
            try
            {
                if (registeredViews.ContainsKey(typeof(T)))
                {
                    registeredViews.Remove(typeof(T));
                }
            }
            catch (Exception exc)
            {
                viewTypeName = typeof(T).Name;
                throw new BaseException("Failed to remove view " + viewTypeName, "Unable to remove view", exc);
            }
        }

        static T FindView<T>()
        {
            string viewTypeName = "not defined";
            try
            {
                var result = FindObject<T>(registeredViews);
                if (result == null)
                {
                    RegisterView<T>(default(T));
                    result = FindObject<T>(registeredViews);
                }
                return result;
            }
            catch (Exception exc)
            {
                viewTypeName = typeof(T).Name;
                throw new BaseException("Unable to find view " + viewTypeName, "View does not exist.", exc);
            }
        }

        /// <summary>
        /// Method contains all registered view models. If view model is already registered, its reference in collection is
        /// set to income parameter (view model)
        /// </summary>
        /// <typeparam name="T">Type T of targeted view model</typeparam>
        /// <param name="obj">Object of type T</param>
        public static T RegisterViewModel<T>(T obj = default(T), bool overrideModelObj = false)
        {
            string viewModelTypeName = "not defined";
            try
            {
                if (!registeredViewModels.ContainsKey(typeof(T)))
                {
                    registeredViewModels.Add(typeof(T), obj);
                }
                else
                {
                    if (overrideModelObj)
                    {
                        registeredViewModels[typeof(T)] = obj;
                    }
                    else
                    {
                        T viewModel = FindViewModel<T>();
                        obj = viewModel;
                    }
                }
                return (T)obj;
            }
            catch (Exception exc)
            {
                viewModelTypeName = typeof(T).Name;
                throw new BaseException("Failed to register model " + viewModelTypeName, "Unable to register model.", exc);
            }
        }

        public static void UnRegisterViewModel<T>()
        {
            string viewModelTypeName = "not defined";
            try
            {
                if (registeredViewModels.ContainsKey(typeof(T)))
                {
                    registeredViewModels.Remove(typeof(T));
                }
            }
            catch (Exception exc)
            {
                viewModelTypeName = typeof(T).Name;
                throw new BaseException("Failed to remove model " + viewModelTypeName, "Unable to remove model.", exc);
            }
        }

        static T FindViewModel<T>(params object[] list)
        {
            var result = FindObject<T>(registeredViewModels, list);
            return result;
        }

        public static void ShowAsDialog<T1, T2>(params object[] list)
        {
            Window myView = null;
            try
            {

                myView = GetView<T1, T2>(list);
                myView.ShowDialog();
            }
            catch (System.InvalidOperationException)
            {
                if (myView != null)
                {
                    myView.Close();
                    myView = null;
                }
            }
        }

        public static void ShowAsView<T1, T2>(params object[] list)
        {
            Window myView = GetView<T1, T2>(list);
            myView.Visibility = Visibility.Visible;
            //myView.Topmost = true;
        }

        public static Window SetFocusOnView<T1>()
        {
            T1 view = FindView<T1>();
            if (view != null)
            {
                (view as Window).Focus();
                return view as Window;
            }
            else
                return null;
        }

        static Window GetView<T1, T2>(params object[] list)
        {
            string viewTypeName = "not defined";
            string viewModelTypeName = "not defined";
            try
            {
                T1 view = FindView<T1>();
                T2 viewModel = FindViewModel<T2>(list);
                if (viewModel == null)
                {
                    RegisterViewModel<T2>(default(T2));
                    viewModel = FindObject<T2>(registeredViewModels, list);
                }
                (view as Window).DataContext = viewModel;
                return (view as Window);
            }
            catch (Exception exc)
            {
                viewTypeName = typeof(T1).Name;
                viewModelTypeName = typeof(T2).Name;
                throw new BaseException("Failed to register view " + viewTypeName + " with model " + viewModelTypeName, "Unable to register view with model.", exc);
            }
        }

        public static T GetViewModel<T>(params object[] list)
        {
            string viewModelTypeName = "not defined";
            try
            {
                return FindViewModel<T>(list);
            }
            catch (Exception exc)
            {
                viewModelTypeName = typeof(T).Name;
                throw new BaseException("Unable to find model " + viewModelTypeName, "Unable to find view model.", exc);
            }
        }

        public static void CloseView<T1, T2>(bool removeViewModel = true)
        {
            string viewTypeName = "not defined";
            string viewModelTypeName = "not defined";
            try
            {
                T1 view = FindView<T1>();
                if (view != null)
                {
                    Window window = view as Window;
                    T2 viewModel = (T2)window.DataContext;
                    window.Close();
                    UnRegisterView<T1>();
                    //UnRegisterView<T1>(view);
                    if (removeViewModel)
                        UnRegisterViewModel<T2>();
                    //UnRegisterViewModel<T2>(viewModel);
                }
            }
            catch (Exception exc)
            {
                viewTypeName = typeof(T1).Name;
                viewModelTypeName = typeof(T2).Name;
                throw new BaseException("Unable to close " + viewTypeName + " with model " + viewModelTypeName, "Unable to close view.", exc);
            }
        }

        #region Object Logic
        static T FindObject<T>(Dictionary<Type, object> objectList, params object[] list)
        {
            if (objectList.ContainsKey(typeof(T)))
            {
                if (objectList[typeof(T)] != null)
                    return (T)objectList[typeof(T)];
                else
                {
                    return InstantiateObject<T>(objectList, list);
                }
            }
            return default(T);
        }

        static T InstantiateObject<T>(Dictionary<Type, object> objectList, params object[] list)
        {
            var obj = InstantiateObject<T>(list);
            if (obj != null)
                objectList[typeof(T)] = obj;
            return obj;
        }

        static T InstantiateObject<T>(params object[] list)
        {
            Type currentType = typeof(T);
            if (!currentType.IsInterface)
            {
                return CreateObject<T>(currentType, list);
            }
            else
            {
                return CreateObject<T>(FindTypeThatImplementInterface(currentType), list);
            }
        }

        static Type FindTypeThatImplementInterface(Type currentInterface)
        {
            var types = AppDomain.CurrentDomain.GetAssemblies().ToList()
                                .SelectMany(s => s.GetTypes())
                                .Where(p => currentInterface.IsAssignableFrom(p) && currentInterface.Name.Contains(p.Name) && !p.IsInterface);
            if (types == null)
                return null;
            else
                return types.First();
        }

        static T CreateObject<T>(Type objectType, params object[] list)
        {
            if (objectType == null)
                return default(T);

            object targetObject = null;
            if (list.Count() == 0)
            {
                targetObject = InvokeParameterlessObjectConstructor(objectType);
            }
            else
            {
                targetObject = InvokeObjectConstructor(objectType, list);
            }

            return (T)targetObject;
        }

        static object InvokeParameterlessObjectConstructor(Type objectType)
        {
            object targetObject = null;
            ConstructorInfo objectConstructorParameterless = objectType.GetConstructor(Type.EmptyTypes);
            targetObject = objectConstructorParameterless.Invoke(new object[] { });
            return targetObject;
        }

        static object InvokeObjectConstructor(Type objectType, params object[] list)
        {
            object targetObject = null;
            ConstructorInfo[] objectConstructors = objectType.GetConstructors();
            var objectParamsConstructors = objectConstructors.Where(c => c.GetParameters().Count() == list.Count());

            ConstructorInfo targetConstructor = null;

            foreach (var constructor in objectParamsConstructors)
            {
                int index = 0;
                bool found = true;
                foreach (var par in constructor.GetParameters())
                {
                    Type currentType = null;
                    var v = list[index];
                    if (v != null)
                    {
                        currentType = v.GetType();
                        if (currentType != par.ParameterType)
                        {
                            found = false;

                            if (par.ParameterType.IsInterface)
                            {
                                //check if expected type implements parameter interface
                                found = currentType.GetInterface(par.ParameterType.FullName, true) != null ? true : false;
                            }
                            else
                            {
                                //check if expected type is inherited by parameter type
                                found = currentType.IsSubclassOf(par.ParameterType);
                            }

                            if (!found)
                                break;
                        }
                    }
                    index++;
                }

                if (found)
                {
                    targetConstructor = constructor;
                    break;
                }
            }

            if (targetConstructor != null)
            {
                targetObject = targetConstructor.Invoke(list);
            }

            return targetObject;
        }
        #endregion

        public static string GetWindowPath()
        {
            var stringPath = string.Empty;
            foreach (var regView in registeredViews)
            {
                if (regView.Value is Window && (regView.Value as Window).DataContext != null)
                {
                    if ((regView.Value as Window).DataContext is IViewModel)
                    {
                        stringPath += (regView.Value as Window).Name + ": " + ((regView.Value as Window).DataContext as IViewModel).Name;
                        if (registeredViews.Last().Value != regView.Value)
                            stringPath += "  >>  ";
                    }
                }
            }
            return stringPath;
        }

        public static void MinimizeAll()
        {
            foreach (var regView in registeredViews)
            {
                if (regView.Value is Window && (regView.Value as Window).DataContext != null)
                {
                    (regView.Value as Window).WindowState = WindowState.Minimized;
                }
            }
        }
    }

}
