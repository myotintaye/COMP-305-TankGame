using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelTwoManager : MonoBehaviour
{

	public float timer = 10f;
	
	public Camera mainCamera;
	
	public int totalRound;
	public int currentPlayer;

	public GameObject tank1;
	public GameObject tank2;
	
	public Text txtTankA;
	public Text txtTankAHealth;
	public Text txtTankABomb;
	
	public Text txtTankB;
	public Text txtTankBHealth;
	public Text txtTankBBomb;
	
	public Text txtRound;
	public Text txtTimeLeft;
	
	public Text txtWinningMessage;	
	public GameObject gameOverPanel;

	// Use this for initialization
	void Start ()
	{
		totalRound = 1;
		
		currentPlayer = 0; 
			
		float zCamera = mainCamera.transform.position.z;
		mainCamera.transform.position = new Vector3(tank1.transform.position.x, tank1.transform.position.y, zCamera);
		
		mainCamera.SendMessage("SetFollowedTank", tank1);


		tank1.SendMessage("Activate");
		tank2.SendMessage("Deactivate");
		tank2.SendMessage("SetTankDirectionToLeft");
		
		UpdateInfoPanel();
	}
	
	// Update is called once per frame
	void Update ()
	{
		timer -= Time.deltaTime;

		if (timer <= 0)
		{
			SwitchPlayer();
			timer = 10;
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
		if (tank1.GetComponent<TankController>().isDead())
		{
			txtWinningMessage.text = "Tank B wins the game!";
			return true;
		}
		else if (tank2.GetComponent<TankController>().isDead())
		{
			txtWinningMessage.text = "Tank A wins the game!";
			return true;
		}
		else
		{
			return false;
		}
	}
	
	
//    Swtich player, tank1 = 0, tank2 = 1
	void SwitchPlayer()
	{
		
		if (currentPlayer == 1)
		{
			tank1.SendMessage("Activate");
			tank2.SendMessage("Deactivate");
			
			float zCamera = mainCamera.transform.position.z;
			mainCamera.transform.position = new Vector3(tank1.transform.position.x, tank1.transform.position.y, zCamera);
			mainCamera.SendMessage("SetFollowedTank", tank1);
			
			/* Update round information */
			totalRound++;
		}
		else
		{
			tank1.SendMessage("Deactivate");
			tank2.SendMessage("Activate");		
			
			float zCamera = mainCamera.transform.position.z;
			mainCamera.transform.position = new Vector3(tank2.transform.position.x, tank2.transform.position.y, zCamera);
			mainCamera.SendMessage("SetFollowedTank", tank2);

		}

		/* Reset current player */
		currentPlayer = (currentPlayer + 1) % 2;
		
		UpdateInfoPanel();

	}
	
	void UpdateInfoPanel()
	{
		txtRound.text = "Round: " + totalRound.ToString();

		if (currentPlayer == 0)
		{
			txtTankA.text = "Tank A is playing";
			txtTankB.text = "Tank B is waiting";
		}
		else
		{
			txtTankA.text = "Tank B is waiting";
			txtTankB.text = "Tank B is playing";
		}
		
		txtTankAHealth.text = "Health: " + tank1.GetComponent<TankController>().GetHealth().ToString();
		txtTankBHealth.text = "Health: " + tank2.GetComponent<TankController>().GetHealth().ToString();
 		txtTankABomb.text = "Bomb: " + tank1.GetComponent<TankController>().GetBombChance().ToString();
		txtTankBBomb.text = "Bomb: " + tank2.GetComponent<TankController>().GetBombChance().ToString();
		
	}

	void UpdateHealth(GameObject tank)
	{
		String hitTank = tank.name;

		Debug.Log("Hit tank = " + hitTank);

		if (hitTank == "Tank1")
		{
			txtTankAHealth.text = "Health: " + tank.GetComponent<TankController>().GetHealth().ToString();
		}
		else
		{
			txtTankBHealth.text = "Health: " + tank.GetComponent<TankController>().GetHealth().ToString();
		}
	}

	void UpdateBombChance(GameObject tank)
	{
		String firedByTank = tank.name;

		Debug.Log("Fired by tank = " + firedByTank);

		if (firedByTank == "Tank1")
		{
			txtTankABomb.text = "Bomb: " + tank.GetComponent<TankController>().GetBombChance().ToString();
		}
		else
		{
			txtTankBBomb.text = "Bomb: " + tank.GetComponent<TankController>().GetBombChance().ToString();
		}

	}
	
	void UpdateDealthInfo(GameObject tank)
	{
		String deadTank = tank.name;

		if (deadTank == "Tank1")
		{
			txtTankAHealth.text = "Tank A is dead";
		}
		else if (deadTank == "Tank2")
		{
			txtTankBHealth.text = "Tank B is dead";
		}
	
	}

}
