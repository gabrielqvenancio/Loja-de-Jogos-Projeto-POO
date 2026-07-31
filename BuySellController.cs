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

        bool loop = true;
        while (loop)
        {
            Console.Clear();
            _view.ShowAccount(account);
            _view.ShowMenu();
            Console.WriteLine();
            var opt = StandardView.ReadOption();

            switch (opt)
            {
                case 1:
                {
                    var name = _view.ReadGameName("Nome do jogo para comprar: ");
                    if (name is null) { StandardView.ShowMessage("Nome inválido."); break; }
                    var ok = _model.Purchase(account, name);
                    StandardView.ShowMessage(ok ? "Compra realizada." : "Compra falhou.");
                    break;
                }
                case 2:
                {
                    var name = _view.ReadGameName("Nome do jogo para vender: ");
                    if (name is null) { StandardView.ShowMessage("Nome inválido."); break; }
                    var ok = _model.Sell(account, name);
                    StandardView.ShowMessage(ok ? "Venda realizada." : "Venda falhou.");
                    break;
                }
                case -1:
                    loop = false; break;
                default:
                    StandardView.ShowMessage("Opção inválida."); break;
            }

            if (loop) { Console.WriteLine("\nPressione qualquer tecla para continuar..."); Console.ReadKey(); }
        }
    }

    private UserAccount? SelectAccount()
    {
        var requestedName = _view.ReadAccountName();
        if (string.IsNullOrWhiteSpace(requestedName))
        {
            return _accounts.GetAll().FirstOrDefault();
        }

        return _accounts.FindByName(requestedName);
    }
}
