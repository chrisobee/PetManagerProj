using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PetManager.Models
{
    public class PetOwner
    {
        [Key]
        public int PetOwnerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }
        public int? ResetDay { get; set; }

        [NotMapped]
        public PetOwner[] Contacts { get; set; } 
        public string PhoneNumber { get; set; }
        [ForeignKey("IdentityUser")]
        public string IdentityUserId { get; set; }
        public IdentityUser IdentityUser { get; set; }
        
        
    }
}
