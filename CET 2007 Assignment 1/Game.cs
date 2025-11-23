using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CET2007A1
{
    /// <summary>
    /// Creating main elements for the game class, s
    /// </summary>
    public class Game
    {
        private static Random ranID = new Random();
        public static Random RandomGenerator  //when someone asks for random generator, it gives the private ranID
        {
            get { return ranID; }
        }
        public int GameID { get; set; }
        public string GameName { get; set; }
        public string GameGenre { get; set; }

        public Game(string gameName, string GameGenre)
        {
            this.GameName = gameName;
            this.GameGenre = GameGenre;
            this.GameID = ranID.Next(0, 999); //this is used when the game object is first created
            // though when the one its landed on is already used it calls  RandomGenerator() that keeps trying.
        }

        public override string ToString()
        {
            return $"{GameName} (ID: {GameID}) Genre: {GameGenre}"; //whenever tostring is called on this game object it returns this format
        }
    }
}