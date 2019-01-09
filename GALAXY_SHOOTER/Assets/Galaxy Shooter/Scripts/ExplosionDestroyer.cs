using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionDestroyer : MonoBehaviour
{

    [SerializeField]
    private float _speed = .5f;
    
    void Start()
    {
        Destroy(this.gameObject, 0.7f);    
    }

    private void Update()
    {
         transform.Translate(Vector3.up * _speed * Time.deltaTime);
    }
}
