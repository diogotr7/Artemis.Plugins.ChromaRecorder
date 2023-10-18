using ChromaAnimation;
using RGB.NET.Core;

namespace RGB.NET.Devices.RazerChromaAnimationRecorder;

public class RazerChromaAnimationRecorderDeviceProvider : AbstractRGBDeviceProvider
{
    
    private static RazerChromaAnimationRecorderDeviceProvider? _instance;
    
    public static RazerChromaAnimationRecorderDeviceProvider Instance => _instance ??= new RazerChromaAnimationRecorderDeviceProvider();
    
    protected override void InitializeSDK()
    {

    }

    protected override IEnumerable<IRGBDevice> LoadDevices()
    {
        var defs = new RazerChromaAnimationRecorderDeviceInfo[]
        {
            new (RGBDeviceType.Mousepad, "Mousepad"),
            new (RGBDeviceType.Mouse, "Mouse"),
            new (RGBDeviceType.Keypad, "Keypad"),
            new (RGBDeviceType.Keyboard, "Keyboard"),
            new (RGBDeviceType.Headset, "Headset"),
            new (RGBDeviceType.Unknown, "ChromaLink"),
            new (RGBDeviceType.LedMatrix, "KeyboardExtended")
        };
        
        foreach (var def in defs)
        {
            yield return new RazerChromaAnimationRecorderDevice(def, GetUpdateTrigger());
        }
    }

    public void StartRecording(DeviceType type)
    {
        var device = Devices.OfType<RazerChromaAnimationRecorderDevice>().First(d => d.DeviceInfo.ChromaDeviceType == type);
        device.StartRecording();
    }
    
    public void StopRecording(DeviceType type, string fileName)
    {
        var device = Devices.OfType<RazerChromaAnimationRecorderDevice>().First(d => d.DeviceInfo.ChromaDeviceType == type);
        device.StopRecording(fileName);
    }
}