﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelOneManager : MonoBehaviour {


	public float timer = 30f;
	
	public int totalRound;
	public int currentPlayer;

	public Camera mainCamera;
	
	public GameObject tankA1;
	
		
	public Text txtWinningMessage;	
	public GameObject gameOverPanel;

	// Use this for initialization
	void Start () {
		totalRound = 1;
		
		currentPlayer = 0; 
//		
//		tankA1.SendMessage("Deactivate");	
//		tankA1.SendMessage("Activate");
			
		float zCamera = mainCamera.transform.position.z;
		mainCamera.transform.position = new Vector3(tankA1.transform.position.x, tankA1.transform.position.y, zCamera);
		
		mainCamera.SendMessage("SetFollowedTank", tankA1);
		
	}
	
	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;

		if (timer <= 0)
		{
			/* Game over */
			
			txtWinningMessage.text = "Congratulations!";
			gameOverPanel.SetActive(true);
		}
	}

}