using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputManager))]
public class LineManager : MonoBehaviour
{
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

    }

    void OnEndDraw()
    {

    }

    void OnStartErase()
    {

    }

    void OnEndErase()
    {

    }

}
