using ListaDeCompras.Compartilhado;
using ListaDeCompras.Modulos.ModuloListaCompras;
using ListaDeCompras.Modulos.ModuloProduto;

namespace ListaDeCompras.Modulos.ModuloItemLista
{
    public static class GeradorIdsItemLista
    {
        private static int contadorIds = 1;
        public static int GerarId()
        {
            return contadorIds++;
        }
    }

    public class ItemLista : EntidadeBase
    {
        public ListaCompras Lista { get; private set; }
        public Produto Produto { get; private set; }
        public decimal Quantidade { get; private set; }

        public ItemLista(ListaCompras lista, Produto produto, decimal quantidade)
        {
            Id = GeradorIdsItemLista.GerarId();
            Lista = lista;
            Produto = produto;
            Quantidade = quantidade;
        }

        public override void Atualizar(EntidadeBase entidadeAtualizada)
        {
            ItemLista itemAtualizado = (ItemLista)entidadeAtualizada;

            Lista = itemAtualizado.Lista;
            Produto = itemAtualizado.Produto;
            Quantidade = itemAtualizado.Quantidade;
        }
    }
}