using UnityEngine;
using System.Collections;

public class FillController : MonoBehaviour {

    private GameController gm;

    void Start(){
        gm = MyController.GetGameController();
    }
	// Update is called once per frame
	void Update () {
        //UnityEngine.UI.Image img = GetComponent<UnityEngine.UI.Image>();
        //img.fillAmount = gm.Health;
	}
}
