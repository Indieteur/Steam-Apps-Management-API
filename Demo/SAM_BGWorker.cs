using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Indieteur.SAMAPI;
namespace Demo
{
    partial class frmMainDemo
    {
        void UpdateGUIElements(ListBoxItem lbi) //This method is called when the user selects a listbox item (and it will be called continually every second.)
        {
            //Update the textboxes accordingly.
            txtName.Text = lbi.Name;
            txtAppID.Text = lbi.AssociatedApp.AppID.ToString();
            txtInstallDir.Text = lbi.AssociatedApp.InstallDir;
           
            btnOpenFileExplore.Enabled = true; //Enable the Open File Explorer button.
           
            if (SAM.EventListenerRunning) //Check if the event listener is running as it is useless to update the Running, Updating Checkboxes and the PID textbox if it isn't.
            {
                UpdateAppStatus(lbi);
            }
            else //If the event listener isn't running at all, make sure to set the associated controls value or enabled/checked property to defaults.
            {
                txtPID.Text = "";
                cboxIsRunning.Checked = false;
                cboxIsUpdating.Checked = false;
                btnLaunchApp.Enabled = true;
                btnExitApp.Enabled = false;
            }
        }

        void UpdateAppStatus(ListBoxItem lbi)  //This method is called when the user selects a listbox item and the event listener is running. (Will be called continually every second if there's an item selected on the list box.)
        {
            cboxIsRunning.Checked = lbi.AssociatedApp.IsRunning; //Set the value of the Running checkbox accordingly.
            if (lbi.AssociatedApp.IsRunning) //Also make sure to enable the Launch button if the application isn't running already or vice versa.
                btnLaunchApp.Enabled = false;
            else
            {
                btnLaunchApp.Enabled = true;
                btnExitApp.Enabled = false; //Additional button to disable when the app isn't running.
            }
               
            cboxIsUpdating.Checked = lbi.AssociatedApp.IsUpdating;
            Process proc = lbi.AssociatedApp.RunningProcess; //Check if we have found the associated process for the selected app.
            if (proc != null) //If we did, set the PID text box value to the ID of the Process and enable the Exit App button.
            {
                txtPID.Text = proc.Id.ToString();
                btnExitApp.Enabled = true;
            }
            else //Otherwise...
            {
                txtPID.Text = "";
            }
        }
    }
}
