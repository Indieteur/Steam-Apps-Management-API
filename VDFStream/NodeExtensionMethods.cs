using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Indieteur.VDFAPI
{
    public static class NodesListExtensionMethod
    {
        /// <summary>
        /// Finds a node in a node collection by using the Name field.
        /// </summary>
        /// <param name="nodes">The collection of nodes to search through.</param>
        /// <param name="Name">The name of the node that the method needs to find.</param>
        /// <param name="CaseSensitive">Indicates if the name argument and the name of the node needs to be an exact match in terms of capitalization.</param>
        /// <param name="throwErrorIfNotFound">Throw an exception if the node could not be found. If false, method will return null instead.</param>
        /// <returns></returns>
        public static VDFNode FindNode(this IEnumerable<VDFNode> nodes, string Name, bool CaseSensitive = false, bool throwErrorIfNotFound = false)
        {
            BaseToken baseToken = nodes.FindBaseToken(Name, CaseSensitive, throwErrorIfNotFound); //Use the baseToken GetBaseToken extension method to search for the node.
            if (baseToken != null) //If the result isn't null, we return the Node that was found. We must cast it back to a node type as well.
                return (VDFNode)baseToken;
            return null;
        }
        /// <summary>
        /// Finds a node in a node collection by using the Name field and returns the index of the node if found.
        /// </summary>
        /// <param name="nodes">The collection of nodes to search through.</param>
        /// <param name="Name">The name of the node that the method needs to find.</param>
        /// <param name="CaseSensitive">Indicates if the name argument and the name of the node needs to be an exact match in terms of capitalization.</param>
        /// <param name="throwErrorIfNotFound">Throw an exception if the node could not be found. If set to false, method will return -1 if node could not be found.</param>
        /// <returns></returns>
        public static int FindNodeIndex(this IEnumerable<VDFNode> nodes, string Name, bool CaseSensitive = false, bool throwErrorIfNotFound = false)
        {
            return nodes.FindBaseTokenIndex(Name, CaseSensitive, throwErrorIfNotFound); //Call the base method which finds the node for us.
        }

        /// <summary>
        /// Cleanly removes a node from the list of children of the parent node.
        /// </summary>
        /// <param name="nodes">The collection of nodes to search through.</param>
        /// <param name="Name">The name of the node that the method needs to find.</param>
        /// <param name="CaseSensitive">Indicates if the name argument and the name of the node needs to be an exact match in terms of capitalization.</param>
        /// <param name="FullRemovalFromTheVDFStruct">If true, the ParentVDFStructure property of the node will be set to null as well. Always set this to true if the node is a root node.</param>
        /// <param name="throwErrorIfNotFound">Throw an exception if the node could not be found.</param>
        /// <returns></returns>
        public static void CleanRemoveNode(this List<VDFNode> nodes, string Name, bool CaseSensitive = false, bool FullRemovalFromTheVDFStruct = false, bool throwErrorIfNotFound = false)
        {
            int i = nodes.FindBaseTokenIndex(Name, CaseSensitive, throwErrorIfNotFound); //Find our node from the nodes list by calling the FindBaseTokenIndex method and pass on the arguments.
            if (i == -1) //The FindBaseTokenIndex method will do the error handling for us. However, if the argument throwErrorIfNotFound is set to false, it wouldn't do that so what'll do is exit from this method if the func returns -1.
                return;
            VDFNode tNode = nodes[i];//cache our node.
            tNode.Parent = null; 
            nodes.RemoveAt(i);
            if (FullRemovalFromTheVDFStruct)
                tNode.ParentVDFStructure = null;
        }
    }

    public static class NodeExtensionMethod
    {
        /// <summary>
        /// Creates a copy of the node and its elements.
        /// </summary>
        /// <param name="node">The Node to be duplicated.</param>
        /// <param name="parent">Indicates which node will parent the duplicate node. Set it to null if you want the duplicate node to be a root node.</param>
        /// <returns></returns>
        public static VDFNode Duplicate(this VDFNode node, VDFNode parent, VDFData parentVDFStructure)
        {
            VDFNode newNode = new VDFNode(node.Name, parentVDFStructure, parent); //Create our clone node and set it's name to the name of the original node but set the parent to the one specified by the calling method.
           
                //Do a null check for the Nodes and Keys List of the original node. If they aren't null, loop through the elements of the lists and call the duplicate method for each elements. Make sure to set their parent to the clone node.
                if (node.Nodes != null) 
                    foreach(VDFNode curNode in node.Nodes)
                    {
                        newNode.Nodes.Add(curNode.Duplicate(newNode, parentVDFStructure));
                    }
                if (node.Keys != null)
                    foreach (VDFKey curKey in node.Keys)
                    {
                        newNode.Keys.Add(curKey.Duplicate(newNode));
                    }
            
            return newNode; 
        }
        /// <summary>
        /// Moves the selected node to a new parent node/to the root. 
        /// </summary>
        /// <param name="node">The node to be moved.</param>
        /// <param name="newParent">The new parent of the node. NOTE: If you want the node to be a root node, set this to null.</param>
        public static void Migrate(this VDFNode node, VDFNode newParent)
        {
            
            if (node.Parent != null)
            {
                node.Parent.Nodes.Remove(node); 
                node.Parent = null; 
            }
            if (newParent != null)
            {
                node.Parent = newParent;
                newParent.Nodes.Add(node);
            }
        }

        /// <summary>
        /// Removes node from its parent and/or from the VDFDataStructure.
        /// </summary>
        /// <param name="node">The node to be removed.</param>
        /// <param name="throwErrorOnNoParent">Throw an error if the parent/parentVDFStructure property of the node is set to null.</param>
        /// <param name="FullRemovalFromTheVDFStruct">If true, the ParentVDFStructure property of the node will be set to null as well. Always set this to true if the node is a root node.</param>
        public static void RemoveNodeFromParent(this VDFNode node, bool throwErrorOnNoParent = false, bool FullRemovalFromTheVDFStruct = false)
        {
            if (node.Parent != null)
            {
               
                node.Parent.Nodes.Remove(node);
                node.Parent = null;
                if (FullRemovalFromTheVDFStruct) 
                    node.ParentVDFStructure = null;
            }
            else
            {
                if (node.ParentVDFStructure == null || !FullRemovalFromTheVDFStruct)
                    if (throwErrorOnNoParent)
                        throw new NullReferenceException("Node " + node.Name + " parent property is not set!");
                    else
                        return;
                node.ParentVDFStructure.Nodes.Remove(node);
                node.ParentVDFStructure = null;
                
            }
        }
    }
   
}
