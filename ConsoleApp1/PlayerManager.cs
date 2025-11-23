using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using static CET2007A1.CustomExceptions;
using Newtonsoft.Json;

namespace CET2007A1
{
    /// <summary>
    /// Enables the adding, searching via ID and Username, saving and loading to file of Player information
    /// </summary>
    class PlayerManager
    {
        private List<Player> list = new List<Player>(); //only player manager can add or change this to keep it secure

        public PlayerManager()
        {
            //players will be added by AddPlayers()
            // any players added here are for testing/example only
            list.Add(new Player(1, "Sarah"));
            list.Add(new Player(2, "Chris"));
        }


        //menu options shown below


        //---- MENU OPTION 1 ---------------------------------------------------------------------------------
        /// <summary>
        /// Adding players function with links to RecordStats later on
        /// </summary>


        public void AddPlayers(GameLibrary gameLibrary)
        {
            while (true) //loops until outcome reached
            {
                try
                {
                    Logger.GetInstance().Log("Beginning to add players");
                    Console.WriteLine("Please enter the name of the player you wish to add"); //logs actions, inputs desired username and ID
                    string Username = Console.ReadLine();
                    Console.WriteLine("Please enter the ID you wish your player to have");
                    string id = Console.ReadLine();
                    if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(id)) //if either username or ID is empty..
                    {
                        Logger.GetInstance().Log("Usernmae and ID has not been entereed. Retrying.");
                        Console.WriteLine("You need to add a Username and an ID");
                        continue; //back to start of loop to reenter usernme and ID
                    }

                    bool isNumber = true;  //checking if id is a number. starts as true
                    foreach (char c in id)
                    {
                        if (c < '0' || c > '9')//goes through each character in ID and makes sure its a number
                        {
                            isNumber = false; //if they dont match between 0-9 then its marked as false
                            break;
                        }
                    }
                    if (!isNumber) //if the input was marked as false
                    {
                        Logger.GetInstance().Log("ID is not a number. Retrying.");
                        throw new InvalidIDException("ID must be numeric.");  //logs and throws exception

                    }

                    int ID = int.Parse(id);  //converts string to int

                    //check for duplicate ids

                    
                    foreach (var player in list)  //checks each player in list
                    {
                        if (player.ID == ID)  //if the id matches one a player already has..
                        {
                            Logger.GetInstance().Log($"A player with ID: {id} already exists. Retrying.");
                            throw new DuplicatePlayerException($"A player with ID {id} already exists. Please try a different ID");


                        }
                    }
                    //if the ids a duplicate, go bacl to the start of the loop



                    //check for duplicate username
                    bool DuplicateUsername = false;
                    foreach (var player in list)  //checks each player in list
                    {
                        if (player.Username == Username)  //if username is already used in player list..
                        {
                            Logger.GetInstance().Log($"A player with username: {Username} already exists. Retrying.");
                            Console.WriteLine($"A player with username {Username} already exists, Please try another one");
                            DuplicateUsername = true;  //sets DuplicateUsername to true, breaks loop and returns back to reinputting info
                            break;
                        }
                    }
                    if (DuplicateUsername) continue; //goes back to the start if duplicate


                    //creating and adding players 
                    Player newPlayer = PlayerFactory.CreatePlayer(Username, ID);  //uses player factory for info on what to include
                    list.Add(newPlayer);
                    Console.Clear();
                    Logger.GetInstance().Log($"Preparing to add {Username} with ID: {ID} to the list...");
                    Console.WriteLine($"Prepating to add player {Username} with ID {ID} to list of players");
                    Console.WriteLine("Make sure to save and exit, otherwise they wont be added!");
                    Logger.GetInstance().Log("Player creation complete.");




                    //asking to update stats
                    Logger.GetInstance().Log("Beginning to update stats.");
                    Console.WriteLine("Would you like to update their gaming stats?");
                    string UpdateChoice = Console.ReadLine();
                    if (UpdateChoice == "yes" || UpdateChoice == "Yes" || UpdateChoice == "Y" || UpdateChoice == "y")
                    {
                        Logger.GetInstance().Log($"Stats are beginning to be updated for {Username} | {ID}");
                        newPlayer.GetStatsInterface().RecordStats(gameLibrary); //jumps to record stats in stats, hidden by encapsulation so haas to go through the interface and stats interface
                    }
                    else
                    { //if something other is chosen then stats will not be updated.
                        Logger.GetInstance().Log($"Stats are not being updated for {Username} | {ID}");
                        Console.WriteLine("No worries! Feel free to record gaming stats later.");
                    }
                    Console.WriteLine("Taking you back to the main menu now..");
                    Console.WriteLine("Remember to save before you exit!");
                    Console.WriteLine("Please press any key");
                    Logger.GetInstance().Log("Returning to menu..");
                    Console.ReadKey();
                    Console.Clear();
                    break;
                }
                catch (InvalidIDException ex) //if ID is not numeric
                {
                    Console.WriteLine(ex.Message);
                    Logger.GetInstance().Log(ex.Message);
                }
                catch (DuplicatePlayerException ex)  //if player ID is already in the system
                {
                    Console.WriteLine(ex.Message);
                    Logger.GetInstance().Log(ex.Message);
                }
            }



        }
        //------------------------------------------------------------------------------------------------------


