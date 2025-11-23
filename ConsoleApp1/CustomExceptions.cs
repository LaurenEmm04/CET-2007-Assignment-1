using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CET2007A1
{
    /// <summary>
    /// Displays different types of custom exceptions
    /// </summary>
    class CustomExceptions
    {
        /// <summary>
        /// If player isnt found it pulls this exception
        /// </summary>
        public class PlayerNotFoundException : Exception
        {
            public PlayerNotFoundException(string message) : base(message) { }
        }

        /// <summary>
        /// if ID is invalid (not a number)
        /// </summary>
        public class InvalidIDException : Exception
        {
            public InvalidIDException(string message) : base(message) { }
        }

        /// <summary>
        /// if player username/ID already exists
        /// </summary>
        public class DuplicatePlayerException : Exception
        {
            public DuplicatePlayerException(string message) : base(message) { }
        }
    }
}
