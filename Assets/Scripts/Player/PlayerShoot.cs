using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] Transform _instantiateBulletPosition;


    bool canShoot = true;
    float currentAmmo;

    bool canMissile = true;


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
        float maxAmmo = _player.PlayerStatus.GetStat(TypeStats.MAX_AMMO);
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
    void canMissileAgain() { canMissile = true; }

    bool hasNoAmmo() { return currentAmmo <= 0; }

    public void handlePlayerShoot()
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

        if (canMissile && _player.PlayerStatus.GetStat(TypeStats.MISSILE_COUNT) > 0)
        {
            ShootMissile();
        }
    }

    void shootBullet()
    {
        _onShoot.Invoke();

        canShoot = false;

        float shootDelay = 1 / _player.PlayerStatus.GetStat(TypeStats.FIRE_RATE);

        Invoke("canShootAgain", shootDelay);
        currentAmmo -= 1;

        float maxAmmo = _player.PlayerStatus.GetStat(TypeStats.MAX_AMMO);
        UIManager.instance.setAmmoText(maxAmmo, currentAmmo, false);

        Vector3 gunPosition = _instantiateBulletPosition.position;

        Vector3 bulletDirection = ((Vector2)gunPosition - (Vector2)transform.position).normalized;

        float bulletAirTime = _player.GetStat(TypeStats.BULLET_RANGE);
        float bulletSpeed = _player.GetStat(TypeStats.BULLET_SPEED);
        float bulletSpread = _player.GetStat(TypeStats.BULLET_SPREAD);
        float bulletDamage = _player.GetStat(TypeStats.POWER);
        float bulletSize = _player.GetStat(TypeStats.BULLET_SIZE);
        float bulletKnockBack = _player.GetStat(TypeStats.BULLET_KNOCKBACK);

        int bulletPenetration = (int)_player.GetStat(TypeStats.PENETRATION);
        int numberOfShots = (int)_player.GetStat(TypeStats.NUMBER_OF_SHOTS);
        float chanceExploding = _player.GetStat(TypeStats.EXPLOSION_CHANCE);

        for (int i = 0; i < numberOfShots; i++)
        {
            Vector3 newBulletDirection = BulletSpreadCalc(bulletDirection, bulletSpread, i, numberOfShots);
            bool explode = Random.Range(0f, 1f) > 1 - chanceExploding;
            Bullet.Create(gunPosition, newBulletDirection, bulletSize, bulletAirTime, bulletSpeed, bulletDamage, bulletPenetration, bulletKnockBack);
        }


    }

    Vector3 BulletSpreadCalc(Vector3 oldDirection, float bulletSpread, int shotNum, int maxShots)
    {
        float halfWay = (maxShots - 1) / 2f;
        float distanceFromHalfWay = shotNum - halfWay;
        float angle = distanceFromHalfWay * bulletSpread;

        float defaultAngle = Mathf.Atan2(oldDirection.y, oldDirection.x) * Mathf.Rad2Deg;
        float newAngle = (defaultAngle + angle) * Mathf.Deg2Rad;

        return new Vector3(Mathf.Cos(newAngle), Mathf.Sin(newAngle), 0);
    }



    void ShootMissile()
    {
        Vector3 gunPosition = _instantiateBulletPosition.position;

        float missileDamage = (float)(_player.PlayerStatus.GetStat(TypeStats.POWER) * 0.85);
        int missileCount = (int)(_player.PlayerStatus.GetStat(TypeStats.MISSILE_COUNT));


        canMissile = false;
        float missileDelay = _player.PlayerStatus.GetStat(TypeStats.MISSILE_COOLDOWN);
        Invoke("canMissileAgain", missileDelay);
        for (int i = 0; i < missileCount; i++)
        {
            BacteriophageMissile.Create(gunPosition, WaveManager.instance.GetEnemy(), missileDamage);
        }
    }

    public void StartReload()
    {
        if (!IsInvoking("CompleteReload"))
        {
            _onStartReload.Invoke();
            isReloading = true;
            float reloadTime = 1 / _player.PlayerStatus.GetStat(TypeStats.RELOAD_SPEED);
            Invoke("CompleteReload", reloadTime);
            UIManager.instance.setAmmoText(0, 0, true);
            if (_player.GetStat(TypeStats.GRENADE_COUNT) > 0 && currentAmmo == 0)
            {
                ShootGrenade();
            }
            if (_player.GetStat(TypeStats.SPEED_BURST) > 0 && currentAmmo == 0)
            {
                _player.SpeedBurst(3);
            }
        }
    }

    void ShootGrenade()
    {
        Vector3 gunPosition = _instantiateBulletPosition.position;

        float grenadeDamage = (float)(_player.PlayerStatus.GetStat(TypeStats.POWER) * 0.65);
        float grenadeArea = (int)_player.PlayerStatus.GetStat(TypeStats.GRENADE_AREA);
        int grenadeCount = (int)_player.PlayerStatus.GetStat(TypeStats.GRENADE_COUNT);


        Vector3 grenadeDirection = ((Vector2)gunPosition - (Vector2)transform.position).normalized;

        for (int i = 0; i < grenadeCount; i++)
        {
            Grenade.Create(gunPosition, grenadeDirection, 3, grenadeArea, 5, grenadeDamage);
        }
    }

    void CompleteReload()
    {
        isReloading = false;
        _onEndReload.Invoke();

        float maxAmmo = _player.PlayerStatus.GetStat(TypeStats.MAX_AMMO);
        currentAmmo = maxAmmo;
        UIManager.instance.setAmmoText(maxAmmo, currentAmmo, false);
    }
}
