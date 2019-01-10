using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Indieteur.SAMAPI;

namespace Demo
{
    public partial class frmMainDemo : Form
    {
        SteamAppsManager SAM;
        public frmMainDemo()
        {
            InitializeComponent();
            
        }

        private void frmMainDemo_Load(object sender, EventArgs e)
        {
            LoadSAMAPI(); //Initialze the SteamAppsManager class.
            LoadSteamAppsToListBox(); //Once we are done initializing the SteamAppsManager class, load the steam apps to the list box.
        }

        private void tmrEvents_Tick(object sender, EventArgs e) //We need a timer that will always update the textboxes and the list box items statuses for us. This timer will be called every 1 second.
        {
            RefreshListBox();
            if (lstApps.SelectedItem != null)
            {
                UpdateGUIElements(lstApps.SelectedItem as ListBoxItem);
            }
        }

        private void frmMainDemo_FormClosing(object sender, FormClosingEventArgs e)
        {
            
            if (SAM !=null && SAM.EventListenerMustRun && SAM.EventListenerRunning) //If the user did not stop the event listener before closing the form.
                SAM.StopListeningForEvents();
        }

       
    }
}
