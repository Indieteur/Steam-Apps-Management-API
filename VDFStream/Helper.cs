using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Indieteur.VDFAPI
{
    public enum Delimiters
    {
        CRLF,
        LF,
        SystemDefault
    }
    static class Helper
    {

        public static string UnformatString(string str)
        {
            char[] charofString = str.ToCharArray(); //Let's convert the string to an array of characters
            StringBuilder sb = new StringBuilder(str.Length); //Create our stringbuilder and add offset it's size by 5.


            for (int i = 0; i < charofString.Length; ++i) //loop through all the characters in the string
            {
                switch (charofString[i]) //Check if the character is one of the following inside the switch statement.
                {
                    case '\n':
                        sb.Append("\\n"); //NewLine
                        break;
                    case '\r':
                        continue; //No need to append carriage return to the string. 
                    case '\t':
                        sb.Append("\\t"); //Horizontal Tab
                        break;
                    case '\\':
                        sb.Append("\\\\"); //Forward slash
                        break;
                    case '"':
                        sb.Append("\\\""); //Double Quotes
                        break;
                    default:
                        sb.Append(charofString[i]); //Other characters
                        break;

                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// Converts the enumeration of delimiter to its string value.
        /// </summary>
        /// <param name="delimiters">The delimiter enumeration.</param>
        /// <returns></returns>
        public static string DelimiterEnumToString(Delimiters delimiters)
        {
            switch (delimiters) //Check which delimiter was selected and convert it to it's string or char value.
            {
                case Delimiters.CRLF: //Windows
                    return "\r\n";
                case Delimiters.LF: //Unix/Linux/Mac OS
                    return "\n";
                default: //The newline as defined by the current system of the user.
                    return Environment.NewLine;
            }
        }

        /// <summary>
        /// Returns the horizontal tab character and appends it repeatedly for a set amount of times.
        /// </summary>
        /// <param name="repititions">Indicates how many times the tab character should be repeated.</param>
        /// <returns></returns>
        public static string Tabify (int repititions)
        {
            if (repititions <= 0) //If repititions is set to 0 or a negative number, we return an empty string.
                return "";
            StringBuilder sb = new StringBuilder(repititions); //Initialize our StringBuilder. We already know what the size of the resulting string would be equal to the number of times the tab will be appended to it.
            for (int i = 0; i < repititions; ++i) //Iterate the appending of the tab character until i is equals to repititions.
                sb.Append("\t");
            return sb.ToString(); //return the resulting character.
        }

        /// <summary>
        /// Returns the char value of the element in an array if it exists and returns '\0' or the null character if it doesn't.
        /// </summary>
        /// <param name="index">The index of the element that you want to look at.</param>
        /// <param name="array">The array which contains the element.</param>
        /// <returns></returns>
        public static char Peek(int index, char[] array)
        {
            if (array.Length <= index) 
                return '\0';
            return array[index]; 
           
        }
        

        /// <summary>
        /// Returns the human readable character representation of an escape character. Only accepts the character that succeeds the forward slash character. (e.g. 't' in '\t'),
        /// </summary>
        /// <param name="secondPartChar">The character succeeding the forward slash character. (e.g. 'n' in '\n')</param>
        /// <param name="line">(Optional) The line where the character is on. This is for error handling.</param>
        /// /// <param name="line">(Optional)  The position of the character in the line from Left to Right. This is for error handling.</param>
        /// <returns></returns>
        public static string ParseSecondPartOfEscapeChar(char secondPartChar, int line = 0, int characterPos = 0)
        {
            switch (secondPartChar) //Check what the secondPartChar is
            {
                case 'n': //New Line
                    return Environment.NewLine;
                case 't': //Horizontal Tab
                    return "\t";
                case '\\': //Forward Slash
                    return "\\";
                case '"': //Double Quote
                    return "\"";
                default: //If it isn't any of the characters above
                    throw new VDFStreamException("Invalid escape character detected!", line, characterPos);
            }
        }
    }
    /// <summary>
    /// Exception thrown by the library.
    /// </summary>
    public class VDFStreamException : Exception
    {
        /// <summary>
        /// The Line where the error was found. The minimum value is 1.
        /// </summary>
        public int Line { get; private set; }
        /// <summary>
        /// The position of the error causing character in the line. The minimum value is 1.
        /// </summary>
        public int Character { get; private set; }
        public VDFStreamException(string message) : base(message)
        {

        }
        public VDFStreamException(string message, int line) : base("Line: " + line.ToString() + ". " + message)
        {
            Line = line;
        }
        public VDFStreamException(string message, int line, int character) : base("Line: " + line.ToString() + ". Character: " + character.ToString() + ". " + message)
        {
            Line = line;
            Character = character;
            
        }
    }
}
