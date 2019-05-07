using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BolsaDeValores.Models
{
    public class Actions
    {
        public int id { get; set; }
        public int idOwner { get; set; }
        public Client owner { get; set; }        
        public int idCategory { get; set; }
        public Category category { get; set; }
        public int quantity { get; set; }
        public int priceQuant { get; set; }
        public int quantMinSell { get; set; }
        public bool status { get; set; }
    }
}
