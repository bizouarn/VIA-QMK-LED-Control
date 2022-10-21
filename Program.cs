// See https://aka.ms/new-console-template for more information
using HidLibrary;
using System.Diagnostics;
using TheKey_v2;
using TheKey_v2.Enum;
using System.ServiceProcess;
using System.Timers;
using System.Threading;

// GET Keyboard
var d = new Keyboard();

// Check call function every 1s
var timer = new System.Timers.Timer(1000);
timer.Elapsed += ProcessCheck;
timer.Start();
timer.Enabled = true;

// Ceck function
void ProcessCheck(object source, ElapsedEventArgs e)
{
    /**
     * Check VS is in debug mode
     */
    System.Diagnostics.Debug.WriteLine("Checking for process");
    // Get All process of VISUAL STUDIO
    var processesVS = Process.GetProcessesByName("devenv");
    // Test if process is debug
    if (processesVS.Length > 0)
    {
        // Set light mode to rainbow
        foreach (var p in processesVS)
        {
            if (p.MainWindowTitle.Contains("(D") || p.MainWindowTitle.Contains("(E"))
            {
                d.SetLightColor(20, 100, 100);
                d.SetLightMode(LightMode.Breathing4);
                return;
            }
        }
    }
    /**
     * Set normal mode
     */
    d.SetLightColor(0, 10, 100);
    d.SetLightMode(LightMode.Solid);
}

// Keep process run
while (true) {
    Task.Delay(10000);
}