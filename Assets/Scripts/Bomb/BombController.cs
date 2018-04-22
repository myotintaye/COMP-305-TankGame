using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using Random = UnityEngine.Random;

public class BombController : MonoBehaviour {

    public GameObject explosionPrefab;
	private Renderer _renderer;
	private PolygonCollider2D _collider2D;
	
	private AudioSource asShoot;
	public AudioClip acMapExplosion;
	public AudioClip acBoxExplosion;
	
	public GameObject timerPrefab;
	public GameObject shieldPrefab;
	public GameObject fuelPrefab;	
	public GameObject extraBombPrefab;

	// Use this for initialization
	void Start ()
	{
		_renderer = GetComponent<Renderer>();
		_collider2D = GetComponent<PolygonCollider2D>();
		
		asShoot = GetComponent<AudioSource>();
		asShoot.Play();
	}
	
	// Update is called once per frame
	void Update ()
	{
	}
		
	void OnCollisionEnter2D(Collision2D col)
    {
        SpawnExplosion();
        GetComponent<TrailRenderer>().enabled = false;

	    switch (col.gameObject.tag)
	    {
		    case "Box":
			    generateObject(col.gameObject.transform);
			    Destroy(col.gameObject);
			    
			    asShoot.clip = acBoxExplosion;
			    bombExplosion();
			    
			    break;
		    
		    case "Map":
	        
			    asShoot.clip = acMapExplosion;
			    bombExplosion();

			    break;
		    
		    case "Player":
			    asShoot.clip = acBoxExplosion;
			    bombExplosion();
			    
			    break;
		    
		    case "Timer":
			    asShoot.clip = acBoxExplosion;
			    bombExplosion();
			    
			    Destroy(col.gameObject);
			    
			    break;
		    
		    case "Fuel":
			    asShoot.clip = acBoxExplosion;
			    bombExplosion();
			    
			    Destroy(col.gameObject);
			    
			    break;

		    case "Shield":
			    
			    asShoot.clip = acBoxExplosion;
			    bombExplosion();
			    
			    Destroy(col.gameObject);
			    
			    break;
		    
		    case "ExtraBomb":
			    asShoot.clip = acBoxExplosion;
			    bombExplosion();
			    
			    Destroy(col.gameObject);
			    
			    break;		 
		    
//		    case "ShieldProtection":
//			    
//			    Debug.Log("Enter shield protection collision");
//			    
//			    asShoot.clip = acBoxExplosion;
//			    bombExplosion();
//			    
//			    col.gameObject.SetActive(false);
////			    Destroy(col.gameObject);			
//			    
////			    if (col.gameObject.GetComponentInParent(typeof(GameObject)).name !=
////			        gameObject.GetComponentInParent(typeof(GameObject)).name)
////			    {
////				    asShoot.clip = acBoxExplosion;
////				    bombExplosion();
////			    
////				    Destroy(col.gameObject);			    
////			    }
//			    
//			    break;		
		    
		    default:
			    break;
	    }


    }

	private void bombExplosion()
	{
		_renderer.enabled = false;
		_collider2D.enabled = false;
		
		asShoot.Play();
	        
		Destroy(gameObject, asShoot.clip.length);
	}

    private void SpawnExplosion()
    {
        var explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity) as GameObject;
        Destroy(explosion, 2f);
    }

	private void generateObject(Transform transform)
	{
		int num = Random.Range(0, 4);

		switch (num)
		{
			
			case 0:
				GameObject timer = GameObject.Instantiate(timerPrefab, transform.position, Quaternion.identity) as GameObject;
				break;
			
			case 1:
				GameObject shield = GameObject.Instantiate(shieldPrefab, transform.position, Quaternion.identity) as GameObject;
				break;
			
			case 2:
				GameObject fuel = GameObject.Instantiate(fuelPrefab, transform.position, Quaternion.identity) as GameObject;
				break;
				
			case 3:
				GameObject extraBomb = GameObject.Instantiate(extraBombPrefab, transform.position, Quaternion.identity) as GameObject;
				break;
			
			default:
				break;
		}
		
		
	}

	
}
