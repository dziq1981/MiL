using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class message_blink : MonoBehaviour
{
    public Color textColor;
    public float loopTime = 2;

    public float countDown = 0;
    public float startTime = 0;
    public float halfTime = 0;
    Text text; 

    // Start is called before the first frame update
    void Start()
    {
        halfTime = loopTime / 2;
        text = GetComponent<Text>();
        countDown = loopTime + 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerManager.me.Player2spawned)
        {
            Destroy(gameObject);
        }
        if (countDown >= loopTime) startTime = Time.time;
        countDown = Time.time - startTime;
        if (countDown > halfTime) text.color = new Color(0, 0, 0, 0);
        else text.color = textColor;
    }
}
