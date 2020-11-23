using System;
using System.Collections.Generic;
using System.Text;

namespace CommandPlus.Commanding
{
    /// <summary>
    /// The base class of all application commands.
    /// </summary>
    /// <remarks>
    /// All commands must inherit this class.
    /// </remarks>
    public abstract class Command
    {
        public List<Type> ArgumentTypes = new List<Type>();
        public virtual int ArgumentsLength => ArgumentTypes.Count;

        public abstract void Executed(object[] arguments);

        protected virtual Type GetArgumentTypeOnIndex(int index)
        {
            return ArgumentTypes[index];
        }

        protected virtual T VerifyAndConstruct<T>(int index, object obj)
        {
            if (typeof(T) != GetArgumentTypeOnIndex(index)) throw new ArgumentException("Looks like the index is wrong!", nameof(index));
            return (T)obj;

        }
    }
}
