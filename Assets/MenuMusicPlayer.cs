using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMusicPlayer : MonoBehaviour {

	AudioSource audioSource;

	// Use this for initialization
	void Awake () {
		if (FindObjectsOfType<MenuMusicPlayer>().Length > 1){
			Destroy(this);
		}
	}
	
	void Start(){
		MusicPlayer levelMusicPlayer = FindObjectOfType<MusicPlayer>();
		if (levelMusicPlayer){
			Destroy(levelMusicPlayer);
		}
		print("STARTING");
		audioSource = GetComponent<AudioSource>();
		audioSource.Play();
	}
}
