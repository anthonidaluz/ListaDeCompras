using ListaDeCompras.Compartilhado;
using ListaDeCompras.ConsoleApp.Modulos.ModuloCategoria;
using ListaDeCompras.Modulos.ModuloCategoria;
using System;

namespace ListaDeCompras.Modulos.ModuloProduto
{
    public class TelaProduto : TelaBase, ITelaOpcoes
    {
        private readonly RepositorioProduto repositorioProduto;
        private readonly RepositorioCategoria repositorioCategoria;
        private readonly TelaCategoria telaCategoria;

        public TelaProduto(
            RepositorioProduto repositorioProduto,
            RepositorioCategoria repositorioCategoria,
            TelaCategoria telaCategoria)
            : base("Produto", repositorioProduto)
        {
            this.repositorioProduto = repositorioProduto;
            this.repositorioCategoria = repositorioCategoria;
            this.telaCategoria = telaCategoria;
        }

        public override void VisualizarTodos(bool deveExibirCabecalho)
        {
            if (deveExibirCabecalho)
            {
                Console.WriteLine("------------------------------------------------------------------");
                Console.WriteLine("Visualização de Produtos");
                Console.WriteLine("------------------------------------------------------------------");
            }

            Console.WriteLine(
                "{0, -5} | {1, -20} | {2, -15} | {3, -15} | {4, -10}",
                "Id", "Nome", "Categoria", "Unidade", "Preço (R$)"
            );

            EntidadeBase[] registros = repositorioProduto.SelecionarTodos();

            for (int i = 0; i < registros.Length; i++)
            {
                Produto p = (Produto)registros[i];

                if (p == null)
                    continue;

                Console.WriteLine(
                    "{0, -5} | {1, -20} | {2, -15} | {3, -15} | {4, -10:F2}",
                    p.Id, p.Nome, p.Categoria.Nome, p.UnidadeMedida, p.PrecoAproximado
                );
            }

            if (deveExibirCabecalho)
            {
                Console.WriteLine("------------------------------------------------------------------");
                Console.WriteLine("Digite ENTER para continuar");
                Console.ReadLine();
            }
        }

        protected override EntidadeBase ObterDadosCadastrais()
        {
            string? nome;
            do
            {
                Console.Write("Informe o nome do produto (2 a 100 caracteres): ");
                nome = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(nome) || nome.Length < 2 || nome.Length > 100)
                    Console.WriteLine("[Erro] O nome deve ter entre 2 e 100 caracteres. Tente novamente.");

            } while (string.IsNullOrWhiteSpace(nome) || nome.Length < 2 || nome.Length > 100);

            Console.WriteLine("---------------------------------");
            Console.WriteLine("Selecione uma categoria para o produto");
            Console.WriteLine("---------------------------------");

            telaCategoria.VisualizarTodos(false);

            Console.Write("Informe o ID da categoria escolhida: ");
            int idCategoriaSelecionada = Convert.ToInt32(Console.ReadLine());

            Categoria categoriaSelecionada = (Categoria)repositorioCategoria.SelecionarPorId(idCategoriaSelecionada);

            Console.WriteLine("---------------------------------");
            Console.WriteLine("Selecione a unidade de medida do produto");
            Console.WriteLine("---------------------------------");
            Console.WriteLine("1 - Quilograma");
            Console.WriteLine("2 - Unidade");
            Console.WriteLine("3 - Litro");
            Console.WriteLine("4 - Caixa");
            Console.WriteLine("---------------------------------");
            Console.Write("Informe a unidade escolhida: ");

            string? unidadeSelecionada = Console.ReadLine();
            TipoUnidadeMedida unidade;

            switch (unidadeSelecionada)
            {
                case "1":
                    unidade = TipoUnidadeMedida.Quilograma;
                    break;
                case "2":
                    unidade = TipoUnidadeMedida.Unidade;
                    break;
                case "3":
                    unidade = TipoUnidadeMedida.Litro;
                    break;
                case "4":
                    unidade = TipoUnidadeMedida.Caixa;
                    break;
                default:
                    unidade = TipoUnidadeMedida.Unidade;
                    break;
            }

            decimal precoAproximado = 0;
            bool precoValido = false;

            do
            {
                Console.Write("Informe o preço aproximado (ex: 15,90): ");
                string? inputPreco = Console.ReadLine();
                precoValido = decimal.TryParse(inputPreco, out precoAproximado);

                if (!precoValido)
                    Console.WriteLine("[Erro] Preço inválido. Digite um valor numérico.");

            } while (!precoValido);

            return new Produto(nome, categoriaSelecionada, unidade, precoAproximado);
        }

        protected override bool ExisteRegistroComInformacoesExclusivas(EntidadeBase entidade, int? idIgnorado = null)
        {
            Produto novoProduto = (Produto)entidade;
            EntidadeBase[] produtos = repositorioProduto.SelecionarTodos();

            for (int i = 0; i < produtos.Length; i++)
            {
                Produto p = (Produto)produtos[i];

                if (p == null)
                    continue;

                if (idIgnorado != p.Id &&
                    novoProduto.Nome.Equals(p.Nome, StringComparison.OrdinalIgnoreCase) &&
                    novoProduto.Categoria.Id == p.Categoria.Id)
                {
                    Console.WriteLine("---------------------------------");
                    Console.WriteLine($"Já existe um produto com o nome \"{p.Nome}\" cadastrado na categoria \"{p.Categoria.Nome}\"!");
                    Console.WriteLine("---------------------------------");

                    return true;
                }
            }

            return base.ExisteRegistroComInformacoesExclusivas(entidade);
        }
    }
}