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
                case 4:
                    EditGame();
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

    private void EditGame()
    {
        var name = StandardView.ReadName("Informe o nome do jogo a ser editado: ");
        if (string.IsNullOrWhiteSpace(name))
        {
            StandardView.ShowMessage("Nome inválido.");
            return;
        }

        var game = _store.FindByName(name);
        if (game is null)
        {
            StandardView.ShowMessage("Jogo não encontrado.");
            return;
        }

        string? newName = null;
        Console.Write("Deseja alterar o nome desse jogo (s/n): ");
        var editNameOption = Console.ReadLine();
        if (editNameOption?.Trim().Equals("s", StringComparison.OrdinalIgnoreCase) == true)
        {
            newName = StandardView.ReadName("Novo nome: ");
            if (string.IsNullOrWhiteSpace(newName))
            {
                StandardView.ShowMessage("Nome inválido.");
                return;
            }
        }

        string? newDeveloper = null;
        Console.Write("Deseja alterar a desenvolvedora desse jogo (s/n): ");
        var editDeveloperOption = Console.ReadLine();
        if (editDeveloperOption?.Trim().Equals("s", StringComparison.OrdinalIgnoreCase) == true)
        {
            newDeveloper = StandardView.ReadName("Nova desenvolvedora: ");
            if (string.IsNullOrWhiteSpace(newDeveloper))
            {
                StandardView.ShowMessage("Desenvolvedora inválida.");
                return;
            }
        }

        int? newPrice = null;
        Console.Write("Deseja alterar o preço desse jogo (s/n): ");
        var editPriceOption = Console.ReadLine();
        if (editPriceOption?.Trim().Equals("s", StringComparison.OrdinalIgnoreCase) == true)
        {
            Console.Write("Novo preço: ");
            if (!int.TryParse(Console.ReadLine(), out var price) || price < 0)
            {
                StandardView.ShowMessage("Preço inválido.");
                return;
            }

            newPrice = price;
        }

        int? newQuantity = null;
        Console.Write("Deseja alterar a quantidade desse jogo (s/n): ");
        var editQuantityOption = Console.ReadLine();
        if (editQuantityOption?.Trim().Equals("s", StringComparison.OrdinalIgnoreCase) == true)
        {
            Console.Write("Nova quantidade: ");
            if (!int.TryParse(Console.ReadLine(), out var quantity) || quantity < 1)
            {
                StandardView.ShowMessage("Quantidade inválida.");
                return;
            }

            newQuantity = quantity;
        }

        DateTime? newReleaseDate = null;
        Console.Write("Deseja alterar a data de lançamento desse jogo (s/n): ");
        var editDateOption = Console.ReadLine();
        if (editDateOption?.Trim().Equals("s", StringComparison.OrdinalIgnoreCase) == true)
        {
            Console.Write("Nova data de lançamento (dd/MM/yyyy): ");
            if (!DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out var releaseDate))
            {
                StandardView.ShowMessage("Data inválida.");
                return;
            }

            newReleaseDate = releaseDate;
        }

        var updated = _store.UpdateGame(name, newName, newDeveloper, newPrice, newQuantity, newReleaseDate);
        if (updated)
        {
            _log.Log($"Jogo editado: {name}");
            StandardView.ShowMessage("Jogo editado com sucesso.");
            return;
        }

        StandardView.ShowMessage("Não foi possível editar o jogo.");
    }
}