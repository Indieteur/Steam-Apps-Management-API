using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo
{
    partial class frmMainDemo
    {
        //The default text that will be displayed on the status bar and the buttons.
        const string LOADINGLIB = "Loading Steam Library...";
        const string WAITINGEVENTSTOP = "Waiting for the Event Listener to stop...";
        const string IDLE = "Idle";
        const string BTN_EVENT_START = "Start watching for Events";
        const string BTN_EVENT_STOP = "Stop watching for Events";

        void GUI_BaseControlsEnabled(bool Enabled) //This method handles the enabling of the Refresh and Start/Stop Watching for Events button.
        {
            
            btnRefresh.Enabled = Enabled;
            btnStartWatchEvents.Enabled = Enabled;
            if (Enabled) //Also make sure to let the user know that we are loading the steam library.
                toolStripStatus.Text = IDLE;
            else
                toolStripStatus.Text = LOADINGLIB;
        }

        void GUI_EventListenUpdate(bool Listening) //This method is called when the user presses the Start/Stop Watching Events button.
        {
            if (Listening) //Check if the user wants us to start listening for events.
            {
                btnStartWatchEvents.Text = BTN_EVENT_STOP; 
                groupStatus.Enabled = true;
                //No need to do anything else as the event listening should start immediately.
            }
            else
            {
                if (tmrEvents.Enabled) //Make sure to pause the timer first if we are stopping the event listening.
                    tmrEvents.Stop();
                toolStripStatus.Text = WAITINGEVENTSTOP; //Update the status bar.
                btnStartWatchEvents.Enabled = false; //Disable the Stop Watching Event button so that the user doesn't accidentally press it again while the event listening thread is being stopped.
               
                while (SAM.EventListenerRunning)
                {
                    //Wait for the event listener to stop before we update the GUI.
                }
                //Update the GUI elements and the status bar.
                btnStartWatchEvents.Text = BTN_EVENT_START;
                btnStartWatchEvents.Enabled = true;
                toolStripStatus.Text = IDLE;
                groupStatus.Enabled = false;
                cboxIsRunning.Checked = false;
                cboxIsUpdating.Checked = false;
                txtPID.Text = "";

                tmrEvents.Start(); //Restart the timer.
            }
        }

        void GUI_Reset()
        {
            txtPID.Text = "";
            txtName.Text = "";
            txtAppID.Text = "";
            txtInstallDir.Text = "";
            cboxIsUpdating.Checked = false;
            cboxIsRunning.Checked = false;
            btnOpenFileExplore.Enabled = false;
            btnLaunchApp.Enabled = false;
            btnExitApp.Enabled = false;
        }
    }
}
