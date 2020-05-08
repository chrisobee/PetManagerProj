using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PetManager.Models
{
    public class PetHumanJxn
    {
        [Key]
        public int PetHumanJxnId { get; set; }
        
        [ForeignKey("PetOwner")]
        public int PetOwnerId { get; set; }
        public PetOwner PetOwner { get; set; }
        
        [ForeignKey("Pet")]
        public int PetId { get; set; }
        public Pet Pet { get; set; }

    }
}
