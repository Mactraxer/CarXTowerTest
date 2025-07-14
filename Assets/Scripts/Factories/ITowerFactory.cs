using UnityEngine;

public interface ITowerFactory : IService
{
    TowerPresenter CreateTower(Vector3 position, TowerType type);
    void Dispose(TowerPresenter tower);
}