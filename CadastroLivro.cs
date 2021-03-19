using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace SistemaBiblioteca
{
    class CadastroLivro
    {
        public static Livro CadastroDoLivro(FileManipulator arquivoLivro, List<Livro> livros)
        {
            
            long numeroTombo;
            if (livros.Count == 0)
            {
                numeroTombo = 0;
            }
            else
            {
                numeroTombo = livros.Last().NumeroTombo;
            }
            //VERIFICA O NUMERO DO TOMBO E CRIA OUTRO , SEMPRE EM SEQUENCIA
            if (numeroTombo == numeroTombo)
            {
                numeroTombo += 1;
                Console.WriteLine("Numero do tombo:");
                Console.WriteLine("{0,5}", numeroTombo.ToString("D8"));
            }

            string isbn = CriandoIsbn(livros);
            Console.WriteLine("Digite o titulo do livro");
            string titulo = Console.ReadLine();
            Console.WriteLine("Digite o genero do Livro:");
            string genero = Console.ReadLine();
            DateTime dataPublicacao = VerificandoData();
            Console.WriteLine("Digite o nome do autor:");
            string autor = Console.ReadLine();

            Livro novoLivro = new Livro()
            {
                NumeroTombo = numeroTombo,
                Isbn = isbn,
                Titulo=titulo,
                Genero=genero,
                DataPublicacao=dataPublicacao,
                Autor = autor, 
            };
            livros.Add(novoLivro);
            livros = livros.OrderBy(x => x.Titulo).ToList();
            Console.WriteLine("Livro Cadastrado com sucesso!!\n");
            //MOSTRA A TAG PARA USUARIO FAZER A ANOTAÇÃO DO NUMERO DO TOMBO
            Console.WriteLine("TAG DO LIVRO:" + numeroTombo.ToString("D5"));
            return novoLivro;
        }
      
        //VALIDA SE O ISBN EXISTE OU SE ESTÁ DE ACORDO COM A NORMA ISBN
       internal static string CriandoIsbn(List<Livro>livros)
       {
            string isbn;
            do
            {
                Console.WriteLine("Digite o ISBM do livro:");
                isbn = Console.ReadLine();

                if (!ValidandoIsbn(isbn))
                {
                    Console.WriteLine("ISBN Invalido");
                    
                }
                if (IsbnExiste(livros, isbn))
                {
                    Console.WriteLine("ISBN já cadastrado");
                    return null;
                    
                }
            } while (!ValidandoIsbn(isbn)|| IsbnExiste(livros,isbn));
            return isbn;
       }
        //ELE VERIFICA O ISBN DIGITADO PELO USUARIO E VERIFICA SE ELE É VALIDO OU NÃO
        public static bool ValidandoIsbn(string valor)
        {
            int[] multiplicador = new int[12] { 1, 3, 1, 3, 1, 3, 1, 3, 1, 3, 1, 3 };
            string temp;
            int soma = 0;
            int resto;
            string digito = "";

            if (valor.Length != 13 )
                return false;

            temp = valor.Substring(0,12);

            for (int i = 0; i < 12; i++)
                soma += int.Parse(temp[i].ToString()) * multiplicador[i];

            resto = soma % 10;

            int validador = 10 - resto;

            resto = soma + validador;

            if ((resto % 10) != 0)
            {
                return false;
            }

                digito += resto.ToString();
                temp += validador;

             return valor.EndsWith(temp);

        }
        // AQUI FAZ A VALIDAÇÃO PARA VER SE ESSE ISBN JÁ EXISTE NO ARQUIVO
        public static bool IsbnExiste(List<Livro> livros , string isbn)
        {
            foreach (Livro i in livros)
            {
                if (i.Isbn.Equals(isbn))
                {
                    return true;
                }
            }
            return false;
        }
        //FAZ O CONTROLE DA DATA, PARA QUE O USUARIO NÃO DIGITE UMA DATA MAIOR QUE A ATUAL
        public static DateTime VerificandoData()
        {
            DateTime dataPublicacao;
            CultureInfo CultureBr = new CultureInfo(name: "pt-BR");
            do
            {
                Console.WriteLine("Digite a data da publicação do livro:");
                dataPublicacao = DateTime.ParseExact(Console.ReadLine(), "d", CultureBr);
                if (dataPublicacao > DateTime.Now)
                {
                    Console.WriteLine("DATA MAIOR QUE A DATA ATUAL!");
                }
            } while (dataPublicacao > DateTime.Now);
            return dataPublicacao; 
        }
        //AQUI ELE VERIFICA OS DADOS DA LISTA
        static public List<Livro> ConverterParaLista(string[] lerDados)
        {
            List<Livro> listaLivros = new List<Livro>();
            foreach(var novoLivro in lerDados)
            {
                string numeroTombo = novoLivro.Substring(0, 5).Trim();
                string isbn = novoLivro.Substring(5,13).Trim();
                string titulo = novoLivro.Substring(18,50).Trim();
                string genero = novoLivro.Substring(68,20).Trim();
                string dataPublicacao = novoLivro.Substring(88,10).Trim();
                string autor = novoLivro.Substring(98,20).Trim();
                
                listaLivros.Add(new Livro {NumeroTombo=long.Parse(numeroTombo),Isbn = isbn,Titulo=titulo,Genero=genero, DataPublicacao = DateTime.ParseExact(dataPublicacao,"d",new CultureInfo("pt-BR")), Autor=autor });
            }
            return listaLivros;
        }
        //AQUI SÃO SALVAS OS DADOS QUE FORAM DIGITADOS
        static public string[] ConverterParaSalvar(List<Livro>livros)
        {
            StringBuilder livroSB = new StringBuilder();
            livros.ForEach(livroCad =>
            {
                livroSB.Append(livroCad.NumeroTombo.ToString().PadRight(5, ' ' ));
                livroSB.Append(livroCad.Isbn.PadRight(13, ' '));
                livroSB.Append(livroCad.Titulo.PadRight(50, ' '));
                livroSB.Append(livroCad.Genero.PadRight(20, ' '));
                livroSB.Append(livroCad.DataPublicacao.ToString("dd/MM/yyyy").PadRight(10, ' '));
                livroSB.Append(livroCad.Autor.PadRight(20, ' '));
                
                livroSB.AppendLine();
            });
            return livroSB.ToString().Split('\n');
        }

    }
}



