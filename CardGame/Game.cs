using System.Runtime.CompilerServices;

namespace CardGame;






public class Game
{
    public Player PlayerOne { get; }
    public Player PlayerTwo { get; }

    public int SuitRound { get;set; }

    public int Winner { get; set; }

    




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

    public void ResetRound()
    {
        ResetHands();
    }

    StringBuilder PrintSuitRound()
    {
        StringBuilder suitDisplay = new StringBuilder();
        string suitChar;
        if(SuitRound == 0)
        {
            suitChar = "♠";    
        } else if(SuitRound == 1)
        {
            suitChar = "♥";
        } else if(SuitRound == 2)
        {
            suitChar = "♦"; 
        } else if(SuitRound == 3)
        {
            suitChar = "♣";
        }
        else
        {
            suitChar = "N/A";
        }
        suitDisplay.AppendLine("╔════════════════╗");
        suitDisplay.AppendLine($"║Current Suit: {suitChar} ║");
        suitDisplay.AppendLine("╚════════════════╝");
        return suitDisplay;
    }
    StringBuilder RenderHand(Player currP)
    {
        StringBuilder finalDisp = new StringBuilder();
        List<String> display = Enumerable.Repeat(string.Empty, 6).ToList();
        for(int i = 0; i < currP.CurrentHand.Count; i++)
        {
            currP.CurrentHand[i].PrintCard(display);
            currP.CurrentHand[i].PrintCardState(display);
        }
        foreach(string dispStr in display)
        {
            finalDisp.AppendLine(dispStr);
        }
        return finalDisp;
    }

