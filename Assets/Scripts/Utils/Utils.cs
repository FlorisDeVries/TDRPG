using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public static class Utils
{
    public static Vector3 RandomPointInBounds(Bounds bounds)
    {
        return new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y),
            Random.Range(bounds.min.z, bounds.max.z)
        );
    }

    public static Vector3 RandomPointInBoundsOnZeroPlane(Bounds bounds)
    {
        return new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            0,
            Random.Range(bounds.min.z, bounds.max.z)
        );
    }

    /// <summary>
    /// Helper function that returns the worldposition on an y-plane as "seen" from an input position with the main camera
    /// </summary>
    /// <param name="touchPos">The input position</param>
    /// <param name="yPos">The y-plane to project the touch on</param>
    /// <returns>The 3D world coordinates</returns>
    public static Vector3 GetPlaneIntersection(float yPos = 0)
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        float delta = ray.origin.y - yPos;
        Vector3 dirNorm = ray.direction / ray.direction.y;
        return ray.origin - dirNorm * delta;
    }

    /// <summary>
    /// Helper function that returns the in world click position, or if there is no intersection the point clicked on the plane at the given ypos
    /// </summary>
    /// <param name="ypos">What plane the function should default to if nu intersection is found</param>
    /// <returns></returns>
    public static Vector3 GetInWorldClickPosition(float ypos = 0)
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1000000))
        {
            return hit.point;
        }
        else
        {
            return (GetPlaneIntersection(0));
        }
    }
}
