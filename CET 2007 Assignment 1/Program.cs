using System;
using System.Collections.Generic;
namespace CE2007A1
{
    internal class Program
    {

        static void Main(string[] args)
        {

            // declare the list
            List<Player> players = new List<Player>();

            // Add a player to the list (manual)
            players.Add(new Player(1, "Sarah"));
            players.Add(new Player(2, "Chris"));

            // Access the list (e.g., iterate or search)
            foreach (var player in players)
            {
                // do something with addin player
            }

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
                    }
                    
                    //logic to add Username and ID to list
                    
                    
                    
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
    }
}