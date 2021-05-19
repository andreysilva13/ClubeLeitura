using ClubeLeitura.ConsoleApp.Dominio;

namespace ClubeLeitura.ConsoleApp.Controladores
{
    public class ControladorBase
    {
        const int CAPACIDADE_REGISTROS = 100;
        protected EntidadeBase[] registros;

        private int ultimoId;

        public ControladorBase()
        {
            registros = new EntidadeBase[CAPACIDADE_REGISTROS];
        }

        public bool ExcluirRegistro(int id)
        {
            bool conseguiuExcluir = false;

            for (int i = 0; i < registros.Length; i++)
            {
                EntidadeBase registro = registros[i];

                if (registro != null && registro.id == id)
                {
                    registros[i] = null;
                    conseguiuExcluir = true;
                    break;
                }
            }
            return conseguiuExcluir;
        }

        protected object[] SelecionarTodosRegistros()
        {
            EntidadeBase[] registrosAux = new EntidadeBase[QtdRegistrosCadastrados()];

            int i = 0;

            foreach (EntidadeBase registro in registros)
            {
                if (registro != null)
                    registrosAux[i++] = registro;
            }

            return registrosAux;
        }

        public object SelecionarRegistroPorId(int id)
        {
            EntidadeBase registro = null;

            for (int i = 0; i < registros.Length; i++)
            {
                if (registros[i] != null && registros[i].id == id)
                {
                    registro = registros[i];

                    break;
                }
            }

            return registro;
        }

        protected int QtdRegistrosCadastrados()
        {
            int numeroRegistrosCadastrados = 0;

            for (int i = 0; i < registros.Length; i++)
            {
                if (registros[i] != null)
                {
                    numeroRegistrosCadastrados++;
                }
            }

            return numeroRegistrosCadastrados;
        }

        protected int ObterPosicaoVaga()
        {
            int posicao = -1;

            for (int i = 0; i < registros.Length; i++)
            {
                if (registros[i] == null)
                {
                    posicao = i;
                    break;
                }
            }

            return posicao;
        }

        protected int ObterPosicaoOcupada(int id)
        {
            int posicao = 0;

            for (int i = 0; i < registros.Length; i++)
            {
                if (registros[i] != null && registros[i].id == id)
                {
                    posicao = i;
                    break;
                }
            }

            return posicao;
        }

        protected int NovoId()
        {
            return ++ultimoId;
        }
    }
}