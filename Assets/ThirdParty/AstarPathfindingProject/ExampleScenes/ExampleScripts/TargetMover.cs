using UnityEngine;
using System.Linq;

namespace Pathfinding {
	/** Moves the target in example scenes.
	 * This is a simple script which has the sole purpose
	 * of moving the target point of agents in the example
	 * scenes for the A* Pathfinding Project.
	 *
	 * It is not meant to be pretty, but it does the job.
	 */
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_target_mover.php")]
	public class TargetMover : MonoBehaviour {
		/** Mask for the raycast placement */
		public LayerMask mask;

		public Transform target;
		IAstarAI[] ais;

		/** Determines if the target position should be updated every frame or only on double-click */
		public bool onlyOnDoubleClick;
		public bool use2D;

		Camera cam;

		public void Start () {
			//Cache the Main Camera
			cam = Camera.main;
			// Slightly inefficient way of finding all AIs, but this is just an example script, so it doesn't matter much.
			// FindObjectsOfType does not support interfaces unfortunately.
			ais = FindObjectsOfType<MonoBehaviour>().OfType<IAstarAI>().ToArray();
			useGUILayout = false;
		}

		public void SetNewTargetPosition (Vector3 newPosition) {
			
			if (newPosition != target.position) {
				target.position = newPosition;
			}
		}
	}
}
