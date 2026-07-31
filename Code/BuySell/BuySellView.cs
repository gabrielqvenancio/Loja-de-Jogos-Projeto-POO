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
