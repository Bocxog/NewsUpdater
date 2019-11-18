using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainData.Models {
    public class SubReddit
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }
        public int? Members { get; set; }

        public DateTimeOffset? CakeDay { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }
}