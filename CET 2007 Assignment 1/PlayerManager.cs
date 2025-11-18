﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CET2007A1
{
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
            while (true)
            {
                Console.WriteLine("Please enter the name of the player you wish to add");
                string Username = Console.ReadLine();
                Console.WriteLine("Please enter the ID you wish your player to have");
                string id = Console.ReadLine();
                if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(id))
                {
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
                    Console.WriteLine("ID must only contain numbers");
                    continue; //stops crashing errors via parse by taking the user back to start of loop again
                }

                int ID = int.Parse(id);

                //check for duplicate ids

                bool DuplicateID = false;
                foreach (var player in list)
                {
                    if (player.ID == ID)
                    {
                        Console.WriteLine($"A player with ID {id} already exists. Please try a different ID");
                        DuplicateID = true;
                        break;
                    }
                }
                if (DuplicateID) continue; //if the ids a duplicate, go bacl to the start of the loop

                //check for duplicate username
                bool DuplicateUsername = false;
                foreach (var player in list)
                {
                    if (player.Username == Username)
                    {
                        Console.WriteLine($"A player with username {Username} already exists, Please try another one");
                        DuplicateUsername = true;
                        break;
                    }
                }
                if (DuplicateUsername) continue; //goes back to the start if duplicate

                //creating and adding players
                Player newPlayer = new Player(ID, Username);
                list.Add(newPlayer);
                Console.WriteLine($"Prepating to add player {Username} with ID {ID} to list of players");
                Console.WriteLine("Make sure to save and exit, otherwise they wont be added!");



                //asking to update stats
                Console.WriteLine("Would you like to update their gaming stats?");
                string UpdateChoice = Console.ReadLine();
                if (UpdateChoice == "yes" || UpdateChoice == "Yes" || UpdateChoice == "Y" || UpdateChoice == "y")
                {
                    newPlayer.GetStatsInterface().RecordStats(gameLibrary);
                }
                else
                {
                    Console.WriteLine("No worries! Feel free to record gaming stats later.");
                }
                Console.WriteLine("Taking you back to the main menu now..");
                Console.WriteLine("Remember to save before you exit!");
                Console.WriteLine("Please press any key");
                Console.ReadKey();
                break;
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

        public List<Player> GetAllPlayers()
        {
            return list;
        }

        public void SavePlayerToFile()
        {
            try
            {
                string Json = JsonSerializer.Serialize(list, new JsonSerializerOptions { WriteIndented = true });
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
                if (!File.Exists("Players.json"))
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
}