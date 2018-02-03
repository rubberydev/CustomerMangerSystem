using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ingeneo.AppContratos.ModelViews
{
    public class JobSchedule
    {
        public static void Start()
        {
            IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();
            scheduler.Start();

            IJobDetail job = JobBuilder.Create<EmailJob>().Build();

            ITrigger trigger = TriggerBuilder.Create()
               //.WithIdentity("TContratos")
               .WithDailyTimeIntervalSchedule
                    (x =>                        
                        x.WithIntervalInHours(24)
                        .OnEveryDay()
                        .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(0, 0))
                        )
                .Build();
            scheduler.ScheduleJob(job, trigger);
        }
    }
}