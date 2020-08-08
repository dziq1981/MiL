using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floatingRock : MonoBehaviour
{
    public float maxDisplacement;
    Vector2 defaultPos;
    bool inCollision = false;
    float timer=0;
    // Start is called before the first frame update
    void Start()
    {
        defaultPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!inCollision && transform.position.y<defaultPos.y)
        {
            transform.position += new Vector3(0, Time.deltaTime,0);
        }
        else if (inCollision && defaultPos.y - transform.position.y<maxDisplacement)
        {
            transform.position += new Vector3(0, -Time.deltaTime, 0);            
        }
        if (Time.time - timer > 1f)
        {
            inCollision = false;
        }
    }


    private void OnCollisionStay2D(Collision2D collision)
    {
        timer = Time.time;
        inCollision = true;
    }
    
}
