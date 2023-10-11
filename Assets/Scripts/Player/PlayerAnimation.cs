using UnityEngine;
using UnityEngine.Animations;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] GameObject _pupil;
    Vector3 _pupilOriginalPos;
    [SerializeField] Transform _gunRotationPoint;
    [SerializeField] float _pupilDeadZonePerc;
    Animator _playerAnim;
    SpriteRenderer _playerSprite;
    SpriteRenderer _gunSprite;
    Animator _gunAnim;
    Player _player;
    const float ONE_PIXEL_DISTANCE = 0.01f;
    void Start() {
        _playerAnim = GetComponent<Animator>();
        _gunAnim = GetComponentInChildren<Animator>();
        _player = GetComponent<Player>();
        _playerSprite = GetComponent<SpriteRenderer>();
        _gunSprite = _gunRotationPoint.GetComponentInChildren<SpriteRenderer>();

        _pupilOriginalPos = _pupil.transform.localPosition;
    }

    void Update() {
        UpdatePlayerBody();
        UpdatePlayerPupil();
        UpdatePlayerGunDirection();
    }

    void UpdatePlayerGunDirection()
    {
        Vector2 mousePos = (Vector2)Input.mousePosition - new Vector2(Screen.width/2,Screen.height/2);
        Vector2 rotation = mousePos - (Vector2)transform.position;

        float angle = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        
        _gunSprite.flipY = rotation.x<0;
        
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
        Vector2 rotation = mousePos - (Vector2)transform.position;

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