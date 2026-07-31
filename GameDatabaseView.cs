public class GameDatabaseView
{
    public void ShowMenu()
    {
        Console.WriteLine();
        Console.WriteLine("=== Banco de jogos ===");
        Console.WriteLine();
        Console.WriteLine("1) Registrar jogo");
        Console.WriteLine("2) Listar jogos");
        Console.WriteLine("3) Remover jogo");
        Console.WriteLine("-1) Retornar ao menu");
        Console.WriteLine();
    }

    public int ReadOption()
    {
        Console.Write("Escolha uma opção: ");
        var input = Console.ReadLine();
        return int.TryParse(input, out var option) ? option : 0;
    }

    public void ShowGames(IReadOnlyList<GameInfo> games)
    {
        Console.WriteLine();
        Console.WriteLine("=== Jogos cadastrados ===");
        Console.WriteLine();
        if (games.Count == 0)
        {
            Console.WriteLine("Nenhum jogo cadastrado.");
            return;
        }

        foreach (var game in games)
        {
            Console.WriteLine($"- {game.Name} | {game.Developer} | R$ {game.ReleasePrice} | {game.ReleaseDate:dd/MM/yyyy} | Qtd: {game.Quantity}\n");
        }
    }

    public string? ReadNameToRemove()
    {
        Console.Write("Informe o nome do jogo para remover: ");
        var name = Console.ReadLine();
        return string.IsNullOrWhiteSpace(name) ? null : name.Trim();
    }

    public void ShowMessage(string message)
    {
        Console.WriteLine(message);
    }
}