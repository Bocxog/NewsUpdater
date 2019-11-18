using DomainData;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsUpdater.MessagingModels
{
    class UpdatePostHistory : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            JobDataMap dataMap = context.JobDetail.JobDataMap;
            string id = dataMap.GetString("id");
            string permalink = dataMap.GetString("permalink");

            var post = Program.dataGrabber.UpdatePostInfo(permalink);
            
            using (var db = new DesignTimeDbContextFactory().CreateDbContext(Program.builder))
            {
                var dbPost = db.Posts.Find(id);
                if (dbPost == null)
                    return; // TODO: add alert here

                var historyList = dbPost.HistoryMarks.ToList();
                historyList.Add(post.GetHistoryMark(DateTime.Now));
                dbPost.HistoryMarks = historyList.ToArray();

                await db.SaveChangesAsync();
            }
        }
    }
}
