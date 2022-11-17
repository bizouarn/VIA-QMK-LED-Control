// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using Windows.UI.Notifications;
using Windows.UI.Notifications.Management;
using TheKey_v2;
using TheKey_v2.Enum;
using Timer = System.Timers.Timer;

// GET Keyboard
var d = new Keyboard();

// Win API
var listener = UserNotificationListener.Current;

// Check call function every 1s
var timer = new Timer(2000);
timer.Start();
timer.Enabled = true;


// Check function
async Task ProcessCheck()
{
    /**
     * Check VS is in debug mode
     */
    // Get All process of VISUAL STUDIO
    var processesVs = Process.GetProcessesByName("devenv");
    // Test if process is debug
    if (processesVs.Length > 0)
        // Set light mode to rainbow
        if (processesVs.Any(p => p.MainWindowTitle.Contains("(D") || p.MainWindowTitle.Contains("(E")))
        {
            Debug.WriteLine("Set debug mode");
            d.SetLightColor(20, 100);
            return;
        }

    /**
     * If notification
     */
    var notification = await listener.GetNotificationsAsync(NotificationKinds.Toast);
    foreach (var _ in notification)
    {
        var current = DateTime.Now;
        var nDate = _.CreationTime;
        if (nDate.Day == current.Day && nDate.Hour == current.Hour && nDate.Minute == current.Minute)
        {
            Debug.WriteLine("Set normal mode");
            d.SetLightColor(207, 100);
            return;
        }
    }

    /**
     * Set normal mode
     */
    Debug.WriteLine("Set normal mode");
    d.SetLightColor(0, 10);
    d.SetLightMode(LightMode.Solid);
}

// Keep process run
while (true)
{
    await ProcessCheck();
    await Task.Delay(1000);
}