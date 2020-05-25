using System;
using System.Collections.Generic;
using System.Text;

namespace CoreApiProject.Core.Models
{
    public class Client : BaseEntity
    {
        public string Email { get; set; }
        public Guid ApiKey { get; set; }

        public List<Project> Projects { get; set; }
    }
}
