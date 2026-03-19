namespace CardGame;




public class Player
{
    public int ID { get; }
    public bool Human { get;}
    public int Score { get;set; }

    public bool QueenPlayed{ get;set; }
    public bool KingPlayed{ get;set; }
    public bool JokerPlayed{ get;set; }
    

    public List<Card> Deck { get;set; }
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
        ShuffleSimple();
        CurrentHand = new List<Card>();
        CurrentRound = new List<Card>();
        Discard = new List<Card>();
        QueenPlayed = false;
        KingPlayed = false;
        JokerPlayed = false;
    }

    //Method to shuffle the card deck since it has to convert
    //to an array to shuffle
    public void ShuffleSimple()
    {
        Card[] temp = Deck.ToArray();
        Random.Shared.Shuffle(temp);
        Deck = temp.ToList();
    }

    //Method to handle drawing from the deck - if the deck is empty
    //it moves the discard back into the deck and shuffles again before
    //dealing a new card
    public void Draw()
    {
        if(Deck.Count > 0){
            CurrentHand.Add(Deck[0]);
            Deck.RemoveAt(0);
        } else
        {
            while(Discard.Count > 0)
            {
                Deck.Add(Discard[0]);
                Discard.RemoveAt(0);
            }
            ShuffleSimple();
            CurrentHand.Add(Deck[0]);
            Deck.RemoveAt(0);
        }
    }

    //Reset the player stats for the round which include
    //if special cards were played and moving the cards played
    //that round to the discard list
    public void ClearRound()
    {
        while(CurrentRound.Count > 0)
        {
            Discard.Add(CurrentRound[0]);
            CurrentRound.RemoveAt(0);
        }
        KingPlayed = false;
        QueenPlayed = false;
        JokerPlayed = false;
    }

    //If the player selects a card to play it adds it into the
    //round list and if there are already 5 cards in the list
    //it goes through and removes the first one added
    public void PlayCard(int selection)
    {
        if(CurrentRound.Count < 5){
            CurrentRound.Add(CurrentHand[selection]);
            CurrentHand[selection].Played = true;
        //CurrentHand.RemoveAt(selection);
        } else
        {
            foreach(Card cMax in CurrentHand)
            {
                if (cMax.Equals(CurrentRound[0]))
                {
                    cMax.Played = false;
                    break;
                }
            }
            CurrentRound.RemoveAt(0);
            CurrentRound.Add(CurrentHand[selection]);
            CurrentHand[selection].Played = true;
        }

    }

    //If the player wants to remove a card they selected before
    //they complete their turn this method removes it from the round hand
    public void RecallCard(int selection)
    {
        CurrentHand[selection].Played = false;
        CurrentRound.Remove(CurrentHand[selection]);
    }

    //When confirming the turn is over it removes the cards from the current round list
    // so that the hand is empty when the next round starts for scoring and puts them
    //in the discard deck
    public void ConfirmPlay()
    {
        foreach(Card card in CurrentRound)
        {
            CurrentHand.Remove(card);
            Discard.Add(card);
        }
    }
}