using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AuxiliaryFeature.Exceptions
{
    [Serializable]
    public class BaseException : Exception, IBaseException
    {
        public BaseException() : base()
        { }

        public BaseException(string message, string description) : base()
        {
            BaseMessage = message;
            BaseDescription = description;
        }

        public BaseException(string message, string description, Exception exception)
            : base()
        {
            BaseMessage = message;
            BaseDescription = description;
            var tempException = exception as IBaseException;
            if (tempException == null)
            {
                BaseInnerException = exception;
                if (exception != null)
                    BaseStackTrace += exception.StackTrace;
            }
            else
            {
                while (tempException.BaseInnerException != null)
                {
                    if ((tempException.BaseInnerException as IBaseException) != null)
                        tempException = tempException.BaseInnerException as IBaseException;
                    else
                        break;
                }

                BaseInnerException = tempException.BaseInnerException;
                if (tempException.BaseInnerException != null)
                    BaseStackTrace += tempException.BaseInnerException.ToString();
            }
        }


        public string BaseMessage
        {
            get;
            set;
        }

        public string BaseDescription
        {
            get;
            set;
        }

        public Exception BaseInnerException
        {
            get;
            set;
        }

        public string BaseStackTrace
        {
            get;
            set;
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }
}
