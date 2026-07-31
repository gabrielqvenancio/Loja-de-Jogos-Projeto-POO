public class GameDatabaseController
{
    private readonly GameStore _store;
    private readonly TransactionLog _log;
    private readonly GameDatabaseView _view;

    public GameDatabaseController(GameStore store, TransactionLog log)
    {
        _store = store;
        _log = log;
        _view = new GameDatabaseView();
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
                    new CreateGameController(_store, _log).Run();
                    break;
                case 2:
                    _view.ShowGames(_store.GetAll());
                    break;
                case 3:
                    RemoveGame();
                    break;
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

    private void RemoveGame()
    {
        var name = _view.ReadNameToRemove();
        if (string.IsNullOrWhiteSpace(name))
        {
            _view.ShowMessage("Nome inválido.");
            return;
        }

        var removed = _store.Delete(name);
        if (removed)
        {
            _log.Log($"Jogo removido: {name}");
            _view.ShowMessage("Jogo removido com sucesso.");
            return;
        }

        _view.ShowMessage("Jogo não encontrado.");
    }
}