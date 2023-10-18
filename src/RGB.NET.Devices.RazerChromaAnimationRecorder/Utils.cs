using ChromaAnimation;
using RGB.NET.Core;
using Color = ChromaAnimation.Color;

namespace RGB.NET.Devices.RazerChromaAnimationRecorder;

public static class Utils
{
    public static DeviceType Convert(RGBDeviceType type) => type switch
    {
        RGBDeviceType.Mousepad => DeviceType.Mousepad,
        RGBDeviceType.Mouse => DeviceType.Mouse,
        RGBDeviceType.Keypad => DeviceType.Keypad,
        RGBDeviceType.Keyboard => DeviceType.Keyboard,
        RGBDeviceType.Headset => DeviceType.Headset,
        RGBDeviceType.Unknown => DeviceType.ChromaLink,
        RGBDeviceType.LedMatrix => DeviceType.KeyboardExtended,
        _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
    };

    public static LedId GetInitialLedIdForDeviceType(RGBDeviceType type) => type switch
    {
        RGBDeviceType.Mousepad => LedId.Mousepad1,
        RGBDeviceType.Mouse => LedId.Mouse1,
        RGBDeviceType.Keypad => LedId.Keypad1,
        RGBDeviceType.Keyboard => LedId.Keyboard_Custom1,
        RGBDeviceType.Headset => LedId.Headset1,
        RGBDeviceType.Unknown => LedId.Unknown1,
        RGBDeviceType.LedMatrix => LedId.LedMatrix1,
        _ => LedId.Custom1
    };

    public static Color ConvertColor(Core.Color color)
    {
        return new Color(color.GetR(), color.GetG(), color.GetB(), color.GetA());
    }
}