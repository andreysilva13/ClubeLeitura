using System;
using ClubeLeitura.ConsoleApp.Dominio;

namespace ClubeLeitura.ConsoleApp.Controladores
{
    public class ControladorAmigo : ControladorBase
    {       
        internal string InserirNovoAmigo(Amigo amigo)
        {
            string resultadoValidacao = amigo.Validar();

            if (resultadoValidacao == "AMIGO_VALIDO")
            {
                int posicao = ObterPosicaoVaga();
                amigo.id = NovoId();
                registros[posicao] = amigo;
            }

            return resultadoValidacao;
        }        

        internal string EditarAmigo(int id, Amigo amigo)
        {
            string resultado = amigo.Validar();

            if (resultado == "AMIGO_VALIDO")
            {
                int posicao = ObterPosicaoOcupada(id);
                amigo.id = id;
                registros[posicao] = amigo;
            }

            return resultado;
        }

        internal bool ExisteAmigoComEsteId(int id)
        {
            return SelecionarRegistroPorId(id) != null;
        }

        internal Amigo[] SelecionarTodosAmigos()
        {
             Amigo[] amigosAux = new Amigo[QtdRegistrosCadastrados()];

            Array.Copy(SelecionarTodosRegistros(), amigosAux, amigosAux.Length);

            return amigosAux;
        }

        internal Amigo SelecionarAmigoPorId(int idAmigo)
        {
            return (Amigo)SelecionarRegistroPorId(idAmigo);
        }
    }
}