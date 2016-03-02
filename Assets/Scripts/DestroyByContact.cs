using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DestroyByContact : MonoBehaviour {

    public GameObject explosion;
    public int scoreValue;
    private GameController gm;
    private PlayerController pc;

    private List<string> notTriggerTags = new List<string>() { "Boundary", "EnemyBolt" };


    void Start()
    {
        gm = MyController.GetGameController();
        pc = MyController.GetPlayerController();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Boundary")
            return;

        if (other.tag.Contains("EnemyBolt"))
            return;

        Debug.Log("other.tag:" + other.tag);
        Debug.Log("tag:" + tag);
        if (( other.tag == "Blue" && tag  == "Red") || (other.tag == "Red" && tag == "Blue") )
        {
            Debug.Log("bad hit!");
            gm.UpdateStats(-scoreValue, -scoreValue);
            Mover mov = other.GetComponent<Mover>();
            mov.SetSpeed(-50);
            return;
        }

        gm.UpdateStats(0,scoreValue);
        gm.RemoveEnemy(gameObject);

        Instantiate(explosion, transform.position, transform.rotation);

        if (other.tag == "Player")
            gm.GameOver();
        else
            Destroy(other.gameObject);

        Destroy(gameObject);
        Destroy(transform.parent.gameObject);

    }
}
