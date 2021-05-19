using ClubeLeitura.ConsoleApp.Dominio;
using System;

namespace ClubeLeitura.ConsoleApp.Controladores
{
    public class ControladorCaixa : ControladorBase
    {
        public string InserirNovaCaixa(Caixa caixa)
        {
            string resultadoValidacao = caixa.Validar();

            if (resultadoValidacao == "CAIXA_VALIDA")
            {
                int posicao = ObterPosicaoVaga();
                caixa.id = NovoId();
                registros[posicao] = caixa;
            }

            return resultadoValidacao;
        }

        public string EditarCaixa(int id, Caixa caixa)
        {
            string resultadoValidacao = caixa.Validar();

            if (resultadoValidacao == "CAIXA_VALIDA")
            {
                int posicao = ObterPosicaoOcupada(id);
                caixa.id = id;
                registros[posicao] = caixa;
            }

            return resultadoValidacao;
        }

        public bool ExisteCaixaComEsteId(int id)
        {
            return SelecionarRegistroPorId(id) != null;
        }

        public Caixa[] SelecionarTodasCaixas()
        {
            Caixa[] caixasAux = new Caixa[QtdRegistrosCadastrados()];

            Array.Copy(SelecionarTodosRegistros(), caixasAux, caixasAux.Length);

            return caixasAux;
        }

        internal Caixa SelecionarCaixaPorId(int id)
        {
            return (Caixa)SelecionarRegistroPorId(id);
        }
    }
}