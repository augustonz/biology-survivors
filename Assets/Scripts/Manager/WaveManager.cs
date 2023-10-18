using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
public class WaveManager : MonoBehaviour
{

    [SerializeField] float _spawnRange;
    [SerializeField] Transform _enemiesParent;
    [SerializeField] List<Enemy> enemies;
    [SerializeField] float _spawnCooldown;
    [SerializeField] Enemy enemy1;
    [SerializeField] Enemy enemy2;
    [SerializeField] Enemy enemy3;
    [SerializeField] Wave[] _waves;

    Wave _currentWave;

    public static WaveManager instance;
    public UnityEvent OnEnemyKilled;
    int _enemiesKilledCount;
    public int EnemiesKilledCount { get => _enemiesKilledCount; }

    GameObject _player;
    float _spawnCooldownTimer;
    bool _canSpawn;
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
        Vector3 spawnPoint = GetRandomSpawnPoint();
        for (int i = 0; i < enemySpawnOption.SpawnAmount; i++)
        {
            enemies.Add(Instantiate(enemySpawnOption.EnemyToSpawn, spawnPoint, Quaternion.identity, _enemiesParent).GetComponent<Enemy>());
        }
        enemySpawnOption.SetSpawnTimer(enemySpawnOption.SpawnCooldown);
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
    public Enemy EnemyToSpawn;
    public int SpawnAmount;
    public float SpawnCooldown;
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