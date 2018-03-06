using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
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
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Interval = 60000; // 60 seconds  
            timer.Elapsed += new System.Timers.ElapsedEventHandler(this.OnTimer);
            timer.Start();
        }
        public void OnTimer(object sender, System.Timers.ElapsedEventArgs args)
        {
            var url = "http://foo.bar:8080";
            // TODO: Insert monitoring activities here.  
            WebRequest webRequest = WebRequest.Create(requestUriString: url);
            WebResponse webResp = webRequest.GetResponse();

        }
        protected override void OnStop()
        {
        }
    }
}
