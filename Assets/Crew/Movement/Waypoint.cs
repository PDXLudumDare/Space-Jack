using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Waypoint : MonoBehaviour {
	[SerializeField] string roomName;
	[SerializeField] float gizmosWaypointSize = .1f;
	public Waypoint connectedWaypoint;
	
	public List<Waypoint> neighbors;
	public Waypoint previous { get; set; }
	public float distance { get; set; }

	
	
}
