using Newtonsoft.Json;
using System;

namespace CoreApiProject.Core.BusinessModels
{
    public class TokenModel
    {
        [JsonProperty("accessToken")]
        public string AccessToken { get; set; }

        [JsonProperty("userId")]
        public Guid UserId { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }
    }
}