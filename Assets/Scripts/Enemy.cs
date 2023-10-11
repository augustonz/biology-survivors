using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour,IHittable
{
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
    GameObject healthBar;
    Canvas canvas;

    List<EnemyDamagable> collidingWith = new List<EnemyDamagable>();

    void Awake() {
        rb = GetComponent<Rigidbody2D>();
        canvas = GetComponentInChildren<Canvas>();
        sr = GetComponent<SpriteRenderer>();
        originalMaterial = sr.material;
        
        healthBar = transform.Find("Canvas/HealthBar").gameObject;
    }

    void Start()
    {
        healthBar.SetActive(false);
        hp = maxHp;

        target = FindObjectOfType<Player>().transform;
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
        moveDirection = (target.position - transform.position).normalized;
        rb.MovePosition(transform.position + moveDirection * moveSpeed * Time.fixedDeltaTime);
    }

    public void TakeDamage(float damageAmount) {
        hp-=damageAmount;
        if (hp<=0) {
            Die();
        }
    }

    void Die() {
        GameObject expPoint = PrefabLoader.instance.getExp();
        Instantiate(expPoint,transform.position,Quaternion.identity);
        Destroy(gameObject);
    }

    public void onHit(Bullet bullet) {
        TakeDamage(bullet.getDamage());
        flashAnimation();
    }

    public void OnTriggerEnter2D(Collider2D collider) {
        EnemyDamagable go = collider.gameObject.GetComponent<EnemyDamagable>();
        if (go == null) return;
        collidingWith.Add(go);
        if (delayBetweenAttacksTimer<=0) delayBetweenAttacksTimer=delayBetweenAttacks * 0.2f;
    }

    public void OnTriggerExit2D(Collider2D collider) {
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