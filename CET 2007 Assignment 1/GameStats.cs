using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CET2007A1.Program;

namespace CET2007A1
{
    public class GameStats
    {
        public string Game { get; set; } //stores only Game not the whole object so json can read it
        public int HoursPlayed { get; set; }
        public int HighScore { get; set; }

        public GameStats() { } //for json
        public GameStats(Game game)
        {
            Game = game.GameName;
            HoursPlayed = 0;
            HighScore = 0;
        }
    }
}
