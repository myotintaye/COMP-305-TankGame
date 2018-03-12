using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public Transform tankPosition;
	public Transform tankMoveThreshold;

	// Use this for initialization
	void Start ()
	{
		tankMoveThreshold = transform.GetChild(0);
	}
	
	// Update is called once per frame
	void Update ()
	{
		this.transform.position = new Vector3(tankPosition.position.x, transform.position.y, transform.position.z);
	}

	// Pre-defined Unity function for frawing Gizmos in the editor
	private void OnDrawGizmosSelected()
	{
//		throw new System.NotImplementedException();

		Gizmos.color = Color.cyan;
		Gizmos.DrawLine(tankMoveThreshold.position,
			new Vector3(tankMoveThreshold.position.x, tankMoveThreshold.position.y + 30, tankMoveThreshold.position.z));
		Gizmos.DrawLine(tankMoveThreshold.position,
			new Vector3(tankMoveThreshold.position.x, tankMoveThreshold.position.y - 30, tankMoveThreshold.position.z));
	}
}
