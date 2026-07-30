public class MainMenuController
{
    public void Run()
    {
        int option;
        bool continueProgram;

        MainMenuView view = new MainMenuView();

        do
        {
            continueProgram = view.Run();
        } while(continueProgram);

        Console.WriteLine("Desligando");
    }
}