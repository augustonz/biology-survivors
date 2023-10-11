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
        UIManager.instance.SetPlayerExpBarLength(playerStatus.CurrExp, playerStatus.ExpCap);
    }

    private void LevelUp()
    {
        UIManager.instance.SetPlayerLevelText(playerStatus.CurrLevel);
    }

    public override void OnHit(int damage)
    {
        playerStatus.GetHit(damage);
        healthBarFill.value = playerStatus.CurrHP / playerStatus.MaxHP;
        flashAnimation();
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