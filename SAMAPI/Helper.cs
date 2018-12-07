using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Indieteur.SAMAPI
{
    //Based on https://stackoverflow.com/questions/9809340/how-to-check-if-isnumeric.
    internal static class Helper
    {
        /// <summary>
        /// Returns true if the string is an integer.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsInteger(this string s)
        {
            int output;
            return int.TryParse(s, out output);
        }

        /// <summary>
        /// Returns the integer value of a HKEY_CURRENT_USER sub key.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="valname"></param>
        /// <returns></returns>
        public static int HKCU_RegGetKeyInt(string path, string valname)
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(path)) 
            {
                if (key != null)
                {
                    Object value = key.GetValue(valname); 
                    if (value != null)
                    {
                        return (int)value; //Return the value but cast it as an int first as it is a int type value.
                    }
                }
            }
            return -1; //If key or value isn't found, return -1.
        }
        
        /// <summary>
        /// Finds the process pertaining to the application by checking if the process executable is found inside the application's directory (and if it the process name and the ExeName matches.)
        /// </summary>
        /// <param name="AppMainDir">The directory where the application can be found.</param>
        /// <param name="ExeName">The executable name which will be matched against the process name. Can be left blank and the method will return the earliest created process with an executable found under the application directory instead.</param>
        /// <returns></returns>
        public static Process FindAppProcess(string AppMainDir, string ExeName = null)
        {
            Process process = null; //The process which we will be returning back if ExeName isn't set or if no matching process is found.
            Process[] processesList = Process.GetProcesses(); //Retrieve all the processes running.
            AppMainDir = AppMainDir.ToLower().Trim();

            foreach (Process procFound in processesList) //Loop through the processes.
            {
                
                try //There are a lot of possible exceptions that might be raised that we can just ignore like Access Denied for system processes. 
                {
                    string procFileName = procFound.MainModule.FileName.ToLower().Trim(); //Make sure that the capitalization of the file path won't be an issue.
                    if (procFileName.StartsWith(AppMainDir)) //The first thing that we will need to do is check if the executable associated with the process is located under the correct directory.
                    {
                        if (process != null) //Check if we already have a candidate process. If we do, compare its start time to the start time of the current process we are checking.
                        {
                            if (process.StartTime > procFound.StartTime) //If the cached process is started later than the current process we are checking, it's most likely a child process.
                                process = procFound;
                        }
                        else
                        {
                            process = procFound; //If there's no candidate process yet, then set the current process we are checking as the possible App's process.
                        }
                           
                        if (!string.IsNullOrWhiteSpace(ExeName)) //If the ExeName argument isn't empty.
                        {
                            if (procFound.ProcessName == ExeName) //Check if the process' name matches with ExeName. If it is, then we can somewhat assume that we have the right process. 
                                return procFound;
                        }
                    }
                    
                }
                catch
                {
                    continue; //Just iterate to the next process if an error occurs.
                }
            }
            return process;
            
        }

    }
}
