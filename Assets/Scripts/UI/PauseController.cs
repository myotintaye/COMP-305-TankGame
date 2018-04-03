using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif


public class PauseController : MonoBehaviour {
	
	private Canvas canvas;

	// Use this for initialization
	void Start () {
		canvas = GetComponent<Canvas>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Pause();
		}
	}
	
	
	public void Pause()
	{
		// Toggle pause menu
		canvas.enabled = !canvas.enabled;
		
		Time.timeScale = Time.timeScale == 0 ? 1 : 0; // Toggle 
	}

	public void BackToHome()
	{
		
	}
	
}
