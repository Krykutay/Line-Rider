using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    #region Events
    public delegate void StartDraw();
    public event StartDraw OnStartDraw;
    public delegate void EndDraw();
    public event EndDraw OnEndDraw;
    public delegate void StartErase();
    public event StartErase OnStartErase;
    public delegate void EndErase();
    public event EndErase OnEndErase;
    public delegate void PressPlay();
    public event PressPlay OnPressPlay;
    #endregion

    MouseControls _mouseControls;
    PlayerControls _playerControls;

    void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;

        _mouseControls = new MouseControls();
        _playerControls = new PlayerControls();
    }

    void OnEnable()
    {
        _mouseControls.Enable();
        _playerControls.Enable();
    }

    void OnDisable()
    {
        _mouseControls.Disable();
        _playerControls.Disable();
    }

    void Start()
    {
        _mouseControls.Mouse.Click.started += _ => { if (OnStartDraw != null) OnStartDraw(); };
        _mouseControls.Mouse.Click.canceled += _ => { if (OnEndDraw != null) OnEndDraw(); };
        _mouseControls.Mouse.Erase.started += _ => { if (OnStartErase != null) OnStartErase(); };
        _mouseControls.Mouse.Erase.canceled += _ => { if (OnEndErase != null) OnEndErase(); };

        _playerControls.Player.Space.performed += _ => { if (OnPressPlay != null) OnPressPlay(); };

        //Cursor.lockState = CursorLockMode.Confined;
    }

    public float GetZoom()
    {
        return _mouseControls.Mouse.Zoom.ReadValue<float>();
    }

    public Vector2 GetMousePosition()
    {
        return _mouseControls.Mouse.Position.ReadValue<Vector2>();
    }
}
