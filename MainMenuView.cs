public class MainMenuView
{
    public bool Run()
    {
        Console.WriteLine("1) Acessar banco de dados de jogos");
        Console.WriteLine("2) Acessar sub-sistema de compra e venda");
        Console.WriteLine("3) Acessar sub-sistema de troca");
        Console.WriteLine("-1) Desligar");

        int option = Convert.ToInt32(Console.ReadLine());

        switch(option)
        {
            case 1:
            {
                new GameDatabaseController().Run();
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
                Console.WriteLine("Desligando...");
                Console.WriteLine("Pressione qualquer tecla para fechar.");
                Console.ReadKey();    
                return false;
            }
        }
        return true;
    }
}