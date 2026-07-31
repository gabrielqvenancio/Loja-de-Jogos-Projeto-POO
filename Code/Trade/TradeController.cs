public class TradeController
{
    private readonly TradeView _view;
    private readonly TradeModel _model;
    private readonly AccountStore _accounts;

    public TradeController(AccountStore accounts, TransactionLog log)
    {
        _view = new TradeView();
        _model = new TradeModel(log);
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
                    Trade();
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

    private void Trade()
    {
        var data = _view.ReadTradeData();
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

        var result = _model.Trade(a, b, data.Value.fromGame, data.Value.toGame);
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
