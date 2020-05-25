using System;
using System.Collections.Generic;

namespace CoreApiProject.Core.Models
{
    public class Member : BaseEntity
    {
        public string Name { get; set; }

        public List<MemberProjects> MembersProjects { get; set; }

        public User User { get; set; }
        public Guid UserId { get; set; }
    }
}
