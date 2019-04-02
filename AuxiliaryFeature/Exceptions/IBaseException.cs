using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuxiliaryFeature.Exceptions
{
    public interface IBaseException
    {
        string BaseMessage { get; set; }
        string BaseDescription { get; set; }

        string BaseStackTrace { get; set; }
        Exception BaseInnerException { get; set; }
    }
}
