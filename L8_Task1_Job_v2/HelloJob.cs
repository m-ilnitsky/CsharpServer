using System;
using System.Threading.Tasks;
using Quartz;

namespace L8_Task1_Job_v2
{
    public class HelloJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            await Console.Out.WriteLineAsync("Greetings from HelloJob!");
        }
    }
}
