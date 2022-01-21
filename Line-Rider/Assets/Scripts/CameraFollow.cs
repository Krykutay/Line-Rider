using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform _target;
    [SerializeField] Vector3 _offset = new Vector3(0f, 0f, -1.5f);
    [SerializeField] [Range(0.01f, 1f)] float _smoothSpeed = 0.125f;

    Vector3 _velocity = Vector3.zero;

    void LateUpdate()
    {
        Vector3 desiredPosition = _target.position + _offset;
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref _velocity, _smoothSpeed);
    }

    public void CenterOnTarget()
    {
        transform.position = _target.position + _offset;
    }

}
