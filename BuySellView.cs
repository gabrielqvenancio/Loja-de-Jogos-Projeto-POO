public class BuySellView
{
    public void ShowMenu()
    {
        Console.WriteLine();
        Console.WriteLine("=== Compra e Venda ===");
        Console.WriteLine();
        Console.WriteLine("1) Comprar jogo");
        Console.WriteLine("2) Vender jogo");
        Console.WriteLine("-1) Voltar");
        Console.WriteLine();
    }

    public int ReadOption()
    {
        Console.Write("Escolha uma opção: ");
        var input = Console.ReadLine();
        return int.TryParse(input, out var option) ? option : 0;
    }

    public string? ReadGameName(string prompt)
    {
        Console.Write(prompt);
        var name = Console.ReadLine();
        return string.IsNullOrWhiteSpace(name) ? null : name.Trim();
    }

    public string? ReadAccountName()
    {
        Console.Write("Nome do cliente para operar (deixe vazio para usar o cliente padrão): ");
        var name = Console.ReadLine();
        return string.IsNullOrWhiteSpace(name) ? null : name.Trim();
    }

    public void ShowMessage(string message) => Console.WriteLine(message);

    public void ShowAccount(UserAccount account)
    {
        Console.WriteLine();
        Console.WriteLine($"Cliente: {account.Name} | Saldo: R$ {account.Balance}");
        Console.WriteLine();
        Console.WriteLine("Jogos:");
        if (account.OwnedGames.Count == 0) Console.WriteLine("  (nenhum)");
        foreach (var g in account.OwnedGames)
            Console.WriteLine($" - {g.Name} | R$ {g.ReleasePrice}");
    }
}
