using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.FileExtensions;
using Microsoft.Extensions.Configuration.Json;
using NewsUpdater.DataFlowControllers;
using System;
using System.IO;

namespace NewsUpdater
{
    class Program
    {
        static void Main(string[] args)
        {
            IConfiguration builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile("appsettings.private.json", optional: true, reloadOnChange: true)
                .Build();

            var a = new DataGrabberReddit(builder);

            //TODO: queued refresh data
            //TODO: store to db values (docker)
            //TODO: take grafana online (docker)

            a.UpdatePostInfo("/r/Pikabu/comments/dl6ngi/книги/");
            a.UpdatePostInfo("https://www.reddit.com/r/science/comments/dj4z87/from_2007_to_2017_the_number_of_suicides_among/");

            a.ReadPosts("pikabu");
            a.ReadPosts("WTF");


            Console.WriteLine("");
            Console.WriteLine("FINISH");
            Console.ReadKey();
        }
    }
}
