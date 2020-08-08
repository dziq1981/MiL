using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    public float LoadAfter = 0f;

    private void Start()
    {
        if (LoadAfter == 0)
        {
            Debug.Log("loadAfter: " + LoadAfter.ToString());
        }
        else
        {
            Invoke("LoadNextLevel", LoadAfter);
        }
    }

        public void LoadLevel(string name)
    {
        Debug.Log("LeveL load requested for: "+name);
        SceneManager.LoadScene(name);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (SceneManager.GetActiveScene().name.ToUpper() == "START")
            {
                Application.Quit();
            }
            else
            {
                LoadLevel("999_GameOver");                
            }
        }

    }

    public void QuitRequest()
    {
        Debug.Log("Quit request");
        Application.Quit();
    }

    public void LoadNextLevel()
    {        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void callGameOver()
    {        
        SceneManager.LoadScene("999_GameOver", LoadSceneMode.Single);        
    }
}
