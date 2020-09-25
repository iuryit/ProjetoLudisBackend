using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjetoLudis.Tabelas;

namespace ProjetoLudis.Data
{
    public class Context : IdentityDbContext
    {
        public Context(DbContextOptions<Context> options) :
    base(options)
        { } 
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Esportista> Esportistas { get; set; }
        public DbSet<Comerciante> Comerciantes { get; set; }
        public DbSet<Rota> RotaDb { get; set; }
        public DbSet<RotaPonto> RotaPontoDb { get; set; }

        public DbSet<IdentityUser> Usuario { get; set; }
    }
}