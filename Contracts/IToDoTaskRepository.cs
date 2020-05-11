using PetManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetManager.Contracts
{
    public interface IToDoTasksRepository : IRepositoryBase<ToDoTask>
    {
        Task<IEnumerable<ToDoTask>> GetTasksByPets(List<int> petIds);
        Task<ToDoTask> FindTask(int? taskId);
        void CreateTask(ToDoTask task);
        void EditTask(ToDoTask task);
        void DeleteTask(ToDoTask task);
    }
}
