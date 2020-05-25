using System;

namespace CoreApiProject.Core.Models
{
    public class MemberProjects
    {
        public Project Project { get; set; }
        public Guid ProjectId { get; set; }

        public Member Member { get; set; }
        public Guid MemberId { get; set; }
    }
}
