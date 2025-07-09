using System;
using UnityEngine;

[Serializable]
public struct TowerSlot
{
    public Vector3 Position;
    public TowerType Type;

    public TowerSlot(Vector3 position, TowerType type)
    {
        Position = position;
        Type = type;
    }
}
