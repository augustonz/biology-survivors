using UnityEngine;
using UnityEngine.UI;

public class Player : EnemyDamagable
{
    [SerializeField] Slider healthBarFill;

    PlayerAnimation _playerAnimation;
    PlayerMovement _playerMovement;
    PlayerShoot _playerShoot;
    PlayerStatus _playerStatus;


    public Vector3 PlayerMovement { get => _playerMovement.MoveDirection; }

    public override void Awake()
    {
        base.Awake();
        _playerAnimation = GetComponent<PlayerAnimation>();
        _playerMovement = GetComponent<PlayerMovement>();
        _playerShoot = GetComponent<PlayerShoot>();
        _playerStatus = GetComponent<PlayerStatus>();
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
        _playerStatus.AddExp(expValue);
        OnChangeExp();
    }

    public void OnChangeExp()
    {
        UIManager.instance.SetPlayerExpBarLength(_playerStatus.currExp, _playerStatus.expCap);
    }

    public void LevelUp()
    {
        UIManager.instance.SetPlayerLevelText(_playerStatus.currLevel);
    }

    public override void OnHit(int damage)
    {
        _playerStatus.GetHit(damage);
        OnChangeCurrHealth();
        flashAnimation();
    }

    public void OnChangeCurrHealth()
    {
        healthBarFill.value = _playerStatus.currHP / _playerStatus.GetStat(TypeStats.MAX_HP);
    }

    public void OnEnable()
    {
        _playerMovement.Enable();
    }

    public void OnDisable()
    {
        _playerMovement.Disable();
    }
}