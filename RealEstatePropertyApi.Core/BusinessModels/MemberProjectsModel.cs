using System;
using System.ComponentModel.DataAnnotations;

namespace CoreApiProject.Core.BusinessModels
{
    public class MemberProjectsModel
    {
        [Required]
        public Guid ProjectId { get; set; }

        [Required]
        public Guid MemberId { get; set; }
    }
}
