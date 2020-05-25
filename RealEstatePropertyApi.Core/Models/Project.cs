using System;
using System.Collections.Generic;

namespace CoreApiProject.Core.Models
{
    public class Project : BaseEntity
    {
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }
        public bool Status { get; set; }
        public int Progress { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<Tasks> Tasks { get; set; }

        public List<MemberProjects> MembersProjects { get; set; }

        public User User { get; set; }
        public Guid UserId { get; set; }
    }
}
