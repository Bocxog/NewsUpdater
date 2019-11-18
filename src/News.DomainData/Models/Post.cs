using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DomainData.Models
{
    public class Post
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }

        public string Title { get; set; }
        public string FullName { get; set; }

        public string Content { get; set; }
        public int? Rating { get; set; }

        [Column(TypeName = "jsonb")]
        public PostHistory[] HistoryMarks { get; set; }

        public string AuthorName { get; set; }
        public int? AuthorId { get; set; }
        public virtual User Author { get; set; }



        public bool IsDeleted { get; set; }
        //public bool IsMine { get; set; }
        //[StringLength(20)]
        //public string Type { get; set; }
        public int CommentsCount { get; set; }

        public DateTimeOffset Created { get; set; }
        public DateTimeOffset CreatedUTC { get; set; }


        public TimeSpan? ProcessedTime { get; set; }
        public DateTime LastUpdate { get; set; }

        public string SubRedditId { get; set; }
        public virtual SubReddit SubReddit { get; set; }

        public int? TagId { get; set; }
        public virtual PostTag Tag { get; set; }


        public string Url { get; set; }
        public string Permalink { get; set; }
        public string Shortlink { get; set; }
        public string Thumbnail { get; set; }

        public string SelfText { get; set; }
        public string SelfTextHtml { get; set; }

        public bool IsSpoiler { get; set; }
        public bool IsNSFW { get; set; }
        public bool IsSelfPost { get; set; }
        public bool IsEdited { get; set; }
        public bool IsArchived { get; set; }
        public bool? IsApproved { get; set; }
        public bool? IsRemoved { get; set; }
        public bool IsSaved { get; set; }
        public bool IsStickied { get; set; }
        public bool? IsLiked { get; set; }


        public int MyVote { get; set; }
        public int Downvotes { get; set; }
        public int Upvotes { get; set; }

        public string ApprovedBy { get; set; }
        public string BannedBy { get; set; }
        public int Gilded { get; set; }

        public string Domain { get; set; }
        public int? ReportCount { get; set; }
        public string Kind { get; set; }

        public virtual ICollection<UserComment> UserComments { get; set; }
        public virtual ICollection<PostLink> PostLinks { get; set; }
    }

    public class PostHistory {
        public DateTime TimeStamp { get; set; }
        public int Rating { get; set; }
        public int CommentsCount { get; set; }
    }
}
