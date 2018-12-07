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
        private void btnAddRootNode_Click(object sender, EventArgs e)
        {
            tViewData.SelectedNode = AddRootNode(DEFAULT_NODE_NAME, vdfData);
        }

        private void btnAddNode_Click(object sender, EventArgs e)
        {
            if (AddRemoveSaveButtonErrorHandling())
                return;
            tViewData.SelectedNode = AddNodeToNode(DEFAULT_NODE_NAME,  vdfData, selectedToken, tViewData.SelectedNode);
        }

        private void btnAddKey_Click(object sender, EventArgs e)
        {
            if (AddRemoveSaveButtonErrorHandling())
                return;
            tViewData.SelectedNode = CreateNewKey(DEFAULT_KEY_NAME, DEFAULT_KEY_VALUE, selectedToken,  tViewData.SelectedNode);
        }


        private void btnDeleteNode_Click(object sender, EventArgs e)
        {
            if (AddRemoveSaveButtonErrorHandling())
                return;
            DeleteVDFNode(selectedToken.Token as VDFNode, tViewData.SelectedNode); //We can be sure that the treenode we have selected is a VDFNode as the btnDeleteNode will not be enabled otherwise.
            if (tViewData.Nodes.Count == 0)
                ResetGUIAndVariables(); //Make sure to reset the gui and the variables related to the selected node afterwards if there's nothing else to select.

        }
        private void btnDeleteKey_Click(object sender, EventArgs e)
        {
            if (AddRemoveSaveButtonErrorHandling())
                return;
            DeleteVDFKey(selectedToken.Token as VDFKey, tViewData.SelectedNode); //We can be sure that the treenode we have selected is a VDFKey as the btnDeleteKey will not be enabled otherwise.
        } 

        private void btnRevertInfo_Click(object sender, EventArgs e)
        {
            GUIUpdate_RevertInfo(selectedToken);
            
        }
        private void btnSaveInfo_Click(object sender, EventArgs e)
        {
            if (AddRemoveSaveButtonErrorHandling())
                return;
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Name of Token cannot be empty or whitespace!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            UpdateTokenInfo(selectedToken, tViewData.SelectedNode);
        }
    }
}
