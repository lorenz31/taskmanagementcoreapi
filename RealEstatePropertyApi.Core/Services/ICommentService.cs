using CoreApiProject.Core.BusinessModels;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoreApiProject.Core.Services
{
    public interface ICommentService
    {
        Task<IResponseModel> AddCommentAsync(CommentModel obj);
        Task<List<ViewCommentModel>> GetCommentsPerTaskAsync(Guid taskid);
    }
}
