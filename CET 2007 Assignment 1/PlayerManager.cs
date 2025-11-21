﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static CET2007A1.CustomExceptions;

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


        /// <summary>
        /// Menu options shown below
        /// Asks for Username and ID with validation and try/catch. Asks if user would like to update stats, if yes, redirects to there. If no, they are redirected to the menu.
        /// Logs interactions as usual to Log.txt
        /// </summary>
        /// 

        //---- MENU OPTION 1 ---------------------------------------------------------------------------------
        public void AddPlayers(GameLibrary gameLibrary)
        {
            while (true)
            {
                try
                {
                    Logger.GetInstance().Log("Beginning to add players");
                    Console.WriteLine("Please enter the name of the player you wish to add");
                    string Username = Console.ReadLine();
                    Console.WriteLine("Please enter the ID you wish your player to have");
                    string id = Console.ReadLine();
                    if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(id))
                    {
                        Logger.GetInstance().Log("Usernmae and ID has not been entereed. Retrying.");
                        Console.WriteLine("You need to add a Username and an ID");
                        continue; //back to start of loop to reenter
                    }

                    bool isNumber = true;  //checking if id is a number
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
                        Logger.GetInstance().Log("ID is not a number. Retrying.");
                        throw new InvalidIDException("ID must be numeric.");
                        //stops crashing errors via parse by taking the user back to start of loop again
                    }

                    int ID = int.Parse(id);

                    //check for duplicate ids

                    bool DuplicateID = false;
                    foreach (var player in list)
                    {
                        if (player.ID == ID)
                        {
                            Logger.GetInstance().Log($"A player with ID: {id} already exists. Retrying.");
                            throw new DuplicatePlayerException($"A player with ID {id} already exists. Please try a different ID");


                        }
                    }
                    if (DuplicateID) continue; //if the ids a duplicate, go bacl to the start of the loop

                    //check for duplicate username
                    bool DuplicateUsername = false;
                    foreach (var player in list)
                    {
                        if (player.Username == Username)
                        {
                            Logger.GetInstance().Log($"A player with username: {Username} already exists. Retrying.");
                            Console.WriteLine($"A player with username {Username} already exists, Please try another one");
                            DuplicateUsername = true;
                            break;
                        }
                    }
                    if (DuplicateUsername) continue; //goes back to the start if duplicate


                    //creating and adding players
                    Player newPlayer = PlayerFactory.CreatePlayer(Username, ID);
                    list.Add(newPlayer);
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
                        newPlayer.GetStatsInterface().RecordStats(gameLibrary);
                    }
                    else
                    {
                        Logger.GetInstance().Log($"Stats are not being updated for {Username} | {ID}");
                        Console.WriteLine("No worries! Feel free to record gaming stats later.");
                    }
                    Console.WriteLine("Taking you back to the main menu now..");
                    Console.WriteLine("Remember to save before you exit!");
                    Console.WriteLine("Please press any key");
                    Logger.GetInstance().Log("Returning to menu..");
                    Console.ReadKey();
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
                if (int.TryParse(IDSearch, out int id))
                {
                    FoundPlayer = FindPlayerByID(id);
                }
                else
                {
                    Logger.GetInstance().Log("Unrecognised ID");
                    Console.WriteLine("That ID is not recognised. Please try again.");
                    throw new InvalidIDException("Please make sure to enter a number for the ID");
                }
            }
            else
            {
                Logger.GetInstance().Log("Invalid search type.");
                Console.WriteLine("Invalid search type. Please type 'Username' or 'ID'.");
                return;
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
                throw new PlayerNotFoundException("This player was not found, try and add them instead");
            }

        }

        //----------------------------------------------------------------------------------------------------------------


        //------      MENU OPTION 3     ----------------------------------------------------------------------------

   
        public void ViewPlayerStats()
        {
            Console.WriteLine("Are you searching with Username or ID?");
            string search = Console.ReadLine();
            Player FoundPlayer = null; //player has not been found
            if (search == "Username" || search == "username")
            {
                Console.WriteLine("Please enter the Username your looking for");
                string UsernameSearch = Console.ReadLine();
                FoundPlayer = FindPlayerByUsername(UsernameSearch);

            }
            else if (search == "ID" || search == "id")
            {
                Console.WriteLine("Please enter the ID your looking for");
                string IDSearch = Console.ReadLine();

                if (int.TryParse(IDSearch, out int id))
                {
                    FoundPlayer = FindPlayerByID(id);
                }
                else
                {
                    Console.WriteLine("That isn't recognised. Please enter a username or ID");
                    Logger.GetInstance().Log("Search input not recognised");
                    
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
                throw new PlayerNotFoundException("This player is not found, you should add them instead!");
            }
            Console.WriteLine("\nPress any key to return to the menu");
            Logger.GetInstance().Log("Returning to menu..");
            Console.ReadKey();
        }

        //----------------------------------------------------------------------------------------------------














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
                string Json = JsonSerializer.Serialize(list, new JsonSerializerOptions { WriteIndented = true }); //serialise/writes the list to json
                File.WriteAllText("Players.json", Json); //writes into this file
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

                string Json = File.ReadAllText("Players.json"); 

                if (string.IsNullOrWhiteSpace(Json)) //if the file is empty but there
                {
                    Console.WriteLine("No saved players are found. Add players to begin");
                    return;
                }
                list = JsonSerializer.Deserialize<List<Player>>(Json); 
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