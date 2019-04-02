using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuxiliaryFeature.Utility
{
    public delegate bool RefreshExecuteMethod();
    public delegate bool PreExecuteMethod();
    public delegate bool ExecuteMethod();
    public delegate bool AfterExecuteMethod();

    public class DelegateMethod
    {
        public DelegateMethod()
        {

        }

        public RefreshExecuteMethod RefreshExecuteMethod { get; set; }
        public PreExecuteMethod PreExecuteMethod { get; set; }
        public ExecuteMethod ExecuteMethod { get; set; }
        public AfterExecuteMethod AfterExecuteMethod { get; set; }
    }
}
