using System;
using Artemis.Core;
using Artemis.Core.Modules;
using Artemis.Core.Services;
using ChromaAnimation;
using RGB.NET.Devices.RazerChromaAnimationRecorder;

namespace Artemis.Plugins.ChromaAnimationRecorder;

// ReSharper disable once UnusedType.Global
public class ChromaRecorderModule : PluginFeature
{
    private readonly IWebServerService _webServerService;
    private readonly Plugin _plugin;
    
    public ChromaRecorderModule(IWebServerService webServerService,  Plugin plugin)
    {
        _webServerService = webServerService;
        _plugin = plugin;
    }
    
    public override void Enable()
    {
        _webServerService.AddJsonEndPoint<ChromaRecorderStartRequest>(this, "start", req =>
        {
            RazerChromaAnimationRecorderDeviceProvider.Instance.StartRecording(req.DeviceType);
        });
        _webServerService.AddJsonEndPoint<ChromaRecorderStopRequest>(this, "stop", req =>
        {
            var absoluteFileName = _plugin.ResolveRelativePath(req.FileName);
            RazerChromaAnimationRecorderDeviceProvider.Instance.StopRecording(req.DeviceType, absoluteFileName);
        });
    }

    public override void Disable()
    {
    }
}

// ReSharper disable once ClassNeverInstantiated.Global
public record ChromaRecorderStartRequest(DeviceType DeviceType);

// ReSharper disable once ClassNeverInstantiated.Global
public record ChromaRecorderStopRequest(DeviceType DeviceType, string FileName);