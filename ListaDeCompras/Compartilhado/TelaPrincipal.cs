using ListaDeCompras.ConsoleApp.Modulos.ModuloCategoria;
using ListaDeCompras.Modulos.ModuloCategoria;
using ListaDeCompras.Modulos.ModuloItemLista;
using ListaDeCompras.Modulos.ModuloItensDaLista;
using ListaDeCompras.Modulos.ModuloListaCompras;
using ListaDeCompras.Modulos.ModuloListaDeCompras;
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

        private readonly RepositorioListaCompras repositorioListaCompras;
        private readonly TelaListaCompras telaListaCompras;

        private readonly RepositorioItensLista repositorioItensLista;
        private readonly TelaItensLista telaItensLista;

        public TelaPrincipal()
        {
            repositorioCategoria = new RepositorioCategoria();
            telaCategoria = new TelaCategoria(repositorioCategoria);

            repositorioProduto = new RepositorioProduto();
            telaProduto = new TelaProduto(repositorioProduto, repositorioCategoria, telaCategoria);

            repositorioListaCompras = new RepositorioListaCompras();
            telaListaCompras = new TelaListaCompras(repositorioListaCompras);

            repositorioItensLista = new RepositorioItensLista();

            telaItensLista = new TelaItensLista(
                repositorioItensLista,
                repositorioListaCompras,
                repositorioProduto,
                telaListaCompras,
                telaProduto
            );

            // Injetando o repositório de itens na tela de listas para calcular as somas automáticas
            telaListaCompras.ConfigurarRepositorioItens(repositorioItensLista);
        }

        public ITelaOpcoes? ObterOpcaoMenuPrincipal()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("---------------------------------");
            Console.WriteLine("Lista de Compras");
            Console.WriteLine("---------------------------------");
            Console.WriteLine("1 - Gerenciar categorias");
            Console.WriteLine("2 - Gerenciar produtos");
            Console.WriteLine("3 - Gerenciar listas de compras");
            Console.WriteLine("4 - Gerenciar itens das listas");
            Console.WriteLine("S - Sair");
            Console.WriteLine("---------------------------------");
            Console.Write("> ");

            string? opcaoMenuPrincipal = Console.ReadLine()?.ToUpper();

            if (opcaoMenuPrincipal == "1")
                return telaCategoria;

            if (opcaoMenuPrincipal == "2")
                return telaProduto;

            if (opcaoMenuPrincipal == "3")
                return telaListaCompras;

            if (opcaoMenuPrincipal == "4")
                return telaItensLista;

            return null;
        }
    }
}