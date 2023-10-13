using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour,IHittable
{
    [SerializeField] AudioSource _deathSource;
    [SerializeField] AudioSource _hitSource;

    [Serializable] struct SpawnList { public float Chance; public GameObject toSpawn; }


    public Material flashMaterial;
    public float flashDuration;
    private Material originalMaterial;
    private SpriteRenderer sr;
    Transform target;
    public float moveSpeed;
    private float hp;
    public float maxHp;
    public int damage;
    public float delayBetweenAttacks;
    private float delayBetweenAttacksTimer;
    public EnemyAI goTarget;

    Rigidbody2D rb;
    Vector3 moveDirection;

    List<EnemyDamagable> collidingWith = new List<EnemyDamagable>();

    [SerializeField] SpawnList[] _spawnList;

    void Awake() {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        originalMaterial = sr.material;
    }

    void Start()
    {
        hp = maxHp;

        target = FindObjectOfType<Player>().transform;
        if (target==null) target=gameObject.transform;
    }

    void Update()
    {
        if (collidingWith.Count>0) {
            if (delayBetweenAttacksTimer<=0){
                Attack();
            }
            delayBetweenAttacksTimer-=Time.deltaTime;
        }
    }

    void Attack() {
        collidingWith.ForEach(collider => collider.OnHit(damage));
        delayBetweenAttacksTimer=delayBetweenAttacks;
    }

    void FixedUpdate() {
        Move();
    }

    public void Move() {
        if (target==null) target = transform;
        moveDirection = (target.position - transform.position).normalized;
        rb.MovePosition(transform.position + moveDirection * moveSpeed * Time.fixedDeltaTime);
    }

    public void TakeDamage(float damageAmount) {
        _hitSource.Play();
        hp-=damageAmount;
        if (hp<=0) {
            Die();
        }
    }

    void Die() {
        foreach (SpawnList x in _spawnList)
            if (UnityEngine.Random.Range(0, 100) <= x.Chance)
                Instantiate(x.toSpawn, transform.position, transform.rotation);

        _deathSource.Play();
        _deathSource.transform.SetParent(null);
        Destroy(_deathSource.gameObject, 3);
        ExpManager.instance.SpawnExp(transform.position);
        Destroy(gameObject);
    }

    public void onHit(Bullet bullet) {
        TakeDamage(bullet.getDamage());
        flashAnimation();
    }

    public void OnTriggerEnter2D(Collider2D collider) {
        if (collider.isTrigger) return;
        EnemyDamagable go = collider.gameObject.GetComponent<EnemyDamagable>();
        if (go == null) return;
        collidingWith.Add(go);
        if (delayBetweenAttacksTimer<=0) delayBetweenAttacksTimer=delayBetweenAttacks * 0.2f;
    }

    public void OnTriggerExit2D(Collider2D collider) {
        if (collider.isTrigger) return;
        EnemyDamagable go = collider.gameObject.GetComponent<EnemyDamagable>();
        if (go == null) return;
        collidingWith.Remove(go);
        if (collidingWith.Count==0) delayBetweenAttacksTimer = delayBetweenAttacks * 0.2f;
    }

    public void flashAnimation() {
        sr.material = flashMaterial;
        Invoke("resetSpriteMaterial",flashDuration);
    }

    public void resetSpriteMaterial() {
        sr.material = originalMaterial;
    }
}

public enum EnemyAI {
    PLAYER
}