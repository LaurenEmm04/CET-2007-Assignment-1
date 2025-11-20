using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CET2007A1
{
    class CustomExceptions
    {
        public class PlayerNotFoundException : Exception
        {
            public PlayerNotFoundException(string message) : base(message) { }
        }

        public class InvalidIDException : Exception
        {
            public InvalidIDException(string message) : base(message) { }
        }

        public class DuplicatePlayerException : Exception
        {
            public DuplicatePlayerException(string message) : base(message) { }
        }
    }
}
