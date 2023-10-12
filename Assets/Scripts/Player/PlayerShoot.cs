using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerShoot : MonoBehaviour
{
    public float bulletSpeed = 1f;
    public float bulletMaxRange = 10f;
    public float bulletDamage = 10f;

    public float shootDelay = 0.5f;

    private bool canShoot = true;

    public float maxAmmo = 6;
    public float currentAmmo = 6;
    public float reloadTime = 2f;

    private bool isReloading = false;
    private bool isShootClicked = false;

    [SerializeField] UnityEvent _onShoot;
    [SerializeField] UnityEvent _onStartReload;
    public UnityEvent OnStartReload { get=> _onStartReload; }
    [SerializeField] UnityEvent _onEndReload;
    [SerializeField] Transform _instantiateBulletPosition; 
    private InputManager input;
    // Start is called before the first frame update
    void Start()
    {
        UIManager.instance.setAmmoText(maxAmmo, currentAmmo, false);
        input = InputManager.instance;
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
        Invoke("canShootAgain", shootDelay);
        currentAmmo -= 1;

        UIManager.instance.setAmmoText(maxAmmo, currentAmmo, false);
        
        Vector3 gunPosition = _instantiateBulletPosition.position;

        Vector3 bulletDirection = ((Vector2)gunPosition - (Vector2)transform.position).normalized;

        Bullet.Create(gunPosition, bulletDirection, bulletMaxRange, bulletSpeed, bulletDamage);
    }

    public void StartReload()
    {
        if (currentAmmo < maxAmmo && !IsInvoking("CompleteReload"))
        {
            _onStartReload.Invoke();
            isReloading = true;
            Invoke("CompleteReload", reloadTime);
            UIManager.instance.setAmmoText(0, 0, true);
        }
    }

    void CompleteReload()
    {
        isReloading = false;
        _onEndReload.Invoke();
        currentAmmo = maxAmmo;
        UIManager.instance.setAmmoText(maxAmmo, currentAmmo, false);
    }
}
