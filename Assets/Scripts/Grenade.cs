using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
public class Grenade : MonoBehaviour
{
    private float damage;
    private float size;
    private float knockBack = 0;
    public float KnockBack { get => knockBack; }
    private Vector3 targetDir;
    private Vector2 endPosition;
    private Vector2 origin;

    private Vector2 vel;



    private Rigidbody2D rb;
    float _timer;

    public static void Create(Vector3 origin, Vector3 direction, float timer, float size = 1, float speed = 1, float damage = 10)
    {
        GameObject prefab = PrefabLoader.instance.getGrenade();
        GameObject grenadeObject = Instantiate(prefab, origin, new Quaternion());
        Grenade grenadeScript = grenadeObject.GetComponent<Grenade>();

        grenadeScript.size = size;
        grenadeScript.damage = damage;
        grenadeScript.targetDir = direction * speed;
        grenadeScript._timer = timer;
        grenadeScript.endPosition = origin + (direction.normalized);
        grenadeScript.origin = origin;
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public float getDamage()
    {
        return damage;
    }

    void Update()
    {
        _timer -= Time.deltaTime;
        if (_timer < 0)
        {
            Die();
        }
    }

    void FixedUpdate()
    {
        if (CheckStop(rb.position, endPosition))
        {
            rb.velocity = targetDir * Time.fixedDeltaTime * 50;
            transform.eulerAngles = new Vector3(0, 0, -300 * Time.time);
        }
        else
        {
            rb.velocity = Vector2.SmoothDamp(rb.velocity, Vector2.zero, ref vel, Random.Range(0, 0.5f));
        }
    }

    private bool CheckStop(Vector2 pos1, Vector2 pos2)
    {
        return Vector2.Distance(pos1, origin) < Vector2.Distance(pos2, origin);
    }

    void Die()
    {
        ExplosionEfetivation grenade = Instantiate(PrefabLoader.instance.getGrenadeExplosion(), transform.position, PrefabLoader.instance.getGrenadeExplosion().transform.rotation);
        grenade.ApplyDamage(size, damage);

        grenade.gameObject.transform.localScale *= size;
        Destroy(gameObject);
    }


    void OnTriggerEnter2D(Collider2D collider)
    {
        IHittable hitComponent = collider.gameObject.GetComponent<IHittable>();
        if (hitComponent == null || collider.isTrigger) return;

        hitComponent.onHit(PrefabLoader.instance.getGrenadeExplosion());
        DamageValue.Instantiate(transform.position, (int)damage);
    }


    void DestroyGrenade()
    {
        Instantiate(PrefabLoader.instance.getBulletHit(), transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}

