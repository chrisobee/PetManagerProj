using PetManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetManager.ViewModels
{
    public class TaskWithAllCurrentPets
    {
        public ToDoTask Task { get; set; }

        public List<Pet> CurrentPets { get; set; }
    }
}
