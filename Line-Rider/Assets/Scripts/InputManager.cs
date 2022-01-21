using UnityEngine;

public class InputManager : MonoBehaviour
{
    #region Events
    public delegate void StartDraw();
    public static event StartDraw OnStartDraw;
    public delegate void EndDraw();
    public static event EndDraw OnEndDraw;
    public delegate void StartErase();
    public static event StartErase OnStartErase;
    public delegate void EndErase();
    public static event EndErase OnEndErase;
    #endregion

    MouseControls _mouseControls;

    void Awake()
    {
        _mouseControls = new MouseControls();
    }

    void OnEnable()
    {
        _mouseControls.Enable();
    }

    void OnDisable()
    {
        _mouseControls.Disable();
    }

    void Start()
    {
        _mouseControls.Mouse.Click.started += _ => { if (OnStartDraw != null) OnStartDraw(); };
        _mouseControls.Mouse.Click.canceled += _ => { if (OnEndDraw != null) OnEndDraw(); };
        _mouseControls.Mouse.Erase.started += _ => { if (OnStartErase != null) OnStartErase(); };
        _mouseControls.Mouse.Erase.canceled += _ => { if (OnEndErase != null) OnEndErase(); };

        Cursor.lockState = CursorLockMode.Confined;
    }

    public float GetZoom()
    {
        return _mouseControls.Mouse.Zoom.ReadValue<float>();
    }

    public Vector2 GetMousePosition()
    {
        return _mouseControls.Mouse.Position.ReadValue<Vector2>();
    }

    void Update()
    {
        
    }
}