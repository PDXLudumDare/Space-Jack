using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emote : MonoBehaviour {

	public GameObject emoteIcon;

	public void SetEmoteIcon(Sprite icon){
		emoteIcon.GetComponent<SpriteRenderer>().sprite = icon;
	}
	
}
