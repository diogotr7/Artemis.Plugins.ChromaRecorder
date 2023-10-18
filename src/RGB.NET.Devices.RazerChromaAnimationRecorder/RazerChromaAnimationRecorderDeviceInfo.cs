using ChromaAnimation;
using RGB.NET.Core;

namespace RGB.NET.Devices.RazerChromaAnimationRecorder;

public class RazerChromaAnimationRecorderDeviceInfo : IRGBDeviceInfo
{
    public DeviceType ChromaDeviceType { get; }
    public RGBDeviceType DeviceType { get; }
    public string DeviceName { get; }
    public string Manufacturer => "ChromaRecorder";
    public string Model { get; }
    public object? LayoutMetadata { get; set; }
    
    public RazerChromaAnimationRecorderDeviceInfo(RGBDeviceType deviceType, string model)
    {
        ChromaDeviceType = Utils.Convert(deviceType);
        DeviceType = deviceType;
        Model = model;
        DeviceName = DeviceHelper.CreateDeviceName(Manufacturer, Model);
    }
}