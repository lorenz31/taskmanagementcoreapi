using CoreApiProject.Core.BusinessModels;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoreApiProject.Infrastructure.Repository
{
    public interface IRepository
    {
        #region Projects Queries
        Task<List<ProjectModel>> GetProjectsAsync(Guid userid);
        Task<List<ProjectModel>> GetActiveProjectsAsync(Guid userid);
        #endregion

        #region Members Queries
        Task<List<MemberModel>> GetMembersPerUserAsync(Guid userid);
        #endregion

        #region Tasks Queries
        Task<List<TasksModel>> GetTasksPerProjectAsync(Guid projectid);
        Task<TasksModel> GetTaskDetailAsync(Guid projectid, Guid taskid);
        #endregion

        #region Comment Queries
        Task<List<ViewCommentModel>> GetCommentsPerTaskAsync(Guid taskid);
        #endregion
    }
}
