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
            if (!string.IsNullOrWhiteSpace(steamdir)) //If the steamdir is not null or whitespace, append the "steamapps" directory name.
                steamdir += "\\" + SteamAppsManager.STEAM_APPS_DIRNAME;
            openFileDialog.InitialDirectory = steamdir;
        }

        private void btnLoadfromFile_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = openFileDialog.ShowDialog(this); //Show our open file dialog and store its result on a variable.
            string fileName = openFileDialog.FileName; //Cache our filename as we are going to reset the dialogbox
            ClearDialogBoxes();
            if (dialogResult == DialogResult.Cancel) //If user presses the cancel button, exit from the method.
                return;
            ResetGUIAndVariables(); //Reset our GUI and the variables to defaults.
            vdfData.LoadData(fileName); //Call LoadData function from our vdfData class instance.
            LoadVDFDataToTreeView(vdfData); //Load our VDF Data to the TreeView control.
        }

        private void btnSaveToFile_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = saveFileDialog.ShowDialog(this); //Show our save file dialog and store its result on a variable.
            string fileName = saveFileDialog.FileName; //Cache our filename as we are going to reset the dialogbox
            ClearDialogBoxes();
            if (dialogResult == DialogResult.Cancel) //If user presses the cancel button, exit from the method.
                return;
            vdfData.SaveToFile(fileName, true);//Call the SaveToFile method of the VDFData class instance. Also, set the overwrite argument to true as the savefiledialog will prompt the user if a file will be overwritten.

        }

        
    }
}
