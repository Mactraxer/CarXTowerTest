public interface ITimer
{
    bool IsRunning { get; }
    void Stop();
}