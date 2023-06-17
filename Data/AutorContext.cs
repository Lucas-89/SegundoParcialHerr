using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SegundoParcialHerr.Models;

namespace SegundoParcialHerr.Data
{
    public class AutorContext : DbContext
    {
        public AutorContext (DbContextOptions<AutorContext> options)
            : base(options)
        {
        }

        public DbSet<SegundoParcialHerr.Models.Autor> Autor { get; set; } = default!;

        public DbSet<SegundoParcialHerr.Models.Libro> Libro { get; set; } = default!;

        public DbSet<SegundoParcialHerr.Models.Sucursal> Sucursal { get; set; } = default!;
    }
}
