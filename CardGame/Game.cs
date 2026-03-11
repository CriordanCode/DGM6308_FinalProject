namespace CardGame;






public class Game
{
    public Player playerOne { get; }
    public Player playerTwo { get; }

    public int SuitRound { get; }





    public Game(int humans)
    {
        if(humans == 2)
        {
            playerOne = new Player( 1, true);
            playerTwo = new Player( 2, true);
        } else if(humans == 1)
        {
            playerOne = new Player( 1, true);
            playerTwo = new Player( 2, false);
        } else
        {
            playerOne = new Player( 1, false);
            playerTwo = new Player( 2, false);
        }
        SuitRound = 0;

    }

    public void PickNewSuit()
    {
        SuitRound = Random.Shared.Next(0,4);
    }


    public void PlayRound(Player currentPlayer)
    {
        int playerSelection = 0;
        if(currentPlayer.Human == true){
            while (true)
            {
            
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.A : playerSelection = playerSelection == 0 ? currentPlayer.CurrentHand.Count() : playerSelection--; break;
                    case ConsoleKey.D : playerSelection = playerSelection == currentPlayer.CurrentHand.Count() ? 0 : playerSelection++; break;
                    case ConsoleKey.W : break;
                    case ConsoleKey.S : break;
                    case ConsoleKey.LeftArrow: break;
                    case ConsoleKey.RightArrow: break;
                    case ConsoleKey.UpArrow: break;
                    case ConsoleKey.DownArrow: break;
                    case ConsoleKey.Spacebar: break;
                    case ConsoleKey.Enter: break;
                    default: break;
                }
            }
        } else
        {
            ComputerTurn(currentPlayer);
        }
    }



    public void ComputerTurn(Player currentPlayer)
    {
        foreach(Card card in currentPlayer.CurrentHand)
        {
            if(card.Value < 1 );
           
        }
    }

}