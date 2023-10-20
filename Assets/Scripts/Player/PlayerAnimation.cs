using UnityEngine;
using System.Threading.Tasks;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] GameObject _pupil;
    [SerializeField] Transform _gunRotationPoint;
    [SerializeField] float _pupilDeadZonePerc;
    [SerializeField] int _getHitFlashDurationMillis;
    [SerializeField] Material _getHitMaterial;
    [SerializeField] Material _invincibleMaterial;
    [SerializeField] int _invincibilityBlinkInterval;
    Animator _playerAnim;
    Vector3 _pupilOriginalPos;
    SpriteRenderer _playerSprite;
    SpriteRenderer _gunSprite;
    Animator _gunAnim;
    Player _player;
    Material _defaultMaterial;
    SpriteRenderer[] _allSprites;
    const float ONE_PIXEL_DISTANCE = 0.01f;
    void Start() {
        _playerAnim = GetComponent<Animator>();
        _gunAnim = transform.Find("GunRotationAxis").GetComponentInChildren<Animator>();
        _player = GetComponent<Player>();
        _playerSprite = GetComponent<SpriteRenderer>();
        _gunSprite = _gunRotationPoint.GetComponentInChildren<SpriteRenderer>();
        _allSprites = GetComponentsInChildren<SpriteRenderer>();
        _pupilOriginalPos = _pupil.transform.localPosition;
        _defaultMaterial = _playerSprite.material;
    }

    void Update() {
        UpdatePlayerBody();
        UpdatePlayerPupil();
        UpdatePlayerGunDirection();
    }

    public async void FlashRed() {
        UpdateAllSprites(_getHitMaterial);

        await Task.Delay(_getHitFlashDurationMillis);

        UpdateAllSprites(_defaultMaterial);

        int invincibilityDuration = _player.PlayerStatus.InvincibilityTimeMillis;

        FlashInvincibility(invincibilityDuration-_getHitFlashDurationMillis,_invincibilityBlinkInterval);
    }

    void UpdateAllSprites(Material mat) {
        foreach (SpriteRenderer sr in _allSprites)
        {   
            sr.material = mat;
        }
    }

    async void FlashInvincibility(int invicibilityDuration,int invicibilityBlinkInterval) {
        bool isInvincible = true;

        Task.Run(async ()=>{
            await Task.Delay(invicibilityDuration);
            isInvincible = false;
        });

        while (isInvincible) {
            UpdateAllSprites(_invincibleMaterial);
            await Task.Delay(invicibilityBlinkInterval);
            UpdateAllSprites(_defaultMaterial);
            await Task.Delay(invicibilityBlinkInterval);
        }
    }

    public void AnimateGunShooting() {
        _gunAnim.SetTrigger("shoot");
    }

    public void AnimateStartGunReloading() {
        _gunAnim.SetBool("reloading",true);
    }

    public void AnimateEndGunReloading() {
        _gunAnim.SetBool("reloading",false);
    }

    public void AnimatePlayerDeath() {
        _playerAnim.SetTrigger("die");
    }

    void UpdatePlayerGunDirection()
    {
        Vector2 mousePos = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2)_gunRotationPoint.position;

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        
        _gunSprite.flipY = mousePos.x<0;
        
        _gunRotationPoint.eulerAngles = new Vector3(0, 0, angle);
    }

    void UpdatePlayerBody() {
        _playerSprite.flipX = _player.PlayerMovement.x<0;
        if (_player.PlayerMovement.x==0) {
            _playerAnim.SetBool("run",false);
        } else {
            _playerAnim.SetBool("run",true);
        }
    }

    void UpdatePlayerPupil() {
        Vector2 mousePos = (Vector2)Input.mousePosition - new Vector2(Screen.width/2,Screen.height/2);

        Vector3 pupilPosition = _pupilOriginalPos;

        if (mousePos.y < -Screen.height * _pupilDeadZonePerc) {
            pupilPosition = pupilPosition + (Vector3) Vector2.down * ONE_PIXEL_DISTANCE;
        } else if (mousePos.y > Screen.height * _pupilDeadZonePerc) {
            pupilPosition = pupilPosition + (Vector3) Vector2.up * ONE_PIXEL_DISTANCE;
        }

        if (mousePos.x < -Screen.width * _pupilDeadZonePerc) {
            pupilPosition = pupilPosition + (Vector3) Vector2.left * ONE_PIXEL_DISTANCE * 2;
        } else if (mousePos.x > Screen.width * _pupilDeadZonePerc) {
            pupilPosition = pupilPosition + (Vector3) Vector2.right * ONE_PIXEL_DISTANCE;
        }

        _pupil.transform.localPosition = pupilPosition;
    }
}