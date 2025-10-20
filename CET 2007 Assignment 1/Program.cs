using System;
using System.Collections.Generic;
namespace CE2007A1
{
    internal class Program
    {

        static void Main(string[] args)
        {
            PlayerManager playerManager = new PlayerManager();
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
                    //updating logic
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
                PlayerStats = new Stats(0, 0);  //blank values when players are made
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
                else if (!id.All(char.IsDigit))  //checks that its a number. info found at https://www.geeksforgeeks.org/c-sharp/c-sharp-char-isdigit-method/
                {
                    Console.WriteLine("The ID must contain numbers only");
                }
                else
                {
                    int ID = int.Parse(id); //converts string ID to int ID for easier handling
                    Console.WriteLine($"Added player {Username} with ID {ID} to list of players");
                    players.Add(new Player(ID, Username)); // user inputted player is added to the list
                }



            }
         
        }


        class Stats : IUpdatableStats
        {
            public int HoursPlayed { get; private set; }
            public int HighScore { get; private set; }

            public Stats(int hoursPlayed, int highScore)
            {
                this.HoursPlayed = hoursPlayed;
                this.HighScore = highScore;
            }

            public void UpdateHighScore(int NewScore)
            {
                if (NewScore > HighScore)
                    HighScore = NewScore;
            }


            public void UpdateHoursPlayed(int NewHours)
            {
                if (NewHours > 0)
                {
                    HoursPlayed += NewHours;
                }
            }
        }

        interface IUpdatableStats
        {
            void UpdateHighScore(int newscore);
            void UpdateHoursPlayed(int newhours);
        }

        class Game
        {
            public int GameID { get; set; }
            public string GameName { get; set; }
            public string GameGenre { get; set; }

            public Game(int  gameID, string gameName, string GameGenre)
            {
                this.GameID = gameID;
                this.GameName = gameName;
                this.GameGenre = GameGenre;

            }
        }
        class GameLibrary
        {
            List<Game> games = new List<Game>();

            public void AddGame(Game game) 
            {
                //game logic in here
            }
            public Game FindGameByID(int id)
            {
                //search logic
            }
            public void RemoveGame(Game game)
            {
                games.Remove(game); //maybe??
            }

            class Leaderboards
            {
                public List<Player> TopPlayersFromScore(List<Player> players) 
                {
                    //sort player by high score
                }
                public List<Player> TopActivePlayers(List<Player> players)
                {
                    //sort player by top activity
                }
            }
}