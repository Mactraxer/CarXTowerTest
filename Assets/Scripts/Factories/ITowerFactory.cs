using UnityEngine;

public interface ITowerFactory : IService
{
    public TowerPresenter CreateTower(Vector3 position, TowerType type);
}