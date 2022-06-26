using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsInstalledApplicationFinder
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void action_btn_get_Click(object sender, EventArgs e)
        {
            string uninstallKey = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";
            using (RegistryKey rk = Registry.LocalMachine.OpenSubKey(uninstallKey))
            {
                foreach (string skName in rk.GetSubKeyNames())
                {
                    using (RegistryKey sk = rk.OpenSubKey(skName))
                    {
                        try
                        {

                            var displayName = sk.GetValue("DisplayName");


                            ///productive app collection 
                            List<string> Productiveapp = new List<string>();

                            //app names
                            Productiveapp.Add("Git");
                            Productiveapp.Add("FortiClient");

                            foreach(var v in Productiveapp)
                            {
                                if(displayName != null)
                                if (displayName.Equals(v)) //check installed application name  equals hardcoded app names // eg git,forticlient
                                {
                                    var CurrentDirectory = Directory.GetCurrentDirectory();
                                    var exists = CurrentDirectory + "\\ProductiveApps";
                                    if (!Directory.Exists(exists))
                                    {
                                        System.IO.Directory.CreateDirectory(exists);
                                    }
                                    var path = exists + "\\Apps.text";
                                    if (!File.Exists(path))
                                    {

                                        File.Create(path);
                                    }
                                    else
                                    {
                                        using (var tw = new StreamWriter(path, true))
                                        {
                                                //write the app name to file
                                            tw.WriteLine(displayName.ToString());
                                        }
                                    }
                                }

                            }
                            





                            var size = sk.GetValue("EstimatedSize");

                            ListViewItem item;
                            if (displayName != null)
                            {
                                if (size != null)
                                    item = new ListViewItem(new string[] {displayName.ToString(), 
                                                       size.ToString()});
                                else
                                    item = new ListViewItem(new string[] { displayName.ToString() });
                                lstDisplayHardware.Items.Add(item);

                            }
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                }
                label1.Text += " (" + lstDisplayHardware.Items.Count.ToString() + ")";
            }  
        }

        private void lstDisplayHardware_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
