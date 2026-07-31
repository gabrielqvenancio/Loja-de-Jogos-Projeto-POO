using System.Globalization;
using System.Text;

public class GameInfo
{
    public string Name { get; }
    public DateTime ReleaseDate { get; }
    public int ReleasePrice { get; }
    public string Developer { get; }
    public int Quantity { get; private set; }
    public string NormalizedName => NormalizeName(Name);

    public GameInfo(string name, DateTime releaseDate, int releasePrice, string developer, int quantity = 1)
    {
        Name = name.Trim();
        ReleaseDate = releaseDate;
        ReleasePrice = releasePrice;
        Developer = developer.Trim();
        if (quantity < 1) throw new ArgumentOutOfRangeException(nameof(quantity), "Quantidade deve ser pelo menos 1.");
        Quantity = quantity;
    }

    public bool DecrementQuantity()
    {
        if (Quantity <= 0) return false;
        Quantity--;
        return true;
    }

    public void IncrementQuantity(int amount = 1)
    {
        if (amount <= 0) return;
        Quantity += amount;
    }

    public static string NormalizeName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return string.Empty;
        }

        var normalized = value.ToUpperInvariant();
        var builder = new StringBuilder();

        foreach (var character in normalized)
        {
            var category = CharUnicodeInfo.GetUnicodeCategory(character);
            if (category == UnicodeCategory.UppercaseLetter || category == UnicodeCategory.DecimalDigitNumber)
            {
                builder.Append(character);
            }
        }

        return builder.ToString();
    }

    public bool HasSameName(GameInfo game)
    {
        ArgumentNullException.ThrowIfNull(game);
        return NormalizedName == game.NormalizedName;
    }
}