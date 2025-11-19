using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CET2007A1
{
    public class Leaderboard
    {
        private List<Player> players;
        private GameLibrary gameLibrary;
        public Leaderboard(List<Player> players, GameLibrary gameLibrary)
        {
            this.players = players;
            this.gameLibrary = gameLibrary;
        }

        public void SortByScore()
        {
            int n = players.Count;  //stores how many people are in the list

            for (int i = 0; i < n - 1; i++) //pushes highest remaining score towards the front
            {
                for (int j = 0; j < n - i - 1; j++)//compares each player to the one next to them
                {
                    // compare scores using encapsulation
                    if (players[j].GetStatsInterface().GetTotalHighScore() < players[j + 1].GetStatsInterface().GetTotalHighScore())
                    {
                        // swap positions if needed
                        Player temp = players[j];
                        players[j] = players[j + 1];
                        players[j + 1] = temp;
                    }
                }
            }
        }

        public void SortByHoursPlayed()
        {
            int n = players.Count();

            for (int i =0;i<n-1;i++)
            {
                for (int j =0;j<n-i-1;j++)
                {
                    int HoursPlayedA = players[j].GetStatsInterface().GetTotalPlayedHours();
                    int HoursPlayedB = players[j + 1].GetStatsInterface().GetTotalPlayedHours();

                    if (HoursPlayedA<HoursPlayedB) //decending 
                    {
                        Logger.GetInstance().Log($"Swapping {players[j].Username} with {players[j + 1].Username}");

                        Player temp = players[j];
                        players[j] = players[j + 1];
                        players[j + 1] = temp;
                    }
                }
            }
        }



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

        public void ShowFilteredLeaderboard(string FilteredGame)
        {
            Console.WriteLine($"---- Displaying leaderboard for {FilteredGame} ----");
            bool found = false;

            foreach (var p in players)
            {
                var stats = p.GetStatsInterface(); //looking for game in each players stats
                foreach (var gs in stats.GetGameStats())
                {
                    if (string.Equals(gs.Game, FilteredGame, StringComparison.OrdinalIgnoreCase)) //added string comparison as if you put in lowercase game name it woudnt recognise it
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
