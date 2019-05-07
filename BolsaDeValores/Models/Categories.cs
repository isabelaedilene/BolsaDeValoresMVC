using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BolsaDeValores.Models
{
    public class Categories
    {
        [Key]
        public int Id { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }

        public ICollection<Actions> Actions { get; set; }
    }
}
