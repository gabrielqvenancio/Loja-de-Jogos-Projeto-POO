public class EditCustomerModel
{
    private readonly AccountStore _accounts;
    private readonly TransactionLog _log;

    public EditCustomerModel(AccountStore accounts, TransactionLog log)
    {
        _accounts = accounts;
        _log = log;
    }

    public bool EditCustomer(string currentName, string? newName, int? newBalance)
    {
        if (string.IsNullOrWhiteSpace(currentName))
        {
            return false;
        }

        var account = _accounts.FindByName(currentName);
        if (account is null)
        {
            return false;
        }

        if (!string.IsNullOrWhiteSpace(newName))
        {
            var existingAccountWithSameName = _accounts.FindByName(newName);
            if (existingAccountWithSameName is not null && !existingAccountWithSameName.Name.Equals(account.Name, StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }
        }

        if (!string.IsNullOrWhiteSpace(newName))
        {
            var renamedAccount = new UserAccount(newName.Trim(), account.Balance);
            _accounts.Remove(account.Name);
            _accounts.Add(renamedAccount);
            account = renamedAccount;
        }

        if (newBalance.HasValue)
        {
            if (!account.SetBalance(newBalance.Value))
            {
                return false;
            }
        }

        _log.Log($"Cliente editado: {account.Name}");
        return true;
    }
}
