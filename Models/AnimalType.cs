using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PetManager.Models
{
    public class AnimalType
    {
        [Key]
        public int AnimalTypeId { get; set; }
        public string Name { get; set; }
        [NotMapped]
        public Recommendation[] recommendationsForAllTypes { get; set; }

        public AnimalType()
        {
            recommendationsForAllTypes = new Recommendation[]
            {
                new Recommendation{RecommendationId = 1, TaskName = "Feed AM", Interval = "Daily" },
                new Recommendation{RecommendationId = 2, TaskName = "Feed PM", Interval = "Daily" },
                new Recommendation{RecommendationId = 3, TaskName = "Give Medication AM", Interval = "Daily" },
                new Recommendation{RecommendationId = 4, TaskName = "Give Medication PM", Interval = "Daily"}
            };
        }
    }
}
