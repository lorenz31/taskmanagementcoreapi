using CoreApiProject.Core;
using CoreApiProject.Core.BusinessModels;
using CoreApiProject.Core.Models;
using CoreApiProject.Core.Services;
using CoreApiProject.DAL.DataContext;
using CoreApiProject.Infrastructure.Repository;

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApiProject.Services.Service
{
    public class ProjectService : IProjectService
    {
        private readonly DatabaseContext _db;
        private IResponseModel _response;
        private IRepository _repo;
        private ILoggerService _loggerService;

        public ProjectService(
            DatabaseContext db,
            IResponseModel response,
            IRepository repo,
            ILoggerService loggerService)
        {
            _db = db;
            _response = response;
            _repo = repo;
            _loggerService = loggerService;
        }

        public async Task<IResponseModel> AddNewProjectAsync(ProjectModel obj)
        {
            try
            {
                var project = new Project
                {
                    Id = Guid.NewGuid(),
                    Name = obj.Name,
                    DateCreated = DateTime.UtcNow,
                    Status = true,
                    Progress = 0,
                    StartDate = obj.StartDate,
                    EndDate = obj.EndDate,
                    UserId = obj.UserId
                };

                _db.Projects.Add(project);
                await _db.SaveChangesAsync();

                _response.Status = true;
                _response.Message = "Project successfully added.";

                return _response;
            }
            catch (Exception ex)
            {
                _loggerService.Log("Add New Property", ex.InnerException.Message, ex.Message, ex.StackTrace, "");

                _response.Status = false;
                _response.Message = "Error adding property.";

                return _response;
            }
        }

        public async Task<List<ProjectModel>> GetProjectsAsync(Guid userid) => await _repo.GetProjectsAsync(userid);

        public async Task<List<ProjectModel>> GetActiveProjectsAsync(Guid userid) => await _repo.GetActiveProjectsAsync(userid);

        public async Task<bool> UpdateProjectStatusAsync(Guid userid, Guid projectid)
        {
            try
            {
                var projectDetail = await _db.Projects.Where(t => t.UserId == userid && t.Id == projectid).SingleOrDefaultAsync();

                if (projectDetail == null) return false;

                projectDetail.Status = true;

                _db.Entry(projectDetail).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IResponseModel> UpdateProjectDetailAsync(ProjectModel obj)
        {
            try
            {
                var projectDetail = new Project
                {
                    Id = obj.Id,
                    Name = obj.Name,
                    Status = obj.Status,
                    Progress = obj.Progress
                };

                _db.Projects.Update(projectDetail);
                await _db.SaveChangesAsync();

                _response.Status = true;
                _response.Message = "Project details successfully updated";

                return _response;
            }
            catch (Exception ex)
            {
                _response.Status = false;
                _response.Message = "Error updating project details.";

                return _response;
            }
        }

        public async Task<IResponseModel> AssignMemberAsync(MemberProjectsModel obj)
        {
            try
            {
                var project = new MemberProjects
                {
                    ProjectId = obj.ProjectId,
                    MemberId = obj.MemberId
                };

                _db.MemberProjects.Add(project);
                await _db.SaveChangesAsync();

                _response.Status = true;
                _response.Message = "Member assigned to project.";

                return _response;
            }
            catch (Exception ex)
            {
                //_loggerService.Log("Add New Property", ex.InnerException.Message, ex.Message, ex.StackTrace, "");

                _response.Status = false;
                _response.Message = "Error assigning member to project.";

                return _response;
            }
        }
    }
}