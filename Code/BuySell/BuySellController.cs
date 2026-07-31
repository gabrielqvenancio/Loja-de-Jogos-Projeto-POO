public class BuySellController
{
    private readonly BuySellView _view;
    private readonly BuySellModel _model;
    private readonly AccountStore _accounts;

    public BuySellController(GameStore store, AccountStore accounts, TransactionLog log)
    {
        _view = new BuySellView();
        _model = new BuySellModel(store, log, accounts);
        _accounts = accounts;
    }

    public void Run()
    {
        if (_accounts.GetAll().Count == 0)
        {
            StandardView.ShowMessage("Nenhuma conta disponível.");
            return;
        }

        var account = SelectAccount();
        if (account is null)
        {
            StandardView.ShowMessage("Cliente inválido.");
            return;
        }

        bool continueOperations = true;
        while (continueOperations)
        {
            Console.Clear();
            _view.ShowAccount(account);
            _view.ShowMenu();
            Console.WriteLine();

            var option = StandardView.ReadOption();

            switch (option)
            {
                case 1:
                {
                    BuyGame(account);
                    break;
                }
                case 2:
                {
                    SellGame(account);
                    break;
                }
                case -1:
                    continueOperations = false;
                    break;
                default:
                    StandardView.ShowMessage("Opção inválida.");
                    break;
            }

            if (continueOperations) { Console.WriteLine("\nPressione qualquer tecla para continuar..."); Console.ReadKey(); }
        }
    }

    private void BuyGame(UserAccount account)
    {
        var name = StandardView.ReadName("Nome do jogo para comprar: ");
        if (name is null)
        { 
            StandardView.ShowMessage("Nome inválido.");
            return;
        }
        StandardView.ShowMessage(_model.Purchase(account, name) ? "Compra realizada." : "Compra falhou.");
    }

    private void SellGame(UserAccount account)
    {
        var name = StandardView.ReadName("Nome do jogo para vender: ");
        if (name is null)
        {
            StandardView.ShowMessage("Nome inválido.");
            return;
        }
        StandardView.ShowMessage(_model.Sell(account, name) ? "Venda realizada." : "Venda falhou.");
    }

    private UserAccount? SelectAccount()
    {
        var requestedName = StandardView.ReadName("Nome do cliente para operar (deixe vazio para usar o cliente padrão): ");

        if (string.IsNullOrWhiteSpace(requestedName))
        {
            return _accounts.GetAll().FirstOrDefault();
        }

        return _accounts.FindByName(requestedName);
    }
}
