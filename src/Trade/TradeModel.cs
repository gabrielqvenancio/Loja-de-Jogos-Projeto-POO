public class TradeModel
{
    private readonly TransactionLog _log;
    private readonly GameStore _store;

    public TradeModel(TransactionLog log, GameStore store)
    {
        _log = log;
        _store = store;
    }

    public (bool Success, int? PaymentAmount, string? PayingUserName) UserTrade(UserAccount from, UserAccount to, string fromGameName, string toGameName)
    {
        if (from is null || to is null) return (false, null, null);

        var fromNormalized = GameInfo.NormalizeName(fromGameName);
        var toNormalized = GameInfo.NormalizeName(toGameName);

        if (!from.Owns(fromNormalized) || !to.Owns(toNormalized)) return (false, null, null);

        var fromGame = from.OwnedGames.First(g => g.NormalizedName == fromNormalized);
        var toGame = to.OwnedGames.First(g => g.NormalizedName == toNormalized);

        var difference = Math.Abs(fromGame.ReleasePrice - toGame.ReleasePrice);
        var payingUserName = fromGame.ReleasePrice > toGame.ReleasePrice ? to.Name : from.Name;
        var paymentAmount = difference > 0 ? difference : (int?)null;

        if (!from.RemoveGame(fromNormalized)) return (false, null, null);
        if (!to.RemoveGame(toNormalized))
        {
            from.AddGame(fromGame);
            return (false, null, null);
        }

        from.AddGame(toGame);
        to.AddGame(fromGame);

        _log.Log($"{from.Name} trocou {fromGame.Name} com {to.Name} por {toGame.Name}" + (paymentAmount.HasValue ? $" com pagamento de R$ {paymentAmount.Value} para {payingUserName}" : string.Empty));
        return (true, paymentAmount, payingUserName);
    }

    public (bool Success, int? PaymentAmount, string? PayingUserName) StoreTrade(UserAccount user, string userGameName, string storeGameName)
    {
        if (user is null) return (false, null, null);

        var userNormalized = GameInfo.NormalizeName(userGameName);
        var storeNormalized = GameInfo.NormalizeName(storeGameName);

        if (!user.Owns(userNormalized) || _store.GetAll().FirstOrDefault(existing => existing.NormalizedName == storeNormalized) is null) return (false, null, null);

        var userGame = user.OwnedGames.First(g => g.NormalizedName == userNormalized);
        var storeGame = _store.GetAll().First(g => g.NormalizedName == storeNormalized);

        var difference = Math.Abs(userGame.ReleasePrice - storeGame.ReleasePrice);
        var payingUserName = userGame.ReleasePrice > storeGame.ReleasePrice ? "loja" : user.Name;
        var paymentAmount = difference > 0 ? difference : (int?)null;

        if (!user.RemoveGame(userNormalized)) return (false, null, null);
        if (!_store.Delete(storeNormalized))
        {
            user.AddGame(userGame);
            return (false, null, null);
        }

        user.AddGame(storeGame);
        _store.Add(userGame);

        _log.Log($"{user.Name} trocou {userGame.Name} com a loja por {storeGame.Name}" + (paymentAmount.HasValue ? $" com pagamento de R$ {paymentAmount.Value} para {payingUserName}" : string.Empty));
        return (true, paymentAmount, payingUserName);
    }
}
