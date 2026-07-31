public class CreateGameController
{
    private readonly CreateGameView _view;
    private readonly CreateGameModel _model;

    public CreateGameController(GameStore store, TransactionLog log)
    {
        _view = new CreateGameView();
        _model = new CreateGameModel(store, log);
    }

    public void Run()
    {
        var game = _view.ReadGameData();
        if (game is null)
        {
            Console.WriteLine("Operação cancelada.");
            return;
        }

        var success = _model.RegisterGame(game);
        _view.ShowResult(success, game);
    }
}