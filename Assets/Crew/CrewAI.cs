using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

[RequireComponent(typeof(DesireSystem))]
public class CrewAI : MonoBehaviour
{
    [SerializeField] Vector2 nextMovementTimeRange = new Vector2(3f, 30f);
    [SerializeField] Vector2 nextDesireTimeRange = new Vector2(3f, 30f);
    [SerializeField] float desireLifeSpan = 15f;

    float nextMovementTime;
    float nextDesireTime;
    float timeSinceMoved;
    float timeSinceDesireCreated;

    DesireSystem desireSystem;

    Waypoint[] waypoints;
    Seeker seeker;

    // Use this for initialization
    void Start()
    {
        desireSystem = GetComponent<DesireSystem>();
        nextMovementTime = UnityEngine.Random.Range(nextMovementTimeRange.x, nextMovementTimeRange.y);
        nextDesireTime = UnityEngine.Random.Range(nextDesireTimeRange.x, nextDesireTimeRange.y);
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
                    //TODO Run this when a desire is given
                    nextDesireTime = UnityEngine.Random.Range(nextDesireTimeRange.x, nextDesireTimeRange.y);
                }
            }
        }
    }

    private void Move()
    {
        print(nextMovementTime);
        if (Time.time - timeSinceMoved > nextMovementTime)
        {
            timeSinceMoved = 0; //Wait for a little bit
            Waypoint waypoint = GetRandomWaypoint();
            print(name + " GO TO " + waypoint);
            seeker = GetComponent<Seeker>();
            print(seeker);
            seeker.StartPath(transform.position, waypoint.transform.position, OnPathComplete);
        }
    }

    public void OnPathComplete (Path p) {
        if (p.error){
            Debug.LogWarning(p.error);
        }
        timeSinceMoved = Time.time; //Reset move timer
        nextMovementTime = UnityEngine.Random.Range(nextMovementTimeRange.x, nextMovementTimeRange.y);
    }

    private Waypoint GetRandomWaypoint()
    {
        Waypoint[] waypoints = FindObjectsOfType<Waypoint>();
        if (waypoints.Length > 0){
            return waypoints[UnityEngine.Random.Range(0, waypoints.Length)];
        }
        Debug.LogError("No waypoints found");
        return null;
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
