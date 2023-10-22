using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class KillCell : MonoBehaviour, IDamage
{
    [SerializeField] float _damage;
    [SerializeField] private float knockBack;
    private Vector3 targetDir;

    private Rigidbody2D rb;
    float _airTimeTimer;
    public float damage => _damage;


    private void Update()
    {
        transform.eulerAngles = Vector3.zero;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        IHittable hitComponent = collider.gameObject.GetComponent<IHittable>();
        if (hitComponent == null || collider.isTrigger) return;

        hitComponent.onHit(this);
        DamageValue.Instantiate(transform.position, (int)damage);
    }
}

