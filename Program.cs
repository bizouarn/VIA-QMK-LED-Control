using System.Diagnostics;
using Windows.UI.Notifications;
using Windows.UI.Notifications.Management;
using TheKey_v2;
using TheKey_v2.Enum;

const int TIMER_INTERVAL = 1000; // 1 second

// GET Keyboard
var d = new Keyboard();

// Win API
var listener = UserNotificationListener.Current;

////
// Keep process running
////
while (true)
{
    await ProcessCheck();
    await Task.Delay(TIMER_INTERVAL);
}

////
// Check function
////
async Task ProcessCheck()
{
    ////
    // Check VS is in debug mode
    ////
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

    ////
    // Check iƒ Windows notification
    ////
    var notification = await listener.GetNotificationsAsync(NotificationKinds.Toast);
    foreach (var _ in notification)
    {
        var current = DateTime.Now;
        var nDate = _.CreationTime;
        if (nDate.Day == current.Day && nDate.Hour == current.Hour)
        {
            var diff = current.Subtract(nDate.DateTime);
            if (diff.TotalSeconds < 60)
            {
                Debug.WriteLine("Set notification mode");
                d.SetLightColor(207, 100);
                return;
            }
        }
    }

    ////
    // Set normal mode
    ////
    Debug.WriteLine("Set normal mode");
    d.SetLightColor(0, 10);
    d.SetLightMode(LightMode.Solid);
}