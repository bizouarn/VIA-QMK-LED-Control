namespace TheKey_v2.Enum;

internal enum LigthControl
{
    // QMK BACKLIGHT
    IdQmkBacklightBrightness = 0x09,
    IdQmkBacklightEffect = 0x0A,

    // QMK RGBLIGHT
    IdQmkRgblightBrightness = 0x80,
    IdQmkRgblightEffect = 0x81,
    IdQmkRgblightEffectSpeed = 0x82,
    IdQmkRgblightColor = 0x83
}