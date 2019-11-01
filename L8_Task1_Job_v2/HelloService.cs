using Quartz;
using Quartz.Impl;
using System;

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

        public bool Start()
        {
            try
            {
                _scheduler = _factory.GetScheduler();

                _scheduler.Start();
                _scheduler.ScheduleJob(_jobDetail, _trigger);
            }
            catch (Exception) { }

            return true;
        }

        public bool Stop()
        {
            if (_scheduler == null)
            {
                return true;
            }

            _scheduler.Shutdown();

            return true;
        }
    }
}