        //------     MENU OPTION 2    -------------------------------------------------------------------------

        /// <summary>
        /// updates the current players' stats
        /// </summary>

        public void UpdatePlayerStats(GameLibrary gameLibrary)
        {

            Console.WriteLine("Are you searching with Username or ID?");
            string Search = Console.ReadLine();
            Player FoundPlayer = null; //sets it to be empty to start
            if (Search == "Username" || Search == "username") //if looking via username...
            {
                Logger.GetInstance().Log("Searching via username.");
                Console.WriteLine("Please enter the username you're looking for:");
                string NameSearch = Console.ReadLine();
                FoundPlayer = FindPlayerByUsername(NameSearch);
            }
            else if (Search == "ID" || Search == "id")  //if looking via ID...
            {
                Logger.GetInstance().Log("Searching via ID.");
                Console.WriteLine("Please enter the player ID:");
                string IDSearch = Console.ReadLine();
                if (int.TryParse(IDSearch, out int id))  //turns string to int
                {
                    FoundPlayer = FindPlayerByID(id); //if the player your looking fot matches one found in the list FoundPlayer is recognised.
                }
                else //if it doesnt..
                {
                    Logger.GetInstance().Log("Unrecognised ID");
                    Console.WriteLine("That ID is not recognised. Please try again.");
                    throw new InvalidIDException("Please make sure to enter a number for the ID");
                }
            }
            else //other error
            {
                Logger.GetInstance().Log("Invalid search type.");
                Console.WriteLine("Invalid search type. Please type 'Username' or 'ID'.");
                return;
            }

            // if found player is recognised..
            if (FoundPlayer != null)  //found player is not null
            {
                Console.Clear();
                Logger.GetInstance().Log($"Player: {FoundPlayer.Username} | ID: {FoundPlayer.ID} found."); //logs found player
                Console.WriteLine($"Player found: {FoundPlayer.Username} ID: {FoundPlayer.ID}");
                FoundPlayer.GetStatsInterface().RecordStats(gameLibrary); //calls record stats to update stats
            }
            else
            {
                Logger.GetInstance().Log("Player doesn't exist.");
                Console.WriteLine("This player doesn't exist.");
                throw new PlayerNotFoundException("This player was not found, try and add them instead");
            }

        }

        //----------------------------------------------------------------------------------------------------------------


        //------      MENU OPTION 3     ----------------------------------------------------------------------------

        /// <summary>
        /// viewing a players stats
        /// </summary>

