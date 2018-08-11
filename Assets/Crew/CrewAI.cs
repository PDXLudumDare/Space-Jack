using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MovementController))]
[RequireComponent(typeof(DesireSystem))]
public class CrewAI : MonoBehaviour
{
    [SerializeField] float nextMovementTime = 10f;
    [SerializeField] float nextDesireTime = 1f;
    [SerializeField] float desireLifeSpan = 5f;

    float timeSinceMoved;
    float timeSinceDesireCreated;


    DesireSystem desireSystem;

    // Use this for initialization
    void Start()
    {
        desireSystem = GetComponent<DesireSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        CreateDesire();
    }

    private void CreateDesire()
    {
        if (Time.time - timeSinceDesireCreated > nextDesireTime)
        {
            if (desireSystem.currentDesire == null)
            {
				timeSinceDesireCreated = Time.time;
                desireSystem.CreateDesire();
            }
            else
            {
                if (Time.time - timeSinceDesireCreated > desireLifeSpan)
                {
					timeSinceDesireCreated = Time.time;
					desireSystem.LoseDesire();
                }
            }
        }
    }

    private void Move()
    {
        if (Time.time - timeSinceMoved > nextMovementTime)
        {
            timeSinceMoved = Time.time;
            print(GetNearestWaypoint());
        }
    }

    Waypoint GetNearestWaypoint()
    {
        Waypoint[] waypoints = FindObjectsOfType<Waypoint>();
        Waypoint nearestWaypoint = null;
        float nearestWaypointDistance = Mathf.Infinity;
        foreach (Waypoint waypoint in waypoints)
        {
            float distanceToWaypoint = (waypoint.transform.position - transform.position).magnitude;
            if (distanceToWaypoint < nearestWaypointDistance)
            {
                nearestWaypoint = waypoint;
                nearestWaypointDistance = distanceToWaypoint;
            }
        }
        return nearestWaypoint;
    }
}
