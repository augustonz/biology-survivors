using UnityEngine;
public abstract class EnemyDamagable : MonoBehaviour
{
    public abstract void OnHit(int damage, IHittable target);


}