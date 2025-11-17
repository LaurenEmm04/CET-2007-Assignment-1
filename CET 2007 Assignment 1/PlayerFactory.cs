using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CET2007A1
{
    public static class PlayerFactory
    {
        public static Player CreatePlayer(string username)
        {
            Player newPlayer = new Player(username);
            Logger.GetInstance().Log($"Username: {username} (ID: {newPlayer.ID})");
            return newPlayer;
        }
    }
}
