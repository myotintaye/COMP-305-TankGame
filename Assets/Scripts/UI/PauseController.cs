using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif



public class PauseController : MonoBehaviour {
	
	private Canvas canvas;

	public Canvas help;
	public GameObject gameManager;

	// Use this for initialization
	void Start () {
		canvas = GetComponent<Canvas>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape) && help.enabled == false)
		{
			Pause();
		}
	}
	
	public void Pause()
	{
		// Toggle pause menu
		canvas.enabled = !canvas.enabled;
		
		Time.timeScale = Time.timeScale == 0 ? 1 : 0; // Toggle 
		
		/* To block spacebar key during pause */
		gameManager.SendMessage("UpdatePauseStatus");
	}

	public void BackToHome()
	{
		Pause();
		SceneManager.LoadScene("Initial");
	}
	
	public void ViewInstructions()
	{
		help.enabled = !help.enabled;
		canvas.enabled = !canvas.enabled;
	}
	
}
