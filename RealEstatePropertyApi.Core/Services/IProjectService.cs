using CoreApiProject.Core.BusinessModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoreApiProject.Core.Services
{
    public interface IProjectService
    {
        Task<IResponseModel> AddNewProjectAsync(ProjectModel obj, string baseurl);
        Task<List<ProjectModel>> GetProjectsAsync(Guid userid);
        Task<List<ProjectModel>> GetActiveProjectsAsync(Guid userid);
        Task<bool> UpdateProjectStatusAsync(Guid userid, Guid projectid);
        Task<IResponseModel> UpdateProjectDetailAsync(ProjectModel obj);
        Task<IResponseModel> AssignMemberAsync(MemberProjectsModel obj);
    }
}