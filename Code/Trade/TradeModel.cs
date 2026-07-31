public class TradeModel
{
    private readonly TransactionLog _log;

    public TradeModel(TransactionLog log)
    {
        _log = log;
    }

    public (bool Success, int? PaymentAmount, string? PayingUserName) Trade(UserAccount from, UserAccount to, string fromGameName, string toGameName)
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
}
