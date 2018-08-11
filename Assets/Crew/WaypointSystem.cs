using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class WaypointSystem : MonoBehaviour {
	public bool isLoop = false;


	private void OnDrawGizmos()
	{
		Vector3 firstPosition = transform.GetChild(0).position;
		Vector3 previousPosition = firstPosition;

		Waypoint[] waypoints = GetComponentsInChildren<Waypoint>();
		foreach (Waypoint waypoint in waypoints)
		{
			Gizmos.DrawSphere(waypoint.transform.position, .2f);
			Gizmos.DrawLine(previousPosition, waypoint.transform.position);
			previousPosition = waypoint.transform.position;
			if (waypoint.connectedWaypoint){
				Gizmos.DrawLine(waypoint.transform.position, waypoint.connectedWaypoint.transform.position);
			}
		}
		if (isLoop){
			Gizmos.DrawLine(previousPosition, firstPosition);
		}
	}
}
