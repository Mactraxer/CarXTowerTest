using System;
using System.Collections.Generic;
using UnityEngine;

public class TimerService : ITimerService, ITickable
{
    private readonly List<TimerInstance> _timers = new();

    public ITimer ScheduleOnce(float delay, Action callback)
    {
        var timer = new TimerInstance(callback, delay, false);
        _timers.Add(timer);
        return timer;
    }

    public ITimer ScheduleRepeating(float interval, Action callback)
    {
        var timer = new TimerInstance(callback, interval, true);
        _timers.Add(timer);
        return timer;
    }

    public void StopScheduledTimer(ITimer timer)
    {
        timer.Stop();
        _timers.Remove((TimerInstance)timer);
    }

    public void Tick()
    {
        for (int i = _timers.Count - 1; i >= 0; i--)
        {
            var timer = _timers[i];
            if (!timer.IsRunning)
            {
                _timers.RemoveAt(i);
                continue;
            }

            timer.Elapsed += Time.deltaTime;
            if (timer.Elapsed >= timer.Interval)
            {
                timer.Callback?.Invoke();
                if (timer.Repeat)
                {
                    timer.Elapsed = 0f;
                }
                else
                {
                    timer.Stop();
                    _timers.RemoveAt(i);
                }
            }
        }
    }
}
