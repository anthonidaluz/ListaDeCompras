using ListaDeCompras.Compartilhado;
using ListaDeCompras.Modulos.ModuloItensDaLista;
using ListaDeCompras.Modulos.ModuloListaCompras;
using ListaDeCompras.Modulos.ModuloListaDeCompras;
using ListaDeCompras.Modulos.ModuloProduto;
using System;

namespace ListaDeCompras.Modulos.ModuloItemLista
{
    public class TelaItensLista : TelaBase, ITelaOpcoes
    {
        private readonly RepositorioItensLista repositorioItemLista;
        private readonly RepositorioListaCompras repositorioListaCompras;
        private readonly RepositorioProduto repositorioProduto;
        private readonly TelaListaCompras telaListaCompras;
        private readonly TelaProduto telaProduto;

        public TelaItensLista(
            RepositorioItensLista repositorioItemLista,
            RepositorioListaCompras repositorioListaCompras,
            RepositorioProduto repositorioProduto,
            TelaListaCompras telaListaCompras,
            TelaProduto telaProduto)
            : base("Item da Lista", repositorioItemLista)
        {
            this.repositorioItemLista = repositorioItemLista;
            this.repositorioListaCompras = repositorioListaCompras;
            this.repositorioProduto = repositorioProduto;
            this.telaListaCompras = telaListaCompras;
            this.telaProduto = telaProduto;
        }

        public override void VisualizarTodos(bool deveExibirCabecalho)
        {
            if (deveExibirCabecalho)
            {
                Console.WriteLine("------------------------------------------------------------------------------------------");
                Console.WriteLine("Visualização de Itens nas Listas");
                Console.WriteLine("------------------------------------------------------------------------------------------");
            }

            Console.WriteLine(
                "{0, -5} | {1, -20} | {2, -20} | {3, -15} | {4, -10} | {5, -12}",
                "Id", "Lista", "Produto", "Categoria", "Qtd", "Total (R$)"
            );

            EntidadeBase[] registros = repositorioItemLista.SelecionarTodos();

            for (int i = 0; i < registros.Length; i++)
            {
                ItemLista item = (ItemLista)registros[i];

                if (item == null)
                    continue;

                decimal valorTotalItem = item.Quantidade * item.Produto.PrecoAproximado;

                Console.WriteLine(
                    "{0, -5} | {1, -20} | {2, -20} | {3, -15} | {4, -10} | {5, -12:F2}",
                    item.Id, item.Lista.Nome, item.Produto.Nome, item.Produto.Categoria.Nome, item.Quantidade, valorTotalItem
                );
            }

            if (deveExibirCabecalho)
            {
                Console.WriteLine("------------------------------------------------------------------------------------------");
                Console.WriteLine("Digite ENTER para continuar");
                Console.ReadLine();
            }
        }

        protected override EntidadeBase ObterDadosCadastrais()
        {
            Console.WriteLine("---------------------------------");
            Console.WriteLine("Selecione a Lista de Compras:");
            Console.WriteLine("---------------------------------");
            telaListaCompras.VisualizarTodos(false);
            Console.Write("Informe o ID da Lista: ");
            int idLista = Convert.ToInt32(Console.ReadLine());
            ListaCompras listaSelecionada = (ListaCompras)repositorioListaCompras.SelecionarPorId(idLista);

            Console.WriteLine("---------------------------------");
            Console.WriteLine("Selecione o Produto (A Categoria será exibida abaixo):");
            Console.WriteLine("---------------------------------");
            telaProduto.VisualizarTodos(false);
            Console.Write("Informe o ID do Produto: ");
            int idProduto = Convert.ToInt32(Console.ReadLine());
            Produto produtoSelecionado = (Produto)repositorioProduto.SelecionarPorId(idProduto);

            decimal quantidade = 0;
            bool qtdValida = false;
            do
            {
                Console.Write($"Informe a quantidade ({produtoSelecionado.UnidadeMedida}): ");
                string? inputQtd = Console.ReadLine();
                qtdValida = decimal.TryParse(inputQtd, out quantidade);

                if (!qtdValida || quantidade <= 0)
                {
                    Console.WriteLine("[Erro] A quantidade deve ser um número positivo maior que zero.");
                    qtdValida = false;
                }

            } while (!qtdValida);

            return new ItemLista(listaSelecionada, produtoSelecionado, quantidade);
        }

        protected override bool ExisteRegistroComInformacoesExclusivas(EntidadeBase entidade, int? idIgnorado = null)
        {
            ItemLista novoItem = (ItemLista)entidade;
            EntidadeBase[] itens = repositorioItemLista.SelecionarTodos();

            for (int i = 0; i < itens.Length; i++)
            {
                ItemLista itemExistente = (ItemLista)itens[i];

                if (itemExistente == null)
                    continue;

                if (idIgnorado != itemExistente.Id &&
                    novoItem.Lista.Id == itemExistente.Lista.Id &&
                    novoItem.Produto.Id == itemExistente.Produto.Id)
                {
                    Console.WriteLine("---------------------------------");
                    Console.WriteLine($"O produto \"{novoItem.Produto.Nome}\" já está na lista \"{novoItem.Lista.Nome}\"!");
                    Console.WriteLine("---------------------------------");

                    return true;
                }
            }

            return base.ExisteRegistroComInformacoesExclusivas(entidade);
        }
    }
}