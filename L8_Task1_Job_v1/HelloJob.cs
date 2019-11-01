using System;
using Quartz;

namespace L8_Task1_Job_v1
{
    public class HelloJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            Console.Out.WriteLineAsync("Greetings from HelloJob!");
        }
    }
}
