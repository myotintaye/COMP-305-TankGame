using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelThreeManager : MonoBehaviour {
	
	public float timer = 10f;
	
	public int totalRound;
	public int currentPlayer;

	public Camera mainCamera;
	
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
	
	public Text txtWinningMessage;	
	public GameObject gameOverPanel;

	private bool isInTransition = false;
	private bool getMedicalKit = false;
	private bool getShield = false;
	private bool getTimer = false;


	// Use this for initialization
	void Start () {
		totalRound = 1;
		
		currentPlayer = 0; 
			
		float zCamera = mainCamera.transform.position.z;
		mainCamera.transform.position = new Vector3(tankA1.transform.position.x, tankA1.transform.position.y + 1, zCamera);
		
		mainCamera.SendMessage("SetFollowedTank", tankA1);

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
			Transition();
		}

		if (!isInTransition)
		{
			txtTimeLeft.text = "Time: " + Mathf.FloorToInt(timer).ToString();			
		}

		if (checkWinning())
		{
			/* Game over */
			gameOverPanel.SetActive(true);
		};
	}
	
	IEnumerator WaitForSwitch(){
		yield return new WaitForSeconds(3);
		yield return null;
	}
	
	void Transition()
	{
		isInTransition = true;
		
		tankA1.SendMessage("Deactivate");
		tankA2.SendMessage("Deactivate");
		tankB1.SendMessage("Deactivate");
		tankB2.SendMessage("Deactivate");

		timer = 10;

		txtTimeLeft.text = "Switching Player";
			
		Invoke("SwitchPlayer", 2);
	}

	bool checkWinning()
	{
		if (tankA1.GetComponent<TankController>().isDead() && tankA2.GetComponent<TankController>().isDead())
		{
			txtWinningMessage.text = "Team B wins the game!";
			return true;
		}
		else if (tankB1.GetComponent<TankController>().isDead() && tankB2.GetComponent<TankController>().isDead())
		{
			txtWinningMessage.text = "Team A wins the game!";
			return true;
		}
		else
		{
			return false;
		}
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
				if (!tankA1.GetComponent<TankController>().isDead())
				{
					tankA1.SendMessage("Activate");
					timer = 10;

					float zCamera = mainCamera.transform.position.z;
					mainCamera.transform.position = new Vector3(tankA1.transform.position.x, tankA1.transform.position.y + 1, zCamera);
					mainCamera.SendMessage("SetFollowedTank", tankA1);
				}
				else
				{
					/* Skip the player when dead */
					timer = -1;
				}
				
				/* New round */
				totalRound++;
				break;
			
			case 1:
				if (!tankB1.GetComponent<TankController>().isDead())
				{
					tankB1.SendMessage("Activate");
					timer = 10;
					
					float zCamera = mainCamera.transform.position.z;
					mainCamera.transform.position = new Vector3(tankB1.transform.position.x, tankB1.transform.position.y + 1, zCamera);
					mainCamera.SendMessage("SetFollowedTank", tankB1);
				}				
				else
				{
					/* Skip the player when dead */
					timer = -1;
				}

				break;
			
			case 2:
				if (!tankA2.GetComponent<TankController>().isDead())
				{
					timer = 10;
					tankA2.SendMessage("Activate");
					
					float zCamera = mainCamera.transform.position.z;
					mainCamera.transform.position = new Vector3(tankA2.transform.position.x, tankA2.transform.position.y + 1, zCamera);
					mainCamera.SendMessage("SetFollowedTank", tankA2);
				}
				else
				{
					/* Skip the player when dead */
					timer = -1;
				}

				break;
			
			case 3:
				if (!tankB2.GetComponent<TankController>().isDead())
				{
					timer = 10;
					tankB2.SendMessage("Activate");
					
					float zCamera = mainCamera.transform.position.z;
					mainCamera.transform.position = new Vector3(tankB2.transform.position.x, tankB2.transform.position.y + 1, zCamera);
					mainCamera.SendMessage("SetFollowedTank", tankB2);
				}				
				else
				{
					/* Skip the player when dead */
					timer = -1;
				}
			
			other:
				break;
		}

		isInTransition = false;
		UpdateInfoPanel();

	}
	
	void UpdateInfoPanel()
	{
		txtRound.text = "Round: " + totalRound.ToString();
		
		if (!tankA1.GetComponent<TankController>().isDead())
		{
			txtTankA1.text = "standby";
		}				
		else
		{
			txtTankA1.text = "dead";
		}
		
		if (!tankB1.GetComponent<TankController>().isDead())
		{
			txtTankB1.text = "standby";
		}				
		else
		{
			txtTankB1.text = "dead";
		}
		
		
		if (!tankA2.GetComponent<TankController>().isDead())
		{
			txtTankA2.text = "standby";
		}				
		else
		{
			txtTankA2.text = "dead";
		}
		
		
		if (!tankB2.GetComponent<TankController>().isDead())
		{
			txtTankB2.text = "standby";
		}				
		else
		{
			txtTankB2.text = "dead";
		}
		
//		txtTankB2.text = "Tank B2 standby";
		
		switch (currentPlayer)
		{
			case 0:		
				if (!tankA1.GetComponent<TankController>().isDead())
				{
					txtTankA1.text = "on play";
				}				
				else
				{
					txtTankA1.text = "dead";
				}
				
				break;
			
			case 1:
				if (!tankB1.GetComponent<TankController>().isDead())
				{
					txtTankB1.text = "on play";
				}				
				else
				{
					txtTankB1.text = "dead";
				}
				break;
			
			case 2:
				if (!tankA2.GetComponent<TankController>().isDead())
				{
					txtTankA2.text = "on play";
				}				
				else
				{
					txtTankA2.text = "dead";
				}
				break;
			
			case 3:
				if (!tankB2.GetComponent<TankController>().isDead())
				{
					txtTankB2.text = "on play";
				}				
				else
				{
					txtTankB2.text = "dead";
				}
				break;
			
			other:
				break;
		}
		
		/* Update health info */
		txtTankA1Health.text = tankA1.GetComponent<TankController>().GetHealth().ToString();
		txtTankA2Health.text = tankA2.GetComponent<TankController>().GetHealth().ToString();
		
		txtTankB1Health.text = tankB1.GetComponent<TankController>().GetHealth().ToString();
		txtTankB2Health.text = tankB2.GetComponent<TankController>().GetHealth().ToString();

		/* Update bomb info */
		txtTankA1Bomb.text = tankA1.GetComponent<TankController>().GetBombChance().ToString();
		txtTankA2Bomb.text = tankA2.GetComponent<TankController>().GetBombChance().ToString();		
		
		txtTankB1Bomb.text = tankB1.GetComponent<TankController>().GetBombChance().ToString();
		txtTankB2Bomb.text = tankB2.GetComponent<TankController>().GetBombChance().ToString();		
	}
	
	void UpdateHealth(GameObject tank)
	{
		String hitTank = tank.name;

		Debug.Log("Hit tank = " + hitTank);

		if (hitTank == "TankA1")
		{
			txtTankA1Health.text = tank.GetComponent<TankController>().GetHealth().ToString();
		}
		else if (hitTank == "TankA2")
		{
			txtTankA2Health.text = tank.GetComponent<TankController>().GetHealth().ToString();
		}
		else if (hitTank == "TankB1")
		{
			txtTankB1Health.text = tank.GetComponent<TankController>().GetHealth().ToString();
		}
		else if (hitTank == "TankB2")
		{
			txtTankB2Health.text = tank.GetComponent<TankController>().GetHealth().ToString();
		}
	}

	void UpdateBombChance(GameObject tank)
	{
		String firedByTank = tank.name;

		if (firedByTank == "TankA1")
		{
			txtTankA1Bomb.text = tank.GetComponent<TankController>().GetBombChance().ToString();
		}
		else if (firedByTank == "TankA2")
		{
			txtTankA2Bomb.text = tank.GetComponent<TankController>().GetBombChance().ToString();
		}
		else if (firedByTank == "TankB1")
		{
			txtTankB1Bomb.text = tank.GetComponent<TankController>().GetBombChance().ToString();
		}
		else if (firedByTank == "TankB2")
		{
			txtTankB2Bomb.text = tank.GetComponent<TankController>().GetBombChance().ToString();
		}

	}

	void UpdateDealthInfo(GameObject tank)
	{
		String deadTank = tank.name;

		if (deadTank == "TankA1")
		{
			txtTankA1Health.text = "dead";
		}
		else if (deadTank == "TankA2")
		{
			txtTankA2Health.text = "dead";
		}
		else if (deadTank == "TankB1")
		{
			txtTankB1Health.text = "dead";
		}
		else if (deadTank == "TankB2")
		{
			txtTankB2Health.text = "dead";
		}		
	}
	
	void AddTime()
	{
		timer += 5;
		getTimer = true;
	}
	
	
	void AddShield()
	{
		getShield = true;
	}

	void AddMedicalKit()
	{
		getMedicalKit = true;
	}
	
	void UpdatePauseStatus()
	{
		tankA1.SendMessage("UpdatePauseStatus");
		tankA2.SendMessage("UpdatePauseStatus");
		tankB1.SendMessage("UpdatePauseStatus");
		tankB2.SendMessage("UpdatePauseStatus");
	}

}
