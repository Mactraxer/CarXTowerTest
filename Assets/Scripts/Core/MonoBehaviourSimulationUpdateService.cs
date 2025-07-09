using System.Collections.Generic;
using UnityEngine;

public class MonoBehaviourSimulationUpdateService : MonoBehaviour, ISimulationUpdateService
{
    private readonly List<ITickable> tickables = new();

    public void Register(ITickable tickable) => tickables.Add(tickable);
    public void Unregister(ITickable tickable) => tickables.Remove(tickable);

    public void Tick()
    {
        for (int i = 0; i < tickables.Count; i++)
        {
            tickables[i].Tick();
        }
    }

    private void Update() => Tick();

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
