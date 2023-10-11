using UnityEngine;
using UnityEngine.UI;

public class Player : EnemyDamagable
{

    private PlayerMovement playerMovement;
    private PlayerStatus playerStatus;
    private PlayerShoot playerShoot;


    [SerializeField] Slider healthBarFill;


    public override void Awake()
    {
        base.Awake();

        playerMovement = GetComponent<PlayerMovement>();
        playerStatus = GetComponent<PlayerStatus>();
        playerShoot = GetComponent<PlayerShoot>();

    }

    public void Start()
    {
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
        playerStatus.AddExp(expValue);
        OnChangeExp();
    }

    public void OnChangeExp()
    {
        UIManager.instance.SetPlayerExpBarLength(playerStatus.currExp, playerStatus.expCap);
    }

    public void LevelUp()
    {
        UIManager.instance.SetPlayerLevelText(playerStatus.currLevel);
    }

    public override void OnHit(int damage)
    {
        playerStatus.GetHit(damage);
        OnChangeCurrHealth();
        flashAnimation();
    }

    public void OnChangeCurrHealth()
    {
        healthBarFill.value = playerStatus.currHP / playerStatus.GetStat(TypeStats.MAX_HP);
    }

    public void OnEnable()
    {
        playerMovement.Enable();
    }

    public void OnDisable()
    {
        playerMovement.Disable();
    }
}