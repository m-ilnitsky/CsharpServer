using Ninject.Modules;
using Quartz;
using Quartz.Impl;
using System.Collections.Specialized;

namespace L8_Task1_Job_v2
{
    public class HelloJobModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IJobDetail>().ToMethod(ctx => JobBuilder.Create<HelloJob>()
                    .WithIdentity("HelloJob", "HelloJobGroup")
                    .Build());

            Bind<ITrigger>().ToMethod(ctx => TriggerBuilder.Create()
                    .WithIdentity("HelloJobTrigger", "HelloJobGroup")
                    .StartNow()
                    .WithSimpleSchedule(x => x
                        .WithIntervalInSeconds(3)
                        .RepeatForever())
                    .Build());

            Bind<StdSchedulerFactory>().ToMethod(ctx => new StdSchedulerFactory(
                new NameValueCollection
                {
                    { "quartz.serializer.type", "binary" }
                })
            );
        }
    }
}
