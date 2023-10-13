using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class WaveManager : MonoBehaviour
{

    [SerializeField] float _spawnRange;
    [SerializeField] float _spawnCooldown;
    [SerializeField] Transform _enemiesParent;
    [SerializeField] Enemy enemy1;
    [SerializeField] Enemy enemy2;
    [SerializeField] Enemy enemy3;

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
    void Start() {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update() {
        float currentTime = MatchTimer.instance.GetCurrentTIme;

        CheckSpawnEnemies(currentTime);
    }

    public void AddEnemiesKilledStat() {
        _enemiesKilledCount++;
    }

    void CheckSpawnEnemies(float currentTime) {
        if (!_canSpawn) {
            _spawnCooldownTimer-=Time.deltaTime;
            if (_spawnCooldownTimer>0) return;
            _canSpawn=true;
        }
        if (currentTime>10) SpawnEnemy(enemy1,1);
    }

    void SpawnEnemy(Enemy enemy,int amount) {
        Vector3 spawnPoint = GetRandomSpawnPoint();
        for (int i = 0; i < amount; i++)
        {
            Instantiate(enemy,spawnPoint,Quaternion.identity,_enemiesParent);
        }
        _canSpawn=false; 
        _spawnCooldownTimer=_spawnCooldown;
    }

    Vector3 GetRandomSpawnPoint() {
        Vector2 randomCirclePoint = Random.insideUnitCircle.normalized * _spawnRange;
        return _player.transform.position +  (Vector3) randomCirclePoint;
    }

    void OnDrawGizmos() {
        if (!_player) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(_player.transform.position,_spawnRange);
    }
}

