using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.FileExtensions;
using Microsoft.Extensions.Configuration.Json;
using NewsUpdater.DataFlowControllers;
using System;
using System.IO;
using NewsUpdater.MessagingModels;
using RedditSharp.Things;
using System.Collections.Generic;
using ScheduleLib;
using System.Threading.Tasks;
using System.Threading;
using DomainData;

namespace NewsUpdater
{
    class Program
    {
        public static DataGrabberReddit dataGrabber;
        public static IConfiguration builder;
        public static Scheduler scheduler;
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile("appsettings.private.json", optional: true, reloadOnChange: true)
                .Build();

            using (var db = new DesignTimeDbContextFactory().CreateDbContext(builder))
            {
                using (scheduler = new Scheduler())
                {
                    dataGrabber = new DataGrabberReddit(builder);

                    //75% done: queued refresh data
                    //60% done: store to db values (docker)
                    //TODO: take grafana online (docker)

                    var listToUpdate = new List<string>();
                    listToUpdate.Add("/r/Pikabu/comments/dl6ngi/книги/");
                    listToUpdate.Add("/r/science/comments/dj4z87/from_2007_to_2017_the_number_of_suicides_among/");

                    //dataGrabber.ReadPosts("pikabu");
                    //a.ReadPosts("WTF");
                    await SetQueueToUpdateSubredditInfo(scheduler);
                    Console.WriteLine("");
                    Console.WriteLine("FINISH");
                    Console.ReadKey();
                }
            }
        }

        private static IReadOnlyList<string> SubredditList = new List<string> { "Pikabu", "science", "WTF" };
        private static async Task SetQueueToUpdateSubredditInfo(Scheduler scheduler)
        {
            foreach (var subreddit in SubredditList)
            {
                await scheduler.SetupTask(typeof(UpdateTopPostsTask), x => x.WithIntervalInMinutes(5).RepeatForever(), x => x.UsingJobData("name", subreddit));
                Thread.Sleep(10 * 1000);
            }
        }
    }
}
