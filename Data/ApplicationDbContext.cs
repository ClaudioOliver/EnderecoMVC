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

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
    }
}