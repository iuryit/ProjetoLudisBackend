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
        public DbSet<Quadra> Quadras { get; set; }
        public DbSet<Esporte> Esportes { get; set; }
        public DbSet<QuadraEsportes> QuadraEsportes { get; set; }
        public DbSet<AgendaQuadra> AgendaQuadras { get; set; }

        //public DbSet<IdentityUser> Usuario { get; set; }
       /* protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<QuadraEsportes>()
                .HasKey(AD => new { AD.QuadraId, AD.EsporteId });

        }*/
    }
}