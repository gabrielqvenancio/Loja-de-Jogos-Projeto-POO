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

    public bool Purchase(UserAccount account, string gameName)
    {
        if (account is null || string.IsNullOrWhiteSpace(gameName)) return false;

        var normalized = GameInfo.NormalizeName(gameName);
        var game = _store.GetAll().FirstOrDefault(g => g.NormalizedName == normalized);
        if (game is null || game.Quantity <= 0) return false;

        var admin = _accounts.FindByName("Admin");
        if (admin is null) return false;

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

    public bool Sell(UserAccount account, string gameName)
    {
        if (account is null || string.IsNullOrWhiteSpace(gameName)) return false;

        var normalized = GameInfo.NormalizeName(gameName);
        if (!account.Owns(normalized)) return false;

        var game = account.OwnedGames.FirstOrDefault(g => g.NormalizedName == normalized);
        if (game is null) return false;

        if (!account.RemoveGame(normalized)) return false;

        var admin = _accounts.FindByName("Admin");
        if (admin is null || !admin.Debit(game.ReleasePrice))
        {
            account.AddGame(game);
            return false;
        }

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
