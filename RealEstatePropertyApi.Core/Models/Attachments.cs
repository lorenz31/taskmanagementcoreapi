using System;

namespace CoreApiProject.Core.Models
{
    public class Attachments : BaseEntity
    {
        public string AttachmentLocation { get; set; }
        public DateTime DateCreated { get; set; }

        public Tasks Tasks { get; set; }
        public Guid TaskId { get; set; }
    }
}
