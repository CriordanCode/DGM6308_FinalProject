





try{
    Console.OutputEncoding = Encoding.UTF8;
    Game main = ShowIntroRules();
    Console.Clear();
    RunGame(main);
    
}
catch{
    
}
finally{
    
}

Game ShowIntroRules()
{
    StringBuilder display = new StringBuilder();
    display.AppendLine();
    display.AppendLine("DGM 6308 Final Project");
    display.AppendLine();
    display.AppendLine();
    display.AppendLine();
    display.AppendLine("This card game is the final project for the class");
    display.AppendLine("DGM 6308 and serves as an exercise in coding");
    display.AppendLine("Console Output Based Applications");
    display.AppendLine();
    display.AppendLine("This Game can be played with up to Two(2) human");
    display.AppendLine("players, or as a spectator for computers with Zero(0)");
    display.AppendLine("human players. Please input the number of human");
    display.AppendLine("players you wish to proceed with");
    display.AppendLine();
    display.AppendLine("[0] Computer vs Computer");
    display.AppendLine("[1] Player vs Computer");
    display.AppendLine("[2] Player vs Player");
    Console.WriteLine(display);

    int? humanPlayers = null;
    while(humanPlayers is null)
    {
        Console.CursorVisible = false;

        switch (Console.ReadKey(true).Key)
        {
            case ConsoleKey.D0 : humanPlayers = 0; break;
            case ConsoleKey.D1 : humanPlayers = 1; break;
            case ConsoleKey.D2 : humanPlayers = 2; break;
        }
    }

    return new Game(humanPlayers.Value);
}


void RunGame(Game game)
{
 
    while(game.Winner == 0)
    {

        game.PlayRound();
        
    }
}


void RenderGame(Game game)
{
    Console.Clear();
    Console.WriteLine("A");
    Console.WriteLine(PrintSuitRound(game));
    Console.WriteLine();
    Console.WriteLine();
    Console.WriteLine();
    Console.WriteLine(RenderHand(game, game.PlayerOne));
    Console.WriteLine();
    Console.WriteLine(RenderHand(game, game.PlayerTwo));
    Console.WriteLine();
    Console.WriteLine();
    Console.WriteLine("Score Player One: " + game.PlayerOne.Score);
    Console.WriteLine("Score Player Two: " + game.PlayerTwo.Score);



}

StringBuilder RenderHand(Game game, Player currP)
{
    StringBuilder finalDisp = new StringBuilder();
    List<String> display = Enumerable.Repeat(string.Empty, 5).ToList();
    foreach(Card card in currP.CurrentHand)
    {
        card.PrintCard(display);
    }
    foreach(string dispStr in display)
    {
        finalDisp.AppendLine(dispStr);
    }
    return finalDisp;
}

void PrintHand(Game game)
{
    List<String> cardArts = new List<String>();
    foreach(Card card in game.PlayerOne.CurrentHand)
    {
        cardArts.Add(card.RawPrintCard());
    }
    Console.WriteLine("B");
    var cardLines = cardArts.Select(x => x.ToString().Split('\r').ToList().Select(y => y.Replace("\r", string.Empty).Replace("\n", string.Empty)).ToList()).ToList();
        var maximumCardHeight = cardLines.Max(x => x.Count);
        for(var i = 0; i < maximumCardHeight - 1; i++)
        {
            cardLines.ForEach(x =>
            {
                if (i < x.Count) Console.Write(x[i]);
            });
            Console.WriteLine();
        }

}

StringBuilder PrintSuitRound(Game game)
{
    StringBuilder suitDisplay = new StringBuilder();
    string suitChar;
    if(game.SuitRound == 0)
    {
        suitChar = "♠";    
    } else if(game.SuitRound == 1)
    {
        suitChar = "♥";
    } else if(game.SuitRound == 2)
    {
        suitChar = "♦"; 
    } else if(game.SuitRound == 3)
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