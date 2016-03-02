using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour {

    public float speed;

	// Use this for initialization
	void Start () {
        SpeedUpdate();
    }

    void Update(){
        SpeedUpdate();
    }

    void SpeedUpdate()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;
    }

    public void SetSpeed(int _speed)
    {
        speed = _speed;
    }
	
	
}
