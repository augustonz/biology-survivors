using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour, IHittable
{
    [SerializeField] AudioSource _deathSource;
    [SerializeField] AudioSource _hitSource;
    [SerializeField] SpriteRenderer sr;
    [SerializeField] Material flashMaterial;
    [SerializeField] float flashDuration;
    [SerializeField] Material originalMaterial;
    [SerializeField] float moveSpeed;
    [SerializeField] float maxHp;
    [SerializeField] int _damage;
    [SerializeField] float _detectPlayerRange;
    [SerializeField] float _dashSpeed;
    [SerializeField] float _dashDuration;
    [SerializeField] float _preSpeed;
    Animator _anim;
    bool _isChargingAttack;
    bool _isAttacking;
    Vector3 _chargeTarget;
    Vector3 _chargeInitialPosition;
    Vector3 moveDirection;
    Transform target;
    float hp;
    float delayBetweenAttacks = 0.5f;
    float delayBetweenAttacksTimer;
    bool _isKnockBack = false;
    Rigidbody2D rb;
    List<EnemyDamagable> collidingWith = new List<EnemyDamagable>();

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        originalMaterial = sr.material;
    }

    void Start()
    {
        hp = maxHp;

        target = FindObjectOfType<Player>().transform;
        if (target == null) target = gameObject.transform;
        _anim.SetFloat("PreSpeed",_preSpeed);
    }

    void Update()
    {
        if (IsIdle()) sr.flipX = target.transform.position.x > transform.position.x;

        if (delayBetweenAttacksTimer <= 0 && collidingWith.Count>0)
        {
            Attack(collidingWith[0]);
        }
        delayBetweenAttacksTimer -= Time.deltaTime;

        if (IsIdle() && Vector2.Distance(target.transform.position,transform.position) <= _detectPlayerRange) {
            StartChargingAttack();
        }
    }

    bool IsIdle() {
        return !_isChargingAttack && !_isAttacking;
    } 

    void Attack(EnemyDamagable target)
    {
        target.OnHit(_damage);
        delayBetweenAttacksTimer = delayBetweenAttacks;
    }

    void FixedUpdate()
    {
        Move();
    }

    public void Move()
    {
        if (target == null) target = transform;
        moveDirection = (target.position - transform.position).normalized;

        rb.velocity = rb.velocity + (Vector2)(moveDirection * 6f);

        if (_isChargingAttack) {
            rb.velocity = Vector2.zero;
        } else if (_isAttacking) {
            rb.velocity = GetAttackingDirection() * _dashSpeed;
        } else if (!_isKnockBack && rb.velocity.magnitude > moveSpeed) {
            rb.velocity = rb.velocity.normalized * moveSpeed;
        }
    }

    Vector2 GetAttackingDirection() {
        return (_chargeTarget - _chargeInitialPosition).normalized;
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

    public void StartChargingAttack() {
        _isChargingAttack=true;
        _chargeTarget = target.transform.position;
        _chargeInitialPosition = transform.position;
        _anim.SetTrigger("Pre");
    }

    public void StartAttaking() {
        _isChargingAttack=false;
        _isAttacking = true;
        Invoke("StopAttaking",_dashDuration);
    }

    public void StopAttaking() {
        _isAttacking = false;
        _anim.SetTrigger("Idle");
    }

    void Die() {
        _deathSource.Play();
        _deathSource.transform.SetParent(null);
        Destroy(_deathSource.gameObject, 3);
        
        WaveManager.instance.OnEnemyKilled.Invoke();

        Destroy(gameObject);
    }

    public void onHit(Bullet bullet)
    {
        Vector2 hitDirection = (transform.position - target.transform.position).normalized;
        //ApplyKnockback(hitDirection * bullet.KnockBack);
        TakeDamage(bullet.getDamage());
        flashAnimation();
    }
    public void onHit(ExplosionEfetivation explosion)
    {
        Vector2 hitDirection = (transform.position - target.transform.position).normalized;
        //ApplyKnockback(hitDirection * 1);
        TakeDamage(explosion.damage);
        flashAnimation();
    }

    async void ApplyKnockback(Vector2 knockBackDirection) {
        _isKnockBack = true;
        rb.velocity = Vector2.zero;
        rb.AddForce(knockBackDirection,ForceMode2D.Impulse);
        await Task.Delay(250);
        _isKnockBack = false;
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.isTrigger) return;
        EnemyDamagable go = collider.gameObject.GetComponent<EnemyDamagable>();
        if (go == null) return;

        collidingWith.Add(go);
        Attack(go);
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

    void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position,_detectPlayerRange);
    }
}