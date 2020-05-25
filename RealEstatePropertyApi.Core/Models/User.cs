using System.Collections.Generic;

namespace CoreApiProject.Core.Models
{
    public class User : BaseEntity
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }

        public List<Project> Projects { get; set; }
        public List<Member> Members { get; set; }
    }
}
