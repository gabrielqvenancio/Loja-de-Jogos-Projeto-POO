public class GameStore
{
    private readonly List<GameInfo> _games = new();

    public IReadOnlyList<GameInfo> GetAll() => _games;

    public bool Add(GameInfo game)
    {
        ArgumentNullException.ThrowIfNull(game);

        if (_games.Any(existingGame => existingGame.HasSameName(game)))
        {
            return false;
        }

        _games.Add(game);
        return true;
    }

    public bool IncreaseStock(GameInfo game)
    {
        ArgumentNullException.ThrowIfNull(game);

        var existingGame = _games.FirstOrDefault(existing => existing.HasSameName(game));
        if (existingGame != null)
        {
            existingGame.IncrementQuantity(game.Quantity);
            return true;
        }

        _games.Add(game);
        return true;
    }

    public bool DecrementStock(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return false;
        }

        var normalizedName = GameInfo.NormalizeName(name);
        var game = _games.FirstOrDefault(existing => existing.NormalizedName == normalizedName);
        if (game == null || game.Quantity <= 0)
        {
            return false;
        }

        if (game.Quantity == 1)
        {
            _games.Remove(game);
            return true;
        }

        game.DecrementQuantity();
        return true;
    }

    public bool Delete(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return false;
        }

        var normalizedName = GameInfo.NormalizeName(name);
        var gameToRemove = _games.FirstOrDefault(game => game.NormalizedName == normalizedName);

        if (gameToRemove is null)
        {
            return false;
        }

        _games.Remove(gameToRemove);
        return true;
    }
}
