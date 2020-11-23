using System;
using System.Collections.Generic;
using System.Text;

namespace CommandPlus.Exceptions
{
    public class UnexceptedValueException : Exception
    {
        public UnexceptedValueException(int position, string exceptedType, string actualType) : base($"Excepted {exceptedType} on position {position} but found {actualType}")
        {
        }
        public UnexceptedValueException(int position, string exceptedType, string actualType, Exception inner) : base($"Excepted {exceptedType} on position {position} but found {actualType}", inner)
        {
        }
        public UnexceptedValueException(int position, string exceptedType) : base($"Excepted {exceptedType} on position {position} but found it wasn't {exceptedType}")
        { 
        }
        public UnexceptedValueException(int position, string exceptedType, Exception inner) : base($"Excepted {exceptedType} on position {position} but found it wasn't {exceptedType}", inner)
        {
        }
    }
}
