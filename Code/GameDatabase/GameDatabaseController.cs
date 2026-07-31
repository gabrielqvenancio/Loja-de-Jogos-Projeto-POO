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
            var option = StandardView.ReadOption();
            Console.WriteLine();

            switch (option)
            {
                case 1:
                    CreateGame();
                    break;
                case 2:
                    ShowGames();
                    break;
                case 3:
                    RemoveGame();
                    break;
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

    private void CreateGame()
    {
        CreateGameModel model = new(_store, _log);
        CreateGameView view = new();

        var game = view.ReadGameData();
        if (game is null)
        {
            StandardView.ShowMessage("Operação cancelada.");
            return;
        }

        var success = model.RegisterGame(game);
        view.ShowResult(success, game);
    }

    private void RemoveGame()
    {
        RemoveGameModel model = new(_store);

        var name = StandardView.ReadName("Informe o nome do jogo para remover: ");
        if (string.IsNullOrWhiteSpace(name))
        {
            StandardView.ShowMessage("Nome inválido.");
            return;
        }

        if (model.RemoveGame(name))
        {
            _log.Log($"Jogo removido: {name}");
            StandardView.ShowMessage("Jogo removido com sucesso.");
            return;
        }

        StandardView.ShowMessage("Jogo não encontrado.");
    }

    private void ShowGames()
    {
        _view.ShowGames(_store.GetAll());
    }
}