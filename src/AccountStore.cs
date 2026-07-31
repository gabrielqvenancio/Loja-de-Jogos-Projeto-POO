using System.Collections.Generic;

public class AccountStore
{
    private readonly List<UserAccount> _accounts = new();

    public IReadOnlyList<UserAccount> GetAll() => _accounts;

    public bool Add(UserAccount account)
    {
        if (account is null) return false;
        if (_accounts.Any(a => a.Name.Equals(account.Name, System.StringComparison.OrdinalIgnoreCase))) return false;
        _accounts.Add(account);
        return true;
    }

    public bool Remove(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) return false;

        var account = FindByName(name);
        if (account is null) return false;

        return _accounts.Remove(account);
    }

    public UserAccount? FindByName(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) return null;
        return _accounts.FirstOrDefault(a => a.Name.Equals(name, System.StringComparison.OrdinalIgnoreCase));
    }
}
