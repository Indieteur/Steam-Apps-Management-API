using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Indieteur.SAMAPI;

namespace Demo
{
    partial class frmMainDemo
    {
        void LoadSAMAPI() //Called once only when the form is first loaded.
        {
            try
            {
                SAM = new SteamAppsManager(); //Load our steam app manager. The library should be able to automatically find our steam directory.
            }
            catch (Exception ex)
            {
                NullReferenceException nullRefException = ex as NullReferenceException;  //The library will throw a null reference exception when it cannot find installation directory.
                if (nullRefException != null) 
                {
                    MessageBox.Show("Steam installation folder was not found! Please provide the path to the Steam installation folder.", "Steam App Manager", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    DialogResult dResult = folderBrowse.ShowDialog(); //Show the dialog which will allow the user to browse the steam installation directory manually.
                    if (dResult == DialogResult.Cancel)//If the user aborts the operation...
                    {
                        OnLoadFailed(); 
                        return;
                    }
                    TryLoadManual(folderBrowse.SelectedPath); //If the user selects a directory...
                }
                else
                    throw ex; //For other errors, throw it back and show it to the user.

            }
        }

        void TryLoadManual(string Path)
        {
            bool loadOK = false; //This variable will tell the loop if the steam library was loaded successfully.
            while (!loadOK) //Loop until we have successfully loaded the library or when the user aborts the operation.
            {
                if (string.IsNullOrWhiteSpace(Path) || !Directory.Exists(Path)) 
                {
                    Path = OnInvalidPathSelected(); //Call the method which will show the user the browse dialog.
                    continue; //Iterate again so that the check path is valid statement will be called again.
                }
                try
                {
                    SAM = new SteamAppsManager(Path); //Try to load the steam library again but this time with the path that was provided.
                    loadOK = true; //If load was successful, there should be no errors thus this piece of code should be reached which will exit the loop.
                }
                catch
                {
                    Path = OnInvalidPathSelected(); //Just try again if the given path is not the steam library installation location.
                    continue;

                }
            }
        }

        string OnInvalidPathSelected()
        {
            MessageBox.Show("Invalid path selected! Please try again.", "Steam App Manager", MessageBoxButtons.OK, MessageBoxIcon.Error); 
            DialogResult dResult = folderBrowse.ShowDialog();
            if (dResult == DialogResult.Cancel) //If user presses the cancel or close button then abort the load and show the VDF Demo form instead.
            {
                OnLoadFailed();
                return null;
            }
            return folderBrowse.SelectedPath;
        }
        void OnLoadFailed()
        {
            MessageBox.Show("Unable to locate Steam Installation folder. Disabling Steam Apps Manager library and launching VDF Demo instead.", "Steam App Manager", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Program.failedStartMain = true; //Since we cannot launch the VDF Demo form on its own (The form will close if we close this form as it is a child form), we will need to launch the VDF demo form from the main method which launched this form.
            Close();  
        }
    }
}
