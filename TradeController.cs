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
        bool loop = true;
        while (loop)
        {
            Console.Clear();
            _view.ShowMenu();
            var opt = _view.ReadOption();
            Console.WriteLine();
            switch (opt)
            {
                case 1:
                {
                    var data = _view.ReadTradeData();
                    if (data == null) { _view.ShowMessage("Dados inválidos."); break; }

                    var a = _accounts.FindByName(data.Value.fromUser);
                    var b = _accounts.FindByName(data.Value.toUser);
                    if (a == null || b == null) { _view.ShowMessage("Cliente não encontrado."); break; }

                    var result = _model.Trade(a, b, data.Value.fromGame, data.Value.toGame);
                    if (result.Success)
                    {
                        if (result.PaymentAmount.HasValue)
                        {
                            _view.ShowMessage($"Troca realizada. {result.PayingUserName} precisa pagar R$ {result.PaymentAmount.Value} para completar a troca.");
                        }
                        else
                        {
                            _view.ShowMessage("Troca realizada.");
                        }
                    }
                    else
                    {
                        _view.ShowMessage("Troca falhou.");
                    }
                    break;
                }
                case -1:
                    loop = false; break;
                default:
                    _view.ShowMessage("Opção inválida."); break;
            }

            if (loop) { Console.WriteLine("\nPressione qualquer tecla para continuar..."); Console.ReadKey(); }
        }
    }
}
