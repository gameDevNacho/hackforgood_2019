﻿using System.Collections;
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
        SetInitialDirection();
        destination = GetClosestNode();
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

        //action getfromlist
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
}
