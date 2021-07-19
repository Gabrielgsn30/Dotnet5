using System;
using Microsoft.EntityFrameworkCore;
using Tarefa1;

namespace Tarefa1
{
    public class ApplicationDbContext : DbContext
    {

        public DbSet<Filme> Filmes { get; set; }
        public DbSet<Diretor> Diretores { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    }
}