        public void ViewPlayerStats()
        {
            Console.WriteLine("Are you searching with Username or ID?");
            string search = Console.ReadLine();
            Player FoundPlayer = null; //player has not been found is set as default 
            if (search == "Username" || search == "username")
            {
                Console.WriteLine("Please enter the Username your looking for");
                string UsernameSearch = Console.ReadLine();
                FoundPlayer = FindPlayerByUsername(UsernameSearch); //if name searched matches a name in the list

            }
            else if (search == "ID" || search == "id")
            {
                Console.WriteLine("Please enter the ID your looking for");
                string IDSearch = Console.ReadLine();

                if (int.TryParse(IDSearch, out int id))
                {
                    FoundPlayer = FindPlayerByID(id); //if id searched matches id in the list..
                }
                else
                {
                    Console.WriteLine("That isn't recognised. Please enter a username or ID");
                    Logger.GetInstance().Log("Search input not recognised");

                }
            }

            if (FoundPlayer != null) //if found player recognised
            {
                Console.Clear();
                Logger.GetInstance().Log($"Player recognised as {FoundPlayer.Username} - {FoundPlayer.ID}");
                Console.WriteLine($"Player found: {FoundPlayer.Username} (ID: {FoundPlayer.ID})"); //logs, notifies and displays player stats
                Console.WriteLine("Displaying player stats now..");
                Logger.GetInstance().Log("Displaying stats for the player..");

                FoundPlayer.GetStatsInterface().DisplayStats(); //displays the stats of the found player
            }
            else
            {
                Console.WriteLine("This player doesnt exist"); //name or id inputted does not match
                Logger.GetInstance().Log("Player does not exist.");
                throw new PlayerNotFoundException("This player is not found, you should add them instead!");
            }
            Console.WriteLine("\nPress any key to return to the menu");
            Logger.GetInstance().Log("Returning to menu..");
            Console.ReadKey();
            Console.Clear();
        }

        //----------------------------------------------------------------------------------------------------

        // -----   MENU OPTION 4  ---------------------------------------------------------------------------------


        /// <summary>
        /// Displaying the game library
        /// </summary>

        public void DisplayGameLibrary(GameLibrary gameLibrary)
        {
            Console.WriteLine("Showing all of the games in your game library");
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
                    Console.Clear();
                    break; //loop exit
                }
            }
        }

        //---------------------------------------------------------------------------------------------------

        //--------  MENU OPITON 5 ---------------------------------------------------------------------------

        /// <summary>
        /// Displaying leaderboard. Options for unfiltered leaderboards, and filtered by Hours Played and High Score.
        /// </summary>
        public void DisplayLeaderboard(Leaderboard leaderboard)
        {
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
            Console.Clear();
        }

        //------------------------------------------------------------------------------------------------







        //linear searching, going through individual players
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


        //linear searching, goes through individual players
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

        /// <summary>
        /// Returns every player in the system
        /// </summary>
        public List<Player> GetAllPlayers()
        {
            return list;
        }

        /// <summary>
        /// Saves the newly created player to Players.json
        /// </summary>
        public void SavePlayerToFile()
        {
            try
            {
                string jsonData = JsonConvert.SerializeObject(list, Formatting.Indented); //serialise/writes the list to json
                File.WriteAllText("Players.json", jsonData); //writes into this file
                Console.WriteLine("Players have been saved to the file!"); //console message for confirmation
            }
            catch (Exception ex) //error notice
            {
                Console.WriteLine("Error saving players: " + ex.Message);
            }
        }

        /// <summary>
        /// Loads previously saved players when the system is next used.
        /// </summary>
        public void LoadPlayerFromFile()
        {
            try
            {
                if (!File.Exists("Players.json")) //if the file doesnt exist
                {
                    Console.WriteLine("You havn't saved any players yet! Go save some!"); //says to go save some players so the file can be created
                    return;
                }

                string jsonData = File.ReadAllText("Players.json");

                if (string.IsNullOrWhiteSpace(jsonData)) //if the file is empty but there
                {
                    Console.WriteLine("No saved players are found. Add players to begin");
                    return;
                }
                list = JsonConvert.DeserializeObject<List<Player>>(jsonData) ?? new List<Player>();  //deserialise, if unable to, make a new list instead of NULL
                Console.WriteLine("Players have been loaded from the file!"); //if everything is ok, file is loaded and ready to be used successfully
            }
            catch (Exception ex)  //file loading error notice with logging
            {
                Logger.GetInstance().Log(ex.Message);
                Console.WriteLine("Error loading players: " + ex.Message);
                Console.WriteLine("Starting a new list:");
                Logger.GetInstance().Log("Failed to load Player log. Making a new one..");
                list = new List<Player>();  //creates a new list to stop crashes so the user can keep going
            }


        }


    }
}