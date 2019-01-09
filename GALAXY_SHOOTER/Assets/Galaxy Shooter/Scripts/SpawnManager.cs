using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {

    [SerializeField]
    private GameObject _enemyPrefab;

    [SerializeField]
    private GameObject[] _powerups;

    private GameManager _gameManager;


    private void Start()
    {
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        
    }

    
    IEnumerator EnemySpawnRoutine()
    {
        while(_gameManager.gameOver == false)
        {
        Instantiate(_enemyPrefab, new Vector3(Random.Range(-8f, 8f), 6, 0), Quaternion.identity);
        yield return new WaitForSeconds(1.0f);

        } 
    }

    IEnumerator PowerupSpawnRoutine()
    {
        while (_gameManager.gameOver == false)
        {

            int randomPowerUp = Random.Range(0, 3);
            Instantiate(_powerups[randomPowerUp], new Vector3(Random.Range(-8f, 8f), 6, 0), Quaternion.identity);
            yield return new WaitForSeconds(10.0f);
        }
    }

    public void startSpawning()
    { 
            StartCoroutine(EnemySpawnRoutine());
            StartCoroutine(PowerupSpawnRoutine());
    }
    
    private void Update()
    {
        if(_gameManager.gameOver == true)
        {
            StopAllCoroutines();
        }      
    }
}