using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PetManager.Models
{
    public class Frequency
    {
        [Key]
        public int FrequencyId { get; set; }
        public string Interval { get; set; }
    }
}
