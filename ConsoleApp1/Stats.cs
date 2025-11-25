using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace CET2007A1
{
    /// <summary>
    /// Stats uses the 
    /// </summary>
    public class Stats : IUpdatableStats
    {
        [JsonProperty]
        private List<GameStats> GameStatsList = new List<GameStats>();




        public Stats() { }

        public IReadOnlyList<GameStats> GetGameStats()
        {
            return GameStatsList.AsReadOnly();
        }
        //records stats
        public void RecordStats(GameLibrary gameLibrary) //enables use of Game and GameGenre
        {
            Game chosenGame = null;

            //loop until a game is chosen
            while (chosenGame == null) //loopd until player enters vaild game
            {
                Console.WriteLine("Showing all games in your game library..");
                gameLibrary.GameList();
                Console.WriteLine("Which game are you updating stats for? (please enter a game name or ID)");
                string gameInput = Console.ReadLine();
                if (int.TryParse(gameInput, out int gameID)) //check if input is a number
                {
                    chosenGame = gameLibrary.FindGameByID(gameID); //searches via ID first
                }
                else
                {
                    chosenGame = gameLibrary.FindGameByName(gameInput); //then searches via name if no id was entered
                }

                if (chosenGame == null) //if game doesnt exist
                {
                    Console.WriteLine("That game isn't in here! Add it instead!");
                    return;
                }
            }

            GameStats gameStats = null; //declares gamestats variable and calls it null to hold stats for this game
            foreach (var gs in GameStatsList) //loops through each gamestats object 
            {
                if (gs.Game == chosenGame.GameName) //if the game matches what the user put in
                {
                    gameStats = gs; //if yes assigns to gamestats
                    break; //stops search
                }
            }
            if (gameStats == null) //if no stats exist yet creates new gamestats object
            {
                gameStats = new GameStats(chosenGame); //creates new stats
                GameStatsList.Add(gameStats); //adds to the  list
            }

            Console.WriteLine($"How many hours have you played on {chosenGame.GameName}?");
            string HoursPlayedInput = Console.ReadLine();
            if (int.TryParse(HoursPlayedInput, out int hours) && hours >= 0) //converts user input to int. makes sure its not negative
            {
                gameStats.HoursPlayed += hours; //adds hours to existing hours played
                Console.WriteLine($"Updated hours played for {chosenGame.GameName}: {gameStats.HoursPlayed}");
            }
            else
            {
                Console.WriteLine("Sorry, you need to add a positive number");
            }

            Console.WriteLine($"Have you achieved a high score this time in {chosenGame.GameName}? \nPlease answer Yes or No");
            string isHighScore = Console.ReadLine();
            if (isHighScore == "yes" || isHighScore == "Yes" || isHighScore == "y" || isHighScore == "Y")
            {
                Console.WriteLine($"What is your new high score in {chosenGame.GameName}?");
                string NewScoreInput = Console.ReadLine();
                if (int.TryParse(NewScoreInput, out int newScore)) //stores as int
                {
                    if (newScore > gameStats.HighScore) //if new is higher than old
                    {
                        gameStats.HighScore = newScore;
                        Console.WriteLine($"New high score logged into the system: {chosenGame.GameName}");
                        Logger.GetInstance().Log($"New high score logged for {chosenGame.GameName}");

                    }
                    else
                    {
                        Console.WriteLine($"Your score wasn't higher than your existing one! \nCurrent high score to beat: {gameStats.HighScore}.");
                    }
                }
                else
                {
                    Console.WriteLine("Thats not a valid number!");
                }
            }
            else
            {
                Console.WriteLine("Aww, that's a shame, better luck next time!");
            }
        }





        public void UpdateHighScore(Game selectedGame, int newScore)
        {
            GameStats gameStats = null;
            foreach (var gs in GameStatsList)  ///checks if selected game already has stats
            {
                if (gs.Game == selectedGame.GameName) //checks if the stats object belongs to the chosen game
                {
                    gameStats = gs;// if yes assigns it to gamestats
                    break;
                }

            }

            if (gameStats == null) //if no stats exist for this game
            {
                gameStats = new GameStats(selectedGame); //create new gamestats object
                GameStatsList.Add(gameStats); //add to list
            }

            //update high score only if the new score is higher than the older one
            if (gameStats != null && newScore > gameStats.HighScore)
            {
                gameStats.HighScore = newScore;
            }
                

        }



        public void UpdateHoursPlayed(Game selectedGame, int newHours)
        {
            GameStats stats = null;
            foreach (var gs in GameStatsList) //searches through gamestatslist to see if game already has stats
            {
                if (gs.Game == selectedGame.GameName)
                {
                    stats = gs; //found stats for the game
                    break;
                }
            }

            if (stats == null) //if no stats exist yet create a new gamestats object and add to the list
            {
                stats = new GameStats(selectedGame);
                GameStatsList.Add(stats);
            }


            if (stats != null && newHours > 0)//stats has to be positive to be added
            {
                stats.HoursPlayed += newHours;
            }
                


        }

        public int GetTotalPlayedHours()
        {
            int totalHours = 0;
            foreach (var gs in GameStatsList) //adds hours played from each gamestats object in list
            {
                totalHours += gs.HoursPlayed;
            }
            return totalHours;
        }

        public int GetTotalHighScore()
        {
            int totalHighScore = 0;
            foreach (var gs in GameStatsList) //loops through each GameStats and finds the highest high score
            {
                if (gs.HighScore > totalHighScore) //if new score is bigger than old
                {
                    totalHighScore = gs.HighScore;  //sets as new high score
                }
                   
            }
            return totalHighScore;
        }

        public void DisplayStats()
        {

            Console.WriteLine("---- Player Stats ----");
            if (GameStatsList.Count == 0)
            {
                Console.WriteLine($"You have no stats recorded");
                return;
            }

            foreach (var stats in GameStatsList)
            {
                Console.WriteLine($"Game: {stats.Game}");
                Console.WriteLine($"Hours Played: {stats.HoursPlayed}");
                Console.WriteLine($"High Score: {stats.HighScore}");
                Console.WriteLine("----------------------");
            }
        }



    }
}