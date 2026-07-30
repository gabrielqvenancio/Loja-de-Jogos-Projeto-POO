public class GameDatabaseView
{
    public bool Run()
    {
        Console.WriteLine("1) Registrar jogo");
        Console.WriteLine("2) Atualizar jogo");
        Console.WriteLine("3) Resgatar jogo");
        Console.WriteLine("4) Apagar jogo");
        Console.WriteLine("-1) Retornar ao menu");

        int option = Convert.ToInt32(Console.ReadLine());

        switch(option)
        {
            case 1:
            {
                new CreateGameController().Run();
                break;        
            }
            case 2:
            {
                //new UpdateGameController().Run();
                break;        
            }
            case 3:
            {
                //new ReadGameController().Run();
                break;        
            }
            case 4:
            {
                //new DeleteGameController().Run();
                break;        
            }
            case -1:
            {
                return false;    
            }
        }  
        return true;
    }
}