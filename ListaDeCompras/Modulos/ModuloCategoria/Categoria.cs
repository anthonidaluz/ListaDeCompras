using ListaDeCompras.Compartilhado;
using System;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;
using System.Text;

namespace ListaDeCompras.Modulos.ModuloCategoria
{

    public static class GeradorIdsCategoria
    {
        private static int contadorIds = 1;
        public static int GerarId()
        {
            return contadorIds++;
        }

    }
    public enum CorCategoria
    {
        Branco,
        Vermelho,
        Verde,
        Azul
    }

    public class Categoria : EntidadeBase
    {
        public string Nome { get; private set; }
        public CorCategoria Cor { get; private set; }

        public Categoria(string nome, CorCategoria cor)
        {
            Id = GeradorIdsCategoria.GerarId();
            Nome = nome;
            Cor = cor;
        }

        public override void Atualizar(EntidadeBase entidadeAtualizada)
        {
            Categoria categoriaAtualizada = (Categoria)entidadeAtualizada;

            Nome = categoriaAtualizada.Nome;
            Cor = categoriaAtualizada.Cor;
        }

    }
}
