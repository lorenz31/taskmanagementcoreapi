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
    public class TasksService : ITasksService
    {
        private readonly DatabaseContext _db;
        private IResponseModel _response;
        private IRepository _repo;

        public TasksService(
            DatabaseContext db,
            IResponseModel response,
            IRepository repo)
        {
            _db = db;
            _response = response;
            _repo = repo;
        }

        public async Task<IResponseModel> AddTaskAsync(TasksModel obj)
        {
            try
            {
                var tasks = new Tasks
                {
                    Id = Guid.NewGuid(),
                    Title = obj.Title,
                    Description = obj.Description,
                    AssigneeId = obj.AssigneeId,
                    Status = false,
                    DateCreated = DateTime.UtcNow,
                    IsPriority = obj.IsPriority,
                    ProjectId = obj.ProjectId
                };

                _db.Tasks.Add(tasks);
                await _db.SaveChangesAsync();

                _response.Status = true;
                _response.Message = "Task successfully added.";

                return _response;
            }
            catch (Exception ex)
            {
                _response.Status = false;
                _response.Message = ex.Message;

                return _response;
            }
        }

        public async Task<List<TasksModel>> GetTasksPerProjectAsync(Guid projectid) => await _repo.GetTasksPerProjectAsync(projectid);

        public async Task<TasksModel> GetTaskDetailAsync(Guid projectid, Guid taskid) => await _repo.GetTaskDetailAsync(projectid, taskid);

        public async Task<bool> UpdateTaskStatusAsync(Guid projectid, Guid taskid)
        {
            var taskDetail = await _db.Tasks.Where(t => t.ProjectId == projectid && t.Id == taskid).SingleOrDefaultAsync();

            if (taskDetail == null) return false;

            taskDetail.Status = true;

            _db.Entry(taskDetail).State = EntityState.Modified;
            await _db.SaveChangesAsync();

            return true;
        }
    }
}