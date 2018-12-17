using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Indieteur.SAMAPI;

namespace Demo
{
    partial class frmMainDemo
    {
        private void lstApps_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstApps.SelectedItem == null)
                return;
            UpdateGUIElements(lstApps.SelectedItem as ListBoxItem); //This method will update the textboxes and the checkboxes for us.

        }

        void LoadSteamAppsToListBox() //This method loads the steam apps to the list box.
        {
            GUI_BaseControlsEnabled(false); //Make sure that we disable the main controls first as we do not want the user to click either the refresh button again or the Start/Stop Watching Event when we are refreshing the library.
            lstApps.Items.Clear(); 
            foreach (SteamApp sapp in SAM.SteamApps) //loop through all the apps and add them to the list box by using the ListBoxItem class.
            {
                ListBoxItem lbi = new ListBoxItem(sapp.Name, sapp, SAM);
                lstApps.Items.Add(lbi);
            }
            tmrEvents.Start(); //Restart the timer.
            GUI_BaseControlsEnabled(true); //Re-enable the main controls.
        }

       

        void RefreshListBox() //This method is called every second by the timer. What this method does is refresh the texts on the list box which will reflect the status of the apps (e.g. if it is running or updating)
        {
            for (int i = 0; i < lstApps.Items.Count; ++i)
            {
                lstApps.Items[i] = lstApps.Items[i];
            }
        }

       
    }
}
