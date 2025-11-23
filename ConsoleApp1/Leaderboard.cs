using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CET2007A1
{
    /// <summary>
    /// Sorts by HighScore, HoursPlayed, shows filtered and unfiltered leaderboard
    /// </summary>
    public class Leaderboard
    {
        private List<Player> players;   //stays secure
        private GameLibrary gameLibrary;  //stays secure
        public Leaderboard(List<Player> players, GameLibrary gameLibrary) //enables access via the public method
        {
            this.players = players; //declaring variables
            this.gameLibrary = gameLibrary;
        }


        //bubble sort

        /// <summary>
        /// sorts players in the players list by high score
        /// </summary>
        public void SortByScore()
        {
            int n = players.Count;  //stores how many people are in the list

            for (int i = 0; i < n - 1; i++) //pushes highest remaining score towards the front
            {
                for (int j = 0; j < n - i - 1; j++)//compares each player to the one next to them
                {

                    //gets total high score of player j
                    int ScoreA = players[j].GetStatsInterface().GetTotalHighScore();

                    //gets the total high score of the player next to j (j+1)
                    int ScoreB = players[j + 1].GetStatsInterface().GetTotalHighScore();

                    //if J's score is less than j+1's then we swap to decending scores
                    if (ScoreA < ScoreB)
                    {
                        //logs the players that are being swapped
                        Logger.GetInstance().Log($"Swapping {players[j].Username} with {players[j + 1].Username}");

                        //swaps 2 players in the list
                        Player temp = players[j];  //stores the first player tempoaraly
                        players[j] = players[j + 1]; //moves 2nd player into first position since they have the higher score
                        players[j + 1] = temp; //puts the stored player (old J) into second position

                    }
                }
            }
        }

        //bubble sort
        /// <summary>
        /// Sorts players in the players list by their hours played
        /// </summary>
        public void SortByHoursPlayed()
        {
            int n = players.Count; //another way of counting the number of players in a list

            for (int i = 0; i < n - 1; i++) //outer loop
            {
                for (int j = 0; j < n - i - 1; j++) //inner loop, compares the nearby players
                {
                    //gets total hours played for player j
                    int HoursPlayedA = players[j].GetStatsInterface().GetTotalPlayedHours();

                    //gets total hours played for the player next to j (J+1)
                    int HoursPlayedB = players[j + 1].GetStatsInterface().GetTotalPlayedHours();

                    //if j's total hours is less than j+1's
                    if (HoursPlayedA < HoursPlayedB)
                    {
                        //logs the players that are being swapped
                        Logger.GetInstance().Log($"Swapping {players[j].Username} with {players[j + 1].Username}");
                        //swaps 2 players in the list
                        Player temp = players[j]; //stores the first player tempoaraly
                        players[j] = players[j + 1];  //moves 2nd player into first position since they have the higher hours played
                        players[j + 1] = temp;   //puts the stored player (old J) into second position
                    }
                }
            }
        }


        /// <summary>
        /// Displays the unfiltered leaderboard username, high score, hours played
        /// </summary>
        public void ShowLeaderboard()
        {

            Console.WriteLine("---- Show Leaderboard ----");
            foreach (var p in players)
            {
                var stats = p.GetStatsInterface();
                Console.WriteLine($"{p.Username} | High Score: {stats.GetTotalHighScore()} | Hours played: {stats.GetTotalPlayedHours()}");
            }
            Console.WriteLine("--------------------------");
        }


        /// <summary>
        /// Shows the filtered by game played leaderboard, displaying username, high score and hours played for the chosen game
        /// </summary>
        public void ShowFilteredLeaderboard(string FilteredGame)
        {
            Console.WriteLine($"---- Displaying leaderboard for {FilteredGame} ----");
            bool found = false;

            foreach (var p in players)
            {
                var stats = p.GetStatsInterface(); //looking for game in each players stats
                foreach (var gs in stats.GetGameStats()) //loops through the players recorded stats
                {
                    //if the filtered game matches something in the game list then filter by that
                    if (string.Equals(gs.Game, FilteredGame, StringComparison.OrdinalIgnoreCase)) //added string comparison as before if you put in lowercase game name it woudnt recognise it
                    {
                        Console.WriteLine($"{p.Username} | High score: {gs.HighScore} | Hours Played: {gs.HoursPlayed}");
                        found = true;
                    }
                }

            }
            if (!found)
            {
                Console.WriteLine("No players have stats for this game yet.");
            }
        }



    }
}
