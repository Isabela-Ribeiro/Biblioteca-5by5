using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Configuration;

namespace SistemaBiblioteca
{
    class CadastroEmprestimo
    {
        //CADASTRA O LIVRO PARA EMPRESTIMO
        public static Emprestimo CadastroDeEmprestimo(List<Emprestimo> emprestimos, List<Cliente> clientes, List<Livro> livros)
        {
            
            CultureInfo CultureBr = new CultureInfo(name: "pt-BR");
            DateTime dataEmprestimo = DateTime.Now;
            DateTime dataDevolucao = DateTime.Now;
            Console.WriteLine("Digite o número tombo :");
            long numeroTombo = long.Parse(Console.ReadLine());
            //VERIFICA SE O NUMERO DO TOMBO EXISTE NA LISTA DO LIVRO
            if (livros.Exists(tombo => tombo.NumeroTombo.Equals(numeroTombo)))
            {
                //VERIFICA SE EXISTE O NUMERO TOMBO COM STATUS 1 NA LISTA DE EMPRESTIMO
                if (emprestimos.Exists(emprestimo => emprestimo.NumeroTombo.Equals(numeroTombo) && emprestimo.StatusEmprestimo.Equals(1)))
                {
                    Console.WriteLine("livro não está disponivel para emprestimo");
                    return null;
                }
                //CASO PASSE PELO PRIMEIRO IF ENTÃO O LIVRO NÃO FOI EMPRESTADO ELE ENTRA NA VALIDAÇÃO DO CPF
                Console.Write("Informe o CPF do Cliente: ");
                string cpf = Console.ReadLine();
                if (clientes.Exists(cliente => cliente.Cpf.Equals(cpf)))
                {
                    Console.WriteLine("Digite a a data Para devolucao dd/MM/yyyy:");
                    dataDevolucao = DateTime.ParseExact(Console.ReadLine(), "d", CultureBr);

                    Console.WriteLine("\nEMPRESTANDO LIVRO COM SUCESSO");
                    return new Emprestimo()
                    {
                        IdCliente = cpf,
                        NumeroTombo = numeroTombo,
                        DataEmprestimo = dataEmprestimo,
                        DataDevolucao = dataDevolucao,
                        StatusEmprestimo = 1,
                    };
                }
                else
                {
                    Console.WriteLine("CPF NÃO ENCONTRADO");
                    return null;
                }
            }

            else
            {
                Console.WriteLine("NUMERO TOMBO NÃO ENCONTRADO");
                return null;
            }
        }
        //FAZ O CADASTRO DE DEVOLUCAO DO LIVRO
        public static Emprestimo CadastroDeDevolucao(List<Emprestimo> emprestimos, List<Livro> livros)
        {
            
            Console.WriteLine("Digite o número tombo :");
            long numeroTombo = long.Parse(Console.ReadLine());
            //VERIFICA SE O NUMERO TOMBO EXISTE E SE ELE É 1
            if (emprestimos.Exists(emprestimo => emprestimo.NumeroTombo.Equals(numeroTombo) && emprestimo.StatusEmprestimo.Equals(1)))
            {
                int dia = 0;
                DateTime atual = DateTime.Now;
                DateTime data;
                //ELE BUSCA NA LISTA EMPRESTIMO E TRAZ TODA A INFORMAÇÃO DO LIVRO
                var NovoEmprestimos = emprestimos.FirstOrDefault(emprestimo => emprestimo.NumeroTombo.Equals(numeroTombo) &&
                emprestimo.StatusEmprestimo.Equals(1));
                //REMOVE O ITEM
                emprestimos.Remove(NovoEmprestimos);
                //ADICIONA COM UM NOVO STATUS NA LISTA
                NovoEmprestimos.StatusEmprestimo = 2;
                emprestimos.Add(NovoEmprestimos);
                //FAZ UM CALCULO PARA VER SE TEM MULTA A SER PAGA
                data = NovoEmprestimos.DataDevolucao;
                dia = atual.Day - data.Day ;
                double multa = 0.00;
                if (dia > 0)
                {
                    multa = dia * 0.10;
                    Console.WriteLine("VALOR DA MULTA É DE:" + multa.ToString("C"));
                }
                
                Console.WriteLine("\nLIVRO DEVOLVIDO COM SUCESSO!!");
            }
            else
            {
                Console.WriteLine("LIVRO NÃO ENCOTRADO PARA DEVOLUÇÃO");
                return null;
            }

            return null;
        }

        static public List<Emprestimo> ConverterParaLista(string[] lerDados)
        {
            List<Emprestimo> listaEmprestimo = new List<Emprestimo>();
            foreach (var novoEmprestimo in lerDados)
            {
                string idCliente = novoEmprestimo.Substring(0, 11).Trim();
                string numeroTombo = novoEmprestimo.Substring(11, 5).Trim();
                string dataEmprestimo = novoEmprestimo.Substring(16, 10).Trim();
                string dataDevolucao = novoEmprestimo.Substring(26, 10).Trim();
                string statusEmprestimo = novoEmprestimo.Substring(36, 1).Trim();


                listaEmprestimo.Add(new Emprestimo { IdCliente = idCliente, NumeroTombo = long.Parse(numeroTombo), DataEmprestimo = DateTime.ParseExact(dataEmprestimo, "d", new CultureInfo("pt-BR")), DataDevolucao = DateTime.ParseExact(dataDevolucao, "d", new CultureInfo("pt-BR")), StatusEmprestimo = int.Parse(statusEmprestimo) });
            }
            return listaEmprestimo;
        }
        static public string[] ConverterParaSalvar(List<Emprestimo> emprestimos)
        {

            StringBuilder emprestimoSB = new StringBuilder();
            emprestimos.ForEach(EmpCad =>
            {
                emprestimoSB.Append($"{ EmpCad.IdCliente.PadRight(11, ' ')}") ;
                emprestimoSB.Append($"{EmpCad.NumeroTombo.ToString().PadRight(5, ' ')}");
                emprestimoSB.Append($"{EmpCad.DataEmprestimo.ToString("dd/MM/yyyy").PadRight(10, ' ')}");
                emprestimoSB.Append($"{EmpCad.DataDevolucao.ToString("dd/MM/yyyy").PadRight(10, ' ')}");
                emprestimoSB.Append($"{EmpCad.StatusEmprestimo.ToString().PadRight(1, ' ')}" );

                emprestimoSB.AppendLine();

            });

            return emprestimoSB.ToString().Split('\n');
        }

    }
}
