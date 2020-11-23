using System;
using System.Collections.Generic;
using System.Text;

namespace CommandPlus.Exceptions
{
    public class EndOfStringException : Exception
    {
        public EndOfStringException() : base("The string has ended and no further read can perform.")
        {
        }
    }
}
