//Run the game after selecting the amount of players
try{
    Console.OutputEncoding = Encoding.UTF8;
    Game main = ShowIntroRules();
    Console.Clear();
    ShowRules();
    RunGame(main);
}
catch{
}
finally{
}
//Show the intro and overview of the game and allow for the user to select
//the amount of human players they are playing with today
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
//Display the rules for the player and wait for input before moving on
void ShowRules()
{
    Console.Clear();
    Console.WriteLine();
    Console.WriteLine("The object of the card game is to get the highest");
    Console.WriteLine("scoring hand possible playing no more than 5 cards");
    Console.WriteLine("in a single round. Only the winner of the round will");
    Console.WriteLine("have their points earned that round to their total.");
    Console.WriteLine();
    Console.WriteLine("Each round a suit will be chosen randomly from the");
    Console.WriteLine("standard 4 suits. During that round only cards from");
    Console.WriteLine("that suit will score or have an effect normally.");
    Console.WriteLine();
    Console.WriteLine("Cards 2 - 10 earn you points based directly on their");
    Console.WriteLine("number value, while face cards and 2 Jokers have special");
    Console.WriteLine("effects when played. Jacks when played double the points");
    Console.WriteLine("gained from your single highest card. Queens when played");
    Console.WriteLine("cut your opponents points for that round in half. Kings");
    Console.WriteLine("when played ensure that you win the round and your points.");
    Console.WriteLine("Aces allow you to play any other card of that suit even if");
    Console.WriteLine("it isn't that suit's round. Jokers will add double the");
    Console.WriteLine("points you scored that round to your total if you win.");
    Console.WriteLine();
    Console.WriteLine("Press Any Key To Start The Game!");
    Console.ReadKey(true);
}

//Runs the game until a winner is declared and then prints that before exiting
void RunGame(Game game)
{
    while(game.Winner == 0)
    {
        game.PlayRound();  
    }
    Console.WriteLine("Player " + (game.Winner == 1 ? "One" : "Two") + " Has Won The Game!");
}

