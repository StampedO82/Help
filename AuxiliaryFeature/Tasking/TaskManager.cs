using AuxiliaryFeature.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace AuxiliaryFeature.Tasking
{
    public static class TaskManager
    {
        static Queue<Task> taskCollection = new Queue<Task>();
        static Queue<Task> TaskCollection
        {
            get
            {
                lock (lockObjTaskCollection)
                {
                    return taskCollection;
                }
            }
            set { taskCollection = value; }
        }

        static CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        static TaskScheduler taskScheduler = null;
        static Dispatcher disp = null;

        public static void SetTaskServiceProperties(CancellationTokenSource cts, TaskScheduler ts, Dispatcher di = null)
        {
            cancellationTokenSource = cts;
            taskScheduler = ts;
            disp = di;
        }

        public static void CancelTasks()
        {
            cancellationTokenSource.Cancel();
        }

        public static void ThrowIfCancellationRequested()
        {
            if (cancellationTokenSource.IsCancellationRequested)
                cancellationTokenSource.Token.ThrowIfCancellationRequested();
        }

        static object lockObjTaskRunning = new object();
        static object lockObjTaskCollection = new object();

        static bool tasksRunning = false;
        static bool TaskRunning
        {
            get
            {   //??????
                lock (lockObjTaskRunning)
                {
                    return tasksRunning;
                }
            }
            set { tasksRunning = value; }
        }

        #region public static Task Create<TOut>(Func<TOut> methodToRunAction, Action<TOut> methodCompletedAction, Action<Exception> methodExceptionAction = null, Action<object> methodCancelledAction = null)
        public static Task Create<TOut>(Func<TOut> methodToRunAction, Action<TOut> methodCompletedAction, Action<Exception> methodExceptionAction = null, Action<object> methodCancelledAction = null)
        {
            TaskService taskService = new TaskService(cancellationTokenSource, taskScheduler);
            Task newTask = taskService.Create(methodToRunAction, methodCompletedAction, methodExceptionAction, methodCancelledAction);
            if (newTask != null)
                AddTask(newTask);
            return newTask;
        }

        public static bool CreateAndRun<TOut>(Func<TOut> methodToRunAction, Action<TOut> methodCompletedAction, Action<Exception> methodExceptionAction = null, Action<object> methodCancelledAction = null)
        {
            Create(methodToRunAction, methodCompletedAction, methodExceptionAction, methodCancelledAction);
            if (!TaskRunning)
                return Run(null);
            return false;
        }
        #endregion

        #region public static Task Create<TIn, TOut>(Func<TIn, TOut> methodToRunAction, TIn par, Action<TOut> methodCompletedAction, Action<Exception> methodExceptionAction = null, Action<object> methodCancelledAction = null)
        public static Task Create<TIn, TOut>(Func<TIn, TOut> methodToRunAction, TIn par, Action<TOut> methodCompletedAction, Action<Exception> methodExceptionAction = null, Action<object> methodCancelledAction = null)
        {
            TaskService taskService = new TaskService(cancellationTokenSource, taskScheduler);
            Task newTask = taskService.Create(methodToRunAction, par, methodCompletedAction, methodExceptionAction, methodCancelledAction);
            if (newTask != null)
                AddTask(newTask);
            return newTask;
        }

        public static bool CreateAndRun<TIn, TOut>(Func<TIn, TOut> methodToRunAction, TIn par, Action<TOut> methodCompletedAction, Action<Exception> methodExceptionAction = null, Action<object> methodCancelledAction = null)
        {
            Create<TIn, TOut>(methodToRunAction, par, methodCompletedAction, methodExceptionAction, methodCancelledAction);
            if (!TaskRunning)
                return Run(null);
            return false;
        }
        #endregion

        #region public static Task Create<TIn>(Action<TIn> methodToRunAction, TIn par, Action methodCompletedAction, Action<Exception> methodExceptionAction = null, Action<object> methodCancelledAction = null)
        public static Task Create<TIn>(Action<TIn> methodToRunAction, TIn par, Action methodCompletedAction, Action<Exception> methodExceptionAction = null, Action<object> methodCancelledAction = null)
        {
            TaskService taskService = new TaskService(cancellationTokenSource, taskScheduler);
            Task newTask = taskService.Create(methodToRunAction, par, methodCompletedAction, methodExceptionAction, methodCancelledAction);
            if (newTask != null)
                AddTask(newTask);
            return newTask;
        }
        public static bool CreateAndRun<TIn>(Action<TIn> methodToRunAction, TIn par, Action methodCompletedAction, Action<Exception> methodExceptionAction = null, Action<object> methodCancelledAction = null)
        {
            Create<TIn>(methodToRunAction, par, methodCompletedAction, methodExceptionAction, methodCancelledAction);
            if (!TaskRunning)
                return Run(null);
            return false;
        }
        #endregion

        #region public Task Create(Action methodToRunAction, Action methodCompletedAction, Action<Exception> methodExceptionAction = null, Action<object> methodCancelledAction = null)
        public static Task Create(Action methodToRunAction, Action methodCompletedAction, Action<Exception> methodExceptionAction = null, Action<object> methodCancelledAction = null)
        {
            TaskService taskService = new TaskService(cancellationTokenSource, taskScheduler);
            Task newTask = taskService.Create(methodToRunAction, methodCompletedAction, methodExceptionAction, methodCancelledAction);
            if (newTask != null)
                AddTask(newTask);
            return newTask;
        }

        public static bool CreateAndRun(Action methodToRunAction, Action methodCompletedAction, Action<Exception> methodExceptionAction = null, Action<object> methodCancelledAction = null)
        {
            Create(methodToRunAction, methodCompletedAction, methodExceptionAction, methodCancelledAction);
            if (!TaskRunning)
                return Run(null);
            return false;
        }
        #endregion

        #region public Task Create<TIn1, TIn2, TOut>(Func<TIn1, TIn2, TOut> methodToRunAction, TIn1 par1, TIn2 par2, Action<TOut> methodCompletedAction, Action<Exception> methodExceptionAction = null, Action<object> methodCancelledAction = null)
        public static Task Create<TIn1, TIn2, TOut>(Func<TIn1, TIn2, TOut> methodToRunAction, TIn1 par1, TIn2 par2, Action<TOut> methodCompletedAction, Action<Exception> methodExceptionAction = null, Action<object> methodCancelledAction = null)
        {
            TaskService taskService = new TaskService(cancellationTokenSource, taskScheduler);
            Task newTask = taskService.Create(methodToRunAction, par1, par2, methodCompletedAction, methodExceptionAction, methodCancelledAction);
            if (newTask != null)
                AddTask(newTask);
            return newTask;
        }

        public static bool CreateAndRun<TIn1, TIn2, TOut>(Func<TIn1, TIn2, TOut> methodToRunAction, TIn1 par1, TIn2 par2, Action<TOut> methodCompletedAction, Action<Exception> methodExceptionAction = null, Action<object> methodCancelledAction = null)
        {
            Create<TIn1, TIn2, TOut>(methodToRunAction, par1, par2, methodCompletedAction, methodExceptionAction = null, methodCancelledAction = null);
            if (!TaskRunning)
                return Run(null);
            return false;
        }

        #endregion

        #region  public Task Create<TIn1, TIn2, TIn3, TOut>(Func<TIn1, TIn2, TIn3, TOut> methodToRunAction, TIn1 par1, TIn2 par2, TIn3 par3, Action<TOut> methodCompletedAction, Action<Exception> methodExceptionAction = null, Action<object> methodCancelledAction = null)
        public static Task Create<TIn1, TIn2, TIn3, TOut>(Func<TIn1, TIn2, TIn3, TOut> methodToRunAction, TIn1 par1, TIn2 par2, TIn3 par3, Action<TOut> methodCompletedAction, Action<Exception> methodExceptionAction = null, Action<object> methodCancelledAction = null)
        {
            TaskService taskService = new TaskService(cancellationTokenSource, taskScheduler);
            Task newTask = taskService.Create(methodToRunAction, par1, par2, par3, methodCompletedAction, methodExceptionAction, methodCancelledAction);
            if (newTask != null)
                AddTask(newTask);
            return newTask;
        }

        public static bool CreateAndRun<TIn1, TIn2, TIn3, TOut>(Func<TIn1, TIn2, TIn3, TOut> methodToRunAction, TIn1 par1, TIn2 par2, TIn3 par3, Action<TOut> methodCompletedAction, Action<Exception> methodExceptionAction = null, Action<object> methodCancelledAction = null)
        {
            Create<TIn1, TIn2, TIn3, TOut>(methodToRunAction, par1, par2, par3, methodCompletedAction, methodExceptionAction, methodCancelledAction);
            if (!TaskRunning)
                return Run(null);
            return false;
        }
        #endregion

        #region public static Task Create<TIn1, TIn2, TIn3, TIn4, TOut>(Func<TIn1, TIn2, TIn3, TIn4, TOut> methodToRunAction, TIn1 par1, TIn2 par2, TIn3 par3, TIn4 par4, Action<TOut> methodCompletedAction, Action<Exception> methodExceptionAction = null, Action<object> methodCancelledAction = null)
        public static Task Create<TIn1, TIn2, TIn3, TIn4, TOut>(Func<TIn1, TIn2, TIn3, TIn4, TOut> methodToRunAction, TIn1 par1, TIn2 par2, TIn3 par3, TIn4 par4, Action<TOut> methodCompletedAction, Action<Exception> methodExceptionAction = null, Action<object> methodCancelledAction = null)
        {
            TaskService taskService = new TaskService(cancellationTokenSource, taskScheduler);
            Task newTask = taskService.Create(methodToRunAction, par1, par2, par3, par4, methodCompletedAction, methodExceptionAction, methodCancelledAction);
            if (newTask != null)
                AddTask(newTask);
            return newTask;
        }

        public static bool CreateAndRun<TIn1, TIn2, TIn3, TIn4, TOut>(Func<TIn1, TIn2, TIn3, TIn4, TOut> methodToRunAction, TIn1 par1, TIn2 par2, TIn3 par3, TIn4 par4, Action<TOut> methodCompletedAction, Action<Exception> methodExceptionAction = null, Action<object> methodCancelledAction = null)
        {
            Create<TIn1, TIn2, TIn3, TIn4, TOut>(methodToRunAction, par1, par2, par3, par4, methodCompletedAction, methodExceptionAction, methodCancelledAction);
            if (!TaskRunning)
                return Run(null);
            return false;
        }
        #endregion

        static void AddTask(Task task)
        {
            if (task != null && !TaskCollection.Contains(task))
            {
                TaskCollection.Enqueue(task);
            }
        }

        public static bool Run(Action onFinished = null, bool stopOnException = true, Action onCancel = null)
        {
            TaskRunning = true;
            while (TaskCollection.Count() > 0)
            {
                Task task = TaskCollection.First();
                if (disp != null)
                {
                    if (disp != Dispatcher.CurrentDispatcher)
                        disp.BeginInvoke(new Action(() => task.Start()));
                    else
                        disp.Invoke(new Action(() => task.Start()));
                }
                else
                    task.Start();

                try
                {
                    task.WaitWithPumping();
                    TaskCollection.Dequeue();
                }
                catch (AggregateException)
                {
                    if (task.Status == TaskStatus.Canceled || task.Status == TaskStatus.Faulted)
                    {
                        TaskRunning = false;
                        TaskCollection.Clear();
                        //if (onCancel != null)
                        //    onCancel();
                    }
                }
                if (!TaskRunning && stopOnException)
                {
                    if (onCancel != null)
                        onCancel();
                    return false;
                }
            }
            TaskRunning = false;

            onFinished?.Invoke();

            return true;
        }

        static ParallelOptions options = new ParallelOptions()
        {
            CancellationToken = cancellationTokenSource.Token
        };

        public static void RunParallel(Action onFinished = null, bool stopOnException = true)
        {
            TaskRunning = true;
            try
            {
                ParallelLoopResult res1 = Parallel.ForEach(TaskCollection, options, (task) =>
                {
                    task.Start();
                    task.WaitWithPumping();
                });
            }
            catch (OperationCanceledException)
            {
                TaskRunning = false;
                Console.WriteLine("Operation Cancelled");
            }
            catch (AggregateException aggEx)
            {
                TaskRunning = false;
                foreach (Exception ex in aggEx.InnerExceptions)
                {
                    Console.WriteLine(string.Format("Caught exception '{0}'", ex.Message));
                }
            }

            TaskCollection = new Queue<Task>();

            TaskRunning = false;

            onFinished?.Invoke();
        }
    }
}
