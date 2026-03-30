namespace CardGame;

public class Game
{
    public Player PlayerOne { get; }
    public Player PlayerTwo { get; }
    public int SuitRound { get;set; }
    public int Winner { get; set; }

    //Constructor to create the game based on how many human
    //players were given, as it is a two player game all scenarios
    //are covered, 2 CPU, 1 Human 1 CPU, 2 Human and then makes sure
    //to draw up to 10 cards for each player to start the game.
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
    //Set the current suit round
    public void PickNewSuit()
    {
        SuitRound = Random.Shared.Next(0,4);
    }
    //Clear the player values for the next round
    //cleans up variables like if specific face
    //cards were played for the next round so that
    //those values don't carry over.
    public void ResetRound()
    {
        PlayerOne.ClearRound();
        PlayerTwo.ClearRound();
    }
    //Method to build the display for what suit
    //the current round is for the players.
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
        } else
        {
            suitChar = "N/A";
        }
        suitDisplay.AppendLine("╔════════════════╗");
        suitDisplay.AppendLine($"║Current Suit: {suitChar} ║");
        suitDisplay.AppendLine("╚════════════════╝");
        return suitDisplay;
    }
    //Method to build the display for the controls for the player to have
    StringBuilder PrintControls()
    {
        StringBuilder controlDisplay = new StringBuilder();
        controlDisplay.AppendLine("╔════════════════════════════════════════════════════════════╗");
        controlDisplay.AppendLine("║                         Controls                           ║");
        controlDisplay.AppendLine("║ A - Move Card Selector Left ║ D - Move Card Selector Right ║");
        controlDisplay.AppendLine("║ W - Play Selected Card      ║ S - Recall Selected Card     ║");
        controlDisplay.AppendLine("║          Space - Confirm Hand To Play this round           ║");
        controlDisplay.AppendLine("╚════════════════════════════════════════════════════════════╝");
        return controlDisplay;
    }
    //Method to display the hahnd of the current player selected
    //goes through and prints the card and display into a list of strings
    //that can be appened horizontally so that I can update the list
    //while display all cards in a line
    StringBuilder RenderHand(List<Card> currHand)
    {
        StringBuilder finalDisp = new StringBuilder();
        List<String> display = Enumerable.Repeat(string.Empty, 6).ToList();
        for(int i = 0; i < currHand.Count; i++)
        {
            currHand[i].PrintCard(display);
            currHand[i].PrintCardState(display);
        }
        foreach(string dispStr in display)
        {
            finalDisp.AppendLine(dispStr);
        }
        return finalDisp;
    }
    //Method to govern the logic of a turn for the player
    //if it is a human player it relies on input from the
    //keyboard to move the selector and select each card
    //however if it is a computer it just calls a different method
    //for the computer to use logic to complete its turn
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
    //Method to render the console output of the game
    //It will show the current hands and then the current
    //score of each player below that
    public void RenderGameState()
    {
        Console.Clear();
        Console.WriteLine(PrintSuitRound());
        Console.WriteLine();
        Console.WriteLine(RenderHand(PlayerOne.CurrentHand));
        Console.WriteLine(RenderHand(PlayerTwo.CurrentHand));
        Console.WriteLine();
        Console.WriteLine("Score Player One: " + PlayerOne.Score);
        Console.WriteLine("Score Player Two: " + PlayerTwo.Score);
        Console.WriteLine();
        Console.WriteLine(PrintControls());
    }
    //Running through a games round it covers the logic
    //First it picks a new suit for the round to play and then
    //resets each players variables for the round, while the hands
    //are not full it will draw them up to 10 cards and then run
    //the play turn command on each one. after each turn it will
    //then print the hands that were played and their scores along
    //with the winner of each round
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
        Console.Clear();
        Console.WriteLine("Player One Played: ");
        Console.WriteLine(RenderHand(PlayerOne.CurrentRound));
        Console.WriteLine();
        Console.WriteLine("Player Two Played: ");
        Console.WriteLine(RenderHand(PlayerTwo.CurrentRound));
        Console.WriteLine();
        Console.WriteLine("Player One Hand Score Before Modifiers: " + ScoreHand(PlayerOne));
        Console.WriteLine("Player Two Hand Score Before Modifiers: " + ScoreHand(PlayerTwo));
        (int x, int y) finalScore = ScoreRound();
        Console.WriteLine("Player One Hand Score After Modifiers: " + finalScore.x);
        Console.WriteLine("Player Two Hand Score After Modifiers: " + finalScore.y);
        Console.WriteLine();
        PrintRoundWinner(finalScore);
        Console.WriteLine("Press Any Key to start the next round.");
        Console.ReadKey(true);
        CheckForWinner();
        
    }
    //Method to print the round winner to inform the player who won each round.
    public void PrintRoundWinner((int x,int y) roundScore)
    {
        if(PlayerOne.KingPlayed && !PlayerTwo.KingPlayed)
        {
            Console.WriteLine("By Playing a King, Player One Won This Round!");
        } else if(!PlayerOne.KingPlayed && PlayerTwo.KingPlayed)
        {
            Console.WriteLine("By Playing a King, Player Two Won This Round");
        } else if(roundScore.x > roundScore.y)
        {
            Console.WriteLine("Player One Won This Round!");
        } else if(roundScore.y > roundScore.x)
        {
            Console.WriteLine("Player Two Won This Round!");
        } else
        {
            Console.WriteLine("It's A Draw! Neither Player Wins This Round.");
        }
        Console.WriteLine();
    }
    public void ComputerTurn(Player currentPlayer)
    {
        Card? jokerPresent = new Joker();
        bool hasJoker = false;
        foreach(Card card in currentPlayer.CurrentHand)
        {
            if(card.Suit == -1)
            {
                jokerPresent = card;
                hasJoker = true;
            }
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
        if(currentPlayer.CurrentRound.Count < 5 && hasJoker)
        {
            currentPlayer.CurrentHand.Add(jokerPresent);
        }
        if(currentPlayer.CurrentRound.Count == 0)
        {
            while(currentPlayer.CurrentRound.Count < 3)
            {
                currentPlayer.PlayCard(Random.Shared.Next(0, currentPlayer.CurrentHand.Count));
            }
        }
        RenderGameState();
        foreach(Card playedCard in currentPlayer.CurrentRound)
        {
            currentPlayer.CurrentHand.Remove(playedCard);
        }
    }
    //Method to sort hand based on suit and value
    public void SortHand(List<Card> Hand)
    {
        for(int iter = 0; iter < Hand.Count; iter++)
        {
            bool sorted = false;
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
    //Method to score the player hand, goes through each
    //of the cards and if valid for scoring adds them to the
    //current score and deals with the playing of face cards
    //and tracking those as well
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
    //Method for if the player played a queen
    public void QueenPlayed(Player current)
    {
        current.QueenPlayed = true;
    }
    //Method for if the player played a king
    public void KingPlayed(Player current)
    {
        current.KingPlayed = true;
    }
    //Method that returns a tuple of the two players scores from the
    //round after factoring in the abilities of the face cards. The logic
    //follows this flow:
    //Raw Scores of Hands -> Adjust if Queen Played -> Check winner if by King player ->
    //If no kings or both players played kings, check which score is higher -> add the
    //higher scoring players score to their total -> if the higher scoring player played
    //a Joker add the score again (doubling their poitns earned)
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
    //Check if either player's scores have reached 100 yet to be declared winner
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