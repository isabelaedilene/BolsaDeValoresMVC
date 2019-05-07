using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BolsaDeValores.Models
{
    public class Category
    {
        public int id { get; set; }
        public String name { get; set; }
        public String description { get; set; }

        public ICollection<Actions> Actions { get; set; }
    }
}
