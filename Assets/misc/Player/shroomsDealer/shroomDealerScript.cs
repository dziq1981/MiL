using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shroomDealerScript : MonoBehaviour {


    PlayerControls player;
    bool dying;
    float timeOfDeath;
    SpriteRenderer sprite;
	// Use this for initialization
	void Start () {
        player = FindObjectOfType<PlayerControls>();
        sprite = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        if (dying)
        {
            float t = (Time.time - timeOfDeath) / 2;
            float minimum = 1;
            float maximum = 0;
            sprite.color = new Color(1f, 1f, 1f, Mathf.SmoothStep(minimum, maximum, t));
        }
    }

    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        player.shrooming = true;
    }*/

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.GetComponent<PlayerControls>().shrooming = true;
        //player.shrooming = true;
        Debug.Log("Shrrooming");
        Destroy(gameObject,2);
        dying = true;
        GetComponent<PolygonCollider2D>().enabled = false;
        timeOfDeath = Time.time;

    }
}
