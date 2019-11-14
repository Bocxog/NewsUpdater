using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainData.Models {
    public class Community {
        public int Id { get; set; }
        [StringLength(32)]
        public string Name { get; set; }
        public string Link { get; set; }

        public DateTime CakeDay { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }
}