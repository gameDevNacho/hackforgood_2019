using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaferTruck : MonoBehaviour
{
  [SerializeField]
  float speed = 5;
  [SerializeField]
  GarbageType truckType;

  public Node startNode;
  Node destination;

  Directions currentDirection;
  Directions nextDirection;

  List<Directions> directions;
  int dirIndex = 0;

  bool outOfTrack = false;
  float rotationSpeed = 1000;
  float maxTimeRotating = 1.5f;
  float timeRotating = 0;
  Vector3 outOfTrackDir;

  Transform initialTransform;

  private static readonly float arrivedThreshold = 0.1f;

  void Start()
  {
    //SetInitialDirection();
    startNode = GetClosestNode();
    initialTransform = this.transform;
  }

  // Update is called once per frame
  void Update()
  {
    if (destination)
    {
      Move();
    }
  }


  private void Move()
  {
    Vector3 direction = destination.transform.position - transform.position;
    direction.Normalize();
    transform.position += direction * speed * Time.deltaTime;
    this.transform.LookAt(destination.transform);
  }

  private Node GetClosestNode()
  {
    Node[] nodes = FindObjectsOfType<Node>();
    Node closestNode = nodes[0];
    float minDistance = (transform.position - closestNode.transform.position).sqrMagnitude;

    for (int i = 1; i < nodes.Length; i++)
    {
      float dist = (nodes[i].transform.position - transform.position).sqrMagnitude;
      if (dist < minDistance)
      {
        minDistance = dist;
        closestNode = nodes[i];
      }
    }

    return closestNode;
  }
}
