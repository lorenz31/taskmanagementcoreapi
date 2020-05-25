using System;
using System.ComponentModel.DataAnnotations;

namespace CoreApiProject.Core.BusinessModels
{
    public class TasksModel
    {
        public Guid Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        public Guid AssigneeId { get; set; }

        [Required]
        public bool Status { get; set; }

        [Required]
        public bool IsPriority { get; set; }

        [Required]
        public Guid ProjectId { get; set; }
    }
}