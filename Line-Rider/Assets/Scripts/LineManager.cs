using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineManager : MonoBehaviour
{
    [SerializeField] float _lineSeparationDistance = 0.2f;
    [SerializeField] float _lineWidth = 0.08f;
    [SerializeField] Color _lineColor = Color.black;
    [SerializeField] int _lineCapVertices = 5;
    [SerializeField] float _effoctorSpeed = 10f;
    [SerializeField] PhysicsMaterial2D _physicsMaterial2D;
    [SerializeField] Player _player;

    Pan _panning;
    Zoom _zoom;
    Camera _mainCamera;
    InputManager _inputManager;

    List<GameObject> _lines;
    List<Vector2> _currentLine;
    LineRenderer _currentLineRenderer;
    EdgeCollider2D _currentLineEdgeCollider;
    GameObject _currentLineObject;

    bool _drawing = false;
    bool _erasing = false;

    void Awake()
    {
        _mainCamera = Camera.main;
        _inputManager = InputManager.Instance;
        _panning = GetComponent<Pan>();
        _zoom = GetComponent<Zoom>();

    }

    void OnEnable()
    {
        _inputManager.OnStartDraw += OnStartDraw;
        _inputManager.OnEndDraw += OnEndDraw;
        _inputManager.OnStartErase += OnStartErase;
        _inputManager.OnEndErase += OnEndErase;
    }

    void OnDisable()
    {
        _inputManager.OnStartDraw -= OnStartDraw;
        _inputManager.OnEndDraw -= OnEndDraw;
        _inputManager.OnStartErase -= OnStartErase;
        _inputManager.OnEndErase -= OnEndErase;
    }

    void Update()
    {
        if (!_player.playing)
        {
            _panning.PanScreen(GetCurrentScreenPoint());
            _zoom.ZoomScreen(GetZoomValue());
        }
    }

    #region Drawing

    void OnStartDraw()
    {
        if (!_erasing)
            StartCoroutine(Drawing());
    }

    void OnEndDraw()
    {
        _drawing = false;
    }

    IEnumerator Drawing()
    {
        _drawing = true;
        StartLine();
        while (_drawing)
        {
            AddPoint(GetCurrentWorldPoint());
            yield return null;
        }
        EndLine();
    }

    void StartLine()
    {
        // Instante the new line
        _currentLine = new List<Vector2>();
        _currentLineObject = new GameObject();
        _currentLineObject.name = "Line";
        _currentLineObject.transform.parent = transform;
        _currentLineRenderer = _currentLineObject.AddComponent<LineRenderer>();
        _currentLineEdgeCollider = _currentLineObject.AddComponent<EdgeCollider2D>();
        SurfaceEffector2D currentEffector = _currentLineObject.AddComponent<SurfaceEffector2D>();

        // Set settings
        _currentLineRenderer.positionCount = 0;
        _currentLineRenderer.startWidth = _lineWidth;
        _currentLineRenderer.endWidth = _lineWidth;
        _currentLineRenderer.numCapVertices = _lineCapVertices;
        _currentLineRenderer.material = new Material(Shader.Find("Particles/Standard Unlit"));
        _currentLineRenderer.startColor = _lineColor;
        _currentLineRenderer.endColor = _lineColor;
        _currentLineEdgeCollider.edgeRadius = 0.1f;
        _currentLineEdgeCollider.sharedMaterial = _physicsMaterial2D;
        _currentLineEdgeCollider.usedByEffector = true;
        currentEffector.speed = _effoctorSpeed;

        _currentLineObject.layer = 1 << 3;
    }

    void AddPoint(Vector2 point)
    {
        if (PlacePoint(point))
        {
            _currentLine.Add(point);
            _currentLineRenderer.positionCount++;
            _currentLineRenderer.SetPosition(_currentLineRenderer.positionCount - 1, point);
        }
    }

    bool PlacePoint(Vector2 point)
    {
        if (_currentLine.Count == 0)
            return true;

        if (Vector2.Distance(point, _currentLine[_currentLine.Count - 1]) < _lineSeparationDistance)
            return false;

        return true;
    }

    void EndLine()
    {
        if (_currentLine.Count == 1)
            DestroyLine(_currentLineObject);
        else
            _currentLineEdgeCollider.SetPoints(_currentLine);
    }

    #endregion

    void OnStartErase()
    {
        if (!_drawing)
            StartCoroutine(Erasing());
    }

    void OnEndErase()
    {
        _erasing = false;
    }

    IEnumerator Erasing()
    {
        _erasing = true;
        while (_erasing)
        {
            Vector2 screenMousePosition = GetCurrentScreenPoint();
            GameObject gameObject = Utils.Raycast(_mainCamera, screenMousePosition, 1<<8);
            if (gameObject != null)
                DestroyLine(gameObject);
            yield return null;
        }
    }

    void DestroyLine(GameObject gameObject)
    {
        Destroy(gameObject);
    }


    Vector2 GetCurrentScreenPoint()
    {
        return _inputManager.GetMousePosition();
    }

    Vector2 GetCurrentWorldPoint()
    {
        return _mainCamera.ScreenToWorldPoint(_inputManager.GetMousePosition());
    }

    float GetZoomValue()
    {
        return _inputManager.GetZoom();
    }
}
