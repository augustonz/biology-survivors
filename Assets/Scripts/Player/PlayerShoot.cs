using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] Transform _instantiateBulletPosition;


    bool canShoot = true;
    float currentAmmo;

    bool isReloading = false;
    bool isShootClicked = false;

    private InputManager input;
    [SerializeField] UnityEvent _onShoot;
    [SerializeField] UnityEvent _onStartReload;
    [SerializeField] UnityEvent _onEndReload;
    Player _player;

    void Start()
    {
        _player = GetComponent<Player>();
        input = InputManager.instance;
        float maxAmmo = _player.PlayerStatus.GetStat(TypeStats.NUMBER_OF_SHOTS);
        currentAmmo = maxAmmo;
        UIManager.instance.setAmmoText(maxAmmo, currentAmmo, false);
    }

    // Update is called once per frame
    void Update()
    {
        if (input.GetReloadInput()) StartReload();
        handlePlayerShoot();
    }

    void canShootAgain() { canShoot = true; }

    bool hasNoAmmo() { return currentAmmo <= 0; }

    void handlePlayerShoot()
    {
        if (input.GetClickInput())
        {
            isShootClicked = true;
            if (hasNoAmmo())
            {
                StartReload();
                return;
            }

        }
        if (input.clickAction.WasReleasedThisFrame()) isShootClicked = false;

        if (input.clickAction.IsPressed() && !hasNoAmmo() && !isReloading && canShoot)
        {
            shootBullet();
        }
    }

    void shootBullet()
    {
        _onShoot.Invoke();

        canShoot = false;
        
        float shootDelay = 1/_player.PlayerStatus.GetStat(TypeStats.FIRE_RATE);

        Invoke("canShootAgain", shootDelay);
        currentAmmo -= 1;

        float maxAmmo = _player.PlayerStatus.GetStat(TypeStats.NUMBER_OF_SHOTS);
        UIManager.instance.setAmmoText(maxAmmo, currentAmmo, false);
        
        Vector3 gunPosition = _instantiateBulletPosition.position;

        Vector3 bulletDirection = ((Vector2)gunPosition - (Vector2)transform.position).normalized;

        float bulletAirTime = _player.PlayerStatus.GetStat(TypeStats.BULLET_RANGE);
        float bulletSpeed = _player.PlayerStatus.GetStat(TypeStats.BULLET_SPEED);
        float bulletDamage = _player.PlayerStatus.GetStat(TypeStats.POWER);
        int bulletPenetration = (int) _player.PlayerStatus.GetStat(TypeStats.PENETRATION);


        Bullet.Create(gunPosition, bulletDirection, bulletAirTime, bulletSpeed, bulletDamage,bulletPenetration);
    }

    public void StartReload()
    {
        if (!IsInvoking("CompleteReload"))
        {
            _onStartReload.Invoke();
            isReloading = true;
            float reloadTime = 1/_player.PlayerStatus.GetStat(TypeStats.RELOAD_SPEED);
            Invoke("CompleteReload", reloadTime);
            UIManager.instance.setAmmoText(0, 0, true);
        }
    }

    void CompleteReload()
    {
        isReloading = false;
        _onEndReload.Invoke();

        float maxAmmo = _player.PlayerStatus.GetStat(TypeStats.NUMBER_OF_SHOTS);
        currentAmmo = maxAmmo;
        UIManager.instance.setAmmoText(maxAmmo, currentAmmo, false);
    }
}
