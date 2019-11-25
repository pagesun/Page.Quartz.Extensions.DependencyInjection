using System;
using System.Threading.Tasks;
using Quartz;

namespace Page.Quartz.Extensions.Example.Jobs
{
    public class HelloJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            try
            {
                Console.WriteLine("---------------------------------");
                Console.WriteLine("------------Hello Job------------");
                Console.WriteLine("---------------------------------");
            }
            catch (Exception e)
            {
                var jobExecution = new JobExecutionException(e) {RefireImmediately = true};

                throw jobExecution;
            }

            return Task.FromResult(true);
        }
    }
}
