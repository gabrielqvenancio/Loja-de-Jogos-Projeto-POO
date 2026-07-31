public class RemoveGameView
{
    public string? ReadNameToRemove()
    {
        Console.Write("Informe o nome do jogo para remover: ");
        var name = Console.ReadLine();
        return string.IsNullOrWhiteSpace(name) ? null : name.Trim();
    }
}