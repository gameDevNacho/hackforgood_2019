using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField]
    private List<Node> nodes;

    private int yellowGarbage;
    private int blueGarbage;
    private int greenGarbage;
    private int brownGarbage;
    private int grayGarbage;

    public void Initialize()
    {
        for (int i = 0; i < nodes.Count; i++)
        {
            nodes[i].InitializeNode(this);
        }
    }

    public void AddGarbage(GarbageType garbageType)
    {
        switch (garbageType)
        {
            case GarbageType.Yellow:
                yellowGarbage++;
                break;
            case GarbageType.Blue:
                blueGarbage++;
                break;
            case GarbageType.Green:
                greenGarbage++;
                break;
            case GarbageType.Brown:
                brownGarbage++;
                break;
            case GarbageType.Gray:
                grayGarbage++;
                break;
        }
    }
}
