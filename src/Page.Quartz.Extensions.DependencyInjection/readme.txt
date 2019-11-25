1.appsettings.json add quartz node.
	"Quartz": {
		"Scheduler": {
		  "InstanceName": "AMS.Report"
		},
		"ThreadPool": {
		  "Type": "Quartz.Simpl.SimpleThreadPool, Quartz",
		  "ThreadPriority": "Normal",
		  "ThreadCount": 10
		},
		"Plugin": {
		  "JobInitializer": {
			"Type": "Quartz.Plugin.Xml.XMLSchedulingDataProcessorPlugin, Quartz.Plugins",
			"FileNames": "quartz_jobs.xml"
		  }
		}
	  }

  2.add quartz_jobs.xml and set jobs config
	refer to:https://github.com/quartznet/quartznet/blob/master/src/Quartz/Xml/job_scheduling_data_2_0.xsd

  3.AddQuartz
  public void ConfigureServices(IServiceCollection services)
    {
		//add your services

        services.AddQuartz(Configuration);
    }