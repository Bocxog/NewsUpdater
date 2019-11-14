namespace DomainData.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class User
    {
        public int Id { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        public int? Karma { get; set; }
        public DateTime CakeDay { get; set; }

        public virtual ICollection<UserComment> UserComments { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<UserLink> UserReferences { get; set; }
    }
}
