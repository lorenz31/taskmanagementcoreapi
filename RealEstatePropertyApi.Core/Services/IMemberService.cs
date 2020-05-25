using CoreApiProject.Core.BusinessModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoreApiProject.Core.Services
{
    public interface IMemberService
    {
        Task<IResponseModel> AddNewMemberAsync(MemberModel obj);
        Task<List<MemberModel>> GetMembersPerUserAsync(Guid userid);
    }
}
