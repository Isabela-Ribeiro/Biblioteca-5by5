using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace SistemaBiblioteca
{
    public class CadastroCliente
    {
        //FAZ O CADASTRO DO CLIENTE
        public static Cliente Cadastro(FileManipulator arquivoCliente, List<Cliente> lista)
        {
            
            //CRIA UM INCICE PARA O CLIENTE
            long id;
            if (lista.Count == 0)
            {
                id = 0;
            }
            else
            {
                id = lista.Last().IdCliente;
            }
            if (id == id)
            {
                id += 1;
            }
            Console.WriteLine($"Id do cliente:{id}");
            string cpf = CpfVerifica(lista);
            Console.WriteLine("Digite o nome do cliente:");
            string nome = Console.ReadLine();
            DateTime dataNascimento = ValidaData();
            Console.WriteLine("Digite o numero de telefone com o ddd");
            string telefone = Console.ReadLine();
            Console.WriteLine("Digite o nome da rua ou avenida e o numero da casa:");
            string logradouro = Console.ReadLine();
            Console.WriteLine("Digite o nome do bairro:");
            string bairro = Console.ReadLine();
            Console.WriteLine("Digite o nome da cidade:");
            string cidade = Console.ReadLine();
            Console.WriteLine("Digite o estado:");
            string estado = Console.ReadLine();
            Console.WriteLine("Digite o Cep:");
            string cep = Console.ReadLine();

            Cliente listaCliente = new Cliente()
            {
                IdCliente = id,
                Cpf = cpf,
                Nome = nome,
                DataNascimento = dataNascimento,
                Telefone = telefone,
                Logradouro = logradouro,
                Bairro = bairro,
                Cidade = cidade,
                Estado = estado,
                Cep = cep
            };
            lista.Add(listaCliente);
            Console.WriteLine("Cliente cadastrado com sucesso!");
            return listaCliente;
        }
        //CHAMA OS METODOS VERIFICA EXISTENCI E VALIDA
        internal static string CpfVerifica(List<Cliente> lista)
        {
            string cpf;
            do
            {
                Console.WriteLine("Digite o CPF do cliente:");
                cpf = Console.ReadLine();
                if (!ValidaCpf(cpf))
                {
                    Console.WriteLine("CPF inválido");

                }

                if (VerificaExistencia(lista, cpf))
                {
                    Console.WriteLine("CPF já cadastrado");
                    return null;
                }



            } while (!ValidaCpf(cpf) || VerificaExistencia(lista, cpf));

            return cpf;
        }
        //VERIFICA SE CPF É VALIDO
        internal static bool ValidaCpf(string valor)
        {
            int resto;
            int soma = 0;
            string digitado = "";
            string calculado = "";
            string cpf = valor;
            if (valor == "")
            {
                return false;
            }

            if (cpf.Length != 11)
            {
                return false;
            }

            switch (cpf)
            {
                case "11111111111":
                    return false;
                case "00000000000":
                    return false;
                case "22222222222":
                    return false;
                case "33333333333":
                    return false;
                case "44444444444":
                    return false;
                case "55555555555":
                    return false;
                case "66666666666":
                    return false;
                case "77777777777":
                    return false;
                case "88888888888":
                    return false;
                case "99999999999":
                    return false;
            }

            // Pesos para calcular o primeiro digito
            int[] peso1 = new int[] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            // Pesos para calcular o segundo digito
            int[] peso2 = new int[] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };


            calculado = valor.Substring(0, 9);
            for (int i = 0; i < 9; i++)
                soma += int.Parse(calculado[i].ToString()) * peso1[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digitado = resto.ToString();

            calculado = calculado + digitado;

            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(calculado[i].ToString()) * peso2[i];

            resto = soma % 11;
            if (resto < 2)
            {
                resto = 0;
            }
            else
            {
                resto = 11 - resto;
            }

            digitado = digitado + resto.ToString();
            return valor.EndsWith(digitado);
        }
        //VERIFICA SE CPF JÁ EXISTE NESTA LISTA
        internal static bool VerificaExistencia(List<Cliente> lista, string cpf)
        {
            foreach (Cliente i in lista)
            {
                if (i.Cpf.Equals(cpf))
                    return true;
            }
            return false;
        }
        //VERIFICA DATA PARA QUE O USUARIO NÃO DIGITE UMA DATA MAIOR DO QUE ATUAL
        public static DateTime ValidaData()
        {
            DateTime dataNascimento;
            CultureInfo CultureBr = new CultureInfo(name: "pt-BR");
            do
            {
                Console.WriteLine("Digite a data de Nascimento: {dd/mm/yyyy}");
                dataNascimento = DateTime.ParseExact(Console.ReadLine(), "d", CultureBr);
                if (dataNascimento > DateTime.Now)
                {
                    Console.WriteLine("DATA MAIOR QUE ATUAL!");
                }
            } while (dataNascimento > DateTime.Now);
            
            return dataNascimento;
        }

        public static string[] CriandoArquivo(List<Cliente> lista)
        {
            StringBuilder clienteSB = new StringBuilder();
            lista.ForEach(clienteCad =>
            {
                clienteSB.Append(clienteCad.IdCliente.ToString().PadRight(5, ' '));
                clienteSB.Append(clienteCad.Cpf.PadRight(11, ' '));
                clienteSB.Append(clienteCad.Nome.PadRight(50, ' '));
                clienteSB.Append(clienteCad.DataNascimento.ToString("dd/MM/yyyy").PadRight(10, ' '));
                clienteSB.Append(clienteCad.Telefone.ToString().PadRight(11, ' '));
                clienteSB.Append(clienteCad.Logradouro.PadRight(15, ' '));
                clienteSB.Append(clienteCad.Bairro.PadRight(15, ' '));
                clienteSB.Append(clienteCad.Cidade.PadRight(15, ' '));
                clienteSB.Append(clienteCad.Estado.PadRight(15, ' '));
                clienteSB.Append(clienteCad.Cep.PadRight(8, ' '));

                clienteSB.AppendLine();

            });
            return clienteSB.ToString().Split(',');
        }
        public static List<Cliente> TrazendoParaLista(string[] LerDados)
        {
            List<Cliente> novaLista = new List<Cliente>();
            foreach (var cliente in LerDados)
            {
                string id = cliente.Substring(0, 5).Trim();
                string cpf = cliente.Substring(5, 11).Trim();
                string nome = cliente.Substring(16, 50).Trim();
                string dataNascimento = cliente.Substring(66, 10).Trim();
                string telefone = cliente.Substring(76, 11).Trim();
                string logradouro = cliente.Substring(87, 15).Trim();
                string bairro = cliente.Substring(101, 15).Trim();
                string cidade = cliente.Substring(116, 15).Trim();
                string estado = cliente.Substring(131, 15).Trim();
                string cep = cliente.Substring(146, 8).Trim();

                novaLista.Add(new Cliente { IdCliente = long.Parse(id), Cpf = cpf, Nome = nome, DataNascimento = DateTime.ParseExact(dataNascimento, "d", new CultureInfo(name: "pt-BR")), Telefone = telefone, Logradouro = logradouro, Bairro = bairro, Cidade = cidade, Estado = estado, Cep = cep });
            }
            return novaLista;
        }
    }
}
