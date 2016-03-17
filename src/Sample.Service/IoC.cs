using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Autofac;
using Autofac.Builder;
using Serilog;

namespace Sample.Service
{
    public static class IoC
    {
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Io")]
        public static IContainer LetThereBeIoC(ContainerBuildOptions containerBuildOptions = ContainerBuildOptions.None, Action<ContainerBuilder> preHooks = null)
        {
            var builder = new ContainerBuilder();

            builder.RegisterAssemblyModules(Constants.ServiceAssembly);

            try
            {
                var sw = Stopwatch.StartNew();
                if (preHooks != null) preHooks(builder);
                var container = builder.Build(containerBuildOptions);
                sw.Stop();
                container.Resolve<ILogger>().Information("Container built in {ElapsedTime} seconds", sw.Elapsed.TotalSeconds);
                return container;
            }
            catch (Exception exc)
            {
                if (Debugger.IsAttached)
                    Debugger.Break();

                Log.Fatal(exc, "Container failed to build");
            }

            return null;
        }
    }
}