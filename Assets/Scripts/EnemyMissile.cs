using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyMissile : MonoBehaviour, IHittable
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
    public float rotationSpeed;
    private float hp;
    public float maxHp;
    public int damage;
    public EnemyAI goTarget;

    [SerializeField] Sprite _spriteUp;
    [SerializeField] Sprite _spriteUpLeft;
    [SerializeField] Sprite _spriteLeft;
    [SerializeField] Sprite _spriteDownLeft;
    [SerializeField] Sprite _spriteDown;

    Rigidbody2D rb;
    Vector3 moveDirection;
    Vector3 lastMoveDirection;

    List<EnemyDamagable> collidingWith = new List<EnemyDamagable>();

    [SerializeField] SpawnList[] _spawnList;
    [SerializeField] int _xp;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        originalMaterial = sr.material;
    }

    void Start()
    {
        hp = maxHp;

        target = FindObjectOfType<Player>().transform;
        if (target == null) target = gameObject.transform;

        lastMoveDirection = Quaternion.AngleAxis(90, Vector3.forward) * (target.position - transform.position).normalized;
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
        collidingWith.ForEach(collider => collider.OnHit(damage));
        Die();
    }

    void FixedUpdate()
    {
        Move();
    }

    public void Move()
    {
        if (target != null)
            moveDirection = Vector3.RotateTowards(lastMoveDirection, (target.position - transform.position).normalized, rotationSpeed * Time.fixedDeltaTime, 1);

        //transform.up = moveDirection;

        lastMoveDirection = moveDirection;

        ChangeSprite(Vector3.Angle(moveDirection, Vector3.up), moveDirection.x > 0);


        rb.MovePosition(transform.position + moveDirection * moveSpeed * Time.fixedDeltaTime);
    }

    public void TakeDamage(float damageAmount)
    {
        _hitSource.Play();
        hp -= damageAmount;
        if (hp <= 0)
        {
            Die();
        }
    }

    void ChangeSprite(float angle, bool right)
    {

        if (angle < 25) //up
            sr.sprite = _spriteUp;
        else if (angle < 65)
            sr.sprite = _spriteUpLeft;
        else if (angle < 115)
            sr.sprite = _spriteLeft;
        else if (angle < 155)
            sr.sprite = _spriteDownLeft;
        else
            sr.sprite = _spriteDown;

        sr.flipX = right;
    }

    void Die()
    {
        foreach (SpawnList x in _spawnList)
            if (UnityEngine.Random.Range(0, 100) <= x.Chance)
                Instantiate(x.toSpawn, transform.position, transform.rotation);

        _deathSource.Play();
        _deathSource.transform.SetParent(null);
        Destroy(_deathSource.gameObject, 3);
        ExpManager.instance.SpawnExp(transform.position, _xp);
        WaveManager.instance.OnEnemyKilled.Invoke();
        Destroy(gameObject);
    }

    public void onHit(Bullet bullet)
    {
        TakeDamage(bullet.getDamage());
        flashAnimation();
    }
    public void onHit(float damage)
    {
        TakeDamage(damage);
        flashAnimation();
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.isTrigger) return;
        EnemyDamagable go = collider.gameObject.GetComponent<EnemyDamagable>();
        if (go == null) return;
        collidingWith.Add(go);
    }

    public void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.isTrigger) return;
        EnemyDamagable go = collider.gameObject.GetComponent<EnemyDamagable>();
        if (go == null) return;
        collidingWith.Remove(go);
    }

    public void flashAnimation()
    {
        sr.material = flashMaterial;
        Invoke("resetSpriteMaterial", flashDuration);
    }

    public void resetSpriteMaterial()
    {
        sr.material = originalMaterial;
    }
}