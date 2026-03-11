
using System.Text;

Exception? exception = null;




try{
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
        game.PickNewSuit();
        RenderGame(game);
        game.PlayRound();
    }
}


void RenderGame(Game game)
{
    Console.Clear();
    Console.WriteLine("A");
    PrintHand(game);
    List<String> displayRaw = Enumerable.Repeat(string.Empty, 10).ToList();
    RenderHand(game, displayRaw);
    StringBuilder display = new StringBuilder();
    display.Append(PrintSuitRound(game));
    display.AppendLine();
    foreach(string renderString in displayRaw)
    {
        display.AppendLine(renderString);
    }

    Console.WriteLine(display);
}

void RenderHand(Game game, List<String> display)
{
    
    foreach(Card card in game.PlayerOne.CurrentHand)
    {
        Console.WriteLine("W");
        card.PrintCard(display);
    }
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
    suitDisplay.AppendLine("╔═══════════════╗");
    suitDisplay.AppendLine($"║Current Suit: {game.SuitRound}║");
    suitDisplay.AppendLine("╚═══════════════╝");
    return suitDisplay;
}