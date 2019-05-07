using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BolsaDeValores.Models;

namespace BolsaDeValores.Models
{
    public class BolsaDeValoresContext : DbContext
    {
        public BolsaDeValoresContext (DbContextOptions<BolsaDeValoresContext> options)
            : base(options)
        {
        }

        public DbSet<BolsaDeValores.Models.Client> Client { get; set; }

        public DbSet<BolsaDeValores.Models.Category> Category { get; set; }

        public DbSet<BolsaDeValores.Models.Actions> Actions { get; set; }
    }
}
