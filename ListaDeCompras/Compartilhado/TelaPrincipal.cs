using System;
using System.Collections.Generic;
using System.Text;

namespace ListaDeCompras.Compartilhado
{
    public class TelaPrincipal
    {
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
                return null;

            if (opcaoMenuPrincipal == "2")
                return null;

            if (opcaoMenuPrincipal == "3")
                return null;

            return null;
        }
    }
}