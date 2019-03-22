using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bezier
{
    Vector3 a, b, c, d;

    public Bezier(Vector3 a, Vector3 b, Vector3 c, Vector3 d)
    {
        this.a = a;
        this.b = b;
        this.c = c;
        this.d = d;
    }

    public Vector3 EvaluateQuadratic(float t)
    {
        Vector3 p0 = Vector3.Lerp(a, b, t);
        Vector3 p1 = Vector3.Lerp(b, c, t);
        return Vector3.Lerp(p0, p1, t);
    }

    public Vector3 EvaluateCubic(float t)
    {
        Vector3 p0 = EvaluateQuadratic(t);
        Vector3 p1 = EvaluateQuadratic(t);
        return Vector3.Lerp(p0, p1, t);
    }

    //con calculo de normal, bitangente y tangente tambien
    //public Point3f EvaluateCubic(float t)
    //{
    //    Vector3 p0 = EvaluateQuadratic(a.Position, b.Position, c.Position, t);
    //    Vector3 p1 = EvaluateQuadratic(b.Position, c.Position, d.Position, t);

    //    Point3f point = new Point3f(Vector3.Lerp(p0, p1, t));

    //    point.Tangent = EvaluateTangent(a.Position, b.Position, c.Position, d.Position, t);

    //    point.Bitangent = Vector3.Cross(Vector3.up, point.Tangent).normalized;

    //    point.Normal = Vector3.Cross(point.Bitangent, point.Tangent).normalized;

    //    return point;
    //}

    public Vector3 EvaluateTangent(float t)
    {
        float square = Mathf.Pow(t, 2);
        Vector3 tangent = a * (-square + 2 * t - 1) +
                          b * (3 * square - 4 * t + 1) +
                          c * (-3 * square + 2 * t) +
                          d * square;
        return tangent.normalized;
    }
}
