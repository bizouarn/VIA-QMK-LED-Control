namespace TheKey_v2.Enum;

internal enum LightControl
{
    // QMK BACK LIGHT
    IdQmkBackLightBrightness = 0x09,
    IdQmkBackLightEffect = 0x0A,

    // QMK RGB LIGHT
    IdQmkRgbLightBrightness = 0x80,
    IdQmkRgbLightEffect = 0x81,
    IdQmkRgbLightEffectSpeed = 0x82,
    IdQmkRgbLightColor = 0x83
}