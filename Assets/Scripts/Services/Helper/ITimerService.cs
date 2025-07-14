using System;

public interface ITimerService : IService
{
    ITimer ScheduleOnce(float delay, Action callback);
    ITimer ScheduleRepeating(float interval, Action callback);
    void StopScheduledTimer(ITimer timer);
}
