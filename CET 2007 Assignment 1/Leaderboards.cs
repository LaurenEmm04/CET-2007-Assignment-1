using CET2007A1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CET2007A1
{
    class Leaderboards : ILeaderboardObserver
    {
        private List<Player> TopPlayers = new List<Player>();

        public void Update(Player player)
        {
            //add the player to the list if hes not already there.
            if (!TopPlayers.Contains(player))
                TopPlayers.Add(player);

            //sort by highest high score
            for (int i =0; i < TopPlayers.Count; i++)
            {
                for (int j = i + 1; j < TopPlayers.Count; j++)
                {
                    if (TopPlayers[j].PlayerStats.GetTotalHighScore() > TopPlayers[i].PlayerStats.GetTotalHighScore())
                    {
                        var temp = TopPlayers[i];
                        TopPlayers[i] = TopPlayers[j];
                        TopPlayers[j] = temp;
                    }
                }
            }

            DisplayLeaderboard();
        }

        public void DisplayLeaderboard()
        {
            Console.WriteLine("\n---- Leaderboard ----");
            foreach (var p in TopPlayers)
            {
                Console.WriteLine($"{p.Username} - High Score: {p.PlayerStats.GetTotalHighScore()}");
            }
        }
    }
}





public List<Player> TopPlayersFromScore(List<Player> players)
{
    return players;
}

public List<Player> TopActivePlayers(List<Player> players)
{
    return players;
}