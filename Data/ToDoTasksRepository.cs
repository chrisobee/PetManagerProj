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
    }
}
