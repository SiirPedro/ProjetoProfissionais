using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TabelaDeProfissionais.Models;

namespace TabelaDeProfissionais.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {
        }
        public DbSet<Profissional> Profissionais { get; set; }

       
    }

}
