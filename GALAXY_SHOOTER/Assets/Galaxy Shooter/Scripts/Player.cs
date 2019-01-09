using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public bool canTripleShot = false;

    public bool doubleSpeed = false;

    public bool shieldActive = false;

    public int lives = 3;

    [SerializeField]
    private GameObject _laserPrefab;

    [SerializeField]
    private GameObject _tripleShotPrefab;

    [SerializeField]
    private GameObject _shieldPrefab;

    [SerializeField]
    private GameObject _laserCenterLocation;

    [SerializeField]
    private GameObject _ExplosionPrefab;

    [SerializeField]
    private GameObject[] _engineFires;
    
    [SerializeField]
    private float _fireRate = 0.25f;
    
    private float _canFire = 0.0f;
    
    [SerializeField]
    private float _speed = 5.0f;

    private UIManager _uIManager;
    private GameManager _gameManager;
    private SpawnManager _spawnManager;
    
    [SerializeField]
    private AudioSource _audioSource;

    private int hitCount = 0;
    
    private void Start()
    {
        transform.position = Vector3.zero;
       
        
        if (shieldActive == true)
        {
            Debug.Log("Shield Active");
            GameObject shield = Instantiate(_shieldPrefab);
            shield.transform.SetParent(this.transform, false);
        }
        
        _uIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
            
            if (_uIManager != null) 
            {
                _uIManager.UpdateLives(lives);
            }

        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
            
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        
        if (_spawnManager != null)
        {
            _spawnManager.startSpawning();
        }

        _audioSource = GetComponent<AudioSource>();

        hitCount = 0;
        
    }

    private void Update()
    {
        Positions();
        

        if (Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0))
        {
            Shoot();
        }
        
         if (_gameManager.pause == true)
        {
            Time.timeScale = 0;
            _audioSource.volume = 0;
            
        }

        else {
            Time.timeScale = 1;
            _audioSource.volume = 1;
        }
    }

    private void FixedUpdate() 
    {        
        Movement();
    }



    private void Shoot()
    {


        if (Time.time > _canFire)
        {
            _audioSource.Play();

            if (canTripleShot == true)
            {
                if (_gameManager.pause == false)
                {
                    Instantiate(_tripleShotPrefab, _laserCenterLocation.transform.position, transform.rotation);
                }
            }

            else
            {
                if (_gameManager.pause == false)
                {
                    Instantiate(_laserPrefab, _laserCenterLocation.transform.position, transform.rotation);
                }
            }
            
            _canFire = Time.time + _fireRate;
        }

    }
    
    private void Shield()
    {
        if (shieldActive == true)
        {
            Debug.Log("Shield Active");
            GameObject shield = Instantiate(_shieldPrefab);
            shield.transform.SetParent(this.transform, false);
        }
    }

    
    private void Positions()
    {
        if (transform.position.y > 4)
        {
            transform.position = new Vector3(transform.position.x, 4, 0);
        }
        
        else if (transform.position.y < -4)
        {
            transform.position = new Vector3(transform.position.x, -4, 0);
        }
        
        if (transform.position.x < -9)
        {
            transform.position = new Vector3(9, transform.position.y, 0);
        }
        
        else if (transform.position.x > 9)
        {
            transform.position = new Vector3(-9, transform.position.y, 0);
        }
    }
    
    private void Movement()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        if (doubleSpeed == true)
        {
            transform.Translate(Vector3.right * (_speed * 1.5f) * horizontalInput * Time.fixedDeltaTime, Space.World);
            transform.Translate(Vector3.up * (_speed * 1.5f) * verticalInput * Time.fixedDeltaTime, Space.World);
        }
        else
        {
            transform.Translate(Vector3.right * _speed * horizontalInput * Time.fixedDeltaTime, Space.World);
            transform.Translate(Vector3.up * _speed * verticalInput * Time.fixedDeltaTime, Space.World);
        }

        if (horizontalInput > 0)
        {
            transform.Rotate(new Vector3(0, 0, -1) * horizontalInput * Time.fixedDeltaTime * 400);
        }

        else if (horizontalInput < 0)
        {
            transform.Rotate(new Vector3(0, 0, -1) * horizontalInput * Time.fixedDeltaTime * 400);
        }  
        
        Vector3 eulerAngles = transform.rotation.eulerAngles;
        
        if (eulerAngles.z > 45 && eulerAngles.z < 180)
        {
            transform.rotation = Quaternion.AngleAxis(-315, new Vector3(0, 0, 1));
        }

        else if (eulerAngles.z < 315 && eulerAngles.z > 180)
        {
            transform.rotation = Quaternion.AngleAxis(-315, new Vector3(0, 0, -1));
        }
        
    }
    public void TripleShotPowerupOn()
    {
        canTripleShot = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }
    public IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        canTripleShot = false;
    }
    
    public void DoubleSpeedPowerUpOn()
    {
        doubleSpeed = true;
        StartCoroutine(DoubleSpeedPowerDownRoutine());
    }
    public IEnumerator DoubleSpeedPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        doubleSpeed = false;
    }
    
    public void ShieldPowerupOn()
    {
        shieldActive = true;
        Shield();
    }
    
    public void Damage() 
    {

        hitCount++;
        
        if(hitCount == 1)
        {
            _engineFires[0].SetActive(true);
        }

        else if (hitCount == 2)
        {
            _engineFires[1].SetActive(true);
        }
        
        if (shieldActive == false)
        lives -= 1 ;
        
        
        if (shieldActive == true)
        
        lives--;
        _uIManager.UpdateLives(lives);
        _uIManager.ScoreDown();
        
        
        if (lives < 1)
        {
            Instantiate(_ExplosionPrefab, transform.position, Quaternion.identity);
            _gameManager.gameOver = true;
            _uIManager.ShowTitleScreen();
            Destroy(this.gameObject);
            
        }
    }
}


 
    
