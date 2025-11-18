using CET2007A1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CET2007A1
{
    public interface IUpdatableStats
    {
        void UpdateHighScore(Game selectedGame, int newScore);
        void UpdateHoursPlayed(Game selectedGame, int newHours);
        void RecordStats(GameLibrary gameLibrary);
        void DisplayStats();
    }
}