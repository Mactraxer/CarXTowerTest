using System;
using UnityEngine;

public interface IEnemy : ITarget
{
    Vector3 Position { get; }
    event Action<IEnemy> OnDeath;
}