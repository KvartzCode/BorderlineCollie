using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBobbing : MonoBehaviour
{
    public Transform bodyTransform;
    public float bobbingSpeed = 1f;

    public Transform startPoint; // Set these in the Inspector.
    public Transform controlPoint; // Control point for the Bezier curve.
    public Transform endPoint; // Set these in the Inspector.

    private void Update()
    {
        float t = Mathf.PingPong(Time.time * bobbingSpeed, 1f);
        Vector3 targetPosition = GetQuadraticBezierPoint(t);

        transform.position = bodyTransform.position + targetPosition;
    }

    private Vector3 GetQuadraticBezierPoint(float t)
    {
        // B(t) = (1-t)^2 * P0 + 2 * (1-t) * t * P1 + t^2 * P2
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        float uuu = uu * u;
        float ttt = tt * t;

        Vector3 p = uuu * startPoint.position; // (1-t)^3 * P0
        p += 2 * uu * t * controlPoint.position; // 2 * (1-t)^2 * t * P1
        p += tt * endPoint.position; // t^2 * P2

        return p;
    }
}
