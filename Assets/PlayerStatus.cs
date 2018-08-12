using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class PlayerStatus : MonoBehaviour {
	
	[SerializeField] int securityPoints = 100;
	[SerializeField] TextMeshProUGUI securityText;

	//TODO Max security points

	void Awake(){
		//TODO Set to static function
	}

	// Use this for initialization
	void Start () {
		securityText.text = securityPoints.ToString();
	}
	
	public void ChangeSecurityPoints(int value){
		securityPoints = securityPoints + value;
		securityText.text = securityPoints.ToString();
		if (securityPoints < 0){
			GameOver();
		}
	}

    private void GameOver()
    {
        print("GAME OVER MAN");
    }
}
