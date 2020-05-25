using CoreApiProject.Core.BusinessModels;
using CoreApiProject.Core.Models;
using CoreApiProject.Core.Services;
using CoreApiProject.DAL.DataContext;
using CoreApiProject.Infrastructure.Repository;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoreApiProject.Infrastructure.Service
{
    public class MemberService : IMemberService
    {
        private readonly DatabaseContext _db;
        private IResponseModel _response;
        private IRepository _repo;

        public MemberService(
            DatabaseContext db,
            IResponseModel response,
            IRepository repo)
        {
            _db = db;
            _response = response;
            _repo = repo;
        }

        public async Task<IResponseModel> AddNewMemberAsync(MemberModel obj)
        {
            try
            {
                var member = new Member
                {
                    Id = Guid.NewGuid(),
                    Name = obj.Name,
                    UserId = obj.UserId
                };

                _db.Members.Add(member);
                await _db.SaveChangesAsync();

                _response.Status = true;
                _response.Message = "Member successfully added.";

                return _response;
            }
            catch (Exception ex)
            {
                _response.Status = false;
                _response.Message = ex.Message;

                return _response;
            }
        }

        public async Task<List<MemberModel>> GetMembersPerUserAsync(Guid userid) => await _repo.GetMembersPerUserAsync(userid);
    }
}
