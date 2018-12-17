using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.Win32;

namespace Indieteur.SAMAPI
{
    //Based on https://stackoverflow.com/questions/42651987/get-process-id-or-process-name-after-launching-a-steam-game-via-steam-rungamei
    //This file contains the method which fires the delegates registered to the events of the class.
    public partial class SteamAppsManager
    {
        public delegate void dStatus_Change(SteamApp app);
        public delegate void dExec_Change(SteamApp app, Process process);
        internal const string REG_APPS_KEY = "Apps";
        internal const string REG_UPDATING_KEY = "Updating";
        internal const string REG_RUNNING_KEY = "Running";
        /// <summary>
        /// This event is called when an installed Steam Application is being updated.
        /// </summary>
        public event dStatus_Change SteamApp_OnUpdating;
        /// <summary>
        /// This event is called when a Steam Application has been updated or if the update has been aborted.
        /// </summary>
        public event dStatus_Change SteamApp_OnUpdateAbortOrFinish;
        /// <summary>
        /// This event is called when a Steam Application status was set to launched by the Steam Client.
        /// </summary>
        public event dStatus_Change SteamApp_OnLaunched;
        /// <summary>
        /// This event is called when a Steam Application status was set to not running by the Steam Client.
        /// </summary>
        public event dStatus_Change SteamApp_OnExit;

        /// <summary>
        /// This event is called when the process associated to the Steam Application has been detected.
        /// </summary>
        public event dExec_Change SteamApp_OnProcessDetected;
        /// <summary>
        /// This event is called when the process associated to the Steam Application has exited.
        /// </summary>
        public event dExec_Change SteamApp_OnProcessQuit;



        /// <summary>
        /// Manually updates the running and updating status of the Steam Applications and if a Steam App is running, detects the process associated with it. This function also calls the methods subscribed to the events associated with the status changes.
        /// </summary>
        public void CheckForEvents()
        {
            foreach (SteamApp sapp in _steamapps) 
            {
                lock (sapp) //lock the steam app class for thread safety reasons.
                {
                    //Cache the isRunning, isUpdating statuses and the associated process which we will be using for comparison after the status and associated process of the Application has been updated.
                    bool isRunning = sapp._isRunning; 
                    bool isUpdating = sapp._isUpdating;
                    Process proc = sapp._runningProc;

                    sapp.UpdateAppStatus(); 

                    //Compare the values before the update was performed and after the update was performed. Check if they match. If they don't, it means something has changed so call the event associated with the change.
                    if (!isRunning && sapp._isRunning) 
                        Interlocked.CompareExchange(ref SteamApp_OnLaunched, null, null)?.Invoke(sapp); 
                    else if (isRunning && !sapp._isRunning)
                        Interlocked.CompareExchange(ref SteamApp_OnExit, null, null)?.Invoke(sapp);

                    if (!isUpdating && sapp._isUpdating)
                        Interlocked.CompareExchange(ref SteamApp_OnUpdating, null, null)?.Invoke(sapp);
                    else if (isUpdating && !sapp._isUpdating)
                        Interlocked.CompareExchange(ref SteamApp_OnUpdateAbortOrFinish, null, null)?.Invoke(sapp);

                    if (proc == null && sapp._runningProc != null)
                        Interlocked.CompareExchange(ref SteamApp_OnProcessDetected, null, null)?.Invoke(sapp, sapp._runningProc);
                    else if (proc != null && sapp._runningProc == null)
                        Interlocked.CompareExchange(ref SteamApp_OnProcessQuit, null, null)?.Invoke(sapp, proc);

                }
            }
        }
    }
}
