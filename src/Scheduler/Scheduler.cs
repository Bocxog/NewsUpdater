using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Specialized;
using System.Threading;
using System.Threading.Tasks;

namespace ScheduleLib
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

        private static int index = 0;
        private int GetNextNumber() => Interlocked.Increment(ref index);
        public async Task SetupTask(Type jobType, Action<SimpleScheduleBuilder> scheduleBuilder, Action<JobBuilder> advancedSetting = null)
        {
            var nextTaskNumber = GetNextNumber();
            // define the job and tie it to our HelloJob class
            JobBuilder jobDetailBuilder = JobBuilder
                .Create(jobType)
                //Create<HelloJob>()
                .WithIdentity("job" + nextTaskNumber);

            advancedSetting?.Invoke(jobDetailBuilder);
            IJobDetail jobDetail = jobDetailBuilder.Build();

            // Trigger the job to run now, and then repeat every 10 seconds
            //ICronTrigger
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("trigger" + nextTaskNumber)
                .StartNow()
                .WithSimpleSchedule(scheduleBuilder)
                //.WithSimpleSchedule(x=> x.WithIntervalInHours(1).WithRepeatCount(24*4))
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
