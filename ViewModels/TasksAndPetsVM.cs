using PetManager.Models;
using PetManager.Services;
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
        public NearbyPlace NearbyVets { get; set; }
        public NearbyPlace NearbyPetStores { get; set; }
        
    }
}
