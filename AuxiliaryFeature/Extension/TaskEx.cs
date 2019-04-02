using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace AuxiliaryFeature.Extension
{
    public static class TaskEx
    {
        //http://blogs.planetsoftware.com.au/paul/archive/2010/12/05/waiting-for-a-task-donrsquot-block-the-main-ui-thread.aspx
        public static void WaitWithPumping(this Task task, TaskScheduler taskScheduler = null)
        {

            if (task == null) throw new ArgumentNullException("task");

            var nestedFrame = new DispatcherFrame();

            if (taskScheduler == null)
                task.ContinueWith(_ => nestedFrame.Continue = false);
            else
                task.ContinueWith(_ => nestedFrame.Continue = false, taskScheduler);

            Dispatcher.PushFrame(nestedFrame);

            task.Wait();

        }
    }
}
