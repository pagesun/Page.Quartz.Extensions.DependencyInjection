using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Page.Quartz.Extensions.Example.Services
{
    public class Service : IService
    {
        public void SayHello()
        {
            Console.WriteLine("---------------hello--------------");
        }
    }
}
