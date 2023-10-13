using UnityEngine;
using UnityEngine.UI;

public class Player : EnemyDamagable
{
    PlayerAnimation _playerAnimation;
    [SerializeField] PlayerPickUpRange _playerPickUpRange;
    [SerializeField] CharacterAudioManager _characterAudioManager;
    PlayerMovement _playerMovement;
    public Vector3 PlayerMovement { get => _playerMovement.MoveDirection; }
    PlayerShoot _playerShoot;
    PlayerStatus _playerStatus;
    public PlayerStatus PlayerStatus { get => _playerStatus; }

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
        _characterAudioManager.GatheredXP();
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
        UIManager.instance.SetPlayerHealth((int)_playerStatus.currHP,(int)_playerStatus.GetStat(TypeStats.MAX_HP));
        flashAnimation();
    }

    public void OnEnable()
    {
        _playerMovement.Enable();
    }

    public void OnDisable()
    {
        _playerMovement.Disable();
    }

    public void OnChangeState(GameState gameState)
    {
        if (gameState == GameState.INGAME)
        {
            _playerMovement.SetSpeed(_playerStatus.GetStat(TypeStats.SPEED));
            _playerPickUpRange.SetPickUpRange(_playerStatus.GetStat(TypeStats.PICK_UP_RANGE));
        }
    }
}