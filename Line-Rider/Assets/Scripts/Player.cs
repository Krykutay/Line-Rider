using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    PlayerControls _playerControls;
    Rigidbody2D _rb;

    Vector3 _startingPosition;

    bool _playing = false;

    void Awake()
    {
        _playerControls = new PlayerControls();
        _rb = GetComponent<Rigidbody2D>();

        _startingPosition = transform.position;
    }

    void OnEnable()
    {
        _playerControls.Enable();
    }

    void OnDisable()
    {
        _playerControls.Disable();
    }

    void Start()
    {
        _playerControls.Player.Space.performed += _ => StartGame();
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
