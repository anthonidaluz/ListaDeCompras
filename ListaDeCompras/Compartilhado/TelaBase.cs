using System;
using System.Collections.Generic;
using System.Text;

namespace ListaDeCompras.Compartilhado
{
    public abstract class TelaBase
    {
        private string nomeEntidade = string.Empty;
        private RepositorioBase repositorio;

        protected TelaBase(string nomeEntidade, RepositorioBase repositorio)
        {
            this.nomeEntidade = nomeEntidade;
            this.repositorio = repositorio;
        }

        public virtual string? ObterOpcaoMenu()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("---------------------------------");
            Console.WriteLine($"Gestão de {nomeEntidade}s");
            Console.WriteLine("---------------------------------");
            Console.WriteLine($"1 - Cadastrar {nomeEntidade}");
            Console.WriteLine($"2 - Editar {nomeEntidade}");
            Console.WriteLine($"3 - Excluir {nomeEntidade}");
            Console.WriteLine($"4 - Visualizar {nomeEntidade}s");
            Console.WriteLine("S - Sair");
            Console.WriteLine("---------------------------------");
            Console.Write("> ");

            string? opcaoMenuInterno = Console.ReadLine()?.ToUpper();

            return opcaoMenuInterno;
        }

        public void Cadastrar()
        {
            Console.WriteLine("---------------------------------");
            Console.WriteLine($"Cadastro de {nomeEntidade}");
            Console.WriteLine("---------------------------------");

            EntidadeBase novaEntidade = ObterDadosCadastrais();

            if (ExisteRegistroComInformacoesExclusivas(novaEntidade))
            {
                Console.WriteLine("Digite ENTER para continuar");
                Console.ReadLine();
                return;
            }

            repositorio.Cadastrar(novaEntidade);

            Console.WriteLine("---------------------------------");
            Console.WriteLine($"O registro \"{novaEntidade.Id}\" foi cadastrado com sucesso!");
            Console.WriteLine("---------------------------------");
            Console.WriteLine("Digite ENTER para continuar");
            Console.ReadLine();
        }

        public void Editar()
        {
            Console.WriteLine("---------------------------------");
            Console.WriteLine($"Edição de {nomeEntidade}");
            Console.WriteLine("---------------------------------");

            VisualizarTodos(false);

            Console.WriteLine("---------------------------------");

            Console.Write("Digite o ID do registro que deseja editar: ");
            int idSelecionado = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("---------------------------------");

            EntidadeBase entidadeAtualizada = ObterDadosCadastrais();

            if (ExisteRegistroComInformacoesExclusivas(entidadeAtualizada, idSelecionado))
            {
                Console.WriteLine("Digite ENTER para continuar");
                Console.ReadLine();
                return;
            }

            repositorio.Editar(idSelecionado, entidadeAtualizada);

            Console.WriteLine("---------------------------------");
            Console.WriteLine($"O registro \"{entidadeAtualizada.Id}\" foi editado com sucesso!");
            Console.WriteLine("---------------------------------");
            Console.WriteLine("Digite ENTER para continuar");
            Console.ReadLine();
        }

        public void Excluir()
        {
            Console.WriteLine("---------------------------------");
            Console.WriteLine($"Exclusão de {nomeEntidade}");
            Console.WriteLine("---------------------------------");

            VisualizarTodos(false);

            Console.WriteLine("---------------------------------");

            Console.Write("Digite o ID do registro que deseja excluir: ");
            int idSelecionado = Convert.ToInt32(Console.ReadLine());

            if (ExistemDependenciasAtivasDoRegistro(idSelecionado))
            {
                Console.WriteLine("Digente ENTER para continuar");
                Console.ReadLine();
                return;

            }

            repositorio.Excluir(idSelecionado);

            Console.WriteLine("---------------------------------");
            Console.WriteLine($"O registro \"{idSelecionado}\" foi excluído com sucesso!");
            Console.WriteLine("---------------------------------");
            Console.WriteLine("Digite ENTER para continuar");
            Console.ReadLine();
        }

        public abstract void VisualizarTodos(bool deveExibirCabecalho);

        protected abstract EntidadeBase ObterDadosCadastrais();

        protected virtual bool ExisteRegistroComInformacoesExclusivas(EntidadeBase entidade, int? idIgnorado = null)
        {
            return false;
        }

        protected virtual bool ExistemDependenciasAtivasDoRegistro(int idRegistro)
        {
            return false;
        }


    }
}