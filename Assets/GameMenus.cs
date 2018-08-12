using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenus : MonoBehaviour {
	
	int menuIndex = 0;
	int buildIndex;

	void Start(){
		buildIndex = SceneManager.GetActiveScene().buildIndex;
	}

	public void NextLevel(){
		
		int nextBuildIndex = buildIndex + 1;
		if (nextBuildIndex <= SceneManager.sceneCountInBuildSettings){
			Time.timeScale = 1f;
			SceneManager.LoadScene(buildIndex + 1);
		}else{
			LoadMainMenu();
		}
		
	}

	public void RestartLevel(){
		Time.timeScale = 1f;
		SceneManager.LoadScene(buildIndex);
	}

	public void LoadMainMenu(){
		Time.timeScale = 1f;
		SceneManager.LoadScene(menuIndex);
	}
}
