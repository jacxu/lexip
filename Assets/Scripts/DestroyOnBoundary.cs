using UnityEngine;
using System.Collections;

public class DestroyOnBoundary : MonoBehaviour {

	void OnTriggerExit(Collider other)
    {
        Destroy(other.gameObject);
    }
}
