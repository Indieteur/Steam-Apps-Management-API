using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using System.Diagnostics;

namespace Indieteur.SAMAPI
{
    public static class Extension
    {
        /// <summary>
        /// Locate a Steam App using its name.
        /// </summary>
        /// <param name="listofapps"></param>
        /// <param name="name">Name of the app to locate.</param>
        /// <param name="CaseSensitive">Indicates if the capitalization of the name should matter for the search.</param>
        /// <param name="ThrowErrorOnNotFound">Indicates whether the method should throw an error if no matching Application is found.</param>
        /// <returns></returns>
        public static SteamApp FindAppByName(this IEnumerable<SteamApp> listofapps, string name, bool CaseSensitive = false, bool ThrowErrorOnNotFound = false)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException("Argument name cannot be null, empty or whitespace!");           
            if (!CaseSensitive) //If not case sensitive, then convert the name to its lower case variant.
                name = name.ToLower();

            foreach (SteamApp sapp in listofapps)
            {
                string compareName; //Will store the name of the steam app we are checking
                if (CaseSensitive) //If case Sensitive is set to true, set compareName exactly to the Name of the steam app
                    compareName = sapp.Name;
                else
                    compareName = sapp.Name.ToLower();
                if (compareName == name)// If we have similar names then we have the app.
                    return sapp;

            }

            if (ThrowErrorOnNotFound)
                throw new SteamAppNotFoundException(name + " application is not found!");
            return null;
        }

        /// <summary>
        /// Locate a Steam App using its unique ID.
        /// </summary>
        /// <param name="listofapps"></param>
        /// <param name="AppID">The unique identifier of the application.</param>
        /// <param name="ThrowErrorOnNotFound">Indicates whether the method should throw an error if no matching Application is found.</param>
        /// <returns></returns>
        public static SteamApp FindAppByID(this IEnumerable<SteamApp> listofapps, int AppID, bool ThrowErrorOnNotFound = false)
        {
            foreach (SteamApp sapp in listofapps) //loop through our list of apps
            {
                if (sapp.AppID == AppID) //If we the AppID arg matches with the app id of the Steam App we are checkig then we found our app.
                    return sapp;
            }
            if (ThrowErrorOnNotFound)
                throw new SteamAppNotFoundException("application with ID of " + AppID.ToString() + " is not found!");
            return null;
        }

        /// <summary>
        /// Kills the process and its children processes.
        /// </summary>
        /// <param name="_proc"></param>
        public static void KillProcessAndChildren(this Process _proc)
        {
            KillProcessAndChildrens(_proc.Id);
            
        }
        //Based from https://stackoverflow.com/questions/23845395/in-c-how-to-kill-a-process-tree-reliably.
        static void KillProcessAndChildrens(int pid)
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher
              ("Select * From Win32_Process Where ParentProcessID=" + pid);
            ManagementObjectCollection moc = searcher.Get();
            foreach (ManagementObject mo in moc)
            {
                KillProcessAndChildrens(Convert.ToInt32(mo["ProcessID"]));
            }
            try
            {
                Process proc = Process.GetProcessById(pid);
                proc.Kill();
            }
            catch (ArgumentException)
            {
                // Process already exited.
            }
        }
        
    }

    /// <summary>
    /// An exception thrown by FindAppByID and FindAppByName methods.
    /// </summary>
    public class SteamAppNotFoundException : Exception
    {
        public SteamAppNotFoundException(string Message) : base(Message)
        {

        }
    }
}
