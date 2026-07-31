public class CustomerDatabaseModel
{
    private readonly AccountStore _accounts;
    private readonly TransactionLog _log;

    public CustomerDatabaseModel(AccountStore accounts, TransactionLog log)
    {
        _accounts = accounts;
        _log = log;
    }

    public IReadOnlyList<UserAccount> GetAll() => _accounts.GetAll();

    public bool RegisterCustomer(string name, int initialBalance)
    {
        if (string.IsNullOrWhiteSpace(name) || initialBalance < 0)
        {
            return false;
        }

        var account = new UserAccount(name.Trim(), initialBalance);
        var added = _accounts.Add(account);
        if (added)
        {
            _log.Log($"Cliente cadastrado: {account.Name} (saldo inicial R$ {account.Balance})");
        }

        return added;
    }

    public bool RemoveCustomer(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return false;
        }

        var removed = _accounts.Remove(name.Trim());
        if (removed)
        {
            _log.Log($"Cliente removido: {name.Trim()}");
        }

        return removed;
    }

    public bool AddBalance(string name, int amount)
    {
        if (string.IsNullOrWhiteSpace(name) || amount < 0)
        {
            return false;
        }

        var account = _accounts.FindByName(name);
        if (account == null)
        {
            return false;
        }

        account.Credit(amount);
        _log.Log($"Saldo adicionado para {account.Name}: +R$ {amount} (total R$ {account.Balance})");
        return true;
    }
}
