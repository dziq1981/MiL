using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class fox : MonoBehaviour {

    private const string attackTrigger = "attackTrigger";
    PlayerControls[] players;
    Animator animator;
    public float walkingSpeed = 1f;
    public float walkingMinX = -2f;
    public float walkingMaxX = 3f;

    public float foxY = 0;
    private float normalFoxY;

    public bool isAttacking = false;
    private bool wasAttacking = false;
    private float initialX;
    public enum Direction
    {
        left=-1, right=1
    }

    public Direction direction;
    private bool dying = false;
    private float timeOfDeath;
    private SpriteRenderer sprite;




	// Use this for initialization
	void Start () {
        players = FindObjectsOfType<PlayerControls>();
        animator = GetComponent<Animator>();
        initialX = transform.position.x;
        normalFoxY = transform.position.y;
        sprite = GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
        float foxSpeed = walkingSpeed;
        foreach (PlayerControls player in players)
        {
            if (direction == Direction.left)
            {
                if ((player.transform.position.x < transform.position.x) && ((transform.position.x - player.transform.position.x) < 1f))
                {
                    jump();             
                }
            }
            else
            {
                if ((player.transform.position.x > (transform.position.x)) && ((player.transform.position.x- transform.position.x) < 1f))
                {
                    jump();
                }                
            }
        }
        if (isAttacking) foxSpeed = 0;
        if (wasAttacking && !isAttacking) animator.ResetTrigger(attackTrigger);
        wasAttacking = isAttacking;
        transform.position += new Vector3(Time.deltaTime * ((direction == Direction.left) ? -1 :1 ) * foxSpeed, 0, 0);
        if (transform.position.x - initialX < walkingMinX)
        {
            direction = Direction.right;
            GetComponent<SpriteRenderer>().flipX = true;
        }
        if (transform.position.x - initialX > walkingMaxX)
        {
            direction = Direction.left;
            GetComponent<SpriteRenderer>().flipX = false;
        }
        if (dying)
        {
            float t = (Time.time - timeOfDeath) / 2;
            float minimum = 1;
            float maximum = 0;
            sprite.color = new Color(1f, 1f, 1f, Mathf.SmoothStep(minimum, maximum, t));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag=="weaponsRange")
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
        walkingSpeed = 0;
        GetComponent<PolygonCollider2D>().enabled = false;
        animator.SetBool("foxHit", true);
        dying = true;
        timeOfDeath = Time.time;
        FindObjectOfType<hudManager>().score += 25;
        GameObject.Destroy(gameObject,2);
        
    }
    void jump()
    {
        //GetComponent<Rigidbody2D>().velocity += new Vector2(0, 2f);
        animator.SetTrigger(attackTrigger);
    }
}

