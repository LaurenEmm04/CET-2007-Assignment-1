using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CET2007A1
{
    public class Game
    {
        private static Random ranID = new Random();
        public int GameID { get; set; }
        public string GameName { get; set; }
        public string GameGenre { get; set; }

        public Game(string gameName, string GameGenre)
        {
            this.GameName = gameName;
            this.GameGenre = GameGenre;
        }

        public override string ToString()
        {
            return $"{GameName} (ID: {GameID}) Genre: {GameGenre}";
        }
    }
}