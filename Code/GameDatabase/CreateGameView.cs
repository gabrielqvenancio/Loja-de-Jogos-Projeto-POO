using System.Globalization;

public class CreateGameView
{
    public static bool TryParseReleaseDate(string? input, out DateTime releaseDate)
    {
        releaseDate = default;
        if (string.IsNullOrWhiteSpace(input))
        {
            return false;
        }

        var value = input.Trim();
        var formats = new[]
        {
            "dd/MM/yyyy",
            "d/MM/yyyy",
            "dd/M/yyyy",
            "d/M/yyyy",
            "dd-MM-yyyy",
            "d-MM-yyyy",
            "yyyy-MM-dd"
        };

        return DateTime.TryParseExact(value, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out releaseDate)
            || DateTime.TryParse(value, CultureInfo.GetCultureInfo("pt-BR"), DateTimeStyles.None, out releaseDate)
            || DateTime.TryParse(value, CultureInfo.InvariantCulture, DateTimeStyles.None, out releaseDate);
    }

    public GameInfo? ReadGameData()
    {
        Console.Clear();
        Console.WriteLine("=== Cadastro de jogo ===");
        Console.WriteLine();

        Console.Write("Nome: ");
        var name = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(name))
        {
            Console.WriteLine("Nome inválido.");
            return null;
        }

        Console.Write("Desenvolvedora: ");
        var developer = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(developer))
        {
            Console.WriteLine("Desenvolvedora inválida.");
            return null;
        }

        Console.Write("Preço novo: ");
        if (!int.TryParse(Console.ReadLine(), out var price) || price < 0)
        {
            Console.WriteLine("Preço inválido.");
            return null;
        }

        Console.Write("Quantidade: ");
        if (!int.TryParse(Console.ReadLine(), out var quantity) || quantity < 1)
        {
            Console.WriteLine("Quantidade inválida. Deve ser um número inteiro maior que zero.");
            return null;
        }

        Console.Write("Data de lançamento (dd/MM/yyyy): ");
        var rawDate = Console.ReadLine();
        if (!TryParseReleaseDate(rawDate, out var releaseDate))
        {
            Console.WriteLine("Data inválida. Use o formato dd/MM/yyyy.");
            return null;
        }

        return new GameInfo(name.Trim(), releaseDate, price, developer.Trim(), quantity);
    }

    public void ShowResult(bool success, GameInfo? game)
    {
        Console.WriteLine();
        if (success)
        {
            Console.WriteLine($"Jogo '{game?.Name}' cadastrado com sucesso!");
            return;
        }

        Console.WriteLine("Não foi possível cadastrar. Já existe um jogo com esse nome.");
    }
}