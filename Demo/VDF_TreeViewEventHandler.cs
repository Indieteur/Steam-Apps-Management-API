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
        private void tViewData_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node == null) //If none is selected, update our GUI.
            {
                selectedToken = null;
                GUIUpdate_NoneSelected();
                return;
            }
            selectedToken = (TreeNodeVDFTag)e.Node.Tag; //We can be 99 percent sure that the tag of the node is a TreeNodeVDFTag so we can just cast it without doing any error checking.

            if (selectedToken.TagType == TreeNodeVDFTag.Type.Node)
            {
                GUIUpdate_NodeSelected(); //Update our GUI Buttons
                GUIUpdate_LoadNodeInfo(selectedToken.Token as VDFNode); //Load information about the node on our textboxes. We can safely assume that the token will be a node as the tagtype is set to node.
            }
            else
            {
                GUIUpdate_KeySelected();
                GUIUpdate_LoadKeyInfo(selectedToken.Token as VDFKey); 
            }
        }
    }
}
