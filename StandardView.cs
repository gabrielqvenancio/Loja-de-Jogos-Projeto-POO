public static class StandardView
{
    public static int ReadOption()
    {
        Console.Write("Escolha uma opção: ");
        var input = Console.ReadLine();
        return int.TryParse(input, out var option) ? option : 0;
    }

    public static void ShowMessage(string msg) => Console.WriteLine(msg);
}