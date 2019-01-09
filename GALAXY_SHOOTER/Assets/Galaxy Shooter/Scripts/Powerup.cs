using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{

    [SerializeField]
    private float _speed = .05f;
    [SerializeField]
    private int powerupID;
    [SerializeField]
    private AudioClip _clip;

    private GameManager _gameManager;


    private void Start()
    {
      _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
    }

    void Update()
    {

        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        
        if (transform.position.y < -8 )
        {
            Destroy(this.gameObject);
        }
        
        if (_gameManager.gameOver == true)        
        {
            Destroy(this.gameObject);
        }

         if (_gameManager.pause == true)
        {
            Time.timeScale = 0;
        }

        else {
            Time.timeScale = 1;
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collided with :" + other.name);

        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position, 1f);

            if (player != null)
            {
                if (powerupID == 0)
                {
                    player.TripleShotPowerupOn();
                }

                else if (powerupID == 1)
                {
                    player.DoubleSpeedPowerUpOn();
                }
                
                else if (powerupID == 2)
                {
                    player.ShieldPowerupOn();
                }

                Destroy(this.gameObject);
            }
        }
    }
   
}