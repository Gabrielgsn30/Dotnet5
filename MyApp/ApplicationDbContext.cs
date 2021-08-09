using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Tarefa1;

namespace Tarefa1
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser> 
    {

        public DbSet<Filme> Filmes { get; set; }
        public DbSet<Diretor> Diretores { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    }
}