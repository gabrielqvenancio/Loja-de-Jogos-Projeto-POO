public class TransactionLog
{
    private readonly List<string> _entries = new();

    public void Log(string message)
    {
        if (string.IsNullOrWhiteSpace(message))
        {
            return;
        }

        _entries.Add($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}");
    }

    public IReadOnlyList<string> GetEntries() => _entries;

    public void Clear()
    {
        _entries.Clear();
    }
}