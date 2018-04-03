using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

	private Transform tankPosition;

	// Use this for initialization
	void Start()
	{
	}

	// Update is called once per frame
	void Update()
	{
		this.transform.position = new Vector3(tankPosition.position.x, transform.position.y, transform.position.z);
	}

	public void SetFollowedTank(GameObject tank)
	{
		tankPosition = tank.transform;
	}
	
}
