public class RemoveGameModel
{
    private readonly GameStore _store;
    public RemoveGameModel(GameStore store)
    {
        _store = store;
    }

    public bool RemoveGame(string name)
    {
        return _store.Delete(name);
    }
}