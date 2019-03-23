using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    private List<Node> nodes;
    List<Truck> trucks;

    private int yellowGarbage;
    private int blueGarbage;
    private int greenGarbage;
    private int brownGarbage;
    private int grayGarbage;

    public void Initialize()
    {
        nodes = new List<Node>(FindObjectsOfType<Node>());
        trucks = new List<Truck>(FindObjectsOfType<Truck>());

        for (int i = 0; i < nodes.Count; i++)
        {
            nodes[i].InitializeNode(this);
        }
    }

    private void Start()
    {
        Initialize();
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

    public void OnVamonosAtomos()
    {
        trucks[0].SetDirections(ActionList.Instance.GetMoveDirections());
        trucks[0].ResetTransform();
    }
}
