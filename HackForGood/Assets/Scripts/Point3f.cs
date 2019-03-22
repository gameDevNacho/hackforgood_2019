using UnityEngine;

public class Point3f {


    Vector3 position;

    public Vector3 Position
    {
        get
        {
            return position;
        }
        set
        {
            position = value;
        }
    }

    Quaternion rotation;

    public Quaternion Rotation
    {
        get
        {
            return rotation;
        }
        set
        {
            rotation = value;
            normal = (rotation * normal).normalized;
            bitangent = (rotation * bitangent).normalized;
        }
    }

    //Equivale a la normal de la carretera en este punto
    Vector3 normal;

    public Vector3 Normal
    {
        get { return normal; }
        set { normal = value; }
    }


    Vector3 tangent;

    public Vector3 Tangent
    {
        get { return tangent; }
        set { tangent = value; }
    }

    Vector3 bitangent;

    public Vector3 Bitangent
    {
        get { return bitangent; }
        set { bitangent = value; }
    }


    public Point3f(Vector3 position)
    {
        this.position = position;
        rotation = Quaternion.Euler(Vector3.up);
        normal = Vector3.up;
        tangent = Vector3.forward;
        bitangent = Vector3.left;
    }

    public void Rotate(float angle)
    {
        rotation = Quaternion.AngleAxis(angle, tangent);
        normal = rotation * normal;
        bitangent = rotation * bitangent;
        Debug.DrawLine(position, position + normal, Color.red);
    }

    //El eje es el vector tangente a la carretera en este punto normalizado
    //public void RotatePoint(float angle)
    //{
    //    float cos = Mathf.Cos(angle);
    //    float sin = Mathf.Sin(angle);
    //    float x = tangent.x;
    //    float y = tangent.y;
    //    float z = tangent.z;

    //    Vector4 column0 = new Vector4(cos + Mathf.Pow(x, 2) * (1 - cos),
    //                                  y * x * (1 - cos) + z * sin,
    //                                  y * x * (1 - cos) - y * sin,
    //                                  0);

    //    Vector4 column1 = new Vector4(x * y * (1 - cos) - z * sin,
    //                                  cos + Mathf.Pow(y, 2) * (1 - cos),
    //                                  z * y * (1 - cos) + x * sin,
    //                                  0);

    //    Vector4 column2 = new Vector4(x * z * (1 - cos) + x * sin,
    //                                  y * z * (1 - cos) - x * sin,
    //                                  cos + Mathf.Pow(z, 2) * (1 - cos),
    //                                  0);

    //    Vector4 column3 = new Vector4(0, 0, 0, 1);


    //    Matrix4x4 rotationMatrix = new Matrix4x4(column0, column1, column2, column3);

    //    normal = rotationMatrix.MultiplyPoint3x4(position).normalized;
    //}

    //public void RecalculateVectors(int index)
    //{
    //    Path.points
    //}
}