    public void PlayTurn(Player currentPlayer)
    {
        int playerSelectionPrev = 0;
        int playerSelection = 0;
        bool turnOver = false;
        
        if(currentPlayer.Human == true){
            while (!turnOver)
            {
                RenderGameState();
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.A :
                        playerSelectionPrev = playerSelection;
                        playerSelection --; 
                        if(playerSelection == -1)
                        {
                            playerSelection = currentPlayer.CurrentHand.Count - 1;
                        }
                        currentPlayer.CurrentHand[playerSelectionPrev].Selected = false; 
                        currentPlayer.CurrentHand[playerSelection].Selected = true; 
                        break;
                    case ConsoleKey.D : 
                        playerSelectionPrev = playerSelection;
                        playerSelection ++;
                        if(playerSelection == currentPlayer.CurrentHand.Count)
                        {
                            playerSelection = 0;
                        }
                        currentPlayer.CurrentHand[playerSelectionPrev].Selected = false;
                        currentPlayer.CurrentHand[playerSelection].Selected = true;
                        break;
                    case ConsoleKey.W : currentPlayer.PlayCard(playerSelection); break;
                    case ConsoleKey.S : currentPlayer.RecallCard(playerSelection); break;
                    case ConsoleKey.Spacebar: 
                        currentPlayer.CurrentHand[playerSelection].Selected = false; 
                        currentPlayer.ConfirmPlay(); 
                        turnOver = true; 
                        break;
                    default: break;
                }
            }
        } else
        {
            Console.WriteLine("Turn 2");
            ComputerTurn(currentPlayer);
        }
    }

    public void RenderGameState()
    {
        Console.Clear();
        Console.WriteLine(PrintSuitRound());
        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine(RenderHand(PlayerOne));
        Console.WriteLine(RenderHand(PlayerTwo));
        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine("Score Player One: " + PlayerOne.Score);
        Console.WriteLine("Score Player Two: " + PlayerTwo.Score);
    }

    public void PlayRound()
    {
        PickNewSuit();
        ResetRound();
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
        Console.WriteLine("Player One Hand Score Before Modifiers: " + ScoreHand(PlayerOne));
        Console.WriteLine("Player Two Hand Score Before Modifiers: " + ScoreHand(PlayerTwo));
        (int x, int y) finaleScore = ScoreRound();
        Console.WriteLine("Player One Hand Score After Modifiers: " + finaleScore.x);
        Console.WriteLine("Player Two Hand Score After Modifiers: " + finaleScore.y);
        Console.ReadKey(true);
        CheckForWinner();
        
    }


    //Revist logic for computer turn eventually (does not account for aces in hand to make better plays)
    public void ComputerTurn(Player currentPlayer)
    {
        foreach(Card card in currentPlayer.CurrentHand)
        {
            if(card.Suit == SuitRound)
            {
                if(currentPlayer.CurrentRound.Count > 4){
                    if(card.Value > currentPlayer.CurrentRound[0].Value)
                    {
                        currentPlayer.CurrentRound.RemoveAt(0);
                        currentPlayer.CurrentRound.Add(card);
                    }
                } else
                {
                    currentPlayer.CurrentRound.Add(card);
                    card.Played = true;
                }
            }
            if(card.Value == 14)
            {
                if(currentPlayer.CurrentRound.Count > 4){
                    if(card.Value > currentPlayer.CurrentRound[0].Value)
                    {
                        currentPlayer.CurrentRound.RemoveAt(0);
                        currentPlayer.CurrentRound.Add(card);
                    }
                } else
                {
                    currentPlayer.CurrentRound.Add(card);
                }
                foreach(Card cardAce in currentPlayer.CurrentHand)
                {
                    if(card.Suit == cardAce.Suit)
                    {
                        if(currentPlayer.CurrentRound.Count > 4)
                        {
                            for(int iter = 0; iter < currentPlayer.CurrentRound.Count; iter++)
                            {
                                if(cardAce.Value > currentPlayer.CurrentRound[0].Value)
                                {
                                    currentPlayer.CurrentRound.RemoveAt(0);
                                    currentPlayer.CurrentRound.Add(cardAce);
                                    break;
                                }
                            }
                        }
                    }
                }
            }
           
        }
        RenderGameState();
        foreach(Card playedCard in currentPlayer.CurrentRound)
        {
            currentPlayer.CurrentHand.Remove(playedCard);
        }
        
        Console.WriteLine("Computer Turn Over");
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
            if(card.Suit == -1)
            {
                currPlayer.JokerPlayed = true;
            }
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
        current.QueenPlayed = true;
    }

    public void KingPlayed(Player current)
    {
        current.KingPlayed = true;
    }

    public (int x, int y) ScoreRound()
    {
        int playerOneTempScore = ScoreHand(PlayerOne);
        int playerTwoTempScore = ScoreHand(PlayerTwo);
        if (PlayerOne.QueenPlayed)
        {
            playerTwoTempScore /= 2;
        }
        if (PlayerTwo.QueenPlayed)
        {
            playerOneTempScore /= 2;
        }
        if(PlayerOne.KingPlayed && !PlayerTwo.KingPlayed)
        {
            PlayerOne.Score += playerOneTempScore;
            if(PlayerOne.JokerPlayed == true)
            {
                PlayerOne.Score += playerOneTempScore;
            }
        } else if(!PlayerOne.KingPlayed && PlayerTwo.KingPlayed)
        {
            PlayerTwo.Score += playerTwoTempScore;
            if(PlayerTwo.JokerPlayed == true)
            {
                PlayerTwo.Score += playerTwoTempScore;
            }
        } else
        {
            if(playerOneTempScore > playerTwoTempScore)
            {
                PlayerOne.Score += playerOneTempScore;
                if(PlayerOne.JokerPlayed == true)
                {
                    PlayerOne.Score += playerOneTempScore;
                }
            } else if(playerOneTempScore < playerTwoTempScore)
            {
                PlayerTwo.Score += playerTwoTempScore;
                if(PlayerTwo.JokerPlayed == true)
                {
                    PlayerTwo.Score += playerTwoTempScore;
                }
            }
        }
        return (playerOneTempScore, playerTwoTempScore);
        
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

    public void ResetHands()
    {
        PlayerOne.ClearRound();
        PlayerTwo.ClearRound();
    }

}