namespace CardGame;

public class Card
{
    public int Value { get;set; }
    public int Suit { get;set; }
    public bool Selected { get;set; }
    public bool Played { get;set; }

    //Constructor with given value and suit for the card
    public Card (int ValueIn, int SuitIn)
    {
        Value = ValueIn;
        Suit = SuitIn;
        Selected = false;
        Played = false;
    }
    //Base constructor for no value or suit given
    public Card()
    {
        Value = 0;
        Suit = -1;
        Selected = false;
        Played = false;
    }
    //Prints the card value in the format created
    public virtual void PrintCard(List<String> render)
    {
        render[0] +=  "╔═════╗ ";
        render[1] += $"║{ValueToString()}░░░║ ";
        render[2] += $"║░░{SuitToString()}░░║ ";
        render[3] += $"║░░░{ValueToString()}║ ";
        render[4] +=  "╚═════╝ ";
    }
    //Prints the bottom line depending on if the card is played or selected
    //so that the player has a visual update of their hand
    public virtual void PrintCardState(List<String> render)
    {
        if(Selected == true)
        {
            render[5] += "******* ";
        } else if(Played == true)
        {
            render[5] += "------- ";
        } else
        {
            render[5] += "        ";
        }
    }
    //Takes the value of the card and returns a string
    //This is most useful for keeping track of the face
    //cards that don't typically carry a numerical value
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
    //A method to convert the suit of the card to a string so that it
    //can be printed
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

//A child class of card for the Joker as it is a special case
public class Joker : Card
{
    public Joker()
    {
        Value = 0;
        Suit = -1;
        Played = false;
        Selected = false;
    }
    public override string ValueToString()
    {
        return "Joker";
    }
    //Seperate print card method to format joker different from the other cards
    public override void PrintCard(List<String> render)
    {
        render[0] +=  "╔═════╗ ";
        render[1] += $"║{ValueToString()}║ ";
        render[2] += $"║░░{SuitToString()}░░║ ";
        render[3] += $"║{ValueToString()}║ ";
        render[4] +=  "╚═════╝ ";
    }
    public override string SuitToString()
    {
        return "$";
    }
}
