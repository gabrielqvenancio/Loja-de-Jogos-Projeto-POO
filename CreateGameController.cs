public class CreateGameController
{
    public void Run()
    {
        new CreateGameView().Run();
        new CreateGameModel().Run();
    }
}