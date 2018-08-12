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

    public Waypoint waypointDestination;

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
                    //TODO Run this when a desire is granted
                    nextDesireTime = UnityEngine.Random.Range(nextDesireTimeRange.x, nextDesireTimeRange.y);
                }
            }
        }
    }

    private void Move()
    {
        if (Time.time - timeSinceMoved > nextMovementTime)
        {
            timeSinceMoved = 0; //Wait for a little bit
            waypointDestination = GetRandomUnclaimedWaypoint();
            seeker = GetComponent<Seeker>();
            seeker.StartPath(transform.position, waypointDestination.transform.position, OnPathComplete);
        }
    }

    public void OnPathComplete (Path p) {
        if (p.error){
            Debug.LogWarning(p.error);
        }
        timeSinceMoved = Time.time; //Reset move timer
        nextMovementTime = UnityEngine.Random.Range(nextMovementTimeRange.x, nextMovementTimeRange.y);
    }

    private Waypoint GetRandomUnclaimedWaypoint()
    {
        List<Waypoint> waypoints = new List<Waypoint>(FindObjectsOfType<Waypoint>());
        List<CrewAI> crewMembers = new List<CrewAI>(FindObjectsOfType<CrewAI>());
        
        crewMembers.Remove(this);
        List<Waypoint> waypointsToRemove = new List<Waypoint>();
        foreach(CrewAI crewMember in crewMembers){
            foreach(Waypoint waypoint in waypoints){
                if (crewMember.waypointDestination == waypoint){
                    waypointsToRemove.Add(waypoint);
                }
            }
            
        }
        foreach (Waypoint waypointToRemove in waypointsToRemove){
            waypoints.Remove(waypointToRemove);
        }
        if (waypoints.Count > 0){
            return waypoints[UnityEngine.Random.Range(0, waypoints.Count)];
        }
        Debug.LogError("No waypoints found");
        return null;
    }
}
