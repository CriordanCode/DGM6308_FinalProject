namespace CardGame;

public class Card
{
    public int Value { get;set; }
    public int Suit { get;set; }

    public Card (int ValueIn, int SuitIn)
    {
        Value = ValueIn;
        Suit = SuitIn;
    }

    public Card()
    {
        Value = 0;
        Suit = -1;
    }

    public virtual void PrintCard(List<String> render)
    {
        render[0] +=  "╔═════╗   ";
        render[1] += $"║{ValueToString()}░░░║   ";
        render[2] += $"║░░{SuitToString()}░░║   ";
        render[3] += $"║░░░{ValueToString()}║   ";
        render[4] +=  "╚═════╝   ";
    }

    public virtual string RawPrintCard()
    {
        return $"""
        ╔═════╗   
        ║{ValueToString()}░░░║   
        ║░░{SuitToString()}░░║   
        ║░░░{ValueToString()}║   
        ╚═════╝   
        """;
    }

    public virtual string ValueToString()
    {
        switch (Value)
        {
            case < 10   : return ("0" + Value);
            case 10     : return "10";
            case 11     : return " J";
            case 12     : return " Q";
            case 13     : return " K";
            case 14     : return " A";
            default     : return "Value Not Supported";
        }
    }

    public virtual string SuitToString()
    {
        switch (Suit)
        {
            case 0  : return "♠";
            case 1  : return "♥";
            case 2  : return "♦";
            case 3  : return "♣";
            default : return "Suit Not Suppported";
        }
    }
}

public class Joker : Card
{

    public Joker()
    {
        Value = 0;
        Suit = -1;
    }

    public override string ValueToString()
    {
        return "Joker";
    }
    public override void PrintCard(List<String> render)
    {
        render[0] +=  "╔═════╗";
        render[1] += $"║{ValueToString()}║";
        render[2] += $"║░░{SuitToString()}░░║";
        render[3] += $"║{ValueToString()}║";
        render[4] +=  "╚═════╝";
    }
    public override string SuitToString()
    {
        return "$";
    }

}
