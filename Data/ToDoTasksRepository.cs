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

        public ToDoTask FindTask(int taskId) => FindByCondition(t => t.TaskId == taskId).SingleOrDefault();

        public IEnumerable<ToDoTask> GetTasks(List<int> petIds) => FindAll().Where(t => petIds.Contains(t.PetId));
    }
}
