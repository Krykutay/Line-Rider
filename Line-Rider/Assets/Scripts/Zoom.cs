using UnityEngine;

public class Zoom : MonoBehaviour
{
    [SerializeField] float _zoomSpeed = 2f;
    [SerializeField] float _minZoom = 1;
    [SerializeField] float _maxZoom = 15;

    Camera _mainCamera;

    float _startingZPosition;

    void Awake()
    {
        _mainCamera = Camera.main;

        _startingZPosition = _mainCamera.transform.position.z;
    }

    public void ZoomScreen(float increment)
    {
        if (increment == 0)
            return;

        float target = Mathf.Clamp(_mainCamera.orthographicSize + increment, _minZoom, _maxZoom);
        _mainCamera.orthographicSize = Mathf.Lerp(_mainCamera.orthographicSize, target, Time.deltaTime * _zoomSpeed);
    }

}
