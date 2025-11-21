using System;
using System.Collections.Generic;
using System.Text.Json;
using System.IO;
using static CET2007A1.CustomExceptions;
using System.Linq.Expressions;




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
        /// <param name="args"></param>
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
                        Logger.GetInstance().Log("Choice 4 chosen");
                        Console.WriteLine("Showing all of the games in your game library");
                        Logger.GetInstance().Log("Displaying game library..");
                        gameLibrary.GameList();
                        while (true)
                        {
                            string AddGames;
                            Console.WriteLine("Would you like to add any new games?");
                            Logger.GetInstance().Log("Asking if they want to add new games.");
                            AddGames = Console.ReadLine();
                            if (AddGames == "Yes" || AddGames == "yes" || AddGames == "y" || AddGames == "Y")
                            {
                                Logger.GetInstance().Log("Adding games to library.");
                                gameLibrary.AddGame(); //promps for name and genre
                            }
                            else
                            {
                                Logger.GetInstance().Log("No games will be added to the library.");
                                Console.WriteLine("No new games will be added \n Returning to the main menu..");
                                Console.WriteLine("Please press any button to be redirected.");
                                Logger.GetInstance().Log("Returning to the menu..");
                                Console.ReadKey();
                                break; //loop exit
                            }
                        }
                    }



                    else if (choice == "5")
                    {
                        Logger.GetInstance().Log("Displaying option 5. Leaderboard Options.");
                        Console.WriteLine("Leaderboard options:");
                        Console.WriteLine("1. Sort by High Score");
                        Console.WriteLine("2. Sort by Hours Played");
                        Console.WriteLine("What would you like to sort the leaderboard by?");
                        string SortChoice = Console.ReadLine();

                        if (SortChoice == "High Score" || SortChoice == "high score" || SortChoice == "1")
                        {
                            Logger.GetInstance().Log("Sorting leaderboard by High Score");
                            Console.WriteLine("Loading leaderboard sorted by high score..");
                            leaderboard.SortByScore();
                        }
                        else if (SortChoice == "Hours Played" || SortChoice == "hours played" || SortChoice == "2")
                        {
                            Logger.GetInstance().Log("Sorting leaderboard by Hours Played");
                            Console.WriteLine("Leaderboard sorted by hours played");
                            leaderboard.SortByHoursPlayed();
                        }
                        else
                        {
                            Console.WriteLine("Incorrect input. Please sort by either High Score (1) or Hours Played (2)");
                            return;
                        }
                        Console.WriteLine("Would you like to filter results to a specific game?");
                        string FilteredChoice = Console.ReadLine();
                        if (FilteredChoice == "Yes" || FilteredChoice == "yes")
                        {
                            Console.WriteLine("Please enter the game name");
                            string FilteredGame = Console.ReadLine();
                            Logger.GetInstance().Log($"Filtering the leaderboard by {FilteredGame}");
                            leaderboard.ShowFilteredLeaderboard(FilteredGame);
                        }
                        else
                        {
                            Logger.GetInstance().Log("Showing unfiltered leaderboard..");
                            leaderboard.ShowLeaderboard();
                        }
                        Console.WriteLine("Please press any key to return to the menu.");
                        Console.ReadKey();
                        Logger.GetInstance().Log("Returning to menu..");
                    }
                }
                catch (InvalidIDException ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    Logger.GetInstance().Log(ex.Message);
                }
                catch (DuplicatePlayerException ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    Logger.GetInstance().Log(ex.Message);
                }
                catch (PlayerNotFoundException ex)
                {
                    Console.WriteLine(ex.Message);
                    Logger.GetInstance().Log(ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Unexpected error occured, returing to menu");
                    Logger.GetInstance().Log(ex.ToString());
                }


            }
        }
    }
}




