using CommandPlus.Exceptions;
using System;
using System.Text;

namespace CommandPlus
{
    /// <summary>
    /// Represents an Immutable String Reader.
    /// </summary>
    public class StringParser : IStringReader
    {
        private static readonly char SyntaxEscape = '\\';
        private static readonly char SyntaxQuote = '"';

        /// <summary>
        /// The current Text of this <see cref="StringParser"/>.
        /// </summary>
        public string Text { get; private set; }

        /// <summary>
        /// The current position of <see cref="Text"/>.
        /// </summary>
        public int Position { get; set; }

        /// <summary>
        /// Initializes an instance of <see cref="StringParser"/>.
        /// </summary>
        /// <param name="str">The string to read.</param>
        public StringParser(string str)
        {
            Text = str;
        }

        /// <summary>
        /// Duplicates another <see cref="StringParser"/>.
        /// </summary>
        /// <param name="other">The <see cref="StringParser"/> to copy.</param>
        public StringParser(StringParser other)
        {
            Text = other.Text;
            Position = other.Position;
        }

        /// <summary>
        /// Gets the remaining length of this <see cref="StringParser"/>.
        /// </summary>
        public int RemainingLength => Text.Length - Position;
        /// <summary>
        /// Gets the total length of this <see cref="StringParser"/>.
        /// DO NOT USE - use <see cref="string.Length"/> on <see cref="Text"/> instead.
        /// </summary>
        [Obsolete]
        public int TotalLength => Text.Length;

        public string Read => Text.Substring(0, Position);
        public string Remaining => Text.Substring(Position);

        public bool CanRead(int length) => Position + length <= Text.Length;
        public bool IsReadable => CanRead(1);

        /// <summary>
        /// Gets the character at current position.
        /// </summary>
        /// <returns>The character found.</returns>
        public char OnCurrent()
        {
            return Text[Position];
        }

        /// <summary>
        /// Gets the character at the offset of the current position.
        /// </summary>
        /// <param name="offset">The offset value.</param>
        /// <returns>The character found.</returns>
        public char OnOffset(int offset)
        {
            return Text[Position + offset];
        }

        public char Next()
        {
            return Text[Position++];
        }

        private static bool IsNumber(char c)
        {
            return c >= '0' && c <= '9' || c == '.' || c == '-';
        }

        public void SkipWhitespace()
        {
            while (IsReadable && char.IsWhiteSpace(OnCurrent()))
            {
                Skip();
            }
        }

        public void Skip()
        {
            Position++;
        }

        public int ReadInt32()
        {
            var start = Position;
            while (IsReadable && IsNumber(OnCurrent()))
            {
                Skip();
            }

            var number = Text.Substring(start, Position - start);
            if (number.Length == 0)
            {
                throw new UnexceptedValueException(Position, "Int64");
            }

            try
            {
                return int.Parse(number);
            }
            catch (FormatException fex)
            {
                Position = start;
                throw new UnexceptedValueException(Position, "Int64", fex);
            }
        }

        public long ReadInt64()
        {
            var start = Position;
            while (IsReadable && IsNumber(OnCurrent()))
            {
                Skip();
            }

            var number = Text.Substring(start, Position - start);
            if (number.Length == 0)
            {
                throw new UnexceptedValueException(Position, "Int64");
            }

            try
            {
                return long.Parse(number);
            }
            catch (FormatException fex)
            {
                Position = start;
                throw new UnexceptedValueException(Position, "Int64", fex);
            }
        }

        public double ReadDouble()
        {
            var start = Position;
            while (IsReadable && IsNumber(OnCurrent()))
            {
                Skip();
            }

            var number = Text.Substring(start, Position - start);
            if (number.Length == 0)
            {
                throw new UnexceptedValueException(Position, "Single");
            }

            try
            {
                return double.Parse(number);
            }
            catch (FormatException fex)
            {
                Position = start;
                throw new UnexceptedValueException(Position, "Double", fex);
            }
        }

        public float ReadSingle()
        {
            var start = Position;
            while (IsReadable && IsNumber(OnCurrent()))
            {
                Skip();
            }

            var number = Text.Substring(start, Position - start);
            if (number.Length == 0)
            {
                throw new UnexceptedValueException(Position, "Single");
            }

            try
            {
                return float.Parse(number);
            }
            catch (FormatException fex)
            {
                Position = start;
                throw new UnexceptedValueException(Position, "Single", fex);
            }
        }

        public static bool IsAllowedInUnquotedString(char c)
        {
            return c >= '0' && c <= '9'
                   || c >= 'A' && c <= 'Z'
                   || c >= 'a' && c <= 'z'
                   || c == '_' || c == '-'
                   || c == '.' || c == '+';
        }

        public string ReadUnquotedString()
        {
            var start = Position;
            while (IsReadable && IsAllowedInUnquotedString(OnCurrent()))
            {
                Skip();
            }

            return Text.Substring(start, Position - start);
        }

        public string ReadQuotedString()
        {
            if (!IsReadable)
            {
                throw new EndOfStringException();
            }
            else if (OnCurrent() != SyntaxQuote)
            {
                throw new UnexceptedValueException(Position, "Quote");
            }

            Skip();
            var result = new StringBuilder();
            var escaped = false;
            while (IsReadable)
            {
                var c = Next();
                if (escaped)
                {
                    if (c == SyntaxQuote || c == SyntaxEscape)
                    {
                        result.Append(c);
                        escaped = false;
                    }
                    else
                    {
                        Position--;
                        throw new FormatException("Invalid escape on position " + (Position + 1));
                    }
                }
                else if (c == SyntaxEscape)
                {
                    escaped = true;
                }
                else if (c == SyntaxQuote)
                {
                    return result.ToString();
                }
                else
                {
                    result.Append(c);
                }
            }

            throw new UnexceptedValueException(Position, "End of Quote");
        }

        /// <summary>
        /// Determines whether the string is quoted or not and reads it.
        /// </summary>
        /// <returns>The next string.</returns>
        public string ReadString()
        {
            if (IsReadable && OnCurrent() == SyntaxQuote)
            {
                return ReadQuotedString();
            }
            else
            {
                return ReadUnquotedString();
            }
        }

        public bool ReadBoolean()
        {
            var start = Position;
            var value = ReadString();
            if (value.Length == 0)
            {
                throw new UnexceptedValueException(Position, "Boolean", "nothing");
            }

            if (value.Equals("true"))
            {
                return true;
            }
            else if (value.Equals("false"))
            {
                return false;
            }
            else
            {
                Position = start;
                throw new UnexceptedValueException(Position, "Boolean");
            }
        }

        public void Expect(char c)
        {
            if (!IsReadable || OnCurrent() != c)
            {
                throw new UnexceptedValueException(Position, $"Symbol '{c}'");
            }

            Skip();
        }
    }
}
