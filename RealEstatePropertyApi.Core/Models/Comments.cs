using System;

namespace CoreApiProject.Core.Models
{
    public class Comments : BaseEntity
    {
        public string Comment { get; set; }
        public DateTime DateCreated { get; set; }
        public Guid MemberId { get; set; }

        public Tasks Tasks { get; set; }
        public Guid TaskId { get; set; }
    }
}
