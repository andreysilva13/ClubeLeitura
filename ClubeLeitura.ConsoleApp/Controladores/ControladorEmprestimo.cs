using ClubeLeitura.ConsoleApp.Dominio;
using System;

namespace ClubeLeitura.ConsoleApp.Controladores
{
    public class ControladorEmprestimo : ControladorBase
    {
        public string RegistrarEmprestimo(Amigo amigo, Revista revista, DateTime data)
        {
            Emprestimo emprestimo = new Emprestimo(amigo, revista, data);

            string resultadoValidacao = emprestimo.Validar();

            if (resultadoValidacao == "EMPRESTIMO_VALIDO")
            {
                amigo.RegistrarEmprestimo(emprestimo);
                revista.RegistrarEmprestimo(emprestimo);

                int posicao = ObterPosicaoVaga();
                emprestimo.id = NovoId();
                registros[posicao] = emprestimo;
            }

            return resultadoValidacao;
        }

        internal bool RegistrarDevolucao(int idEmprestimo, DateTime data)
        {
            Emprestimo emprestimo = (Emprestimo)SelecionarRegistroPorId(idEmprestimo);

            emprestimo.Fechar(data);

            return true;
        }

        public bool ExisteEmprestimoComEsteId(int idEmprestimo)
        {
             return SelecionarRegistroPorId(idEmprestimo) != null;
        }

        internal Emprestimo[] SelecionarEmprestimosEmAberto()
        {
            Emprestimo[] emprestimosEmAberto = new Emprestimo[QtdEmprestimosEmAberto()];

            object[] todosEmprestimos = SelecionarTodosRegistros();

            int i = 0;

            foreach (Emprestimo e in todosEmprestimos)
            {
                if (e.estaAberto)
                {
                    emprestimosEmAberto[i++] = e;
                }
            }

            return emprestimosEmAberto;
        }

        private int QtdEmprestimosEmAberto()
        {
            object[] todosEmprestimos = SelecionarTodosRegistros();

            int numeroEmprestimosEmAberto = 0;

            foreach (Emprestimo emprestimo in todosEmprestimos)
            {
                if (emprestimo.estaAberto)
                {
                    numeroEmprestimosEmAberto++;
                }
            }

            return numeroEmprestimosEmAberto;
        }

        internal Emprestimo[] SelecionarEmprestimosFechados(int mes)
        {
            Emprestimo[] emprestimosFechados = new Emprestimo[QtdEmprestimosFechados(mes)];

            object[] todosEmprestimos = SelecionarTodosRegistros();

            int i = 0;

            foreach (Emprestimo e in todosEmprestimos)
            {
                if (e.EstaFechado() && e.Mes == mes)
                {
                    emprestimosFechados[i++] = e;
                }
            }

            return emprestimosFechados;
        }

        private int QtdEmprestimosFechados(int mes)
        {
            object[] todosEmprestimos = SelecionarTodosRegistros();

            int numeroEmprestimosFechado = 0;

            foreach (Emprestimo emprestimo in todosEmprestimos)
            {
                if (emprestimo.EstaFechado() && emprestimo.Mes == mes)
                {
                    numeroEmprestimosFechado++;
                }
            }

            return numeroEmprestimosFechado;
        }
    }
}