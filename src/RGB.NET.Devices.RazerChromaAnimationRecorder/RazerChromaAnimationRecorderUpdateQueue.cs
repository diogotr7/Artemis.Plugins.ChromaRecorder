using ChromaAnimation;
using RGB.NET.Core;
using Color = RGB.NET.Core.Color;

namespace RGB.NET.Devices.RazerChromaAnimationRecorder;

internal sealed class RazerChromaAnimationRecorderUpdateQueue : UpdateQueue
{
    private readonly DeviceType _deviceType;
    private readonly ChromaAnimation.Color[][] _currentFrame;
    private readonly List<ColorFrame> _animation;
    private DateTime? _lastFrameTime;

    public bool IsRecording { get; set; }

    public RazerChromaAnimationRecorderUpdateQueue(RazerChromaAnimationRecorderDeviceInfo deviceInfo, IDeviceUpdateTrigger updateTrigger)
        : base(updateTrigger)
    {
        _deviceType = deviceInfo.ChromaDeviceType;
        var (rows, columns) = Animation.GetDeviceDimensions(_deviceType);
        _currentFrame = new ChromaAnimation.Color[rows][];
        for (var row = 0; row < rows; row++)
        {
            _currentFrame[row] = new ChromaAnimation.Color[columns];
        }

        _animation = new List<ColorFrame>();
    }

    protected override bool Update(in ReadOnlySpan<(object key, Color color)> dataSet)
    {
        if (!IsRecording)
        {
            foreach (var (key, color) in dataSet)
            {
                var (row, column) = ((int, int))key;
                _currentFrame[row][column] = Utils.ConvertColor(color);
            }

            return true;
        }

        var utcNow = DateTime.UtcNow;

        if (_lastFrameTime == null)
        {
            _lastFrameTime = utcNow;
            return true;// ignore first frame, save time and wait for next frame
        }
        
        var previous = CloneCurrentFrame();

        foreach (var (key, color) in dataSet)
        {
            var (row, column) = ((int, int))key;
            _currentFrame[row][column] = Utils.ConvertColor(color);
        }
        
        var duration = (float)(utcNow - _lastFrameTime.Value).TotalSeconds;
        _animation.Add(new ColorFrame(duration, previous));
        _lastFrameTime = DateTime.UtcNow;

        return true;
    }

    public void StartRecording()
    {
        IsRecording = true;
    }

    public void StopRecording(string path)
    {
        var animation = new Animation(_deviceType, _animation.ToArray());

        IsRecording = false;
        _lastFrameTime = null;
        _animation.Clear();

        File.WriteAllBytes(path, animation.ToBytes());
    }

    private ChromaAnimation.Color[][] CloneCurrentFrame()
    {
        var clone = new ChromaAnimation.Color[_currentFrame.Length][];

        for (var row = 0; row < _currentFrame.Length; row++)
        {
            clone[row] = new ChromaAnimation.Color[_currentFrame[row].Length];
            for (var column = 0; column < _currentFrame[row].Length; column++)
            {
                clone[row][column] = _currentFrame[row][column];
            }
        }

        return clone;
    }
}