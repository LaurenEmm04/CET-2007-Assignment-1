using System;
using System.Collections.Generic;
using System.Text.Json;
using System.IO;

namespace CET2007A1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            PlayerManager playerManager = new PlayerManager();
            playerManager.LoadPlayerFromFile();  //loading player file
            GameLibrary gameLibrary = new GameLibrary();
            gameLibrary.LoadGameLibraryFromFile(); //loadingthe game library
            while (true)
            {
                Console.WriteLine("Hi there!");
                Console.WriteLine("Welcome to the player management system!");
                Console.WriteLine("What would you like to do?");
                Console.WriteLine("1. Add a new player and record their gaming stats");
                Console.WriteLine("2. Update a current players stats");
                Console.WriteLine("3. View a current players stats");
                Console.WriteLine("4. View and add games in your library");
                Console.WriteLine("5. Exit and save");
                string choice = Console.ReadLine();

                if (choice == "5")
                {
                    playerManager.SavePlayerToFile(); //saving player list
                    gameLibrary.SaveGameLibraryToFile(); //saving game library
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

                else if (choice == "4")
                {
                    Console.WriteLine("Showing all of the games in your game library");
                    gameLibrary.GameList();
                    while (true)
                    {
                        string AddGames;
                        Console.WriteLine("Would you like to add any new games?");
                        AddGames = Console.ReadLine();
                        if (AddGames == "Yes" || AddGames == "yes" || AddGames == "y" || AddGames == "Y")
                        {
                            gameLibrary.AddGame(); //promps for name and genre
                        }
                        else
                        {
                            Console.WriteLine("No new games will be added \n Returning to the main menu..");
                            Console.WriteLine("Please press any button to be redirected.");
                            Console.ReadKey();
                            break; //loop exit
                        }
                    }
                    
                    
                    
                }
            }
        }
    }
}
