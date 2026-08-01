public class TradeController
{
    private readonly TradeView _view;
    private readonly TradeModel _model;
    private readonly AccountStore _accounts;

    public TradeController(GameStore store, AccountStore accounts, TransactionLog log)
    {
        _view = new TradeView();
        _model = new TradeModel(log, store);
        _accounts = accounts;
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
                    UserTrade();
                    break;
                }
                case 2:
                {
                    StoreTrade();
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

    private void UserTrade()
    {
        var data = _view.ReadUserTradeData();
        if (data is null)
        {
            StandardView.ShowMessage("Dados inválidos.");
            return;
        }

        var a = _accounts.FindByName(data.Value.fromUser);
        var b = _accounts.FindByName(data.Value.toUser);
        if (a is null || b is null)
        {
            StandardView.ShowMessage("Cliente não encontrado.");
            return;
        }

        var result = _model.UserTrade(a, b, data.Value.fromGame, data.Value.toGame);
        if (result.Success)
        {
            if (result.PaymentAmount.HasValue)
            {
                StandardView.ShowMessage($"Troca realizada. {result.PayingUserName} precisa pagar R$ {result.PaymentAmount.Value} para completar a troca.");
            }
            else
            {
                StandardView.ShowMessage("Troca realizada.");
            }
        }
        else
        {
            StandardView.ShowMessage("Troca falhou.");
        }
    }

    private void StoreTrade()
    {
        var data = _view.ReadStoreTradeData();
        if (data is null)
        {
            StandardView.ShowMessage("Dados inválidos.");
            return;
        }

        var a = _accounts.FindByName(data.Value.fromUser);
        if (a is null)
        {
            StandardView.ShowMessage("Cliente não encontrado.");
            return;
        }

        var result = _model.StoreTrade(a, data.Value.fromGame, data.Value.toGame);
        if (result.Success)
        {
            if (result.PaymentAmount.HasValue)
            {
                StandardView.ShowMessage($"Troca realizada. {result.PayingUserName} precisa pagar R$ {result.PaymentAmount.Value} para completar a troca.");
            }
            else
            {
                StandardView.ShowMessage("Troca realizada.");
            }
        }
        else
        {
            StandardView.ShowMessage("Troca falhou.");
        }
    }
}
