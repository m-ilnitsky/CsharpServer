using Quartz;
using Topshelf;
using Topshelf.Ninject;
using Topshelf.Quartz;
using Topshelf.Quartz.Ninject;

namespace L8_Task1_Job_v1
{
    class Program
    {
        static void Main()
        {
            HostFactory.Run(c =>
            {
                // Topshelf.Ninject (Optional) - Initiates Ninject and consumes Modules
                c.UseNinject(new HelloJobModule());

                c.UseQuartzNinject();

                c.ScheduleQuartzJobAsService(q =>
                        q.WithJob(() =>
                            JobBuilder.Create<HelloJob>().Build())
                        .AddTrigger(() =>
                            TriggerBuilder.Create()
                                .WithSimpleSchedule(builder => builder
                                    .WithIntervalInSeconds(3)
                                    .RepeatForever())
                                .Build()));
            });
        }
    }
}
