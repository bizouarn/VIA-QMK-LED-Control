using System.Diagnostics;
using Windows.UI.Notifications;
using Windows.UI.Notifications.Management;
using TheKey_v2;
using TheKey_v2.Enum;

const short TIMER_INTERVAL = 1000; // 1 second

// GET Keyboard
var d = new Keyboard("vid_359b");
var status = "?";

// Win API
var listener = UserNotificationListener.Current;

// EXIT Contition
var startDate = DateTime.Now;
var maxRuntime = 39 / 5 + 1;
var exit = false;

////
// Keep process running
////
while (exit == false)
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
    // Check runtime time
    ////
    var runTime = DateTime.Now - startDate;
    if (runTime >= TimeSpan.FromHours(maxRuntime)) SetStatus("end");

    ////
    // Check iƒ Windows notification
    ////
    var notifications = await listener.GetNotificationsAsync(NotificationKinds.Toast);
    var now = DateTime.Now;
    var recentNotification = notifications.FirstOrDefault(n => (now - n.CreationTime.DateTime).TotalSeconds <= 60);
    if (recentNotification != null)
    {
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
            d.SetLightMode(LightMode.Breathing3);
            break;
        case "debugE":
            Debug.WriteLine("Set debug exec mode");
            d.SetLightColor(20, 100);
            d.SetLightMode(LightMode.Solid);
            break;
        case "notify":
            Debug.WriteLine("Set notification mode");
            d.SetLightColor(207, 100);
            d.SetLightMode(LightMode.RainbowMood1);
            break;
        case "end":
            Debug.WriteLine("Set end mode");
            d.SetLightMode(LightMode.Off);
            exit = true;
            break;
        default:
            Debug.WriteLine("Set normal mode");
            d.SetLightColor(0, 10);
            d.SetLightMode(LightMode.Solid);
            break;
    }
}