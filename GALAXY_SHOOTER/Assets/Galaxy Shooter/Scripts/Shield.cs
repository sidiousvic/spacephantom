using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{


    public int LifeBar = 1;

    void Start()
    {
        //StartCoroutine(ShieldPowerDownRoutine());

    }

    void Update()
    {
       
    }

    public void Damage()
    {
        LifeBar--;

        if (LifeBar < 1)
        {
            Destroy(this.gameObject);
        }
    }

    //public IEnumerator ShieldPowerDownRoutine()
    //{
    //    yield return new WaitForSeconds(5.0f);
    //    Destroy(this.gameObject);
    //}

}