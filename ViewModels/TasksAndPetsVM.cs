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
        public List<PetOwner> Contacts { get; set; }
        public NearbyPlace NearbyVets { get; set; }
        public NearbyPlace NearbyPetStores { get; set; }
        public List<Recommendation> RecommendationsWithoutType { get; set; }

        public TasksAndPetsVM()
        {
            RecommendationsWithoutType = new List<Recommendation>()
            {
                new Recommendation {TaskName = "Feed AM", Interval = "Daily"},
                new Recommendation {TaskName = "Feed PM", Interval = "Daily"},
                new Recommendation {TaskName = "Give Medication AM", Interval = "Daily"},
                new Recommendation {TaskName = "Give Medication PM", Interval = "Daily"}
            };
        }

    }
}
