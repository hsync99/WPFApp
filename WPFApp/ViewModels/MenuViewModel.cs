using Microsoft.Win32;
using Realms.Sync;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using WPFApp.Views;
using WPFApp.Models.UI;

namespace WPFApp.ViewModels
{
    public class MenuViewModel:BaseViewModel
    {
        private string _userid;
        public string userid { get; set; }

        private string _fname;
        private string _lname;
        private string _email;

        private Models.UI.User _user;
        public Models.UI.User User { get => _user; set => _user = value; }
        public string Fname { get=>_fname; set => _fname = value; }
        public string Lname { get => _lname;set => _lname = value; }
        public string Email { get => _email;set=> _email = value; } 

        public string balance;
        public string Balance { get => balance; set => balance = value; }   

        private List<InstalledApp> _installedApps;
        public List<InstalledApp> InstalledApps { get=> _installedApps; set => _installedApps = value; }

        private InstalledApp _iapp;
        public InstalledApp IApp { get=>_iapp; set => _iapp = value; }
        private const string keyBase = @"SOFTWARE\WoW6432Node\Microsoft\Windows\CurrentVersion\Uninstall";
        //@"SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths";
        string registry_key_64 = @"SOFTWARE\WoW6432Node\Microsoft\Windows\CurrentVersion\Uninstall";
        public MenuViewModel()
        {
            

            GetAllInstalledApps();
           // GetUserById(userid);
        
        }
 
        
        
        public class InstalledApp
        {
            public string DisplayIcon { get; set; } 
            public string DisplayName { get; set; }
            public string InstallationLocation { get; set; }
        }

        public void GetAllInstalledApps()
        {
            IApp = new InstalledApp();
            InstalledApps = new List<InstalledApp>();
            InstalledApps = GetFullListInstalledApplication();
            InstalledApp app = new InstalledApp();
            foreach(var a in InstalledApps) 
            {
                IApp.DisplayName = a.DisplayName;
                string[] formattedDI = a.DisplayIcon.Split(',');
                IApp.InstallationLocation = formattedDI[0];

            }
           
        }
       
       
        private static List<InstalledApp> GetFullListInstalledApplication()
        {
            IEnumerable<InstalledApp> finalList = new List<InstalledApp>();

            string registry_key_32 = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";
            string registry_key_64 = @"SOFTWARE\WoW6432Node\Microsoft\Windows\CurrentVersion\Uninstall";

            List<InstalledApp> win32AppsCU = GetInstalledApplication(Registry.CurrentUser, registry_key_32);
            List<InstalledApp> win32AppsLM = GetInstalledApplication(Registry.LocalMachine, registry_key_32);
            List<InstalledApp> win64AppsCU = GetInstalledApplication(Registry.CurrentUser, registry_key_64);
            List<InstalledApp> win64AppsLM = GetInstalledApplication(Registry.LocalMachine, registry_key_64);

            finalList = win32AppsCU.Concat(win32AppsLM).Concat(win64AppsCU).Concat(win64AppsLM);

            finalList = finalList.GroupBy(d => d.DisplayName).Select(d => d.First());

            return finalList.OrderBy(o => o.DisplayName).ToList();
        }
        
        private static List<InstalledApp> GetInstalledApplication(RegistryKey regKey, string registryKey)
        {
            List<InstalledApp> list = new List<InstalledApp>();
            using (Microsoft.Win32.RegistryKey key = regKey.OpenSubKey(registryKey))
            {
                if (key != null)
                {
                    foreach (string name in key.GetSubKeyNames())
                    {
                        using (RegistryKey subkey = key.OpenSubKey(name))
                        {
                            string displayIcon = (string)subkey.GetValue("DisplayIcon");
                            string displayName = (string)subkey.GetValue("DisplayName");
                            string installLocation = (string)subkey.GetValue("InstallLocation");

                            if (!string.IsNullOrEmpty(displayName)) // && !string.IsNullOrEmpty(installLocation)
                            {
                                list.Add(new InstalledApp()
                                {
                                    DisplayIcon = displayIcon?? "",
                                    DisplayName = displayName.Trim(),
                                    InstallationLocation = installLocation
                                });
                            }
                        }
                    }
                }
            }

            return list;
        }
       public void SelectItemByIndexInList(object selecteditem)
        {
            try
            {
                var a = selecteditem as InstalledApp;
                string b = a.DisplayIcon;
                string[] formattedDI = b.Split(',');
                Process.Start(formattedDI[0]);
            }
            catch (Exception ex)
            {

            }
           
        }
    }
}

