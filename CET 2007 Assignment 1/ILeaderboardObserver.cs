using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CET2007A1
{
    public interface ILeaderboardObserver
    {
        void Update(Player player); //called whenever a players stats change
    }
}
