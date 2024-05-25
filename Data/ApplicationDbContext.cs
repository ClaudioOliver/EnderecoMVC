using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnderecoMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace EnderecoMVC.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Enderecos> Enderecos { get; set; }

    }
}