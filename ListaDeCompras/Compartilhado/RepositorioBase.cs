using System;
using System.Collections.Generic;
using System.Text;

namespace ListaDeCompras.Compartilhado
{
    public abstract class RepositorioBase
    {
        private EntidadeBase[] registros = new EntidadeBase[100];

        public void Cadastrar(EntidadeBase novoRegistro)
        {
            for (int i = 0; i < registros.Length; i++)
            {
                if (registros[i] == null)
                {
                    registros[i] = novoRegistro;
                    break;
                }
            }
        }

        public bool Editar(int idSelecionado, EntidadeBase entidadeAtualizada)
        {
            EntidadeBase? entidadeSelecionada = SelecionarPorId(idSelecionado);

            if (entidadeSelecionada == null)
                return false;

            entidadeSelecionada.Atualizar(entidadeAtualizada);

            return true;
        }

        public bool Excluir(int idSelecionado)
        {
            for (int i = 0; i < registros.Length; i++)
            {
                EntidadeBase o = registros[i];

                if (o == null)
                    continue;

                if (o.Id == idSelecionado)
                {
                    registros[i] = null;
                    return true;
                }
            }

            return false;
        }

        public EntidadeBase? SelecionarPorId(int idSelecionado)
        {
            for (int i = 0; i < registros.Length; i++)
            {
                EntidadeBase o = registros[i];

                if (o == null)
                    continue;

                if (o.Id == idSelecionado)
                    return o;
            }

            return null;
        }

        public EntidadeBase[] SelecionarTodos()
        {
            return registros;
        }
    }
}
