using UnityEngine;
using UnityEngine.UI;

public class Player : EnemyDamagable
{

    public float hp;
    public float maxHp;

    public int level = 1;
    public float expCap = 10;
    public float exp = 0;


    Slider healthBarFill;


    public override void Awake()
    {
        base.Awake();
    }

    public void Start()
    {
        healthBarFill = GetComponentInChildren<Slider>();
    }

    static Vector3 GetRandomPositionOnPlane()
    {
        return new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f), 1f);
    }

    void Update()
    {
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("PickUp"))
        {
            PickUp pickUp = collider.GetComponent<PickUp>();
            pickUp.onPick(this);
        }
    }

    public void GetExp(int expValue)
    {
        exp += expValue;
        if (exp >= expCap)
        {
            LevelUp();
        }
        UIManager.instance.SetPlayerExpBarLength(exp, expCap);
    }

    private void LevelUp()
    {
        level++;
        UIManager.instance.SetPlayerLevelText(level);
        exp -= expCap;
        expCap += 10;
    }

    public override void OnHit(int damage)
    {
        hp -= damage;
        healthBarFill.value = hp / maxHp;
        if (hp <= 0)
        {
            Die();
        }
        flashAnimation();
    }



    void Die()
    {
        Destroy(gameObject);
    }
}