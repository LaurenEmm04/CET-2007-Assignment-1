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
            Leaderboard leaderboard = new Leaderboard(playerManager.GetAllPlayers(), gameLibrary);
            while (true)
            {
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
                    Logger.GetInstance().Log("Option 2 chosen. Searching for players..");
                    Console.WriteLine("Are you searching with Username or ID?");
                    string Search = Console.ReadLine();
                    Player FoundPlayer = null; //sets it to be empty to start
                    if (Search == "Username" || Search == "username") //if looking via username...
                    {
                        Logger.GetInstance().Log("Searching via username.");
                        Console.WriteLine("Please enter the username you're looking for:");
                        string NameSearch = Console.ReadLine();
                        FoundPlayer = playerManager.FindPlayerByUsername(NameSearch);
                    }
                    else if (Search == "ID" || Search == "id")  //if looking via ID...
                    {
                        Logger.GetInstance().Log("Searching via ID.");
                        Console.WriteLine("Please enter the player ID:");
                        string IDSearch = Console.ReadLine();
                        if (int.TryParse(IDSearch, out int id))
                        {
                            FoundPlayer = playerManager.FindPlayerByID(id);
                        }
                        else
                        {
                            Logger.GetInstance().Log("Unrecognised ID");
                            Console.WriteLine("That ID is not recognised. Please try again.");
                            continue;
                        }
                    }
                    else
                    {
                        Logger.GetInstance().Log("Invalid search type.");
                        Console.WriteLine("Invalid search type. Please type 'Username' or 'ID'.");
                        continue;
                    }

                    // if found player is recognised..
                    if (FoundPlayer != null)  //found player is not null
                    {
                        Logger.GetInstance().Log($"Player: {FoundPlayer.Username} | ID: {FoundPlayer.ID} found.");
                        Console.WriteLine($"Player found: {FoundPlayer.Username} ID: {FoundPlayer.ID}");
                        Logger.GetInstance().Log("Asking if they would like to record stats.");
                        FoundPlayer.GetStatsInterface().RecordStats(gameLibrary); //calls record stats
                    }
                    else
                    {
                        Logger.GetInstance().Log("Player doesn't exist.");
                        Console.WriteLine("This player doesn't exist.");
                    }
                }

                else if (choice == "3")
                {
                    Logger.GetInstance().Log("Option 3 chosen.");
                    Console.WriteLine("Are you searching with Username or ID?");
                    string search = Console.ReadLine();
                    Player FoundPlayer = null; //player has not been found
                    if (search == "Username" || search == "username")
                    {
                        Console.WriteLine("Please enter the Username your looking for");
                        string UsernameSearch = Console.ReadLine();
                        FoundPlayer = playerManager.FindPlayerByUsername(UsernameSearch);

                    }
                    else if (search == "ID" || search == "id")
                    {
                        Console.WriteLine("Please enter the ID your looking for");
                        string IDSearch = Console.ReadLine();

                        if (int.TryParse(IDSearch, out int id))
                        {
                            FoundPlayer = playerManager.FindPlayerByID(id);
                        }
                        else
                        {
                            Console.WriteLine("That isn't recognised. Please enter a username or ID");
                            Logger.GetInstance().Log("Search input not recognised");
                            continue;
                        }
                    }


                    if (FoundPlayer != null)
                    {
                        Logger.GetInstance().Log($"Player recognised as {FoundPlayer.Username} - {FoundPlayer.ID}");
                        Console.WriteLine($"Player found: {FoundPlayer.Username} (ID: {FoundPlayer.ID})");
                        Console.WriteLine("Displaying player stats now..");
                        Logger.GetInstance().Log("Displaying stats for the player..");

                        FoundPlayer.GetStatsInterface().DisplayStats(); //displays the stats of the found player
                    }
                    else
                    {
                        Console.WriteLine("This player doesnt exist");
                        Logger.GetInstance().Log("Player does not exist.");
                    }
                    Console.WriteLine("\nPress any key to return to the menu");
                    Logger.GetInstance().Log("Returning to menu..");
                    Console.ReadKey();

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



                else if (choice == "5")  //optionally sort per game
                {
                    Logger.GetInstance().Log("Displaying option 5. Leaderboard Options.");
                    Console.WriteLine("Leaderboard options:");
                    Console.WriteLine("1. Sort by High Score");
                    Console.WriteLine("2. Sort by Hours Played");
                    Console.WriteLine("What would you like to sort the leaderboard by?");
                    string SortChoice = Console.ReadLine();

                    if (SortChoice == "High Score" || SortChoice == "high score" || SortChoice =="1")
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
        }
    }
}




