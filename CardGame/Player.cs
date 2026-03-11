using System.Linq;

namespace CardGame;




public class Player
{
    public int ID { get; }
    public bool Human { get;}
    public int Score { get;set; }

    public List<Card> Deck { get; }
    public List<Card> CurrentHand;
    public List<Card> CurrentRound;
    public List<Card> Discard;

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
        CurrentRound = new List<Card>();
        Discard = new List<Card>();
    }


    public void Draw()
    {
        CurrentHand.Add(Deck[0]);
        Deck.Remove(Deck[0]);
    }

    public void PlayCard(int selection)
    {
        CurrentRound.Add(CurrentHand[selection]);
        //CurrentHand.RemoveAt(selection);
    }

    public void RecallCard(int selection)
    {
        CurrentRound.Remove(CurrentHand[selection]);
    }

    public void ConfirmPlay()
    {
        foreach(Card card in CurrentRound)
        {
            CurrentHand.Remove(card);
        }
    }
}