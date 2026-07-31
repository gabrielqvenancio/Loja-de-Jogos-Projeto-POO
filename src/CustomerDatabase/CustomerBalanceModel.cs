public class CustomerBalanceModel
{
    private readonly AccountStore _accounts;
    private readonly TransactionLog _log;

    public CustomerBalanceModel(AccountStore accounts, TransactionLog log)
    {
        _accounts = accounts;
        _log = log;
    }

    public bool AddBalance(string name, int amount)
    {
        if (string.IsNullOrWhiteSpace(name) || amount < 0)
        {
            return false;
        }

        var account = _accounts.FindByName(name);
        if (account is null)
        {
            return false;
        }

        account.Credit(amount);
        _log.Log($"Saldo adicionado para {account.Name}: +R$ {amount} (total R$ {account.Balance})");
        return true;
    }
}
