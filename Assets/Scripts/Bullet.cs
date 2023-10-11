using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Bullet : MonoBehaviour
{
    private float speed;
    private Vector3 direction;
    private float distanceTraveled = 0;
    private float maxRange;

    private float damage;

    private Vector3 targetDir;
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public static void Create(Vector3 origin, Vector3 direction, float maxRange = 10, float speed = 1, float damage = 10)
    {
        GameObject prefab = PrefabLoader.instance.getBullet();
        GameObject bulletObject = Instantiate(prefab, origin, new Quaternion());
        Bullet bulletScript = bulletObject.GetComponent<Bullet>();

        bulletScript.direction = direction;
        bulletScript.speed = speed;
        bulletScript.damage = damage;
        bulletScript.maxRange = maxRange;
        bulletScript.targetDir = direction * speed;
    }

    public float getDamage()
    {
        return damage;
    }

    void Update()
    {
        distanceTraveled += (targetDir * Time.deltaTime).magnitude;
        if (distanceTraveled > maxRange) Destroy(gameObject);
    }

    void FixedUpdate()
    {
        rb.velocity = targetDir * Time.fixedDeltaTime * 50;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        IHittable hitComponent = collider.gameObject.GetComponent<IHittable>();
        if (hitComponent == null) return;
        hitComponent.onHit(this);
        DamageValue.Instantiate(transform.position,(int)damage);
        Destroy(gameObject);
    }
}

