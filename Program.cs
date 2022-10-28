// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using System.Timers;
using Windows.UI.Notifications;
using Windows.UI.Notifications.Management;
using TheKey_v2;
using TheKey_v2.Enum;
using Timer = System.Timers.Timer;
using System;

// GET Keyboard
var d = new Keyboard();

// Win API
var listener = UserNotificationListener.Current;

// Check call function every 1s
var timer = new Timer(1000);
timer.Elapsed += ProcessCheck;
timer.Start();
timer.Enabled = true;


// Check function
async void ProcessCheck(object source, ElapsedEventArgs e)
{
    /**
     * Check VS is in debug mode
     */
    Debug.WriteLine("Checking for process");
    // Get All process of VISUAL STUDIO
    var processesVs = Process.GetProcessesByName("devenv");
    // Test if process is debug
    if (processesVs.Length > 0)
        // Set light mode to rainbow
        if (processesVs.Any(p => p.MainWindowTitle.Contains("(D") || p.MainWindowTitle.Contains("(E")))
        {
            d.SetLightColor(20, 100);
            d.SetLightMode(LightMode.Breathing4);
            return;
        }

    /**
     * If notification
     */
    var notification = await listener.GetNotificationsAsync(NotificationKinds.Toast);
    foreach(var _ in notification)
    {
        d.SetLightColor(207, 100);
        return;
    }

    /**
     * Set normal mode
     */
    d.SetLightColor(0, 10);
    d.SetLightMode(LightMode.Solid);
}

// Keep process run
while (true) Task.Delay(10000);