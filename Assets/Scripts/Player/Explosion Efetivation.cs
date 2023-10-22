using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ExplosionEfetivation : MonoBehaviour, IDamage
{
    [SerializeField] float _baseScale;
    float _damage;

    public float damage => _damage;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, 3);   
    }

    public void ApplyDamage(float scale, float damage)
    {
        this.transform.localScale = Vector3.one * _baseScale * scale;

        Collider2D[] hit = Physics2D.OverlapCircleAll(transform.position, scale * _baseScale);

        foreach (Collider2D hit2 in hit)
        {
            if (hit2.TryGetComponent(out IHittable value))
            {
                value.onHit(this);
            }
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _baseScale);
    }
}
