using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CET2007A1
{
    /// <summary>
    /// Creates a reusable factory class for player creation. 
    /// </summary>
    public static class PlayerFactory
    {
        public static Player CreatePlayer(string username, int id)
        {
            return new Player(id, username);
        }
    }
}
