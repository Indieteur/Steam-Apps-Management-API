using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Indieteur.SAMAPI
{
    public partial class SteamAppsManager
    {
        /// <summary>
        /// Returns true if the Event Listener is running. False if not.
        /// </summary>
        public bool EventListenerRunning
        {
            get
            {
                if (listenerThread != null) //Check if the listenerThread variable is null. If not then, return the isalive field of the listenerThread.
                {
                    return listenerThread.IsAlive;
                }
                return false; //We can safely assume that the event listener thread is not running if the listenerThread var is set to null.
            }
        }

        /// <summary>
        /// Returns true if the library was ordered to start listening for events and false if not or if the library already received the command to stop.
        /// </summary>
        public bool EventListenerMustRun
        {
            get
            {
                return (listenerShouldRun > 0);
            }
        }
        /// <summary>
        /// The interval set between Event Listener checks in milliseconds.
        /// </summary>
        public int EventListenerInterval
        {
            get
            {
                return listener_interval;
            }
        }

        int listenerShouldRun = 0; //Thread Cleanup based on https://stackoverflow.com/questions/12312155/finishing-up-a-thread-loop-after-disposal.
        int listener_interval;

        Thread listenerThread;

        /// <summary>
        /// Creates a new thread which will listen for Steam Events.
        /// </summary>
        /// <param name="Interval">The interval between checks in milliseconds.</param>
        public void StartListeningForEvents (int Interval = 1000)
        {
            if (listenerShouldRun > 0)
                throw new ThreadStateException("A listener thread has already been instantiated!");
            Interlocked.Exchange(ref listenerShouldRun, 1); //Similar to listenerShouldRun = 1; but for thread safety reason, we need to do call this method instead.
            Interlocked.Exchange(ref listener_interval, Interval);
            listenerThread = new Thread(new ThreadStart(ListenerThreadMethod)); //Create a new thread which will call the method ListenerThreadMethod.
            listenerThread.Start(); //Start the thread.
        }

        /// <summary>
        /// Sets the interval between checks.
        /// </summary>
        /// <param name="Interval">The interval between checks in milliseconds.</param>
        public void SetEventListenerInterval (int Interval)
        {
            if (listenerShouldRun <= 0)
                throw new ThreadStateException("The listener thread is not running!");
            Interlocked.Exchange(ref listener_interval, Interval); //Similar to listener_interval = interval; but for thread safety reason, we need to do call this method instead.
        }

        /// <summary>
        /// Stops the thread which listens for events. (NOTE: A delay might occur before the thread completely stops executing.)
        /// </summary>
        public void StopListeningForEvents()
        {
            if (listenerShouldRun <= 0)
                throw new ThreadStateException("The listener thread is not running!");
            Interlocked.Exchange(ref listenerShouldRun, 0);
        }

        void ListenerThreadMethod()
        {
            while (listenerShouldRun > 0) //Continue looping to listen for events until the main thread has set the listenerShouldRun variable to 0. (which means stop the thread.)
            {
                CheckForEvents(); //Perform the event checking.
                Thread.Sleep(listener_interval); //Sleep for the specified amount of interval.
            }
            
        }
    }
}
