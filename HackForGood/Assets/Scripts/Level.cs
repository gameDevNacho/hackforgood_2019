using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level : Singleton<Level>
{
  private List<Node> nodes;
  List<Truck> trucks;

  public Node firstNode;
  
  public int currentTruckIndex = 0;

  private int yellowGarbage;
  private int blueGarbage;
  private int greenGarbage;
  private int brownGarbage;
  private int grayGarbage;


  public bool Riding = false; 

  public void Initialize()
  {
    nodes = new List<Node>(FindObjectsOfType<Node>());
    trucks = new List<Truck>(FindObjectsOfType<Truck>());

    for (int i = 0; i < nodes.Count; i++)
    {
      nodes[i].InitializeNode(this);
    }

    trucks[currentTruckIndex].ResetTransform();
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
    if (currentTruckIndex < trucks.Count)
    {
      trucks[currentTruckIndex].ResetTransform();
      trucks[currentTruckIndex].SetDirections(ActionList.Instance.GetMoveDirections());
      Riding = true;
    }    
  }

  public void OnRideEnd()
  {
    Riding = false;
  }

  public void NextTruck()
  {
    if (currentTruckIndex < trucks.Count - 1)
    {
      currentTruckIndex++;
      trucks[currentTruckIndex].ResetTransform();
      ActionList.Instance.actionList.Clear();
      foreach (RectTransform child in ActionList.Instance.GetComponent<RectTransform>())
      {
        Debug.Log(child);
        Destroy(child.gameObject);
      }

      GameObject.Find("ScrollView").GetComponent<ScrollRect>().verticalNormalizedPosition = 0.5f;
    }
  }
}


