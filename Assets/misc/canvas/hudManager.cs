using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class hudManager : MonoBehaviour
{
    private const string sc = "SCORE: ";
    private const string ti = "TIME: ";

    public float timeLeft = 5;
    public int timeToShow;
    public int score = 0;
    public int initialScore;

    private GameObject timeDisplay;
    private GameObject scoreDisplay;

    Text timeText;
    Text scoreText;

    public int lives = 3;
    public static hudManager me = null;

    

    // Start is called before the first frame update
    void Start()
    {
        
        if (FindObjectsOfType<hudManager>().Length > 1)
        {
            Destroy(this);
            return;
        }
        me = this;
        initialScore = 0;
        timeToShow = Mathf.RoundToInt(timeLeft);
        timeDisplay = FindObjectOfType<timeObject>().gameObject;
        scoreDisplay = FindObjectOfType<scoreObject>().gameObject;
        timeText = timeDisplay.GetComponent<Text>();
        scoreText = scoreDisplay.GetComponent<Text>();
        DontDestroyOnLoad(this);
        SceneManager.activeSceneChanged += SceneManager_activeSceneChanged;
    }

    private void SceneManager_activeSceneChanged(Scene arg0, Scene arg1)
    {
        timeLeft = 500;
        if (arg1.name.ToLower().Contains("gameover"))
        {
            Debug.Log("gameover");
            GameObject.Destroy(FindObjectOfType<hudManager>().gameObject);
            me = null;
        }
        else
        {
            scoreDisplay = null;
            timeDisplay = null;
            initialScore = score;
            Debug.Log("nullyfing!");
        }
    }

    private void findMeMyTexts(Scene arg1)
    {
        GameObject[] gos = arg1.GetRootGameObjects();
        foreach (var go in gos)
        {
            if (go.name == "Canvas")
            {
                foreach (Text txt in go.GetComponentsInChildren<Text>())
                {
                    GameObject g = txt.gameObject;
                    Debug.Log(g.name);
                    if (g.tag == "scoreDisplay") scoreDisplay = g;
                    if (g.tag == "timeDisplay") timeDisplay = g;
                }
                break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (scoreDisplay == null || timeDisplay == null)
        {
            findMeMyTexts(SceneManager.GetActiveScene());
            Debug.Log("trying to find objects");
            timeText = timeDisplay.GetComponent<Text>();
            scoreText = scoreDisplay.GetComponent<Text>();
            return;
        }
        timeLeft -= Time.deltaTime;
        timeToShow = Mathf.RoundToInt(timeLeft);
        scoreText.text = sc + score.ToString().PadLeft(8, '0');
        timeText.text = ti + timeToShow.ToString().PadLeft(3,'0');
        if (timeToShow<=0)
        {
            FindObjectOfType<PlayerControls>().playerDying();
            timeLeft = 50;
        }
    }



}
