using UnityEngine;
using UnityEngine.UI;

public class Player : EnemyDamagable
{
    public float playerSpeed = 1f;
    public float bulletSpeed = 1f;
    public float bulletMaxRange = 10f;
    public float bulletDamage = 10f;

    public float hp;
    public float maxHp;

    public float shootDelay = 0.5f;

    private bool canShoot = true;

    public float maxAmmo = 6;
    public float currentAmmo = 6;
    public float reloadTime = 2f;

    private bool isReloading = false;
    private bool isShootClicked = false;

    public int level = 1;
    public float expCap = 10;
    public float exp = 0;
    Vector3 moveDirection;
    Rigidbody2D rb;
    [SerializeField] Slider healthBarFill;

    public override void Awake() {
        base.Awake();
    }

    public void Start() {
        UIManager.instance.setAmmoText(maxAmmo,currentAmmo,false);
        rb = GetComponent<Rigidbody2D>();
    }

    static Vector3 GetRandomPositionOnPlane() {
        return new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f), 1f);
    }

    void handlePlayerMovement() {
            moveDirection = new Vector3();
            if (Input.GetKey(KeyCode.W)) moveDirection += new Vector3(0,1,0);
            if (Input.GetKey(KeyCode.A)) moveDirection += new Vector3(-1,0,0);
            if (Input.GetKey(KeyCode.S)) moveDirection += new Vector3(0,-1,0);
            if (Input.GetKey(KeyCode.D)) moveDirection += new Vector3(1,0,0);
    }

    void handlePlayerGunDirection() {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); 
        Vector3 rotation = mousePos - transform.position;
        float angle = Mathf.Atan2(rotation.y,rotation.x) * Mathf.Rad2Deg;
        transform.GetChild(0).eulerAngles = new Vector3(0,0,angle);
    }

    void canShootAgain() {canShoot = true;}

    bool hasNoAmmo() {return currentAmmo<=0;}

    void handlePlayerShoot() {
        if (Input.GetMouseButtonDown(0)) {
            isShootClicked = true;
            if (hasNoAmmo()) {
                StartReload();
                return;
            }
            
        }
        if (Input.GetMouseButtonUp(0)) isShootClicked = false;

        if (isShootClicked && !hasNoAmmo() && !isReloading && canShoot) {
            shootBullet();
        }
    }

    void shootBullet() {
        canShoot = false;
        Invoke("canShootAgain",shootDelay);
        currentAmmo-=1;
        UIManager.instance.setAmmoText(maxAmmo,currentAmmo,false);
        Vector3 gunPosition = transform.GetChild(0).GetChild(0).position;
        Vector3 destination = Camera.main.ScreenToWorldPoint(Input.mousePosition); 
        Vector3 bulletDirection = new Vector3(destination.x-gunPosition.x,destination.y-gunPosition.y,0).normalized;
        Bullet.Create(gunPosition,bulletDirection,bulletMaxRange,bulletSpeed,bulletDamage);
    }

    void StartReload() {
        if (currentAmmo<maxAmmo && !IsInvoking("CompleteReload")) {
            isReloading=true;
            Invoke("CompleteReload",reloadTime);
            UIManager.instance.setAmmoText(0,0,true);
        }
    }

    void CompleteReload() {
        isReloading=false;
        currentAmmo=maxAmmo;
        UIManager.instance.setAmmoText(maxAmmo,currentAmmo,false);
    }

    void Update() {
        handlePlayerInput();
        handlePlayerMovement();
        handlePlayerGunDirection();
        handlePlayerShoot();
    }

    void FixedUpdate() {
        rb.MovePosition(transform.position + moveDirection.normalized * playerSpeed * Time.deltaTime);
    }

    void handlePlayerInput() {
        if (Input.GetKeyDown(KeyCode.R)) StartReload();

    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.CompareTag("PickUp")) {
            PickUp pickUp = collider.GetComponent<PickUp>();
            pickUp.onPick(this);
        }
    }

    public void GetExp(int expValue) {
        exp+=expValue;
        if (exp>=expCap) {
            LevelUp();
        }
        UIManager.instance.SetPlayerExpBarLength(exp,expCap);
    }

    private void LevelUp() {
        level++;
        UIManager.instance.SetPlayerLevelText(level);
        exp-=expCap;
        expCap+=10;
    }

    public override void OnHit(int damage) {
        hp-=damage;
        healthBarFill.value = hp/maxHp;
        if (hp<=0) {
            Die();
        }
        flashAnimation();
    }

    void Die() {
        Destroy(gameObject);
    }
}