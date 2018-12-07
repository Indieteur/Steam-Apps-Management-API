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
        void ClearDialogBoxes()
        {
            openFileDialog.FileName = "";
            saveFileDialog.FileName = "";
        }

        void GUIUpdate_NoneSelected()
        {
            GUIUpdate_ClearInfo();
            groupInfo.Enabled = false;
            GUIUpdate_SetTextNameEnabled(false);
            GUIUpdate_SetTextValueEnabled(false);
            btnSaveInfo.Enabled = false;
            btnRevertInfo.Enabled = false;
            btnAddKey.Enabled = false;
            btnAddNode.Enabled = false;
            btnDeleteKey.Enabled = false;
            btnDeleteNode.Enabled = false;
        }

        void GUIUpdate_NodeSelected()
        {
            GUIUpdate_TokenSelected();
            btnDeleteKey.Enabled = false;
            btnDeleteNode.Enabled = true;
            GUIUpdate_SetTextValueEnabled(false);
        }
        void GUIUpdate_KeySelected()
        {
            GUIUpdate_TokenSelected();
            btnDeleteKey.Enabled = true;
            btnDeleteNode.Enabled = false;
            GUIUpdate_SetTextValueEnabled(true);
        }

        void GUIUpdate_TokenSelected()
        {
            GUIUpdate_ClearInfo();
            groupInfo.Enabled = true;
            GUIUpdate_SetTextNameEnabled(true);
            btnSaveInfo.Enabled = true;
            btnRevertInfo.Enabled = true;
            btnAddKey.Enabled = true;
            btnAddNode.Enabled = true;
        }

        void GUIUpdate_SetTextValueEnabled(bool Enabled)
        {
            lblValue.Enabled = Enabled;
            txtValue.Enabled = Enabled;
        }
        void GUIUpdate_SetTextNameEnabled(bool Enabled)
        {
            lblName.Enabled = Enabled;
            txtName.Enabled = Enabled;
        }

        void GUIUpdate_ClearInfo()
        {
            txtName.Text = "";
            txtValue.Text = "";
        }
        void GUIUpdate_RevertInfo (TreeNodeVDFTag token)
        {
            if (token.TagType == TreeNodeVDFTag.Type.Key)
            {
                GUIUpdate_LoadKeyInfo(token.Token as VDFKey);
            }
            else
            {
                GUIUpdate_LoadNodeInfo(token.Token as VDFNode);
            }
        }
        void GUIUpdate_LoadNodeInfo(VDFNode node)
        {
            txtName.Text = node.Name;
        }
        void GUIUpdate_LoadKeyInfo(VDFKey key)
        {
            txtName.Text = key.Name;
            txtValue.Text = key.Value;
        }
    }
}
