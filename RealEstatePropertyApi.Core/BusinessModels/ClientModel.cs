using System;
using System.ComponentModel.DataAnnotations;

namespace CoreApiProject.Core.BusinessModels
{
    public class ClientModel
    {
        public Guid ClientId { get; set; }

        [Required]
        public string Email { get; set; }

        public Guid ApiKey { get; set; }
        //public string Salt { get; set; }
    }
}
