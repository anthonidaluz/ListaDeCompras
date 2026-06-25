using ListaDeCompras.Compartilhado;
using System;

namespace ListaDeCompras.Modulos.ModuloListaCompras
{
    public enum StatusLista
    {
        Aberta,
        Concluida
    }

    public static class GeradorIdsListaCompras
    {
        private static int contadorIds = 1;
        public static int GerarId()
        {
            return contadorIds++;
        }
    }

    public class ListaCompras : EntidadeBase
    {
        public string Nome { get; private set; }
        public DateTime DataCriacao { get; private set; }
        public StatusLista Status { get; private set; }

        public ListaCompras(string nome, StatusLista status = StatusLista.Aberta)
        {
            Id = GeradorIdsListaCompras.GerarId();
            Nome = nome;
            DataCriacao = DateTime.Now; 
            Status = status;
        }

        public override void Atualizar(EntidadeBase entidadeAtualizada)
        {
            ListaCompras listaAtualizada = (ListaCompras)entidadeAtualizada;

            Nome = listaAtualizada.Nome;
            Status = listaAtualizada.Status;
        }
    }
}