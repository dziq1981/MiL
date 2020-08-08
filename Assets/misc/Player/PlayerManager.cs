using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    private bool player2spawned = false;
    GameObject player2, player1;
    public bool Player2spawned { get => player2spawned;}
    public GameObject Player2 { get => player2;}
    public GameObject Player1 { get => player1; }

    public static PlayerManager me;
    // Start is called before the first frame update
    void Start()
    {
        if (FindObjectsOfType<PlayerManager>().Length>1)
        {
            Destroy(gameObject);
            return;
        }

        player2 = null;// FindObjectOfType<Player2Controls>().gameObject;
        player1 = null;// FindObjectOfType<PlayerControls>().gameObject;
        //player2.SetActive(false);
        DontDestroyOnLoad(gameObject);
        SceneManager.activeSceneChanged += SceneManager_activeSceneChanged;
        me = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (player2==null)
        {
            player2 = FindObjectOfType<Player2Controls>()?.gameObject;
            player2?.SetActive(player2spawned);
            me.player2 = player2;
        }
        if (player1 == null)
        {
            player1 = FindObjectOfType<PlayerControls>()?.gameObject;
            me.player1 = player1;
        }
        if (Input.GetButtonDown("EnterPlayer2") && !player2spawned)
        {            
            player2.SetActive(true);
            player2spawned = true;
            player2.transform.position = new Vector3(player1.transform.position.x, 4, player1.transform.position.z);
            player2.GetComponent<Player2Controls>().playerSpawnPosition = new Vector3(player1.transform.position.x, 4, player1.transform.position.z);
            me.player2 = player2;
            me.player2spawned = true;
        }
        
    }

    private void SceneManager_activeSceneChanged(Scene arg0, Scene arg1)
    {        
        if (arg1.name.ToLower().Contains("gameover"))
        {
            Debug.Log("Player manager should die!");
            Destroy(FindObjectOfType<PlayerManager>().gameObject,1);
            
        }
        else
        {
            player1 = null;
            player2 = null;
            Debug.Log("nullyfing player2!");
        }
    }
}
