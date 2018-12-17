using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Indieteur.VDFAPI
{
    static class BaseTokenExtensionMethod
    {
        /// <summary>
        /// Finds a Node or a Key from a BaseToken collection by using the Name field.
        /// </summary>
        /// <param name="tokens">The collection of BaseToken to search through.</param>
        /// <param name="Name">The name of the node that the method needs to find.</param>
        /// <param name="CaseSensitive">Indicates if the name argument and the name of the BaseToken needs to be an exact match in terms of capitalization.</param>
        /// <param name="throwErrorIfNotFound">Throw an exception if the BaseToken could not be found. If false, method will return null instead.</param>
        /// <returns></returns>
        internal static BaseToken FindBaseToken(this IEnumerable<BaseToken> tokens, string Name, bool CaseSensitive = false, bool throwErrorIfNotFound = false)
        {
            int tokensLength = tokens.Count(); //Store the length of our tokens list to a variable 
            if (!CaseSensitive) //If CaseSensitve is set to false, then we convert the Name argument to lower case.
                Name = Name.ToLower();
            for (int i = 0; i < tokensLength; ++i) 
            {
                string tokenName; //This will store the name of the BaseToken that we are checking.
                BaseToken currentToken = tokens.ElementAt(i); //Cache the BaseToken that we are working with.
                if (CaseSensitive) //If CaseSensitive is set to true, set the tokenName variable to the name of the currentToken without doing any string manipulation.
                    tokenName = currentToken.Name;
                else //If CasenSensitive is set  to false, set the tokenName variable to the lower case equivalent of the currentToken's name.
                    tokenName = currentToken.Name.ToLower();

                if (tokenName == Name) 
                    return currentToken;
            }
            if (throwErrorIfNotFound) //We're done looping through our collection and we haven't found the currentToken.
                throw new TokenNotFoundException(Name + " has not been found in the collection!");
            return null; //If throwError is set to false, then return null instead.
        }
        /// <summary>
        /// Finds a Node or a Key from a BaseToken collection by using the Name field and return the index of the element if found.
        /// </summary>
        /// <param name="tokens">The collection of BaseToken to search through.</param>
        /// <param name="Name">The name of the node that the method needs to find.</param>
        /// <param name="CaseSensitive">Indicates if the name argument and the name of the BaseToken needs to be an exact match in terms of capitalization.</param>
        /// <param name="throwErrorIfNotFound">Throw an exception if the BaseToken could not be found. If false, method will return -1 if element could not be found.</param>
        /// <returns></returns>
        internal static int FindBaseTokenIndex(this IEnumerable<BaseToken> tokens, string Name, bool CaseSensitive = false, bool throwErrorIfNotFound = false)
        {
            int tokensLength = tokens.Count(); //Store the length of our tokens list to a variable 
            if (!CaseSensitive) //If CaseSensitve is set to false, then we convert the Name argument to lower case.
                Name = Name.ToLower();
            for (int i = 0; i < tokensLength; ++i) 
            {
                string tokenName; //This will store the name of the BaseToken that we are checking.
                BaseToken currentToken = tokens.ElementAt(i); //Cache the BaseToken that we are working with.
                if (CaseSensitive) //If CaseSensitive is set to true, set the tokenName variable to the name of the currentToken without doing any string manipulation.
                    tokenName = currentToken.Name;
                else //If CasenSensitive is set  to false, set the tokenName variable to the lower case equivalent of the currentToken's name.
                    tokenName = currentToken.Name.ToLower();

                if (tokenName == Name) 
                    return i;
            }
            if (throwErrorIfNotFound) //We're done looping through our collection and we haven't found the currentToken. 
                throw new TokenNotFoundException(Name + " has not been found in the collection!");
            return -1; //If throwError is set to false, then return -1 instead.
        }

    }

    public class TokenNotFoundException : Exception
    {
        public TokenNotFoundException(string Message) : base(Message)
        {

        }
    }
}
