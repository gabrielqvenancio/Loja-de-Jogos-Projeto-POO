public class MainMenuView
{
    public void ShowMenu()
    {
        Console.WriteLine();
        Console.WriteLine("=== Loja de Jogos ===");
        Console.WriteLine();
        Console.WriteLine("1) Acessar banco de dados de jogos");
        Console.WriteLine("2) Acessar banco de dados de clientes");
        Console.WriteLine("3) Acessar sub-sistema de compra e venda");
        Console.WriteLine("4) Acessar sub-sistema de troca");
        Console.WriteLine("-1) Desligar");
        Console.WriteLine();
    }

    public int ReadOption()
    {
        Console.Write("Escolha uma opção: ");
        var input = Console.ReadLine();
        return int.TryParse(input, out var option) ? option : 0;
    }

    public void ShowMessage(string message)
    {
        Console.WriteLine(message);
    }

    public void ShowExitMessage()
    {
        Console.WriteLine("Desligando...");
        Console.WriteLine("Pressione qualquer tecla para fechar.");
    }
}