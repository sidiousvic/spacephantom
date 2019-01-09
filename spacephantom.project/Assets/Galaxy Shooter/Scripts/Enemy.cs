using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField]
    private float _speed = .05f;

    [SerializeField]
    private GameObject _Enemy_ExplosionPrefab;

    [SerializeField]
    private AudioClip _clip;

    public int EnemyLives = 5;
    
    private UIManager _uIManager;
   private GameManager _gameManager;

    private void Start()
    {
        _uIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();

        _clip = GetComponent<AudioClip>();
        
    }


    void Update()
    {

        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        
        EnemyRespawn();
        
         if (_gameManager.pause == true)
        {
            Time.timeScale = 0;
        }

        else {
            Time.timeScale = 1;
        }
       
    }
    
    void EnemyRespawn()
    {
        if (transform.position.y < -6)
        {
            _uIManager.ScoreDown();
            Destroy(this.gameObject);
            //float randomX = Random.Range(-8, 8);
            //transform.position = new Vector3(randomX, 6, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collided with :" + other.name);

        if (other.tag == "Laser")
        {
            if(other.transform.parent != null)
            {
                Destroy(other.transform.parent.gameObject);                
            }
            EnemyLives -= 1;            

            if (EnemyLives < 1)
            {
                Instantiate(_Enemy_ExplosionPrefab, transform.position, transform.rotation);
                _uIManager.ScoreUp();
                //AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position, 1f);
                Destroy(this.gameObject);
            }
            Destroy(other.gameObject);
       
        }        
        
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();

            if (player != null)
            {
                player.Damage();
                Instantiate(_Enemy_ExplosionPrefab, transform.position, transform.rotation);
                _uIManager.ScoreUp();
                //AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position, 1f);
                Destroy(this.gameObject);                
            }
            
        }
        
         if (other.tag == "Shield")
        {    
            Shield shield = other.GetComponent<Shield>();

            if (other.transform != null)
            {
                Instantiate(_Enemy_ExplosionPrefab, transform.position, transform.rotation);
                shield.Damage();
                _uIManager.ScoreUp();
                Destroy(this.gameObject);
                
            }
            
        }
    }
}
