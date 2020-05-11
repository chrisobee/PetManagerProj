using PetManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetManager.ViewModels
{
    public class TasksAndPetsVM
    {
        public PetOwner PetOwner { get; set; }
        public List<Pet> CurrentUsersPets { get; set; }
        public List<ToDoTask> CurrentUsersTasks { get; set; }
    }
}
