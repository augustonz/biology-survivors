using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
public class WaveManager : MonoBehaviour
{
    [Header("Waves config")]
    [SerializeField] float _spawnRange;
    [SerializeField] Transform _enemiesParent;
    [SerializeField] List<Enemy> enemies;
    [SerializeField] Wave[] _waves;

    [Header("Boss config")]
    [SerializeField] GameObject _bossPrefab;
    [SerializeField] float _startBossTime;
    public UnityEvent _killBoss;
    Wave _currentWave;
    GameObject _bossInstance;
    bool _bossSpawned;


    public static WaveManager instance;
    public UnityEvent OnEnemyKilled;
    int _enemiesKilledCount;
    public int EnemiesKilledCount { get => _enemiesKilledCount; }
    GameObject _player;
    bool _canSpawn;
    Vector3[] _cardinalSpawnPoints = new Vector3[] {
        new Vector3(1,0,0),
        new Vector3(-1,0,0),
        new Vector3(0,1,0),
        new Vector3(0,-1,0)
    };
    void Awake()
    {
        if (instance != null && instance != this) Destroy(this);
        else instance = this;
    }
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _canSpawn = false;
    }

    void Update()
    {
        if (_bossSpawned && _bossInstance == null) {
            _killBoss.Invoke();
            _bossInstance = gameObject;
        }

        if (MatchTimer.instance.GetCurrentTIme>_startBossTime && _bossSpawned==false) {
            SpawnBoss();
        }

        if (!_canSpawn) return;
        _currentWave = GetCurrentWave();

        SpawningWave(_currentWave);
    }



    void SpawningWave(Wave wave)
    {
        if (wave == null) return;
        foreach (EnemySpawnOption enemySpawnOption in wave.EnemySpawnOptions)
        {
            enemySpawnOption.UpdateTimer(Time.deltaTime);
            if (enemySpawnOption.ShouldSpawn())
            {
                SpawnEnemy(enemySpawnOption);
            }
        }

    }

    Wave GetCurrentWave()
    {
        float currentTime = MatchTimer.instance.GetCurrentTIme;

        foreach (Wave wave in _waves)
        {
            if (currentTime >= wave.StartWaveTime && currentTime < wave.EndWaveTime)
            {
                return wave;
            }
        }
        return null;
    }

    public void OnEnemyDeath(Enemy e)
    {
        enemies.Remove(e);
    }

    public Transform GetEnemy()
    {
        if (enemies.Count > 0)
        {
            return enemies[0].gameObject.transform;
        }
        return null;
    }

    public void AddEnemiesKilledStat()
    {
        _enemiesKilledCount++;
    }

    public void ChangeState(GameState gameState)
    {
        if (gameState == GameState.INGAME) _canSpawn = true;
        if (gameState != GameState.INGAME) _canSpawn = false;
    }

    void SpawnEnemy(EnemySpawnOption enemySpawnOption)
    {
        Vector3 spawnPoint;
        switch(enemySpawnOption.SpawnPointType)
        {
            case SpawnPointType.HORIZONTAL:
                spawnPoint = GetRandomHorizontalSpawnPoint();
                break;

            case SpawnPointType.VERTICAL:
                spawnPoint = GetRandomVerticalSpawnPoint();
                break;

            case SpawnPointType.CARDINAL:
                spawnPoint = GetRandomCardinalSpawnPoint();
                break;

            case SpawnPointType.ANY:
                spawnPoint = GetRandomSpawnPoint();
                break;

            default:
                spawnPoint = GetRandomSpawnPoint();
                break;
        }
        for (int i = 0; i < enemySpawnOption.SpawnAmount; i++)
        {
            enemies.Add(Instantiate(enemySpawnOption.EnemyToSpawn, spawnPoint + Vector3.up * i, Quaternion.identity, _enemiesParent).GetComponent<Enemy>());
        }
        enemySpawnOption.SetSpawnTimer(enemySpawnOption.SpawnCooldown);
    }

    void SpawnBoss()
    {
        MatchTimer.instance.pauseTimer();
        Vector3 spawnPoint = GetRandomSpawnPoint();

        _bossInstance = Instantiate(_bossPrefab, spawnPoint, Quaternion.identity, _enemiesParent);
        _bossSpawned=true;
    }

    Vector3 GetRandomCardinalSpawnPoint() {
        int pos = UnityEngine.Random.Range(0,4);
        return _player.transform.position + _cardinalSpawnPoints[pos] * _spawnRange;
    }

    Vector3 GetRandomHorizontalSpawnPoint() {
        int pos = UnityEngine.Random.Range(0,2);
        return _player.transform.position + _cardinalSpawnPoints[pos] * _spawnRange;
    }

    Vector3 GetRandomVerticalSpawnPoint() {
        int pos = UnityEngine.Random.Range(2,4);
        return _player.transform.position + _cardinalSpawnPoints[pos] * _spawnRange;
    }

    Vector3 GetRandomSpawnPoint()
    {
        Vector2 randomCirclePoint = UnityEngine.Random.insideUnitCircle.normalized * _spawnRange;
        return _player.transform.position + (Vector3)randomCirclePoint;
    }

    void OnDrawGizmos()
    {
        if (!_player) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(_player.transform.position, _spawnRange);
    }
}

[Serializable]
public class Wave
{
    public float StartWaveTime;
    public float EndWaveTime;
    public EnemySpawnOption[] EnemySpawnOptions;
}

[Serializable]
public class EnemySpawnOption
{
    public GameObject EnemyToSpawn;
    public int SpawnAmount;
    public float SpawnCooldown;
    public SpawnPointType SpawnPointType;
    float _spawnTimer;

    public void SetSpawnTimer(float newTimer)
    {
        _spawnTimer = newTimer;
    }

    public bool ShouldSpawn()
    {
        return _spawnTimer <= 0;
    }

    public void UpdateTimer(float deltaTime)
    {
        _spawnTimer -= deltaTime;
    }

}

public enum SpawnPointType {
    ANY,HORIZONTAL,VERTICAL,CARDINAL
}