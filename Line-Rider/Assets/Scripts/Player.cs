using UnityEngine;

public class Player : MonoBehaviour
{
    CameraFollow _cameraFollow;
    InputManager _inputManager;
    Rigidbody2D _rb;

    Vector3 _startingPosition;
    Quaternion _startingRotation;

    public bool playing { get; private set; }

    void Awake()
    {
        _cameraFollow = Camera.main.GetComponent<CameraFollow>();
        _rb = GetComponent<Rigidbody2D>();

        _startingPosition = transform.position;
        _startingRotation = transform.rotation;

        _inputManager = InputManager.Instance;

        playing = false;
    }

    void OnEnable()
    {
        _inputManager.OnPressPlay += StartGame;
    }

    void OnDisable()
    {
        _inputManager.OnPressPlay -= StartGame;
    }

    void StartGame()
    {
        playing = !playing;
        if (playing)
        {
            _rb.bodyType = RigidbodyType2D.Dynamic;
            _rb.interpolation = RigidbodyInterpolation2D.Interpolate;
            _cameraFollow.enabled = true;
        }
        else
        {
            _rb.bodyType = RigidbodyType2D.Static;
            transform.position = _startingPosition;
            transform.rotation = _startingRotation;
            _cameraFollow.CenterOnTarget();
            _cameraFollow.enabled = false;
        }
    }

}
