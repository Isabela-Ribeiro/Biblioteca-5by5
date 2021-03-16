using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaBiblioteca
{
    class Relatorio
    {
        public static List<Emprestimo> RelatorioDeLivros(List<Emprestimo> emprestimos)
        {
            //PERCORRE A LISTA DE EMPRESTIMO E TRAZ AS SUA INFORMAÇÃO
            emprestimos.ForEach(EmpCad =>
            {
                //VERIFICA SE É UM LIVRO EMPRESTADO
                if (EmpCad.StatusEmprestimo == 1)
                {
                    Console.WriteLine("Cpf do Cliente: " + EmpCad.IdCliente);
                    Console.WriteLine("Numero Tombo: " + EmpCad.NumeroTombo);
                    Console.WriteLine("Status Do Livro: " + EmpCad.StatusEmprestimo.ToString() + " Emprestado");
                    Console.WriteLine("Data do Emprestimo: " + EmpCad.DataEmprestimo.ToString("dd/MM/yyyy"));
                    Console.WriteLine("Data da Devolução: " + EmpCad.DataDevolucao.ToString("dd/MM/yyyy"));
                }
                //VERIFICA SE É UM LIVRO DEVOLVIDO
                if (EmpCad.StatusEmprestimo == 2)
                {
                    Console.WriteLine("Cpf do Cliente: " + EmpCad.IdCliente);
                    Console.WriteLine("Numero Tombo: " + EmpCad.NumeroTombo);
                    Console.WriteLine("Status Do Livro: " + EmpCad.StatusEmprestimo.ToString() + " Devolvido");
                    Console.WriteLine("Data do Emprestimo: " + EmpCad.DataEmprestimo.ToString("dd/MM/yyyy"));
                    Console.WriteLine("Data da Devolução: " + EmpCad.DataDevolucao.ToString("dd/MM/yyyy"));
                }
            });

            return emprestimos;
        }
    }
}
