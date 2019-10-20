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
            a.ReadPosts("pikabu");
            a.ReadPosts("WTF");

            Console.WriteLine("");
            Console.WriteLine("FINISH");
            Console.ReadKey();
        }
    }
}
