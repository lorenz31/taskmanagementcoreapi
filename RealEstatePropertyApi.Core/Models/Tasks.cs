using System;
using System.Collections.Generic;

namespace CoreApiProject.Core.Models
{
    public class Tasks : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid? AssigneeId { get; set; }
        public bool Status { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsPriority { get; set; }
        public List<Comments> Comments { get; set; }
        public List<Attachments> Attachments { get; set; }

        public Project Project { get; set; }
        public Guid ProjectId { get; set; }
    }
}
