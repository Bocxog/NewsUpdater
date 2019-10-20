using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.FileExtensions;
using Microsoft.Extensions.Configuration.Json;
using RedditSharp;
using RedditSharp.Things;

namespace NewsUpdater.DataFlowControllers
{
    public class DataGrabberReddit
    {
        private readonly RedditSharp.Reddit reddit;
        public DataGrabberReddit(IConfiguration configuration)
        {
            var username = configuration.GetValue<string>("reddit:username");
            var password = configuration.GetValue<string>("reddit:password");
            var appid = configuration.GetValue<string>("reddit:appid");
            var appkey = configuration.GetValue<string>("reddit:appkey");
            var webAgent = new BotWebAgent(username, password, appid, appkey, "http://www.example.com/unused/redirect/uri");
            //This will check if the access token is about to expire before each request and automatically request a new one for you
            //"false" means that it will NOT load the logged in user profile so reddit.User will be null
            reddit = new RedditSharp.Reddit(webAgent, false);
        }

        public void ReadPosts(string subreddit)
        {
            var now = new DateTimeOffset(DateTime.UtcNow);
            var posts = reddit.Search<Post>($"subreddit:{subreddit}", Sorting.Top, TimeSorting.Hour);
            foreach (var post in posts)
            {
                Console.WriteLine($"{post.CreatedUTC.Subtract(now).TotalMinutes}. Rating: {post.Score}. {post.Created}. {post.CommentCount}. {post.Title}");
                foreach( var comment in post.Comments.OrderByDescending(x => x.Upvotes).Take(5)){
                    Console.WriteLine($"\tRating: {comment.Score}. {comment.Created}. {comment.Body}");
                }
            }
        }

    }
}
