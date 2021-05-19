using ClubeLeitura.ConsoleApp.Controladores;
using ClubeLeitura.ConsoleApp.Dominio;
using System;

namespace ClubeLeitura.ConsoleApp.Telas
{
    public class TelaCaixa : TelaBase, ICadastravel
    {
        private readonly ControladorCaixa controladorCaixa;
        public TelaCaixa(ControladorCaixa controlador)
            : base("Cadastro de Caixas")
        {
            controladorCaixa = controlador;
        }

        public void InserirNovoRegistro()
        {
            ConfigurarTela("Inserindo uma nova caixa...");

            Caixa caixa = ObterCaixa();

            string resultadoValidacao = controladorCaixa.InserirNovaCaixa(caixa);

            if (resultadoValidacao == "CAIXA_VALIDA")
                ApresentarMensagem("Caixa inserida com sucesso", TipoMensagem.Sucesso);
            else
            {
                ApresentarMensagem(resultadoValidacao, TipoMensagem.Erro);
                InserirNovoRegistro();
            }
        }

        public void EditarRegistro()
        {
            ConfigurarTela("Editando uma caixa...");

            bool temRegistros = VisualizarRegistros(TipoVisualizacao.Pesquisando);

            if (temRegistros == false)
                return;

            Console.Write("\nDigite o número da caixa que deseja editar: ");
            int id = Convert.ToInt32(Console.ReadLine());

            bool numeroEncontrado = controladorCaixa.ExisteCaixaComEsteId(id);
            if (numeroEncontrado == false)
            {
                ApresentarMensagem("Nenhuma caixa foi encontrada com este número: " + id, TipoMensagem.Erro);
                EditarRegistro();
                return;
            }

            Caixa caixa = ObterCaixa();

            string resultadoValidacao = controladorCaixa.EditarCaixa(id, caixa);

            if (resultadoValidacao == "CAIXA_VALIDA")
                ApresentarMensagem("Caixa editada com sucesso", TipoMensagem.Sucesso);
            else
            {
                ApresentarMensagem(resultadoValidacao, TipoMensagem.Erro);
                EditarRegistro();
            }
        }

        public void ExcluirRegistro()
        {
            ConfigurarTela("Excluindo uma caixa...");

            bool temRegistros = VisualizarRegistros(TipoVisualizacao.Pesquisando);

            if (temRegistros == false)
                return;

            Console.Write("\nDigite o número da caixa que deseja excluir: ");
            int id = Convert.ToInt32(Console.ReadLine());

            bool numeroEncontrado = controladorCaixa.ExisteCaixaComEsteId(id);
            if (numeroEncontrado == false)
            {
                ApresentarMensagem("Nenhuma caixa foi encontrado com este número: " + id, TipoMensagem.Erro);
                ExcluirRegistro();
                return;
            }

            bool conseguiuExcluir = controladorCaixa.ExcluirRegistro(id);

            if (conseguiuExcluir)
                ApresentarMensagem("Caixa excluída com sucesso", TipoMensagem.Sucesso);
            else
            {
                ApresentarMensagem("Falha ao tentar excluir a caixa", TipoMensagem.Erro);
                ExcluirRegistro();
            }
        }

        
        public bool VisualizarRegistros(TipoVisualizacao tipo)
        {
            if (tipo == TipoVisualizacao.VisualizandoTela)
                ConfigurarTela("Visualizando caixas...");

            Caixa[] caixas = controladorCaixa.SelecionarTodasCaixas();

            if (caixas.Length == 0)
            {
                ApresentarMensagem("Nenhuma caixa cadastrada!", TipoMensagem.Atencao);
                return false;
            }

            string configuracaColunasTabela = "{0,-10} | {1,-55} | {2,-35}";

            MontarCabecalhoTabela(configuracaColunasTabela, "Id", "Etiqueta", "Cor");

            foreach (Caixa caixa in caixas)
            {
                Console.WriteLine(configuracaColunasTabela, caixa.id, caixa.etiqueta, caixa.cor);
            }

            return true;
        }

        private Caixa ObterCaixa()
        {
            Console.Write("Digite a etiqueta da caixa: ");
            string etiqueta = Console.ReadLine();

            Console.Write("Digite a cor da caixa: ");
            string cor = Console.ReadLine();

            return new Caixa(cor, etiqueta);
        }
    }
}