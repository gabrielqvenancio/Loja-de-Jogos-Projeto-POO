using System.Collections.Generic;

public class UserAccount
{
    public string Name { get; }
    public int Balance { get; private set; }
    private readonly List<GameInfo> _ownedGames = new();

    public UserAccount(string name, int initialBalance = 0)
    {
        Name = name;
        Balance = initialBalance;
    }

    public IReadOnlyList<GameInfo> OwnedGames => _ownedGames;

    public bool Owns(string normalizedName) => _ownedGames.Any(g => g.NormalizedName == normalizedName);

    public bool AddGame(GameInfo game)
    {
        if (game == null) return false;
        if (Owns(game.NormalizedName)) return false;
        _ownedGames.Add(game);
        return true;
    }

    public bool RemoveGame(string normalizedName)
    {
        var g = _ownedGames.FirstOrDefault(x => x.NormalizedName == normalizedName);
        if (g == null) return false;
        _ownedGames.Remove(g);
        return true;
    }

    public bool Debit(int amount)
    {
        if (amount < 0) return false;
        if (Balance < amount) return false;
        Balance -= amount;
        return true;
    }

    public bool SetBalance(int amount)
    {
        if (amount < 0) return false;
        Balance = amount;
        return true;
    }

    public void Credit(int amount)
    {
        if (amount <= 0) return;
        Balance += amount;
    }
}
