using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Specialized;
using System.Threading.Tasks;

namespace Scheduler
{
    public class Scheduler : IDisposable
    {
        IScheduler scheduler;
        public Scheduler()
        {
            InitScheduler().GetAwaiter().GetResult();
        }
        private async Task InitScheduler() { 
            try
            {
                // Grab the Scheduler instance from the Factory
                NameValueCollection props = new NameValueCollection
                {
                    { "quartz.serializer.type", "binary" }
                };
                StdSchedulerFactory factory = new StdSchedulerFactory(props);
                scheduler = await factory.GetScheduler();

                // and start it off
                await scheduler.Start();
            }
            catch (SchedulerException se)
            {
                await Console.Error.WriteLineAsync(se.ToString());
            }
        }

        public async Task<bool> CheckExist(TriggerKey triggerKey) => await scheduler.CheckExists(triggerKey);
        public async Task<bool> CheckExist(JobKey jobKey) => await scheduler.CheckExists(jobKey);
        
        
        public async Task SetupTask(Type jobType, Action<SimpleScheduleBuilder> scheduleBuilder)
        {
            // define the job and tie it to our HelloJob class
            IJobDetail jobDetail = JobBuilder
                .Create(jobType)                
                //Create<HelloJob>()
                .WithIdentity("job1", "group1")
                .Build();

            // Trigger the job to run now, and then repeat every 10 seconds
            //ICronTrigger
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("trigger1", "group1")
                .StartNow()
                .WithSimpleSchedule(scheduleBuilder)
                //=> x
                //    .WithIntervalInSeconds(10)
                //    .RepeatForever())
            //.WithCalendarIntervalSchedule
            //.WithCronSchedule
            //.WithDailyTimeIntervalSchedule
            //.WithSimpleSchedule
                .Build();

            // Tell quartz to schedule the job using our trigger
            await scheduler.ScheduleJob(jobDetail, trigger);

        }

        public void Dispose()
        {
            // and last shut down the scheduler when you are ready to close your program
            scheduler.Shutdown().Wait();
        }
    }
}
