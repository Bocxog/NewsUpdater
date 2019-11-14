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


            using (var db = new DesignTimeDbContextFactory().CreateDbContext(Program.builder))
            {
                foreach (var post in posts)
                {
                    var dbPost = db.Posts.Find(post.Id);
                    if (dbPost != null)
                        continue;
                    //post.Subreddit.Id
                    db.Posts.Add(new DomainData.Models.Post
                    {
                        Id = post.Id,
                        Title = post.Title,
                        FullName = post.FullName,
                        /*Kind = post.Kind,
                        ?SubredditName = post.SubredditName,
                        ?SubredditName = post.Subreddit.Id,

                        AuthorName
                        //TagId = post.LinkFlairText,
                        //Author = post.Author,

                        MyVote = post.Vote,

                        Url = post.Url.ToString(),
                        Permalink = post.Permalink.ToString(),
                        Shortlink = post.Shortlink.ToString(),
                        Thumbnail = post.Thumbnail.ToString(),

                        SelfText = post.SelfText,
                        SelfTextHtml = post.SelfTextHtml,*/
                        CommentsCount = post.CommentCount,

                        //IsSpoiler = post.IsSpoiler,
                        //IsNSFW = post.NSFW,
                        //IsSelfPost = post.IsSelfPost,
                        //IsEdited = post.Edited,
                        //IsArchived = post.IsArchived,
                        //IsApproved = post.IsApproved,
                        //IsRemoved = post.IsRemoved,
                        //IsSaved = post.Saved,
                        //IsStickied = post.IsStickied,
                        //IdLiked = post.Liked,

                        //Domain = post.Domain,

                        Rating = post.Score,
                        //Downvotes = post.Downvotes,
                        //Upvotes = post.Upvotes,

                        //ApprovedBy = post.ApprovedBy,
                        //BannedBy = post.BannedBy,
                        //Gilded = post.Gilded,
                        //ReportCount = post.ReportCount,
                    });
                    //foreach (var comment in post.Comments) {
                    //    dbPost.UserComments.Add(new DomainData.Models.UserComment { });
                    //        }
                    await db.SaveChangesAsync();
                    //ScheduleLib.Scheduler.add new ();
                }
            }
        }
    }
}
