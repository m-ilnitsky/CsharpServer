using Topshelf;
using Topshelf.Ninject;
using Quartz;
using Topshelf.Quartz;
using Topshelf.Quartz.Ninject;

namespace L8_Task1_Job_v2
{
    class Program
    {
        static void Main()
        {
            HostFactory.Run(c =>
            {
                c.UseNinject(new HelloJobModule());

                c.Service<HelloService>(s =>
                {
                    // Topshelf.Quartz (Optional) - Construct service using Ninject
                    s.ConstructUsingNinject();

                    s.WhenStarted((service, control) =>
                    {
                        service.Start();
                        return true;
                    });
                    s.WhenStopped((service, control) =>
                    {
                        service.Stop();
                        return true;
                    });

                    // Topshelf.Quartz.Ninject (Optional) - Construct IJob instance with Ninject
                    s.UseQuartzNinject();

                    // Schedule a job to run in the background every 5 seconds.
                    // The full Quartz Builder framework is available here.
                    s.ScheduleQuartzJob(q =>
                        q.WithJob(() =>
                            JobBuilder.Create<HelloJob>().Build())
                            .AddTrigger(() =>
                                TriggerBuilder.Create()
                                    .WithSimpleSchedule(builder => builder.WithIntervalInSeconds(3).RepeatForever()).Build())
                        );
                });
            });
        }
    }
}
