using Microsoft.Win32;
using Realms.Sync;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WPFApp.ViewModels
{
    public class MenuViewModel:BaseViewModel
    {
        private List<string> app;
        public List<string> App { get=> app; set => app = value; }
        private string _userid;
        public string userid { get => _userid; set => _userid = value; }
        private Button _btn;
        public Button Btn { get=> _btn; set => _btn = value; }  
        private List<Button> _buttons;
        private List<InstalledApp> _installedApps;
        public List<InstalledApp> InstalledApps { get=> _installedApps; set => _installedApps = value; }

        private InstalledApp _iapp;
        public InstalledApp IApp { get=>_iapp; set => _iapp = value; }
        public List<Button> Buttons { get=>_buttons; set => _buttons = value; }
        private const string keyBase = @"SOFTWARE\WoW6432Node\Microsoft\Windows\CurrentVersion\Uninstall";
        //@"SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths";
        string registry_key_64 = @"SOFTWARE\WoW6432Node\Microsoft\Windows\CurrentVersion\Uninstall";
        public MenuViewModel()
        {
            
            //App = new List<string>();


            //Buttons = new List<Button>();
            //for (int i = 0; i < 10; i++)
            //{
            //    Btn = new Button();
            //    Btn.Height = 50;
            //    Btn.Width = 50;
            //    Buttons.Add(Btn);
            //}
            GetAllInstalledApps();

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
                //if(a.DisplayName == "AnyDesk")
                //{
                //    app.DisplayIcon= a.DisplayIcon;
                //    string[] formattedDI = a.DisplayIcon.Split(',');


                //    app.DisplayName = a.DisplayName;
                //    app.InstallationLocation = a.InstallationLocation;
                //    Process.Start(formattedDI[0]);
                //}
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
           var a = selecteditem as InstalledApp;
            string b = a.DisplayIcon;
            string[] formattedDI = b.Split(',');
            Process.Start(formattedDI[0]);
        }
    }
}

