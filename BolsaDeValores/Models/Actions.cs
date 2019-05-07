using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BolsaDeValores.Models
{
    public class Actions
    {
        [Key]
        public int Id { get; set; }
        public int IdCategory { get; set; }
        public int IdOwner { get; set; }
        public int Quantity { get; set; }
        public int PriceQuant { get; set; }
        public int QuantMinSell { get; set; }
        public bool Status { get; set; }
        
        public Clients Owner { get; set; }
        public Categories Category { get; set; }
    }
}
