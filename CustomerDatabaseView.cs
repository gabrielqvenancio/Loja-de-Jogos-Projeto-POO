public class CustomerDatabaseView
{
    public void ShowMenu()
    {
        Console.WriteLine();
        Console.WriteLine("=== Banco de clientes ===");
        Console.WriteLine();
        Console.WriteLine("1) Cadastrar cliente");
        Console.WriteLine("2) Listar clientes");
        Console.WriteLine("3) Remover cliente");
        Console.WriteLine("4) Adicionar saldo");
        Console.WriteLine("-1) Retornar ao menu");
        Console.WriteLine();
    }

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

    public string? ReadCustomerName(string prompt)
    {
        Console.Write(prompt);
        var name = Console.ReadLine();
        return string.IsNullOrWhiteSpace(name) ? null : name.Trim();
    }

    public int? ReadBalance()
    {
        Console.Write("Quantidade de saldo a adicionar: ");
        if (!int.TryParse(Console.ReadLine(), out var balance) || balance < 0)
        {
            Console.WriteLine("Saldo inválido.");
            return null;
        }

        return balance;
    }

    public void ShowCustomers(IReadOnlyList<UserAccount> customers)
    {
        Console.WriteLine();
        Console.WriteLine("=== Clientes cadastrados ===");
        Console.WriteLine();

        if (customers.Count == 0)
        {
            Console.WriteLine("Nenhum cliente cadastrado.");
            return;
        }

        foreach (var customer in customers)
        {
            Console.WriteLine($"- {customer.Name} | Saldo: R$ {customer.Balance}");
        }
    }
}
