using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Truck : MonoBehaviour
{
  [SerializeField]
  float speed = 5;
  [SerializeField]
  GarbageType truckType;

  public Node startNode;
  Node destination;

  Directions currentDirection;
  Directions nextDirection;

<<<<<<< HEAD
  List<Directions> directions;
  int dirIndex = 0;
=======
    [HideInInspector]
    public List<Directions> directions;
    int dirIndex = 0;
>>>>>>> d187d33b5162bfacf0878b496f7c3aabf2e14293

  bool outOfTrack = false;
  float rotationSpeed = 1000;
  float maxTimeRotating = 1.5f;
  float timeRotating = 0;
  Vector3 outOfTrackDir;

<<<<<<< HEAD
=======
    Vector3 initialPos;
    Quaternion initialRot;
>>>>>>> d187d33b5162bfacf0878b496f7c3aabf2e14293

  bool GarbagePickedUp = false;

  Transform initialTransform;

  private static readonly float arrivedThreshold = 0.1f;

  // Start is called before the first frame update
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
<<<<<<< HEAD
      Move();
=======
        //SetInitialDirection();
        startNode = GetClosestNode();
        initialPos = this.transform.position;
        initialRot = transform.rotation;
>>>>>>> d187d33b5162bfacf0878b496f7c3aabf2e14293
    }
    if (outOfTrack)
    {
      RotateOutOfControl();
      timeRotating += Time.deltaTime;
      if (timeRotating > maxTimeRotating)
      {
        //Destroy(this.gameObject);
        ResetTransform();

      }
    }
  }

  private void LateUpdate()
  {
    if (destination)
    {
      if (HasArrivedDestination())
      {
        OnArrivedToDestination();
      }
    }
  }

  private void Move()
  {
    Vector3 direction = destination.transform.position - transform.position;
    direction.Normalize();
    transform.position += direction * speed * Time.deltaTime;
    this.transform.LookAt(destination.transform);
  }

  private bool HasArrivedDestination()
  {
    return (destination.transform.position - transform.position).magnitude <= arrivedThreshold;
  }

  private void OnArrivedToDestination()
  {
    Debug.Log(destination.name);
       
    if (dirIndex < directions.Count)
    {
      nextDirection = directions[dirIndex];
      dirIndex++;

      PickUpGarbage();
      
      Node nextNode = destination.GetNodeAtDirection(nextDirection);
      if (nextNode)
      {
        transform.position = destination.transform.position;
        transform.LookAt(nextNode.transform);
        destination = destination.GetNodeAtDirection(nextDirection);
      }
      else
      {
        Crash();
        outOfTrackDir = transform.forward;      
      }
    }
    else
    {
      CheckFactory();
      Level.Instance.OnRideEnd();
      destination = null;
      dirIndex = 0;
    }

    //action getfromlist
  }

  private void PickUpGarbage()
  {
    if (destination.hasGarbage)
    {
      Garbage g = destination.GetGarbage();
      if (g.garbageType == truckType)
      {
        destination.RemoveGarbage();
        GarbagePickedUp = true;
        SpriteInstancer.Instance.InstantiateAt(transform.position, true);
      }
    }
  }

  private void CheckFactory()
  {
    if (destination.isFactory)
    {
     
      if (destination.type == truckType)
      {

        if (GarbagePickedUp)
        {
          SpriteInstancer.Instance.InstantiateAt(transform.position, true,true);
          //Llamar para cambio de camion
          Level.Instance.NextTruck();
        }
        else
        {
<<<<<<< HEAD
          SpriteInstancer.Instance.InstantiateAt(transform.position, false);
          ResetTransform();
        }       
      }
=======
            destination = null;
            //dirIndex = 0;
        }

        //action getfromlist
>>>>>>> d187d33b5162bfacf0878b496f7c3aabf2e14293
    }
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

  private void SetInitialDirection()
  {
    Directions dir = Directions.Right;
    Vector3 front = Vector3.Cross(transform.up, transform.forward).normalized;
    float minAngle = 359;
    float angle;

    angle = Mathf.Abs(Vector3.Angle(front, Vector3.forward));
    if (angle < minAngle)
    {
      minAngle = angle;
      dir = Directions.Right;
    }

    angle = Mathf.Abs(Vector3.Angle(front, Vector3.back));
    if (angle < minAngle)
    {
      minAngle = angle;
      dir = Directions.Left;
    }

    angle = Mathf.Abs(Vector3.Angle(front, Vector3.left));
    if (angle < minAngle)
    {
      minAngle = angle;
      dir = Directions.Down;
    }

    angle = Mathf.Abs(Vector3.Angle(front, Vector3.right));
    if (angle < minAngle)
    {
      minAngle = angle;
      dir = Directions.Up;
    }

    currentDirection = dir;
    nextDirection = dir;
  }

  public void SetDirections(List<Directions> dirs)
  {
    Debug.Log(dirs.Count);

    directions = dirs;
    destination = startNode.GetNodeAtDirection(directions[dirIndex]);
    if (destination == null)
    {
<<<<<<< HEAD
      Crash();
=======
        this.transform.position = initialPos;
        this.transform.rotation = initialRot;
>>>>>>> d187d33b5162bfacf0878b496f7c3aabf2e14293
    }

    dirIndex++;
  }


  public void Crash()
  {
    outOfTrack = true;
    SpriteInstancer.Instance.InstantiateAt(transform.position, false);
    Level.Instance.OnRideEnd();
  }

  public void RotateOutOfControl()
  {
    transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    transform.position += outOfTrackDir * Time.deltaTime * speed;
  }

  public void ResetTransform()
  {
    transform.position =  Level.Instance.firstNode.transform.position;
    transform.rotation =  Quaternion.Euler(0,90,0);
    dirIndex = 0;
    outOfTrack = false;
    GarbagePickedUp = false;
  }



}
