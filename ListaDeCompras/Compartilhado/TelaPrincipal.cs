using ListaDeCompras.ConsoleApp.Modulos.ModuloCategoria;
using ListaDeCompras.Modulos.ModuloCategoria;
using ListaDeCompras.Modulos.ModuloProduto;
using System;
using System.Collections.Generic;
using System.Text;

namespace ListaDeCompras.Compartilhado
{
    public class TelaPrincipal
    {
        private readonly RepositorioCategoria repositorioCategoria;
        private readonly TelaCategoria telaCategoria;

        private readonly RepositorioProduto repositorioProduto;
        private readonly TelaProduto telaProduto;

        public TelaPrincipal()
        {
            repositorioCategoria = new RepositorioCategoria();
            telaCategoria = new TelaCategoria(repositorioCategoria);

            repositorioProduto = new RepositorioProduto();
            telaProduto = new TelaProduto(repositorioProduto, repositorioCategoria, telaCategoria);
        }

        public ITelaOpcoes? ObterOpcaoMenuPrincipal()
        {
            Console.WriteLine("---------------------------------");
            Console.WriteLine("Lista de Compras");
            Console.WriteLine("---------------------------------");
            Console.WriteLine("1 - Gerenciar categorias");
            Console.WriteLine("2 - Gerenciar produtos");
            Console.WriteLine("3 - Gerenciar listas de compras");
            Console.WriteLine("S - Sair");
            Console.WriteLine("---------------------------------");
            Console.Write("> ");

            string? opcaoMenuPrincipal = Console.ReadLine()?.ToUpper();

            if (opcaoMenuPrincipal == "1")
                return telaCategoria;

            if (opcaoMenuPrincipal == "2")
                return telaProduto;

            if (opcaoMenuPrincipal == "3")
                return null;

            return null;
        }
    }
}