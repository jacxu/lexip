using UnityEngine;
using System.Collections;

public class ShieldController : MonoBehaviour {

    private GameController gm;
	// Use this for initialization
	void Start () {
        gameObject.SetActive(false);
        gm = MyController.GetGameController();
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "EnemyBoltB")
        {
            if (tag == "ShieldBlue")
                gm.BoltBluePoint++;
            else if (tag == "ShieldRed")
                gm.BoltBluePoint--;
            Destroy(other.gameObject);
        }

        if (other.tag == "EnemyBoltR")
        {
            if (tag == "ShieldRed")
                gm.BoltRedPoint++;
            else if (tag == "ShieldBlue")
                gm.BoltRedPoint--;
            Destroy(other.gameObject);
        }
    }

}
