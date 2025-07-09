using System;

public class TimerInstance : ITimer
{
    public float Elapsed;
    public float Interval;
    public bool Repeat;
    public Action Callback;
    public bool IsRunning { get; private set; } = true;

    public TimerInstance(Action callback, float interval, bool repeat)
    {
        Callback = callback;
        Interval = interval;
        Repeat = repeat;
        Elapsed = 0f;
    }

    public void Stop()
    {
        IsRunning = false;
        Callback = null;
    }
}
