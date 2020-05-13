using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PetManager.Models
{
    public class ContactsJxn
    {
        [Key]
        public int ContactJxnId { get; set; }

        [ForeignKey("PetOwner")]
        public int? PetOwnerId { get; set; }
        public PetOwner PetOwner { get; set; }

        [ForeignKey("Contact")]
        public int? ContactId { get; set; }
        public PetOwner Contact { get; set; }
    }
}
