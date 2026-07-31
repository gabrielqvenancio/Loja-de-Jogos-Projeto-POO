public class CreateCustomerModel
{
    private readonly AccountStore _accounts;
    private readonly TransactionLog _log;

    public CreateCustomerModel(AccountStore accounts, TransactionLog log)
    {
        _accounts = accounts;
        _log = log;
    }

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
}