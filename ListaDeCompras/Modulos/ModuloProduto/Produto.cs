using ListaDeCompras.Compartilhado;
using ListaDeCompras.Modulos.ModuloCategoria;
using System;
using System.Collections.Generic;
using System.Text;

namespace ListaDeCompras.Modulos.ModuloProduto
{
    public enum TipoUnidadeMedida
    {
        Quilograma,
        Unidade,
        Litro,
        Caixa
    }

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

        public TipoUnidadeMedida UnidadeMedida { get; private set; }

        public decimal PrecoAproximado { get; private set; }

        public Produto(string nome, Categoria categoria, TipoUnidadeMedida unidadeMedida, decimal precoAproximado)
        {
            Id = GeradorIdsProduto.GerarId();
            Nome = nome;
            Categoria = categoria;
            UnidadeMedida = unidadeMedida;
            PrecoAproximado = precoAproximado;
        }

        public override void Atualizar(EntidadeBase entidadeAtualizada)
        {
            Produto produtoAtualizado = (Produto)entidadeAtualizada;

            Nome = produtoAtualizado.Nome;
            Categoria = produtoAtualizado.Categoria;
            UnidadeMedida = produtoAtualizado.UnidadeMedida;
            PrecoAproximado = produtoAtualizado.PrecoAproximado;
        }
    }
}