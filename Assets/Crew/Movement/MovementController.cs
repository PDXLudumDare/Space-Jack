using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour {
	public Waypoint currentWaypoint;
	public float waypointDistanceThreshold = .1f;

	PathManager pathManager;

	void Start(){
		pathManager = FindObjectOfType<PathManager>();
	}

	void MoveTowardsWaypoint(Waypoint waypoint){
		//StartCoroutine(Patrol());
	}

}
