using Microsoft.Extensions.DependencyModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Page.Quartz.Extensions.DependencyInjection.Extensions
{
    public static class AppDomainExtensions
    {
        /// <summary>
        /// get dependency assembly
        /// </summary>
        /// <param name="domain">domain</param>
        /// <returns></returns>
        public static List<Assembly> GetCurrentPathAssembly(this AppDomain domain)
        {
            var libraries = DependencyContext.Default.CompileLibraries
                .Where(x => !x.Name.StartsWith("Microsoft") && !x.Name.StartsWith("System"))
                .ToList();
            var list = new List<Assembly>();
            if (libraries.Any())
                list.AddRange(from dll in libraries where dll.Type == "project" select Assembly.Load(dll.Name));
            return list;
        }
    }
}
