namespace CardGame;






public class Game
{
    public Player PlayerOne { get; }
    public Player PlayerTwo { get; }

    public int SuitRound { get;set; }

    public int Winner { get; set; }

    private bool OneQueenPlayed { get; set; }
    private bool OneKingPlayed { get; set; }
    private bool TwoQueenPlayed { get; set; }
    private bool TwoKingPlayed { get; set; }
    




    public Game(int humans)
    {
        if(humans == 2)
        {
            PlayerOne = new Player( 1, true);
            PlayerTwo = new Player( 2, true);
        } else if(humans == 1)
        {
            PlayerOne = new Player( 1, true);
            PlayerTwo = new Player( 2, false);
        } else
        {
            PlayerOne = new Player( 1, false);
            PlayerTwo = new Player( 2, false);
        }
        SuitRound = 0;
        Winner = 0;
        for(int i = 0; i < 10; i++)
        {
            PlayerOne.Draw();
            PlayerTwo.Draw();
        }
    }

    public void PickNewSuit()
    {
        SuitRound = Random.Shared.Next(0,4);
    }


    public void PlayTurn(Player currentPlayer)
    {
        int playerSelection = 0;
        bool turnOver = false;
        
        if(currentPlayer.Human == true){
            while (!turnOver)
            {
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.A : playerSelection = playerSelection == 0 ? currentPlayer.CurrentHand.Count : playerSelection--; break;
                    case ConsoleKey.D : playerSelection = playerSelection == currentPlayer.CurrentHand.Count ? 0 : playerSelection++; break;
                    case ConsoleKey.W : currentPlayer.PlayCard(playerSelection); break;
                    case ConsoleKey.S : currentPlayer.RecallCard(playerSelection); break;
                    case ConsoleKey.Spacebar: currentPlayer.ConfirmPlay(); turnOver = true; break;
                    default: break;
                }
            }
        } else
        {
            ComputerTurn(currentPlayer);
        }
    }

    public void PlayRound()
    {
        while(PlayerOne.CurrentHand.Count < 10)
        {
            PlayerOne.Draw();
        }
        while(PlayerTwo.CurrentHand.Count < 10)
        {
            PlayerTwo.Draw();
        }
        PlayTurn(PlayerOne);
        PlayTurn(PlayerTwo);
    }


    //Revist logic for computer turn eventually (does not account for aces in hand to make better plays)
    public void ComputerTurn(Player currentPlayer)
    {
        foreach(Card card in currentPlayer.CurrentHand)
        {
            if(card.Suit == SuitRound)
            {
                if(currentPlayer.CurrentRound.Count > 4){
                    foreach(Card playedCard in currentPlayer.CurrentRound)
                    {
                        if(card.Value > playedCard.Value)
                        {
                            currentPlayer.CurrentRound.Remove(playedCard);
                            currentPlayer.CurrentRound.Add(card);
                            currentPlayer.CurrentHand.Add(playedCard);
                            currentPlayer.CurrentHand.Remove(card);
                        }
                    }
                } else
                {
                    currentPlayer.CurrentHand.Remove(card);
                    currentPlayer.CurrentRound.Add(card);
                }
            }
           
        }
    }

    public void SortHand(List<Card> Hand)
    {
        bool sorted = false;
        for(int iter = 0; iter < Hand.Count; iter++)
        {
            Card temp = Hand[iter];
            Hand.Remove(temp);
            for(int sortIter = 0; sortIter < Hand.Count; iter++)
            {
                if(temp.Suit < Hand[sortIter].Suit)
                {
                    Hand.Insert(sortIter, temp);
                    sorted = true;
                    break;
                } else if(temp.Suit == Hand[sortIter].Suit && temp.Value < Hand[sortIter].Value)
                {
                    Hand.Insert(sortIter, temp);
                    sorted = true;
                    break;
                }
            }
            if(sorted == false)
            {
                Hand.Add(temp);
            }
        }

    }
    public int ScoreHand(Player currPlayer)
    {
        int score = 0;
        int highest = 0;
        bool jackPresent = false;
        foreach(Card card in currPlayer.CurrentRound)
        {
            if(card.Suit == SuitRound){
                if(card.Value < 11)
                {
                    score += card.Value;
                    if(card.Value > highest)
                    {
                        highest = card.Value;
                    }
                } else if(card.Value == 11)
                {
                    jackPresent = true;
                } else if(card.Value == 12)
                {
                    QueenPlayed(currPlayer);
                } else if(card.Value == 13){
                    KingPlayed(currPlayer);
                }
            }
            if(card.Value == 14)
            {
                foreach(Card aceChecker in currPlayer.CurrentRound)
                {
                    if(aceChecker.Suit == card.Suit){
                        if(aceChecker.Value < 11)
                        {
                            score += aceChecker.Value;
                            if(aceChecker.Value > highest)
                            {
                                highest = aceChecker.Value;
                            }
                        } else if(aceChecker.Value == 11)
                        {
                            jackPresent = true;
                        } else if(card.Value == 12)
                        {
                            QueenPlayed(currPlayer);
                        } else if(card.Value == 13){
                            KingPlayed(currPlayer);
                        }
                    }
                }
            }
        }
        if(jackPresent == true)
        {
            score += highest;
        }
        return score;
    }

    public void QueenPlayed(Player current)
    {
        if(current.ID == 1)
        {
            OneQueenPlayed = true;
        } else if(current.ID == 2)
        {
            TwoQueenPlayed = true;
        }
    }

    public void KingPlayed(Player current)
    {
        if(current.ID == 1)
        {
            OneKingPlayed = true;
        } else if(current.ID == 2)
        {
            TwoKingPlayed = true;
        }
    }

    public void ScoreRound()
    {
        int playerOneTempScore = ScoreHand(PlayerOne);
        int playerTwoTempScore = ScoreHand(PlayerTwo);
        if (OneQueenPlayed)
        {
            playerTwoTempScore /= 2;
        }
        if (TwoQueenPlayed)
        {
            playerOneTempScore /= 2;
        }
        if(OneKingPlayed && !TwoKingPlayed)
        {
            PlayerOne.Score += playerOneTempScore;
        } else if(!OneKingPlayed && TwoKingPlayed)
        {
            PlayerTwo.Score += playerTwoTempScore;
        } else
        {
            if(playerOneTempScore > playerTwoTempScore)
            {
                PlayerOne.Score += playerOneTempScore;
            } else if(playerOneTempScore < playerTwoTempScore)
            {
                PlayerTwo.Score += playerTwoTempScore;
            }
        }
        
        
    }

    public void CheckForWinner()
    {
        if(PlayerOne.Score >= 100)
        {
            Winner = 1;   
        } else if(PlayerTwo.Score >= 100)
        {
            Winner = 2;
        }
    }
}