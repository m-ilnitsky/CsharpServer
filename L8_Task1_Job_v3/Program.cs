using System;
using Topshelf;
using Topshelf.Ninject;

namespace L8_Task1_Job_v3
{
    class Program
    {
        static void Main()
        {
            var rc = HostFactory.Run(c =>
            {
                // Topshelf.Ninject (Optional) - Initiates Ninject and consumes Modules
                c.UseNinject(new HelloJobModule());

                c.Service<HelloService>(s =>
                {
                    //Topshelf.Ninject (Optional) - Construct service using Ninject
                    s.ConstructUsingNinject();

                    s.WhenStarted((service, control) =>
                    {
                        service.Start().Wait();
                        return true;
                    });
                    s.WhenStopped((service, control) =>
                    {
                        service.Stop().Wait();
                        return true;
                    });
                });

                c.RunAsLocalSystem();

                c.SetDescription("Sample Topshelf Host for Hello World");
                c.SetDisplayName("HelloJob v.3 DisplayName");
                c.SetServiceName("HelloJob v.3 ServiceName");
            });

            var exitCode = (int)Convert.ChangeType(rc, rc.GetTypeCode());
            Environment.ExitCode = exitCode;
        }
    }
}
