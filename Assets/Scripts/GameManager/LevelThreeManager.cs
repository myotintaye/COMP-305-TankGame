using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelThreeManager : MonoBehaviour {
	
	public float timer = 10f;
	
	public int totalRound;
	public int currentPlayer;

	public GameObject tankA1;
	public GameObject tankA2;
	public GameObject tankB1;
	public GameObject tankB2;
	
	public Text txtTankA1;
	public Text txtTankA1Health;
	public Text txtTankA1Bomb;
	
	public Text txtTankA2;
	public Text txtTankA2Health;
	public Text txtTankA2Bomb;
	
	public Text txtTankB1;
	public Text txtTankB1Health;
	public Text txtTankB1Bomb;
	
	public Text txtTankB2;
	public Text txtTankB2Health;
	public Text txtTankB2Bomb;
	
	public Text txtRound;
	public Text txtTimeLeft;


	// Use this for initialization
	void Start () {
		totalRound = 1;
		
		currentPlayer = 0; 

		tankA1.SendMessage("Activate");
		tankA2.SendMessage("Deactivate");
		tankB1.SendMessage("Deactivate");
		tankB2.SendMessage("Deactivate");
		
		tankB1.SendMessage("SetTankDirectionToLeft");
		tankB2.SendMessage("SetTankDirectionToLeft");
		
		UpdateInfoPanel();
		
	}
	
	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;

		if (timer <= 0)
		{
			SwitchPlayer();
			timer = 10;
		}
		
		txtTimeLeft.text = "Time: " + Mathf.FloorToInt(timer).ToString();
	}
	
	//    Swtich player, tankA1 = 0, tankB1 = 1, tankA2 = 2, tankB2 = 3
	void SwitchPlayer()
	{
		tankA1.SendMessage("Deactivate");
		tankA2.SendMessage("Deactivate");
		tankB1.SendMessage("Deactivate");
		tankB2.SendMessage("Deactivate");
		
		
		/* Reset current player */
		currentPlayer = (currentPlayer + 1) % 4;

		switch (currentPlayer)
		{
			case 0:
				tankA1.SendMessage("Activate");
				totalRound++;
				break;
			
			case 1:
				tankB1.SendMessage("Activate");
				break;
			
			case 2:
				tankA2.SendMessage("Activate");
				break;
			
			case 3:
				tankB2.SendMessage("Activate");
				break;
			
			other:
				break;
		}


		
		UpdateInfoPanel();

	}
	
	void UpdateInfoPanel()
	{
		txtRound.text = "Round: " + totalRound.ToString();
		
		txtTankA1.text = "Tank A1 stand by";
		txtTankA2.text = "Tank A2 stand by";
		txtTankB1.text = "Tank B1 stand by";
		txtTankB2.text = "Tank B2 stand by";
		
		switch (currentPlayer)
		{
			case 0:		
				txtTankA1.text = "Tank A1 is playing";
				break;
			
			case 1:
				txtTankA2.text = "Tank A2 is playing";
				break;
			
			case 2:
				txtTankB1.text = "Tank B1 is playing";
				break;
			
			case 3:
				txtTankB2.text = "Tank B2 is playing";
				break;
			
			other:
				break;
		}
		
		/* Update health info */
		txtTankA1Health.text = "Health: " + tankA1.GetComponent<TankController>().GetHealth().ToString();
		txtTankA2Health.text = "Health: " + tankA2.GetComponent<TankController>().GetHealth().ToString();
		
		txtTankB1Health.text = "Health: " + tankB1.GetComponent<TankController>().GetHealth().ToString();
		txtTankB2Health.text = "Health: " + tankB2.GetComponent<TankController>().GetHealth().ToString();

		/* Update bomb info */
		txtTankA1Bomb.text = "Bomb: " + tankA1.GetComponent<TankController>().GetBombChance().ToString();
		txtTankA2Bomb.text = "Bomb: " + tankA2.GetComponent<TankController>().GetBombChance().ToString();		
		
		txtTankB1Bomb.text = "Bomb: " + tankB1.GetComponent<TankController>().GetBombChance().ToString();
		txtTankB2Bomb.text = "Bomb: " + tankB2.GetComponent<TankController>().GetBombChance().ToString();		
	}
	
	void UpdateHealth(GameObject tank)
	{
		String hitTank = tank.name;

		Debug.Log("Hit tank = " + hitTank);

		if (hitTank == "TankA1")
		{
			txtTankA1Health.text = "Health: " + tank.GetComponent<TankController>().GetHealth().ToString();
		}
		else if (hitTank == "TankA2")
		{
			txtTankA2Health.text = "Health: " + tank.GetComponent<TankController>().GetHealth().ToString();
		}
		else if (hitTank == "TankB1")
		{
			txtTankB1Health.text = "Health: " + tank.GetComponent<TankController>().GetHealth().ToString();
		}
		else if (hitTank == "TankB2")
		{
			txtTankB2Health.text = "Health: " + tank.GetComponent<TankController>().GetHealth().ToString();
		}
	}

	void UpdateBombChance(GameObject tank)
	{
		String firedByTank = tank.name;

		if (firedByTank == "TankA1")
		{
			txtTankA1Bomb.text = "Bomb: " + tank.GetComponent<TankController>().GetBombChance().ToString();
		}
		else if (firedByTank == "TankA2")
		{
			txtTankA2Bomb.text = "Bomb: " + tank.GetComponent<TankController>().GetBombChance().ToString();
		}
		else if (firedByTank == "TankB1")
		{
			txtTankB1Bomb.text = "Bomb: " + tank.GetComponent<TankController>().GetBombChance().ToString();
		}
		else if (firedByTank == "TankB2")
		{
			txtTankB2Bomb.text = "Bomb: " + tank.GetComponent<TankController>().GetBombChance().ToString();
		}

	}
	
}
