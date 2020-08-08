using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class batterfly : MonoBehaviour
{
    private bool dying;
    private float timeOfDeath;
    private SpriteRenderer sprite;
    public Vector2 moveTo;
    public float timeToMove;
    private Vector2 moveFrom;
    private float timeStamp;

    private bool takeSnapshot = true;
    private bool goingBack = false;
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        moveFrom = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (dying)
        {
            float t = (Time.time - timeOfDeath) / 2;
            sprite.color = new Color(1f, 1f, 1f, Mathf.SmoothStep(1, 0, t));
            transform.Rotate(0, 0, Time.deltaTime * 360);            
        }
        if (takeSnapshot)
        {
            timeStamp = Time.time;
            takeSnapshot = false;
        }
        if (goingBack)
        {
            moveTheBat(moveTo, moveFrom);
        }
        else
        {
            moveTheBat(moveFrom, moveTo);
        }

    }

    private void moveTheBat(Vector2 from, Vector2 to)
    {
        float tB = (Time.time - timeStamp) / timeToMove;
        float x = Mathf.SmoothStep(from.x, to.x, tB);
        float y = Mathf.SmoothStep(from.y, to.y, tB);
        transform.position = new Vector2(x, y);
        if (Mathf.Abs(x - to.x) < 0.1 && Mathf.Abs(y - to.y) < 0.1)
        {
            goingBack = !goingBack;
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
            //PlayerControls pc = FindObjectOfType<PlayerControls>();
            //pc.playerDying();
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
