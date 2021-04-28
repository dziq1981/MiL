using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collect : MonoBehaviour {
    public AudioClip pling;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {		
	AudioSource.PlayClipAtPoint(pling, FindObjectOfType<Camera>().transform.position);
	FindObjectOfType<hudManager>().score += 10;
        Destroy(gameObject);
    }

    
}
