using UnityEngine;

public static class Utils
{
    public static GameObject Raycast(Camera mainCamera, Vector2 screenPosition, int layermask)
    {
        Ray ray = mainCamera.ScreenPointToRay(screenPosition);
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity, layermask);
        if (hit.collider != null)
            return hit.collider.gameObject;
        else
            return null;
    }

}
