using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour {

    bool flagJumped = false;
    bool lastFlip = false;
    bool dying = false;
    bool onShrooms = false;
    public bool shrooming = false;

    bool shroomingStage1 = false;
    bool shroomingStage2 = false;
    bool shroomingStage3 = false;
    bool shroomingStage4 = false;

    float jumpCountDown;

    Vector3 holdPosition;
    public Vector3 playerSpawnPosition;

    float immortal = 0;

    float shroomTime;

    public bool attacking = false;
    Animator animator;
    Rigidbody2D rb2;    
    PolygonCollider2D weaponsRangeCollider;
    SpriteRenderer sprite;

    public string jump;
    public string horizontal;
    public string fire1;
    public GameObject otherPlayer;
    //public PlayerManager playerManager;
    // Use this for initialization
    void Start()
    {
        setup();
        jump = "Jump";
        horizontal = "Horizontal";
        fire1 = "Fire1";
        otherPlayer = getOtherPlayer();
}

    public virtual GameObject getOtherPlayer()
    {
        return PlayerManager.me.Player2;
    }

    public void setup()
    {
        rb2 = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        weaponsRangeCollider = transform.GetChild(0).gameObject.GetComponent<PolygonCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        transform.position = playerSpawnPosition;
        //playerManager = FindObjectOfType<PlayerManager>();        
    }

    private void flipCollider()
    {
        PolygonCollider2D polygonCollider2D = GetComponent<PolygonCollider2D>();
        List<Vector2> newPoints = new List<Vector2>();
        foreach (var point in polygonCollider2D.points)
        {
            newPoints.Add(new Vector2(-point.x, point.y));
        }
        polygonCollider2D.points = newPoints.ToArray();        
        newPoints = new List<Vector2>();
        foreach (var point in weaponsRangeCollider.points)
        {
            newPoints.Add(new Vector2(-point.x, point.y));
        }
        weaponsRangeCollider.points = newPoints.ToArray();
    }

    // Update is called once per frame
    void Update()
    {
        if (otherPlayer == null) otherPlayer = getOtherPlayer();
        if (!FindObjectOfType<curtainAppear>().isLight) return;
        flagJumped = checkIfInAir();
        if (!dying)
        {
            control();
        }
        attacking = animator.GetBool("attack");
        if (transform.position.y <= -5)
        {
            playerDead();
        }
        animator.SetBool("air",checkIfInAir());
        weaponsRangeCollider.enabled = attacking;
        shroomEffect();
        if (PlayerManager.me.Player2spawned && otherPlayer!=null)
        {
            playerSpawnPosition = new Vector3(otherPlayer.transform.position.x, 4, otherPlayer.transform.position.z);
        }
    }

    private bool checkIfInAir()
    {
        ContactPoint2D[] contacts = new ContactPoint2D[10];
        int contactCount = GetComponent<PolygonCollider2D>().GetContacts(contacts);
        for (int i=0; i<10;i++)
        {
            if (contacts[i].normal.y > 0.65f)
            {
                jumpCountDown = Time.time;
                return false;
            }

        }
        if (Time.time - jumpCountDown < 0.2f)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private void shroomEffect()
    {
        if (shrooming)
        {
            shroomingStage1 = true;
            shroomTime = Time.time;
            shroomingStage1 = true;
            shroomingStage2 = false;
            shroomingStage3 = false;
            shroomingStage4 = false;
            shrooming = false;
            holdPosition = transform.position;
            onShrooms = true;
            GetComponent<PolygonCollider2D>().enabled = false;
        }
        if (shroomingStage1)
        {
            fluidRescaleX(1, 3, 1);
            if (transform.localScale.x == 3)
            {
                shroomingStage1 = false;
                shroomingStage2 = true;
                shroomTime = Time.time;
            }
        }
        if (shroomingStage2)
        {
            fluidRescaleX(3, 2, 1);
            if (transform.localScale.x == 2)
            {
                shroomingStage2 = false;
                shroomingStage3 = true;
                shroomTime = Time.time;
            }
        }
        if (shroomingStage3)
        {
            fluidRescaleY(1, 4, 2);
            if (transform.localScale.y == 4)
            {
                shroomingStage3 = false;
                shroomingStage4 = true;
                shroomTime = Time.time;
            }
        }
        if (shroomingStage4)
        {
            fluidRescaleY(4, 2, 2);
            if (transform.localScale.y == 2)
            {
                shroomingStage4 = false;
                GetComponent<PolygonCollider2D>().enabled = true;
            }
        }
        if (shroomingStage1 || shroomingStage2 || shroomingStage3 || shroomingStage4)
            transform.position = holdPosition + new Vector3(0,0.5f,0);
    }

    private void fluidRescaleY(float fromValue, float toValue, float xScale)
    {
        float t = (Time.time - shroomTime) / 0.25f;        
        transform.localScale = new Vector2(xScale, Mathf.SmoothStep(fromValue, toValue, t));
    }
    private void fluidRescaleX(float fromValue, float toValue, float yScale)
    {
        float t = (Time.time - shroomTime) / 0.25f;
        transform.localScale = new Vector2(Mathf.SmoothStep(fromValue, toValue, t), yScale);
    }

    private void control()
    {
        if (Input.GetButtonDown(jump) && !flagJumped)
        {
            rb2.velocity = new Vector2(0, 7 * (onShrooms ? 1.5f : 1));
        }
        float horizontalInputAxis = Input.GetAxisRaw(horizontal);
        if (horizontalInputAxis != 0)
        {
            animator.SetBool("walk", true);
            transform.position += new Vector3(6f * horizontalInputAxis * Time.deltaTime* (onShrooms ? 1.5f : 1), 0, 0);
            GetComponentInChildren<SpriteRenderer>().flipX = horizontalInputAxis < 0;
            if (horizontalInputAxis < 0 != lastFlip)
            {
                flipCollider();
                lastFlip = !lastFlip;
            }
        }
        else
        {
            animator.SetBool("walk", false);
        }

        if (Input.GetButtonDown(fire1))
        {
            animator.SetBool("attack", true);
        }
        else
        {
            animator.SetBool("attack", false);
        }
        if (immortal >= 0)
        {
            immortal -= Time.deltaTime;
            sprite.enabled = !sprite.enabled;
        }
        else
        {
            sprite.enabled = true;
        }
    }

    public void playerDying()
    {
        if (onShrooms)
        {
            rb2.velocity = new Vector2(GetComponentInChildren<SpriteRenderer>().flipX ? 1 : -1, 1);
            transform.localScale = Vector2.one;
            onShrooms = false;
            immortal = 1;
            return;
        }
        if (immortal > 0) return;
        dying = true;
        GetComponent<PolygonCollider2D>().isTrigger = true;
        immortal = 1;
        rb2.velocity = new Vector2(-2, 1);
        GetComponent<SpriteRenderer>().sortingOrder = 3;
    }

    private void playerDead()
    {
        if (FindObjectOfType<canvasKnighManager>().takeLive() < 0)
        {
            Debug.Log("GameOver");
            FindObjectOfType<curtainAppear>().getDark = true;
        }
        else
        {
            GetComponent<PolygonCollider2D>().isTrigger = false;
            transform.position = playerSpawnPosition;
            rb2.velocity = Vector2.zero;
            rb2.rotation = 0;
            transform.localEulerAngles = Vector3.zero;
            dying = false;
            transform.localScale = Vector2.one;
            onShrooms = false;
            immortal = 2;
            GetComponent<SpriteRenderer>().sortingOrder = 0;
        }
    }



}
