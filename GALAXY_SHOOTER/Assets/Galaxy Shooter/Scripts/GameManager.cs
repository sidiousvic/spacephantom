using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public bool gameOver = true;
    public bool pause = false;
    public GameObject player;

    private UIManager _uiManager;

    

    void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    void Update()
    {

        if (gameOver == true)
        {
            if (Input.GetKey(KeyCode.Return))
            {
                Instantiate(player, Vector3.zero, Quaternion.identity);
                gameOver = false;
                _uiManager.HideTitleScreen();                
                
            }
        }
        
        if (Input.GetKeyDown(KeyCode.RightShift))
        {
            pause = !pause;
        }
    }
}

