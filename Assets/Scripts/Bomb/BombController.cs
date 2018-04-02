using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class BombController : MonoBehaviour {

    public GameObject explosionPrefab;
	private Rigidbody2D rBody;

	// Use this for initialization
	void Start () {
		rBody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update ()
	{
//		Debug.Log("Bomb Position = " + gameObject.transform.position);
//		if (gameObject.transform.position.y < -10)
//		{
//			Destroy(gameObject);
//		}
	}
		
	void OnCollisionEnter2D(Collision2D col)
    {
        SpawnExplosion();

        if (col.gameObject.name == "Box")
        {
            Debug.Log("Hit box");
            Destroy(gameObject);
            Destroy(col.gameObject);
        }
        else if (col.gameObject.name == "Map")
        {
            Destroy(gameObject);
        }

        /* Avoid hit to be counted multiple times */
        if (col.gameObject.CompareTag("Player"))
        {

            Destroy(gameObject);
        }

    }

    private void SpawnExplosion()
    {
        var explosion = Instantiate(explosionPrefab,transform.position, Quaternion.identity) as GameObject;
        Destroy(explosion, 2f);
    }
}
