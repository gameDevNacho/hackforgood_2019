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
            node.gameObject.name = "Node " + (int)node.transform.parent.position.x + "-" + (int)node.transform.parent.position.z;
            foreach (Node n in nodes)
            {
                if (node != n)
                {
                    Vector3 dif = (node.transform.parent.position - n.transform.parent.position);
                    if (0.5 < dif.magnitude && dif.magnitude < 4)
                    {
                        Vector3 ndif = dif.normalized;
                        if (ndif == Vector3.forward)
                        {
                            node.SetNodeAt(n, Directions.Up);
                        }
                        else if (ndif == Vector3.back)
                        {
                            node.SetNodeAt(n, Directions.Down);
                        }
                        else if (ndif == Vector3.left)
                        {
                            node.SetNodeAt(n, Directions.Left);
                        }
                        else if (ndif == Vector3.right)
                        {
                            node.SetNodeAt(n, Directions.Right);
                        }
                        EditorUtility.SetDirty(node);
                        //Undo.RecordObject(node, "NodeSetUp");
                    }
                }
            }
        }
    }
}
