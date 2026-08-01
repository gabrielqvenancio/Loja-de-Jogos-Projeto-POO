public class MainMenuController
{
    private readonly MainMenuView _view;
    private readonly GameStore _store;
    private readonly TransactionLog _log;
    private readonly AccountStore _accounts;

    public MainMenuController(GameStore store, AccountStore accounts, TransactionLog log)
    {
        _view = new MainMenuView();
        _store = store;
        _log = log;
        _accounts = accounts;
    }

    public void Run()
    {
        new TestModel().Run(_store, _accounts);
        
        bool continueProgram = true;

        while (continueProgram)
        {
            Console.Clear();
            _view.ShowMenu();
            var option = StandardView.ReadOption();
            Console.WriteLine();

            switch (option)
            {
                case 1:
                    new GameDatabaseController(_store, _log).Run();
                    break;
                case 2:
                    new CustomerDatabaseController(_accounts, _log).Run();
                    break;
                case 3:
                    new BuySellController(_store, _accounts, _log).Run();
                    break;
                case 4:
                    new TradeController(_store, _accounts, _log).Run();
                    break;
                case -1:
                    continueProgram = false;
                    _view.ShowExitMessage();
                    break;
                default:
                    StandardView.ShowMessage("Opção inválida.");
                    break;
            }

            if (continueProgram)
            {
                StandardView.ShowMessage("\nPressione qualquer tecla para continuar...");
                Console.ReadKey();
            }
        }
    }
}