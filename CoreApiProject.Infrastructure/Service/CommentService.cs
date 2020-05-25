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
    public class CommentService : ICommentService
    {
        private DatabaseContext _db;
        private IResponseModel _response;
        private IRepository _repo;

        public CommentService(
            DatabaseContext db,
            IResponseModel response,
            IRepository repo)
        {
            _db = db;
            _response = response;
            _repo = repo;
        }

        public async Task<IResponseModel> AddCommentAsync(CommentModel obj)
        {
            try
            {
                var comment = new Comments
                {
                    Id = Guid.NewGuid(),
                    Comment = obj.Comment,
                    DateCreated = DateTime.UtcNow,
                    MemberId = obj.MemberId,
                    TaskId = obj.TaskId
                };

                _db.Comments.Add(comment);
                await _db.SaveChangesAsync();

                _response.Status = true;
                _response.Message = "Comment successfully added.";

                return _response;
            }
            catch (Exception ex)
            {
                _response.Status = false;
                _response.Message = ex.Message;

                return _response;
            }
        }

        public async Task<List<ViewCommentModel>> GetCommentsPerTaskAsync(Guid taskid) => await _repo.GetCommentsPerTaskAsync(taskid);
    }
}
