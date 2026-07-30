public class MainMenuController
{
    public void Run()
    {
        int option;
        bool continueProgram;

        MainMenuView view = new MainMenuView();

        do
        {
            Console.Clear();
            continueProgram = view.Run();
        } while(continueProgram);
    }
}