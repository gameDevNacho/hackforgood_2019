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
    [SerializeField]
    private bool hasGarbage;
    [SerializeField]
    private Transform nodeCenter;

    private Garbage garbage;

    private Level level;

    public void InitializeNode(Level level)
    {
        this.level = level;

        if(hasGarbage)
        {
            garbage = Instantiate(garbagePrefab, nodeCenter.position, garbagePrefab.transform.rotation, transform);
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
}
