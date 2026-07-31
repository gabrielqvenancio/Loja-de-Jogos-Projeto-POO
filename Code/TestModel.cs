public class TestModel
{
    public void Run(GameStore store, AccountStore accounts)
    {
        CreateTestData(store, accounts);
    }

    private void CreateTestData(GameStore store, AccountStore accounts)
    {
        var admin = new UserAccount("Admin", 1000);
        var player = new UserAccount("Player1", 200);
        var secondPlayer = new UserAccount("Player2", 150);
        accounts.Add(admin);
        accounts.Add(player);
        accounts.Add(secondPlayer);

        var sampleGame = new GameInfo("The Witcher 3", new DateTime(2015, 5, 19), 80, "CD Projekt Red");
        store.Add(sampleGame);
    }
}