using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheKey_v2.Enum
{
    enum COM
    {
        // QMK BACKLIGHT
        id_qmk_backlight_brightness = 0x09,
        id_qmk_backlight_effect = 0x0A,

        // QMK RGBLIGHT
        id_qmk_rgblight_brightness = 0x80,
        id_qmk_rgblight_effect = 0x81,
        id_qmk_rgblight_effect_speed = 0x82,
        id_qmk_rgblight_color = 0x83,
    }
}
