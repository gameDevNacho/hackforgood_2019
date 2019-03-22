using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class InefficientDirAssignation : MonoBehaviour
{
    [MenuItem("Window/AsignarVecinos")]
    static void AssignNeigbours()
    {

        Node[] nodes = FindObjectsOfType<Node>();

        foreach (Node node in nodes)
        {
            node.gameObject.name = "Node " + (int)node.transform.position.x + "-" + (int)node.transform.position.z;
            foreach (Node n in nodes)
            {
                if (node != n)
                {
                    Vector3 dif = (node.transform.position - n.transform.position);
                    if (1 < dif.magnitude && dif.magnitude < 3)
                    {
                        Vector3 ndif = dif.normalized;
                        if (ndif == Vector3.forward)
                        {
                            node.SetNodeAt(n, Directions.Up);
                            n.SetNodeAt(node, Directions.Down);
                        }
                        else if (ndif == Vector3.back)
                        {
                            node.SetNodeAt(n, Directions.Down);
                            n.SetNodeAt(node, Directions.Up);
                        }
                        else if (ndif == Vector3.left)
                        {
                            node.SetNodeAt(n, Directions.Left);
                            n.SetNodeAt(node, Directions.Right);
                        }
                        else if (ndif == Vector3.right)
                        {
                            node.SetNodeAt(n, Directions.Right);
                            n.SetNodeAt(node, Directions.Left);
                        }
                    }
                }
            }
        }
    }
}
