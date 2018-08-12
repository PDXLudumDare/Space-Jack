using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LimitImageSize : MonoBehaviour {
	[SerializeField] float maxWidth = 1f; 
	[SerializeField] float maxHeight = 1f;

	SpriteRenderer sr;
	

	// Use this for initialization
	void Start () {
		sr = GetComponent<SpriteRenderer>();
		transform.localScale = new Vector3(
			maxWidth /sr.bounds.size.x,
			maxHeight /sr.bounds.size.y,
			1);		
	}
	
}
