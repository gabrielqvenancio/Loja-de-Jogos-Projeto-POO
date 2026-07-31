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
        if (game is null || game.Quantity <= 0)
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

    public GameInfo? FindByName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return null;
        }

        var normalizedName = GameInfo.NormalizeName(name);
        return _games.FirstOrDefault(game => game.NormalizedName == normalizedName);
    }

    public bool UpdateGame(string currentName, string? newName, string? newDeveloper, int? newPrice, int? newQuantity, DateTime? newReleaseDate)
    {
        var game = FindByName(currentName);
        if (game is null)
        {
            return false;
        }

        if (!string.IsNullOrWhiteSpace(newName))
        {
            var existingGame = _games.FirstOrDefault(existing => existing.HasSameName(new GameInfo(newName, DateTime.Now, 0, string.Empty)));
            if (existingGame is not null && existingGame != game)
            {
                return false;
            }
        }

        if (!string.IsNullOrWhiteSpace(newName))
        {
            var updatedGame = new GameInfo(newName.Trim(), game.ReleaseDate, game.ReleasePrice, game.Developer, game.Quantity);
            _games.Remove(game);
            _games.Add(updatedGame);
            game = updatedGame;
        }

        if (!string.IsNullOrWhiteSpace(newDeveloper))
        {
            var updatedGame = new GameInfo(game.Name, game.ReleaseDate, game.ReleasePrice, newDeveloper.Trim(), game.Quantity);
            _games.Remove(game);
            _games.Add(updatedGame);
            game = updatedGame;
        }

        if (newPrice.HasValue)
        {
            var updatedGame = new GameInfo(game.Name, game.ReleaseDate, newPrice.Value, game.Developer, game.Quantity);
            _games.Remove(game);
            _games.Add(updatedGame);
            game = updatedGame;
        }

        if (newQuantity.HasValue)
        {
            var updatedGame = new GameInfo(game.Name, game.ReleaseDate, game.ReleasePrice, game.Developer, newQuantity.Value);
            _games.Remove(game);
            _games.Add(updatedGame);
            game = updatedGame;
        }

        if (newReleaseDate.HasValue)
        {
            var updatedGame = new GameInfo(game.Name, newReleaseDate.Value, game.ReleasePrice, game.Developer, game.Quantity);
            _games.Remove(game);
            _games.Add(updatedGame);
        }

        return true;
    }
}
