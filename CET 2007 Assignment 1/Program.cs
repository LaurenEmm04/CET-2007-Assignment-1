using System;
using System.Collections.Generic;
using System.Text.Json;
using System.IO;

namespace CE2007A1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            PlayerManager playerManager = new PlayerManager();
            playerManager.LoadPlayerFromFile();
            GameLibrary gameLibrary = new GameLibrary();
            while (true)
            {
                Console.WriteLine("Hi there!");
                Console.WriteLine("Welcome to the player management system!");
                Console.WriteLine("What would you like to do?");
                Console.WriteLine("1. Add a new player and record their gaming stats");
                Console.WriteLine("2. Update a current players stats");
                Console.WriteLine("3. View a current players stats");
                Console.WriteLine("4. Exit and save");
                string choice = Console.ReadLine();

                if (choice == "4")
                {
                    playerManager.SavePlayerToFile();
                    break;
                }

                else if (choice == "1")
                {
                    playerManager.AddPlayers(gameLibrary); //calls the block for adding players
                }
                else if (choice == "2")
                {
                    Console.WriteLine("Are you searching with Username or ID?");
                    string Search = Console.ReadLine();
                    Player FoundPlayer = null; //sets it to be empty to start
                    if (Search == "Username" || Search == "username") //if looking via username...
                    {
                        Console.WriteLine("Please enter the username you're looking for:");
                        string NameSearch = Console.ReadLine();
                        FoundPlayer = playerManager.FindPlayerByUsername(NameSearch);
                    }
                    else if (Search == "ID" || Search == "id")  //if looking via ID...
                    {
                        Console.WriteLine("Please enter the player ID:");
                        string IDSearch = Console.ReadLine();
                        if (int.TryParse(IDSearch, out int id))
                        {
                            FoundPlayer = playerManager.FindPlayerByID(id);
                        }
                        else
                        {
                            Console.WriteLine("That ID is not recognised. Please try again.");
                            continue;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid search type. Please type 'Username' or 'ID'.");
                        continue; 
                    }

                    // if found player is recognised..
                    if (FoundPlayer != null)  //found player is not null
                    {
                        Console.WriteLine($"Player found: {FoundPlayer.Username} ID: {FoundPlayer.ID}");
                        Console.WriteLine("Displaying player stats now . . .");
                        FoundPlayer.PlayerStats.RecordStats(gameLibrary); //calls record stats
                    }
                    else
                    {
                        Console.WriteLine("This player doesn't exist.");
                    }
                }

                else if (choice == "3")
                {
                    Console.WriteLine("What players stats do you want to see?");
                    //add viewing logic
                }
            }
        }

        class Player
        {
            public int ID { get; set; }
            public string Username { get; set; }
            public Stats PlayerStats { get; set; } //links to stats block

            public Player(int id, string username)
            {
                this.ID = id;
                this.Username = username;
                PlayerStats = new Stats();
            }
            public Player() { } //empty constructor for json loading

            public IUpdatableStats GetStatsInterface()
            {
                return PlayerStats;
            }
        }

        class PlayerManager
        {
            private List<Player> list = new List<Player>(); //only player manager can access and change this

            public PlayerManager()
            {
                //players will be added by AddPlayers()
                // any added players are for testing only
                list.Add(new Player(1, "Sarah"));
                list.Add(new Player(2, "Chris"));
            }

            public void AddPlayers(GameLibrary gameLibrary)
            {
                Console.WriteLine("Please enter the name of the player you wish to add");
                string Username = Console.ReadLine();
                Console.WriteLine("Please enter the ID you wish your player to have");
                string id = Console.ReadLine();
                if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(id))
                {
                    Console.WriteLine("You need to add a Username and an ID");
                    return;
                }
                bool isNumber = true;
                foreach (char c in id)
                {
                    if (c < '0' || c > '9')
                    {
                        isNumber = false;
                        break;
                    }
                }
                if (!isNumber)
                {
                    Console.WriteLine("ID must only contain numbers");
                    return; //stops crashing errors via parse by taking the user back to input id again
                }

                int ID = int.Parse(id);

                //check for duplicate ids
                foreach (var player in list)
                {
                    if (player.ID == ID)
                    {
                        Console.WriteLine($"A player with ID {id} already exists. Please try a different ID");
                        return;
                    }
                }

                //check for duplicate username
                foreach (var player in list)
                {
                    if (player.Username == Username)
                    {
                        Console.WriteLine($"A player with username {Username} already exists, Please try another one");
                        return;
                    }
                }

              
                Player newPlayer = new Player(ID, Username);
                list.Add(newPlayer);
                Console.WriteLine($"Added player {Username} with ID {ID} to list of players");

                Console.WriteLine("Would you like to update their gaming stats?");
                string UpdateChoice = Console.ReadLine();
                if (UpdateChoice == "yes" || UpdateChoice == "Yes" || UpdateChoice == "Y" || UpdateChoice == "y")
                {
                    newPlayer.PlayerStats.RecordStats(gameLibrary);
                }
                else
                {
                    Console.WriteLine("No worries! Feel free to record gaming stats later.");
                    Console.WriteLine("Taking you back to the main menu now..");
                    Console.WriteLine("Please press any key");
                    Console.ReadKey();
                    return;
                }
            }





            public Player FindPlayerByID(int id)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].ID == id)
                    {
                        return list[i];
                    }
                }
                return null;
            }

            public Player FindPlayerByUsername(string username)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].Username.ToLower() == username.ToLower())
                    {
                        return list[i];
                    }
                }
                return null;
            }

            public void SavePlayerToFile()
            {
                try
                {
                    string Json = JsonSerializer.Serialize(list, new JsonSerializerOptions { WriteIndented = true});
                    File.WriteAllText("Players.json", Json);
                    Console.WriteLine("Players have been saved to the file!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error saving players: " + ex.Message);
                }
            }

            public void LoadPlayerFromFile()
            {
                try
                {
                    if (!File.Exists ("Players.json"))
                    {
                        Console.WriteLine("You havn't saved any players yet! Go save some!");
                        return;
                    }

                    string Json = File.ReadAllText("Players.json");

                    if (string.IsNullOrWhiteSpace(Json))
                    {
                        Console.WriteLine("No saved players are found. Add players to begin");
                        return;
                    }
                    list = JsonSerializer.Deserialize<List<Player>>(Json);
                    Console.WriteLine("Players have been loaded from the file!");
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error loading players: " + e.Message);
                }

               
            }
        }

        class Stats : IUpdatableStats
        {
            public List<GameStats> GameStatsList { get; set; } = new List<GameStats>();
          


            public Stats() { }

            public IReadOnlyList<GameStats> GetGameStats()
            {
                return GameStatsList.AsReadOnly();
            }

            public void RecordStats(GameLibrary gameLibrary) //enables use of Game and GameGenre
            {
                if (!gameLibrary.HasGames())
                {
                    Console.WriteLine("Your game library is empty! You need to add atleast one game!");
                    gameLibrary.AddGame();
                }
                Game chosenGame = null;

                //loop until a game is chosen
                while (chosenGame == null)
                {
                    Console.WriteLine("Which game are you updating stats for? (please enter a game name)");
                    string gameNameInput = Console.ReadLine();

                    chosenGame = gameLibrary.FindGameByName(gameNameInput);
                    if (chosenGame == null)
                    {
                        Console.WriteLine("That game isn't in here! Add it instead!");
                        return;
                    }
                }

                GameStats gameStats = null;
                foreach (var gs in GameStatsList)
                {
                    if (gs.Game == chosenGame.GameName)
                    {
                        gameStats = gs;
                        break;
                    }
                }
                if (gameStats == null)
                {
                    gameStats = new GameStats(chosenGame);
                    GameStatsList.Add(gameStats);
                }

                Console.WriteLine($"How many hours have you played on {chosenGame.GameName}?");
                string HoursPlayedInput = Console.ReadLine();
                if (int.TryParse(HoursPlayedInput, out int hours) && hours >= 0)
                {
                    gameStats.HoursPlayed += hours;
                    Console.WriteLine($"Updated hours played for {chosenGame.GameName}: {gameStats.HoursPlayed}");
                }
                else
                {
                    Console.WriteLine("Sorry, you need to add a positive number");
                }

                Console.WriteLine($"Have you achieved a high score this time in {chosenGame.GameName}? \nPlease answer Yes or No");
                string isHighScore = Console.ReadLine();
                if (isHighScore == "yes" || isHighScore == "Yes" || isHighScore == "y" || isHighScore == "Y")
                    {
                    Console.WriteLine($"What is your new high score in {chosenGame.GameName}?");
                    string NewScoreInput = Console.ReadLine();
                    if (int.TryParse(NewScoreInput, out int newScore))
                    {
                        if (newScore > gameStats.HighScore)
                        {
                            gameStats.HighScore = newScore;
                            Console.WriteLine($"New high score logged into the system: {chosenGame.GameName}");
                        }
                        else
                        {
                            Console.WriteLine($"Your score wasn't higher than your existing one! \nCurrent high score to beat: {gameStats.HighScore}.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Thats not a valid number!");
                    }
                }
                else
                {
                    Console.WriteLine("Aww, that's a shame, better luck next time!");
                }
            }

            public void UpdateHighScore(Game selectedGame, int newScore)
            {
                GameStats gameStats = null;
                foreach (var gs in GameStatsList)
                {
                    if (gs.Game == selectedGame.GameName)
                    {
                        gameStats = gs;
                        break;
                    }
                    
                }
                if (gameStats != null && newScore > gameStats.HighScore)
                    gameStats.HighScore = newScore;
                
            }
            
            

            public void UpdateHoursPlayed(Game selectedGame, int newHours)
            {
                GameStats stats = null;
                foreach (var gs in GameStatsList)
                {
                    if (gs.Game == selectedGame.GameName)
                    {
                        stats = gs;
                        break;
                    }
                }
                if (stats != null && newHours>0)
                    stats.HoursPlayed += newHours;
                
                  
            }

            public int GetTotalPlayedHours()
            {
                int totalHours = 0;
                foreach (var gs in GameStatsList)
                {
                    totalHours += gs.HoursPlayed;
                }
                return totalHours;
            }

            public int GetTotalHighScore()
            {
                int totalHighScore = 0;
                foreach (var gs in GameStatsList)
                {
                    if (gs.HighScore > totalHighScore)
                        totalHighScore = gs.HighScore;
                }
                return totalHighScore;
            }
        }

        public class GameStats
        {
            public string Game { get; set; } //stores only Game not the whole object so json can read it
            public int HoursPlayed { get; set; }
            public int HighScore { get; set; }

            public GameStats() { } //for json
            public GameStats(Game game)
            {
                Game = game.GameName;
                HoursPlayed = 0;
                HighScore = 0;
            }
        }

        interface IUpdatableStats
        {
            void UpdateHighScore(Game selectedGame, int newScore);
            void UpdateHoursPlayed(Game selectedGame, int newHours);
            void RecordStats(GameLibrary gameLibrary);
        }

        public class Game
        {
            private static Random ranID = new Random();
            public int GameID { get; set; }
            public string GameName { get; set; }
            public string GameGenre { get; set; }

            public Game(string gameName, string GameGenre)
            {
                this.GameID = ranID.Next(0, 99);
                this.GameName = gameName;
                this.GameGenre = GameGenre;
            }

            public override string ToString()
            {
                return $"{GameName} (ID: {GameID}) Genre: {GameGenre}";
            }
        }

        class GameLibrary
        {
            private List<Game> games = new List<Game>();
            public bool HasGames()
            {
                return games.Count > 0;
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

        class Leaderboards
        {
            public List<Player> TopPlayersFromScore(List<Player> players)
            {
                return players;
            }

            public List<Player> TopActivePlayers(List<Player> players)
            {
                return players;
            }
        }
    }
}
