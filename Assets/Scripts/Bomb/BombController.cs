using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class BombController : MonoBehaviour {

    public GameObject explosionPrefab;
	private Renderer _renderer;
	private PolygonCollider2D _collider2D;
	
	private AudioSource asShoot;
	public AudioClip acMapExplosion;
	public AudioClip acBoxExplosion;

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

        if (col.gameObject.CompareTag("Box"))
        {
            Debug.Log("Hit box");
	        
	        _renderer.enabled = false;
	        _collider2D.enabled = false;

	        asShoot.clip = acBoxExplosion;
	        asShoot.Play();

	        
	        Destroy(gameObject, asShoot.clip.length);
            Destroy(col.gameObject);
	        
        }
        else if (col.gameObject.name == "Map")
        {
	        Debug.Log("Hit map");
	        
	        _renderer.enabled = false;
	        _collider2D.enabled = false;
	        
	        asShoot.clip = acMapExplosion;
	        asShoot.Play();
	        
	        Destroy(gameObject, asShoot.clip.length);
        }

        /* Avoid hit to be counted multiple times */
        if (col.gameObject.CompareTag("Player"))
        {
	        _renderer.enabled = false;
	        _collider2D.enabled = false;
	        
	        asShoot.clip = acBoxExplosion;
	        asShoot.Play();
	        
	        Destroy(gameObject, asShoot.clip.length);
        }

    }

    private void SpawnExplosion()
    {
        var explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity) as GameObject;
        Destroy(explosion, 2f);
    }
}
