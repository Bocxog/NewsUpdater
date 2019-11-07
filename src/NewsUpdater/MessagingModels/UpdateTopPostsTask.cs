using Quartz;
using RedditSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NewsUpdater.MessagingModels
{
    class UpdateTopPostsTask : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            JobDataMap dataMap = context.JobDetail.JobDataMap;
            string subredditName = dataMap.GetString("name");

            var posts = Program.dataGrabber.ReadPosts(subredditName, Sorting.Top, TimeSorting.Hour);
            foreach (var post in posts)
            {
                //if (DBNull.contains(post))
                //    continue;
                //DBNull.add(new post);
                //ScheduleLib.Scheduler.add new ();
            }
        }
    }
}
