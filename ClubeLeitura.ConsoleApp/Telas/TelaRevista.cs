using ClubeLeitura.ConsoleApp.Controladores;
using ClubeLeitura.ConsoleApp.Dominio;
using System;

namespace ClubeLeitura.ConsoleApp.Telas
{
    public class TelaRevista : TelaBase, ICadastravel
    {
        private readonly TelaCaixa telaCaixa;
        private readonly ControladorCaixa controladorCaixa;        

        private readonly ControladorRevista controladorRevista;

        public TelaRevista(ControladorRevista ctrlRevista, ControladorCaixa ctrlCaixa, TelaCaixa tlCaixa)
            : base("Cadastro de Revistas")
        {
            controladorRevista = ctrlRevista;
            telaCaixa = tlCaixa;
            controladorCaixa = ctrlCaixa;
        }


        public void InserirNovoRegistro()
        {
             ConfigurarTela("Inserindo uma nova revista...");

            Revista revista = ObterRevista(TipoAcao.Inserindo);

            string resultadoValidacao = controladorRevista.InserirNovaRevista(revista);

            if (resultadoValidacao == "REVISTA_VALIDA")
                ApresentarMensagem("Revista inserida com sucesso", TipoMensagem.Sucesso);
            else
            {
                ApresentarMensagem(resultadoValidacao, TipoMensagem.Erro);
                InserirNovoRegistro();
            }
        }

        public void EditarRegistro()
        {
            ConfigurarTela("Editando uma revista...");

            bool temRegistros = VisualizarRegistros(TipoVisualizacao.Pesquisando);

            if (temRegistros == false)
                return;

            Console.Write("\nDigite o número da revista que deseja editar: ");
            int id = Convert.ToInt32(Console.ReadLine());

            bool numeroEncontrado = controladorRevista.ExisteRevistaComEsteId(id);
            if (numeroEncontrado == false)
            {
                ApresentarMensagem("Nenhuma revista foi encontrada com este número: " + id, TipoMensagem.Erro);
                EditarRegistro();
                return;
            }

            Revista revista = ObterRevista(TipoAcao.Editando);

            string resultadoValidacao = controladorRevista.EditarRevista(id, revista);

            if (resultadoValidacao == "REVISTA_VALIDA")
                ApresentarMensagem("Revista editada com sucesso", TipoMensagem.Sucesso);
            else
            {
                ApresentarMensagem(resultadoValidacao, TipoMensagem.Erro);
                EditarRegistro();
            }
        }

        public void ExcluirRegistro()
        {
            ConfigurarTela("Excluindo uma revista...");

            bool temRegistros = VisualizarRegistros(TipoVisualizacao.Pesquisando);

            if (temRegistros == false)
                return;

            Console.Write("\nDigite o número da revista que deseja excluir: ");
            int id = Convert.ToInt32(Console.ReadLine());

            bool numeroEncontrado = controladorRevista.ExisteRevistaComEsteId(id);
            if (numeroEncontrado == false)
            {
                ApresentarMensagem("Nenhuma revista foi encontrado com este número: " + id, TipoMensagem.Erro);
                ExcluirRegistro();
                return;
            }

            bool conseguiuExcluir = controladorRevista.ExcluirRegistro(id);

            if (conseguiuExcluir)
                ApresentarMensagem("Revista excluída com sucesso", TipoMensagem.Sucesso);
            else
            {
                ApresentarMensagem("Falha ao tentar excluir a revista", TipoMensagem.Erro);
                ExcluirRegistro();
            }
        }

        public bool VisualizarRegistros(TipoVisualizacao tipo)
        {
            if (tipo == TipoVisualizacao.VisualizandoTela)
                ConfigurarTela("Visualizando revistas...");

            Revista[] revistas = controladorRevista.SelecionarTodasRevistas();

            if (revistas.Length == 0)
            {
                ApresentarMensagem("Nenhum revista cadastrado!", TipoMensagem.Atencao);
                return false;
            }

            string configuracaColunasTabela = "{0,-10} | {1,-25} | {2,-25} | {3,-25}";

            MontarCabecalhoTabela(configuracaColunasTabela, "Id", "Nome", "Coleção", "Caixa");

            foreach (Revista revista in revistas)
            {
                Console.WriteLine(configuracaColunasTabela, revista.id, revista.nome,
                    revista.colecao, revista.caixa.cor);
            }

            return true;
        }

        private Revista ObterRevista(TipoAcao tipoAcao)
        {
            telaCaixa.VisualizarRegistros(TipoVisualizacao.Pesquisando);

            Console.Write("\nDigite o número da caixa: ");
            int idCaixa = Convert.ToInt32(Console.ReadLine());

            bool numeroEncontrado = controladorCaixa.ExisteCaixaComEsteId(idCaixa);
            if (numeroEncontrado == false)
            {
                ApresentarMensagem("Nenhuma revista foi encontrada com este número: " 
                    + idCaixa, TipoMensagem.Erro);

                ConfigurarTela($"{tipoAcao} uma revista...");

                return ObterRevista(tipoAcao);
            }

            Caixa caixa = controladorCaixa.SelecionarCaixaPorId(idCaixa);

            Console.Write("Digite a nome da revista: ");
            string nome = Console.ReadLine();

            Console.Write("Digite a coleção da revista: ");
            string colecao = Console.ReadLine();

            Console.Write("Digite o número de edição da revista: ");
            int numeroEdicao = Convert.ToInt32(Console.ReadLine());

            return new Revista(nome, colecao, numeroEdicao, caixa);
        }

    }
}
