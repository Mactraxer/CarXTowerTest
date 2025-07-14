using System.Collections.Generic;

public class LevelModel
{
    private readonly List<TowerSlot> _towerSlots;
    private readonly float _spawnEnemyDelay;

    public TowerSlot[] TowerSlots => _towerSlots.ToArray();

    public float SpawnEnemyDelay => _spawnEnemyDelay;

    public LevelModel(TowerSlot[] towerSlots, float spawnEnemyDelay)
    {
        _towerSlots = new List<TowerSlot>(towerSlots);
        _spawnEnemyDelay = spawnEnemyDelay;
    }
}
