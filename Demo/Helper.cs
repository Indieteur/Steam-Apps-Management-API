using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using System.Windows.Forms;


namespace Demo
{
    static class Helper
    {
        
    }

    public partial class frmDemoVDF : Form
    {
        const string DEFAULT_NODE_NAME = "New Node"; //Will be the default name of our node when created.
        const string DEFAULT_KEY_NAME = "New Key"; //Will be the default name of our key when created.
        const string DEFAULT_KEY_VALUE = "New Value"; //Will be the default value of our key when created.

       void ResetGUIAndVariables()
        {
            GUIUpdate_NoneSelected(); //Make sure to reset our GUI
            selectedToken = null; //And nullify our selectedToken variable.
        }
        
        bool AddRemoveSaveButtonErrorHandling()
        {
            if (tViewData.SelectedNode == null)
            {
                MessageBox.Show("Update Token request failed! SelectedNode property of tViewData is not set!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                GUIUpdate_NoneSelected();
                return true;
            }
            if (selectedToken == null)
            {
                MessageBox.Show("Update Token request failed! SelectedToken variable of frmDemo is not set!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                GUIUpdate_NoneSelected();
                return true;
            }
            return false;
        }
    }
}
