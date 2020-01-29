using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionMethods
{
    /// <summary>
    /// Linearly maps value from number range A onto number range B.
    /// </summary>
    /// <param name="value">Value to be mapped.</param>
    /// <param name="fromSource">Minimum of range containing value.</param>
    /// <param name="toSource">Maximum of range containing value.</param>
    /// <param name="fromTarget">Minimum of target range.</param>
    /// <param name="toTarget">Maximum of target range.</param>
    /// <returns></returns>
    public static float Map(this float value, float fromSource, float toSource, float fromTarget, float toTarget)
    {
        return (value - fromSource) / (toSource - fromSource) * (toTarget - fromTarget) + fromTarget;
    }
}
