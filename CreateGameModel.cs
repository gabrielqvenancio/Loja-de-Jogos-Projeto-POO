public class CreateGameModel
{
    private readonly GameStore _store;
    private readonly TransactionLog _log;

    public CreateGameModel(GameStore store, TransactionLog log)
    {
        _store = store;
        _log = log;
    }

    public bool RegisterGame(GameInfo game)
    {
        if (_store.Add(game))
        {
            _log.Log($"Jogo registrado: {game.Name}");
            return true;
        }

        _log.Log($"Falha ao registrar: {game.Name}");
        return false;
    }
}