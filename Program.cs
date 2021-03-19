using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaBiblioteca
{
    class Program
    {
        static void Main(string[] args)
        {
            FileManipulator arquivoCliente = new FileManipulator { Path = @"C:\Arquivo\", Name = "CSV CLIENTE.dat" };
            FileManipulatorController.InicializarArquivo(arquivoCliente);
            FileManipulator arquivoLivro = new FileManipulator { Path = @"C:\Arquivo", Name = "CSV LIVRO.dat" };
            FileManipulatorController.InicializarArquivo(arquivoLivro);
            FileManipulator arquivoEmprestimo = new FileManipulator { Path = @"C:\Arquivo", Name = "CSV EMPRESTIMO.dat" };
            FileManipulatorController.InicializarArquivo(arquivoEmprestimo);
            MenuPrincipal(arquivoCliente, arquivoLivro, arquivoEmprestimo);
        }
        static public void MenuPrincipal(FileManipulator arquivoCliente, FileManipulator arquivoLivro, FileManipulator arquivoEmprestimo)
        {
            Cliente cliente = new Cliente();
            List<Emprestimo> emprestimos = new List<Emprestimo>();
            List<Cliente> clientes = new List<Cliente>();
            List<Livro> livros = new List<Livro>();
            int opc;

            do
            {
                Console.WriteLine("--------------MENU-----------------\n");
                Console.WriteLine(" 1 - CADASTRO DE CLIENTE");
                Console.WriteLine(" 2 - CADASTRO DE LIVRO");
                Console.WriteLine(" 3 - EMPRÉSTIMO DE LIVRO");
                Console.WriteLine(" 4 - DEVOLUÇÃO DO LIVRO");
                Console.WriteLine(" 5 - RELATÓRIO DO LIVRO");
                Console.WriteLine(" 0 - SAIR");
                Console.WriteLine(" DIGITE UMA OPÇÃO:");
                opc = int.Parse(Console.ReadLine());
                switch (opc)
                {
                    case 1:
                        //FAZ O CADASTRO DO CLIENTE
                        Console.Clear();
                        Console.WriteLine("----------CADASTRO DE CLIENTE-----------\n");

                        clientes = CadastroCliente.TrazendoParaLista(FileManipulatorController.LerArquivo(arquivoCliente));
                        CadastroCliente.Cadastro(arquivoCliente, clientes);
                        FileManipulatorController.EscreverNoArquivo(arquivoCliente, CadastroCliente.CriandoArquivo(clientes));

                        break;
                    case 2:
                        //CADASTRO DO LIVRO
                        Console.Clear();
                        Console.WriteLine("----------CADASTRO DO LIVRO-----------\n");

                        livros = CadastroLivro.ConverterParaLista(FileManipulatorController.LerArquivo(arquivoLivro));
                        CadastroLivro.CadastroDoLivro(arquivoLivro, livros);
                        FileManipulatorController.EscreverNoArquivo(arquivoLivro, CadastroLivro.ConverterParaSalvar(livros));

                        break;
                    case 3:
                        //EMPRESTIMO DO LIVRO
                        Console.Clear();
                        Console.WriteLine("------------EMPRÉSTIMO DE LIVRO-----------------");

                        clientes = CadastroCliente.TrazendoParaLista(FileManipulatorController.LerArquivo(arquivoCliente));
                        livros = CadastroLivro.ConverterParaLista(FileManipulatorController.LerArquivo(arquivoLivro));
                        emprestimos = CadastroEmprestimo.ConverterParaLista(FileManipulatorController.LerArquivo(arquivoEmprestimo));
                        Emprestimo novoemprestimo = CadastroEmprestimo.CadastroDeEmprestimo(emprestimos, clientes, livros);

                        if (novoemprestimo != null)
                        {
                            emprestimos.Add(novoemprestimo);
                            FileManipulatorController.EscreverNoArquivo(arquivoEmprestimo, CadastroEmprestimo.ConverterParaSalvar(emprestimos));
                        }
                        break;
                    case 4:
                        Console.Clear();
                        //DEVOLUCAO DO LIVRO

                        Console.WriteLine("------------DEVOLUÇÃO DO LIVRO-------------------");

                        emprestimos = CadastroEmprestimo.ConverterParaLista(FileManipulatorController.LerArquivo(arquivoEmprestimo));
                        Emprestimo novaDevolucao = CadastroEmprestimo.CadastroDeDevolucao(emprestimos, livros);
                        FileManipulatorController.EscreverNoArquivo(arquivoEmprestimo, CadastroEmprestimo.ConverterParaSalvar(emprestimos));

                        if (novaDevolucao != null)
                        {
                            emprestimos.Add(novaDevolucao);
                            FileManipulatorController.EscreverNoArquivo(arquivoEmprestimo, CadastroEmprestimo.ConverterParaSalvar(emprestimos));
                        }

                        break;
                    case 5:
                        Console.Clear();
                        //IMPRIME O RELARIO DO LIVRO E DADOS DO CLIENTE

                        Console.WriteLine("-----------RELATÓRIO DO LIVRO---------------------");

                        emprestimos = CadastroEmprestimo.ConverterParaLista(FileManipulatorController.LerArquivo(arquivoEmprestimo));
                        Relatorio.RelatorioDeLivros(emprestimos);
                        break;
                    case 0:
                        Console.WriteLine("PROGRAMA FINALIZADO");
                        break;
                    default:
                        Console.WriteLine("OPCÃO INVALIDA!");
                        break;
                }
            } while (opc != 0);
        }
    }
}

