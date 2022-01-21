using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    InputManager _inputManager;
    Rigidbody2D _rb;

    Vector3 _startingPosition;

    bool _playing = false;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();

        _startingPosition = transform.position;

        _inputManager = InputManager.Instance;
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
        _playing = !_playing;
        if (_playing)
        {
            _rb.bodyType = RigidbodyType2D.Dynamic;
        }
        else
        {
            _rb.bodyType = RigidbodyType2D.Static;
            transform.position = _startingPosition;
        }
    }

    void Update()
    {
        
    }
}
