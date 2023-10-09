using UnityEngine;
public abstract class EnemyDamagable : MonoBehaviour
{
    public Material flashMaterial;
    public float duration = 0.1f;
    private Material originalMaterial;
    private SpriteRenderer sr;

    public virtual void Awake() {
        sr = GetComponent<SpriteRenderer>();
        originalMaterial = sr.material;
        flashMaterial = new Material(flashMaterial);
    }
    public abstract void OnHit(int damage);

    public void flashAnimation() {
        sr.material = flashMaterial;
        flashMaterial.color = Color.red;
        Invoke("resetSpriteMaterial",duration);
    }

    public void resetSpriteMaterial() {
        sr.material = originalMaterial;
    }
}