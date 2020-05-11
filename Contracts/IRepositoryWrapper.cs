using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetManager.Contracts
{
    public interface IRepositoryWrapper
    {
        IPetOwnerRepository PetOwner { get; }
        IPetRepository Pet { get; }
        IToDoTasksRepository ToDoTask { get; }
        Task Save();
    }
}
