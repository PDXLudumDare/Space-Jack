﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class PlayerStatus : MonoBehaviour {
	
	[SerializeField] int securityPoints = 100;
	[SerializeField] TextMeshProUGUI securityText;
	[SerializeField] float timeUntilWin = 100f;
	[SerializeField] int displayMultiplier = 100;
	[SerializeField] TextMeshProUGUI timerDisplay;

	[SerializeField] GameObject winMenu;
	[SerializeField] GameObject loseMenu;
	[SerializeField] GameObject pauseMenu;

	float timer = 0;
	bool pauseMenuOpen = false;

	//TODO Max security points

	void Awake(){
		//TODO Set to static function
	}

	// Use this for initialization
	void Start () {
		securityText.text = securityPoints.ToString();
	}

	void Update(){
		timer += Time.deltaTime;
		DisplayTimer();
		if (timer > timeUntilWin) {	
			GameWin();
		}
		if (Input.GetKeyDown(KeyCode.Escape)){
			if (pauseMenuOpen){
				ClosePauseMenu();
			}else{
				OpenPauseMenu();
			}
			
		}
	}

    private void DisplayTimer()
    {
        timerDisplay.text = Mathf.RoundToInt(timer * displayMultiplier)  + " / " + timeUntilWin * displayMultiplier + " TB";
    }

    public void ChangeSecurityPoints(int value){
		securityPoints = securityPoints + value;
		securityText.text = securityPoints.ToString();
		if (securityPoints < 0){
			GameOver();
		}
	}

	private void GameWin()
    {
        winMenu.SetActive(true);
    }

    private void GameOver()
    {
        loseMenu.SetActive(true);
    }

	private void OpenPauseMenu()
    {
		pauseMenuOpen = true;
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
    }
	private void ClosePauseMenu()
    {
		pauseMenuOpen = false;
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
    }
}
