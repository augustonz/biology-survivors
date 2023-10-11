using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        canShoot = false;
        Invoke("canShootAgain", shootDelay);
        currentAmmo -= 1;
        UIManager.instance.setAmmoText(maxAmmo, currentAmmo, false);
        Vector3 gunPosition = transform.GetChild(0).GetChild(0).position;
        Vector3 destination = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 bulletDirection = new Vector3(destination.x - gunPosition.x, destination.y - gunPosition.y, 0).normalized;
        Bullet.Create(gunPosition, bulletDirection, bulletMaxRange, bulletSpeed, bulletDamage);
    }

    void StartReload()
    {
        if (currentAmmo < maxAmmo && !IsInvoking("CompleteReload"))
        {
            isReloading = true;
            Invoke("CompleteReload", reloadTime);
            UIManager.instance.setAmmoText(0, 0, true);
        }
    }

    void CompleteReload()
    {
        isReloading = false;
        currentAmmo = maxAmmo;
        UIManager.instance.setAmmoText(maxAmmo, currentAmmo, false);
    }
}
