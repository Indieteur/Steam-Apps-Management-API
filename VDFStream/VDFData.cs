using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Indieteur.VDFAPI
{
    /// <summary>
    /// A VDF Data Structure Class.
    /// </summary>
    public class VDFData
    {
        const string SMALLEST_VDFDATA = "a{\n}"; //Define the smallest possible VDF data structure that we can encounter.

        /// <summary>
        /// List of root nodes in the VDF Data Structure.
        /// </summary>
        public List<VDFNode> Nodes { get; private set; }

        /* Reader Variables*/
        VDFNode currentNode; //The node we are currently working on. 
        StringBuilder sb; //This should be more efficient than concatenating chars to form one string for our current task

        Mode currentMode = Mode.none; //The mode that the reader is currently on.

        string previousString;
        int lineCounter = 1;
        int characterCount = 1;

        /// <summary>
        /// Creates an empty VDF Data Structure.
        /// </summary>
        public VDFData()
        {
            Nodes = new List<VDFNode>();
        }

        /// <summary>
        /// Creates a VDF Data Structure and adds a single node to it.
        /// </summary>
        /// <param name="nodeToAdd">The node to be added to the Nodes List.</param>
        public VDFData(VDFNode nodeToAdd)
        {
            LoadData(nodeToAdd);   
        }

        /// <summary>
        /// Creates a VDF Data Structure and copies the elements of the specified collection to the Nodes List of this instance.
        /// </summary>
        /// <param name="nodesToAdd">The element to be copied to the Nodes List.</param>
        public VDFData(IEnumerable<VDFNode> nodesToAdd)
        {
            LoadData(nodesToAdd);
        }

        /// <summary>
        /// Parses a file or a string into VDF.
        /// </summary>
        /// <param name="data">The path of the file or the string that will be parsed.</param>
        /// <param name="dataIsPath">If true, the data argument will be treated as a path to a file. If false, it will be treated as data.</param>
        public VDFData(string data, bool dataIsPath = true)
        {
            LoadData(data, dataIsPath);
        }
        /// <summary>
        /// Parses a character array to a VDF Data.
        /// </summary>
        /// <param name="stream"></param>
        public VDFData(char[] stream)
        {
            LoadData(stream); //Call the LoadData method and pass on the stream argument.
        }

        /// <summary>
        /// Creates a VDF Data Structure and adds a single node to it. NOTE: This clears the current list of root nodes.
        /// </summary>
        /// <param name="nodeToAdd">The node to be added to the Nodes List.</param>
        public void LoadData(VDFNode nodeToAdd)
        {

            Nodes = new List<VDFNode>(1); //We are adding nodeToAdd to the list so let's explicitly instantiate a List<Node> with the length of 1 element.
            nodeToAdd.ParentVDFStructure = this; //Change the ParentVDFStructure property of the added node to this instance of VDFData.
            Nodes.Add(nodeToAdd); //Add nodeToAdd to the list.

        }

        /// <summary>
        /// Creates a VDF Data Structure and copies the elements of the specified collection to the Nodes List of this instance. NOTE: This clears the current list of root nodes.
        /// </summary>
        /// <param name="nodesToAdd">The element to be copied to the Nodes List.</param>
        public void LoadData(IEnumerable<VDFNode> nodesToAdd)
        {
            Nodes = new List<VDFNode>(nodesToAdd);
            foreach (VDFNode node in Nodes) //Change the ParentVDFStructure property of each of the added nodes to this instance of VDFData.
            {
                node.ParentVDFStructure = this;
            }
        }

        /// <summary>
        /// Parses a file or a string into VDF. NOTE: This clears the current list of root nodes.
        /// </summary>
        /// <param name="data">The path of the file or the string that will be parsed.</param>
        /// <param name="dataIsPath">If true, the data argument will be treated as a path to a file. If false, it will be treated as data.</param>
        public void LoadData(string data, bool dataIsPath = true)
        {
            if (dataIsPath) //If dataIsPath is set to true, it means the data argument tells us the location of the file that contains the actual data.
            {
                if (!File.Exists(data)) //Check if file exists. Throw an error if it doesn't.
                    throw new FileNotFoundException("File " + data + " is not found!");
                LoadData(File.ReadAllText(data).ToCharArray()); //Read all the contents of the file and convert it into a char array which we can then parse using the LoadData function.
            }
            else //If dataIsPath is set to false, it means that the data argument contains the data that we want to parse.
            {
                LoadData(data.ToCharArray()); //We just need to convert the data to a character array and then parse it using the LoadData method.
            }
        }

        /// <summary>
        /// Parses a character array into VDF Data. NOTE: This clears the current list of root nodes.
        /// </summary>
        /// <param name="stream">The character array to be parsed.</param>
        public void LoadData(char[] stream)
        {
            if (stream.Length < SMALLEST_VDFDATA.Length) //Check if the length of the char array is equal to or larger than the smallest VDF data structure possible. If it isn't, we can safely assume that it is not a VDF data thus we can exit from the constructor.
                throw new VDFStreamException("Provided data is not a valid VDF Data structure!");
            ResetReaderVariables(); //Reset the variables needed and initalize the Nodes list.

            int i = 0;
            while (i < stream.Length) //Loop through the our characters array
            {
                char curChar = stream[i]; //To avoid confusion, let's create a variable that will hold the current character we are working with.

                if (curChar == '\n') //Check if the current character is a new line. If it is, we will try to create a new token or key from the current contents of our stringbuilder and previousString
                {
                    ++lineCounter; //We should keep track of the line that we are currently in case we have some errors.
                    characterCount = 0; //We should also  keep track of the position of the character that we are working on in the line (Not in the stream array)
                    currentMode = Mode.none; //We can consider the new line as a delimiter as it is a whitespace as well. We set the mode to none.
                    TryNewTokenOrKey(false); //Everytime we hit a delimiter, we must always try to create a new token or key from our stringbuilder and previousString.
                    goto IterateI; //Add 1 to both i and characterCount then proceed to the next iteration.
                }
                else if (curChar == '\\' && currentMode != Mode.comment && currentMode != Mode.squareBracketTokens) //We check if the current character is a '\' which is the beginning of an escape character. We must also make sure that we do not check this if we are in a comment block or inside square brackets.
                {
                    char nextChar = Helper.Peek(i + 1, stream); //Check what the next character is by using our Peek Helper method.
                    if (nextChar == '\0') //If next character is null. (Normally if we reached the end of the stream.)
                        throw new VDFStreamException("Incomplete escape character detected!", lineCounter, characterCount); //Throw error.
                    if (sb == null)
                        sb = new StringBuilder(); //We are almost sure that this character is a valid escape character. We just need to make sure that our Stringbuilder is not set to null.
                    sb.Append(Helper.ParseSecondPartOfEscapeChar(nextChar, lineCounter, characterCount + 1)); //Use our Helper method to convert the escape character to its Human readable character counterpart. It also checks if it is valid. If it isn't, it throws an error.
                    i += 2; //Since we already looked at the next character. No point in looking at it again. We just proceed to the character after it.
                    characterCount += 2; //Make sure that we add 2 not 1 to the character counter.
                    continue; //We continue to the next iteration.
                }

                if (currentMode == Mode.comment) //If we are inside a comment block, no need to do anything else. Continue to the next character until we are in a new line.
                    goto IterateI;
                else if (currentMode == Mode.squareBracketTokens) //If we have detected a left square bracket beforehand and we haven't detected a right square bracket yet.
                {
                    if (curChar == ']') //If our current character is the right square bracket. Reset the currentMode back to normal.
                        currentMode = Mode.none;
                    goto IterateI;
                }
                else if (currentMode == Mode.insideDoubleQuotes) //If we are inside a double quote.
                {
                    if (curChar == '"') //If we are already found a '"' before and we found it again, it means that we are closing a token.
                    {
                        currentMode = Mode.none; //Set the mode back to none as we aren't inside a token anymore.
                        TryNewTokenOrKey(true); //Anything inside the double quotes is a token so we set the MustHaveNewToken argument to true.
                        goto IterateI; //We know what our character is and we handled it. No need to do anything else so we proceed to the next character.
                    }
                }
                else //If we are neither inside a double quote nor a comment block.
                {
                    if (curChar == '"') //If we aren't inside a double quote already and we found '"'.
                    {
                        currentMode = Mode.insideDoubleQuotes; //Set the current mode to insideDoubleQuotes.
                        TryNewTokenOrKey(false); //Lets try a new token or key as the contents of the Stringbuilder might not have been turn into a token yet. (e.g. It wasn't inside a double quote.)
                        sb = new StringBuilder();
                    }
                    else if (curChar == '[') //Sometimes VDF files have bracketed tokens after a token. There's no information about what these do in the steam documentation. Ignore it for now.
                    {
                        currentMode = Mode.squareBracketTokens; 
                        TryNewTokenOrKey(false);
                    }
                    else if (curChar == '/' && Helper.Peek(i + 1, stream) == '/') //If we our current character is '/' and the next character is '/' as well, then we are in a comment block.
                    {

                        currentMode = Mode.comment; //Set the mode to comment so any succeeding characters will be ignored until we are in a new line.
                        TryNewTokenOrKey(false); //Try a new token or key. The contents of the Stringbuilder and/or previousString might not have been set yet. Normally, it's because the previous characters weren't inside a double quote.

                        i += 2; //We already looked at the next character so we skip it and proceed to the character following it.
                        characterCount += 2; //Add 2 to the character count as well.
                        continue; //Proceed to the character after the next character.

                    }
                    else if (curChar == '{') //If the character is '{'
                    {
                        if (previousString == null) //Let's first check if the StringBuilder hasn't been flushed yet.
                        {
                            if (sb == null) //If the StringBuilder is null, throw an error. This happens when there's no previous word/character before '{' that isn't a part of a key already. Basically, our node doesn't have a name which isn't good. 
                                throw new VDFStreamException("Node name is set to null!", lineCounter, characterCount);
                            SetPreviousStringToStringBuilder(); //Set our previousString variable to the contents of the StringBuilder.
                        }

                        VDFNode newNode = new VDFNode(previousString, this); //If we detected a '{', it means we are creating a new node so let's do it.
                        
                        previousString = null; //We already processed the previousString and it's the name of our node so set the variable to null.
                        if (currentNode != null) //Check if we are already inside another node.
                        {
                            newNode.Parent = currentNode; //If we are, then the node we are creating is the children of our current node so let's set the parent reference of the new node to our current node.
                            currentNode.Nodes.Add(newNode); //We must do the same for our currentNode but we have to add the new node to the list of children nodes of our current node this time.
                        }
                        else
                            Nodes.Add(newNode); //If we aren't, it means that the node we are creating is a root node so we have to add the new node to the nodes list of this class.
                        
                            
                        currentNode = newNode; //Set the node that we are working on to our new node.
                    }
                    else if (curChar == '}') //If the character is '}'
                    {
                        TryNewTokenOrKey(false); //First, we must make sure that the Stringbuilder and the previousString has been taken care of. 
                        if (currentNode.Parent != null)
                            currentNode = currentNode.Parent; //Now we check if our current node has a parent. If it does, we set the current node that we are working on to the parent node.
                        else
                            currentNode = null; //If it doesn't have any parent, it means that the node we are working on is a root node. We have to make sure that we set the currentNode to null as we have already reached the end of this current node.
                        
                    }
                    else if (char.IsWhiteSpace(curChar)) //If the character is a whitespace
                        TryNewTokenOrKey(false); //Since whitespaces are delimiters, we need to check if the Stringbuilder and the previousString has been taken care of. Sometimes, it isn't due to the previous word/characters before this whitespace isn't inside a double quotation.
                    else
                        goto AppendChar; //If the character isn't any of the above, then it's part of a token so we append it to our StringBuilder.
                    goto IterateI;
                }

                AppendChar:
                if (sb == null)
                    sb = new StringBuilder(); //If StringBuilder isn't initialized, initialize it to start a new token.
                sb.Append(curChar); //Append the current character.

                IterateI:
                ++i; //Add 1 to i to go to the next character.
                ++characterCount; //Add 1 to the character counter as well in case we have any errors.
            }
            if (currentNode != null)
                throw new VDFStreamException("\"}\" expected.", lineCounter, characterCount);
        }

        /// <summary>
        /// Saves VDF Data Structure to file.
        /// </summary>
        /// <param name="FilePath">Indicates the path to where the file will be saved.</param>
        /// <param name="Overwrite">If set to true and the file already exists, overwrite it. If set to false and the file exists already, the method will throw an error.</param>
        public void SaveToFile (string FilePath, bool Overwrite = false)
        {
            SaveToFile(FilePath, Delimiters.SystemDefault, 0, Overwrite);
        }
        /// <summary>
        /// Saves VDF Data Structure to file.
        /// </summary>
        /// <param name="FilePath">Indicates the path to where the file will be saved.</param>
        /// <param name="delimiter">Indicates the delimiter to be appended after the name of the node, the curly brackets and the key-value pair.</param>
        /// <param name="StartingTabLevel">Indicates how many tab characters should be appended at the beginning of the name of the node, the curly brackets and the key-value pair.</param>
        /// <param name="Overwrite">If set to true and the file already exists, overwrite it. If set to false and the file exists already, the method will throw an error.</param>
        public void SaveToFile (string FilePath, Delimiters delimiter, int StartingTabLevel = 0, bool Overwrite = false)
        {
            if (!Overwrite && File.Exists(FilePath))
                throw new VDFStreamException("File " + FilePath + " already exists!");
            File.WriteAllText(FilePath, ToString(delimiter, StartingTabLevel));
        }

        /// <summary>
        /// Returns the parsable string equivalent of the VDF Data Structure.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return ToString(Delimiters.SystemDefault); //Call the other ToString overload method, pass on a default value for the delimiter argument.
        }

        /// <summary>
        /// Creates a VDF string which contains this node and its children (Keys and Nodes).
        /// </summary>
        /// <param name="delimiter">Indicates the delimiter to be appended after the name of the node, the curly brackets and the key-value pair.</param>
        /// <param name="StartingTabLevel">Indicates how many tab characters should be appended at the beginning of the name of the node, the curly brackets and the key-value pair.</param>
        /// <returns></returns>
        public string ToString(Delimiters delimiter, int StartingTabLevel = 0)
        {
            string strDelimiter = Helper.DelimiterEnumToString(delimiter); //Convert the selected delimiter to its String Value
            StringBuilder sb = new StringBuilder(); //Instantiate our StringBuilder

            //Make sure that the Nodes List is set to an object.
            if (Nodes != null)
                for (int i = 0; i < Nodes.Count; ++i)
                {
                    VDFNode node = Nodes[i]; //Cache the node we are currently working on.
                    if (i == Nodes.Count - 1) //We are on the final node. Set the strDelimiter to "" or empty so that we don't have a useless character at the end of the string.
                        strDelimiter = "";
                    sb.Append(node.ToString(delimiter, StartingTabLevel) + strDelimiter); //We must make sure that the child nodes have the same styling as their parent node so pass on the delimiter and the starting tab level.
                }
            return sb.ToString(); //return our built string.
        }

        /* Helper Functions for the reader */

        /// <summary>
        /// Resets all the variables needed by the reader to perform its task.
        /// </summary>
        void ResetReaderVariables()
        {
            if (Nodes == null)
                Nodes = new List<VDFNode>(); //Create new nodes list if it is null
            else
                Nodes.Clear(); //Just clear the nodes list if it is set already.
            //Reset all the variables needed by the reader.
            currentNode = null;
            sb = null;
            currentMode = Mode.none;
            previousString = null;
            lineCounter = 1;
            characterCount = 1;
        }

        /// <summary>
        /// Try to create a new token or key from the string in the StringBuilder and PreviousString.
        /// </summary>
        /// <param name="MustHaveNewToken">Indicates if a new token is required to be created using the StringBuilder.</param>
        void TryNewTokenOrKey(bool MustHaveNewToken)
        {
            if (previousString != null) //If we already have a string before this one that isn't part of another key or isn't the name of another node.
            {
                string newToken = ""; //This will contain the value of our new token.
                if (MustHaveNewToken && sb != null) //If we are required to have a new token and the StringBuilder isn't empty.
                    newToken = sb.ToString(); //Set the newToken to the value of the StringBuilder.
                else if (!MustHaveNewToken) //If we aren't required to have a new token.
                {
                    if (sb == null) //If stringbuilder is not instantiated, then we just exit of the function. We don't need to do anything.
                        return;
                    else //If it has conetents, then we set the newToken to the contents of the stringBuilder.
                        newToken = sb.ToString();
                }
                
                //Since we have a unhandled previousString, it means that the current token we are working on and the previous one is a key-value pair. If that's the case, then it needs to be inside a node so we check that.
                if (currentNode == null)
                    throw new VDFStreamException("Key-value pair must be inside a node!", lineCounter, characterCount); //Throw error if we aren't inside a noed.
                VDFKey key = new VDFKey(previousString, newToken, currentNode); //Create our key-value pair using the previousString and the newToken variable.
                currentNode.Keys.Add(key); //Add our newly created key to the current node we are working on.
                previousString = null; //We already know that the previous String is the name of the key and it has already been parsed, so set the referencing variable to null.
                sb = null; //We do the same with stringbuilder.
            }
            else //If we haven't found any string yet or the previous strings have already been parsed.
            {
                if (sb == null) //If Stringbuilder is not instantiated.
                {
                    if (MustHaveNewToken) //If we are required to have a new token ad the stringbuilder is null, we throw an error.
                        throw new VDFStreamException("Node name or Key name is set to null!", lineCounter, characterCount);
                    else //If not, we just exit of the function.
                        return;
                }
                SetPreviousStringToStringBuilder(); //We do not know if this string is a name of a node or a name of a key so lets just store it for now and proceed to the next characters in order to find out.
            }

        }

        /// <summary>
        /// Sets the variable previousString to the resulting string from the StringBuilder.
        /// </summary>
        void SetPreviousStringToStringBuilder()
        {
            previousString = sb.ToString(); //Set the previousString variable to the value of the stringbuilder.
            //if (string.IsNullOrWhiteSpace(previousString)) //Since the previousString can only be a node name or a key name, then it should not be empty or whitespace. We need to check if that's the case and throw an error if it isn't.
            //    throw new VDFStreamException("Node name or Key name is empty!", lineCounter, characterCount);
            sb = null; //We already have cached the value of the stringbuilder so we set it to null to handle the next token.
        }

        /* enumerations */

        /// <summary>
        /// The current mode of the reader.
        /// </summary>
        enum Mode
        {
            /// <summary>
            /// The reader is inside a comment block.
            /// </summary>
            comment,
            /// <summary>
            /// The reader is inside a pair of double quotes.
            /// </summary>
            insideDoubleQuotes,
            /// <summary>
            /// The reader is inside a pair of square brackets.
            /// </summary>
            squareBracketTokens,
            none
        }
    }



}

