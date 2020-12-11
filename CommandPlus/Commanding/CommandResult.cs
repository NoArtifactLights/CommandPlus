using System;
using System.Collections.Generic;
using System.Text;

namespace CommandPlus.Commanding
{
    /// <summary>
    /// Represents the result of an instance of <see cref="Command"/>.
    /// </summary>
    public struct CommandResult
    {
        /// <summary>
        /// Initializes a new instance of <see cref="CommandResult"/> with specified status and text. 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="status"></param>
        public CommandResult(string text, CommandResultStatus status)
        {
            ResultText = text;
            Status = status;
        }

        /// <summary>
        /// The result text that is shown to the user.
        /// </summary>
        public string ResultText { get; }
        /// <summary>
        /// The status of the command.
        /// </summary>
        public CommandResultStatus Status { get; }
    }

    /// <summary>
    /// Represents the status of a command.
    /// </summary>
    public enum CommandResultStatus
    {
        /// <summary>
        /// The command has been executed successfully.
        /// </summary>
        Success,
        /// <summary>
        /// The command is unable to parse it's argument.
        /// </summary>
        Unparseable,
        /// <summary>
        /// The command was failed to execute.
        /// </summary>
        Fail,
        /// <summary>
        /// The command has been executed successfully, but with warnings.
        /// </summary>
        Warning
    }
}
