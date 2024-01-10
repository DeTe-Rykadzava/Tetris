// See https://aka.ms/new-console-template for more information

using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using Tetris.Core.Game;

var SavedScore = 0;

if (!File.Exists("ScoreSave.txt"))
{
    using var sw = new StreamWriter("ScoreSave.txt");
    var JsString = JsonSerializer.Serialize(new { SavedScore = 0 });
    sw.WriteLine(JsString);
}
else
{
    try
    {
        var fileJSON = JsonNode.Parse(File.OpenRead("ScoreSave.txt"));
        SavedScore = fileJSON["SavedScore"].GetValue<int>();
    }
    catch (Exception e)
    {
        Console.WriteLine(e);
    }
}


var game = new Game();
game.GameOver += () =>
{
    Console.Clear();
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine("\n\t\tGAME OVER");
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine($"\nYou have scored {game.Score} points");
    Console.ResetColor();
    Console.WriteLine("\nWant play again? {Y} - Yes / {N} - No\n");
    
    if (SavedScore < game.Score)
    {
        SavedScore = game.Score;
        SaveScore();
    }

    while (true)
    {
        var inputKey = Console.ReadKey();
        if (inputKey.Key == ConsoleKey.Y)
        {
            ShowMainMenu();
            return;
        }
        if(inputKey.Key == ConsoleKey.N)
        {
            Environment.Exit(0);
            return;
        }

        Console.WriteLine("You have entered an unexpected key");
    }
};

Task.Run(() =>
{
    ShowMainMenu();
});

while (true) { }

void ShowMainMenu()
{
    Console.Clear();
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine("\n\t\t\tTETRIS");
    Console.ResetColor();
    Console.WriteLine("===========================================================");
    Console.WriteLine("\t\tУправление:");
    Console.WriteLine("\t{\u2190}\t подвинуть фигуру влево");
    Console.WriteLine("\t{\u2192}\t подвинуть фигуру вправо");
    Console.WriteLine("\t{\u2193}\t подвинуть фигуру вниз");
    Console.WriteLine("\t{Space}\t повернуть фигуру");
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine("Рекомендуется нажатие без удержания кнопки, это работает лучше");
    Console.ResetColor();
    Console.WriteLine("===========================================================");
    Console.WriteLine($"\tМаксимально набранное количество очков: {SavedScore}");
    Console.WriteLine("===========================================================");
    Console.WriteLine("\n{G} - начать игру / {C} - закрыть игру");
    
    while (true)
    {
        var inputKey = Console.ReadKey().Key;
        if (inputKey == ConsoleKey.G)
        {
            game.StartGame();
            //Console.WriteLine("GameEnd");
            break;
        }
        if(inputKey == ConsoleKey.C)
        {
            Environment.Exit(0);
            return;
        }

        Console.WriteLine("You have entered an unexpected key");
    }
}

void SaveScore()
{
    using var sw = new StreamWriter("ScoreSave.txt");
    sw.WriteLine(JsonSerializer.Serialize(new { SavedScore = SavedScore }));
    sw.Dispose();
}