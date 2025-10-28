using System;
using System.Collections.Generic;
using System.Linq;

namespace CE2007A1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            PlayerManager playerManager = new PlayerManager();
            GameLibrary gameLibrary = new GameLibrary();
            while (true)
            {
                Console.WriteLine("Hi there!");
                Console.WriteLine("Welcome to the player management system!");
                Console.WriteLine("What would you like to do?");
                Console.WriteLine("1. Add a new player and record their gaming stats");
                Console.WriteLine("2. Update a current players stats");
                Console.WriteLine("3. View a current players stats");
                Console.WriteLine("4. Exit");
                string choice = Console.ReadLine();

                if (choice == "4")
                    break;

                else if (choice == "1")
                {
                    playerManager.AddPlayers(); //calls the block for adding players
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
            public IUpdatableStats PlayerStats { get; set; } //links to stats block

            public Player(int id, string username)
            {
                this.ID = id;
                this.Username = username;
                PlayerStats = new Stats();
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

            public void AddPlayers()
            {
                Console.WriteLine("Please enter the name of the player you wish to add");
                string Username = Console.ReadLine();
                Console.WriteLine("Please enter the ID you wish your player to have");
                string id = Console.ReadLine();
                if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(id))
                {
                    Console.WriteLine("You need to add a Username and an ID");
                }
                else if (!id.All(char.IsDigit))  //checks that its a number
                {
                    Console.WriteLine("The ID must contain numbers only");
                }
                else
                {
                    int ID = int.Parse(id);
                    Console.WriteLine($"Added player {Username} with ID {ID} to list of players");
                    list.Add(new Player(ID, Username));
                }
            }

            
            public Player FindPlayerByID(int id)
            {
                return list.FirstOrDefault(p => p.ID == id);
            }

            public Player FindPlayerByUsername(string username)
            {
                return list.FirstOrDefault(p => p.Username == username);
            }
        }

        class Stats : IUpdatableStats
        {
            private List<GameStats> GameStatsList = new List<GameStats>();

            public void RecordStats(GameLibrary gameLibrary) //enables use of Game and GameGenre
            {
                Console.WriteLine("Which game are you updating stats for? (please enter a game name)");
                string gameNameInput = Console.ReadLine();

                Game selectedGame = gameLibrary.FindGameByName(gameNameInput);
                if (selectedGame == null)
                {
                    Console.WriteLine("That game isn't in here! Add it instead!");
                    return;
                }

                GameStats gameStats = GameStatsList.FirstOrDefault(gs => gs.Game == selectedGame);
                if (gameStats == null)
                {
                    gameStats = new GameStats(selectedGame);
                    GameStatsList.Add(gameStats);
                }

                Console.WriteLine($"How many hours have you played on {selectedGame.GameName}?");
                string HoursPlayedInput = Console.ReadLine();
                if (int.TryParse(HoursPlayedInput, out int hours) && hours >= 0)
                {
                    gameStats.HoursPlayed += hours;
                    Console.WriteLine($"Updated hours played for {selectedGame.GameName}: {gameStats.HoursPlayed}");
                }
                else
                {
                    Console.WriteLine("Sorry, you need to add a positive number");
                }

                Console.WriteLine($"Have you achieved a high score this time in {selectedGame.GameName}? \nPlease answer Yes or No");
                string isHighScore = Console.ReadLine();
                if (isHighScore == "yes" || isHighScore == "Yes" || isHighScore == "y" || isHighScore == "Y")
                    {
                    Console.WriteLine($"What is your new high score in {selectedGame.GameName}?");
                    string NewScoreInput = Console.ReadLine();
                    if (int.TryParse(NewScoreInput, out int newScore))
                    {
                        if (newScore > gameStats.HighScore)
                        {
                            gameStats.HighScore = newScore;
                            Console.WriteLine($"New high score logged into the system: {selectedGame.GameName}");
                        }
                        else
                        {
                            Console.WriteLine($"Your score wasn't higher than your existing one! Current high score to beat: {gameStats.HighScore}.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("That’s not a valid number!");
                    }
                }
                else
                {
                    Console.WriteLine("Aww, that's a shame, better luck next time!");
                }
            }

            private class GameStats
            {
                public Game Game { get; private set; }
                public int HoursPlayed { get; set; }
                public int HighScore { get; set; }

                public GameStats(Game game)
                {
                    Game = game;
                    HoursPlayed = 0;
                    HighScore = 0;
                }
            }

            public void UpdateHighScore(Game selectedGame, int newScore) 
            {
                var gameStats = GameStatsList.FirstOrDefault(gs => gs.Game == selectedGame);  //finds existing game stats object for the game instead of making a new one every time 
                if (gameStats != null && newScore>gameStats.HighScore)
                    gameStats.HighScore = newScore;
            }

            public void UpdateHoursPlayed(Game selectedGame, int newHours)
            {
                var gameStats = GameStatsList.FirstOrDefault(gs => gs.Game == selectedGame);
                if (gameStats != null && newHours > 0)
                    gameStats.HoursPlayed += newHours;
            }
        }

        interface IUpdatableStats
        {
            void UpdateHighScore(int newscore);
            void UpdateHoursPlayed(int newhours);
            void RecordStats(GameLibrary gameLibrary);
        }

        class Game
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
            List<Game> games = new List<Game>();

            public void AddGame(Game game = null)
            {
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
                Game NewGame = new Game(GName, GGenre);
                games.Add(NewGame);
                Console.WriteLine("You have added a new game to your game library!");
            }

            public Game FindGameByID(int GID)
            {
                return games.FirstOrDefault(g => g.GameID == GID);
            }

            public Game FindGameByName(string GName)
            {
                return games.FirstOrDefault(g => g.GameName == GName);
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
