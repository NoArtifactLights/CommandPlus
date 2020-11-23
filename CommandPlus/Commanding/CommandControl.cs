using CommandPlus.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommandPlus.Commanding
{
    /// <summary>
    /// Represents the controller of commands.
    /// </summary>
    /// <remarks>
    /// This class is the dispatcher of all commands. It handles the command parsing, the command processing, 
    /// and the command executing. It filters out all impossible commands, such as commands requires argument but with no arguments.
    /// Current not supporting reloads.
    /// </remarks>
    public class CommandControl
    {
        private Dictionary<string, Command> commands = new Dictionary<string, Command>();

        public void Register(string name, Command instance)
        {
            if (instance == null) throw new ArgumentNullException(nameof(instance));
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("The name is empty.", nameof(name));

            commands.Add(name.ToLower(), instance);
        }

        public void ParseAndRun(string command)
        {
            if (string.IsNullOrWhiteSpace(command)) throw new UnexceptedValueException(0, "Command Name", "nothing");
            StringParser sp = new StringParser(command);
            string name = sp.ReadUnquotedString();
            if (!commands.ContainsKey(name.ToLower())) throw new UnknownCommandException(name);

            Command cmd = commands[name];

            #region Calculates the length
            List<string> temp = new List<string>(command.Split(' '));
            temp.Remove(name);
            int count = temp.Count;
            #endregion

            if (count != cmd.ArgumentsLength) throw new UnknownCommandException(name, count);

            Type @string = typeof(string);
            Type @bool = typeof(bool);
            Type @int = typeof(int);
            Type @long = typeof(long);
            Type @float = typeof(float);
            Type @double = typeof(double);

            List<object> types = new List<object>();

            foreach(var param in cmd.ArgumentTypes)
            {

                // Sorry but switch does not let me to switch about types :(
                sp.SkipWhitespace();
                if (param == @string)
                {
                    types.Add(sp.ReadString());
                } 
                else if(param == @bool) 
                {
                    types.Add(sp.ReadBoolean());
                }
                else if(param == @int)
                {
                    types.Add(sp.ReadInt32());
                }
                else if(param == @long)
                {
                    types.Add(sp.ReadInt64());
                }
                else if(param == @float)
                {
                    types.Add(sp.ReadSingle());
                }
                else if(param == @double)
                {
                    types.Add(sp.ReadDouble());
                }
                else
                {
                    throw new InvalidOperationException("The type defined are not common types. This is not your fault - It is the author's.");
                }
            }

            cmd.Executed(types.ToArray());
        }
    }
}
