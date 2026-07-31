public class CustomerBalanceView()
{
    public int? ReadBalanceToAdd()
    {
        Console.Write("Quantidade de saldo a adicionar: ");
        if (!int.TryParse(Console.ReadLine(), out var balance) || balance < 0)
        {
            Console.WriteLine("Saldo inválido.");
            return null;
        }

        return balance;
    }
}