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

    public bool ShowTransactionDetails(UserAccount account, GameInfo game, string transactionType)
    {
        while(true)
        {
            Console.Clear();
            Console.WriteLine($"O usuário {account.Name} {transactionType}á o jogo {game.Name} por R${game.ReleasePrice}.");
            Console.WriteLine();
            Console.WriteLine("Digite: 1 - Confirmar operação, 2 - Encerrar operação");
            
            int option = StandardView.ReadOption();
            switch(option)
            {
                case 1:
                    Console.WriteLine("Começando operação...");
                    return true;
                case 2:
                    Console.WriteLine("Encerrando operação...");
                    return false;
                default:
                    Console.WriteLine("Opção inválida.");
                    break;
            }

            Console.WriteLine("Pressione qualquer tecla para continuar...");
            Console.ReadKey();
        }
    }
}
