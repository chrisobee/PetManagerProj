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
            var singleTask = tasks.SingleOrDefault();
            return singleTask;
        }

        public async Task<IEnumerable<ToDoTask>> GetTasksByPets(int? petId)
        {
            var tasks = await FindByCondition(t => petIds.Contains(t.PetId));
            return tasks;
        }
        //public IEnumerable<ToDoTask> GetTasks(List<int> petIds) => FindAll().Where(t => petIds.Contains(t.PetId));
    }
}
