using System;
using System.Collections.Generic;
using Indieteur.VDFAPI;
using System.IO;

namespace Indieteur.SAMAPI
{
    static class LibraryFoldersHelper
    {
        const string LIBRARY_FOLDERS_NAME = "libraryfolders.vdf"; //The default file name of the file which lists all the library folders of steam.
        const string LIBFILE_NODE_NAME = "LibraryFolders"; //The node name inside the Library Folders file which contains the list of our steam libraries
        const string APPMANIFEST_SEARCH_STRING = "appmanifest_*.acf"; //The search pattern to be used for searching steam apps manifest.


        /// <summary>
        /// Lists all the library folders of the steam installation.
        /// </summary>
        /// <param name="MainSteamInstallPath">The main directory of steam. (The installation folder.)</param>
        /// <returns></returns>
        public static List<string> RetrieveLibraryFolders(string MainSteamInstallPath)
        {
            List<string> libraryFolders = new List<string>(1); //Initialize our list of library folders with a count of 1 as we know that the InstallDir is a library folder.
            libraryFolders.Add(MainSteamInstallPath);

            //Let us now locate the file that contains the list of all the steam library folders in the machine.
            string LibFileFullPath = MainSteamInstallPath + "\\" + SteamAppsManager.STEAM_APPS_DIRNAME + "\\" + LIBRARY_FOLDERS_NAME; //The full path of the library folders file.

            if (!File.Exists(LibFileFullPath))
                return libraryFolders; 

            VDFData vdfReader = new VDFData(LibFileFullPath);

            VDFNode vNode = vdfReader.Nodes.FindNode(LIBFILE_NODE_NAME); //Find the node that contains the list of steam libraries.

            if (vNode == null || vNode.Keys == null || vNode.Keys.Count == 0) //If it isn't found or the Nodes key is null or empty, there's nothing else to be done. Return the list of libraryfolders that we already have. (which is just MainSteamInstallPath)
                return libraryFolders;

            foreach (VDFKey vKey in vNode.Keys) //List all the keys inside the vNode node.
            {
                if (vKey.Name.IsInteger()) //As per what I've seen from the Library Folders file, it seems that the key name for the location of the folders itself is a number so check if the key name is a number.
                {
                    libraryFolders.Add(vKey.Value); 
                }
            }
            return libraryFolders;
        }

        /// <summary>
        /// Retrieves all the Steam Apps under the Library Directories.
        /// </summary>
        /// <param name="LibPaths">List of Steam Library Paths to perform the search on.</param>
        /// <returns></returns>
        public static List<SteamApp> RetrieveSteamAppsUnderLibraryFolders(IEnumerable<string> LibPaths)
        {
            List<SteamApp> steamapps = new List<SteamApp>();
            foreach (string libpath in LibPaths) 
            {
                steamapps.AddRange(RetrieveAppsUnderLibraryFolder(libpath));
            }
            return steamapps; 
        }


        /// <summary>
        /// Retrieves all the Steam Apps under the specified Library Folder.
        /// </summary>
        /// <param name="LibPath">The Library Directory to search on.</param>
        /// <returns></returns>
        public static List<SteamApp> RetrieveAppsUnderLibraryFolder(string LibPath)
        {

            List<SteamApp> steamapps = new List<SteamApp>(); 
           
            string steamAppsFolderPath = LibPath + "\\" + SteamAppsManager.STEAM_APPS_DIRNAME; //This is the folder that contains the application manifests.
            if (!Directory.Exists(steamAppsFolderPath))
                return steamapps;
            string[] manifests = Directory.GetFiles(steamAppsFolderPath, APPMANIFEST_SEARCH_STRING); //Search for the application manifests under the SteamApps Folder of the library.

            if (manifests.Length == 0) 
                return steamapps;

            foreach (string manifest in manifests) //Parse all our manifest file in to a SteamApp instance.
            {
                steamapps.Add(new SteamApp(manifest, LibPath));
            }
            return steamapps;
        }
    }



}
