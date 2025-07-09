using System;
using UnityEngine;

public interface IEnemy
{
    Vector3 Position { get; }
    event Action<IEnemy> OnDeath;
}