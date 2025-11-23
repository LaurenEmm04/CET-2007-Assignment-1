using System;
using System.Collections.Generic;
using System.Text.Json;
using System.IO;
using static CET2007A1.CustomExceptions;
using System.Linq.Expressions;
using Microsoft.SqlServer.Server;




namespace CET2007A1
{
    /// <summary>
    /// Program class. Holds all of the main
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// the main functions are here, such as the menu and 
        /// </summary>
        static void Main(string[] args)
        {
            PlayerManager playerManager = new PlayerManager();
            playerManager.LoadPlayerFromFile();  //loading player file
            GameLibrary gameLibrary = new GameLibrary();
            gameLibrary.LoadGameLibraryFromFile(); //loadingthe game library
            Leaderboard leaderboard = new Leaderboard(playerManager.GetAllPlayers(), gameLibrary);
            while (true)
            {
                try
                {
                    //Loading the menu
                    Console.WriteLine(Path.GetFullPath("Players.json"));
                    Logger.GetInstance().Log("Menu Loaded.");
                    Console.WriteLine("Hi there!");
                    Console.WriteLine("Welcome to the player management system!");
                    Console.WriteLine("What would you like to do?");
                    Console.WriteLine("1. Add a new player and record their gaming stats");
                    Console.WriteLine("2. Update a current players stats");
                    Console.WriteLine("3. View a current players stats");
                    Console.WriteLine("4. View and add games in your library");
                    Console.WriteLine("5. Show current leaderboard");
                    Console.WriteLine("6. Exit and save");
                    string choice = Console.ReadLine();
                    Console.Clear();

                    if (choice == "6")
                    {
                        Logger.GetInstance().Log("Option 6 chosen. Exiting and saving..");
                        playerManager.SavePlayerToFile(); //saving player list
                        gameLibrary.SaveGameLibraryToFile(); //saving game library
                        break;
                    }

                    else if (choice == "1")
                    {

                        Logger.GetInstance().Log("Option 1 chosen. Adding players..");
                        playerManager.AddPlayers(gameLibrary); //calls the block for adding players
                    }


                    else if (choice == "2")
                    {
                        Logger.GetInstance().Log("Option 2 chosen. Updating players stats..");
                        playerManager.UpdatePlayerStats(gameLibrary);  //calls the block for updating players
                    }



                    else if (choice == "3")
                    {
                        Logger.GetInstance().Log("Option 3 chosen. Viewing player stats..");
                        playerManager.ViewPlayerStats();

                    }


                    else if (choice == "4")
                    {
                        playerManager.DisplayGameLibrary(gameLibrary);
                        Logger.GetInstance().Log("Choice 4 chosen. Displaying game library..");
                    }



                    else if (choice == "5")
                    {
                        playerManager.DisplayLeaderboard(leaderboard);
                        Logger.GetInstance().Log("Displaying option 5. Leaderboard Options.");
                    }



                }
                catch (InvalidIDException ex)  //if id not a number
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    Logger.GetInstance().Log(ex.Message);
                }
                catch (DuplicatePlayerException ex)  //if player username/id already exists
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    Logger.GetInstance().Log(ex.Message);
                }
                catch (PlayerNotFoundException ex) //if searched for player doesnt exist
                {
                    Console.WriteLine(ex.Message);
                    Logger.GetInstance().Log(ex.Message);
                }
                catch (Exception ex)  //general exception
                {
                    Console.WriteLine("Unexpected error occured, returing to menu");
                    Logger.GetInstance().Log(ex.ToString());  //logs the exception found pulling all the info from it
                }

                
            }
        }
    }
}




