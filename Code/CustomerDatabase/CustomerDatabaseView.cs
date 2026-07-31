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
