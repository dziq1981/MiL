using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class canvasKnighManager : MonoBehaviour
{
    
    public GameObject liveImage;
    private hudManager hud;

    // Start is called before the first frame update
    void Start()
    {
        hud = FindObjectOfType<hudManager>();        
        generateLives();
    }

    private void generateLives()
    {
        for (int i = 0; i < hud.lives; i++)
        {
            GameObject go = GameObject.Instantiate(liveImage,transform);
            go.GetComponent<RectTransform>().localPosition = new Vector3(i * (-130) - 65, 0, 0);
        }
    }

    public int takeLive()
    {
        hud.lives--;
        singleLive[] singleLives = FindObjectsOfType<singleLive>();
        foreach (var sl in singleLives)
        {
            Destroy(sl.gameObject);
        }
        generateLives();
        return hud.lives;
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
