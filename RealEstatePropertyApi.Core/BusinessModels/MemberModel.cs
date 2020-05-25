using System;

namespace CoreApiProject.Core.BusinessModels
{
    public class MemberModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid UserId { get; set; }
    }
}
