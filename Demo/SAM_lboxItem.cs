using Indieteur.SAMAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo
{
    class ListBoxItem //The item that will be added to the list box.
    {
        public string Name;
        public SteamApp AssociatedApp;
        SteamAppsManager sam;
        public ListBoxItem(string name, SteamApp associatedApp, SteamAppsManager steamAppsMan)
        {
            Name = name;
            AssociatedApp = associatedApp;
            sam = steamAppsMan;
        }
        public override string ToString() //Will be called by the list box when the item is added/re-added. The resulting string is the text shown on the list box for the item.
        {
            StringBuilder sb = new StringBuilder(Name);
            if (sam.EventListenerRunning) //Reflect the status only if the Event Listener is running.
            {
                if (AssociatedApp.IsRunning)
                    sb.Append(" [Running]");
                if (AssociatedApp.IsUpdating)
                    sb.Append(" (Updating)");
            }
            return sb.ToString();
        }
    }
}
