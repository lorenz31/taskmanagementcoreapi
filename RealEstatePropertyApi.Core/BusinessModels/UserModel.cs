using System;
using System.ComponentModel.DataAnnotations;

namespace CoreApiProject.Core.BusinessModels
{
    public class UserModel : IUserModel
    {
        public Guid UserId { get; set; }

        //public string ClientId { get; set; }

        public string Secret { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        public string Salt { get; set; }
    }
}
