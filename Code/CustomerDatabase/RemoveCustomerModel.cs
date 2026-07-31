public class RemoveCustomerModel
{
    private readonly AccountStore _accounts;
    private readonly TransactionLog _log;

    public RemoveCustomerModel(AccountStore accounts, TransactionLog log)
    {
        _accounts = accounts;
        _log = log;
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
}