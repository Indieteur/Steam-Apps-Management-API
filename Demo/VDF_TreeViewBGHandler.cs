using Indieteur.VDFAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Demo
{
    public partial class frmDemoVDF : Form
    {
        void LoadVDFDataToTreeView(VDFData _vdfdata)
        {
            tViewData.Nodes.Clear(); //Clear our TreeView
            if (_vdfdata.Nodes != null) //Check if our vdfData has any nodes.
            {
                foreach (VDFNode vNode in _vdfdata.Nodes) //Loop through the root nodes of our VDFData Structure
                {
                    LoadVDFNodeToTreeView(vNode);
                }
            }
            if (tViewData.Nodes.Count > 0) //Make sure to expand our first node if it exists.
                tViewData.Nodes[0].Expand();
        }

        void LoadVDFNodeToTreeView(VDFNode node, TreeNode parent = null)
        {
            TreeNode newTreeNode = new TreeNode(node.Name); //Create our TreeNode
            newTreeNode.Tag = new TreeNodeVDFTag(TreeNodeVDFTag.Type.Node, node); //Set the tag of our new TreeNode to our VDFNode using our TreeNodeVDFTag which will help us identify which BaseToken we have.
            if (node.Keys != null)//Check if the keys list of our VDFNode is null
            {
                foreach (VDFKey key in node.Keys)
                {
                    LoadVDFKeyToTreeView(key, newTreeNode); //Loop through each child keys of our VDFNode and add them to our newTreeNode child nodes recursively.
                }
            }
            if (node.Nodes != null) //Check if the nodes list of our VDFNode is null
            {
                foreach (VDFNode childNode in node.Nodes)
                {
                    LoadVDFNodeToTreeView(childNode, newTreeNode);
                }
            }


            if (parent == null) //Check if the parent argument is set to null. If it is, it means that our node is a root node. Add them directly to the Treeview. Otherwise, add them to the parent TreeViewNode.
                tViewData.Nodes.Add(newTreeNode);
            else
                parent.Nodes.Add(newTreeNode);
        }

        void LoadVDFKeyToTreeView(VDFKey key, TreeNode parent)
        {
            if (parent == null) //Key must have a parent node. Throw an error if parent is set to null.
                throw new ArgumentNullException("parent argument cannot be null!");
            TreeNode newTreeNode = new TreeNode(key.Name); //Create our TreeNode
            newTreeNode.Tag = new TreeNodeVDFTag(TreeNodeVDFTag.Type.Key, key);
            parent.Nodes.Add(newTreeNode); //Add our key to the parent TreeNode.
        }

        TreeNode AddRootNode(string Name, VDFData _vdfdata)
        {
            VDFNode newNode; //Declare our newNode type VDFNode variable which we will be used by our CreateNewNode to pass back our newly created VDFNode.
            TreeNode newTreeNode = CreateNewNode(Name, _vdfdata, null, out newNode); //Call the method which will do the job of creating a TreeNode and VDFNode for us.
            if (_vdfdata.Nodes == null) //Check if the nodes list under VDFData is set to null. Throw an error if it is.
                throw new NullReferenceException("Nodes list of VDFData class is set to null!");
            _vdfdata.Nodes.Add(newNode); //Add our newNode to the list of nodes under VDFData.
            tViewData.Nodes.Add(newTreeNode); //Do the same thing for our newTreeNode
            return newTreeNode;
        }

        TreeNode AddNodeToNode(string Name, VDFData parentVDFDataStructure, TreeNodeVDFTag treeNodeTag, TreeNode nodeSelected)
        {
            TreeNode newTreeNode; //Variable that will store our new tree node.
            if (treeNodeTag.TagType == TreeNodeVDFTag.Type.Node) //If we have the node itself selected on our Treeview, we just need to call the AddNodeToNode method directly.
            {
                newTreeNode = AddNodeToNode(Name, nodeSelected, parentVDFDataStructure, treeNodeTag.Token as VDFNode);
            }
            else
            {
                //We will need to retrieve the parent of this key for this one if the key is the one that is selected. 
                if (nodeSelected.Parent == null) //Make sure that the parent of the TreeNode is set to another TreeNode
                    throw new NullReferenceException("TreeNode " + nodeSelected.Text + " parent property is not set!");
                VDFKey selectedKey = treeNodeTag.Token as VDFKey; //Cast our token to key first.
                if (selectedKey.Parent == null) //Make sure to check if the parent property was set to a VDFNode
                    throw new NullReferenceException("Parent property of key " + selectedKey.Name + " is not set!");
                VDFNode parentNode = selectedKey.Parent; //Get its parent
                //We have all that we need.
                newTreeNode = AddNodeToNode(Name, nodeSelected.Parent, vdfData, parentNode);
            }
            return newTreeNode;
        }

        TreeNode AddNodeToNode(string Name, TreeNode Parent, VDFData parentVDFDataStructure, VDFNode nodeParent)
        {
            VDFNode newNode; //Declare our newNode type VDFNode variable which we will be used by our CreateNewNode to pass back our newly created VDFNode.
            TreeNode newTreeNode = CreateNewNode(Name, parentVDFDataStructure, nodeParent, out newNode); //Call the method which will do the job of creating a TreeNode and VDFNode for us.
            if (nodeParent.Nodes == null) //Check if the nodes list under vdfnode is set to null. Throw an error if it is.
                throw new NullReferenceException("Nodes list of VDFData class is set to null!");
            nodeParent.Nodes.Add(newNode); //Add our newNode to the list of nodes under parent vdfnode.
            Parent.Nodes.Add(newTreeNode); //Do the same thing for our newTreeNode
            return newTreeNode;
        }

        TreeNode CreateNewNode(string Name, VDFData parentVDFStructure, VDFNode parentNode, out VDFNode newNode)
        {
            newNode = new VDFNode(Name, parentVDFStructure, parentNode); //Create our new VDFNode first which will be referenced by our newNode argument (which will be passed back to the calling method.)
            TreeNode newTreeNode = new TreeNode(Name); //Create our TreeNode.
            newTreeNode.Tag = new TreeNodeVDFTag(TreeNodeVDFTag.Type.Node, newNode); //Set the tag to our newNode. Make sure to use VDFTag.
            return newTreeNode; //return the newTreeNode
        }


        TreeNode CreateNewKey(string Name, string Value, TreeNodeVDFTag treeNodeTag, TreeNode nodeSelected)
        {
            TreeNode newTreeNode;
            if (treeNodeTag.TagType == TreeNodeVDFTag.Type.Node) //If the tag type of our selected tree node is a VDFNode. Then it's quite easy to create a new key for us. Just need to call the CreateNewKey method.
            {
                newTreeNode = CreateNewKey(Name, Value, treeNodeTag.Token as VDFNode, nodeSelected);
            }
            else
            {
                //We will need to retrieve the parent of this key for this one if the key is the one that is selected. 
                if (nodeSelected.Parent == null) //Make sure that the parent of the TreeNode is set to another TreeNode
                    throw new NullReferenceException("TreeNode " + nodeSelected.Text + " parent property is not set!");
                VDFKey selectedKey = treeNodeTag.Token as VDFKey; //Cast our token to key first.
                if (selectedKey.Parent == null) //Make sure to check if the parent property was set to a VDFNode
                    throw new NullReferenceException("Parent property of key " + selectedKey.Name + " is not set!");
                VDFNode parentNode = selectedKey.Parent; //Get its parent
                //We have all that we need.
                newTreeNode = CreateNewKey(Name, Value, parentNode, nodeSelected.Parent );
            }
            return newTreeNode;
        }
        TreeNode CreateNewKey(string Name, string Value, VDFNode parent, TreeNode parentTreeNode)
        {
            if (parent.Keys == null) //Make sure that the keys list of our parent node is instantiated.
                throw new NullReferenceException("Keys list of " + parent.Name + " node is set to null!");
            VDFKey newKey = new VDFKey(Name, Value, parent); //Create our key with the values passed on to us as arguments.
            parent.Keys.Add(newKey); //Add the newKey to the list of keys that our parent node have.
            TreeNode newTreeNode = new TreeNode(Name); //Create our new treenode.
            newTreeNode.Tag = new TreeNodeVDFTag(TreeNodeVDFTag.Type.Key, newKey); //Store our VDFKey on the tag property of our newTreeNode. Make sure to use the TreeNodeVDFTag.
            parentTreeNode.Nodes.Add(newTreeNode); //Add our new TreeNode to the parentTreeode
            return newTreeNode; //Return our newTreeNode back to the calling method.
        }

        void DeleteVDFNode(VDFNode nodeToDelete, TreeNode treeNodeToDelete)
        {
            if (nodeToDelete.Parent == null) //If Parent property of the nodeToDelete is null then most likely we are a root node.
            {
                if (nodeToDelete.ParentVDFStructure == null) //If our node ParentVDFStructure is set to null, then our node is oprhaned which shouldn't be the case. Throw an error.
                    throw new NullReferenceException("ParentVDFStructure property of Node " + nodeToDelete.Name + " is not set!");
                nodeToDelete.RemoveNodeFromParent(FullRemovalFromTheVDFStruct: true);
                tViewData.Nodes.Remove(treeNodeToDelete); //No need to do checks for our TreeNode as we can most certainly assume that our TreeNode is a root node as well.
            }
            else //If the nodeToDelete's parent property is set to another VDFNode then just remove it from the child nodes list of the parent. Do the same for the treenode.
            {
                nodeToDelete.RemoveNodeFromParent();
                treeNodeToDelete.Parent.Nodes.Remove(treeNodeToDelete);
            }
           
        }

        void DeleteVDFKey(VDFKey keyToDelete, TreeNode treeNodeToDelete)
        {
            if (keyToDelete.Parent == null) //A key will always have a parent node. If that's not the case, throw an error.
                throw new NullReferenceException("Parent property of Key " + keyToDelete.Name + " is not set!");
            keyToDelete.RemoveKeyFromNode();
            treeNodeToDelete.Parent.Nodes.Remove(treeNodeToDelete); //Our treeNodeToDelete treeNode will always have a parent node as it is a key.
        }


        void UpdateTokenInfo(TreeNodeVDFTag treeNodeTag, TreeNode treeNodeToUpdate)
        {
            if (treeNodeTag.TagType == TreeNodeVDFTag.Type.Key) //Call the correct function by checking the type of token that we have.
                UpdateKeyInfo(txtName.Text, txtValue.Text, (VDFKey)treeNodeTag.Token, treeNodeToUpdate);
            else
                UpdateNodeInfo(txtName.Text, (VDFNode)treeNodeTag.Token, treeNodeToUpdate);
        }

        void UpdateNodeInfo(string Name, VDFNode nodeToUpdate, TreeNode treeNodeToUpdate)
        {
            if (Name != nodeToUpdate.Name) //To prevent initializing a new string. Check if Name is just the same as the one on the node.
            {
                nodeToUpdate.Name = Name;
                treeNodeToUpdate.Text = Name;
            }
        }

        void UpdateKeyInfo(string Name, string Value, VDFKey keyToUpdate, TreeNode treeNodeToUpdate)
        {
            if (Name != keyToUpdate.Name) //To prevent initializing a new string. Check if Name is just the same as the one on the node.
            {
                keyToUpdate.Name = Name;
                treeNodeToUpdate.Text = Name;
            }
            if (Value != keyToUpdate.Value) //Check if the value string is just the same on the VDFKey instance.
                keyToUpdate.Value = Value;
        }

    }
}
