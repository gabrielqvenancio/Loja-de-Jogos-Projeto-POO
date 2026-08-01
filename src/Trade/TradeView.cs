public class TradeView
{
    public void ShowMenu()
    {
        Console.WriteLine();
        Console.WriteLine("=== Troca ===");
        Console.WriteLine();
        Console.WriteLine("1) Realizar troca entre usuários");
        Console.WriteLine("2) Realizar troca com a loja");
        Console.WriteLine("-1) Voltar");
        Console.WriteLine();
    }

    public (string fromUser, string toUser, string fromGame, string toGame)? ReadUserTradeData()
    {
        Console.Write("Nome do usuário A (quem oferece): ");
        var a = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(a)) return null;

        Console.Write("Nome do usuário B (quem recebe): ");
        var b = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(b)) return null;

        Console.Write("Jogo de A para trocar: ");
        var ga = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(ga)) return null;

        Console.Write("Jogo de B para trocar: ");
        var gb = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(gb)) return null;

        return (a.Trim(), b.Trim(), ga.Trim(), gb.Trim());
    }

    public (string fromUser, string fromGame, string toGame)? ReadStoreTradeData()
    {
        Console.Write("Nome do cliente: ");
        var u = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(u)) return null;

        Console.Write("Jogo que o cliente oferece para trocar: ");
        var gu = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(gu)) return null;

        Console.Write("Jogo que a loja oferece para trocar: ");
        var gs = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(gs)) return null;

        return (u.Trim(), gu.Trim(), gs.Trim());
    }
}
