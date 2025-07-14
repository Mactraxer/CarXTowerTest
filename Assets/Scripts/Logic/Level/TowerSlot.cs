using System;
using UnityEngine;

[Serializable]
public struct TowerSlot
{
    public Vector3 Position;
    public TowerType Type;
    public TrajectoryMode TrajectoryMode;
}
