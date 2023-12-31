using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class BacteriophageMissile : MonoBehaviour
{

    [Serializable] struct SpawnList { public float Chance; public GameObject toSpawn; }
    [SerializeField] ExplosionEfetivation _explosion;

    private SpriteRenderer sr;
    [SerializeField] Transform target;
    public float moveSpeed;
    public float rotationSpeed;
    public float damage;
    public float explosionScale;
    public EnemyAI goTarget;

    Rigidbody2D rb;
    Vector3 moveDirection;
    Vector3 lastMoveDirection;

    List<IHittable> collidingWith = new List<IHittable>();

    [SerializeField] SpawnList[] _spawnList;

    public static void Create(Vector3 origin, Transform target, float damage = 10)
    {
        GameObject prefab = PrefabLoader.instance.GetFriendlyMissile();
        GameObject missileObject = Instantiate(prefab, origin, new Quaternion());
        BacteriophageMissile missileScript = missileObject.GetComponent<BacteriophageMissile>();

        missileScript.damage = damage;
        missileScript.Target(target);
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
    }

    void Update()
    {
        if (collidingWith.Count > 0)
        {
            Attack();
        }
    }

    void Attack()
    {
        Die();
    }

    void FixedUpdate()
    {
        Move();
    }

    public void Target(Transform currentTarget)
    {
        target = currentTarget;

        lastMoveDirection = Quaternion.AngleAxis(90, Vector3.forward) * (target.position - transform.position).normalized;
    }

    public void Move()
    {
        if (target != null)
            moveDirection = Vector3.RotateTowards(lastMoveDirection, (target.position - transform.position).normalized, rotationSpeed * Time.fixedDeltaTime, 1);

        transform.up = moveDirection;

        lastMoveDirection = moveDirection;

        rb.MovePosition(transform.position + moveDirection * moveSpeed * Time.fixedDeltaTime);
    }

    void Die()
    {
        foreach (SpawnList x in _spawnList)
            if (UnityEngine.Random.Range(0, 100) <= x.Chance)
                Instantiate(x.toSpawn, transform.position, transform.rotation);

        Instantiate(_explosion, transform.position, _explosion.transform.rotation).ApplyDamage(explosionScale, damage);

        Destroy(gameObject);
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.isTrigger) return;
        IHittable go = collider.gameObject.GetComponent<IHittable>();
        DamageValue.Instantiate(transform.position, (int)damage);
        if (go == null) return;
        collidingWith.Add(go);
    }

    public float getDamage()
    {
        return damage;
    }
}

public enum EnemyAI
{
    PLAYER
}