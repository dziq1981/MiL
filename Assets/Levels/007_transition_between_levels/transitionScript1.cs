using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class transitionScript1 : MonoBehaviour
{
    SpriteRenderer[] playerSprites;
    List<GameObject> players;
    float timeMarker;
    // Start is called before the first frame update
    void Start()
    {
        playerSprites = FindObjectsOfType<SpriteRenderer>();
        players = new List<GameObject>();
        FindObjectOfType<CameraFollowsPlayer>().enabled = false;
        foreach (var pS in playerSprites)
        {
            if (pS.tag == "Player") players.Add(pS.gameObject);
        }
        foreach (var gO in FindObjectOfType<Camera>().GetComponentsInChildren<SpriteRenderer>())
        {
            gO.enabled = false;
        }
        foreach (var gO in FindObjectsOfType<PlayerControls>())
        {
            gO.gameObject.SetActive(false);
        }
        FindObjectOfType<Canvas>().enabled = false;
        timeMarker = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        foreach(var p in playerSprites)
        {
            /*if (p.tag=="Player") p.transform.Rotate(new Vector3(0,0,1), 750f*Time.deltaTime);
            else
            {*/
                p.transform.Rotate(new Vector3(0, 0, 1), -750f / (p.transform.position.z*2f) * Time.deltaTime);
            //}
        }
        if (Time.timeSinceLevelLoad>3f)
        {
            FindObjectOfType<LevelManager>().LoadNextLevel();
        }
    }
}
