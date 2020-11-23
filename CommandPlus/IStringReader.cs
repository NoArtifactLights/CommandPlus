using System;
using System.Collections.Generic;
using System.Text;

namespace CommandPlus
{
    /// <summary>
    /// Represents the reader of the String instance.
    /// </summary>
    /// <remarks>
    /// String Reader, for example, <see cref="StringParser"/> parses values from a <see cref="string"/>, like a <see cref="System.IO.BinaryReader"/>.
    /// This is used for parsing inputs.
    /// </remarks>
    public interface IStringReader
    {
        /// <summary>
        /// Reads the next character and pushes the cursor to the next position.
        /// </summary>
        /// <returns>The character on the current position.</returns>
        char Next();
        /// <summary>
        /// Skips the character and pushes the cursor to the next position.
        /// </summary>
        void Skip();
        /// <summary>
        /// Skips across the current whitespace and pushes the cursor to the next position after the whitespace.
        /// </summary>
        void SkipWhitespace();
        /// <summary>
        /// Reads an <see cref="int"/> from the current string and pushes the cursor to the next position after the end of the current <see cref="int"/> value.
        /// </summary>
        /// <returns>The <see cref="int"/> value from the current position.</returns>
        /// <exception cref="UnexceptedValueException"></exception>
        int ReadInt32();
        /// <summary>
        /// Reads a <see cref="long"/> from the current string and pushes the cursor to the next position after the end of the current <see cref="long"/> value.
        /// </summary>
        /// <returns>The <see cref="long"/> value from the current position.</returns>
        /// <exception cref="UnexceptedValueException"></exception>
        long ReadInt64();
        /// <summary>
        /// Reads a <see cref="double"/> from the current string and pushes the cursor to the next position after the end of the current <see cref="double"/> value.
        /// </summary>
        /// <returns>The <see cref="double"/> value from the current position.</returns>
        /// <exception cref="UnexceptedValueException"></exception>
        double ReadDouble();
        /// <summary>
        /// Reads a <see cref="float"/> from the current string and pushes the cursor to the next position after the end of the current <see cref="float"/> value.
        /// </summary>
        /// <returns>The <see cref="float"/> value from the current position.</returns>
        /// <exception cref="UnexceptedValueException"></exception>
        float ReadSingle();
        /// <summary>
        /// Reads a <see cref="string"/> from the current string and pushes the cursor to the next position after the end of the <see cref="string"/>.
        /// This method does not processes the quotes. If you wants to remove the quotes, call <see cref="ReadQuotedString"/>.
        /// </summary>
        /// <returns>The <see cref="string"/> value from the current position.</returns>
        string ReadUnquotedString();
        /// <summary>
        /// Reads a <see cref="string"/> from the current string and pushes the cursor to the next position after the end of the <see cref="string"/>.
        /// This method removes the surrounding quotes from the string, but it will only work if the string is quoted. If you can confirm the next value does not have quotes, use <see cref="ReadUnquotedString()"/>.
        /// </summary>
        /// <returns>The <see cref="string"/> value from the current position, with surrounding quotes removed.</returns>
        string ReadQuotedString();
        /// <summary>
        /// Reads a <see cref="string"/> directly from the current string and pushes the cursor to the next position after the end of the <see cref="string"/>.
        /// </summary>
        /// <returns>The unprocessed <see cref="string"/> value from the current position.</returns>
        string ReadString();
        /// <summary>
        /// Reads a <see cref="bool"/> value from the current position, with surrounding quotes removed.
        /// </summary>
        /// <returns>The <see cref="bool"/> value from the current position.</returns>
        bool ReadBoolean();
        void Expect(char c);
    }
}
