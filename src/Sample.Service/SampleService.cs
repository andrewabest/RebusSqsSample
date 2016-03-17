using System;
using System.Linq;
using Autofac;
using FluentScheduler;
using FluentScheduler.Model;
using Sample.Service.Infrastructure.Scheduler;
using Serilog;

namespace Sample.Service
{
    public class SampleService 
    {
        private IContainer _container;

        public SampleService Start()
        {
            _container = IoC.LetThereBeIoC();

            Log.Information("Registering schedules");
            TaskManager.UnobservedTaskException += TaskManager_UnobservedTaskException;
            TaskManager.TaskEnd += (s, e) => LogNextRunTime();
            TaskManager.TaskFactory = _container.Resolve<AutofacTaskFactory>();
            TaskManager.Initialize(_container.Resolve<RegistryProvider>());
            
            LogNextRunTimeForAllSchedules();

            return this;
        }

        public bool Stop()
        {
            var container = _container;
            container?.Dispose();
            _container = null;

            return true;
        }

        private static void LogNextRunTime()
        {
            var nextToRun = TaskManager.AllSchedules.OrderBy(x => x.NextRunTime).First();
            Log.Information("Next schedule {Schedule} will run at {NextRunTime}", nextToRun.Name, nextToRun.NextRunTime);
        }

        private static void LogNextRunTimeForAllSchedules()
        {
            foreach (var nextToRun in TaskManager.AllSchedules.OrderBy(x => x.NextRunTime))
            {
                Log.Information("Next schedule {Schedule} will run at {NextRunTime}", nextToRun.Name, nextToRun.NextRunTime);
            }
        }

        private void TaskManager_UnobservedTaskException(TaskExceptionInformation sender, UnhandledExceptionEventArgs e)
        {
            var template = "An error happened with a scheduled task";

            var exception = e.ExceptionObject as Exception;
            if (exception != null)
            {
                Log.Fatal(exception, template);
            }
            else
            {
                Log.Fatal(template + ": " + e.ExceptionObject);
            }
        }
    }
}