using System;

namespace CoreApiProject.Core.BusinessModels
{
    public interface IUserModel
    {
        Guid UserId { get; set; }
        //string ClientId { get; set; }
        string Secret { get; set; }
        string Username { get; set; }
        string Password { get; set; }
        string Salt { get; set; }
    }
}
