using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicPlayer : MonoBehaviour {

	AudioSource audioSource;

	// Use this for initialization
	void Awake () {
		if (FindObjectsOfType<MusicPlayer>().Length > 1){
			Destroy(this);
		}else{
			DontDestroyOnLoad(this.gameObject);
			print("WORKS");
		}
	}
	
	void Start(){
		print("STARTING");
		audioSource = GetComponent<AudioSource>();
		audioSource.Play();
	}
}
