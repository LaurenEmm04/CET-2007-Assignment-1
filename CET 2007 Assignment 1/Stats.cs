using System;
using System.Collections.Generic;

namespace CET2007A1
{
    /// <summary>
    /// Stats uses the 
    /// </summary>
    class Stats : IUpdatableStats
    {
        public List<GameStats> GameStatsList { get; set; } = new List<GameStats>();




        public Stats() { }

        public IReadOnlyList<GameStats> GetGameStats()
        {
            return GameStatsList.AsReadOnly();
        }

        public void RecordStats(GameLibrary gameLibrary) //enables use of Game and GameGenre
        {
            Game chosenGame = null;

            //loop until a game is chosen
            while (chosenGame == null)
            {
                Console.WriteLine("Showing all games in your game library..");
                gameLibrary.GameList();
                Console.WriteLine("Which game are you updating stats for? (please enter a game name or ID)");
                string gameInput = Console.ReadLine();
                if (int.TryParse(gameInput, out int gameID))
                {
                    chosenGame = gameLibrary.FindGameByID(gameID); //searches via ID first
                }
                else
                {
                    chosenGame = gameLibrary.FindGameByName(gameInput); //then searches via name if no id was entered
                }
                    
                if (chosenGame == null)
                {
                    Console.WriteLine("That game isn't in here! Add it instead!");
                    return;
                }
            }

            GameStats gameStats = null;
            foreach (var gs in GameStatsList)
            {
                if (gs.Game == chosenGame.GameName)
                {
                    gameStats = gs;
                    break;
                }
            }
            if (gameStats == null)
            {
                gameStats = new GameStats(chosenGame);
                GameStatsList.Add(gameStats);
            }

            Console.WriteLine($"How many hours have you played on {chosenGame.GameName}?");
            string HoursPlayedInput = Console.ReadLine();
            if (int.TryParse(HoursPlayedInput, out int hours) && hours >= 0)
            {
                gameStats.HoursPlayed += hours;
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
                if (int.TryParse(NewScoreInput, out int newScore))
                {
                    if (newScore > gameStats.HighScore)
                    {
                        gameStats.HighScore = newScore;
                        Console.WriteLine($"New high score logged into the system: {chosenGame.GameName}");
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
            foreach (var gs in GameStatsList)
            {
                if (gs.Game == selectedGame.GameName)
                {
                    gameStats = gs;
                    break;
                }

            }
            if (gameStats != null && newScore > gameStats.HighScore)
                gameStats.HighScore = newScore;

        }



        public void UpdateHoursPlayed(Game selectedGame, int newHours)
        {
            GameStats stats = null;
            foreach (var gs in GameStatsList)
            {
                if (gs.Game == selectedGame.GameName)
                {
                    stats = gs;
                    break;
                }
            }
            if (stats != null && newHours > 0)
                stats.HoursPlayed += newHours;


        }

        public int GetTotalPlayedHours()
        {
            int totalHours = 0;
            foreach (var gs in GameStatsList)
            {
                totalHours += gs.HoursPlayed;
            }
            return totalHours;
        }

        public int GetTotalHighScore()
        {
            int totalHighScore = 0;
            foreach (var gs in GameStatsList)
            {
                if (gs.HighScore > totalHighScore)
                    totalHighScore = gs.HighScore;
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