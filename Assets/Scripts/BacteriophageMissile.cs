using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BacteriophageMissile : MonoBehaviour
{

    [Serializable] struct SpawnList { public float Chance; public GameObject toSpawn; }


    private SpriteRenderer sr;
    [SerializeField] Transform target;
    public float moveSpeed;
    public float rotationSpeed;
    public int damage;
    public EnemyAI goTarget;

    Rigidbody2D rb;
    Vector3 moveDirection;
    Vector3 lastMoveDirection;

    List<IHittable> collidingWith = new List<IHittable>();

    [SerializeField] SpawnList[] _spawnList;

    void Awake() {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
    }

    void Update()
    {
        if (collidingWith.Count>0) {
            Attack();
        }
    }

    void Attack() {
        collidingWith.ForEach(collider => collider.onHit(damage));
        Die();
    }

    void FixedUpdate() {
        Move();
    }

    public void Target(Transform currentTarget)
    {
        target = currentTarget;
        lastMoveDirection = (target.position - transform.position).normalized;
    }

    public void Move() {
        if (target!=null) 
            moveDirection = Vector3.RotateTowards(lastMoveDirection,(target.position - transform.position).normalized, rotationSpeed, 1);

        transform.up = moveDirection;

        lastMoveDirection = moveDirection;

        rb.MovePosition(transform.position + moveDirection * moveSpeed * Time.fixedDeltaTime);
    }

    void Die() {
        foreach (SpawnList x in _spawnList)
            if (UnityEngine.Random.Range(0, 100) <= x.Chance)
                Instantiate(x.toSpawn, transform.position, transform.rotation);

        Destroy(gameObject);
    }

    public void OnTriggerEnter2D(Collider2D collider) {
        if (collider.isTrigger) return;
        IHittable go = collider.gameObject.GetComponent<IHittable>();
        if (go == null) return;
        collidingWith.Add(go);
    }
}

public enum EnemyAI {
    PLAYER
}