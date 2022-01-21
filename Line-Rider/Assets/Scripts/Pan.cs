using UnityEngine;

public class Pan : MonoBehaviour
{
    [SerializeField] float _panSpeed = 2f;

    Transform _mainCamera;

    void Awake()
    {
        _mainCamera = Camera.main.transform;
    }

    public void PanScreen(Vector2 mouseScreenPosition)
    {
        Vector2 direction = PanDirection(mouseScreenPosition);
        _mainCamera.position = Vector3.Lerp(_mainCamera.position, (Vector3)direction + _mainCamera.position, Time.deltaTime * _panSpeed);
    }

    Vector2 PanDirection(Vector2 mouseScreenPosition)
    {
        Vector2 direction = Vector2.zero;

        if (mouseScreenPosition.y >= Screen.height * 0.95f)
        {
            direction.y += 1;
        }
        else if (mouseScreenPosition.y <= Screen.height * 0.05f)
        {
            direction.y -= 1;
        }

        if (mouseScreenPosition.x >= Screen.width * 0.95f)
        {
            direction.x += 1;
        }
        else if (mouseScreenPosition.x <= Screen.width * 0.05f)
        {
            direction.x -= 1;
        }

        return direction;
    }
}
