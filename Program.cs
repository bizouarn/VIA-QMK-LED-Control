using System.Diagnostics;
using Windows.UI.Notifications;
using Windows.UI.Notifications.Management;
using TheKey_v2;
using TheKey_v2.Enum;

const int TIMER_INTERVAL = 1000; // 1 second

// GET Keyboard
var d = new Keyboard();
var status = "?";

// Win API
var listener = UserNotificationListener.Current;

////
// Keep process running
////
while (true)
{
    try
    {
        await ProcessCheck();
    }
    catch (Exception e)
    {
        Debug.WriteLine(e);
        throw;
    }

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
        foreach (var p in processesVs)
        {
            if (!p.MainWindowTitle.Contains("visual studio", StringComparison.OrdinalIgnoreCase)) continue;
            if (p.MainWindowTitle.Contains("(D"))
            {
                SetStatus("debugB");
                return;
            }

            if (p.MainWindowTitle.Contains("(E") ||
                p.MainWindowTitle.Contains("(É"))
            {
                SetStatus("debugE");
                return;
            }
        }

    ////
    // Check iƒ Windows notification
    ////
    var notification = await listener.GetNotificationsAsync(NotificationKinds.Toast);
    foreach (var _ in notification)
    {
        var current = DateTime.Now;
        var nDate = _.CreationTime;
        var diff = current.Subtract(nDate.DateTime);
        if (diff.TotalSeconds > 60)
            continue;
        SetStatus("notify");
        return;
    }

    ////
    // Set normal mode
    ////
    SetStatus("");
}

void SetStatus(string? statusP)
{
    if (status == statusP) return;
    status = statusP;
    switch (status)
    {
        case "debugB":
            Debug.WriteLine("Set debug breakpoint mode");
            d.SetLightColor(0, 100);
            break;
        case "debugE":
            Debug.WriteLine("Set debug exec mode");
            d.SetLightColor(20, 100);
            break;
        case "notify":
            Debug.WriteLine("Set notification mode");
            d.SetLightColor(207, 100);
            break;
        default:
            Debug.WriteLine("Set normal mode");
            d.SetLightColor(0, 10);
            d.SetLightMode(LightMode.Solid);
            break;
    }
}