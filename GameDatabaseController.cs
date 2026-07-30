public class GameDatabaseController
{
    public void Run()
    {
        int option;
        bool continueOperations = true;

        GameDatabaseView view = new GameDatabaseView();

        do
        {
            Console.Clear();
            continueOperations = view.Run();
        } while(continueOperations);
    }
}