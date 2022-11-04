using HidLibrary;
using TheKey_v2.Enum;

namespace TheKey_v2;

internal class Keyboard
{
    private readonly List<HidDevice> _devices;

    public Keyboard()
    {
        _devices = HidDevices.Enumerate().Where(x => x.DevicePath.Contains("vid_feed")).ToList();
    }

    private void Write(byte[] data)
    {
        // complete data to byte[32]
        var bytes = new byte[32];
        foreach (var (b, i) in data.Select((b, i) => (b, i))) bytes[i] = b;
        foreach (var device in _devices) device.Write(bytes);
    }

    public void SetLightMode(LightMode mode)
    {
        Write(new byte[]
        {
            0x00, 0x07, (byte) LigthControl.IdQmkRgblightEffect, (byte) mode
        });
    }

    public void SetLightBrightness(int percent)
    {
        var brightness = (byte) (percent * 2.55);
        Write(new byte[]
        {
            0x00, 0x07, (byte) LigthControl.IdQmkRgblightBrightness, brightness
        });
    }

    // NOT TESTED
    public void SetLightModeSpeed(int percent)
    {
        var speed = (byte) (percent * 2.55);
        Write(new byte[]
        {
            0x00, 0x07, (byte) LigthControl.IdQmkRgblightEffectSpeed, speed
        });
    }

    // Note color is in HSL format
    public void SetLightColor(int hue, int saturation, int light = 100)
    {
        var h = (byte) (hue * 100 / 360 * 2.55);
        var s = (byte) (saturation * 2.55);
        var l = (byte) (light * 2.55);
        SetLightBrightness(l);
        Write(new byte[]
        {
            0x00, 0x07, (byte) LigthControl.IdQmkRgblightColor, h, s
        });
    }

    // NOT TESTED
    public void SetBackLightModeSpeed(int percent)
    {
        var speed = (byte) (percent * 2.55);
        Write(new byte[]
        {
            0x00, 0x07, (byte) LigthControl.IdQmkBacklightBrightness, speed
        });
    }

    // NOT TESTED
    public void SetBackLightMode(int mode)
    {
        Write(new byte[]
        {
            0x00, 0x07, (byte) LigthControl.IdQmkBacklightEffect, (byte) mode
        });
    }
}