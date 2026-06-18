using System;
using System.Collections.Generic;
using System.Text;

namespace ListaDeCompras.Compartilhado
{
    public abstract class EntidadeBase
    {
        public int Id { get; set; }

        public abstract void Atualizar(EntidadeBase entidadeAtualziada);

    }
}
