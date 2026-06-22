using ListaDeCompras.Compartilhado;
using ListaDeCompras.Modulos.ModuloCategoria;

namespace ListaDeCompras.ConsoleApp.Modulos.ModuloCategoria;

public class TelaCategoria : TelaBase, ITelaOpcoes
{
    private readonly RepositorioCategoria repositorioCategoria;

    public TelaCategoria(RepositorioCategoria repositorioCategoria) : base("Categoria", repositorioCategoria)
    {
        this.repositorioCategoria = repositorioCategoria;
    }

    public override void VisualizarTodos(bool deveExibirCabecalho)
    {
        if (deveExibirCabecalho)
        {
            Console.WriteLine("---------------------------------");
            Console.WriteLine("Visualização de Categorias");
            Console.WriteLine("---------------------------------");
        }

        Console.WriteLine(
            "{0, -7} | {1, -20} | {2, -10}",
            "Id", "Nome", "Cor"
        );

        EntidadeBase[] registros = repositorioCategoria.SelecionarTodos();

        for (int i = 0; i < registros.Length; i++)
        {
            Categoria c = (Categoria)registros[i];

            if (c == null)
                continue;

            Console.WriteLine(
                "{0, -7} | {1, -20} | {2, -10}",
                c.Id, c.Nome, c.Cor
            );
        }

        if (deveExibirCabecalho)
        {
            Console.WriteLine("---------------------------------");
            Console.WriteLine("Digite ENTER para continuar");
            Console.ReadLine();
        }
    }

    protected override EntidadeBase ObterDadosCadastrais()
    {
        Console.Write("Informe o nome da categoria: ");
        string? nome = Console.ReadLine();

        Console.WriteLine("---------------------------------");
        Console.WriteLine("Selecione uma cor disponível para a categoria");
        Console.WriteLine("---------------------------------");
        Console.WriteLine("1 - Branco (Padrão)");
        Console.WriteLine("2 - Vermelho");
        Console.WriteLine("3 - Verde");
        Console.WriteLine("4 - Azul");
        Console.WriteLine("---------------------------------");
        Console.Write("Informe a cor escolhida: ");
        string? corSelecionada = Console.ReadLine();

        CorCategoria cor;

        switch (corSelecionada)
        {
            case "1":
                cor = CorCategoria.Branco;
                break;

            case "2":
                cor = CorCategoria.Vermelho;
                break;

            case "3":
                cor = CorCategoria.Verde;
                break;

            case "4":
                cor = CorCategoria.Azul;
                break;

            default:
                cor = CorCategoria.Branco;
                break;
        }

        return new Categoria(nome!, cor);
    }

    protected override bool ExisteRegistroComInformacoesExclusivas(EntidadeBase entidade, int? idIgnorado = null)
    {
        Categoria novaCategoria = (Categoria)entidade;

        EntidadeBase[] categorias = repositorioCategoria.SelecionarTodos();

        for (int i = 0; i < categorias.Length; i++)
        {
            Categoria c = (Categoria)categorias[i];

            if (c == null)
                continue;

            if (idIgnorado != c.Id && novaCategoria.Nome == c.Nome)
            {
                Console.WriteLine("---------------------------------");
                Console.WriteLine($"Já existe uma categoria com o nome \"{c.Nome}\"!");
                Console.WriteLine("---------------------------------");

                return true;
            }
        }

        return base.ExisteRegistroComInformacoesExclusivas(entidade);
    }

    protected override bool ExistemDependenciasAtivasDoRegistro(int idRegistro)
    {
        // TODO - Não permitir excluir uma categoria caso tenha produtos vinculados
        return base.ExistemDependenciasAtivasDoRegistro(idRegistro);
    }
}