namespace CardGame;

public class Card
{
    public int value { get; }
    public int suit { get; }

    public Card (int ValueIn, int SuitIn)
    {
        value = ValueIn;
        suit = SuitIn;
    }

    public void PrintCard(string[] render)
    {
        render[0] +=  "╔═════╗";
        render[1] += $"║{ValueToString()}░░░║";
        render[2] += $"║░░{SuitToString()}░░║";
        render[3] += $"║░░░{ValueToString()}║";
        render[4] +=  "╚═════╝";
    }

    public string ValueToString()
    {
        switch (value)
        {
            case < 10   : return ("0" + value);
            case 10     : return "10";
            case 11     : return " J";
            case 12     : return " Q";
            case 13     : return " K";
            case 14     : return " A";
            default     : return "Value Not Supported";
        }
    }

    public string SuitToString()
    {
        switch (suit)
        {
            case 0  : return "♠";
            case 1  : return "♥";
            case 2  : return "♦";
            case 3  : return "♣";
        }
    }
}

public class Joker : Card
{

    public Joker()
    {
        value = 0;
        suit = -1;
    }

    public string ValueToString()
    {
        return "Joker";
    }
    public void PrintCard(string[] render)
    {
        render[0] +=  "╔═════╗";
        render[1] += $"║{ValueToString()}║";
        render[2] += $"║░░{SuitToString()}░░║";
        render[3] += $"║{ValueToString()}║";
        render[4] +=  "╚═════╝";
    }
    public string SuitToString()
    {
        return "$";
    }

}
