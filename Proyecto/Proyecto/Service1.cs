using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Proyecto
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            var timer = new Timer { AutoReset = true, Interval = 60000 };
            timer.Elapsed += timer_Elapsed;
            timer.Start();
        }

        private void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            var processes = Process.GetProcessesByName("firefox");

            foreach (var process in processes)
            {
                process.CloseMainWindow();
                if (!process.HasExited)
                {
                    process.Kill();
                    process.Close();
                }
            }
        }

        protected override void OnStop()
        {
        }
    }
}
