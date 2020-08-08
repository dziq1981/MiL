using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class portal : MonoBehaviour
{
    public Animator animator;    
    public int minScoreToOpen;
    public Text textInfo;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ((hudManager.me.score-hudManager.me.initialScore)>=minScoreToOpen)
        {
            animator.SetBool("stable", true);
            GetComponent<PolygonCollider2D>().enabled = true;
            GameObject.Destroy(textInfo);
        }
        else
        {
            textInfo.text = (minScoreToOpen - FindObjectOfType<hudManager>().score+hudManager.me.initialScore).ToString();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag=="Player") FindObjectOfType<LevelManager>().LoadNextLevel();
    }
}
