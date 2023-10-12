using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Bullet : MonoBehaviour
{
    private float airTime;
    private int bulletPenetration;
    private float damage;
    private Vector3 targetDir;

    private Rigidbody2D rb;
    float _airTimeTimer;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public static void Create(Vector3 origin, Vector3 direction, float airTime = 1, float speed = 1, float damage = 10,int penetration = 0)
    {
        GameObject prefab = PrefabLoader.instance.getBullet();
        GameObject bulletObject = Instantiate(prefab, origin, new Quaternion());
        Bullet bulletScript = bulletObject.GetComponent<Bullet>();

        bulletScript.damage = damage;
        bulletScript.airTime = airTime;
        bulletScript.targetDir = direction * speed;
        bulletScript.bulletPenetration = penetration;
    }

    public float getDamage()
    {
        return damage;
    }

    void Update()
    {
        _airTimeTimer+=Time.deltaTime;
        if (_airTimeTimer > airTime) DestroyBullet();
    }

    void FixedUpdate()
    {
        rb.velocity = targetDir * Time.fixedDeltaTime * 50;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        IHittable hitComponent = collider.gameObject.GetComponent<IHittable>();
        if (hitComponent == null || collider.isTrigger) return;
        
        hitComponent.onHit(this);
        DamageValue.Instantiate(transform.position,(int)damage);

        bulletPenetration-=1;

        if (bulletPenetration<0) DestroyBullet();

    }

    void DestroyBullet() {
        Instantiate(PrefabLoader.instance.getBulletHit(),transform.position,Quaternion.identity);
        Destroy(gameObject);
    }
}

