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

	// Use this for initialization
	void Start () {
		totalRound = 1;
		
		currentPlayer = 0; 
			
		float zCamera = mainCamera.transform.position.z;
		mainCamera.transform.position = new Vector3(tankA1.transform.position.x, tankA1.transform.position.y, zCamera);
		
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
			SwitchPlayer();
		}
		
		txtTimeLeft.text = "Time: " + Mathf.FloorToInt(timer).ToString();

		if (checkWinning())
		{
			/* Game over */
			gameOverPanel.SetActive(true);
		};
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
					mainCamera.transform.position = new Vector3(tankA1.transform.position.x, tankA1.transform.position.y, zCamera);
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
					mainCamera.transform.position = new Vector3(tankB1.transform.position.x, tankB1.transform.position.y, zCamera);
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
					mainCamera.transform.position = new Vector3(tankA2.transform.position.x, tankA2.transform.position.y, zCamera);
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
					mainCamera.transform.position = new Vector3(tankB2.transform.position.x, tankB2.transform.position.y, zCamera);
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

		
		UpdateInfoPanel();

	}
	
	void UpdateInfoPanel()
	{
		txtRound.text = "Round: " + totalRound.ToString();
		
		if (!tankA1.GetComponent<TankController>().isDead())
		{
			txtTankA1.text = "Tank A1 standby";
		}				
		else
		{
			txtTankA1.text = "Tank A1 is dead";
		}
		
		if (!tankB1.GetComponent<TankController>().isDead())
		{
			txtTankB1.text = "Tank B1 standby";
		}				
		else
		{
			txtTankB1.text = "Tank B1 is dead";
		}
		
		
		if (!tankA2.GetComponent<TankController>().isDead())
		{
			txtTankA2.text = "Tank A2 standby";
		}				
		else
		{
			txtTankA2.text = "Tank A2 is dead";
		}
		
		
		if (!tankB2.GetComponent<TankController>().isDead())
		{
			txtTankB2.text = "Tank B2 standby";
		}				
		else
		{
			txtTankB2.text = "Tank B2 is dead";
		}
		
		txtTankB2.text = "Tank B2 stand by";
		
		switch (currentPlayer)
		{
			case 0:		
				if (!tankA1.GetComponent<TankController>().isDead())
				{
					txtTankA1.text = "Tank A1 is playing";
				}				
				else
				{
					txtTankA1.text = "Tank A1 is dead";
				}
				
				break;
			
			case 1:
				if (!tankB1.GetComponent<TankController>().isDead())
				{
					txtTankB1.text = "Tank B1 is playing";
				}				
				else
				{
					txtTankB1.text = "Tank B1 is dead";
				}
				break;
			
			case 2:
				if (!tankA2.GetComponent<TankController>().isDead())
				{
					txtTankA2.text = "Tank A2 is playing";
				}				
				else
				{
					txtTankA2.text = "Tank A2 is dead";
				}
				break;
			
			case 3:
				if (!tankB2.GetComponent<TankController>().isDead())
				{
					txtTankB2.text = "Tank B2 is playing";
				}				
				else
				{
					txtTankB2.text = "Tank B2 is dead";
				}
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

	void UpdateDealthInfo(GameObject tank)
	{
		String deadTank = tank.name;

		if (deadTank == "TankA1")
		{
			txtTankA1Health.text = "Tank A1 is dead";
		}
		else if (deadTank == "TankA2")
		{
			txtTankA2Health.text = "Tank A2 is dead";
		}
		else if (deadTank == "TankB1")
		{
			txtTankB1Health.text = "Tank B1 is dead";
		}
		else if (deadTank == "TankB2")
		{
			txtTankB2Health.text = "Tank B2 is dead";
		}		
	}
	

}
