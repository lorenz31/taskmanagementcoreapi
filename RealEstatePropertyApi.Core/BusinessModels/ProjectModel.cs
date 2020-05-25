using System;
using System.ComponentModel.DataAnnotations;

namespace CoreApiProject.Core.BusinessModels
{
    public class ProjectModel
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        //[Required]
        public bool Status { get; set; }

        //[Required]
        public int Progress { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public Guid UserId { get; set; }
    }
}