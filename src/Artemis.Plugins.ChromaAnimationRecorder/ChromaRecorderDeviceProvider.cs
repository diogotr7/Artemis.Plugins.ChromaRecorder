using RGB.NET.Core;
using Artemis.Core.DeviceProviders;
using Artemis.Core.Services;
using RGB.NET.Devices.RazerChromaAnimationRecorder;

namespace Artemis.Plugins.ChromaAnimationRecorder;

// ReSharper disable once UnusedType.Global
public class ChromaRecorderDeviceProvider : DeviceProvider
{
    private readonly IDeviceService _deviceService;
    
    public ChromaRecorderDeviceProvider(IDeviceService deviceService)
    {
        _deviceService = deviceService;
    }
    
    public override void Enable()
    {
        _deviceService.AddDeviceProvider(this);
    }

    public override void Disable()
    {
        _deviceService.RemoveDeviceProvider(this);
    }

    public override IRGBDeviceProvider RgbDeviceProvider => RazerChromaAnimationRecorderDeviceProvider.Instance;
}