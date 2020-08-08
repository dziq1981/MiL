using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowsPlayer : MonoBehaviour {
    private const float initialPosition = 0f;

	// Use this for initialization
	void Start () {
        foreach (var background in GetComponentsInChildren<SpriteRenderer>())
        {
            background.enabled = true;
        }
	}
	
	// Update is called once per frame
	void Update () {
        float oldX = transform.position.x;
        float camPositionX = PlayerManager.me.Player1.transform.position.x;
        if (PlayerManager.me.Player2spawned)
        {
            camPositionX += PlayerManager.me.Player2.transform.position.x;
            camPositionX /= 2;
        }
        if (camPositionX>initialPosition)
        {
            transform.position = new Vector3(camPositionX - initialPosition, 0,-10);
        }
        else
        {
            transform.position = new Vector3(0, 0, -10);
        }
        foreach (var background in GetComponentsInChildren<SpriteRenderer>())
        {            
            if (background.transform.position.z > 95) continue;
            background.transform.position += new Vector3((oldX-transform.position.x)/background.transform.position.z,0,0);
        }
    }
}
