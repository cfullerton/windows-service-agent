using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Net;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace agent
{
    public partial class agent : ServiceBase
    {
        public agent()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            // Set up a timer to trigger every minute.  
            System.Timers.Timer timer = new System.Timers.Timer
            {
                Interval = 60000 // 60 seconds  
            };
            timer.Elapsed += new System.Timers.ElapsedEventHandler(this.OnTimer);
            timer.Start();
        }
        public void OnTimer(object sender, System.Timers.ElapsedEventArgs args)
        {
            var baseUrl = "http://foo.bar:8080";
            var queryString = "";
            queryString += "?systemname" + System.Environment.MachineName;

            //initialize the select query with command text
            System.Management.SelectQuery query = new SelectQuery(@"Select * from Win32_ComputerSystem");

            //initialize the searcher with the query it is supposed to execute
            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(query))
            {
                //execute the query
                foreach (ManagementObject process in searcher.Get())
                {
                    //print system info
                    process.Get();
                
                    queryString += "&Manufacturer=" + process["Manufacturer"];
                    queryString += "&Model=" + process["Model"];
                    queryString += "&UserName=" + process["UserName"];

                }
            }

            WebRequest webRequest = WebRequest.Create(requestUriString: baseUrl + queryString);
            WebResponse webResp = webRequest.GetResponse();

        }
        protected override void OnStop()
        {
        }
    }
}
