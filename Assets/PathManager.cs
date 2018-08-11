using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.AI;

public class PathManager : MonoBehaviour
{

	public float walk_speed = 5.0f;

	private Stack<Vector3> current_path;

	private Vector3 current_waypoint_position;

	private float move_time_total;

	private float move_time_current;

	public void NavigateTo(Vector3 destination)
	{
		// find closest waypoint to current position and destination position
		current_path = new Stack<Vector3>();
		var current_node = FindClosestWaypoint(transform.position);
		var end_node = FindClosestWaypoint(destination);
		if (current_node == null || end_node == null || current_node == end_node)
			return;
		
		// open_list is nodes we want to visit, closed_list is nodes we've visited
		var open_list = new SortedList<float, Waypoint>();
		var closed_list = new List<Waypoint>();
		open_list.Add(0, current_node);
		current_node.previous = null;
		current_node.distance = 0f;
		
		
		while (open_list.Count > 0)
		{
			current_node = open_list.Values[0];
			open_list.RemoveAt(0);
			var dist = current_node.distance;
			closed_list.Add(current_node);
			if (current_node == end_node) // check to see if we reached our destination
			{
				break;
			}

			foreach (var neighbor in current_node.neighbors)
			{
				if (closed_list.Contains(neighbor) || open_list.ContainsValue(neighbor))
				{
					continue;
				}
				neighbor.previous = current_node;
				neighbor.distance = dist + (neighbor.transform.position - current_node.transform.position).magnitude;
				var distance_to_target = (neighbor.transform.position - end_node.transform.position).magnitude;
				open_list.Add(neighbor.distance + distance_to_target, neighbor);
			}
		}

		if (current_node == end_node)
		{
			while (current_node.previous != null)
			{
				current_path.Push(current_node.transform.position);
				current_node = current_node.previous;
			}
			current_path.Push(transform.position);
		}
	}

	public void Stop()
	{
		current_path = null;
		move_time_total = 0;
		move_time_current = 0;
	}
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (current_path != null && current_path.Count > 0)
		{
			if (move_time_current < move_time_total)
			{
				move_time_current += Time.deltaTime;
				if (move_time_current > move_time_total)
					move_time_current = move_time_total;
				transform.position = Vector3.Lerp(current_waypoint_position, current_path.Peek(),
					move_time_current / move_time_total);
			}
			else
			{
				current_waypoint_position = current_path.Pop();
				if (current_path.Count == 0)
					Stop();
				else
				{
					move_time_current = 0;
					move_time_total = (current_waypoint_position - current_path.Peek()).magnitude / walk_speed;
				}
			}
		}
	}

	private Waypoint FindClosestWaypoint(Vector3 target)
	{
		GameObject closest = null;
		float closest_dist = Mathf.Infinity;
		foreach (var waypoint in GameObject.FindGameObjectsWithTag("Waypoint"))
		{
			var dist = (waypoint.transform.position - target).magnitude;
			if (dist < closest_dist)
			{
				closest = waypoint;
				closest_dist = dist;
			}
		}

		if (closest != null)
		{
			return closest.GetComponent<Waypoint>();
		}

		return null;
	}
}
