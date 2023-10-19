using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{

    public static CameraShake instance;

    [SerializeField] static CinemachineVirtualCamera cineCamera;
    [SerializeField] static CinemachineBasicMultiChannelPerlin cinemachinePerlin;

    private static float shakeDuration = 0f;

    private static float shakeAmount = 0.7f;

    private float vel;
    private Vector3 vel2 = Vector3.zero;

    Vector3 originalPos;

    void Awake()
    {

        if (instance != null && instance != this) Destroy(this);
        else instance = this;

        if (cineCamera == null)
        {

            cineCamera = GetComponent<CinemachineVirtualCamera>();
        }

    }

    void Start()
    {
        cinemachinePerlin = cineCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

    }

    public static void ShakeOnce(float lenght, float strength)
    {
        cinemachinePerlin.m_AmplitudeGain = strength;
        shakeDuration = lenght;
    }

    void Update()
    {

        if (shakeDuration > 0)
        {
            shakeDuration -= Time.deltaTime;
            if (shakeDuration <= 0)
            {
                cinemachinePerlin.m_AmplitudeGain = 0f;
            }

            cinemachinePerlin.m_AmplitudeGain = Mathf.SmoothDamp(cinemachinePerlin.m_AmplitudeGain, 0, ref vel, 0.7f);

        }

    }
}