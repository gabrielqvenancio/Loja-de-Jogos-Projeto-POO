public class CustomerDatabaseController
{
    private readonly CustomerDatabaseView _view;
    private readonly AccountStore _accounts;
    private readonly TransactionLog _log;

    public CustomerDatabaseController(AccountStore accounts, TransactionLog log)
    {
        _view = new CustomerDatabaseView();
        _accounts = accounts;
        _log = log;
    }

    public void Run()
    {
        bool continueOperations = true;

        while (continueOperations)
        {
            Console.Clear();
            _view.ShowMenu();
            var option = StandardView.ReadOption();
            Console.WriteLine();

            switch (option)
            {
                case 1:
                {
                    RegisterCustomer();
                    break;
                }
                case 2:
                    ShowCustomers();
                    break;
                case 3:
                {
                    RemoveCustomer();
                    break;
                }
                case 4:
                {
                    EditCustomer();
                    break;
                }
                case 5:
                {
                    AddBalance();
                    break;
                }
                case -1:
                    continueOperations = false;
                    break;
                default:
                    StandardView.ShowMessage("Opção inválida.");
                    break;
            }

            if (continueOperations)
            {
                Console.WriteLine("\nPressione qualquer tecla para continuar...");
                Console.ReadKey();
            }
        }
    }

    private void RegisterCustomer()
    {
        CreateCustomerModel model = new(_accounts, _log);
        CreateCustomerView view = new();
        
        var data = view.ReadCustomerData();
        if (data is null)
        {
            StandardView.ShowMessage("Dados inválidos.");
            return;
        }

        var success = model.RegisterCustomer(data.Value.Name!, data.Value.InitialBalance ?? 0);
        StandardView.ShowMessage(success ? "Cliente cadastrado com sucesso." : "Não foi possível cadastrar o cliente.");
    }

    private void ShowCustomers()
    {
        _view.ShowCustomers(_accounts.GetAll());
    }

    private void RemoveCustomer()
    {
        RemoveCustomerModel model = new(_accounts, _log);

        var name = StandardView.ReadName("Informe o nome do cliente para remover: ");
        if (string.IsNullOrWhiteSpace(name))
        {
            StandardView.ShowMessage("Nome inválido.");
            return;
        }

        var success = model.RemoveCustomer(name);
        StandardView.ShowMessage(success ? "Cliente removido com sucesso." : "Cliente não encontrado.");
    }

    private void EditCustomer()
    {
        EditCustomerModel model = new(_accounts, _log);

        var name = StandardView.ReadName("Informe o nome do cliente para editar: ");
        if (string.IsNullOrWhiteSpace(name))
        {
            StandardView.ShowMessage("Nome inválido.");
            return;
        }

        Console.Write("Deseja editar o nome desse cliente (s/n): ");
        var editNameOption = Console.ReadLine();
        var shouldEditName = editNameOption?.Trim().Equals("s", StringComparison.OrdinalIgnoreCase) == true;

        string? newName = null;
        if (shouldEditName)
        {
            newName = StandardView.ReadName("Novo nome: ");
            if (string.IsNullOrWhiteSpace(newName))
            {
                StandardView.ShowMessage("Nome inválido.");
                return;
            }
        }

        Console.Write("Deseja editar o saldo? (s/n): ");
        var editBalanceOption = Console.ReadLine();
        var shouldEditBalance = editBalanceOption?.Trim().Equals("s", StringComparison.OrdinalIgnoreCase) == true;

        int? newBalance = null;
        if (shouldEditBalance)
        {
            Console.Write("Informe o novo saldo: ");
            if (!int.TryParse(Console.ReadLine(), out var balance) || balance < 0)
            {
                StandardView.ShowMessage("Saldo inválido.");
                return;
            }

            newBalance = balance;
        }

        var success = model.EditCustomer(name, newName, newBalance);
        StandardView.ShowMessage(success ? "Cliente editado com sucesso." : "Cliente não encontrado.");
    }

    private void AddBalance()
    {
        CustomerBalanceModel model = new(_accounts, _log);
        CustomerBalanceView view = new();

        var name = StandardView.ReadName("Informe o nome do cliente para atribuir saldo: ");
        if (string.IsNullOrWhiteSpace(name))
        {
            StandardView.ShowMessage("Nome inválido.");
            return;
        }

        var balance = view.ReadBalanceToAdd();
        if (balance is null)
        {
            return;
        }

        var success = model.AddBalance(name, balance.Value);
        StandardView.ShowMessage(success ? "Saldo adicionado com sucesso." : "Cliente não encontrado.");
    }
}
