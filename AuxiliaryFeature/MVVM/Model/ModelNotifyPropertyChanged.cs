using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Xml.Serialization;

namespace AuxiliaryFeature.MVVM.Model
{
    public class ModelNotifyPropertyChanged : INotifyPropertyChanged
    {
        bool isDirty = false;
        [XmlIgnore()]
        public bool IsDirty
        {
            get { return isDirty; }
            private set { isDirty = value; }
        }

        bool _isExceptionalDirtyFlag = false;
        [XmlIgnore()]
        public bool IsExceptionalDirtyFlag
        {
            get { return _isExceptionalDirtyFlag; }
            private set { _isExceptionalDirtyFlag = value; }
        }

        public void StartInitializing()
        {
            InitialeState = true;
        }

        public void StopInitializing()
        {
            InitialeState = false;
        }

        bool initialeState = false;
        [XmlIgnore()]
        public bool InitialeState
        {
            get { return initialeState; }
            private set
            {
                initialeState = value;
            }
        }

        public void SetExceptionalDirtyFlag(bool e = true)
        {
            IsExceptionalDirtyFlag = e;
        }

        //force dirty flag in executing process
        public void ForceDirtyFlag(bool s = true)
        {
            IsDirty = s;
        }

        //setting the dirty flag
        public void SetDirtyFlag(bool s = true)
        {
            if (!InitialeState)
                IsDirty = s;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = "")
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public void RefreshProperty(string propertyName = "", bool setDirty = true)
        {
            if (setDirty)
                SetDirtyFlag();
            OnPropertyChanged(propertyName);
        }

        protected void NotifyPropertyChanged<T>(Expression<Func<T>> property)
        {
            var lambda = (LambdaExpression)property;
            MemberExpression memberExpression;

            if (lambda.Body is UnaryExpression)
            {
                var unaryExpression = (UnaryExpression)lambda.Body;
                memberExpression = (MemberExpression)unaryExpression.Operand;
            }
            else
                memberExpression = (MemberExpression)lambda.Body;

            this.OnPropertyChanged(memberExpression.Member.Name);
        }

        public void RefreshProperty<T>(Expression<Func<T>> property, bool setDirty = true)
        {
            if (setDirty)
                SetDirtyFlag();
            NotifyPropertyChanged<T>(property);
        }


        protected bool SetProperty<T>(string name, ref T oldValue, T newValue) where T : System.IComparable<T>
        {
            if (oldValue == null || oldValue.CompareTo(newValue) != 0)
            {
                oldValue = newValue;
                RefreshProperty(name);
                isDirty = true;
                return true;
            }
            return false;
        }
    }
}
