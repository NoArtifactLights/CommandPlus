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
        /// <summary>
        /// A list containing allowed arguments as well as their types.
        /// </summary>
        public List<Type> ArgumentTypes = new List<Type>();
        /// <summary>
        /// Gets a value indicating how many arguments are received.
        /// </summary>
        public virtual int ArgumentsLength => ArgumentTypes.Count;

        /// <summary>
        /// Called when this command is executed.
        /// </summary>
        /// <param name="arguments">The arguments that has been received.</param>
        public abstract CommandResult Executed(object[] arguments);

        /// <summary>
        /// Gets the type of the argument on specified index.
        /// </summary>
        /// <param name="index">The index of the argument.</param>
        /// <returns>The type of the specified argument.</returns>
        protected virtual Type GetArgumentTypeOnIndex(int index)
        {
            return ArgumentTypes[index];
        }

        /// <summary>
        /// Verifies whether the type given matches the allowed types and then parses the value.
        /// </summary>
        /// <typeparam name="T">The type of the object.</typeparam>
        /// <param name="index">The index of value.</param>
        /// <param name="obj">The value object.</param>
        /// <returns>The value.</returns>
        protected virtual T VerifyAndConstruct<T>(int index, object obj)
        {
            if (typeof(T) != GetArgumentTypeOnIndex(index)) throw new ArgumentException("Looks like the index is wrong!", nameof(index));
            return (T)obj;

        }
    }
}
