using Microsoft.EntityFrameworkCore;
using PetManager.Contracts;
using PetManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetManager.Data
{
    public class ToDoTasksRepository : RepositoryBase<ToDoTask>, IToDoTasksRepository
    {
        public ToDoTasksRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }

        public void CreateTask(ToDoTask task) => Create(task);

        public void DeleteTask(ToDoTask task) => Delete(task);

        public void EditTask(ToDoTask task) => Update(task);

        public async Task<ToDoTask> FindTask(int? taskId) 
        {
            var tasks = await FindByCondition(t => t.TaskId == taskId);
            var singleTask = tasks.Include(t => t.Pet).SingleOrDefault();
            return singleTask;
        }

        public async Task<IEnumerable<ToDoTask>> GetTasksByPet(int? petId)
        {
            var results = await FindByCondition(t => t.PetId == petId);
            var tasks = results.Include(t => t.Pet).ToList();
            return tasks;
        }
    }
}
