using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UnityExtensionMethods
{
    /// <summary>
    /// Rotates a transform so its right hand direction points towards 
    /// <paramref name="targetPosition"/> at the provided speed.
    /// </summary>
    /// <param name="value">Transform being rotated.</param>
    /// <param name="targetPosition">Target location.</param>
    /// <param name="speed">Rotation speed.</param>
    public static void LookAt2D(this Transform value, Vector3 targetPosition, float speed)
    {
        var dir = targetPosition - value.position;
        value.right = Vector3.Lerp(value.right, new Vector3(dir.x, dir.y, value.position.z), speed * Time.deltaTime);
    }
}
