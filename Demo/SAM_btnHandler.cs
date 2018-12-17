using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Indieteur.SAMAPI;
namespace Demo
{
    //This file handles all the events associated with the GUI buttons.
    partial class frmMainDemo
    {
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            SAM_Refresh();
        }
        private void btnStartWatchEvents_Click(object sender, EventArgs e)
        {
            StartStopWatchEvents();
        }
        private void btnOpenVDFEditor_Click(object sender, EventArgs e)
        {
            ShowVDFDemo();
        }
        private void btnOpenFileExplore_Click(object sender, EventArgs e)
        {
            OpenDir(txtInstallDir.Text);
        }
        private void btnLaunchApp_Click(object sender, EventArgs e)
        {
            if (lstApps.SelectedItem != null)
                RunApp(lstApps.SelectedItem as ListBoxItem);
        }
        private void btnExitApp_Click(object sender, EventArgs e)
        {
            if (lstApps.SelectedItem != null)
                StopApp(lstApps.SelectedItem as ListBoxItem);
        }


        void StopApp(ListBoxItem lbi)
        {
            lbi.AssociatedApp.RunningProcess.CloseMainWindow(); //Change this to kill or KillProcessAndChildren instead of CloseMainWindow for an immediate termination of process.
        }
        void RunApp(ListBoxItem lbi)
        {
            try
            {
                lbi.AssociatedApp.Launch();
            }
            catch
            {
                //If we encountered an error, let our user know.
                MessageBox.Show("An error has occured when launching application " + lbi.Name, "Steam Apps Management", MessageBoxButtons.OK, MessageBoxIcon.Error); 
            }
        }

        void OpenDir(string path) 
        {
            if (!Directory.Exists(path)) 
                MessageBox.Show("Directory " + path + " does not exist!", "Steam Apps Management", MessageBoxButtons.OK, MessageBoxIcon.Error);
            try
            {
                Process.Start(path); //If it does, try opening it using the default file explorer.
            }
            catch
            {
              
                MessageBox.Show("An error has occured when opening directory " + path , "Steam Apps Management", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void ShowVDFDemo() //This method is called when the Show VDF Editor button is pressed.
        {
            frmDemoVDF frmVDF = new frmDemoVDF();
            frmVDF.Show(this);
        }

        void SAM_Refresh() //This method is called when the Refresh Button is pressed.
        {
            if (tmrEvents.Enabled) //stop our timer first before we do anything.
                tmrEvents.Stop();
            SAM.Refresh(); //Perform the refresh.
            GUI_Reset(); 
            LoadSteamAppsToListBox(); //Reload data to list box.
        }

        void StartStopWatchEvents() //This method is called when the Start/Stop watching events button is pressed.
        {
            if (SAM.EventListenerRunning) 
            {
                SAM.StopListeningForEvents();
                GUI_EventListenUpdate(false);
            }
            else
            {
                SAM.StartListeningForEvents();
                GUI_EventListenUpdate(true);

            }
        }
    }
}
