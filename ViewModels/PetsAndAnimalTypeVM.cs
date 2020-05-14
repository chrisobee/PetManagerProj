using PetManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetManager.ViewModels
{
    public class PetsAndAnimalTypeVM
    {
        public List<AnimalType> AnimalTypes { get; set; }
        public Pet Pet { get; set; }
        public List<ToDoTask> PetsTasks { get; set; }
    }
}
