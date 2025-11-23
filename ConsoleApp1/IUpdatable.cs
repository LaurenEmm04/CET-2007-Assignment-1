using CET2007A1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CET2007A1
{
    /// <summary>
    /// Interface holds key methods like updating high scores, hours played, recording stats, getting the total high scores and played hours (adding the old onto the new)
    /// Read only list lets the filtered leaderboard access gamestats() because it wouldnt work otherwise
    /// </summary>
    public interface IUpdatableStats
    {
        void UpdateHighScore(Game selectedGame, int newScore);
        void UpdateHoursPlayed(Game selectedGame, int newHours);
        void RecordStats(GameLibrary gameLibrary);
        void DisplayStats();
        int GetTotalHighScore();
        int GetTotalPlayedHours();
        IReadOnlyList<GameStats> GetGameStats(); //lets filtered leaderboard access the game stats list
    }
}