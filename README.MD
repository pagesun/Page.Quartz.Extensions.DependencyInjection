# Quartz Extensions For Microsoft DI

![](https://github.com/pagesun/Page.Quartz.Extensions.DependencyInjection/workflows/Release/badge.svg)

## appsettings.json add quartz node

```json
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
```



## add quartz_jobs.xml

```xml
<?xml version="1.0" encoding="UTF-8"?>
<job-scheduling-data xmlns="http://quartznet.sourceforge.net/JobSchedulingData" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" version="2.0">
  <processing-directives>
    <overwrite-existing-data>true</overwrite-existing-data>
  </processing-directives>
  <schedule>
    <job>
      <name>HelloJob</name>
      <group>Jobs</group>
      <description>HelloJob</description>
      <!--class path and library name-->
      <job-type>Page.Quartz.Extensions.Example.Jobs.HelloJob, Page.Quartz.Extensions.Example</job-type>
      <durable>true</durable>
      <recover>false</recover>
    </job>
    <trigger>
      <cron>
        <name>HelloTrigger</name>
        <group>Jobs</group>
        <description>HelloJob Trigger</description>
        <job-name>HelloJob</job-name>
        <job-group>Jobs</job-group>
        <cron-expression>0/20 * * * * ?</cron-expression>
      </cron>
    </trigger>
  </schedule>
</job-scheduling-data>
```

refer to:https://github.com/quartznet/quartznet/blob/master/src/Quartz/Xml/job_scheduling_data_2_0.xsd



## add quartz

```c#
public void ConfigureServices(IServiceCollection services)
    {
		//add your services

        services.AddQuartz(Configuration);
    }
```

