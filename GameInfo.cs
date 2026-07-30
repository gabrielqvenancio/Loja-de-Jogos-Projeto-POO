using System.Globalization;

public class GameInfo
{
    public string Name {get;}
    public DateTime ReleaseDate {get;}
    public int ReleasePrice {get;}
    public string Developer {get;}

    public GameInfo(string name, DateTime releaseDate, int releasePrice, string developer)
    {
        Name = name;
        ReleaseDate = releaseDate;
        ReleasePrice = releasePrice;
        Developer = developer;
    }

    public bool HasSameName (GameInfo game)
    {
        string name1 = Name.ToUpper(), nameCondensed1 = "";
        string name2 = game.Name.ToUpper(), nameCondensed2 = "";

        for(int i = 0; i < name1.Length; i++)
        {
            UnicodeCategory charType = CharUnicodeInfo.GetUnicodeCategory(name1[i]);
            if(charType == UnicodeCategory.UppercaseLetter || charType == UnicodeCategory.DecimalDigitNumber)
            {
                nameCondensed1 = String.Concat(nameCondensed1, name1[i].ToString());
            }
        }

        for(int i = 0; i < name2.Length; i++)
        {
            UnicodeCategory charType = CharUnicodeInfo.GetUnicodeCategory(name2[i]);
            if(charType == UnicodeCategory.UppercaseLetter || charType == UnicodeCategory.DecimalDigitNumber)
            {
                nameCondensed2 = String.Concat(nameCondensed2, name2[i].ToString());
            }
        }

        Console.WriteLine(nameCondensed1);Console.WriteLine(nameCondensed2);
        return String.Compare(nameCondensed1, nameCondensed2) == 0;
    }
}