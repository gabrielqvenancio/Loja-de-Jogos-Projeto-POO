public class MainMenuView
{
    public bool Run()
    {
        Console.WriteLine("1) Acessar banco de dados de jogos");
        Console.WriteLine("2) Acessar sub-sistema de compra e venda");
        Console.WriteLine("3) Acessar sub-sistema de troca");
        Console.WriteLine("-1) Desligar");

        int option = Convert.ToInt32(Console.ReadLine());
        bool continueProgram = true;

        switch(option)
        {
            case 1:
            {
                //new GameDataBaseController().Run();
                break;        
            }
            case 2:
            {
                //new BuySellController().Run();
                break;        
            }
            case 3:
            {
                //new TradeController().Run();
                break;        
            }
            case -1:
            {
                continueProgram = false;
                break;        
            }
        }
    
        Console.WriteLine("Pressione qualquer tecla para continuar.");
        Console.ReadKey();
        Console.Clear();
        return continueProgram;
    }
}