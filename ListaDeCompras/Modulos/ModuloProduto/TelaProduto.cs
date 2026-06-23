using ListaDeCompras.Compartilhado;
using ListaDeCompras.ConsoleApp.Modulos.ModuloCategoria;
using ListaDeCompras.Modulos.ModuloCategoria; 
using System;

namespace ListaDeCompras.Modulos.ModuloProduto
{
    public class TelaProduto : TelaBase, ITelaOpcoes
    {
        // Precisamos guardar as ferramentas (dependências) que recebemos no construtor
        private readonly RepositorioProduto repositorioProduto;
        private readonly RepositorioCategoria repositorioCategoria;
        private readonly TelaCategoria telaCategoria;

        // O Construtor agora recebe tudo que a tela precisa para funcionar
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
                Console.WriteLine("---------------------------------");
                Console.WriteLine("Visualização de Produtos");
                Console.WriteLine("---------------------------------");
            }

            // O cabeçalho da nossa tabela
            Console.WriteLine(
                "{0, -7} | {1, -20} | {2, -20}",
                "Id", "Nome", "Categoria"
            );

            // Pegamos todos os produtos do repositório genérico
            EntidadeBase[] registros = repositorioProduto.SelecionarTodos();

            for (int i = 0; i < registros.Length; i++)
            {
                Produto p = (Produto)registros[i];

                if (p == null)
                    continue;

                // Aqui está o pulo do gato: Acessamos o Nome da Categoria DE DENTRO do Produto!
                Console.WriteLine(
                    "{0, -7} | {1, -20} | {2, -20}",
                    p.Id, p.Nome, p.Categoria.Nome
                );
            }

            if (deveExibirCabecalho)
            {
                Console.WriteLine("---------------------------------");
                Console.WriteLine("Digite ENTER para continuar");
                Console.ReadLine();
            }
        }

        protected override EntidadeBase ObterDadosCadastrais()
        {
            Console.Write("Informe o nome do produto: ");
            string? nome = Console.ReadLine();

            Console.WriteLine("---------------------------------");
            Console.WriteLine("Selecione uma categoria para o produto");
            Console.WriteLine("---------------------------------");

            // A MÁGICA ACONTECE AQUI:
            // Nós chamamos a tela de categorias para listar todas as categorias cadastradas na tela do usuário
            telaCategoria.VisualizarTodos(false);

            Console.Write("Informe o ID da categoria escolhida: ");
            int idCategoriaSelecionada = Convert.ToInt32(Console.ReadLine());

            // Agora, vamos no repositório de categorias e buscamos o objeto completo correspondente àquele ID
            Categoria categoriaSelecionada = (Categoria)repositorioCategoria.SelecionarPorId(idCategoriaSelecionada);

            // Se o usuário digitou um ID válido, a categoriaSelecionada vai estar preenchida.
            // Agora é só construir e retornar o novo Produto!
            return new Produto(nome!, categoriaSelecionada);
        }

        // Validação: Não deixar cadastrar dois produtos com o mesmo nome
        protected override bool ExisteRegistroComInformacoesExclusivas(EntidadeBase entidade, int? idIgnorado = null)
        {
            Produto novoProduto = (Produto)entidade;

            EntidadeBase[] produtos = repositorioProduto.SelecionarTodos();

            for (int i = 0; i < produtos.Length; i++)
            {
                Produto p = (Produto)produtos[i];

                if (p == null)
                    continue;

                if (idIgnorado != p.Id && novoProduto.Nome == p.Nome)
                {
                    Console.WriteLine("---------------------------------");
                    Console.WriteLine($"Já existe um produto com o nome \"{p.Nome}\"!");
                    Console.WriteLine("---------------------------------");

                    return true;
                }
            }

            return base.ExisteRegistroComInformacoesExclusivas(entidade);
        }
    }
}