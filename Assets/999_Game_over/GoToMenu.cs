using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GoToMenu : MonoBehaviour
{
    public float countDown;
    AudioSource audioSource;
    GameObject curtain;
    float fog = 0;
    // Start is called before the first frame update
    void Start()
    {
        countDown = 30;
        audioSource = FindObjectOfType<AudioSource>();
        audioSource.volume = 0;
        curtain = GameObject.FindGameObjectWithTag("Finish");
        curtain.GetComponent<Image>().color = new Color(0, 0, 0, 1);
    }

    // Update is called once per frame
    void Update()
    {
        countDown -= Time.deltaTime;
        if (fog <1)        
        {            
            fog += Time.deltaTime / 10;
            if (fog > 1) fog = 1;
            curtain.GetComponent<Image>().color = new Color(0, 0, 0, 1 - fog);
        }
        
        if (audioSource.volume <= 0.4) audioSource.volume += Time.deltaTime / 10;

        if (Input.GetButtonDown("Fire1")) countDown = 0;
        if (countDown<=0)
        {
             SceneManager.LoadScene("001_Menu",LoadSceneMode.Single);
        }
    }
}
