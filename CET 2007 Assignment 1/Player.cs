using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CET2007A1
{

    public class Player
    {
        public int ID { get; set; }
        public string Username { get; set; }

        [JsonInclude]  //allows json to actually work. only json can use player stats, the rest have to use getstatsinterface()
        private Stats PlayerStats { get; set; } // encapsulated

        // expose only the interface for stats
        public IUpdatableStats GetStatsInterface()
        {
            return PlayerStats;
        }

        public Player(int id, string username)
        {
            this.ID = id;
            this.Username = username;
            PlayerStats = new Stats();
        }

        public Player() { } //empty constructor for json loading
    }
}