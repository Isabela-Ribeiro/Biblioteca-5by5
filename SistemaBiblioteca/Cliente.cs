using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaBiblioteca
{
    public class Cliente
    {
        public long IdCliente { get; set; }
        public string Cpf { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Telefone { get; set; }
        public string Logradouro { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Cep { get; set; }

      

    }
}
