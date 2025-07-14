using System.Collections.Generic;

public class LevelPresenter
{
    private readonly LevelModel _levelModel;
    private readonly LevelView _levelView;
    private readonly ITowerFactory _towerFactory;
    private readonly ITimerService _timerService;
    private readonly IEnemyFactory _enemyFactory;
    private readonly List<TowerPresenter> _towers;
    private readonly List<EnemyPresenter> _enemies;
    private ITimer _spawnEnemyRepeatingTimer;

    public LevelPresenter(LevelModel levelModel, LevelView levelView, ITowerFactory towerFactory, ITimerService timerService, IEnemyFactory enemyFactory)
    {
        _levelModel = levelModel;
        _levelView = levelView;
        _towerFactory = towerFactory;
        _timerService = timerService;
        _enemyFactory = enemyFactory;

        _towers = new List<TowerPresenter>();
        _enemies = new List<EnemyPresenter>();
    }

    public void Start()
    {
        Init();
        _spawnEnemyRepeatingTimer = _timerService.ScheduleRepeating(_levelModel.SpawnEnemyDelay, SpawnEnemy);
    }

    private void Init()
    {
        InitTowers();
    }

    private void InitTowers()
    {
        foreach (var towerSlot in _levelModel.TowerSlots)
        {
            var towerPresenter = _towerFactory.CreateTower(towerSlot.Position, towerSlot.Type);
            _towers.Add(towerPresenter);
            towerPresenter.Start();
        }
    }

    private void SpawnEnemy()
    {
        var enemy = _enemyFactory.Create();
        enemy.OnReachDestination += OnEnemyReachedDestination;
        enemy.OnDied += OnEnemyDied;
        _enemies.Add(enemy);
        enemy.StartMove();
    }

    private void OnEnemyDied(EnemyPresenter presenter)
    {
        presenter.OnReachDestination -= OnEnemyReachedDestination;
        presenter.OnDied -= OnEnemyDied;

        _enemyFactory.Dispose(presenter);
        _enemies.Remove(presenter);
    }

    private void OnEnemyReachedDestination(EnemyPresenter presenter)
    {
        presenter.OnReachDestination -= OnEnemyReachedDestination;
        presenter.OnDied -= OnEnemyDied;

        _enemyFactory.Dispose(presenter);
        _enemies.Remove(presenter);
    }
}