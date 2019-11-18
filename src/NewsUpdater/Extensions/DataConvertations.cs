using System;
using System.Collections.Generic;
using System.Text;

namespace NewsUpdater
{
    public static class DataConvertations
    {
        public static DomainData.Models.PostHistory GetHistoryMark(this RedditSharp.Things.Post post, DateTime now) {
            return new DomainData.Models.PostHistory
            {
                CommentsCount = post.CommentCount,
                Rating = post.Score,
                TimeStamp = now
            };
        }
}
}
