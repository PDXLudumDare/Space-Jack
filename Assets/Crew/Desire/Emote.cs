using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emote : MonoBehaviour {
	[SerializeField] float xOffset = 0f;
	[SerializeField] float yOffset = -1f;

	public GameObject emoteIcon;

	Quaternion rotation;

	void Awake (){
		rotation = Quaternion.identity;
		transform.position = new Vector2(
			transform.position.x + xOffset,
			transform.position.y + yOffset
		);
	}

	void LateUpdate(){
		transform.rotation = rotation;
	}

	public void SetEmoteIcon(Sprite icon){
		emoteIcon.GetComponent<SpriteRenderer>().sprite = icon;
	}
	
}
