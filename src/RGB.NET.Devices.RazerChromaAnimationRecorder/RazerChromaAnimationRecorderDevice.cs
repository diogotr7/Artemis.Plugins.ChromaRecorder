using ChromaAnimation;
using RGB.NET.Core;

namespace RGB.NET.Devices.RazerChromaAnimationRecorder;

public class RazerChromaAnimationRecorderDevice : AbstractRGBDevice<RazerChromaAnimationRecorderDeviceInfo>
{
    public RazerChromaAnimationRecorderDevice(RazerChromaAnimationRecorderDeviceInfo deviceInfo , IDeviceUpdateTrigger updateTrigger)
        : base(deviceInfo, new RazerChromaAnimationRecorderUpdateQueue(deviceInfo, updateTrigger))
    {
        RequiresFlush = true;
        InitializeLayout();
    }

    private void InitializeLayout()
    {
        var led1 = Utils.GetInitialLedIdForDeviceType(DeviceInfo.DeviceType);
        var (rows, columns) = Animation.GetDeviceDimensions(DeviceInfo.ChromaDeviceType);

        for (var row = 0; row < rows; row++)
        {
            for (var column = 0; column < columns; column++)
            {
                AddLed(led1++, new Point(20 * column, 20 * row), new Size(19), (row, column));
            }
        }
    }
    
    public void StartRecording()
    {
        ((RazerChromaAnimationRecorderUpdateQueue)UpdateQueue).StartRecording();
    }
    
    public void StopRecording(string fileName)
    {
        ((RazerChromaAnimationRecorderUpdateQueue)UpdateQueue).StopRecording(fileName);
    }
}