using UnityEngine;

public interface ITowerFactory : IService
{
    TowerPresenter CreateTower(Vector3 position, TowerType type, TrajectoryMode trajectoryMode);
    void Dispose(TowerPresenter tower);
}