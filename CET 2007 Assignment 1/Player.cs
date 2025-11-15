using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CET2007A1
{
    
        class Player
        {
            public int ID { get; set; }
            public string Username { get; set; }
            public Stats PlayerStats { get; set; } //links to stats block

            public Player(int id, string username)
            {
                this.ID = id;
                this.Username = username;
                PlayerStats = new Stats();
            }
            public Player() { } //empty constructor for json loading

            public IUpdatableStats GetStatsInterface()
            {
                return PlayerStats;
            }
        }
    }

