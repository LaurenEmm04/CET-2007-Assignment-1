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
    /// <summary>
    /// Holds checks for if the libary has games, listing games, adding games, finding games by name/ID and saving/loading the game library
    /// </summary>
    public class GameLibrary
    {
        private List<Game> games = new List<Game>();

        /// <summary>
        /// Displays the list of games
        /// </summary>
        public void GameList()
        {
            Logger.GetInstance().Log("Displaying the game library.");
            Console.WriteLine("Here is a list of games in the library:");
            foreach (var game in games)
            {
                Console.WriteLine($"- {game.GameName} ({game.GameGenre}) - ID: {game.GameID}");
            }
        }

        /// <summary>
        /// default games are listed here
        /// </summary>
        public GameLibrary()
        {
            //default games to make sure there is something here
            games.Add(new Game("Minecraft", "Sandbox"));
            games.Add(new Game("Stardew Valley", "Farming"));
            games.Add(new Game("Spirit of the North", "Adventure"));
        }



        /// <summary>
        /// Adding games function
        /// </summary>
        /// <param name="game"></param>
        public void AddGame(Game game = null)
        {
            //if a game object is passed through (already made), add it in
            if (game != null)
            {
                games.Add(game);
                Console.WriteLine($"Added game {game.GameName} - {game.GameGenre} - {game.GameID} to library!");
                Logger.GetInstance().Log($"{game.GameName} | {game.GameGenre} - {game.GameID} has been added to the library.");
                return;
            }


            //otherwise, make it here
            Console.WriteLine("Please add the name of the game you wish to add to your library");
            string GName = Console.ReadLine();

            Console.WriteLine($"Please enter the genre of {GName}");
            string GGenre = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(GName) || string.IsNullOrWhiteSpace(GGenre))
            {
                Logger.GetInstance().Log("Error: Game and Genre were not correctly inputted. Retrying.");
                Console.WriteLine("Error. Please enter a name and a genre of your game in the areas provided");
                return;
            }

            foreach (var existingGame in games) //for each game thats in the game list
            {
                if (existingGame.GameName.ToLower() == GName.ToLower()) //if the game your adding matches a game thats already in the list..
                {
                    Console.WriteLine($"This game {GName} already exists in the library!");
                    Logger.GetInstance().Log("Game attempted to add to library already exists. Duplicate game has not been added.");
                    return; //doesnt add it to the library
                }
            }


            //adding new games
            Game NewGame = new Game(GName, GGenre);
            bool DuplicateID = false;

            //checking for duplicate IDs
            foreach (var ExistingGame in games)  //for each game in the list 
            {
                if (ExistingGame.GameID == NewGame.GameID)  //if the game your adding has the same id as the ones in the list..
                {
                    DuplicateID = true;  //it is a duplicate and the random number will be ran again below
                    break;
                }
            }

            while (DuplicateID) //while the number is still found in the list..
            {
                NewGame.GameID = Game.RandomGenerator.Next(0, 999); //itll try and generate a new id that doesnt match
                DuplicateID = false;
                foreach (var ExistingGame in games)  //checks if ID is duplicate
                {
                    if (ExistingGame.GameID == NewGame.GameID) //if it is..
                    {
                        DuplicateID = true; //ID already exists, try again
                        break;
                    }

                }
            }
            Console.WriteLine($"Game: {NewGame.GameName} - Genre: {NewGame.GameGenre} - ID: {NewGame.GameID} has been added to the library.");
            Logger.GetInstance().Log($"Game: {NewGame.GameName} - Genre: {NewGame.GameGenre} - ID: {NewGame.GameID} has been added to the library.");
            games.Add(NewGame);  //added to the game list

        }

        public Game FindGameByName(string GName)
        {
            foreach (var game in games)
            {
                if (game.GameName.ToLower() == GName.ToLower()) //if game name entered in search matches one in the list, return the game info from the list
                {
                    return game;
                }

            }
            Console.WriteLine("Sorry, your game can't be found. Please retry");  //no match
            Logger.GetInstance().Log("Game cannot be found.");
            return null;

        }


        public Game FindGameByID(int GID)
        {   //looping through all library games
            foreach (var game in games)
            {
                if (game.GameID == GID)  //searched for ID matche one in the list, return ID and game info
                {
                    return game;
                }

            }

            Console.WriteLine("Sorry, your game can't be found. Please retry"); //not found
            Logger.GetInstance().Log("Game was not found.");
            return null;
        }


        public void SaveGameLibraryToFile()
        {
            try
            {
                string json = JsonSerializer.Serialize(games, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText("Games.json", json);  //writing eveything game related to game.json
                Console.WriteLine("Your game libary has been written to the file!");
                Logger.GetInstance().Log("Game library saved."); //logging interaction
            }
            catch (Exception ex)
            {
                Console.WriteLine("There's been an error saving your game library :( " + ex.Message); //possible error 
                Logger.GetInstance().Log("Game library not saved");
            }
        }

        public void LoadGameLibraryFromFile()
        {
            try
            {
                if (!File.Exists("Games.json")) //if Games.json doesnt exist
                {
                    Console.WriteLine("You dont seem to have a saved game library..");
                    Logger.GetInstance().Log("Saved game library does not exist");
                    return;
                }
                string json = File.ReadAllText("Games.json"); //if its empty..
                if (string.IsNullOrWhiteSpace(json))
                {
                    Console.WriteLine("Your game library is empty");
                    Logger.GetInstance().Log("Game library is empty.");
                    return;
                }

                games = JsonSerializer.Deserialize<List<Game>>(json);  //if it exists and isnt empty..
                Console.WriteLine("Your game libary is displayed below!"); //displays
                Logger.GetInstance().Log("Game library displayed");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading game libary:" + ex.Message);  //error grabber
                Logger.GetInstance().Log("Game libary couldn't be loaded.");
            }
        }





    }
}