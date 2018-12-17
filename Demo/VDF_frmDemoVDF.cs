using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Indieteur.VDFAPI;
using Indieteur.SAMAPI;

namespace Demo
{
    public partial class frmDemoVDF : Form
    {
        VDFData vdfData = new VDFData(); //Instance of our VDFData.
        TreeNodeVDFTag selectedToken;
        public frmDemoVDF()
        {
            InitializeComponent();
           
        }

     

        private void frmDemo_Load(object sender, EventArgs e)
        {
            string steamdir = SteamAppsManager.GetSteamDirectory(); //Set the initial directory of the openFileDialog control by calling the GetSteamDirectory static method from the SteamAppsMan class. It'll return empty if Steam Directory doesn't exist. 
            if (!string.IsNullOrWhiteSpace(steamdir)) 
                steamdir += "\\steamapps";
            openFileDialog.InitialDirectory = steamdir;
        }

        private void btnLoadfromFile_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = openFileDialog.ShowDialog(this); //Show our open file dialog and store its result on a variable.
            string fileName = openFileDialog.FileName; //Cache our filename as we are going to reset the dialogbox
            ClearDialogBoxes();
            if (dialogResult == DialogResult.Cancel) 
                return;
            ResetGUIAndVariables(); 
            vdfData.LoadData(fileName); 
            LoadVDFDataToTreeView(vdfData); 
        }

        private void btnSaveToFile_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = saveFileDialog.ShowDialog(this); //Show our save file dialog and store its result on a variable.
            string fileName = saveFileDialog.FileName; //Cache our filename as we are going to reset the dialogbox
            ClearDialogBoxes();
            if (dialogResult == DialogResult.Cancel) 
                return;
            vdfData.SaveToFile(fileName, true);

        }

        
    }
}
