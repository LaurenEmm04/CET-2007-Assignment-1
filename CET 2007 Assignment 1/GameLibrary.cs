using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CET2007A1
{
    public class GameLibrary
    {
        private List<Game> games = new List<Game>();

        public bool HasGames()
        {
            return games.Count > 0;
        }

        public void GameList()
        {
            Logger.GetInstance().Log("Displaying the game library.");
            Console.WriteLine("Here is a list of games in the library:");
            foreach (var game in games)
            {
                Console.WriteLine($"- {game.GameName} ({game.GameGenre})");
            }
        }

        public GameLibrary()
        {
            //default games to make sure there is something here
            games.Add(new Game("Minecraft", "Sandbox"));
            games.Add(new Game("Stardew Valley", "Farming"));
            games.Add(new Game("Spirit of the North", "Adventure"));
        }

        public void AddGame(Game game = null)
        {
            //if a game object is passed through, add it in
            if (game != null)
            {
                games.Add(game);
                Console.WriteLine($"Added game {game.GameName} - {game.GameGenre} to library!");
                Logger.GetInstance().Log($"{game.GameName} | {game.GameGenre} has been added to the library.");
                return;
            }

            Console.WriteLine("Please add the name of the game you wish to add to your library");
            string GName = Console.ReadLine();

            Console.WriteLine($"Please enter the genre of {GName}");
            string GGenre = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(GName) || string.IsNullOrWhiteSpace(GGenre))
            {
                Logger.GetInstance().Log("Error: Game and Genre were not correctly inputted. Retrying.");
                Console.WriteLine("Please enter a name and a genre of your game in the areas provided");
                return;
            }

            foreach (var existingGame in games)
            {
                if (existingGame.GameName.ToLower() == GName.ToLower())
                {
                    Console.WriteLine($"This game {GName} already exists in the library!");
                    Logger.GetInstance().Log("Game attempted to add to library already exists. Duplicate game has not been added.");
                    return; //doesnt add it to the library
                }
            }

            //adding new games
            Game NewGame = new Game(GName, GGenre);
            Logger.GetInstance().Log($"Game: {NewGame.GameName} - Genre: {NewGame.GameGenre} has been added to the library.");
            games.Add(NewGame);
            
        }

        public Game FindGameByID(int GID)
        {   //looping through all library games
            foreach (var game in games)
            {
                if (game.GameID == GID)
                {
                    return game;
                }

            }

            Console.WriteLine("Sorry, your game can't be found. Please retry");
            Logger.GetInstance().Log("Game was not found.");
            return null;
        }


        public void SaveGameLibraryToFile()
        {
            try
            {
                string json = JsonSerializer.Serialize(games, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText("Games.json", json);
                Console.WriteLine("Your game libary has been written to the file!");
                Logger.GetInstance().Log("Game library saved.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("There's been an error saving your game library :( " + ex.Message);
                Logger.GetInstance().Log("Game library not saved");
            }
        }

        public void LoadGameLibraryFromFile()
        {
            try
            {
                if (!File.Exists("Games.json"))
                {
                    Console.WriteLine("You dont seem to have a saved game library..");
                    Logger.GetInstance().Log("Saved game library does not exist");
                    return;
                }
                string json = File.ReadAllText("Games.json");
                if (string.IsNullOrWhiteSpace(json))
                {
                    Console.WriteLine("Your game library is empty");
                    Logger.GetInstance().Log("Game library is empty.");
                    return;
                }

                games = JsonSerializer.Deserialize<List<Game>>(json);
                Console.WriteLine("Your game libary is displayed below!");
                Logger.GetInstance().Log("Game library displayed");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading game libary:" + ex.Message);
                Logger.GetInstance().Log("Game libary couldn't be loaded.");
            }
        }


        public Game FindGameByName(string GName)
        {
            foreach (var game in games)
            {
                if (game.GameName.ToLower() == GName.ToLower())
                {
                    return game;
                }

            }
            Console.WriteLine("Sorry, your game can't be found. Please retry");
            Logger.GetInstance().Log("Game cannot be found.");
            return null;

        }


    }
}