using ListaDeCompras.Compartilhado;
using ListaDeCompras.Modulos.ModuloCategoria;
using System;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using System.Text;

namespace ListaDeCompras.Modulos.ModuloProduto
{
    public static class GeradorIdsProduto
    {
        private static int contadorIds = 1;
        public static int GerarId()
        {
            return contadorIds++;
        }

    }
    public class Produto : EntidadeBase
    {

        public string Nome { get; private set; }
        public Categoria Categoria { get; private set; }


        public Produto(string nome, Categoria categoria)
        {
            Id = GeradorIdsProduto.GerarId();
            Nome = nome;
            Categoria = categoria;
        }

        public override void Atualizar(EntidadeBase entidadeAtualizada)
        {
            Produto produtoAtualizado = (Produto)entidadeAtualizada;

            Nome = produtoAtualizado.Nome;
            Categoria = produtoAtualizado.Categoria;
        }
    }
}
