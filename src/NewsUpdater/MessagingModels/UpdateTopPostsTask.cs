using DomainData;
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

            var subreddits = new Dictionary<string, string>();
            using (var db = new DesignTimeDbContextFactory().CreateDbContext(Program.builder))
            {
                foreach (var post in posts)
                {
                    var dbPost = db.Posts.Find(post.Id);
                    var now = DateTime.Now;
                    if (dbPost == null)
                    {
                        dbPost = db.Posts.Add(new DomainData.Models.Post
                        {
                            Id = post.Id,
                            HistoryMarks = new DomainData.Models.PostHistory[]
                            {
                                post.GetHistoryMark(now)
                            }
                        }).Entity;

                        const int minutesInterval = 5;
                        const int totalHours = 2*24;
                        const int repeatCount = totalHours * 60 / minutesInterval;
                        await Program.scheduler.SetupTask(
                            typeof(UpdatePostHistory),
                            x => x.WithIntervalInMinutes(5).WithRepeatCount(repeatCount),
                            x => x.UsingJobData("id", post.Id).UsingJobData("permalink", post.Permalink.ToString())
                            );
                    }
                    dbPost.LastUpdate = now;

                    dbPost.Title = post.Title;
                    dbPost.FullName = post.FullName;
                    dbPost.Kind = post.Kind;
                    if (dbPost.SubRedditId == null)
                    {
                        var subreddit = await UpdateSubreddit(db, post);
                            dbPost.SubRedditId = subreddit.Id;
                    }

                    /*

                        dbPost.AuthorName
                        dbPost.//TagId = post.LinkFlairText;
                        dbPost.//Author = post.Author;
                        */
                    dbPost.MyVote = (int)post.Vote;

                    dbPost.Url = post.Url.ToString();
                    dbPost.Permalink = post.Permalink.ToString();
                    dbPost.Shortlink = post.Shortlink;
                    dbPost.Thumbnail = post.Thumbnail.ToString();

                    dbPost.SelfText = post.SelfText;
                    dbPost.SelfTextHtml = post.SelfTextHtml;
                    dbPost.CommentsCount = post.CommentCount;

                    dbPost.IsSpoiler = post.IsSpoiler;
                    dbPost.IsNSFW = post.NSFW;
                    dbPost.IsSelfPost = post.IsSelfPost;
                    dbPost.IsEdited = post.Edited;
                    dbPost.IsArchived = post.IsArchived;
                    dbPost.IsApproved = post.IsApproved;
                    dbPost.IsRemoved = post.IsRemoved;
                    dbPost.IsSaved = post.Saved;
                    dbPost.IsStickied = post.IsStickied;
                    dbPost.IsLiked = post.Liked;

                    dbPost.Domain = post.Domain;

                    dbPost.Rating = post.Score;
                    dbPost.Downvotes = post.Downvotes;
                    dbPost.Upvotes = post.Upvotes;

                    dbPost.ApprovedBy = post.ApprovedBy;
                    dbPost.BannedBy = post.BannedBy;
                    dbPost.Gilded = post.Gilded;
                    dbPost.ReportCount = post.ReportCount;

                    dbPost.Created = post.Created;
                    dbPost.CreatedUTC = post.CreatedUTC;
                    //foreach (var comment in post.Comments) {
                    //    dbPost.UserComments.Add(new DomainData.Models.UserComment { });
                    //        }
                    await db.SaveChangesAsync();
                    //ScheduleLib.Scheduler.add new ();
                }
            }
        }

        private async Task<DomainData.Models.SubReddit> UpdateSubreddit(DatabaseContext db, RedditSharp.Things.Post post)
        {
            RedditSharp.Things.Subreddit subreddit = post.Subreddit;
            var dbSubreddit = db.SubReddits.Find(subreddit.Id);
            if (dbSubreddit == null)
            {
                dbSubreddit = db.SubReddits.Add(new DomainData.Models.SubReddit { Id = subreddit.Id }).Entity;
            }
            dbSubreddit.Link = subreddit.Url.ToString();
            dbSubreddit.Members = subreddit.Subscribers;
            dbSubreddit.Name = subreddit.Name;
            dbSubreddit.CakeDay = subreddit.Created;

            await db.SaveChangesAsync();

            return dbSubreddit;
        }
    }
}
