using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class curtainAppear : MonoBehaviour
{
    public bool getDark = false;
    public bool getLight = true;
    public bool isLight = false;
    float fog = 0;
    Image image;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (getLight)
        {
            fog += Time.deltaTime;
            if (fog > 0.5)  isLight = true;
            if (fog > 1)
            {
                fog = 1;
                getLight = false;
               
            }
            image.color = new Color(0, 0, 0, 1 - fog);
        }
        if (getDark)
        {
            
            fog -= Time.deltaTime / 2;
            if (fog < 0)
            {
                fog = 0;
                isLight = false;
                getDark = false;
                FindObjectOfType<LevelManager>().callGameOver();
            }
            image.color = new Color(0, 0, 0, 1-fog);
        }
    }
}
