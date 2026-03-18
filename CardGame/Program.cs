





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

