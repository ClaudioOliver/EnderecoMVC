using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnderecoMVC.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Usuario { get; set; }
        public string Senha { get; set; }
    }
}