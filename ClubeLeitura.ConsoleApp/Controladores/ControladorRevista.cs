using ClubeLeitura.ConsoleApp.Dominio;
using System;

namespace ClubeLeitura.ConsoleApp.Controladores
{
    public class ControladorRevista : ControladorBase
    {
        public string InserirNovaRevista(Revista revista)
        {
            string resultadoValidacao = revista.Validar();

            if (resultadoValidacao == "REVISTA_VALIDA")
            {
                int posicao = ObterPosicaoVaga();
                revista.id = NovoId();
                registros[posicao] = revista;
            }

            return resultadoValidacao;
        }
        public string EditarRevista(int id, Revista revista)
        {
            string resultadoValidacao = revista.Validar();

            if (resultadoValidacao == "REVISTA_VALIDA")
            {
                int posicao = ObterPosicaoOcupada(id);
                revista.id = id;
                registros[posicao] = revista;
            }

            return resultadoValidacao;
        }

        public bool ExisteRevistaComEsteId(int id)
        {
            return SelecionarRegistroPorId(id) != null;
        }

        public Revista[] SelecionarTodasRevistas()
        {
            Revista[] revistasAux = new Revista[QtdRegistrosCadastrados()];

            Array.Copy(SelecionarTodosRegistros(), revistasAux, revistasAux.Length);

            return revistasAux;
        }

        internal Revista SelecionarRevistaPorId(int idRevista)
        {
            return (Revista)SelecionarRegistroPorId(idRevista);
        }
    }
}