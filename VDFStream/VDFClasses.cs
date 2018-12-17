using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Indieteur.VDFAPI
{

    /// <summary>
    /// Contains keys which stores information as well as child nodes.
    /// </summary>
    public class VDFNode : BaseToken
    {

        /// <summary>
        /// List of keys under the node.
        /// </summary>
        public List<VDFKey> Keys { get; internal set; }
        /// <summary>
        /// List of children nodes under the parent node.
        /// </summary>
        public List<VDFNode> Nodes { get; internal set; }
        /// <summary>
        /// The parent VDF Data class instance of this node.
        /// </summary>
        public VDFData ParentVDFStructure { get; internal set; }


        public VDFNode()
        {
            InitializeKeysAndNodesList();
        }
        public VDFNode(string name, VDFData parentVDFStructure, VDFNode parent = null)
        {
            Name = name;
            Parent = parent;
            ParentVDFStructure = parentVDFStructure;
            InitializeKeysAndNodesList();
        }


        void InitializeKeysAndNodesList()
        {
            //Create our Keys and Nodes List.
            Keys = new List<VDFKey>();
            Nodes = new List<VDFNode>();

        }


        /// <summary>
        /// Creates a VDF parsable string which contains this node and its children (Keys and Nodes).
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
        /// <param name="TabLevel">Indicates how many tab characters should be appended at the beginning of the name of the node, the curly brackets and the key-value pair.</param>
        /// <returns></returns>
        public string ToString(Delimiters delimiter, int TabLevel = 0)
        {
            string strDelimiter = Helper.DelimiterEnumToString(delimiter); //Convert the selected delimiter to its String Value
            string tab = (TabLevel > 0) ? Helper.Tabify(TabLevel) : ""; //Append horizontal tab(s) at the beginning of our strings.



            StringBuilder sb = new StringBuilder(tab + "\"" + Helper.UnformatString(Name) + "\""); //Begin building our string by starting with the name of our VDFData

            sb.Append(strDelimiter + tab + "{" + strDelimiter); // Append the delimiter and then the '{' character which tells us that we are within the contents of the node and then another delimiter
            if (TabLevel >= 0)
                ++TabLevel; //Make sure to increase the TabLevel if it isn't set to a negative number.

         
            if (Keys != null)
                foreach (VDFKey key in Keys) //Append all the keys under this node to the string
                {
                    sb.Append(key.ToString(TabLevel) + strDelimiter); //Append the string value of the single key element. We must also make sure that the string returned has the correct amount of tabs at the beginning.
                }
            
            if (Nodes != null)
                foreach (VDFNode node in Nodes) //Append all the nodes under this node and their children to the string
                {
                    sb.Append(node.ToString(delimiter, TabLevel) + strDelimiter); //We must make sure that the child nodes have the same styling as their parent node so pass on the delimiter and the tab level.
                }

            sb.Append(tab + "}"); //Close off our node with '}'. Also, make sure that the correct number of tabs is appended before the closing curly brackets.
            return sb.ToString(); 
        }
    }

    /// <summary>
    /// A Key-Value pair found inside a node.
    /// </summary>
    public class VDFKey : BaseToken
    {
        /// <summary>
        /// Value of the Key.
        /// </summary>
        public string Value { get; set; }

        public VDFKey(string name, string value, VDFNode parent)
        {
            Name = name ?? throw new VDFStreamException("Name of the Key cannot be Null!");

            Parent = parent ?? throw new ArgumentNullException("parent");
            Value = value;
        }
        /// <summary>
        /// Creates a VDF key string from the name and value field.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return ToString(0);  //Call the other ToString overload method, the default TabLevel is 0.
        }
        /// <summary>
        /// Creates a VDF key string from the name and value field and appends tab character(s) at the beginning of the string.
        /// </summary>
        /// <param name="TabLevel">Indicates how many times should a tab character be added. Set this to 0 or a negative number if you don't want to append a tab character.</param>
        /// <returns></returns>
        public string ToString(int TabLevel)
        {
            string tab = (TabLevel > 0) ? Helper.Tabify(TabLevel) : ""; //If tab level is greater than 0 then we call the tabify helper method if not, just set the tab string variable to "".
            return tab + "\"" + Helper.UnformatString(Name) + "\" \"" + Helper.UnformatString(Value) + "\""; //Make sure to use the correct format for the Name and Value variables.
        }

    }

    public abstract class BaseToken
    {
        /// <summary>
        /// Name of the Key.
        /// </summary>
        public string Name
        {
            get
            {

                return _name;

            }
            set
            {
                _name = value ?? throw new VDFStreamException("Name cannot be Null!"); 
            }
        }
        /// <summary>
        /// Returns the parent of this key or node.
        /// </summary>
        public VDFNode Parent { get; internal set; }
        string _name; //The actual variable storing the name of our node or key.
    }



}
