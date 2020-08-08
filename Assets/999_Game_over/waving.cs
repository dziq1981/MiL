using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waving : MonoBehaviour
{
    float t;
    // Start is called before the first frame update
    void Start()
    {
        t = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        t = Time.time;
        transform.localScale = new Vector3(7.5f + 2.75f*Mathf.Sin(t*1.25f), 5f + 2.75f*Mathf.Cos(t*1.5f), 0);
    }
}
