public class CustomerDatabaseController
{
    private readonly CustomerDatabaseView _view;
    private readonly CustomerDatabaseModel _model;

    public CustomerDatabaseController(AccountStore accounts, TransactionLog log)
    {
        _view = new CustomerDatabaseView();
        _model = new CustomerDatabaseModel(accounts, log);
    }

    public void Run()
    {
        bool continueOperations = true;

        while (continueOperations)
        {
            Console.Clear();
            _view.ShowMenu();
            var option = _view.ReadOption();
            Console.WriteLine();

            switch (option)
            {
                case 1:
                {
                    var data = _view.ReadCustomerData();
                    if (data == null)
                    {
                        _view.ShowMessage("Dados inválidos.");
                        break;
                    }

                    var success = _model.RegisterCustomer(data.Value.Name!, data.Value.InitialBalance ?? 0);
                    _view.ShowMessage(success ? "Cliente cadastrado com sucesso." : "Não foi possível cadastrar o cliente.");
                    break;
                }
                case 2:
                    _view.ShowCustomers(_model.GetAll().ToList());
                    break;
                case 3:
                {
                    var name = _view.ReadCustomerName("Informe o nome do cliente para remover: ");
                    if (string.IsNullOrWhiteSpace(name))
                    {
                        _view.ShowMessage("Nome inválido.");
                        break;
                    }

                    var success = _model.RemoveCustomer(name);
                    _view.ShowMessage(success ? "Cliente removido com sucesso." : "Cliente não encontrado.");
                    break;
                }
                case 4:
                {
                    var name = _view.ReadCustomerName("Informe o nome do cliente para atribuir saldo: ");
                    if (string.IsNullOrWhiteSpace(name))
                    {
                        _view.ShowMessage("Nome inválido.");
                        break;
                    }

                    var balance = _view.ReadBalance();
                    if (balance == null)
                    {
                        break;
                    }

                    var success = _model.AddBalance(name, balance.Value);
                    _view.ShowMessage(success ? "Saldo adicionado com sucesso." : "Cliente não encontrado.");
                    break;
                }
                case -1:
                    continueOperations = false;
                    break;
                default:
                    _view.ShowMessage("Opção inválida.");
                    break;
            }

            if (continueOperations && option != -1)
            {
                Console.WriteLine("\nPressione qualquer tecla para continuar...");
                Console.ReadKey();
            }
        }
    }
}
