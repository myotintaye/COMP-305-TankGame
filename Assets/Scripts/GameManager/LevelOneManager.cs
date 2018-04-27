using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelOneManager : MonoBehaviour {


	public float timer = 120f;
	
	public int totalRound;
	public int currentPlayer;

	public Camera mainCamera;
	
	public GameObject tankA1;

	public Text txtHealth;
	public Text txtBombChance;
	public Text txtTimeLeft;

	public Text txtGetMedicalKit;
	public Text txtGetShield;
	public Text txtGetTimer;

	public Image imgMedicalDone;
	public Image imgShieldDone;
	public Image imgTimerDone;
	public Image imgInstructions;
	
	public Text txtWinningMessage;	
	public GameObject gameOverPanel;

	private bool getMedicalKit = false;
	private bool getShield = false;
	private bool getTimer = false;


	// Use this for initialization
	void Start () {
		
		totalRound = 1;
		
		currentPlayer = 0; 
//		
//		tankA1.SendMessage("Deactivate");	
//		tankA1.SendMessage("Activate");
		
		UpdateBombChance(tankA1);
		
		tankA1.SendMessage("SetDamage");
		
		float zCamera = mainCamera.transform.position.z;
		mainCamera.transform.position = new Vector3(tankA1.transform.position.x, tankA1.transform.position.y, zCamera);
		
		mainCamera.SendMessage("SetFollowedTank", tankA1);
		
	}
	
	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;
		txtTimeLeft.text = Mathf.FloorToInt(timer).ToString();

		if (timer <= 295)
		{
			imgInstructions.enabled = false;
		}

		if (timer <= 0)
		{
			/* Game over */
			txtWinningMessage.text = "Time's up..";
			gameOverPanel.SetActive(true);
		}

		if (checkWinning())
		{
			txtWinningMessage.text = "Well done! You passed the tutorial.";
			gameOverPanel.SetActive(true);		
		}
		
	}
	
	bool checkWinning()
	{
		if (getMedicalKit && getShield && getTimer)
		{
			return true;
		}
		else
		{
			return false;
		}
	}

	void UpdateHealth(GameObject tank)
	{
		txtHealth.text = tank.GetComponent<TankController>().GetHealth().ToString();
	}

	void UpdateBombChance(GameObject tank)
	{
		txtBombChance.text = tank.GetComponent<TankController>().GetBombChance().ToString();
	}

	void AddMedicalKit()
	{
		getMedicalKit = true;
		
		txtGetMedicalKit.text = "1. Got Medical Kit";
		imgMedicalDone.enabled = true;
	}
	
	void AddShield()
	{
		getShield = true;
		
		txtGetShield.text = "2. Got Shield";
		imgShieldDone.enabled = true;
	}
	
	void AddTime()
	{
		timer += 5;
		getTimer = true;
		
		txtGetTimer.text = "3. Got Timer";
		imgTimerDone.enabled = true;
	}

	void UpdatePauseStatus()
	{
		tankA1.SendMessage("UpdatePauseStatus");
	}

}