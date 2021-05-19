using ClubeLeitura.ConsoleApp.Controladores;
using ClubeLeitura.ConsoleApp.Dominio;
using System;

namespace ClubeLeitura.ConsoleApp.Telas
{
    public class TelaAmigo : TelaBase, ICadastravel
    {
        private readonly ControladorAmigo controladorAmigo;
        public TelaAmigo(ControladorAmigo controlador)
            : base("Cadastro de Amigos")
        {
            controladorAmigo = controlador;
        }

        public void InserirNovoRegistro()
        {
            ConfigurarTela("Inserindo um novo amigo...");

            Amigo amigo = ObterAmigo();

            string resultadoValidacao = controladorAmigo.InserirNovoAmigo(amigo);

            if (resultadoValidacao == "AMIGO_VALIDO")
                ApresentarMensagem("Amigo inserido com sucesso", TipoMensagem.Sucesso);
            else
            {
                ApresentarMensagem(resultadoValidacao, TipoMensagem.Erro);
                InserirNovoRegistro();
            }
        }

        public void EditarRegistro()
        {
            ConfigurarTela("Editando um amigo...");

            bool temRegistros = VisualizarRegistros(TipoVisualizacao.Pesquisando);

            if (temRegistros == false)
                return;

            Console.Write("\nDigite o número do amigo que deseja editar: ");
            int id = Convert.ToInt32(Console.ReadLine());

            bool numeroEncontrado = controladorAmigo.ExisteAmigoComEsteId(id);
            if (numeroEncontrado == false)
            {
                ApresentarMensagem("Nenhum amigo foi encontrado com este número: " + id, TipoMensagem.Erro);
                EditarRegistro();
                return;
            }

            Amigo amigo = ObterAmigo();

            string resultadoValidacao = controladorAmigo.EditarAmigo(id, amigo);

            if (resultadoValidacao == "AMIGO_VALIDO")
                ApresentarMensagem("Amigo editado com sucesso", TipoMensagem.Sucesso);
            else
            {
                ApresentarMensagem(resultadoValidacao, TipoMensagem.Erro);
                EditarRegistro();
            }
        }

        public void ExcluirRegistro()
        {
            ConfigurarTela("Excluindo um amigo...");

            bool temRegistros = VisualizarRegistros(TipoVisualizacao.Pesquisando);

            if (temRegistros == false)
                return;

            Console.Write("\nDigite o número do amigo que deseja excluir: ");
            int id = Convert.ToInt32(Console.ReadLine());

            bool numeroEncontrado = controladorAmigo.ExisteAmigoComEsteId(id);
            if (numeroEncontrado == false)
            {
                ApresentarMensagem("Nenhum amigo foi encontrado com este número: " + id, TipoMensagem.Erro);
                ExcluirRegistro();
                return;
            }

            bool conseguiuExcluir = controladorAmigo.ExcluirRegistro(id);

            if (conseguiuExcluir)
                ApresentarMensagem("Amigo excluído com sucesso", TipoMensagem.Sucesso);
            else
            {
                ApresentarMensagem("Falha ao tentar excluir o amigo", TipoMensagem.Erro);
                ExcluirRegistro();
            }
        }

        public bool VisualizarRegistros(TipoVisualizacao tipo)
        {
            if (tipo == TipoVisualizacao.VisualizandoTela)
                ConfigurarTela("Visualizando amigos...");

            Amigo[] amigos = controladorAmigo.SelecionarTodosAmigos();

            if (amigos.Length == 0)
            {
                ApresentarMensagem("Nenhum amigo cadastrado!", TipoMensagem.Atencao);
                return false;
            }

            string configuracaoColunasTabela = "{0,-10} | {1,-55} | {2,-35}";

            MontarCabecalhoTabela(configuracaoColunasTabela, "Id", "Nome", "Local");

            foreach (Amigo amigo in amigos)
            {
                Console.WriteLine(configuracaoColunasTabela, amigo.id, amigo.nome, amigo.deOndeEh);
            }

            return true;
        }

        private Amigo ObterAmigo()
        {
            Console.Write("Digite o nome do amiguinho: ");
            string nome = Console.ReadLine();

            Console.Write("Digite o nome do responsável: ");
            string nomeResponsavel = Console.ReadLine();

            Console.Write("Digite o telefone do amiguinho: ");
            string telefone = Console.ReadLine();

            Console.Write("Digite da onde é o amiguinho: ");
            string deOndeEh = Console.ReadLine();

            return new Amigo(nome, nomeResponsavel, telefone, deOndeEh);
        }
    }
}
