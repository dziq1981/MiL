using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class batterflyMultiPoint : MonoBehaviour
{
    private bool dying;
    private float timeOfDeath;
    private SpriteRenderer sprite;
    public Vector2 moveTo;
    public Vector2[] relativePointsList;
    private List<float> timing;
    public float timeToMove;
    public Vector2 moveFrom;
    private Vector2 startFrom;
    private float timeStamp;
    private bool takeSnapshot = true;
    private int pointNumber;
    private Color origColor;
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        origColor = sprite.color;
        pointNumber = 0;
        startFrom = transform.position;
        moveFrom = startFrom;
        moveTo = startFrom + relativePointsList[0];
        Vector2 lastPoint = Vector2.zero;
        float totalPathLength = 0;
        List<float> lengths = new List<float>();
        
        foreach (var point in relativePointsList)
        {
            float magnitude = (lastPoint-point).magnitude;
            Debug.Log($"Vector length = {magnitude}");
            lengths.Add(magnitude);
            totalPathLength += magnitude;
            lastPoint = point;
        }
        timing = new List<float>();
        foreach (var length in lengths)
        {
            float item = timeToMove * length / totalPathLength; 
            Debug.Log($"timing = {item}");
            timing.Add(item);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (dying)
        {
            float t = (Time.time - timeOfDeath) / 2;
            sprite.color = new Color(origColor.r, origColor.g, origColor.b, Mathf.SmoothStep(1, 0, t));
            transform.Rotate(0, 0, Time.deltaTime * 360);            
        }
        if (takeSnapshot)
        {
            timeStamp = Time.time;
            takeSnapshot = false;
        }        
            moveTheBat(moveFrom, moveTo);        
    }

    private void moveTheBat(Vector2 from, Vector2 to)
    {
        if (timing[pointNumber] == 0)
        {
            if(pointNumber < relativePointsList.Length - 1) pointNumber++; else pointNumber = 0;
        }
        float tB = (Time.time - timeStamp) / timing[pointNumber];
        float x = Mathf.SmoothStep(from.x, to.x, tB);
        float y = Mathf.SmoothStep(from.y, to.y, tB);
        transform.position = new Vector2(x, y);
        if (Mathf.Abs(x - to.x) < 0.1 && Mathf.Abs(y - to.y) < 0.1)
        {
            if (pointNumber < relativePointsList.Length - 1) pointNumber++; else pointNumber = 0;
            
            moveFrom = moveTo;
            moveTo =  startFrom - relativePointsList[pointNumber];
            takeSnapshot = true;

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "weaponsRange")
        {
            die();
            return;
        }
        if (collision.tag == "Player")
        {

            collision.gameObject?.GetComponent<PlayerControls>()?.playerDying();
        }

    }

    void die()
    {
        GetComponent<PolygonCollider2D>().enabled = false;
        dying = true;
        timeOfDeath = Time.time;
        FindObjectOfType<hudManager>().score += 40;
        GameObject.Destroy(gameObject, 2);
        GetComponent<Animator>().StopPlayback();

    }
}
