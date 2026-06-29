using ListaDeCompras.Compartilhado;
using ListaDeCompras.Modulos.ModuloItemLista; 
using ListaDeCompras.Modulos.ModuloItensDaLista;
using ListaDeCompras.Modulos.ModuloListaDeCompras;
using System;

namespace ListaDeCompras.Modulos.ModuloListaCompras
{
    public class TelaListaCompras : TelaBase, ITelaOpcoes
    {
        private readonly RepositorioListaCompras repositorioListaCompras;

        private RepositorioItensLista repositorioItemLista;

        public TelaListaCompras(RepositorioListaCompras repositorioListaCompras)
            : base("Lista de Compras", repositorioListaCompras)
        {
            this.repositorioListaCompras = repositorioListaCompras;
        }

        public void ConfigurarRepositorioItens(RepositorioItensLista repositorioItemLista)
        {
            this.repositorioItemLista = repositorioItemLista;
        }

        public override void VisualizarTodos(bool deveExibirCabecalho)
        {
            if (deveExibirCabecalho)
            {
                Console.WriteLine("----------------------------------------------------------------------------------");
                Console.WriteLine("Visualização de Listas de Compras");
                Console.WriteLine("----------------------------------------------------------------------------------");
            }

            Console.WriteLine(
                "{0, -5} | {1, -25} | {2, -15} | {3, -10} | {4, -12} | {5, -15}",
                "Id", "Nome da Lista", "Data Criação", "Status", "Total Itens", "Total Estimado"
            );

            EntidadeBase[] registros = repositorioListaCompras.SelecionarTodos();

            for (int i = 0; i < registros.Length; i++)
            {
                ListaCompras l = (ListaCompras)registros[i];

                if (l == null)
                    continue;

                int totalItens = 0;
                decimal totalEstimado = 0m;

                if (repositorioItemLista != null)
                {
                    EntidadeBase[] todosOsItens = repositorioItemLista.SelecionarTodos();
                    for (int j = 0; j < todosOsItens.Length; j++)
                    {
                        ItemLista item = (ItemLista)todosOsItens[j];
                        if (item != null && item.Lista.Id == l.Id)
                        {
                            totalItens++;
                            totalEstimado += item.Quantidade * item.Produto.PrecoAproximado;
                        }
                    }
                }

                Console.WriteLine(
                    "{0, -5} | {1, -25} | {2, -15:dd/MM/yyyy} | {3, -10} | {4, -12} | {5, -15:C}",
                    l.Id, l.Nome, l.DataCriacao, l.Status, totalItens, totalEstimado
                );
            }

            if (deveExibirCabecalho)
            {
                Console.WriteLine("----------------------------------------------------------------------------------");
                Console.WriteLine("Digite ENTER para continuar");
                Console.ReadLine();
            }
        }

        protected override EntidadeBase ObterDadosCadastrais()
        {
            string? nome;
            do
            {
                Console.Write("Informe o nome da lista [3 a 100 caracteres]: ");
                nome = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(nome) || nome.Length < 3 || nome.Length > 100)
                    Console.WriteLine("[Erro] O nome deve ter entre 3 e 100 caracteres.");

            } while (string.IsNullOrWhiteSpace(nome) || nome.Length < 3 || nome.Length > 100);

            Console.WriteLine("---------------------------------");
            Console.WriteLine("Status da Lista:");
            Console.WriteLine("1 - Aberta");
            Console.WriteLine("2 - Concluída");
            Console.WriteLine("---------------------------------");
            Console.Write("Informe o status (Padrão: 1): ");

            string? opcaoStatus = Console.ReadLine();
            StatusLista status = (opcaoStatus == "2") ? StatusLista.Concluida : StatusLista.Aberta;

            return new ListaCompras(nome, status);
        }

        protected override bool ExistemDependenciasAtivasDoRegistro(int idRegistro)
        {
            if (repositorioItemLista != null)
            {
                EntidadeBase[] todosOsItens = repositorioItemLista.SelecionarTodos();
                for (int i = 0; i < todosOsItens.Length; i++)
                {
                    ItemLista item = (ItemLista)todosOsItens[i];
                    if (item != null && item.Lista.Id == idRegistro)
                    {
                        Console.WriteLine("---------------------------------");
                        Console.WriteLine("Erro: Não é possível excluir uma lista que possua itens vinculados!");
                        Console.WriteLine("Remova os itens primeiro.");
                        Console.WriteLine("---------------------------------");
                        return true; 
                    }
                }
            }

            return base.ExistemDependenciasAtivasDoRegistro(idRegistro);
        }
    }
}