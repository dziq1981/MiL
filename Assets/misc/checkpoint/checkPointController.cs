using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkPointController : MonoBehaviour
{

    bool beginTransition = false;
    bool transitioning = false;
    public GameObject activeStone;
    public GameObject inactiveStone;
    float timeStart;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (beginTransition)
        {
            timeStart = Time.time;
            transitioning = true;
            beginTransition = false;            
        }
        if (transitioning)
        {
            float t = (Time.time - timeStart) / 0.5f;
            activeStone.GetComponent<SpriteRenderer>().color = new Color(1,1,1,Mathf.SmoothStep(0, 1, t));
            inactiveStone.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, Mathf.SmoothStep(1,0, t));
        }
        if (Time.time - timeStart>0.5)
        {
            transitioning = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        FindObjectOfType<PlayerControls>().playerSpawnPosition = transform.position + new Vector3(0, 2, 0);
        GetComponent<PolygonCollider2D>().enabled = false;
        beginTransition = true;
    }
}
