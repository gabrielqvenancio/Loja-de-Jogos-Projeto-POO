public class Program
{
    public static void Main()
    {
        new MainMenuController(new GameStore(), new AccountStore(), new TransactionLog()).Run();
    }
}