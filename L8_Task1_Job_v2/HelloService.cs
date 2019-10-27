using Quartz;
using Quartz.Impl;
using System.Threading.Tasks;

namespace L8_Task1_Job_v2
{
    public class HelloService
    {
        private StdSchedulerFactory _factory;
        private IJobDetail _jobDetail;
        private ITrigger _trigger;
        private IScheduler _scheduler;

        public HelloService(StdSchedulerFactory factory, IJobDetail jobDetail, ITrigger trigger)
        {
            _factory = factory;
            _jobDetail = jobDetail;
            _trigger = trigger;
        }

        public async Task Start()
        {
            IScheduler scheduler = await _factory.GetScheduler();
            _scheduler = scheduler;

            await _scheduler.Start();
            await _scheduler.ScheduleJob(_jobDetail, _trigger);
        }

        public async Task Stop()
        {
            await _scheduler.Shutdown();
        }
    }
}
