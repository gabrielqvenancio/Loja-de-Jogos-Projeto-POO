public static class StandardView
{
    public static int ReadOption()
    {
        Console.Write("Escolha uma opção: ");
        var input = Console.ReadLine();
        return int.TryParse(input, out var option) ? option : 0;
    }

    public static string? ReadName(string prompt)
    {
        Console.Write(prompt);
        var name = Console.ReadLine();
        return string.IsNullOrWhiteSpace(name) ? null : name.Trim();
    }

    public static void ShowMessage(string msg) => Console.WriteLine(msg);
}