using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Directions { Up = 0, Down, Left, Right }

public class Node : MonoBehaviour
{
    [SerializeField]
    private Node up;
    [SerializeField]
    private Node down;
    [SerializeField]
    private Node left;
    [SerializeField]
    private Node right;
    [SerializeField]
    private Garbage garbagePrefab;

    public bool hasGarbage = false;
    [SerializeField]
    private Transform nodeCenter;

    private Garbage garbage;
    
    private Level level;

    public static float curveAperture = 2f;

    public void InitializeNode(Level level)
    {
        this.level = level;

        if(garbagePrefab)
        {
            //garbage = Instantiate(garbagePrefab, nodeCenter.position, garbagePrefab.transform.rotation, transform);
            hasGarbage = true;
            garbage = Instantiate(garbagePrefab);
            garbage.transform.position = transform.position;
            level.AddGarbage(garbage.garbageType);
        }
    }

    public Node GetNodeAtDirection(Directions direction)
    {
        switch (direction)
        {
            case Directions.Up:
                return up;
            case Directions.Down:
                return down;
            case Directions.Left:
                return left;
            case Directions.Right:
                return right;
            default:
                return null;
        }
    }

    public Vector3 GetNodePosition()
    {
        return nodeCenter.position;
    }

    public Garbage GetGarbage()
    {
        return garbage;
    }

    public void RemoveGarbage()
    {
        Destroy(garbage.gameObject);
        hasGarbage = false;
    }

    public Bezier BuildBezier(Directions from, Directions to)
    {
        Vector3 a = (GetNodeAtDirection(from).transform.position - transform.position).normalized * curveAperture;
        Vector3 b = transform.position;
        Vector3 c = transform.position;
        Vector3 d = (GetNodeAtDirection(from).transform.position - transform.position).normalized * curveAperture;

        return new Bezier(a, b, c, d);
    }

    public void SetNodeAt(Node node, Directions dir)
    {
        switch (dir)
        {
            case Directions.Up:
                up = node;
                break;
            case Directions.Down:
                down = node;
                break;
            case Directions.Left:
                left = node;
                break;
            case Directions.Right:
                right = node;
                break;
            default:
                break;
        }
    }
}
