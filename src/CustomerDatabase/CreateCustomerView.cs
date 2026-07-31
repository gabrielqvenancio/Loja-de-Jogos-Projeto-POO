public class CreateCustomerView
{
    public (string? Name, int? InitialBalance)? ReadCustomerData()
    {
        Console.Write("Nome do cliente: ");
        var name = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(name))
        {
            Console.WriteLine("Nome inválido.");
            return null;
        }

        Console.Write("Saldo inicial: ");
        if (!int.TryParse(Console.ReadLine(), out var balance) || balance < 0)
        {
            Console.WriteLine("Saldo inválido.");
            return null;
        }

        return (name.Trim(), balance);
    }
}