public class BuySellModel
{
    private readonly GameStore _store;
    private readonly TransactionLog _log;
    private readonly AccountStore _accounts;

    public BuySellModel(GameStore store, TransactionLog log, AccountStore accounts)
    {
        _store = store;
        _log = log;
        _accounts = accounts;
    }

    public GameInfo? GetGameInfo(IReadOnlyList<GameInfo> gameCollection, string gameName)
    {
        if(string.IsNullOrWhiteSpace(gameName)) return null;
        var normalized = GameInfo.NormalizeName(gameName);
        return gameCollection.FirstOrDefault(g => g.NormalizedName == normalized);
    }

    public bool Purchase(UserAccount account, GameInfo game)
    {
        var admin = _accounts.FindByName("Admin");
        if (admin is null) return false;

        if (account is null || account == admin || game.Quantity <= 0) return false;

        if (!account.Debit(game.ReleasePrice)) return false;

        if (!_store.DecrementStock(game.Name))
        {
            account.Credit(game.ReleasePrice);
            return false;
        }

        var purchasedGame = new GameInfo(game.Name, game.ReleaseDate, game.ReleasePrice, game.Developer, 1);
        if (!account.AddGame(purchasedGame))
        {
            account.Credit(game.ReleasePrice);
            _store.IncreaseStock(purchasedGame);
            return false;
        }

        admin.Credit(game.ReleasePrice);
        _log.Log($"{account.Name} comprou {game.Name} por R$ {game.ReleasePrice}");
        return true;
    }

    public bool Sell(UserAccount account, GameInfo game)
    {
        if (account is null) return false;

        var admin = _accounts.FindByName("Admin");
        if (admin is null || !admin.Debit(game.ReleasePrice)) return false;

        if (!account.RemoveGame(game.NormalizedName)) return false;
        account.Credit(game.ReleasePrice);
        var stockGame = new GameInfo(game.Name, game.ReleaseDate, game.ReleasePrice, game.Developer, 1);
        if (!_store.IncreaseStock(stockGame))
        {
            admin.Credit(game.ReleasePrice);
            account.Debit(game.ReleasePrice);
            account.AddGame(game);
            return false;
        }

        _log.Log($"{account.Name} vendeu {game.Name} por R$ {game.ReleasePrice} para o Admin");
        return true;
    }
}
