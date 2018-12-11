using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Indieteur.VDFAPI
{
    public static class KeyListExtensionMethods 
    {
        /// <summary>
        ///  Finds a Key in a Key collection by using the Name field. 
        /// </summary>
        /// <param name="keys">The key collection that contains the key that the method will search for.</param>
        /// <param name="Name">The name of the key that the method will search for.</param>
        /// <param name="CaseSensitive">Indicates if the name argument and the name of the node needs to be an exact match in terms of capitalization.</param>
        /// <param name="throwErrorIfNotFound">Throw an exception if the key could not be found. If false, method will return null instead.</param>
        /// <returns></returns>
        public static VDFKey FindKey(this IEnumerable<VDFKey> keys, string Name, bool CaseSensitive = false, bool throwErrorIfNotFound = false)
        {
            BaseToken baseToken = keys.FindBaseToken(Name, CaseSensitive, throwErrorIfNotFound);
            if (baseToken != null)
                return (VDFKey)baseToken;
            return null;
        }
        /// <summary>
        ///  Finds a Key in a Key collection by using the Name field and returns the index of the key if found.
        /// </summary>
        /// <param name="keys">The key collection that contains the key that the method will search for.</param>
        /// <param name="Name">The name of the key that the method will search for.</param>
        /// <param name="CaseSensitive">Indicates if the name argument and the name of the node needs to be an exact match in terms of capitalization.</param>
        /// <param name="throwErrorIfNotFound">Throw an exception if the key could not be found.  If set to false, method will return -1 if key could be found.</param>
        /// <returns></returns>
        public static int FindKeyIndex(this IEnumerable<VDFKey> keys, string Name, bool CaseSensitive = false, bool throwErrorIfNotFound = false)
        {
            return keys.FindBaseTokenIndex(Name, CaseSensitive, throwErrorIfNotFound); //Call the base method which finds the key for us.
        }

        /// <summary>
        /// Cleanly removes a key from the list of children of the parent node.
        /// </summary>
        /// <param name="keys">The collection of keys to search through.</param>
        /// <param name="Name">The name of the key that the method needs to find.</param>
        /// <param name="CaseSensitive">Indicates if the name argument and the name of the key needs to be an exact match in terms of capitalization.</param>
        /// <param name="throwErrorIfNotFound">Throw an exception if the key could not be found.</param>
        /// <returns></returns>
        public static void CleanRemoveKey(this List<VDFKey> keys, string Name, bool CaseSensitive = false, bool throwErrorIfNotFound = false)
        {
            int i = keys.FindBaseTokenIndex(Name, CaseSensitive, throwErrorIfNotFound); //Find our node from the nodes list by calling the FindBaseTokenIndex method and pass on the arguments.
            if (i == -1) //The FindBaseTokenIndex method will do the error handling for us. However, if the argument throwErrorIfNotFound is set to false, it wouldn't do that so what'll do is exit from this method if the func returns -1.
                return;
            VDFKey tKey = keys[i];//cache our node.
            tKey.Parent = null; //Set the parent of the node to null.
            keys.RemoveAt(i); //Remove node from the parent's node list.
        }

    }


    public static class KeyExtensionMethods
    {
        /// <summary>
        /// Creates a copy of the key.
        /// </summary>
        /// <param name="key">The Key to be duplicated.</param>
        /// <param name="parent">The Node that will parent the key.</param>
        /// <returns></returns>
        public static VDFKey Duplicate(this VDFKey key, VDFNode parent)
        {
            if (parent == null) //We cannot have a key that doesn't have a parent so if the parent argument is set to null, throw an error.
                throw new ArgumentNullException("parent");
            return new VDFKey(key.Name, key.Value, parent); //Return a new instance of a key class. Copy the name and value of the original key but set the parent of the clone to the parent argument passed to this method.
        }
        /// <summary>
        /// Moves the key to another node while making sure that the Parent property is set correctly and that the key is removed from the previous parent's key list and added to the new parent's key list.
        /// </summary>
        /// <param name="key">The key to be moved.</param>
        /// <param name="newParent">The new parent of the key.</param>
        public static void Migrate(this VDFKey key, VDFNode newParent)
        {
            if (newParent == null) //We cannot have a key that doesn't have a newParent so if the parent argument is set to null, throw an error.
                throw new ArgumentNullException("parent");
            if (key.Parent == null) //Check if the Parent property of the key we are manipulating is set to null. If it is, throw an error.
                throw new NullReferenceException("The Parent property of key " + key.Name + " is set to null!");
            key.Parent.Keys.Remove(key); //Remove our key from the list of keys of the parent node .
            key.Parent = newParent; //Set the parent property of our key to our new parent.
            if (newParent.Keys == null) //If the newParent node's keys list is not yet created, we will have to instantiate it ourselves.
                newParent.Keys = new List<VDFKey>();
            newParent.Keys.Add(key); //Add the our key to the list of keys under the parent node.
        }

        /// <summary>
        /// Removes key from its parent.
        /// </summary>
        /// <param name="key">The key to be removed.</param>
        /// <param name="throwErrorOnNoParent">Throw an error if the parent property of the key is set to null.</param>
        public static void RemoveKeyFromNode(this VDFKey key, bool throwErrorOnNoParent = false)
        {
            if (key.Parent != null)
            {
                //If we have a parent node, remove the key from the list of children nodes of the parent node.
                key.Parent.Keys.Remove(key);
                key.Parent = null; //Set the parent property of our key to null.
            }
            else if (key.Parent == null && throwErrorOnNoParent) //If key's parent is set to null and the argument throwErrorOnNoParent is set to true, throw an error.
                throw new NullReferenceException("Key " + key.Name + " parent property is not set!");
        }
    }
}
