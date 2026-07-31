public class TradeView
{
    public void ShowMenu()
    {
        Console.WriteLine();
        Console.WriteLine("=== Troca entre usuários ===");
        Console.WriteLine();
        Console.WriteLine("1) Realizar troca");
        Console.WriteLine("-1) Voltar");
        Console.WriteLine();
    }

    public int ReadOption()
    {
        Console.Write("Escolha uma opção: ");
        var input = Console.ReadLine();
        return int.TryParse(input, out var option) ? option : 0;
    }

    public (string fromUser, string toUser, string fromGame, string toGame)? ReadTradeData()
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

    public void ShowMessage(string msg) => Console.WriteLine(msg);
}
