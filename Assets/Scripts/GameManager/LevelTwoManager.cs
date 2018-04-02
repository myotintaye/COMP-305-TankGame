using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelTwoManager : MonoBehaviour
{

	public float timer = 10f;
	
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

	// Use this for initialization
	void Start ()
	{
		totalRound = 1;
		
		currentPlayer = 0; 

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
	}
	
	
//    Swtich player, tank1 = 0, tank2 = 1
	void SwitchPlayer()
	{
		
		if (currentPlayer == 1)
		{
			tank1.SendMessage("Activate");
			tank2.SendMessage("Deactivate");
			
			/* Update round information */
			totalRound++;
		}
		else
		{
			tank1.SendMessage("Deactivate");
			tank2.SendMessage("Activate");		
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
}
