using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.FileExtensions;
using Microsoft.Extensions.Configuration.Json;
using NewsUpdater.DataFlowControllers;
using System;
using System.IO;
using Foundatio.Messaging;
using NewsUpdater.MessagingModels;
using RedditSharp.Things;
using System.Collections.Generic;
using StackExchange.Redis;
using Foundatio.Caching;

namespace NewsUpdater
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            IConfiguration builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile("appsettings.private.json", optional: true, reloadOnChange: true)
                .Build();

            //RedisHybridCacheClient:
            //IMessageBus messageBus = new InMemoryMessageBus();
            //messageBus = new RedisMessageBus(new RedisMessageBusOptions { });
            //IMessageBus messageRedisBus = new RedisMessageBus(new RedisMessageBusOptions { });
            var log = new StringWriter();
            using (var muxer = ConnectionMultiplexer.Connect("localhost:63790", log))
            {
                var _redisCache = new RedisCacheClient(new RedisCacheClientOptions { ConnectionMultiplexer = muxer });
                _redisCache.RemoveAllAsync().GetAwaiter().GetResult();
                IMessageBus messageBus = new RedisMessageBus(new RedisMessageBusOptions { Subscriber = muxer.GetSubscriber(), Topic = "test-cache" });
                var _hybridCacheClient = new HybridCacheClient(_redisCache, messageBus);

                var a = new DataGrabberReddit(builder);

                //75% done: queued refresh data
                //60% done: store to db values (docker)
                //TODO: take grafana online (docker)

                var listToUpdate = new List<string>();
                listToUpdate.Add("/r/Pikabu/comments/dl6ngi/книги/");
                listToUpdate.Add("/r/science/comments/dj4z87/from_2007_to_2017_the_number_of_suicides_among/");

                //a.ReadPosts("pikabu");
                //a.ReadPosts("WTF");

                await messageBus.SubscribeAsync<Message_RedditPostUpdate>(msg =>
                {
                    Console.WriteLine($"Now we  take: {msg.Link}");
                    // Got message
                    a.UpdatePostInfo(msg.Link);
                });

                foreach (var postUrl in listToUpdate)
                {
                    await messageBus.PublishAsync(new Message_RedditPostUpdate { Link = postUrl });
                }


                Console.WriteLine("");
                Console.WriteLine("FINISH");
                Console.ReadKey();
            }
        }
    }
}
