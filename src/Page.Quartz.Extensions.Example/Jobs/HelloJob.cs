using System;
using System.Threading.Tasks;
using Page.Quartz.Extensions.Example.Services;
using Quartz;

namespace Page.Quartz.Extensions.Example.Jobs
{
    public class HelloJob : IJob
    {
        private IService _service;
        public HelloJob(IService service)
        {
            _service = service;
        }


        public Task Execute(IJobExecutionContext context)
        {
            try
            {
                _service.SayHello();
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
