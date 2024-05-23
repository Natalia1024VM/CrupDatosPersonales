using CapaDal.Entidad;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDal
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Persona> Persona { get; set; }
        public DbSet<Pais> Pais { get; set; }
        public DbSet<Ciudad> Ciudad { get; set; }
        public DbSet<Departamento> Departamento { get; set; }

    }
}
