using HidLibrary;
using TheKey_v2.Enum;

namespace TheKey_v2;

internal class Keyboard
{
    private readonly List<HidDevice> _devices;

    public Keyboard(string filter)
    {
        _devices = HidDevices.Enumerate().Where(x => x.DevicePath.Contains(filter)).ToList();
    }

    private void Write(byte[] data)
    {
        // complete data to byte[32]
        var bytes = new byte[32];
        Buffer.BlockCopy(data, 0, bytes, 0, Math.Min(data.Length, bytes.Length));
        //send data
        foreach (var device in _devices) device.Write(bytes);
    }

    public void SetLightMode(LightMode mode)
    {
        Write(new byte[]
        {
            0x00, 0x07, (byte) LightControl.IdQmkRgbLightEffect, (byte) mode
        });
    }

    public void SetLightBrightness(int percent)
    {
        var brightness = (byte) (percent * 2.55);
        Write(new byte[]
        {
            0x00, 0x07, (byte) LightControl.IdQmkRgbLightBrightness, brightness
        });
    }

    // NOT TESTED
    public void SetLightModeSpeed(int percent)
    {
        var speed = (byte) (percent * 2.55);
        Write(new byte[]
        {
            0x00, 0x07, (byte) LightControl.IdQmkRgbLightEffectSpeed, speed
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
            0x00, 0x07, (byte) LightControl.IdQmkRgbLightColor, h, s
        });
    }

    // NOT TESTED
    public void SetBackLightModeSpeed(int percent)
    {
        var speed = (byte) (percent * 2.55);
        Write(new byte[]
        {
            0x00, 0x07, (byte) LightControl.IdQmkBackLightBrightness, speed
        });
    }

    // NOT TESTED
    public void SetBackLightMode(int mode)
    {
        Write(new byte[]
        {
            0x00, 0x07, (byte) LightControl.IdQmkBackLightEffect, (byte) mode
        });
    }
}