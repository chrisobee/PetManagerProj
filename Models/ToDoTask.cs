using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PetManager.Models
{
    public class ToDoTask
    {
        [Key]
        public int TaskId { get; set; }
        public string TaskName { get; set; }
        public DateTime? ResetDay { get; set; }
        public string SpecialInstructions { get; set; }

        [ForeignKey("Pet")]
        public int PetId { get; set; }
        public Pet Pet { get; set; }

        [ForeignKey("Frequency")]
        public int FrequencyId { get; set; }
        public Frequency Frequency { get; set; }


    }
}
