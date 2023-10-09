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
    Slider healthBarFill;
    GameObject healthBar;
    Canvas canvas;

    List<EnemyDamagable> collidingWith = new List<EnemyDamagable>();

    void Awake() {
        rb = GetComponent<Rigidbody2D>();
        healthBarFill = GetComponentInChildren<Slider>();
        canvas = GetComponentInChildren<Canvas>();
        sr = GetComponent<SpriteRenderer>();
        originalMaterial = sr.material;
        
        healthBar = transform.Find("Canvas/HealthBar").gameObject;
    }

    void Start()
    {
        healthBar.SetActive(false);
        hp = maxHp;
    }

    void Update()
    {
        target = FindTarget();
        
        AdjustHealthBarLocation();

        if (collidingWith.Count>0) {
            if (delayBetweenAttacksTimer<=0){
                Attack();
            }
            delayBetweenAttacksTimer-=Time.deltaTime;
        }
    }

    void AdjustHealthBarLocation() {
        canvas.transform.rotation = Quaternion.Euler(0,0,-this.transform.rotation.z);
    }

    void Attack() {
        collidingWith.ForEach(collider => collider.OnHit(damage));
        delayBetweenAttacksTimer=delayBetweenAttacks;
    }

    void FixedUpdate() {
        if (target!=null) {
            Move();
        }
    }

    public virtual Transform FindTarget() {
        Transform target = null;
        if (goTarget == EnemyAI.PLAYER) {
            Player player = FindObjectOfType<Player>();
            return player.transform;
        }
        return target;
    }

    public void Move() {
        moveDirection = (target.position - transform.position).normalized;
        float angle = Mathf.Atan2(moveDirection.y,moveDirection.x)*Mathf.Rad2Deg;
        rb.rotation = angle;
        rb.MovePosition(transform.position + moveDirection * moveSpeed * Time.fixedDeltaTime);
    }

    public void TakeDamage(float damageAmount) {
        healthBar.SetActive(true);
        hp-=damageAmount;
        healthBarFill.value = hp/maxHp;
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

    public void OnCollisionEnter2D(Collision2D collision) {
        EnemyDamagable go = collision.collider.gameObject.GetComponent<EnemyDamagable>();
        if (go == null) return;
        collidingWith.Add(go);
        if (delayBetweenAttacksTimer<=0) delayBetweenAttacksTimer=delayBetweenAttacks;
    }

    public void OnCollisionExit2D(Collision2D collision) {
        EnemyDamagable go = collision.collider.gameObject.GetComponent<EnemyDamagable>();
        if (go == null) return;
        collidingWith.Remove(go);
        if (collidingWith.Count==0) delayBetweenAttacksTimer = delayBetweenAttacks;
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