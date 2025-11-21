using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CET2007A1
{
    /// <summary>
    /// Declaring key info such as ID, Username, JSON inclusion and the private PlayerStats from Stats.
    /// PlayerStats is used in tandem with IUpdatableStats and GetStatsINterface to ensure encapsulation
    /// </summary>
    public class Player
    {
        public int ID { get; set; }
        public string Username { get; set; }

        [JsonInclude]  //allows playerstats to be used by json when saving and loading. Other code needs to access it though GetStatsInterface() to keep stats secure
        private Stats PlayerStats { get; set; } // encapsulated

        // expose only the interface for stats
        public IUpdatableStats GetStatsInterface() //returns PlayerStats but only through IUpdatableStats
                                                   //keeping  the actual stats object safely encapsulated
        {
                                              
            return PlayerStats;
        }

        public Player(int id, string username) //player constructor
        {
            this.ID = id;
            this.Username = username;
            PlayerStats = new Stats();
        }

        public Player() { } //empty constructor for json loading
    }
}