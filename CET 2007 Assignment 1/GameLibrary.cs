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
                return;
            }

            Console.WriteLine("Please add the name of the game you wish to add to your library");
            string GName = Console.ReadLine();

            Console.WriteLine($"Please enter the genre of {GName}");
            string GGenre = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(GName) || string.IsNullOrWhiteSpace(GGenre))
            {
                Console.WriteLine("Please enter a name and a genre of your game in the areas provided");
                return;
            }

            foreach (var existingGame in games)
            {
                if (existingGame.GameName.ToLower() == GName.ToLower())
                {
                    Console.WriteLine($"This game {GName} already exists in the library!");
                    return; //doesnt add it to the library
                }
            }

            //adding new games
            Game NewGame = new Game(GName, GGenre);
            games.Add(NewGame);
            Console.WriteLine("You have added a new game to your game library!");
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
            return null;
        }


        public void SaveGameLibraryToFile()
        {
            try
            {
                string json = JsonSerializer.Serialize(games, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText("Games.json", json);
                Console.WriteLine("Your game libary has been written to the file!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("There's been an error saving your game library :( " + ex.Message);
            }
        }

        public void LoadGameLibraryFromFile()
        {
            try
            {
                if (!File.Exists("Games.json"))
                {
                    Console.WriteLine("You dont seem to have a saved game library..");
                    return;
                }
                string json = File.ReadAllText("Games.json");
                if (string.IsNullOrWhiteSpace(json))
                {
                    Console.WriteLine("Your game library is empty");
                    return;
                }

                games = JsonSerializer.Deserialize<List<Game>>(json);
                Console.WriteLine("Your game libary is displayed below!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading game libary:" + ex.Message);
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
            return null;

        }


    }
}