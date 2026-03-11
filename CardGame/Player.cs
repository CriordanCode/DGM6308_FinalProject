namespace CardGame;




public class Player
{
    public int ID { get; }
    public bool Human { get;}
    public int Score { get;set; }

    public List<Card> Deck { get; }
    public List<Card> CurrentHand;

    public Player(int idIn, bool isHuman)
    {
        ID = idIn;
        Human = isHuman;
        Deck = new List<Card>();
        for(int sIter = 0; sIter < 4; sIter++)
        {
            for(int vIter = 2; vIter < 15; vIter++)
            {
                Deck.Add(new Card(vIter, sIter));   
            }
        }
        Deck.Add(new Joker());
        Deck.Add(new Joker());
        Deck.Shuffle();
        CurrentHand = new List<Card>();
    }


    public void Draw()
    {
        CurrentHand.Add(deck[0]);
        Deck.Remove(deck[0]);
    }

}