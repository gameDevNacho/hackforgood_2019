using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Truck : MonoBehaviour
{
    [SerializeField]
    float speed = 5;
    [SerializeField]
    GarbageType truckType;

    [SerializeField]
    Node destination;

    Directions currentDirection;
    Directions nextDirection;

    private static readonly float arrivedThreshold = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        currentDirection = Directions.Right;
        nextDirection = Directions.Right;
    }

    // Update is called once per frame
    void Update()
    {
        if(destination)
        {
            Move();
        }
    }

    private void LateUpdate()
    {
        if(HasArrivedDestination())
        {
            OnArrivedToDestination();
        }
    }

    private void Move()
    {
        Vector3 direction = destination.transform.position - transform.position;
        direction.Normalize();
        transform.position += direction * speed * Time.deltaTime;
    }

    private bool HasArrivedDestination()
    {
        return (destination.transform.position - transform.position).magnitude <= arrivedThreshold;
    }

    private void OnArrivedToDestination()
    {
        PickUpGarbage();
        Node nextNode = destination.GetNodeAtDirection(nextDirection);
        transform.position = destination.transform.position;
        transform.LookAt(nextNode.transform);
        destination = destination.GetNodeAtDirection(nextDirection);

        //prueba
        nextDirection = Directions.Right;
    }

    private void PickUpGarbage()
    {
        if(destination.hasGarbage)
        {
            Garbage g = destination.GetGarbage();
            if(g.garbageType == truckType)
                destination.RemoveGarbage();
        }
    }
}
