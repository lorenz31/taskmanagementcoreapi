using System;
using System.ComponentModel.DataAnnotations;

namespace CoreApiProject.Core.BusinessModels
{
    public class CommentModel
    {
        public Guid Id { get; set; }

        [Required]
        public string Comment { get; set; }

        [Required]
        public Guid MemberId { get; set; }

        [Required]
        public Guid TaskId { get; set; }
    }
}
