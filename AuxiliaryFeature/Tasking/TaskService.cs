using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AuxiliaryFeature.Tasking
{
    public class TaskService
    {
        CancellationToken token;
        TaskScheduler taskScheduler;

        public TaskService(CancellationTokenSource cancellationTokenSource, TaskScheduler ts = null)
        {
            token = cancellationTokenSource.Token;
            if (ts == null)
                taskScheduler = TaskScheduler.FromCurrentSynchronizationContext();
            else
                taskScheduler = ts;
        }

        public Task Create<TIn, TOut>(Func<TIn, TOut> methodToRunAction, TIn par, Action<TOut> methodCompletedAction, Action<Exception> methodExceptionAction = null, Action<object> methodCancelledAction = null)
        {
            if (methodToRunAction == null)// || methodCompletedAction == null)
                return null;

            Task<TOut> taskRun = new Task<TOut>(x => methodToRunAction(par), new CancellationTokenSource());

            Task taskCompleted = null;
            if (methodCompletedAction != null)
            {
                taskCompleted = taskRun.ContinueWith
                (
                    x => methodCompletedAction(taskRun.Result), token, TaskContinuationOptions.OnlyOnRanToCompletion, TaskScheduler.FromCurrentSynchronizationContext()
                );
            }

            AddTaskContinuationOnException(taskRun, methodExceptionAction, methodCancelledAction);

            return taskRun;
        }

        public Task Create<TIn>(Action<TIn> methodToRunAction, TIn par, Action methodCompletedAction, Action<Exception> methodExceptionAction = null, Action<object> methodCancelledAction = null)
        {
            if (methodToRunAction == null) // || methodCompletedAction == null)
                return null;

            Task taskRun = new Task(x => methodToRunAction(par), new CancellationTokenSource());

            Task taskCompleted = null;
            if (methodCompletedAction != null)
            {
                taskCompleted = taskRun.ContinueWith
                (
                    x => methodCompletedAction(), token, TaskContinuationOptions.OnlyOnRanToCompletion, TaskScheduler.FromCurrentSynchronizationContext()
                );
            }

            AddTaskContinuationOnException(taskRun, methodExceptionAction, methodCancelledAction);

            return taskRun;
        }

        //test
        public Task Create<TOut>(Func<TOut> methodToRunAction, Action<TOut> methodCompletedAction, Action<Exception> methodExceptionAction = null, Action<object> methodCancelledAction = null)
        {
            if (methodToRunAction == null) // || methodCompletedAction == null)
                return null;

            Task<TOut> taskRun = new Task<TOut>(x => methodToRunAction(), new CancellationTokenSource());

            Task taskCompleted = null;
            if (methodCompletedAction != null)
            {
                taskCompleted = taskRun.ContinueWith
                (
                    x => methodCompletedAction(taskRun.Result), token, TaskContinuationOptions.OnlyOnRanToCompletion, TaskScheduler.FromCurrentSynchronizationContext()
                );
            }

            AddTaskContinuationOnException(taskRun, methodExceptionAction, methodCancelledAction);

            return taskRun;
        }

        public Task Create(Action methodToRunAction, Action methodCompletedAction, Action<Exception> methodExceptionAction = null,
            Action<object> methodCancelledAction = null)
        {
            if (methodToRunAction == null)
                return null;

            Task taskRun = new Task(x => methodToRunAction(), new CancellationTokenSource());

            Task taskCompleted = null;
            if (methodCompletedAction != null)
            {
                taskCompleted = taskRun.ContinueWith
                (
                    x => methodCompletedAction(), token, TaskContinuationOptions.OnlyOnRanToCompletion, TaskScheduler.FromCurrentSynchronizationContext()
                );
            }

            AddTaskContinuationOnException(taskRun, methodExceptionAction, methodCancelledAction);

            return taskRun;
        }

        public Task Create<TIn1, TIn2, TOut>(Func<TIn1, TIn2, TOut> methodToRunAction, TIn1 par1, TIn2 par2, Action<TOut> methodCompletedAction, Action<Exception> methodExceptionAction = null, Action<object> methodCancelledAction = null)
        {
            if (methodToRunAction == null) // || methodCompletedAction == null)
                return null;

            Task<TOut> taskRun = new Task<TOut>(x => methodToRunAction(par1, par2), new CancellationTokenSource());

            Task taskCompleted = null;
            if (methodCompletedAction != null)
            {
                taskCompleted = taskRun.ContinueWith
                (
                    x => methodCompletedAction(taskRun.Result), token, TaskContinuationOptions.OnlyOnRanToCompletion, TaskScheduler.FromCurrentSynchronizationContext()
                );
            }

            AddTaskContinuationOnException(taskRun, methodExceptionAction, methodCancelledAction);

            return taskRun;
        }

        public Task Create<TIn1, TIn2, TIn3, TOut>(Func<TIn1, TIn2, TIn3, TOut> methodToRunAction, TIn1 par1, TIn2 par2, TIn3 par3, Action<TOut> methodCompletedAction, Action<Exception> methodExceptionAction = null, Action<object> methodCancelledAction = null)
        {
            if (methodToRunAction == null) // || methodCompletedAction == null)
                return null;

            Task<TOut> taskRun = new Task<TOut>(x => methodToRunAction(par1, par2, par3), new CancellationTokenSource());

            Task taskCompleted = null;
            if (methodCompletedAction != null)
            {
                taskCompleted = taskRun.ContinueWith
                (
                    x => methodCompletedAction(taskRun.Result), token, TaskContinuationOptions.OnlyOnRanToCompletion, TaskScheduler.FromCurrentSynchronizationContext()
                );
            }

            AddTaskContinuationOnException(taskRun, methodExceptionAction, methodCancelledAction);
            return taskRun;
        }

        public Task Create<TIn1, TIn2, TIn3, TIn4, TOut>(Func<TIn1, TIn2, TIn3, TIn4, TOut> methodToRunAction, TIn1 par1, TIn2 par2, TIn3 par3, TIn4 par4, Action<TOut> methodCompletedAction, Action<Exception> methodExceptionAction = null, Action<object> methodCancelledAction = null)
        {
            if (methodToRunAction == null)// || methodCompletedAction == null)
                return null;

            Task<TOut> taskRun = new Task<TOut>(x => methodToRunAction(par1, par2, par3, par4), new CancellationTokenSource());

            Task taskCompleted = null;
            if (methodCompletedAction != null)
            {
                taskCompleted = taskRun.ContinueWith
                (
                    x => methodCompletedAction(taskRun.Result), token, TaskContinuationOptions.OnlyOnRanToCompletion, TaskScheduler.FromCurrentSynchronizationContext()
                );
            }

            AddTaskContinuationOnException(taskRun, methodExceptionAction, methodCancelledAction);
            return taskRun;
        }

        void AddTaskContinuationOnException(Task task, Action<Exception> methodExceptionAction, Action<object> methodCancelledAction)
        {
            Task taskException = null;
            if (methodExceptionAction != null)
            {
                taskException = task.ContinueWith
                (
                    x => methodExceptionAction(x.Exception.Flatten()), token, TaskContinuationOptions.OnlyOnFaulted, TaskScheduler.Current
                );
            }

            //Task taskCancelled = null;
            //if (methodCancelledAction != null)
            //{
            //    taskCancelled = task.ContinueWith
            //    (
            //        x => methodCancelledAction(x), token, TaskContinuationOptions.OnlyOnCanceled, TaskScheduler.Current 
            //    );
            //}
            Task taskCancelled = null;
            if (methodCancelledAction != null)
            {
                taskCancelled = task.ContinueWith
                (
                     x => methodCancelledAction(x), token, TaskContinuationOptions.OnlyOnFaulted, TaskScheduler.Current
                );
            }
        }
    }
}
