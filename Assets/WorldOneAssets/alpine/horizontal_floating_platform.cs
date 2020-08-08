using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class horizontal_floating_platform : MonoBehaviour
{
    public float maxx = 0;
    public float minx = 0;
    public float speed = 1f;
    public bool goingRight = true;
    private Vector3 startPos;
    private float currentX = 0;
    // Start is called before the first frame update
    void Start()
    {
        startPos = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        currentX += speed * Time.deltaTime * (goingRight ? 1 : -1);
        if ((currentX > maxx && goingRight) || (currentX < minx && !goingRight)) goingRight = !goingRight;
        gameObject.transform.position = startPos + new Vector3(currentX, 0, 0);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {

        //collision.otherCollider.transform.position += new Vector3(speed * Time.deltaTime * (goingRight ? 1 : -1), 0, 0);
        Debug.Log(collision.otherCollider.ToString());
        Debug.Log("Collision");
    }
}
