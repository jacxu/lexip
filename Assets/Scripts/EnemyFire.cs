using UnityEngine;
using System.Collections;
using System;

public class EnemyFire : MonoBehaviour {

    public bool canFire;
    private float nextFire;
    public GameObject enemyBolt;
         

    // Use this for initialization
    void Start() {
        nextFire = UnityEngine.Random.Range(0.0F, 3.0F);
    }

    void Update()
    {
        if (CanFire() && Time.time > nextFire)
        {
            nextFire = Time.time + UnityEngine.Random.Range(0.0F, 3.0F);
            Instantiate(enemyBolt, transform.position, transform.rotation);
        }
    }

    bool CanFire()
    {
        Vector3 fwd = transform.TransformDirection(Vector3.back);
        if (Physics.Raycast(transform.position + Vector3.back, fwd, 1))
            return false;   
        return true;
    }

	

}
