using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputManager))]
public class LineManager : MonoBehaviour
{
    [SerializeField] float _lineSeparationDistance = 0.2f;
    [SerializeField] float _lineWidth = 0.08f;
    [SerializeField] Color _lineColor = Color.black;
    [SerializeField] int _lineCapVertices = 5;

    InputManager _inputManager;

    List<GameObject> _lines;
    List<Vector2> _currentLine;
    LineRenderer _currentLineRenderer;
    EdgeCollider2D _currentLineEdgeCollider;

    bool _drawing = false;
    bool _erasing = false;

    Camera _mainCamera;

    void Awake()
    {
        _inputManager = GetComponent<InputManager>();
        _mainCamera = Camera.main;
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

    void OnStartDraw()
    {
        if (!_erasing)
            StartCoroutine(Drawing());
    }

    void OnEndDraw()
    {
        _drawing = false;
    }

    void OnStartErase()
    {

    }

    void OnEndErase()
    {

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
        GameObject currentLineObject = new GameObject();
        currentLineObject.name = "Line";
        currentLineObject.transform.parent = transform;
        _currentLineRenderer = currentLineObject.AddComponent<LineRenderer>();
        _currentLineEdgeCollider = currentLineObject.AddComponent<EdgeCollider2D>();

        // Set settings
        _currentLineRenderer.positionCount = 0;
        _currentLineRenderer.startWidth = _lineWidth;
        _currentLineRenderer.endWidth = _lineWidth;
        _currentLineRenderer.numCapVertices = _lineCapVertices;
        _currentLineRenderer.material = new Material(Shader.Find("Particles/Standard Unlit"));
        _currentLineRenderer.startColor = _lineColor;
        _currentLineRenderer.endColor = _lineColor;
        _currentLineEdgeCollider.edgeRadius = 0.1f;

    }

    Vector2 GetCurrentWorldPoint()
    {
        return _mainCamera.ScreenToWorldPoint(_inputManager.GetMousePosition());
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
        _currentLineEdgeCollider.SetPoints(_currentLine);
    }

}
